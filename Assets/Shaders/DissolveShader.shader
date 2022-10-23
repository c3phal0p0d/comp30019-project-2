// Implementation inspired by https://lindenreidblog.com/2017/12/16/dissolve-shader-in-unity/

Shader "Unlit/DissolveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        
        _DissolveSpeed("Dissolve Speed", float) = 1.0
        _FadeSpeed("Fade Speed", float) = 1.0
        _DissolveColor("Dissolve Color", Color) = (1, 1, 1, 1)
        _ColorThreshold("Color Threshold", float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseTex;
            float _DissolveSpeed;
            float _FadeSpeed;
            float4 _DissolveColor;
            float _ColorThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv.xy);

                // sample noise texture
                float noiseSample = tex2Dlod(_NoiseTex, float4(i.uv.xy, 0, 0));

                float colorThreshold = _Time * _ColorThreshold;
                float useDissolve = noiseSample - colorThreshold < 0;

                color = (1-useDissolve)*color + useDissolve*_DissolveColor;

                float threshold = _Time * _DissolveSpeed;
                clip(noiseSample - threshold);

                color.a -= saturate(_Time * _FadeSpeed);
                return color;
            }
            ENDCG
        }
    }
}
