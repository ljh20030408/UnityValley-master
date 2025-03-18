// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/CarGlassTransparent" {
	Properties {
		_MainColor("Main Color", Color) = (1,1,1,1)
		//_CubeMap("Cube Map", Cube) = ""{}
		_ReflAmount("Reflect Amount", Float) = 0
		_FresnelPower("Fresnel Amount", float) = 0
		_FresnelOffset("Fresnel Offset", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		half4 _MainColor;
		samplerCUBE _CubeMap;
		fixed _ReflAmount;
		half _FresnelPower;
		half _FresnelOffset;
		struct vertexInput
		{
			float4 vertex:POSITION;
			fixed3 normal:NORMAL;
		};
		struct vertexOutput
		{
			float4 pos:SV_POSITION;
			fixed4 reflectDir:TEXCOORD0;
		};
		vertexOutput vert(vertexInput input)
		{
			 vertexOutput output;
			 output.pos = UnityObjectToClipPos(input.vertex);
			 fixed3 viewDir = normalize((mul(unity_ObjectToWorld, input.vertex) - _WorldSpaceCameraPos).xyz);
			 fixed3 normalDir = normalize(mul(unity_ObjectToWorld, fixed4(input.normal,0)).xyz);
			 output.reflectDir.xyz = reflect(viewDir, normalDir);
			 output.reflectDir.w = pow(dot(normalDir, viewDir)+1,_FresnelPower) * _ReflAmount + _FresnelOffset;
			 return output;
		}
		ENDCG
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			fixed4 frag(vertexOutput input):COLOR
			{
				fixed4 c = _MainColor;
				c.rgb += texCUBE(_CubeMap, input.reflectDir).rgb * input.reflectDir.w;
				return c;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
