using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{

    MusicPlayer musicPlayer;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void GoToSettingMenu()
    {
        SceneManager.LoadScene("SettingsMenuScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void QuitGame()
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

    public void MuteGame()
    {
        musicPlayer.loopSource.mute = !musicPlayer.loopSource.mute;
    }

    public void WindowMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

}
