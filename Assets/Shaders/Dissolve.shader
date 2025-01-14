// Inspired by https://lindenreidblog.com/2017/12/16/dissolve-shader-in-unity/

Shader "Unlit/Dissolve"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo", 2D) = "white" {}
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
        Tags { "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma target 3.0

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD2;
            };

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
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

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = _Color;

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
    }
}
