Shader "Custom/SurfaceShaderTest" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" {}
		_XScrollSpeed("X Scroll Speed", Float) = 1
		_YScrollSpeed("Y Scroll Speed", Float) = 1
		
		_Cols("Cols Count", Int) = 5
		_Rows ("Cols Count", Int) = 18
		_PlaySpeed("Per Frame Length", Float) = 30.0
	}
	SubShader {
//		Tags { "RenderType" = "Transparent" }
		Tags { "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		// 이렇게 정의해야 properties 의 값들 사용 가능
		sampler2D _MainTex;
		//float _XScrollSpeed;
		//float _YScrollSpeed;
		uint _Cols;
		uint _Rows;
		float _PlaySpeed;

		// properties 의 순서에 맞춰 1:1 대응
		struct Input {
			float2 uv;
		};

		float2 calculateUV(float x, float y, int index){
			float dx = 1.0 / float(_Cols);
			float dy = 1.0 / float(_Rows);

			float u = (x + fmod(index, (float)_Cols)) * dx;
			float v = 1.0 - (y + (index / _Cols)) * dy;

			return float2(u, v);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			//fixed2 scrollUV = IN.uv_MainTex;
			//fixed xScrollValue = _XScrollSpeed * _Time.x;
			//fixed yScrollValue = _YScrollSpeed * _Time.x;
			//scrollUV += fixed2(xScrollValue, yScrollValue);
			//float4 c = tex2D(_MainTex, scrollUV);
//
////		o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb; // Albedo = 빛 계산 색 적용
//			float2 pivot = float2(0.09,0.09);
//			float4 main = tex2D (_MainTex, IN.uv_MainTex * 1.15 - pivot);

			int frameCount = _Rows * _Cols;
			float fFrame = fmod(_Time.z * _PlaySpeed, frameCount); // 시간에 따른 프레임 계산
			int iFrame = floor(fFrame); // 현재 재생될 프레임 index
			int iNextFrame = floor(fmod(fFrame + 1.0, frameCount));

			float2 currentUV = calculateUV(IN.uv.x, IN.uv.y, iFrame);
			fixed2 nextUV = calculateUV(IN.uv.x, IN.uv.y, iNextFrame);

			float4 main = tex2D(_MainTex, currentUV);

			float4 result = main;

			// Emission = 고정 색 적용
			o.Emission = result.rgb;
			o.Alpha = result.w;

			// _Time 은 built-in value
		}
		ENDCG
	}
	FallBack "Diffuse"
}
