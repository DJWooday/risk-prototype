using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currHealth;

    private void Start() {
        currHealth = maxHealth;
    }

    public void TakeDamage(float dmgAmount) {
        currHealth -= dmgAmount;

        if (currHealth <= 0)
            Destroy(gameObject);
    }
 }
