// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/BaseTransparent" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Font Texture", 2D) = "white"{}
	}
	SubShader {
		Tags { "RenderType"="Transparent""Queue"="Transparent" }
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			fixed4 _Color;
			sampler2D _MainTex;
			half4 _MainTex_ST;
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
			fixed4 frag(v2f i):COLOR
			{
				fixed4 texColor = tex2D(_MainTex, i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed4 c = _Color;
				c.a = texColor.a;
				return c;
			}
			ENDCG
		}
	} 
}
