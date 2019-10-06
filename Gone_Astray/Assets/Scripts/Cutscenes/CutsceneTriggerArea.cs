using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTriggerArea : MonoBehaviour
{
    public int id;
    public TutoLvl lvl0script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider player)
    {
        lvl0script.PlayNextCutscene(id);
    }
}
