using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = currHealth.ToString();
    }

    public void TakeDamage(float dmg) {
        currHealth -= dmg;

        healthBar.fillAmount = currHealth / maxHealth;

        // Die logic
    }
}
