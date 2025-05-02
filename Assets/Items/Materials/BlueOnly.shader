Shader "Unlit/BlueOnly"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlueBoost("Blue Boost", Range(0, 2)) = 1.0
        _MinBlueThreshold("Min Blue Threshold", Range(0, 1)) = 0.01
        _GreenDesaturation("Green Desaturation", Range(0, 1)) = 0.5 // Controls how much green is softened
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass 
        {
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlueBoost;
            float _MinBlueThreshold;
            float _GreenDesaturation;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
                
                if (col.b > _MinBlueThreshold) 
                {
                    // For blue areas: soften the green component
                    float softenedGreen = lerp(col.g, grey, _GreenDesaturation);
                    col.rgb = float3(grey, softenedGreen, col.b * _BlueBoost);
                }
                else 
                {
                    // Non-blue areas: full grayscale
                    col.rgb = float3(grey, grey, grey);
                }
                
                return col;
            }
            ENDCG
        }        
    }
    FallBack "Diffuse"
}