using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetNPCTarget : MonoBehaviour
{
    public GameObject npc;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "NPC"  || other.gameObject.tag == "cop" || other.gameObject.tag == "robber")  // change position of npc target
        {

            float x, y, z;
            x = Random.Range(10, 200);
            z = Random.Range(10, 200);
            y = Terrain.activeTerrain.SampleHeight(new Vector3(x,0,z));

            transform.position = new Vector3(x,y,z);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
