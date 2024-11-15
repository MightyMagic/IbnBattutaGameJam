Shader "Custom/URP/SimpleWaterShader"
{
    Properties
    {
        _BaseColor ("Base Water Color", Color) = (0, 0.5, 1, 1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveScale ("Wave Scale", Float) = 0.2
        _NoiseTex ("Noise Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _NoiseTex;
            float4 _BaseColor;
            float _WaveSpeed;
            float _WaveScale;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // Time-based wave movement
                float time = _Time.y * _WaveSpeed;
                float2 waveUV = i.uv + float2(time, time);
                float wave = tex2D(_NoiseTex, waveUV).r * _WaveScale;

                // Adjust color based on wave
                float4 color = _BaseColor;
                color.rgb += wave * 0.1;

                return color;
            }
            ENDHLSL
        }
    }
    FallBack "Transparent"
}