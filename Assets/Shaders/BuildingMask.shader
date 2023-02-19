Shader "Custom/BuildingMask"
{
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry-1" }
        Pass{
            
            ColorMask 0
            
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members vertex)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma  fragment frag
            
            struct appdata
            {
                float4 vertex : UNITY_POSITION();

            };
            struct v2f
            {
                float4 pos : SV_POSITION;
            };
            
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            half4 frag(v2f i) : COLOR
            {
                return half4(0,0,0,0);
            }
            
            ENDCG
        }
    }
}
