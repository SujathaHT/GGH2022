Shader "Hidden/NonDualityShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _strength ("Strength of non duality", float) = 0
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
            float _strength;

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                float2 uv=i.uv;
                float2 pixelOffset = (1.0 / _ScreenParams.xy) + 1.0;
                float4 pixel = tex2D( _MainTex, uv);   
                float4 pixelUp = tex2D( _MainTex, uv + float2( 0, -pixelOffset.y ) );
                float4 pixelRight = tex2D( _MainTex, uv + float2( pixelOffset.x, 0 ) );
                float4 pixelLeft = tex2D( _MainTex, uv + float2( -pixelOffset.x, 0 ) );
                float4 pixelDown = tex2D( _MainTex, uv + float2( 0, pixelOffset.y ) );

                float4 pixelLeftTop = tex2D( _MainTex, uv + float2( -pixelOffset.x, -pixelOffset.y) );
                float4 pixelRightTop = tex2D( _MainTex, uv + float2( pixelOffset.x, -pixelOffset.y ) );
                float4 pixelDownLeft = tex2D( _MainTex, uv + float2( -pixelOffset.x, pixelOffset.y ) );
                float4 pixelDownRight = tex2D( _MainTex, uv + float2( pixelOffset.x, pixelOffset.y ) );

                float _nonDualityStrength = _strength / 8;
                float4 col = (1 - _strength) * pixel + 
                        (_nonDualityStrength * pixelRight) + 
                        (_nonDualityStrength * pixelLeft) + 
                        (_nonDualityStrength * pixelUp) +
                        (_nonDualityStrength * pixelDown) + 
                        (_nonDualityStrength * pixelLeftTop) + 
                        (_nonDualityStrength * pixelRightTop) + 
                        (_nonDualityStrength * pixelDownLeft) + 
                        (_nonDualityStrength * pixelDownRight);
                // return (1 - _strength) * pixel + _strength * float4(1,0,0,1);
                return col;
            }
            ENDCG
        }
    }
}
