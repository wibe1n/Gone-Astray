// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Metropolia/GrassShader"
{
	Properties
	{
		_MainTex("Main Texure", 2D) = "white" {}
		_ColorTex("Color Texure", 2D) = "white" {}
		_Cutoff("Alpha Cutoff", range(0,1)) = 0.5
		/*_WindDir("Wind Direction", Vector) = (0,0,0,0)
		_WindDirTex("Wind Dir Texture", 2D) = "black" {}
		_WindStrTex("Wind Strenght Texture", 2D) = "black" {}
		_Speed("Wind speed", float) = 1
		_Intensity("Intencity", range(0,1)) = 0.5*/
	}

	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest"
			"IgnoreProjection" = "True"
			"RenderType" = "TransparentCutout"
		}

		CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles

#pragma surface surf Lambert vertex:vert alphatest:_Cutoff

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_ColorTex;
		};

		sampler2D _MainTex;
		sampler2D _ColorTex;
		float4 _PlayerPos;
		/*sampler2D _WindDirTex;
		sampler2D _WindStrTex;
		float4 _WindDir;
		float _Intensity;
		float _Speed;*/

		//float4 wind(float2 uv)
		//{
		//	float2 wd = tex2Dlod(_WindDirTex, float4(uv.x, uv.y , 0, 0)).xy; //samplaa tuulipatternin kuvasta
		//	float t = wd.x; //tallennetaan pelkkä x komponentti
		//	wd = (wd - 0.5) * 2; //säädetään koko tuulipatternia
		//	float u = fmod(_Time[1] * _Speed - t, 1.0); //otetaan aika mukaan
		//	float w = tex2Dlod(_WindStrTex, float4(u, 0, 0, 0)); //otetaan tuulenvoimakkuus mukaan
		//	return normalize(_WindDir + float4(wd.x, 0, wd.y, 0) * _Intensity) * w; //normalisoidaan koko roska
		//}

		void vert(inout appdata_full v)
		{
			//float4 w = wind(v.texcoord1.xy); //tehään tuulilasku
			//v.vertex += w * v.texcoord.y; //heitetään tuuli vertexin coordinaatteihin

			float3 worldPos = mul((float3x4)unity_ObjectToWorld, v.vertex); //haetaan vertexin koordinaatit Unity scenen suhteen
			float3 bendDir = normalize(float3(worldPos.x, 0, worldPos.z) - float3(_PlayerPos.x, 0, _PlayerPos.z)); //lasketaan taipumisen suunta
			bendDir = cos(_Time[1]) * bendDir;
			float distLimit = 0.7; //pelaajan "hitboxin" radius
			float distMulti = (distLimit - min(distLimit, distance(float3(worldPos.x, 0, worldPos.z), float3(_PlayerPos.x, 0, _PlayerPos.z)))) / distLimit; //varsinainen lasku
			v.vertex.xz += bendDir.xz*distMulti*v.texcoord.y*v.texcoord.y*0.9; //heitetään taivutus vertexin coordinaatteihin
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 m = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex2D(_ColorTex, IN.uv_ColorTex);
			o.Albedo = c.rgb;
			o.Alpha = m.a;
		}

	ENDCG
	}
}
