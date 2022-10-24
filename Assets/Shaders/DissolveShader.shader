// Dissolve effect code inspired by https://lindenreidblog.com/2017/12/16/dissolve-shader-in-unity/

Shader "Unlit/DissolveShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _DissolveSpeed("Dissolve Speed", float) = 1.0
        _FadeSpeed("Fade Speed", float) = 1.0
        _DissolveColor1("Dissolve Color 1", Color) = (1, 1, 1, 1)
		_DissolveColor2("Dissolve Color 2", Color) = (1, 1, 1, 1)
		_ColorThreshold1("Color Threshold 1", float) = 1.0
		_ColorThreshold2("Color Threshold 2", float) = 1.0
        _StartTime("Start Time", float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

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
                float2 uv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseTex;
            float _DissolveSpeed;
            float _FadeSpeed;
            float4 _DissolveColor1;
			float4 _DissolveColor2;
            float _ColorThreshold1;
			float _ColorThreshold2;
            float _StartTime;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv.xy);

                // sample noise texture
                float noiseSample = tex2Dlod(_NoiseTex, float4(i.uv.xy, 0, 0));

                // dissolve colors
                float threshold2 = _Time * _ColorThreshold2 - _StartTime;
				float useDissolve2 = noiseSample - threshold2 < 0;
				color = (1-useDissolve2)*color + useDissolve2*_DissolveColor2;

                float threshold1 = _Time * _ColorThreshold1 - _StartTime;
				float useDissolve1 = noiseSample - threshold1 < 0;
				color = (1-useDissolve1)*color + useDissolve1*_DissolveColor1;

                float threshold = _Time * _DissolveSpeed;
                clip(noiseSample - threshold);

                color.a -= saturate(_Time * _FadeSpeed);
                return color;
            }
            ENDCG
        }
    }
}
