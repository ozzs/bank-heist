using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyBehaviour : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public static int numCoins;
    public GameObject Money;
    private AudioSource pickSound;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = GlobalManager.gold; // save coins on scene transition
        pickSound = Money.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && other.gameObject.transform.tag == "robber")
        {
            numCoins = numCoins + 100;
            coinsText.text = numCoins + " $";
            GlobalManager.gold = numCoins;

            pickSound.PlayDelayed(0);
            this.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
