Shader "Custom/LinearFadeShader"
{
    Properties
    {
        _OpaqueColor ("Opaque Color", Color) = (1, 1, 1, 1)
        _FadeAmount ("Fade Amount", Range(0, 1)) = 0.5
        _FadeDirection ("Fade Direction", Float) = 0 // 0 = TopToBottom, 1 = BottomToTop
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Cull Off
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            fixed4 _OpaqueColor;
            float _FadeAmount;
            int _FadeDirection;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate the fade factor
                float fadeFactor = _FadeDirection == 0 ? (i.worldPos.y / _FadeAmount) : (1.0 - (i.worldPos.y / _FadeAmount));
                fadeFactor = saturate(fadeFactor);
                
                // Interpolate between the opaque color and transparent
                fixed4 color = _OpaqueColor;
                color.a *= fadeFactor;
                
                return color;
            }
            ENDHLSL
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}
