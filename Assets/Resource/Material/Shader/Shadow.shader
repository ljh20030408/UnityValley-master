// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Car/Shadow" {
	Properties {
		_MainColor("Main Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent""IgnoreProjector"="True"}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			uniform fixed4 _MainColor;
			uniform sampler2D _MainTex;
			struct vertexInput
			{
				float4 vertex:POSITION;
				half2 texcoord:TEXCOORD0;
			};
			struct vertexOutput
			{
				float4 pos:SV_POSITION;
				half2 uv:TEXCOORD0;
			};
			vertexOutput vert(vertexInput input)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(input.vertex);
				o.uv = input.texcoord;
				return o;
			}
			fixed4 frag(vertexOutput input):COLOR
			{
				return _MainColor * tex2D(_MainTex, input.uv);
			}
			ENDCG
		}
	} 
}
