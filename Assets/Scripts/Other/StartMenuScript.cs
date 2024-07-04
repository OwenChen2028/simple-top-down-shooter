using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{

    public GameObject defaultMenu;
    public GameObject instructionMenu;
    public GameObject loadingText;
    public GameObject playButton;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SoundManagerScript.PlaySound("select");
        playButton.SetActive(false);
        loadingText.SetActive(true);
        SceneManager.LoadScene("Main");
    }

    public void ShowInstructions()
    {
        SoundManagerScript.PlaySound("select");
        defaultMenu.SetActive(false);
        instructionMenu.SetActive(true);
    }

    public void HideInstructions()
    {
        SoundManagerScript.PlaySound("select");
        defaultMenu.SetActive(true);
        instructionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        SoundManagerScript.PlaySound("select");
        Application.Quit();
    }
}
