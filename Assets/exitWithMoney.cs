using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitWithMoney : MonoBehaviour
{

    public GameObject globalManager;
    public GameObject gameUICanvas;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if (GlobalManager.gold == 500)
            {
                gameUICanvas.GetComponent<PauseMenu>().Win();
                gameObject.SetActive(false);
            }
        }
    }



}
