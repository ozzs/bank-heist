using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "robber" && GlobalManager.gold >= 500)
            GlobalManager.EndLine = true;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
