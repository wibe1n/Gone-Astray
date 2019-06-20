using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Footsteps : MonoBehaviour
{
    
    [FMODUnity.EventRef]
    public string m_EventPath;

    public float m_GroundType;

    public float m_StepDistance;

    Vector3 m_PrevPos;
    float m_DistanceTravelled;

    public bool m_Debug;
    Vector3 m_LinePos;
    Vector3 m_TrianglePoint0;
    Vector3 m_TrianglePoint1;
    Vector3 m_TrianglePoint2;

    public Movement movement;

    void Start()
    {
        //Initialise random, set seed
        Random.InitState(System.DateTime.Now.Second);

        //Initialise member variables
        m_StepDistance = 1;
        m_PrevPos = transform.position;
        m_LinePos = transform.position;
    }

    void Update()
    {
        // Kun ollaan edetty tietyn verran matkaa ja ollaan maassa soitetaan jalanjälki ääni
        m_DistanceTravelled += (transform.position - m_PrevPos).magnitude;

        if (m_DistanceTravelled >= m_StepDistance)//TODO: Play footstep sound based on position from headbob script
        {
            if (movement.m_IsGrounded) {
                PlayFootstepSound();
            }
            m_DistanceTravelled = 0.0f;
        }

        m_PrevPos = transform.position;

        if (m_Debug)
        {
            Debug.DrawLine(m_LinePos, m_LinePos + Vector3.down * 1000.0f);
            Debug.DrawLine(m_TrianglePoint0, m_TrianglePoint1);
            Debug.DrawLine(m_TrianglePoint1, m_TrianglePoint2);
            Debug.DrawLine(m_TrianglePoint2, m_TrianglePoint0);
        }
    }

    void PlayFootstepSound()
    {
        //Defaults
        m_GroundType = 0.0f;

        RaycastHit hit;
        // lähetetään maahan raycasti, haetaan maaston materiaali ja soitetaan oikea ääni sen perusteella. Jos ei ole materiaalia, niin soitetaan default ääni
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000.0f))
        {
            if (m_Debug)
                m_LinePos = transform.position;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                
                int materialIndex = GetMaterialIndex(hit);
                if (materialIndex != -1)
                {
                    Material material = hit.collider.gameObject.GetComponent<Renderer>().materials[materialIndex];
                    if (material.name == "SwampMat1 (Instance)")
                    {
                        
                        if (m_Debug)
                        {//Calculate the points for the triangle in the mesh that we have hit with our raycast.
                            MeshFilter mesh = hit.collider.gameObject.GetComponent<MeshFilter>();
                            if (mesh)
                            {
                                Mesh m = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                                m_TrianglePoint0 = hit.collider.transform.TransformPoint(m.vertices[m.triangles[hit.triangleIndex * 3 + 0]]);
                                m_TrianglePoint1 = hit.collider.transform.TransformPoint(m.vertices[m.triangles[hit.triangleIndex * 3 + 1]]);
                                m_TrianglePoint2 = hit.collider.transform.TransformPoint(m.vertices[m.triangles[hit.triangleIndex * 3 + 2]]);
                            }
                        }

                        //The mask texture determines how the material's main two textures are blended.
                        //Colour values from each texture are blended based on the mask texture's alpha channel value.
                        //0.0f is full dirt texture, 1.0f is full sand texture, 0.5f is half of each. 
                        //Texture2D maskTexture = material.GetTexture("_Mask") as Texture2D;
                        //Color maskPixel = maskTexture.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                        //The specular texture maps shininess / gloss / reflection to the terrain mesh.
                        //We are using it to determine how much water is shown at the cast ray's point of intersection.
                        //Texture2D specTexture2 = material.GetTexture("_SpecGlossMap2") as Texture2D;
                        //We apply tiling assuming it is not already applied to hit.textureCoord2
                        //float tiling = 40.0f;//This is a public variable set on the material, we could reference the actual variable but I ran out of time.
                        //float u = hit.textureCoord.x % (1.0f / tiling);
                        //float v = hit.textureCoord.y % (1.0f / tiling);
                        //Color spec2Pixel = specTexture2.GetPixelBilinear(u, v);

                        //float specMultiplier = 6.0f;//We use a multiplier to better represent the amount of water.
                        //m_Water = maskPixel.a * Mathf.Min(spec2Pixel.a * specMultiplier, 0.9f);//Only the sand texture has water, so we multiply by the mask pixel alpha value.
                        //m_Dirt = (1.0f - maskPixel.a);
                        //m_Sand = maskPixel.a - m_Water * 0.1f;//Ducking the sand a little for the water
                        //m_Wood = 0.0f;
                        m_GroundType = 0.2f;

                    }


                }
            }
            else//If the ray hits somethign other than the ground, we assume it hit a wooden prop - and set the parameter values for wood.
            {
                m_GroundType = 0.0f;
            }
        }


        if (m_EventPath != null)
        {
            FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(m_EventPath);
            e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            SetParameter(e, "GroundType", m_GroundType);

            e.start();
            e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
        }
    }

    void SetParameter(FMOD.Studio.EventInstance e, string name, float value)
    {
        FMOD.Studio.ParameterInstance parameter;
        e.getParameter(name, out parameter);
        parameter.setValue(value);
    }

    int GetMaterialIndex(RaycastHit hit)
    {
        Mesh m = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
        int[] triangle = new int[]
        {
            m.triangles[hit.triangleIndex * 3 + 0],
            m.triangles[hit.triangleIndex * 3 + 1],
            m.triangles[hit.triangleIndex * 3 + 2]
        };
        for (int i = 0; i < m.subMeshCount; ++i)
        {
            int[] triangles = m.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3)
            {
                if (triangles[j + 0] == triangle[0] &&
                    triangles[j + 1] == triangle[1] &&
                    triangles[j + 2] == triangle[2])
                    return i;
            }
        }
        return -1;
    }
}
