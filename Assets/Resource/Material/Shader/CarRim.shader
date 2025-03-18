// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/CarRim" {
	Properties {
		_MainColor ("Main Color", Color) = (1,1,1,1)
		//_CubeMap("Reflect Map", Cube) = ""{}
		_ReflAmount("Reflect Amount", float) = 0
		_FresPow("Fresnel Power", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" "IgnoreProjector"="True"}
		CGINCLUDE
		fixed4 _MainColor;
		samplerCUBE _CubeMapBlur;
		fixed _ReflAmount;
		fixed _FresPow;
		struct vertexInput
		{
			float4 vertex:POSITION;
			fixed3 normal:NORMAL;
		};
		struct v2f
		{
			float4 pos:SV_POSITION;
			fixed4 reflectDir:TEXCOORD0;
		};
		v2f vert(vertexInput input)
		{
			v2f output;
			output.pos = UnityObjectToClipPos(input.vertex);
			fixed3 viewDir = normalize((_WorldSpaceCameraPos - mul(unity_ObjectToWorld, input.vertex)).xyz);
			fixed3 normalDir = normalize(mul(unity_ObjectToWorld, fixed4(input.normal,0.0)).xyz);
			output.reflectDir.xyz = reflect(-viewDir, normalDir);
			output.reflectDir.w = pow(dot(normalDir, viewDir), _FresPow) * _ReflAmount;
			return output;
		}
		ENDCG
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			fixed4 frag(v2f input):COLOR
			{
				fixed4 c = fixed4(0,0,0,1);
				c.rgb = _MainColor.rgb + texCUBE(_CubeMapBlur, input.reflectDir.xyz).rgb * input.reflectDir.w;
				return c;
			}
			ENDCG
		}
	} 
}
