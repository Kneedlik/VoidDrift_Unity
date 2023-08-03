Shader "Custom/GreenTint"
{
    Properties{
         _MainTex("Texture", 2D) = "white" {}
         _TintColor("Tint Color", Color) = (0, 1, 0, 0.5)
    }
        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                fixed4 _TintColor;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 color = tex2D(_MainTex, i.uv);
                    color *= _TintColor;
                    return color;
                }
                ENDCG
            }
         }
             FallBack "Sprites/Default"
}
