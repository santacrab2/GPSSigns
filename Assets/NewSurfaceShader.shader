Shader "TextMeshPro/Distance Field Neon"
{
    Properties
    {
        _FaceColor ("Face Color", Color) = (1, 0, 0, 1)
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 1)) = 0
        _GlowColor ("Glow Color", Color) = (1, 0.5, 0, 1)
        _GlowPower ("Glow Power", Range(0, 5)) = 2
        _GlowWidth ("Glow Width", Range(0, 1)) = 0.25
        _FlickerSpeed ("Flicker Speed", Range(0, 10)) = 2
        _FlickerAmount ("Flicker Amount", Range(0, 1)) = 0.1
        _MainTex ("Font Atlas", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color    : COLOR;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float4 color    : COLOR;
            };

            sampler2D _MainTex;
            fixed4 _FaceColor;
            fixed4 _OutlineColor;
            float _OutlineWidth;
            fixed4 _GlowColor;
            float _GlowPower;
            float _GlowWidth;
            float _FlickerSpeed;
            float _FlickerAmount;
            float4 _MainTex_ST;
            float4 _Timer;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float sdf = tex2D(_MainTex, i.uv).a;

                // Core TMP edge rendering
                float face = smoothstep(0.5, 0.5 - 0.01, sdf);
                fixed4 faceCol = _FaceColor * face;

                // Glow effect
                float glow = smoothstep(0.5 + _GlowWidth, 0.5, sdf);
                glow = pow(glow, _GlowPower);

                float flicker = 1.0 + sin(_Time.y * _FlickerSpeed) * _FlickerAmount;
                fixed4 glowCol = _GlowColor * glow * flicker;

                return (faceCol + glowCol) * i.color;
            }
            ENDCG
        }
    }
}
