Shader "Hidden/BWShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _bwBlend ("Black and White Blend", Range(0,1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _bwBlend;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // http://tech.pro/tutorial/660/csharp-tutorial-convert-a-color-image-to-grayscale
                // magic numbers .3, .59, .11 represents sensitivity of human eye to the RGB components
                // this is nicer than taking average of rgb values
                // float lum = col.r*.3 + col.g*.59 + col.b*.11;
                float lum = col.r*.1 + col.g*.1 + col.b*.81;
                float3 bw = float3(lum,lum,lum);

                col.rgb = lerp(col.rgb, bw, _bwBlend);
                // just invert the colors
                // col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
