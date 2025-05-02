Shader "Unlit/GreenBlueOnly"
{
   Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlueBoost("Blue Boost", Range(0, 2)) = 1.0
        _GreenBoost("Green Boost", Range(0, 2)) = 1.0
        _MinBlueThreshold("Min Blue Threshold", Range(0, 1)) = 0.01
        _MinGreenThreshold("Min Green Threshold", Range(0, 1)) = 0.01
        _GreenDesaturation("Green Desaturation", Range(0, 1)) = 0.5
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
            float _GreenBoost;
            float _MinBlueThreshold;
            float _MinGreenThreshold;
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
                
                // Process green channel
                float greenComponent = (col.g > _MinGreenThreshold) ? (col.g * _GreenBoost) : grey;
                greenComponent = lerp(greenComponent, grey, _GreenDesaturation);
                
                // Process blue channel
                float blueComponent = (col.b > _MinBlueThreshold) ? (col.b * _BlueBoost) : grey;
                
                // Final output: Grayscale red, processed green, processed blue
                col.rgb = float3(grey, greenComponent, blueComponent);
                
                return col;
            }
            ENDCG
        }        
    }
    FallBack "Diffuse"
}
