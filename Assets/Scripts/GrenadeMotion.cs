using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeMotion : MonoBehaviour
{
    public GameObject explotion;
    public GameObject sphere;
    public GameObject cube;
    public AudioSource sound;

    public int killCount;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI deathNotice;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

        killCount = GlobalManager.killCount;
        killCountText = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        deathNotice = GameObject.Find("DeathNotice").GetComponent<TextMeshProUGUI>();


        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2);
        explotion.SetActive(true);
        sphere.SetActive(false);
        cube.SetActive(false);
        sound.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        for(int i =0; i < colliders.Length ;i++)
        {
            Rigidbody r = colliders[i].GetComponent<Rigidbody>();
            if(r != null)
            {
                if (colliders[i].transform.tag == "robber" || colliders[i].transform.tag == "cop")
                {
                    colliders[i].GetComponent<CopsRobbersBehaviour>().health -= 60;

                    if (colliders[i].GetComponent<CopsRobbersBehaviour>().health <= 0 &&  colliders[i].transform.tag != GameObject.Find("FirstPersonPlayer").tag)
                    {
                        killCount = GlobalManager.killCount;
                        killCount++;
                        killCountText.text = "Kill Count: " + killCount;
                        deathNotice.text = "You Killed " + colliders[i].name + "!";
                        GlobalManager.killCount = killCount;

                    }
                }
                else if (colliders[i].transform.tag == "NPC")
                    continue;
                else
                    r.AddExplosionForce(1500, transform.position, 10);
            }
           
        }
        yield return new WaitForSeconds(0.5f);
        explotion.SetActive(false);
        GameObject g = GameObject.Find("Grenade(Clone)");
        Destroy(g);

    }




}
