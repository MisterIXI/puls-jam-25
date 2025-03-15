using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceLakeStateManager : MonoBehaviour
{
    public static IceLakeStateManager Instance;
    private List<IceTile> _iceTiles = new List<IceTile>();
    
    [Range(1,20)]
    [SerializeField] private int ticker;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetIceTileList(List<IceTile> iceTiles)
    {
        _iceTiles = iceTiles;
        Debug.Log("IceLakeStateManager.SetIceTileList");
        Debug.Log(_iceTiles.Count);
        StartCoroutine(StartIceTicker());
    }

    IEnumerator StartIceTicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(ticker);
            ChangeStateOfTiles();
            
        }
        
    }
    private void ChangeStateOfTiles()
    {
        LakeGeneration.Instance.RemoveIceTile(_iceTiles[Random.Range(0, _iceTiles.Count)]);
    }

}