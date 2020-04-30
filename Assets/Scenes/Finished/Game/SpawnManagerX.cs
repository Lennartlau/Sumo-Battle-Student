using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9f;
    public GameObject powerupPrefab;
    public int waveNumber = 1;  
    private int enemyCount;

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    void Update()
    {
        enemyCount = Mathf.Max(FindObjectsOfType<Enemy>().Length, FindObjectsOfType<EnemyX>().Length);
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    /// challenge: spawn specified numberOfEnemies using Instantiate(...)
    void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomPosX = Random.Range(-spawnRange, spawnRange);
        float randomPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(randomPosX, 0, randomPosZ);
        return randomPos;
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
}
