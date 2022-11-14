using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenDoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "FirstPersonPlayer")
        {
            animator.SetBool("isOpening", true);
            audioSrc.PlayDelayed(1);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "FirstPersonPlayer")
        {
            animator.SetBool("isOpening", false);
            audioSrc.PlayDelayed(1);
        }
    }


    void Update()
    {

    }
}
