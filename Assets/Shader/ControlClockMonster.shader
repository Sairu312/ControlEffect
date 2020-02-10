Shader "Custom/ControlClockMonster"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color",Color) = (1,1,1,1)

    }
    SubShader
    {
        Tags { 
            "RenderType"="Opaque" 
            }
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            //単位行列
            static float4x4 one() 
            {
                return float4x4( 
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);  
            }

            //X軸回転行列
            static float4x4 rotX(float angleInRadians) 
            {
                float s = sin(angleInRadians);
                float c = cos(angleInRadians);
  	
                return float4x4( 
                1, 0, 0, 0,
                0, c, s, 0,
                0, -s, c, 0,
                0, 0, 0, 1);  
            }

            //Y軸回転行列
            static float4x4 rotY(float angleInRadians) 
            {
                float s = sin(angleInRadians);
                float c = cos(angleInRadians);
  	
                return float4x4( 
                c, 0, -s, 0,
                0, 1, 0, 0,
                s, 0, c, 0,
                0, 0, 0, 1);  
            }

            //Z軸回転行列
            static float4x4 rotZ(float angleInRadians) 
            {
                float s = sin(angleInRadians);
                float c = cos(angleInRadians);
  	
                return float4x4( 
                c, s, 0, 0,
                -s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);  
            }

            //移動行列
            static float4x4 move(float x, float y, float z)
            {
                return float4x4(
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    x, y, z, 1);
            }


            //拡大縮小行列
            static float4x4 scale(float x, float y, float z)
            {
                return float4x4(
                    x, 0, 0, 0,
                    0, y, 0, 0,
                    0, 0, z, 0,
                    0, 0, 0, 1);
            }

            
            //3つの行列をかける
            static float4x4 mul3(float4x4 a, float4x4 b, float4x4 c)
            {
                float4x4 tmp = mul(a,b);
                tmp = mul(tmp,c);
                return tmp;
            }

            //回転行列
            static float4x4 rotationMat(float Xangle, float Yangle, float Zangle)
            {
                return mul3(rotZ(Zangle), rotX(Xangle), rotY(Yangle));
            }



            //これはvert関数で使用する構造体
            //この構造体が宣言された段階で頂点情報が入っているのだろうか？
            //頂点シェーダからフラグメントへ渡す情報よう構造体かも
            struct appdata
            {
                float4 vertex : POSITION;
                uint vertexId : SV_VertexID;
                float2 uv : TEXCOORD0;
            };


            //こっちはvertとfragで使用するらしい
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };


            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;



            //頂点シェーダ
            v2f vert(appdata v)
            {
                v2f o;
                //float amp = 0.5 * sin(_Time * 100);

                float cubeIndex = floor(v.vertexId / 24);
                float xCubeIndex = fmod(cubeIndex, 10);
                float yCubeIndex = floor(cubeIndex / 10);
                float4x4 transMove = move(1,1,1);
                float4x4 rotationMove = rotationMat(_Time * (xCubeIndex + 1) * 10,0,yCubeIndex * xCubeIndex * _Time);
                float4x4 scaleMove = scale(0.5,10 * sin(_Time * cubeIndex),0.5);



                v.vertex = mul(mul3(transMove, rotationMove, scaleMove), v.vertex);
                //v.vertex.z = sin(cubeIndex);



                //UnityObjectToClipPosは透視投影変換関数として見てよさそう
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }



                
            //フラグメントシェーダ
            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            

            ENDCG
        }
    }
}
