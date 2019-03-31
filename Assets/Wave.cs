using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float timePerWave;

    private float waveTimer;
    private GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (uint i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer += Time.deltaTime;
        if (waveTimer >= timePerWave)
        {
            SpawnWave();
            waveTimer = 0;
        }
    }

    private void SpawnWave()
    {
        int numEnemies = Random.Range(3, 7);
        List<int> indexList = new List<int>();
        for (int i = 0; i < numEnemies; i++)
        {
            // Random index without repeating
            int indexEnemy;
            do
            {
                indexEnemy = Random.Range(0, spawnPoints.Length);
            }
            while (indexList.Contains(indexEnemy));
            indexList.Add(indexEnemy);

            // Activate points
            for (int j = 0; j < indexList.Count; j++)
                spawnPoints[indexList[j]].SetActive(true);
        }
    }
}
