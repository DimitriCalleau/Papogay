Shader "_/Damaged"
{
    Properties
    {
		_Color ("Tint", color) = (1,0,0,1)
    }

	Subshader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Geometry+1" }

		Pass
		{
			ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha

			Stencil
			{
				Ref 2
				Comp equal
				Pass keep
			}

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
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

			float4 _Color;

			half4 frag(v2f i) : SV_Target
			{
				fixed4 col = _Color;
				col.a = _Color.a;
				return col;
			}
			ENDCG
		}
    }
}