Shader "Custom/Wave Fogless"
{
    Properties
    {
		[HDR] _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
		_WaveSpeed("Wave Speed", float) = 1
		_VerticalDistort("Vertical Distort", float) = 1
		_HorizontalDistort("Horizontal Distort", float) = 1
		_ForwardlDistort("Forward Distort", float) = 1
		_ScrollSpeed("Scroll Speed", float)=1
    }
    SubShader
    {
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
        LOD 100

        Pass
        {
		// Alpha blending
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Color;
			float _WaveSpeed;
			float _VerticalDistort;
			float _HorizontalDistort;
			float _ForwardDistort;
			float _ScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

				float displacement = sin(worldPos.x + (_WaveSpeed * _Time)) + sin(worldPos.z + (_WaveSpeed * _Time));
				worldPos.y = worldPos.y + (displacement * _VerticalDistort);
				worldPos.x = worldPos.x + (displacement * _HorizontalDistort);
				worldPos.z = worldPos.z + (displacement * _ForwardDistort);

				o.vertex = mul(UNITY_MATRIX_VP, worldPos);
                //o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				i.uv.y = i.uv.y - _Time * _ScrollSpeed;
                fixed4 col = tex2D(_MainTex, i.uv);
				col *= _Color;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }

			
            ENDCG
        }
    }
}
