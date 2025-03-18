// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "LexLiu/LineTransparent" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color("Color Tine", color) = (1,1,1,1)
	}
	SubShader {
		Tags{"RenderType"="Transparent""Queue"="Transparent"}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _MainTex;
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
				return tex2D(_MainTex, input.uv) * _Color;
			}
			ENDCG	
		}
	} 
}
