using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseScreen;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        //ResumeGame();
        //PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && player != null)
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        SoundManagerScript.PlaySound("select");
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void ResumeGame()
    {
        SoundManagerScript.PlaySound("select");
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void RestartGame()
    {
        SoundManagerScript.PlaySound("select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SoundManagerScript.PlaySound("select");
        SceneManager.LoadScene("Start");
    }
}
