using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;


    Damageable playerDamageable;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("no player found in the scene");
        }

        playerDamageable = player.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        healthSlider.value = CalculateSliderP(playerDamageable.Health,playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }
    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChange);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChange);
    }

    void OnPlayerHealthChange(int newHealth, int MaxHealth)
    {
        healthSlider.value = CalculateSliderP(newHealth, MaxHealth);
        healthBarText.text = "HP " + newHealth + " / " + MaxHealth;
    }


    private float CalculateSliderP(float currentHealth , float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
