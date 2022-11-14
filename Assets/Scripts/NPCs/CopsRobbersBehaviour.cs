using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CopsRobbersBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject des;
    [SerializeField]
    GameObject secDes;
    private bool goToSafe = false;

    public GameObject currEnemy;
    private Animator animator;
    private NavMeshAgent agent;
    private AudioSource shootingSound;
    public ParticleSystem muzzleFlash;

    public string enemyTag = "";
    public float health = 120;

    private float timeToDeSpawn = 5;
    private bool reachedDis = false;

    public GameObject prefabNPC;
    private string respawnByTag;

    public TextMeshProUGUI deathNotice;

    /////////////////

    public float radius;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeEnemy;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        shootingSound = GetComponent<AudioSource>();
        respawnByTag = transform.tag;

        if (transform.tag == "robber")
        {
            checkForCop();
        }
        else
        {
            agent.SetDestination(des.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            transform.tag = "Untagged";
            animator.SetInteger("state", 3);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            timeToDeSpawn -= Time.deltaTime;

            if (timeToDeSpawn <= 0)
            {
                if (respawnByTag == "robber")
                {
                    Respawn(prefabNPC, "RobberSpawner", 100, "robber");
                }
                else
                {
                    /*
                    int rndSpwan = Random.Range(1, 4);
                    string SpawnName = "CopSpawner" + rndSpwan;
                    Respawn(prefabNPC, SpawnName, 200, "cop");
                    */
                }
                Destroy(gameObject);
            }

            return;
        }

        if (agent.enabled == false)
        {
            return;
        }

        if (reachedDis == false)
        {
            reachingDes();
        }

        if (currEnemy != null)
        {
            agent.isStopped = true;

            animator.SetInteger("state", 2);

            if (currEnemy.tag == "Untagged")
            {
                checkForCop();
            }
            else
            {
                // transform.LookAt(currEnemy.transform, transform.up);
                transform.LookAt(new Vector3(currEnemy.transform.position.x, transform.position.y, currEnemy.transform.position.z));
            }

        }

        if (des == null)
        {
            checkForRobber();
            checkForCop();
        }
    }

    public void hitEnemy()
    {
        if (currEnemy != null)
        {
            shootingSound.Play();
            muzzleFlash.Play();
            int rnd = Random.Range(0, 4);
            if (rnd >= 1) // Hit
            {
                if (currEnemy.layer == 3)
                {
                    currEnemy.GetComponent<PlayerBehaviour>().currentHealth -= 20;
                    GlobalManager.health = currEnemy.GetComponent<PlayerBehaviour>().currentHealth;

                    if (currEnemy.GetComponent<PlayerBehaviour>().currentHealth <= 0)
                    {
                        currEnemy = null;
                        checkForCop();
                        checkForRobber();
                    }
                }
                else
                {
                    currEnemy.GetComponent<CopsRobbersBehaviour>().health -= 20;

                    if (currEnemy.GetComponent<CopsRobbersBehaviour>().health <= 0)
                    {
                        deathNotice.text = name + " Killed " + currEnemy.name + "!";
                        currEnemy = null;
                        checkForCop();
                        checkForRobber();
                    }
                }

            }
        }
        else
        {
            checkForCop();
            checkForRobber();
        }
    }

    void reachingDes()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {

                    
                        
                        if (goToSafe == false && secDes != null)
                            agent.SetDestination(secDes.transform.position);
                        else
                            agent.SetDestination(des.transform.position);

                        goToSafe = !goToSafe;
                    
                    
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (animator.GetInteger("state") == 3)
        {
            return;
        }

        if (other.tag == enemyTag)
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget < 15)
                {
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeeEnemy = true;
                        currEnemy = other.gameObject;
                    }
                    else
                    {
                        canSeeEnemy = false;
                    }
                }
            }
            else if (canSeeEnemy)
            {
                canSeeEnemy = false;
            }
        }
    }

    void checkForCop()
    {
        if (agent.enabled)
        {
            if (transform.tag == "robber")
            {
                GameObject[] cops = GameObject.FindGameObjectsWithTag("cop");

                if (cops.Length > 0)
                {
                    int rnd2 = Random.Range(0, cops.Length);

                    agent.SetDestination(cops[rnd2].transform.position);
                    des = cops[rnd2].gameObject;
                    animator.SetInteger("state", 0);
                    agent.isStopped = false;
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    agent.enabled = false;
                    animator.SetInteger("state", 1);
                }
            }
        }
    }

    void checkForRobber()
    {
        if (agent.enabled)
        {
            if (transform.tag == "cop")
            {
                GameObject[] robbers = GameObject.FindGameObjectsWithTag("robber");

                if (robbers.Length > 0)
                {
                    int rnd2 = Random.Range(0, robbers.Length);

                    agent.SetDestination(robbers[rnd2].transform.position);
                    des = robbers[rnd2].gameObject;
                    animator.SetInteger("state", 0);
                    agent.isStopped = false;
                }
                //else
                //{
                 //   agent.SetDestination(transform.position);
                 //   agent.isStopped = true;
                  //  agent.enabled = false;
                   // animator.SetInteger("state", 1);
               // }
            }
        }
    }


    void Respawn(GameObject prefabNPC, string spawnPointName, float health, string newTag)
    {
        GameObject sp = GameObject.Find(spawnPointName);
        GameObject newRobber = Instantiate(prefabNPC, sp.transform.position, Quaternion.identity);
        newRobber.GetComponent<NavMeshAgent>().enabled = true;
        newRobber.GetComponent<CapsuleCollider>().enabled = true;
        newRobber.GetComponent<CopsRobbersBehaviour>().health = health;
        newRobber.GetComponent<CopsRobbersBehaviour>().currEnemy = null;
        newRobber.GetComponent<CopsRobbersBehaviour>().des = des;
        newRobber.transform.tag = newTag;
        newRobber.name = prefabNPC.name;
        newRobber.GetComponent<Animator>().SetInteger("state", 0);
    }

}
