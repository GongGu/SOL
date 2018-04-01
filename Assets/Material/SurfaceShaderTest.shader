Shader "Custom/SurfaceShaderTest" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" {}
		_SubTex ("SubTex", 2D) = "white" {}
	}
	SubShader {
//		Tags { "RenderType" = "Transparent" }
		Tags { "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		// properties 의 순서에 맞춰 1:1 대응
		struct Input {
			float2 uv_MainTex;
			float2 uv_SubTex;
			float4 _Time; // built-in value
		};

		// 이렇게 정의해야 properties 의 값들 사용 가능
		sampler2D _MainTex;
		sampler2D _SubTex;

		void surf (Input IN, inout SurfaceOutput o) {
//			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb; // Albedo = 빛 계산 색 적용
			float2 pivot = float2(0.09,0.09);
			float4 main = tex2D (_MainTex, IN.uv_MainTex * 1.15 - pivot);
			float4 sub = tex2D( _SubTex, IN.uv_SubTex);

			float4 result = main + sub;

			// Emission = 고정 색 적용
			o.Emission = result.rgb;
			o.Alpha = result.w; //fmod(_Time.y, 1);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
