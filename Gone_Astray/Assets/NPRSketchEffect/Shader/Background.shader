// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "NPR Sketch Effect/Background" {
	Properties {
		_MainTex ("Background", 2D) = "white" {}
	}
	SubShader {
		Pass {
			Tags { "RenderType" = "Opaque" }
			LOD 200
			Cull Back
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			struct VSOutput
			{
				float4 pos : POSITION;
				float4 posscr : TEXCOORD0;		
			};
			VSOutput vert (appdata_base v)
			{
				VSOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.posscr = ComputeScreenPos(o.pos);  
				return o;
			}
			float4 frag (VSOutput i) : COLOR  
			{
				float2 uv = i.posscr.xy / i.posscr.w;
				return tex2D(_MainTex, uv);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}