using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGun : MonoBehaviour
{
    public GameObject gunInHand;
    public GameObject gunInDrawer;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnMouseDown()
    {
        gunInHand.transform.gameObject.SetActive(true);
        gunInDrawer.transform.gameObject.SetActive(false);


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
