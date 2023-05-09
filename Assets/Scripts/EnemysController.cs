using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemysController : MonoBehaviour
{
    [SerializeField] private PlayerScript Player;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int maxEnemies = 5;
    [SerializeField] private int round = 0;
    [SerializeField] private int currentEnemies = 0;

    private void Start()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPrefabs[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (currentEnemies <= 0)
        {
            for(int i = 0; i < maxEnemies; i++)
            {
                // Choose a random enemy prefab from the initialEnemies array
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyPrefab = enemyPrefabs[randomIndex];

                // Get a random position within the navMesh
                Vector2 randomPosition = Random.insideUnitCircle * 50f;
                NavMeshHit hit;
                NavMesh.SamplePosition(new Vector3(randomPosition.x, randomPosition.y, 0), out hit, 50f, NavMesh.AllAreas);

                // Instantiate the enemy at the random position
                GameObject newEnemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);

                newEnemy.SetActive(true);

                // Increase the count of current enemies
                currentEnemies++;
            }
            maxEnemies += 5;
            round++;
        }
    }

    public int GetCurrentEnemies()
    {
        return currentEnemies;
    }

    public int GetRound()
    {
        return round;
    }

    public void DecreaseEnemyCount()
    {
        Player.UpdateScore(5);
        currentEnemies--;
    }
}
