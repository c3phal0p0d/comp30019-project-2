// World space texture conversion code inspired by https://www.blog.radiator.debacle.us/2012/01/joys-of-using-world-space-procedural.html
// Texture mapping & lighting code inspired by https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html and series of tutorials at https://catlikecoding.com/unity/tutorials/rendering/ 

Shader "Unlit/WorldSpaceTexture"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
        [Gamma] _Metallic ("Metallic", Range(0, 1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _BumpMap ("Normal", 2D) = "white" {}
        _BumpScale ("Depth", Range(-1,3)) = 0.0
        _ParallaxMap ("Height", 2D) = "white" {}
        _OcclusionMap ("Ambient Occlusion", 2D) = "white" {}
        _Shininess ("Shininess", Float) = 10
        _Scale ("Texture Scale", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // pass for ambient light and first light source
        Pass
        {
            Tags {"LightMode"="ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

            #include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal: NORMAL;
                float4 tangent: TANGENT;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;

                // Vectors that will hold a 3x3 rotation matrix that transforms from tangent to world space
                half3 tspace0 : TEXCOORD2;
                half3 tspace1 : TEXCOORD3;
                half3 tspace2 : TEXCOORD4;

                SHADOW_COORDS(5)
            };

            sampler2D _MainTex;
            sampler2D _BumpMap;
            sampler2D _MetallicGlossMap;
            sampler2D _ParallaxMap;
            sampler2D _OcclusionMap;
            
            fixed4 _Color;
            float _BumpScale;
            float _Parallax;
            float _Scale;
            float _Shininess;
            float _Metallic;
            float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half3 worldNormal = o.worldNormal;

                // Used for normal mapping
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 worldBitangent = cross(worldNormal, worldTangent) * tangentSign;
                o.tspace0 = half3(worldTangent.x, worldBitangent.x, worldNormal.x);
                o.tspace1 = half3(worldTangent.y, worldBitangent.y, worldNormal.y);
                o.tspace2 = half3(worldTangent.z, worldBitangent.z, worldNormal.z);

                TRANSFER_SHADOW(o)

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv;
                float3 normal = i.worldNormal;

                // Determine which side of the wall the texture is on
                if (abs(normal.x)>0.5) {
                    // side of wall
                    uv = i.worldPos.yz;
                } else if (abs(normal.z)>0.5) {
                    // front of wall
                    uv = i.worldPos.xy;
                } else {
                    // top of wall
                    uv = i.worldPos.xz;
                }


                /* LIGHTING */
                // Normal map
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, uv * _Scale));
                // Transform normal from tangent to world space
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);
                worldNormal = lerp(worldNormal, float3(1,1,1), -_BumpScale + 1);

                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfVector = normalize(lightDir + viewDir);

                float3 lightVector = _WorldSpaceLightPos0.xyz - i.worldPos;
	            float attenuation = 1 / (1 + dot(lightVector, lightVector));

                float3 albedo = tex2D(_MainTex, uv * _Scale).rgb;
                fixed occlusion = tex2D(_OcclusionMap, uv * _Scale).r;
                float oneMinusReflectivity;
                float3 specularTint;
				albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);
    
                float3 lightColor = _LightColor0.rgb * attenuation;
                float3 specular = specularTint.rgb * lightColor * pow(DotClamped(halfVector, worldNormal), _Smoothness * 100);
                float3 diffuse = lightColor * DotClamped(lightDir, worldNormal);
                diffuse += max(0, ShadeSH9(float4(worldNormal, 1)));
                diffuse *= 1.2*albedo;
                diffuse *= occlusion;

                fixed shadow = SHADOW_ATTENUATION(i);

                float4 color = float4((diffuse + specular)*shadow, 1); 

                return color;
            }

            ENDCG
        }

        // pass for additional light sources
        Pass 
        {	
            Tags { "LightMode" = "ForwardAdd" }
            Blend One One
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

            #include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal: NORMAL;
                float4 tangent: TANGENT;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;

                // Vectors that will hold a 3x3 rotation matrix that transforms from tangent to world space
                half3 tspace0 : TEXCOORD2;
                half3 tspace1 : TEXCOORD3;
                half3 tspace2 : TEXCOORD4;
                
                SHADOW_COORDS(5)
            };

            sampler2D _MainTex;
            sampler2D _BumpMap;
            sampler2D _MetallicGlossMap;
            sampler2D _ParallaxMap;
            sampler2D _OcclusionMap;
            
            fixed4 _Color;
            float _BumpScale;
            float _Parallax;
            float _Scale;
            float _Shininess;
            float _Metallic;
            float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half3 worldNormal = o.worldNormal;

                // Used for normal mapping
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 worldBitangent = cross(worldNormal, worldTangent) * tangentSign;
                o.tspace0 = half3(worldTangent.x, worldBitangent.x, worldNormal.x);
                o.tspace1 = half3(worldTangent.y, worldBitangent.y, worldNormal.y);
                o.tspace2 = half3(worldTangent.z, worldBitangent.z, worldNormal.z);

                TRANSFER_SHADOW(o)

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv;
                float3 normal = i.worldNormal;
    
                // Determine which side of the wall the texture is on
                if (abs(normal.x)>0.5) {
                    // side of wall
                    uv = i.worldPos.yz;
                } else if (abs(normal.z)>0.5) {
                    // front of wall
                    uv = i.worldPos.xy;
                } else {
                    // top of wall
                    uv = i.worldPos.xz;
                }

                /* LIGHTING */
                // Normal map
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, uv * _Scale));
                // transform normal from tangent to world space
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);
                worldNormal = lerp(worldNormal, float3(1,1,1), -_BumpScale + 1);

                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfVector = normalize(lightDir + viewDir);

                float3 lightVector = _WorldSpaceLightPos0.xyz - i.worldPos;
	            float attenuation = 1 / (1 + dot(lightVector, lightVector));

                float3 albedo = tex2D(_MainTex, uv * _Scale).rgb;
                fixed occlusion = tex2D(_OcclusionMap, uv * _Scale).r;
                float oneMinusReflectivity;
                float3 specularTint;
				albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

                float3 lightColor = _LightColor0.rgb * attenuation;
                float3 specular = specularTint.rgb * lightColor * pow(DotClamped(halfVector, worldNormal), _Smoothness * 100);
                float3 diffuse = lightColor * DotClamped(lightDir, worldNormal);
                diffuse += 1.2*max(0, ShadeSH9(float4(worldNormal, 1)));
                diffuse *= albedo;
                diffuse *= occlusion;

                fixed shadow = SHADOW_ATTENUATION(i);

                float4 color = float4((diffuse + specular)*shadow, 1); 

                return color;
            }

            ENDCG
        }

        // pass for shadow casting
        Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster

            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    Fallback "Specular"
}
