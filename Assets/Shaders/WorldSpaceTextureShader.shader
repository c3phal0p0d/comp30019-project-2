// World space texture conversion code inspired by https://www.blog.radiator.debacle.us/2012/01/joys-of-using-world-space-procedural.html
// Texture mapping & lighting code inspired by https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html and https://en.wikibooks.org/wiki/Cg_Programming/Unity/Smooth_Specular_Highlights

Shader "Unlit/WorldSpaceTextureShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
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

        Pass
        {
            Tags {"LightMode"="ForwardBase"}    // pass for ambient light and first light source

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

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
                half3 tspace0 : TEXCOORD2;
                half3 tspace1 : TEXCOORD3;
                half3 tspace2 : TEXCOORD4;
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

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv;
                float3 normal = i.worldNormal;
                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                float3 lightDirection;
                float attenuation;
        
                // Determine which side the texture is on
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

                // Normal mapping
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, uv * _Scale));
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);
                worldNormal = lerp(worldNormal, float3(1,1,1), -_BumpScale + 1);

                // Get direction of point light
                float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - i.worldPos.xyz;
                float distance = length(vertexToLightSource);
                attenuation = 1.0 / distance;
                lightDirection = normalize(vertexToLightSource);

                half3 worldReflection = reflect(-worldViewDir, worldNormal);

                // Diffuse lighting
                fixed4 diff = _LightColor0 * max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

                // Ambient lighting
                diff.rgb += ShadeSH9(half4(worldNormal,1));

                float3 specular;
                if (dot(worldNormal, lightDirection) < 0.0){
                    // Light source on other side so no specular reflection  
                    specular = float3(0.0, 0.0, 0.0); 
                } else {
                    specular = attenuation * _LightColor0.rgb * pow(max(0.0, dot(reflect(-lightDirection, worldNormal), worldViewDir)), _Shininess);
                }

                fixed4 albedo = tex2D(_MainTex, uv * _Scale);
                fixed occlusion = tex2D(_OcclusionMap, uv * _Scale).r;

                fixed4 color;
                color.rgb = diff * albedo * occlusion + specular;
                return color;
            }

            ENDCG
        }

        Pass 
        {	
            Tags { "LightMode" = "ForwardAdd" }     // pass for additional light sources
            Blend One One
 
            CGPROGRAM
            #pragma vertex vert  
            #pragma fragment frag 
 
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

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
                half3 tspace0 : TEXCOORD2;
                half3 tspace1 : TEXCOORD3;
                half3 tspace2 : TEXCOORD4;
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
 
            v2f vert(appdata v) 
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
 
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv;
                float3 normal = i.worldNormal;
                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                float3 lightDirection;
                float attenuation;
        
                // Determine which side the texture is on
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

                // Get direction of point light
                float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - i.worldPos.xyz;
                float distance = length(vertexToLightSource);
                attenuation = 1.0 / distance;
                lightDirection = normalize(vertexToLightSource);

                // Normal mapping
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, uv * _Scale));
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);
                worldNormal = lerp(worldNormal, float3(1,1,1), -_BumpScale + 1);
                normalize(worldNormal);

                half3 worldReflection = reflect(-worldViewDir, worldNormal);

                // Diffuse lighting
                fixed4 diff = attenuation *_LightColor0 * max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

                float3 specular;
                if (dot(worldNormal, lightDirection) < 0.0){
                    // Light source on other side so no specular reflection  
                    specular = float3(0.0, 0.0, 0.0); 
                } else {
                    specular = attenuation * _LightColor0.rgb * pow(max(0.0, dot(reflect(-lightDirection, worldNormal), worldViewDir)), _Shininess);
                }

                fixed4 color;
                color.rgb = diff;
                return color;
         }
 
         ENDCG
        }
    }
    Fallback "Specular"
}
