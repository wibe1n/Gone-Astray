Shader "Metropolia/WaterShader"
{
	Properties
	{
		_MainTex("Main Tex", 2D) = "white" {}
		_Speed("Speed", float) = 1
		_Transpa("Transparency", range(0.0,1.0)) = 0.5
		_Glow("Intensity", Range(0, 3)) = 1
	}

		SubShader
		{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
			
			CGPROGRAM

			#pragma surface surf Lambert alpha:fade noambient

			struct Input
			{
				float2 uv_MainTex;
			};

			sampler2D _MainTex;
			float _Speed;
			float _Transpa;
			half _Glow;

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex + float2(0,_Speed * sin(_Time[1])));
				c = c * _Glow;
				o.Albedo = c.rgb;
				o.Alpha = _Transpa;
			}
			ENDCG
		}
}