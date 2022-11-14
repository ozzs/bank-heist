using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer_Med3 : MonoBehaviour
{

    public GameObject CrossHair;
    public GameObject CrossHairTouch;
    public GameObject Eye;
    public Text instruction;
    private Animator animator;
    private AudioSource sound;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Eye.transform.position, Eye.transform.forward, out hit))
        {
            if (hit.transform.gameObject == this.gameObject)     // this game object is the scripts object
            {
                CrossHair.SetActive(false);
                CrossHairTouch.SetActive(true);
                instruction.gameObject.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.O))
                {
                    animator.SetBool("Open", true);
                    sound.PlayDelayed(0.5f);
                }


                if (Input.GetKeyDown(KeyCode.C))
                {
                    animator.SetBool("Open", false);
                    sound.Play();
                }


            }
            else
            {
                CrossHairTouch.SetActive(false);
                CrossHair.SetActive(true);
                instruction.gameObject.SetActive(false);

            }

        }


    }
}
