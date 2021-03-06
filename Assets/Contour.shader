﻿Shader "TopographicHeight"
{
    Properties
    {
		_StripeWidth ("Stripe Width", Float) = 20
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPosition : TEXCOORD1;
            };

            float _StripeWidth;

            v2f vert (appdata v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                return OUT;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(1.0, 1.0, 1.0, 1.0) * step(0.5, frac(i.worldPosition.y * _StripeWidth));
                return col;
            }
            ENDCG
        }
    }
}