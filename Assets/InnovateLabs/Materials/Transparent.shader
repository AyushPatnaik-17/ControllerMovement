Shader "Custom/FresnelGlowWithTransparency"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _DistortTex("Distortion Texture", 2D) = "white" {}
        _DistortIntensity("Distortion Intensity", Range(0, 10)) = 0
        _FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)
        _FresnelIntensity("Fresnel Intensity", Range(0, 10)) = 0
        _FresnelRamp("Fresnel Ramp", Range(0, 10)) = 0
        _InvFresnelColor("Inverse Fresnel Color", Color) = (1, 1, 1, 1)
        _InvFresnelIntensity("Inverse Fresnel Intensity", Range(0, 10)) = 0
        _InvFresnelRamp("Inverse Fresnel Ramp", Range(0, 10)) = 0
        [Toggle] NORMAL_MAP("Use Normal Map", Float) = 0
        _NormalMap("Normal Map", 2D) = "white" {}
        _Transparency("Transparency", Range(0, 1)) = 1 // New property for transparency control
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ NORMAL_MAP_ON
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float3 tangent : TEXCOORD3;
                float3 bitangent : TEXCOORD4;
            };

            sampler2D _MainTex, _NormalMap, _DistortTex;
            float4 _MainTex_ST, _FresnelColor, _InvFresnelColor;
            float _FresnelIntensity, _FresnelRamp, _DistortIntensity, _InvFresnelRamp, _InvFresnelIntensity, _Transparency;

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                #if NORMAL_MAP_ON
                    o.tangent = UnityObjectToWorldDir(v.tangent);
                    o.bitangent = cross(o.tangent, o.normal);
                #else
                    o.tangent = float3(0,0,0);
                    o.bitangent = float3(0,0,0);
                #endif

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float distort = tex2D(_DistortTex, i.uv + _Time.xx).r;

                float3 finalNormal = i.normal;
                #if NORMAL_MAP_ON
                    float3 normalMap = UnpackNormal(tex2D(_NormalMap, i.uv));
                    finalNormal = normalMap.r * i.tangent + normalMap.g * i.bitangent + normalMap.b * i.normal;
                #endif

                float fresnelAmount = 1 - max(0, dot(finalNormal, i.viewDir));
                fresnelAmount = pow(fresnelAmount * distort * _DistortIntensity, _FresnelRamp) * _FresnelIntensity;
                float3 fresnelColor = fresnelAmount * _FresnelColor.rgb;

                float invfresnelAmount = max(0, dot(finalNormal, i.viewDir));
                invfresnelAmount = pow(invfresnelAmount * distort * _DistortIntensity, _InvFresnelRamp) * _InvFresnelIntensity;
                float3 invfresnelColor = invfresnelAmount * _InvFresnelColor.rgb;

                float3 finalColor = fresnelColor + invfresnelColor;
                float alpha = tex2D(_MainTex, i.uv).a * _Transparency; // Apply the transparency control
                return fixed4(finalColor, alpha);
            }
            ENDCG
        }
    }

    FallBack "Transparent/Cutout/VertexLit"
}
