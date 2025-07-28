Shader "Custom/Lightning1"
{
    Properties {
        _TintColor ("Tint Color", Color) = (1, 1, 0, 1) // Yellow color
        _Intensity ("Intensity", Range(0, 1)) = 1
        _Speed ("Speed", Range(0, 10)) = 1
        _Thickness ("Thickness", Range(0, 1)) = 0.1
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
            };

            float _Intensity;
            float _Speed;
            float _Thickness;
            float4 _TintColor;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float time = _Time.y * _Speed;
                float noise = sin(i.vertex.x + time);
                float intensity = smoothstep(_Thickness, 0, abs(noise)) * _Intensity;
                fixed4 color = _TintColor * intensity;
                color.a = intensity;
                return color;
            }
            ENDCG
        }
    }
    Fallback "Sprites/Default"
}
