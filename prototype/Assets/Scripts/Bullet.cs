using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10;
    [SerializeField] private float secondsTilDeath;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DieAfterSeconds());
    }

    IEnumerator DieAfterSeconds() {
        yield return new WaitForSeconds(secondsTilDeath);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        if (collision.collider.tag == "Player")
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
    }
}
