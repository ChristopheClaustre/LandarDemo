Shader "Custom/FogOfWarMask" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlurPower ("BlurPower" , int) = 5
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
		uniform float4 _MainTex_TexelSize;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			//Creation d'un blur fonction de la distance une zone de champ de vision (par voisinage)
			half col = 0;
			for (int i = -_BlurPower; i < _BlurPower; ++i)
			{
				for (int j = -_BlurPower; j < _BlurPower; ++j)
				{
					col += tex2D(_MainTex, IN.uv_MainTex + float2(i * _MainTex_TexelSize.x,j * _MainTex_TexelSize.y)).g;
				}
			}
			col = col / (_BlurPower*_BlurPower);
			
			o.Albedo = _Color.rgb ;
			o.Alpha = _Color.a - col;
	 	}
		ENDCG
	} 
	FallBack "Diffuse"
}
