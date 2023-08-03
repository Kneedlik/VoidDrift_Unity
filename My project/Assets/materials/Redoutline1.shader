Shader "Custom/Redoutline1"
{
    Properties{
       _MainTex("Main Texture", 2D) = "white" {}
       _OutlineColor("Outline Color", Color) = (1, 0, 0, 1) // Red color
       _OutlineThickness("Outline Thickness", Range(0, 0.1)) = 0.02
    }
        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _OutlineColor;
                float _OutlineThickness;

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 mainColor = tex2D(_MainTex, i.uv);
                    fixed4 outlineColor = _OutlineColor;
                    float outline = fwidth(i.uv.x) + fwidth(i.uv.y);
                    outline *= _OutlineThickness;
                    float alpha = smoothstep(0.5 - outline, 0.5 + outline, fwidth(i.uv.x));
                    alpha *= smoothstep(0.5 - outline, 0.5 + outline, fwidth(i.uv.y));
                    fixed4 finalColor = lerp(outlineColor, mainColor, alpha);
                    return finalColor;
                }
                ENDCG
            }
       }
}
