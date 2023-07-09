using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverMenu;

    public MusicPlayer musicPlayer;
    
    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {
        PlayerController.damageDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerController.damageDeath -= EnableGameOverMenu;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitLevel()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif
        #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
                Application.Quit();
        #elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene");
        #endif
    }

    public void MuteButton()
    {
        musicPlayer.introSource.mute = !musicPlayer.introSource.mute;
        musicPlayer.loopSource.mute = !musicPlayer.loopSource.mute;
    }

}
