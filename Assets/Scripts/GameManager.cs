using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Menu States
    public enum MenuStates { Main, Sides };
    public MenuStates currentState;

    // Menu Panel Objects
    public GameObject canvas;
    public GameObject mainMenu;
    public GameObject sidesMenu;

    public static GameManager instance = null;
    public bool isRobber = false;

    public GameObject prefabPlayer;
    private GameObject playerDetails;

    private AudioSource clickSound;

    void Awake()
    {
        currentState = MenuStates.Main;
        clickSound = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        switch (currentState)
        {
            // Sets active gameObject for main menu
            case MenuStates.Main:
                mainMenu.SetActive(true);
                sidesMenu.SetActive(false);
                break;

            // Sets active gameObject for sides menu
            case MenuStates.Sides:
                if (sidesMenu != null)
                {
                    sidesMenu.SetActive(true);
                    mainMenu.SetActive(false);
                }
                break;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            if (isRobber)
            {
                GameObject sp = GameObject.Find("RobberSpawnPoint");
                playerDetails = Instantiate(prefabPlayer, sp.transform.position, Quaternion.identity);
                playerDetails.transform.tag = "robber";
                playerDetails.name = prefabPlayer.name;
            }
            else
            {
                GameObject sp = GameObject.Find("CopSpawnPoint");
                playerDetails = Instantiate(prefabPlayer, sp.transform.position, Quaternion.identity);
                playerDetails.transform.tag = "cop";
                playerDetails.name = prefabPlayer.name;
            }
        }
    }

    public void chooseRobbers()
    {
        clickSound.Play();
        isRobber = true;
        canvas.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void chooseCops()
    {
        clickSound.Play();
        isRobber = false;
        canvas.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void playClicked()
    {
        clickSound.Play();
        currentState = MenuStates.Sides;
    }

    public void backClicked()
    {
        clickSound.Play();
        currentState = MenuStates.Main;
    }

    public void quitClicked()
    {
        clickSound.Play();
        Application.Quit();
    }
}
