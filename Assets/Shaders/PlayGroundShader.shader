Shader "Custom/PlayGroundShader" {
	Properties {
		 _MainTex ("Texture", 2D) = "white" {}
		 _Center ("Center", Vector) = (0,0,0,0)
		 _Radius ("Radius", Float) = 0.5
		 _Amount ("Extrusion Amount", Range(-0.1,0.1)) = 0
	 }
	 SubShader {
	 	Tags { "RenderType" = "Faded" }
	 CGPROGRAM
	 	#pragma surface surf Lambert vertex:vert
	 	struct Input {
		 	float2 uv_MainTex;
		 	float3 worldPos;
		 	float3 screenPos;
		 	float3 vertNormal;
		};
		float _Amount;
		void vert(inout appdata_full v,out Input o){
			UNITY_INITIALIZE_OUTPUT(Input,o);
			v.vertex.xyz += v.normal * _Amount;
			o.vertNormal = v.normal;
		}
		 sampler2D _MainTex;
		 float3 _Center;
		 float _Radius;
	 
		void surf (Input IN, inout SurfaceOutput o) {
			 float d = distance(_Center, IN.worldPos) / _Radius;
			 d = step(0.99,d)*step(d,1);
			 o.Albedo.b = abs(fmod(IN.vertNormal.x*20,100)-50)/50;
			 o.Alpha = fmod(IN.screenPos.x + IN.screenPos.y,150)/149;
		 }
	 
	 ENDCG

	}
	FallBack "Diffuse"
}
