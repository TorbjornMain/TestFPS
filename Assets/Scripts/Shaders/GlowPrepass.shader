Shader "Unlit/GlowPrepass"
{
	Properties
	{
		_GlowColor ("Glow Color", Color) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Glowable"="True" }
		LOD 100

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
				float4 vertex : SV_POSITION;
			};

			fixed4 _GlowColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _GlowColor;
				return col;
			}
			ENDCG
		}
	}
}
