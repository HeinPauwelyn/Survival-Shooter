using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform[] spawnPoints;
    public GameObject[] pickups;
    public float spawnTime = 3f;
    
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnPickUp", spawnTime, spawnTime);
    }

    void SpawnPickUp()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int pickUpIndex = Random.Range(0, pickups.Length);
        
        Instantiate(pickups[pickUpIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
