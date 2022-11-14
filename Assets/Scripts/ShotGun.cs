using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShotGun : MonoBehaviour
{
    public GameObject aCamera;
    public GameObject target;
    private LineRenderer lr;
    public GameObject StartPoint;
    public GameObject aGun;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;

           if(Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {
                target.transform.position = hit.point;
                StartCoroutine(FireFlash());
                enemy = hit.collider.gameObject;
                Debug.Log(hit.collider.name);

                if (hit.collider.gameObject.tag == "NPC" || hit.collider.gameObject.tag == "robber" || hit.collider.gameObject.tag == "cop")
                {
                   StartCoroutine(FallAndStandBack(hit.collider.gameObject.tag));
                }
            }
        }
    }


    IEnumerator FallAndStandBack(string tag)
    {
        NavMeshAgent na = enemy.GetComponent<NavMeshAgent>();
        Animator animator = enemy.GetComponent<Animator>();
        if (tag == "NPC")
        {
            animator.SetInteger("StateTransition", 1);  // fall
            na.enabled = false;
            yield return new WaitForSeconds(3f);
            animator.SetInteger("StateTransition", 2);  // stand up
            yield return new WaitForSeconds(5f);
            animator.SetInteger("StateTransition", 3);  // walk
            yield return new WaitForSeconds(2.2f);
            na.enabled = true;
        }
        else
        {
            animator.SetInteger("state", 2);  // fall
            na.enabled = false;
            yield return new WaitForSeconds(3f);
            animator.SetInteger("state", 3);  // stand up
            yield return new WaitForSeconds(3f);
            animator.SetInteger("state", 0);  // walk
            yield return new WaitForSeconds(2.2f);
            na.enabled = true;
        }
    }


    IEnumerator FireFlash()
    {
        AudioSource sound = aGun.GetComponent<AudioSource>();
        sound.Play();
        lr.enabled = true;
        lr.SetPosition(0, StartPoint.transform.position);
        lr.SetPosition(1, target.transform.position);
        yield return new WaitForSeconds(0.05f);
        lr.enabled = false;
    }

}
