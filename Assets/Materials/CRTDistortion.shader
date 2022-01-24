Shader "Hidden/CRTDistortion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionTex ("Distortion Texture", 2D) = "white" {}
        _strength ("Distortion strength", Float) = 0
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
            sampler2D _DistortionTex;
            float _strength;

            fixed4 frag (v2f i) : SV_Target
            {
                half2 dis = tex2D(_DistortionTex, i.uv); // use r,g from tex to calculate distortion
                half2 d = dis * 2 - 1; // converts from 0-1 range to -1 to 1 range
                i.uv += d * _strength; // do the distortion in uv value
                i.uv = saturate(i.uv); // saturate clamps the value between 0 and 1

                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
