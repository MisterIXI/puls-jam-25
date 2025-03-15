using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LakeGeneration : MonoBehaviour
{
    public static LakeGeneration Instance;
    [SerializeField] private Grid grid;
    [SerializeField] private int gridRadius;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private TileBase groundTile;
    private readonly List<IceTile> _iceTiles = new List<IceTile>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int x = -gridRadius; x <= gridRadius; x++)
        {
            for (int y = -gridRadius; y <= gridRadius; y++)
            {
                if (x * x + y * y > gridRadius * gridRadius) continue;
                groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                IceTile iceTile = new IceTile(new Vector2Int(x, y));
                _iceTiles.Add(iceTile);
            }
        }
        IceLakeStateManager.Instance.SetIceTileList(_iceTiles);
    }

    public void RemoveIceTile(IceTile iceTile)
    {
        groundTilemap.SetTile((Vector3Int) iceTile.GetTilePosition(), null);
    }
    
}
