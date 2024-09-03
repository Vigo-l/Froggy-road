using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject logPrefab;
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject staticObstaclePrefab;
    public GameObject goalPrefab;  // Add a reference to the goal prefab

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
                Vector3 coinPosition = new Vector3(Random.Range(-mapWidth / 2f, mapWidth / 2f), i, 0);
                Instantiate(coinPrefab, coinPosition, Quaternion.identity);
            }
        }

        // Spawn the goal at the last row
        SpawnGoal(mapHeight);
    }

    void SpawnCar()
    {
        // Find a random road y-position to spawn the car
        int roadYPos = Random.Range(0, mapHeight);

        // Randomize car speed and direction
        int carDirection = Random.Range(0, 2); // 0: Left to Right, 1: Right to Left
        float speed = Random.Range(2f, 5f);

            GameObject car = Instantiate(
                carPrefab,
                new Vector3(carDirection == 0 ? -mapWidth / 2f - 1 : mapWidth / 2f + 1, yPos, 0),
                Quaternion.identity
            );
            car.GetComponent<CarController>().speed = carDirection == 0 ? speed : -speed;
        }
    }

    void SpawnLog()
    {
        int logDirection = Random.Range(0, 2); // 0: Left to Right, 1: Right to Left
        int logCount = Random.Range(1, 3); // Random number of logs (1-2)
        for (int i = 0; i < logCount; i++)
        {
            float logSpeed = Random.Range(1f, 3f);
            GameObject log = Instantiate(
                logPrefab,
                new Vector3(logDirection == 0 ? -mapWidth / 2f - 1 : mapWidth / 2f + 1, yPos, 0),
                Quaternion.identity
            );
            log.GetComponent<LogController>().speed = logDirection == 0 ? logSpeed : -logSpeed;
        }
    }

    void SpawnSafeSpace(int yPos)
    {
        // Randomly place static obstacles
        int obstacleCount = Random.Range(0, 3); // 0-2 obstacles
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 obstaclePos = new Vector3(Random.Range(-mapWidth / 2f, mapWidth / 2f), yPos, 0);
            Instantiate(staticObstaclePrefab, obstaclePos, Quaternion.identity);
        }
    }

    void SpawnGoal(int yPos)
    {
        Vector3 goalPosition = new Vector3(0, yPos, 0);
        Instantiate(goalPrefab, goalPosition, Quaternion.identity);
    }
}
