Shader "Custom/InverseCrosshairColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.8
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        // Захватываем экран в текстуру
        GrabPass { "_GrabTexture" }

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
                float4 grabPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _GrabTexture;
            float _Opacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Получаем цвет из захваченного экрана
                float2 grabUV = i.grabPos.xy / i.grabPos.w;
                fixed4 bgColor = tex2D(_GrabTexture, grabUV);

                // Инвертируем цвет
                fixed4 invertedColor = fixed4(1 - bgColor.r, 1 - bgColor.g, 1 - bgColor.b, _Opacity);

                return invertedColor;
            }
            ENDCG
        }
    }
}
