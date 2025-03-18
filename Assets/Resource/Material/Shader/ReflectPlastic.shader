// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/ReflectPlastic" {
	Properties {
		_MainColor ("Main Color", Color) = (1,1,1,1)
		_CubeMap("Reflect Map", Cube) = ""{}
		_ReflAmount("Reflect Amount", float) = 0
		_FresnelPower("Fresnel Power", float) = 0
		_FresnelOffset("Fresnel Offset", float) = 0
		_AffectByAmbient("Affect By Ambient", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGINCLUDE
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma exclude_renderers xbox360 ps3 ps4 d3d11_9x
		half4 _MainColor;
		samplerCUBE _CubeMap;
		fixed _ReflAmount;
		half _FresnelPower;
		half _FresnelOffset;
		half _AffectByAmbient;
		struct vertexInput
		{
			float4 vertex:POSITION;
			half3 normal:NORMAL;
		};
		struct vertexOutput
		{
			float4 pos:SV_POSITION;
			fixed3 normalDir:TEXCOORD0;
			float4 worldPos:TEXCOORD1;
		};
		vertexOutput vert(vertexInput input)
		{
			 vertexOutput output;
			 output.pos = UnityObjectToClipPos(input.vertex);
			 
			 output.normalDir = mul(unity_ObjectToWorld, half4(input.normal, 0)).xyz;
			 output.worldPos = mul(unity_ObjectToWorld, input.vertex);
			 
			 return output;
		}
		fixed4 frag(vertexOutput input):COLOR
		{
			//vectors
			fixed3 normalDir = normalize(input.normalDir);
			fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - input.worldPos.xyz);
			fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
			fixed3 reflectDir = reflect(-viewDir, normalDir);
			//dot
			half NdotL = dot(normalDir, lightDir);
			half NdotV = dot(normalDir, viewDir);
			//diffuse
			fixed3 diffuseColor = _MainColor.rgb * (NdotL*0.5+0.5) + UNITY_LIGHTMODEL_AMBIENT.rgb * _AffectByAmbient;
			//reflect
			half reflectAmount = pow(1-NdotV, _FresnelPower) * _ReflAmount + _FresnelOffset;
			fixed3 reflectColor = texCUBE(_CubeMap, reflectDir).rgb * reflectAmount;
			//final color
			fixed4 finalColor = fixed4(1,1,1,1);
			finalColor.rgb = diffuseColor + reflectColor;
			return finalColor;
		}
		ENDCG
		Pass
		{
			Tags{"LightMode"="ForwardBase"}
			CGPROGRAM
			#define UNITY_PASS_FORWARDBASE
			
			ENDCG
		}
	} 
}
