using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI killCountText;
    public static int gold = 0;
    public static float health = 500f;
    public static int killCount = 0;

    public static bool EndLine = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject); // Destroying copy of global object
        }

        DontDestroyOnLoad(gameObject);
        coinsText.text = gold + "";
        killCountText.text = "Kill Count: " + killCount;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
