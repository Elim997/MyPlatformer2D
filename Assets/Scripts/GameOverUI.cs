using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverMenu;

    
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

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

}
