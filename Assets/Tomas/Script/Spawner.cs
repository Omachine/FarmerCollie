using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make so that random npc spawn around the spawner and not on top of it and have a limit of how many npc can be in the scene at once
public class Spawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int npcLimit = 10;
    public float spawnRadius = 5f;
    private int npcCount = 0;
    private float spawnTimer = 0f;

    // Set initial spawn rate to 60 seconds
    //not show in unity but still public
    [HideInInspector]
    public float spawnRate = 60f;

    void Update()
    {
        if (npcCount < npcLimit)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;
                SpawnNpc();

                // After the first spawn, set the spawn rate to 120 seconds
                spawnRate = 120f;
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
