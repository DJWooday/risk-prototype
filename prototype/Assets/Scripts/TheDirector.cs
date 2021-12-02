using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Our AI director will handle spawning enemies in a slightly nicer
/// fashion than just doing it randomly. We can define the costs for spawning 
/// enemies and how often our director gets credits. Essentially we're offloading
/// the role of a DM to the computer.
/// </summary>
public class TheDirector : MonoBehaviour
{
    [SerializeField] private float credits = 100;
    [SerializeField] private float creditRate = 10;

    [SerializeField] private Transform[] spawnPoints;

    [Header("Prefabs")]
    public GameObject basic;
    public GameObject tank;
    public GameObject fast;

    [Header("Prices")]
    [SerializeField] private float basicCost = 20;
    [SerializeField] private float tankCost = 100;
    [SerializeField] private float fastCost = 40;

    // Start is called before the first frame update
    void Start()
    {
        // Gain credits every second
        InvokeRepeating("GainCredits", 1, 1);
        // Try spawning every few seconds.
        // For a more advanced approach, we could spawn based on some probabilistic
        // model taking the player's health, progress, and other factors into account.
        // For prototyping purposes, it's okay to do it on an interval
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTimer() {
        // This coroutine spawns an enemy every 2 to 10 seconds
        while (true) {
            float secs = Random.Range(2, 10);
            yield return new WaitForSeconds(secs);
            Spawn();
        }
    }

    void GainCredits() {
        credits += creditRate;
    }

    void Spawn() {
        // Only spawn if we have at least 40 credits, gotta let the director save up
        if (credits < 2*basicCost) return;

        GameObject enemyToSpawn;
        bool set = false;

        // Pick a random spawn point
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        // We'll roll the dice and pick from a "probability table" to see what mob we
        // want to spawn. If we can't pick it, try again.
        do {

            // Roll dice to pick randomly
            int i = Random.Range(0, 100);
            if (i < 60 && credits >= basicCost) {
                enemyToSpawn = Instantiate(basic, spawnPoint, Quaternion.identity);
                credits -= basicCost;
                set = true;
            } else if (i < 90 && credits >= fastCost) {
                enemyToSpawn = Instantiate(fast, new Vector3(10, 2, 0), Quaternion.identity);
                credits -= fastCost;
                set = true;
            } else if (i < 100 && credits >= tankCost) {
                enemyToSpawn = Instantiate(tank, new Vector3(10, 2, 0), Quaternion.identity);
                credits -= tankCost;
                set = true;
            }
        } while (!set);
        // This will be HEAVILY skewed to producing basics, since we'll run out of credits pretty easily.
    }
}
