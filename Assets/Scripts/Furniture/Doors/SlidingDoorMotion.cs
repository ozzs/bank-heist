using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlidingDoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSrc;
    private GameObject[] NPCs;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
    }

    private void OnTriggerStay(Collider other)
    {
        animator.SetBool("isOpening", true);
        audioSrc.PlayDelayed(0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "robber" && this.tag == "bankDoor")
        {
            for (int i = 0; i < NPCs.Length; i++)
            {
                Animator NPCAnimator = NPCs[i].GetComponent<Animator>();
                NPCAnimator.SetInteger("state", 0);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpening", false);
        audioSrc.PlayDelayed(0.5f);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
