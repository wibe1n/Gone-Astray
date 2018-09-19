using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PencilContourEffect : MonoBehaviour
{
	public Material m_Mat;
	public Texture2D m_NoiseTex;
	[Range(10f, 50f)] public float m_ErrorPeriod = 25f;
	[Range(0f, 0.005f)] public float m_ErrorRange = 0.0015f;
	[Range(0f, 0.1f)] public float m_NoiseAmount = 0.02f;
	[Range(0f, 1f)] public float m_EdgesOnly = 0f;
	[Range(1f, 5f)] public float m_SampleDistance = 1f;
	public Color m_EdgeColor = Color.black;
	public Color m_BackgroundColor = Color.white;

	void Start()
	{
		if (SystemInfo.supportsImageEffects == false || SystemInfo.supportsRenderTextures == false)
			enabled = false;
	}
	void OnEnable()
	{
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}
	void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		m_Mat.SetTexture ("_NoiseTex", m_NoiseTex);
		m_Mat.SetFloat ("_EdgeOnly", m_EdgesOnly);
		m_Mat.SetFloat ("_ErrorPeriod", m_ErrorPeriod);
		m_Mat.SetFloat ("_ErrorRange", m_ErrorRange);
		m_Mat.SetFloat ("_NoiseAmount", m_NoiseAmount);
		m_Mat.SetFloat ("_SampleDistance", m_SampleDistance);
		m_Mat.SetColor ("_EdgeColor", m_EdgeColor);
		m_Mat.SetColor ("_BackgroundColor", m_BackgroundColor);

		RenderTexture rt = RenderTexture.GetTemporary (src.width, src.height, 0);
		Graphics.Blit (src, rt, m_Mat, 0);
		m_Mat.SetTexture ("_EdgeTex", rt);
		Graphics.Blit (src, dest, m_Mat, 1);
		RenderTexture.ReleaseTemporary (rt);
	}
}
