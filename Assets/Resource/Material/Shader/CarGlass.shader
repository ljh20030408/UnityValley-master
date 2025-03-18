Shader "Car/CarGlass" {
	Properties {
		_MainColor("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		//_CubeMap("Cube Map", Cube) = ""{}
		_ReflAmount("Reflect Amount", Float) = 0
		_Fresnel("Fresnel Amount", Range(0, 3)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque"}
		
		CGPROGRAM
		#pragma surface surf Nolight noforwardadd exclude_path:prepass

		half4 _MainColor;
		sampler2D _MainTex;
		samplerCUBE _CubeMap;
		fixed _ReflAmount;
		half _Fresnel;

		struct Input {
			fixed2 uv_MainTex;
			fixed3 worldRefl;
			fixed3 viewDir;
		};
		
		inline fixed4 LightingNolight(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half3 texColor = tex2D (_MainTex, IN.uv_MainTex).rgb;
			half3 reflectionColor = texCUBE(_CubeMap, IN.worldRefl).rgb * _ReflAmount;
			half fresnel = pow(dot(1-IN.viewDir, o.Normal), _Fresnel);
			o.Albedo = texColor * _MainColor.rgb;
			o.Emission = reflectionColor * fresnel;
			o.Alpha = _MainColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
