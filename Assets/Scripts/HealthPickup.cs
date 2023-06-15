using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthRestore = 20;
    public Vector3 spinSpeed = new Vector3(0, 180, 0);

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Update()
    {
        transform.eulerAngles += spinSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // detects damageable character walks into the box collider zone
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            // then we call the Heal func on the damageable component
            bool wasHealed = damageable.Heal(healthRestore);
            // use once and disapear
            if (wasHealed)
            {
                if (pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }

                Destroy(gameObject);
            }
        }

    }

}
