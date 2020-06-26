Shader "Custom/SpatialAnomalies"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ShadowTex("Albed (RGB)", 2D) = "white" {}
        
        _Border("Border", Range(0,20)) = 0.0
        _BlendU("BlendU", Range(0,20)) = 0.0
        _BlendV("BlendV", Range(0,20)) = 0.0
        _Size("Size", Range(0,40)) = 0.0
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _ShadowTex;

        struct Input
        {
            float3 worldPos;
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        
        float _Border;
        float _BlendU;
        float _BlendV;
        float _Size;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed2 pos = fixed2((IN.uv_MainTex.x/_Size),
                                IN.uv_MainTex.y/_Size);

            
            fixed4 c = fixed4(0,0,0,0);
            float dist = distance( fixed3(0,0,0), IN.worldPos);
            float val = abs(sin(dist*3.0-_Time *500));
            fixed4 main = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 sub = tex2D(_ShadowTex, pos);
            if(sub.x < _Border){
                if( val > 0.98){
                    c = main  * fixed4(1,1,1,1);

                }else{
                    c = main  * fixed4(0.8,0.8,0.8,1);
                }
            }else{
                c = main * fixed4(0.2,0.2,0.2,1);
            }
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
