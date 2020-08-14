// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//ポストエフェクトは_MainTexからレンダリング結果を受け取ることで画面に効果をかける
//TVシェーダーには
//画面を歪ませ
//色収差を出し
//画面端を暗くする(これだけで結構雰囲気でる)

Shader "Unlit/TVShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RGBNoise("RGBNoise", Range(0, 1)) = 0
		_ScanLineTail("Tail", Float) = 0.5
		_ScanLineSpeed("TailSpeed", Float) = 100
        _GreenOffset("GreenOfset", Float) = 0.01
        _BlueOffset("BlueOffset",Float) = 0.02
    }
    SubShader
    {
        //カリングとZライトはOFF，ZTestは通す
        Cull Off
        ZTest Always
        ZWrite Off

        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            //ランダム
            float rand(float2 co){
                return frac(sin(dot(co.xy, float2(12.9898, 78233))) * 43758.5453);
            }

            //剰余
            float mod(float2 a, float2 b)
            {
                return a - floor(a / b) * b;
            }

            sampler2D _MainTex;
			float _RGBNoise;
			float _ScanLineTail;
			float _ScanLineSpeed;
            float _GreenOffset;
            float _BlueOffset;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 inUV = i.uv;
                float2 uv = i.uv - 0.5;

                //歪曲収差(収差ではない．画面をそんな感じに歪ませる)
                float vignet = length(uv);
				uv /= 1 - vignet * 0.1;
				float2 texUV = uv + 0.5;

                //歪んで余った分を表示させない
                if (max(abs(uv.y) - 0.5, abs(uv.x) - 0.5) > 0)
				{
					return float4(0, 0, 0, 1);
				}

                float3 col;

                //色収差
                col.r = tex2D(_MainTex, texUV).r;
				col.g = tex2D(_MainTex, texUV - float2(_GreenOffset, 0)).g;
				col.b = tex2D(_MainTex, texUV - float2(_BlueOffset, 0)).b;

                //ランダムで帯状にRGBランダムノイズを走らせる
				if (rand((rand(floor(texUV.y * 500) + _Time.y) - 0.5) + _Time.y) < _RGBNoise)
				{
					col.r = rand(uv + float2(123 + _Time.y, 0));
					col.g = rand(uv + float2(123 + _Time.y, 1));
					col.b = rand(uv + float2(123 + _Time.y, 2));
				}
        
                // ブラウン管の横縞
				float scanLineColor = sin(_Time.y * 10 + uv.y * 500) / 2 + 0.5;
				col *= 0.5 + clamp(scanLineColor + 0.5, 0, 1) * 0.5;

                // スキャンラインの残像を描画
				float tail = clamp((frac(uv.y + _Time.y * _ScanLineSpeed) - 1 + _ScanLineTail) / min(_ScanLineTail, 1), 0, 1);
				col *= tail;
                

                // 画面端を暗くする
				col *= 1 - vignet * 1.3;

                return float4(col, 1);
            }
            ENDCG
        }
    }
}
