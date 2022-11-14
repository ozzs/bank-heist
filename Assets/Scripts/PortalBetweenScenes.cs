using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBetweenScenes : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    private int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        GlobalManager.health = other.GetComponent<PlayerBehaviour>().currentHealth;

        if (other.gameObject.layer == 3 && other.transform.tag == "robber")
        {
            if (currentSceneIndex == 1)
            {
                StartCoroutine(StartSceneTransition(currentSceneIndex + 1));
            }
            else
            {
                StartCoroutine(StartSceneTransition(currentSceneIndex - 1));
            }
        }
    }

    IEnumerator StartSceneTransition(int sceneIndex)
    {
        animator.SetTrigger("portalTrigger");
        yield return new WaitForSeconds(3); // Delay
        SceneManager.LoadScene(sceneIndex);
    }
}
