using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    public GameObject DifficultyMenu;

    public Canvas gameCanvas;

    public WaveSpawner waveObj;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    public void EasyButtonPressed()
    {


        WaveSpawner.difficultyMultiplier = 10;
        DifficultyMenu.SetActive(false);
        waveObj.GenerateWave();
        Time.timeScale = 1f;
    }

    public void NormalButtonPressed()
    {


        WaveSpawner.difficultyMultiplier = 15;
        DifficultyMenu.SetActive(false);
        waveObj.GenerateWave();
        Time.timeScale = 1f;
    }

    public void HardButtonPressed()
    {


        WaveSpawner.difficultyMultiplier = 20;
        DifficultyMenu.SetActive(false);
        waveObj.GenerateWave();
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        DifficultyMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
