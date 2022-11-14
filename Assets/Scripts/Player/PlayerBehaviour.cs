using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maxHealth = 500f;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = GlobalManager.health;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (maxHealth <= 0)
        {
            GlobalManager.health = 0;
            transform.tag = "Untagged";
            Debug.Log("You Died!");
            Time.timeScale = 0f;
            return;
        }
    }
}
