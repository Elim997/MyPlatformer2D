using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    public static UnityAction<int> waveKillCount;

    //public static event Action OnPlayerDeath;

    public int KillCount = 0;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get { return _health; }
        set 
        { 
            
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health<=0)
            {
                
                IsAlive = false;
            }
        
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    //public bool IsHit { 
    //    get 
    //    {
    //        return animator.GetBool(AnimationStrings.isHit);
    //    }
    //    private set 
    //    {
    //        animator.SetBool(AnimationStrings.isHit, value);
    //    }
    //}

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get { return _isAlive; }
        set 
        { 
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive set " + value);

            if (value == false)
            {
                damageableDeath?.Invoke();
                Debug.Log("Im dead lol");
                KillCount++;
                waveKillCount?.Invoke(KillCount);

            }
        }
    }
    // the velocity shouldn't change while this is true but needs to be respected by other physics components like player controller
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // remove invincible
                isInvincible = false;
                timeSinceHit = 0;
            }
            // time between frames
            timeSinceHit += Time.deltaTime;
        }
       
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            // notify other subscribed components that the damageable was hit to handle the knockback
            damageableHit?.Invoke(damage, knockback);
            // invoke to trigger the event
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        else
        {
           
            return false;
        }
    }
    
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            // if the health is equals to or greater than the max health then it will return a negetive, if so it defults the heal to 0 (heal cap).
            int maxHeal = Mathf.Max(MaxHealth - Health,0);
            // healthRestored expected amount to heal
            int actualHeal = Mathf.Min(maxHeal,healthRestore);
            Health += actualHeal;
            // when the damageable component got healed we invoke the unity event characterHealed
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }



}
