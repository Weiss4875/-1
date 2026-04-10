using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    public GameObject[] crystals;
    public Transform[] spawnPoints;

    public float spawnDelay = 5f;
    public float spawnInterval = 5f;
    public int crystalsPerSpawn = 2;

    private List<int> availableIndices;

    void Start()
    {
        availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availableIndices.Add(i);
        }

        StartCoroutine(SpawnCrystals());
    }

    IEnumerator SpawnCrystals()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (availableIndices.Count > 0)
        {
            
            int count = Mathf.Min(crystalsPerSpawn, availableIndices.Count);

            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, availableIndices.Count);
                Transform spawnPoint = spawnPoints[availableIndices[randomIndex]];

                GameObject crystalPrefab = crystals[Random.Range(0, crystals.Length)];
                Instantiate(crystalPrefab, spawnPoint.position, Quaternion.identity);

                availableIndices.RemoveAt(randomIndex);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}