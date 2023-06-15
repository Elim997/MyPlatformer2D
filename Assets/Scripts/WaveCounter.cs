using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{

    public TMP_Text WaveCounterText;
    public TMP_Text MonsterAliveText;
    public TMP_Text KillCountText;

    
    WaveSpawner waveSpawner;

    

    
    private void Awake()
    {
        GameObject wave = GameObject.FindGameObjectWithTag("WaveSpawner");
        
        waveSpawner = wave.GetComponent<WaveSpawner>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        WaveCounterText.text = "Wave : " + waveSpawner.currWave;
        MonsterAliveText.text = "Monster Alive : " + waveSpawner.spawnedEnemies.Count;
        KillCountText.text = "Score : " + waveSpawner.killCountChanged;
    }

    private void OnEnable()
    {
        waveSpawner.waveChanged.AddListener(OnWaveChanged);
        waveSpawner.waveMonsterAlive.AddListener(OnMonsterCount);
        waveSpawner.killCountChanged.AddListener(OnKillCount);
    }

    private void OnDisable()
    {
        waveSpawner.waveChanged.RemoveListener(OnWaveChanged);
        waveSpawner.waveMonsterAlive.RemoveListener(OnMonsterCount);
        waveSpawner.killCountChanged.RemoveListener(OnKillCount);
    }

    void OnWaveChanged(int currentWave)
    {
        WaveCounterText.text = "Wave : " + currentWave;
        
    }

    void OnMonsterCount(int monsterCount)
    {
        MonsterAliveText.text = "Monster Alive : " + monsterCount;
    }

    void OnKillCount(int killCount)
    {
        KillCountText.text = "Score : " + killCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
