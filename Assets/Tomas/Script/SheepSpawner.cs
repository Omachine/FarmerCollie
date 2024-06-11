using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int npcLimit = 10;
    public float spawnRadius = 5f;
    private int npcCount = 0;
    private float spawnTimer = 0f;
    public float spawnRate = 1f;

    void Update()
    {
        if (npcCount < npcLimit)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;
                SpawnNpc();
            }
        }
    }

    void SpawnNpc()
    {
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPos.y = 0f;
        Instantiate(npcPrefab, spawnPos, Quaternion.identity);
        npcCount++;
    }

    public void RemoveNpc()
    {
        npcCount--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
