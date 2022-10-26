Shader "Custom/WorldSpaceTextureSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
        _BumpMap ("Normal", 2D) = "white" {}
        _BumpScale ("Depth", Range(-1,3)) = 0.0
        _ParallaxMap ("Height", 2D) = "white" {}
        _OcclusionMap ("Ambient Occlusion", 2D) = "white" {}
        _Scale ("Texture Scale", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _ParallaxMap;
        sampler2D _OcclusionMap;
        float _Parallax;
        fixed4 _Color;
        float _Scale;
        float _BumpScale;

        struct Input
        {
            float3 worldNormal;
            float3 worldPos;
            float3 viewDir;
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_OcclusionMap;
            float _Scale;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 UV;
            float2 normal;
		    fixed3 normalDir = WorldNormalVector(IN, o.Normal);

            // Implementation inspired by https://www.blog.radiator.debacle.us/2012/01/joys-of-using-world-space-procedural.html
            if (abs(normalDir.x)>0.5) {
                // side of wall
                UV = IN.worldPos.yz;
            } else if (abs(normalDir.z)>0.5) {
                // front of wall
                UV = IN.worldPos.xy;
            } else {
                // top of wall
                UV = IN.worldPos.xz;
            }

            half h = tex2D (_ParallaxMap, UV * _Scale).w;
            float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
            fixed4 c = tex2D(_MainTex, UV * _Scale + offset);
            fixed3 ao = tex2D(_OcclusionMap, UV * _Scale);
            fixed3 n = UnpackNormal(tex2D(_BumpMap, UV * _Scale));

            o.Albedo = c.rgb * ao.rgb * _Color.rgb;
            o.Normal = lerp(n, float3(1,1,1), -_BumpScale + 1);
        }
        
        ENDCG
    }
    FallBack "Diffuse"
}
