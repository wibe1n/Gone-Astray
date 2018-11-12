using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This script plays footstep sounds.
//It will play a footstep sound after a set amount of distance travelled.
//When playing a footstep sound, this script will cast a ray downwards. 
//If that ray hits the ground terrain mesh, it will retreive material values to determine the surface at the current position.
//If that ray does not hit the ground terrain mesh, we assume it has hit a wooden prop and set the surface values for wood.
public class Footsteps : MonoBehaviour
{
    //FMOD Studio variables
    //The FMOD Studio Event path.
    //This script is designed for use with an event that has a game parameter for each of the surface variables, but it will still compile and run if they are not present.
    [FMODUnity.EventRef]
    public string m_EventPath;

    //Surface variables
    //Range: 0.0f - 1.0f
    //These values represent the amount of each type of surface found when raycasting to the ground.
    //They are exposed to the UI (public) only to make it easy to see the values as the player moves through the scene.
    public float m_Wood;
    public float m_Water;
    public float m_Dirt;
    public float m_Sand;

    //Step variables
    //These variables are used to control when the player executes a footstep.
    //This is very basic, and simply executes a footstep based on distance travelled.
    //Ideally, in this case, footsteps would be triggered based on the headbob script. Or if there was an animated player model it could be triggered from the animation system.
    //You could also add variation based on speed travelled, and whether the player is running or walking. 
    public float m_StepDistance = 2.0f;
    float m_StepRand;
    Vector3 m_PrevPos;
    float m_DistanceTravelled;

    //Debug variables
    //If m_Debug is true, this script will:
    // - Draw a debug line to represent the ray that was cast into the ground.
    // - Draw the triangle of the mesh that was hit by the ray that was cast into the ground.
    // - Log the surface values to the console.
    // - Log to the console when an expected game parameter is not found in the FMOD Studio event.
    public bool m_Debug;
    Vector3 m_LinePos;
    Vector3 m_TrianglePoint0;
    Vector3 m_TrianglePoint1;
    Vector3 m_TrianglePoint2;

    void Start()
    {
        //Initialise random, set seed
        Random.InitState(System.DateTime.Now.Second);

        //Initialise member variables
        m_StepRand = Random.Range(0.0f, 0.5f);
        m_PrevPos = transform.position;
        m_LinePos = transform.position;
    }

    void Update()
    {
        m_DistanceTravelled += (transform.position - m_PrevPos).magnitude;
        if (m_DistanceTravelled >= m_StepDistance + m_StepRand)//TODO: Play footstep sound based on position from headbob script
        {
            PlayFootstepSound();
            m_StepRand = Random.Range(0.0f, 0.5f);//Adding subtle random variation to the distance required before a step is taken - Re-randomise after each step.
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
        m_Water = 0.0f;
        m_Dirt = 1.0f;
        m_Sand = 0.0f;
        m_Wood = 0.0f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000.0f))
        {
            if (m_Debug)
                m_LinePos = transform.position;

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))//The Viking Village terrain mesh (terrain_near_01) is set to the Ground layer.
            {
                int materialIndex = GetMaterialIndex(hit);
                if (materialIndex != -1)
                {
                    Material material = hit.collider.gameObject.GetComponent<Renderer>().materials[materialIndex];
                    if (material.name == "mat_terrain_near_01 (Instance)")//This texture name is specific to the terrain mesh in the Viking Village scene.
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
                        Texture2D maskTexture = material.GetTexture("_Mask") as Texture2D;
                        Color maskPixel = maskTexture.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                        //The specular texture maps shininess / gloss / reflection to the terrain mesh.
                        //We are using it to determine how much water is shown at the cast ray's point of intersection.
                        Texture2D specTexture2 = material.GetTexture("_SpecGlossMap2") as Texture2D;
                        //We apply tiling assuming it is not already applied to hit.textureCoord2
                        float tiling = 40.0f;//This is a public variable set on the material, we could reference the actual variable but I ran out of time.
                        float u = hit.textureCoord.x % (1.0f / tiling);
                        float v = hit.textureCoord.y % (1.0f / tiling);
                        Color spec2Pixel = specTexture2.GetPixelBilinear(u, v);

                        float specMultiplier = 6.0f;//We use a multiplier to better represent the amount of water.
                        m_Water = maskPixel.a * Mathf.Min(spec2Pixel.a * specMultiplier, 0.9f);//Only the sand texture has water, so we multiply by the mask pixel alpha value.
                        m_Dirt = (1.0f - maskPixel.a);
                        m_Sand = maskPixel.a - m_Water * 0.1f;//Ducking the sand a little for the water
                        m_Wood = 0.0f;
                    }
                }
            }
            else//If the ray hits somethign other than the ground, we assume it hit a wooden prop (This is specific to the Viking Village scene) - and set the parameter values for wood.
            {
                m_Water = 0.0f;
                m_Dirt = 0.0f;
                m_Sand = 0.0f;
                m_Wood = 1.0f;
            }
        }

        if (m_Debug)
            Debug.Log("Wood: " + m_Wood + " Dirt: " + m_Dirt + " Sand: " + m_Sand + " Water: " + m_Water);

        if (m_EventPath != null)
        {
            FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(m_EventPath);
            e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            SetParameter(e, "Wood", m_Wood);
            SetParameter(e, "Dirt", m_Dirt);
            SetParameter(e, "Sand", m_Sand);
            SetParameter(e, "Water", m_Water);

            e.start();
            e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
        }
    }

    void SetParameter(FMOD.Studio.EventInstance e, string name, float value)
    {
        FMOD.Studio.ParameterInstance parameter;
        e.getParameter(name, out parameter);
        //if (parameter == null)
        //{
        //    if (m_Debug)
        //        Debug.Log("Parameter named: " + name + " does not exist");
        //    return;
        //}
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
