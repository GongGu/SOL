// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShader"
{
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _SubTex ("Texture", 2D) = "white" {}
	  _TintColor ("TintColor",Color) = (1,1,1,1)
      //_BumpMap ("Bumpmap", 2D) = "bump" {}
      //_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      //_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
	 // unity 빌트인 쉐이더인 RenderType 의 Transparent 사용
//      Tags { "RenderType" = "Transparent" }
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
//		#pragma vertex vertexFunction
//		#pragma fragment fragmentFucntion

//		#include "UnityCG.cginc"
// pragma [컴파일할 쉐이더] [사용할 함수이름] [빛계산방식] [옵션]
//      #pragma surface surf Lambert alpha
      #pragma surface surf Lambert 

      struct Input {
//		float vertex :POSITION;
//        float2 uv :TEXCOORD0;
		float2 uv_MainTex;
//        float2 uv_BumpMap;
//          float3 viewDir;
      };

		//vertex shader
		//struct v2f{
		//	float4 position :SV_POSITION;
		//	float2 uv :TEXCOORD0;
		//};

		//v2f	vertexFunction(Input IN){
		//		v2f OUT;

		//	OUT.position = UnityObjectToClipPos(IN.vertex);
		//	OUT.uv = IN.uv;

		//	return OUT;
		//}

      //sampler2D _MainTex;
      //sampler2D _SubTex;
////      sampler2D _BumpMap;
//		//float4 _RimColor;
//		//float _RimPower;

		sampler2D _MainTex;
		fixed4 _TintColor; // properties 를 available 하게 해줌

      void surf (Input IN, inout SurfaceOutput o) {
        //o.Albedo = _TintColor;
		o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;// + tex2D (_SubTex, IN.uv_MainTex).rgb;
		o.Alpha = 1;
//        o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
//          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
//		o.Emission = _RimColor.rgb * pow (rim, _RimPower);
      }
      ENDCG
    } 
    Fallback "Diffuse"
}