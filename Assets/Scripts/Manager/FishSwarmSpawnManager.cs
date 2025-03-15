using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSwarmSpawnManager : MonoBehaviour
{
    public static FishSwarmSpawnManager Instance;
    [SerializeField] private int amountOfFishSwarms = 3;
    [SerializeField] private int spawnRadius = 18;
    [SerializeField] private GameObject fishSwarmPrefab;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amountOfFishSwarms; i++)
        {
            SpawnFishSwarm();
        }
    }

    public void SpawnFishSwarm()
    {
        StartCoroutine(SpawnFishSwarmRoutine());
    }
    

    IEnumerator SpawnFishSwarmRoutine()
    {
        Vector2 spawnPoint = (Random.insideUnitSphere * spawnRadius);
        RaycastHit2D hit = Physics2D.Raycast(spawnPoint, Vector2.down, 1);
        
        while (hit.collider?.gameObject.tag == "IceHole")
        {
            yield return new WaitForSeconds(2);
            spawnPoint = (Random.insideUnitSphere * spawnRadius);
            hit = Physics2D.Raycast(spawnPoint, Vector2.down, 1);
        }
        Instantiate(fishSwarmPrefab, spawnPoint, Quaternion.identity);
    }
}
