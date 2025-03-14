Shader "Custom/GreyscaleToColor"
{
    Properties {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _Colorize("Colorize", Range(0,1)) = 0
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off Lighting Off ZWrite Off Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Colorize;
            float4 _MainTex_ST;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // 计算灰度值
                float gray = dot(col.rgb, fixed3(0.3, 0.59, 0.11));
                fixed3 greyscale = fixed3(gray, gray, gray);
                // _Colorize == 0 时全灰度，== 1 时全彩色
                col.rgb = lerp(greyscale, col.rgb, _Colorize);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}
