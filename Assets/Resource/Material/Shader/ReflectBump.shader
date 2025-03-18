// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/ReflectBump" {
	Properties {
		_MainColor("Main Color", Color) = (1,1,1,1)
		_ReflMask("Reflect Mask", 2D) = "white"{}
		_ReflAmount("Reflect Amount", Float) = 1
		_ReflFresPower("Reflect Fresnel Power", Float) = 1
		_ReflFresOffset("Reflect Fresnel Offset", Float) = 0
		_RimColorMultiply("Rim Color Multiply", Float) = 0
		_RimPower("Rim Power", Float) = 1
		_NormalMap("Normal Map", 2D) = "bump"{}
		_BumpAmount("Bump Amount", range(0,1)) = 0
		_FinalColorAdjust("FinalColorAdjust", Float) = 1.3
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGINCLUDE
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma exclude_renderers xbox360 ps3 ps4 d3d11_9x
		fixed4 _MainColor;
		samplerCUBE _CubeMapBlur;
		sampler2D _ReflMask;
		half4 _ReflMask_ST;
		half _ReflAmount;
		half _ReflFresPower;
		half _ReflFresOffset;
		half _RimColorMultiply;
		half _RimPower;
		sampler2D _NormalMap;
		half4 _NormalMap_ST;
		half _BumpAmount;
		half _FinalColorAdjust;
		struct VertexInput
		{
			float4 vertex:POSITION;
			half3 normal:NORMAL;
			half4 tangent:TANGENT;
			half2 texcoord:TEXCOORD0;
		};
		struct VertexOutput
		{
			float4 pos:SV_POSITION;
			half2 uv:TEXCOORD0;
			half3 normal:TEXCOORD1;
			half3 tangent:TEXCOORD2;
			half3 binormal:TEXCOORD3;
			float4 posWorld:TEXCOORD4;
		};
		VertexOutput vert(VertexInput v)
		{
			VertexOutput o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			o.posWorld = mul(unity_ObjectToWorld, v.vertex);
			o.normal = normalize(mul(unity_ObjectToWorld, half4(v.normal, 0.0)).xyz);
			o.tangent = normalize(mul(unity_ObjectToWorld, half4(v.tangent.xyz, 0.0)).xyz);
			o.binormal = normalize(cross(o.normal, o.tangent.xyz) * v.tangent.w);
			return o;
		}
		fixed4 frag(VertexOutput i):COLOR
		{
			//vectors
			fixed3 normalLocal = UnpackNormal(tex2D(_NormalMap, _NormalMap_ST.xy * i.uv + _NormalMap_ST.zw)).rgb;
			half3x3 tangentTransform = half3x3(i.tangent, i.binormal, i.normal);
			fixed3 normalDir = normalize(mul(normalLocal, tangentTransform));
			normalDir = lerp(i.normal, normalDir, _BumpAmount);
			fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
			fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
			fixed3 reflectDir = reflect(-viewDir, normalDir);
			//dot
			half NdotV = dot(viewDir, normalDir);
			half NdotL = dot(normalDir, lightDir);
			//diffuse
			fixed3 diffuseColor = _MainColor.rgb * (NdotL * 0.5 + 0.5);
			//reflect
			fixed reflectAmount = _ReflAmount * pow((1-NdotV), _ReflFresPower) + _ReflFresOffset;
			fixed3 reflectMask = tex2D(_ReflMask, _ReflMask_ST.xy*i.uv + _ReflMask_ST.zw).rgb;
			fixed3 reflectColor = texCUBE(_CubeMapBlur, reflectDir).rgb * reflectMask * reflectAmount;
			//rim
			fixed3 rimColor = _MainColor.rgb * pow((1-NdotV), _RimPower) * _RimColorMultiply;
			//final
			fixed4 finalColor = fixed4(1.0,1.0,1.0,1.0);
			finalColor.rgb = diffuseColor + reflectColor + rimColor;
			finalColor.rgb = finalColor.rgb * _FinalColorAdjust;
			return finalColor;
		}
		ENDCG
		Pass
		{
			CGPROGRAM
			#define UNITY_PASS_FORWARDBASE			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
