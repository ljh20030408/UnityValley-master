// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/NoLightTexture" {
	Properties {
		_MainColor("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainTexTilingX("Main Texture Tiling X", float) = 1
		_MainTexTilingY("Main Texture Tiling Y", float) = 1
		_MainTexOffsetX("Main Texture Offset X", Float) = 0
		_MainTexOffsetY("Main Texture Offset Y", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque""IgnoreProjector"="True" }
		Cull Back Lighting Off
		CGINCLUDE
		#include "UnityCG.cginc"
		float4 _MainColor;
		sampler2D _MainTex;
		float _MainTexTilingX;
		float _MainTexTilingY;
		float _MainTexOffsetX;
		float _MainTexOffsetY;
		struct v2f
		{
			half2 uv:TEXCOORD0;
			float4 pos:SV_POSITION;
		};
		v2f vert(appdata_base v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			return o;
		}
		ENDCG
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			fixed4 frag(v2f i):COLOR
			{
				fixed4 c = tex2D(_MainTex, float2(i.uv.x*_MainTexTilingX,i.uv.y*_MainTexTilingY) + float2(_MainTexOffsetX,_MainTexOffsetY)) * _MainColor;
				return c;
			}
			ENDCG
		}
	} 
}
