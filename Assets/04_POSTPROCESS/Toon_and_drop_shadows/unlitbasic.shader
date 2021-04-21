Shader "_/Unlit + toon"
{
	//show values to edit in inspector
	Properties
	{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}

		//cel
		_Brightness("Shadow Brightness", Range(0,1)) = 0.2
		_Strength("Strength", Range(0,1)) = 0.5
		_LightColor("LightColor", Color) = (1,1,1,1)
		_ShadowStep("Shadow Steps", Range(0,1)) = 0.5
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }

		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;

			//cel
			float _Brightness;
			float _Strength;
			float _LightColor;
			float _ShadowStep;

			struct appdata 
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				
				//cel
				float3 normal : NORMAL;
			};

			//cel
			float Toon(float3 normal, float3 lightDir) 
			{
				float nDotL = max(0.0, dot(normalize(normal), normalize(lightDir)));
				nDotL /= _ShadowStep;
				return nDotL;
			}

			struct v2f 
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			
				//cel
				half3 worldNormal = NORMAL;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				//cel
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				
				return o;
			}

			fixed3 frag(v2f i) : SV_TARGET
			{
				fixed3 col = tex2D(_MainTex, i.uv);
				col *= _Color;

				//cel
				col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _LightColor + _Brightness;
				
				return col;
			}
			ENDCG
		}
	}
}