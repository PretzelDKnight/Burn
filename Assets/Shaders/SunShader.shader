Shader "Custom/SunShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_ColorMask("Color Mask", Color) = (1,1,1,1)
		[HDR]_BottomColor("Bottom Color", Color) = (1, 1, 1, 1)
		[HDR]_TopColor("Top Color", Color) = (1,1,1,1)
		_Offset("Offset", float) = 0
		_MaskOffset("Mask Offset", float) = 0
		_MinBarWidth("Min Bar Width", float) = 0
		_MaxBarWidth("Max Bar Width", float) = 0
		_ScrollSpeed("Scroll Speed", float) = 0
		_Multiplier("Pixel Multiplier", Range(0,1)) = 0.5
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 position	: POSITION;
				float2 uv		: TEXCOORD0;
			};

			struct v2f
			{
				float4 position	: SV_POSITION;
				float2 uv		: TEXCOORD0;
				half4 color		: COLOR;
				float4 bar      : TEXCOOORD1;
				float2 barUv    : TEXCOORD2;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			sampler2D _Mask;
			float4 _MainTex_ST;
			float4 _Mask_ST;
			half4 _BottomColor;
			half4 _TopColor;
			float4 _ColorMask;
			float _Offset; 
			float _MaskOffset;
			float _MinBarWidth;
			float _MaxBarWidth;
			float _ScrollSpeed;
			float _Multiplier;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.position = UnityObjectToClipPos(IN.position);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
				OUT.barUv = TRANSFORM_TEX(IN.uv, _Mask);

				float factor = mad(OUT.position.y, -0.5, 0.5);
				factor *= 1 + _Offset * 2;
				factor -= _Offset;
				factor = clamp(factor, 0, 1);
				OUT.color = lerp(_BottomColor, _TopColor, factor);

				float barFactor = mad(OUT.position.y, -0.5, 0.5);
				barFactor *= 1 + _MaskOffset * 2;
				barFactor -= _MaskOffset;
				barFactor = clamp(barFactor, 0, 1);
				OUT.bar = lerp(_MinBarWidth, _MaxBarWidth, barFactor);

				return OUT;
			}

			float3 getNoise(float2 uv)
			{
				float3 noise = tex2D(_NoiseTex, uv * 100 + _Time * 50);
				noise = mad(noise, 2.0, -0.5);

				return noise / 255;
			}

			half4 frag(v2f IN) : SV_Target
			{
				float2 uv = IN.uv;
				float2 app = _ScreenParams * _Multiplier;
				uv.x = round(uv.x * app.x) / app.x;
				uv.y = round(uv.y * app.y) / app.y;

				half4 texCol = tex2D(_MainTex, uv);

				IN.barUv.y = IN.barUv.y * IN.bar - _Time * _ScrollSpeed;

				half4 maskCol = tex2D(_Mask, IN.barUv);

				half4 c;
				c.rgb = IN.color.rgb + getNoise(IN.uv);
				c.rgb *= texCol.a;
				c.a = texCol.a;

				return c * _ColorMask * maskCol.a;
			}

			ENDCG
			}
		}
}