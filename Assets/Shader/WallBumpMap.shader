﻿Shader "Unlit/WallBumpMap"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _BumpMap("Bunp map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            fixed4 _Color;
            sampler2D _BumpMap;
            sampler2D _MainTex;

            //ローカル座標系を接空間へ変換する行列
            float4x4 InvTangentMatrix(float3 tan, float3 bin, float3 nor)
            {
                //接空間からローカルへ
                float4x4 mat = float4x4(float4(tan,0),
                                        float4(bin,0),
                                        float4(nor,0),
                                        float4(0,0,0,1)
                                        );
                return transpose(mat);
            }


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.normal = v.normal;

                //接空間ベクトル変換用変数
                float3 n = normalize(v.normal);
                float3 t = v.tangent;
                float3 b = cross(n, t);

                //ライトのローカル変換
                float3 localLight = mul(unity_WorldToObject,float4(0,0,0,1));
                
                //ライトの接空間変換
                o.lightDir = mul(localLight, InvTangentMatrix(t, b, n));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = float4(UnpackNormal(tex2D(_BumpMap, 10 * i.uv)), 1);
                float3 light = normalize(i.lightDir);
                float diff = max(0, dot(normal, light));
                float3 lightColor = _LightColor0;
                return _Color * fixed4(lightColor,0) * diff;
            }
            ENDCG
        }
    }
}
