using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject gameCanvasUI;
    public GameObject pauseMenuUI;
    public GameObject LoseCanvasUI;
    public GameObject WinCanvasUI;
    private AudioSource clickSound;

    
    // Start is called before the first frame update
    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (GlobalManager.health <= 0)
        {
            Lose();
        }
        if ((GlobalManager.killCount >= 10 && GameObject.Find("FirstPersonPlayer").transform.tag == "cop")||
            (GameObject.Find("FirstPersonPlayer").transform.tag == "robber" && GlobalManager.EndLine) )
        {
            Win();
        }
    }

    public void Resume()
    {
        gameCanvasUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        LoseCanvasUI.SetActive(false);
        WinCanvasUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        gameCanvasUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        LoseCanvasUI.SetActive(false);
        WinCanvasUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lose()
    {
        LoseCanvasUI.SetActive(true);
        gameCanvasUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        WinCanvasUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Win()
    {
        WinCanvasUI.SetActive(true);
        LoseCanvasUI.SetActive(false);
        gameCanvasUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        GlobalManager.EndLine = false;
    }

    public void LoadMenu()
    {
        clickSound.Play();
        Destroy(GameObject.Find("GameManager"));
        GameIsPaused = false;
        GlobalManager.health = 500;
        GlobalManager.killCount = 0;
        GlobalManager.gold = 0;
        StartCoroutine(StartSceneTransition(0));
    }

    public void QuitGame()
    {
        clickSound.Play();
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    IEnumerator StartSceneTransition(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(1.05f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
}
