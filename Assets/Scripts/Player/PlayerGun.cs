using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGun : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public AudioSource fireGun;

    public int killCount;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI deathNotice;


    private Rigidbody rb;
    public GameObject aCamera;
    public GameObject explotion;
    private AudioSource sound;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        fireGun = GetComponent<AudioSource>();
        killCount = GlobalManager.killCount;
        killCountText = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        deathNotice = GameObject.Find("DeathNotice").GetComponent<TextMeshProUGUI>();


        sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                fireGun.Play();
                muzzleFlash.Play();
                RaycastHit hit;

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
                {
                    GameObject enemy = hit.collider.gameObject.transform.parent.gameObject;
                    if (transform.tag != enemy.transform.tag &&
                        (enemy.transform.tag == "robber" || enemy.transform.tag == "cop"))
                    {
                        enemy.GetComponent<CopsRobbersBehaviour>().health -= 20;
                        if (enemy.GetComponent<CopsRobbersBehaviour>().health == 0)
                        {
                            killCount = GlobalManager.killCount;
                            killCount++;
                            killCountText.text = "Kill Count: " + killCount;
                            deathNotice.text = "You Killed " + enemy.name + "!";
                            GlobalManager.killCount = killCount;
                        }
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.Q) && !GameObject.Find("Grenade(Clone)"))    // throwing grenade
            {
                Rigidbody rb = Instantiate(projectile, transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 18f, ForceMode.Impulse);
                rb.AddForce(transform.up * 7f, ForceMode.Impulse);
                rb.useGravity = true;
            }
            
        }
    }






}
