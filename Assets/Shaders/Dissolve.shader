// Dissolve effect code inspired by https://lindenreidblog.com/2017/12/16/dissolve-shader-in-unity/
// Texture mapping & lighting code inspired by https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html and series of tutorials at https://catlikecoding.com/unity/tutorials/rendering/ 

Shader "Unlit/Dissolve"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo", 2D) = "white" {}
        [Gamma] _Metallic ("Metallic", Range(0, 1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _OcclusionMap("Occlusion", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpScale ("Depth", Range(-1,3)) = 0.0
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _DissolveSpeed("Dissolve Speed", float) = 1.0
        _DissolveColor1("Dissolve Color 1", Color) = (1, 1, 1, 1)
		_DissolveColor2("Dissolve Color 2", Color) = (1, 1, 1, 1)
		_ColorThreshold1("Color Threshold 1", float) = 1.0
		_ColorThreshold2("Color Threshold 2", float) = 1.0
        _StartTime("Start Time", float) = 1.0
        _ElapsedTime("Elapsed Time", float) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}

            CGPROGRAM

            #pragma target 3.0

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal: NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;

                // Vectors that will hold a 3x3 rotation matrix that transforms from tangent to world space
                half3 tspace0 : TEXCOORD3;
                half3 tspace1 : TEXCOORD4;
                half3 tspace2 : TEXCOORD5;
            };

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Metallic;
            float _Smoothness;
            sampler2D _OcclusionMap;
            sampler2D _BumpMap;
            float _BumpScale;
            sampler2D _NoiseTex;
            float _DissolveSpeed;
            float4 _DissolveColor1;
			float4 _DissolveColor2;
            float _ColorThreshold1;
			float _ColorThreshold2;
            float _StartTime;
            float _ElapsedTime;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                half3 tangent = UnityObjectToWorldDir(v.tangent.xyz);

                // compute bitangent from cross product of normal and tangent
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 bitangent = cross(o.normal, tangent) * tangentSign;

                // output the tangent space matrix
                o.tspace0 = half3(tangent.x, bitangent.x, v.normal.x);
                o.tspace1 = half3(tangent.y, bitangent.y, v.normal.y);
                o.tspace2 = half3(tangent.z, bitangent.z, v.normal.z);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                /* LIGHTING */
                /*
                // Normal map
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
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

                float3 albedo = tex2D(_MainTex, i.uv).rgb;
                float oneMinusReflectivity;
                float3 specularTint;
				albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

                float3 lightColor = _LightColor0.rgb * attenuation;
                float3 specular = specularTint.rgb * lightColor * pow(DotClamped(halfVector, worldNormal), _Smoothness * 100);
                float3 diffuse = lightColor * DotClamped(lightDir, worldNormal);
                fixed occlusion = tex2D(_OcclusionMap, i.uv).r;
                diffuse += 1.5*max(0, ShadeSH9(float4(worldNormal, 1)));
                diffuse *= albedo;
                diffuse *= occlusion;

                float4 color = float4(diffuse + specular, 1); 
                */
                float4 color = _DissolveColor2;

                /* DISSOLVE EFFECT */
                // sample noise texture
                float noiseSample = tex2Dlod(_NoiseTex, float4(i.uv.xy, 0, 0));

                // dissolve colors
                float threshold2 = _ElapsedTime * _ColorThreshold2 - _StartTime;
				float useDissolve2 = noiseSample - threshold2 < 0;
				color = (1-useDissolve2)*color + useDissolve2*_DissolveColor2;

                float threshold1 = _ElapsedTime * _ColorThreshold1 - _StartTime;
				float useDissolve1 = noiseSample - threshold1 < 0;
				color = (1-useDissolve1)*color + useDissolve1*_DissolveColor1;

                float threshold = _ElapsedTime * _DissolveSpeed;
                clip(noiseSample - threshold);
                 
                return color;
            }
            ENDCG
		}

        Pass
        {
            Tags { "LightMode" = "ForwardAdd" }     // pass for additional light sources
            Blend One One

            CGPROGRAM

            #pragma target 3.0

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal: NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;

                // Vectors that will hold a 3x3 rotation matrix that transforms from tangent to world space
                half3 tspace0 : TEXCOORD3;
                half3 tspace1 : TEXCOORD4;
                half3 tspace2 : TEXCOORD5;
            };

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Metallic;
            float _Smoothness;
            sampler2D _OcclusionMap;
            sampler2D _BumpMap;
            float _BumpScale;
            sampler2D _NoiseTex;
            float _DissolveSpeed;
            float4 _DissolveColor1;
			float4 _DissolveColor2;
            float _ColorThreshold1;
			float _ColorThreshold2;
            float _StartTime;
            float _ElapsedTime;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                half3 tangent = UnityObjectToWorldDir(v.tangent.xyz);

                // compute bitangent from cross product of normal and tangent
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 bitangent = cross(o.normal, tangent) * tangentSign;

                // output the tangent space matrix
                o.tspace0 = half3(tangent.x, bitangent.x, v.normal.x);
                o.tspace1 = half3(tangent.y, bitangent.y, v.normal.y);
                o.tspace2 = half3(tangent.z, bitangent.z, v.normal.z);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                /* LIGHTING */
                /*
                i.normal = normalize(i.normal);

                // Normal map
                // sample the normal map, and decode from the Unity encoding
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
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
	            float attenuation = 1 / (1 + dot(lightVector, lightVector)) * 3;

                float3 albedo = tex2D(_MainTex, i.uv).rgb;
                float oneMinusReflectivity;
                float3 specularTint;
				albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

                float3 lightColor = _LightColor0.rgb * attenuation;
                float3 specular = specularTint.rgb * lightColor * pow(DotClamped(halfVector, worldNormal), _Smoothness * 100);
                float3 diffuse = lightColor * DotClamped(lightDir, worldNormal);
                diffuse += 1.5*max(0, ShadeSH9(float4(worldNormal, 1)));
                diffuse *= albedo;

                float4 color = float4(diffuse + specular, 1); 
                */
                float4 color = _DissolveColor2;

                /* DISSOLVE EFFECT */
                // sample noise texture
                float noiseSample = tex2Dlod(_NoiseTex, float4(i.uv.xy, 0, 0));

                // dissolve colors
                float threshold2 = _ElapsedTime * _ColorThreshold2 - _StartTime;
				float useDissolve2 = noiseSample - threshold2 < 0;
				color = (1-useDissolve2)*color + useDissolve2*_DissolveColor2;

                float threshold1 = _ElapsedTime * _ColorThreshold1 - _StartTime;
				float useDissolve1 = noiseSample - threshold1 < 0;
				color = (1-useDissolve1)*color + useDissolve1*_DissolveColor1;

                float threshold = _ElapsedTime * _DissolveSpeed;
                clip(noiseSample - threshold);
                
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
}
