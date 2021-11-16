using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currHealth;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(float dmgValue) {
        currHealth -= dmgValue;
        if (currHealth <= 0)
            Destroy(gameObject);
    }
}
