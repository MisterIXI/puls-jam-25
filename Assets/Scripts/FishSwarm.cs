using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FishSwarm : MonoBehaviour
{
    [SerializeField] private int aliveMin;
    [SerializeField] private int aliveMax;

    IEnumerator Start()
    {
        int timeAlive = Random.Range(aliveMin, aliveMax);
        yield return new WaitForSeconds(timeAlive);
        FishSwarmSpawnManager.Instance.SpawnFishSwarm();
        Destroy(gameObject);
    }
    
}
