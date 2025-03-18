// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/Line" {
	Properties {
		_Color("Color Tine", color) = (1,1,1,1)
	}
	SubShader {
		Pass
		{
			Tags{"RenderType"="Opaque""Queue"="Geometry""IgnoreProjector"="True"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			fixed4 _Color;
			struct v2f
			{
				float4 pos:SV_POSITION;
				half2 uv:TEXCOORD0;
			};
			v2f vert(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}
			fixed4 frag(v2f input):COLOR
			{
				return _Color;
			}
			ENDCG	
		}
	} 
}
