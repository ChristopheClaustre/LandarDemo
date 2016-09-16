Shader "Custom/FogOfWarMask" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlurPower ("BlurPower" , float) = 0.002
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="fade" "LightMode"="ForwardBase"}
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting Off
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf NoLighting noambient keepalpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, float aten){
			fixed4 color;
			color.rgb = s.Albedo;
			color.a = s.Alpha;
			return color;
		}
		fixed4 _Color;
		sampler2D _MainTex;
		float _BlurPower;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			//half4 baseColor1 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower,0));
			//half4 baseColor2 = tex2D (_MainTex, IN.uv_MainTex + float2(0,-_BlurPower));
			//half4 baseColor3 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower,0));
			//half4 baseColor4 = tex2D (_MainTex, IN.uv_MainTex + float2(0,_BlurPower));
			//half4 baseColor = 0.25 * (baseColor1 + baseColor2 + baseColor3 + baseColor4);
		
			half4 baseColor11 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower,0));
			half4 baseColor12 = tex2D (_MainTex, IN.uv_MainTex + float2(0,-_BlurPower));
			half4 baseColor13 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower,0));
			half4 baseColor14 = tex2D (_MainTex, IN.uv_MainTex + float2(0,_BlurPower));
			
			half4 baseColor21 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower/2,0));
			half4 baseColor22 = tex2D (_MainTex, IN.uv_MainTex + float2(0,-_BlurPower/2));
			half4 baseColor23 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower/2,0));
			half4 baseColor24 = tex2D (_MainTex, IN.uv_MainTex + float2(0,_BlurPower/2));
			
			half4 baseColor31 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower,-_BlurPower));
			half4 baseColor32 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower,_BlurPower));
			half4 baseColor33 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower,_BlurPower));
			half4 baseColor34 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower,-_BlurPower));
			
			half4 baseColor41 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower/2,-_BlurPower/2));
			half4 baseColor42 = tex2D (_MainTex, IN.uv_MainTex + float2(-_BlurPower/2,_BlurPower/2));
			half4 baseColor43 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower/2,_BlurPower/2));
			half4 baseColor44 = tex2D (_MainTex, IN.uv_MainTex + float2(_BlurPower/2,-_BlurPower/2));
			
			half4 baseColor = 0.0652 * (baseColor11 + baseColor12 + baseColor13 + baseColor14 + baseColor21 + baseColor22 + baseColor23 + baseColor24 + baseColor31 + baseColor32 + baseColor33 + baseColor34 + baseColor41 + baseColor42 + baseColor43 + baseColor44);
			
			o.Albedo = _Color.rgb *  baseColor.b;
			o.Alpha = _Color.a - baseColor.g; // g = green, color of the aperture mask
	 	}
		ENDCG
	} 
	FallBack "Diffuse"
}
