using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs; //is a reference to the Power-Up prefab
    public GameObject powerUpPrefab;
    private float nextSpawnTime; //keeps track of when the next power-up should be spawned.
    private float rightspawnRangeX = 65;
    private float leftspawnRangeX = -19;
    private float spawnPosZ = 6;
    private float startDelay = 1.0f;
    private float spawnInterval = 2.0f; //is the time interval (in seconds) between spawns for animals
    private float spawnIntervalPoweup = 20;// is the time interval (in seconds) between spawns for powerup
    public Vector3 spawnAreaCenter = Vector3.zero; //// Center of the spawn area
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 60f); // Size of the spawn area.
    private PlayerController playercontrollerScript;
  

    // Start is called before the first frame update
    void Start()
    {
     
        playercontrollerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // constantly repeating spawn animals 
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerUp();
            nextSpawnTime = Time.time + spawnIntervalPoweup;
        }
    }
    void SpawnRandomAnimal()
    {
        if (playercontrollerScript.gameOver == false) //checks the value of a boolean variable called gameOver within the playercontrollerScript.
        {
            // Randomly generate animals index and spawn position

            Vector3 spawnPos = new Vector3(Random.Range(leftspawnRangeX, rightspawnRangeX), 0, Random.Range(0, -spawnPosZ));
            int animalIndex = Random.Range(0, animalPrefabs.Length);

            Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);

        }
    }
    private void SpawnPowerUp()
    {
        if (playercontrollerScript.hasPowerUp == false)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(0, spawnAreaCenter.x + spawnAreaSize.x / 2),0,Random.Range(0, spawnPosZ)
            
                
            );

            Instantiate(powerUpPrefab, randomPosition, Quaternion.identity); //The identity rotation, represented by Quaternion.identity, signifies no rotation at all. It is equivalent to a rotation of zero degrees around all three axes 0
        }
    }
}

