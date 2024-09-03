using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject logPrefab;
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject staticObstaclePrefab;

    public int mapWidth = 10;
    public int mapHeight = 20;

    public float carSpawnInterval = 0.05f; // Time between car spawns
    public float logSpawnInterval = 0.1f; // Time between log spawns

    private float carSpawnTimer = 0f;
    private float logSpawnTimer = 0f;

    void Start()
    {
        GenerateLevel();
    }

    void Update()
    {
        carSpawnTimer += Time.deltaTime;
        logSpawnTimer += Time.deltaTime;

        if (carSpawnTimer >= carSpawnInterval)
        {
            SpawnCar();
            carSpawnTimer = 0f;
        }

        if (logSpawnTimer >= logSpawnInterval)
        {
            SpawnLog();
            logSpawnTimer = 0f;
        }
    }

    void GenerateLevel()
    {
        for (int i = 0; i < mapHeight; i++)
        {
            int laneType = Random.Range(0, 3); // 0: Road, 1: Water, 2: Safe Space

            if (laneType == 0) // Road
            {
                Instantiate(roadPrefab, new Vector3(0, i, 0), Quaternion.identity);
            }
            else if (laneType == 1) // Water
            {
                Instantiate(roadPrefab, new Vector3(0, i, 0), Quaternion.identity); // Use road prefab or create a water prefab
            }
            else // Safe Space
            {
                Instantiate(roadPrefab, new Vector3(0, i, 0), Quaternion.identity); // Safe space can be the same as road or different
                SpawnSafeSpace(i);
            }

            // Spawn coins
            if (Random.Range(0, 4) == 0) // 25% chance to spawn a coin
            {
                Vector3 coinPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), i, 0);
                Instantiate(coinPrefab, coinPosition, Quaternion.identity);
            }
        }
    }

    void SpawnCar()
    {
        // Find a random road y-position to spawn the car
        int roadYPos = Random.Range(0, mapHeight);

        // Randomize car speed and direction
        int carDirection = Random.Range(0, 2); // 0: Left to Right, 1: Right to Left
        float speed = Random.Range(2f, 5f);

        // Spawn the car
        GameObject car = Instantiate(carPrefab, new Vector3(carDirection == 0 ? -10 : 10, roadYPos, 0), Quaternion.identity);
        car.GetComponent<CarController>().speed = carDirection == 0 ? speed : -speed;
    }

    void SpawnLog()
    {
        // Find a random water y-position to spawn the log
        int waterYPos = Random.Range(0, mapHeight);

        // Randomize log speed
        float logSpeed = Random.Range(1f, 3f);

        // Spawn the log
        GameObject log = Instantiate(logPrefab, new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), waterYPos, 0), Quaternion.identity);
        log.GetComponent<LogController>().speed = logSpeed;
    }

    void SpawnSafeSpace(int yPos)
    {
        // Randomly place static obstacles
        int obstacleCount = Random.Range(0, 3);
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 obstaclePos = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), yPos, 0);
            Instantiate(staticObstaclePrefab, obstaclePos, Quaternion.identity);
        }
    }
}
