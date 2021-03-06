Shader "Hidden/TintEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _tintBlend ("TintBlend", Range(0,1)) = 0
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
            float _tintBlend;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float lum = col.r*.1 + col.g*.1 + col.b*.81;
                float4 tint = float4(0.1,0.2,0.9,1);
                // float3 tint = float3(lum,lum,lum);
                tint = col * tint;
                col.rgb = lerp(col.rgb, tint, _tintBlend);
                // just invert the colors
                // col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
