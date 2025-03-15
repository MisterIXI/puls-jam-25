using UnityEngine;
using UnityEngine.Serialization;
public class IceTile
{
    [SerializeField] private Vector2Int TilePosition;
    [SerializeField] private IceTileEnum IceTileState;
    
    public IceTile(Vector2Int vector2Int)
    {
        IceTileState = IceTileEnum.Frozen;
        TilePosition = vector2Int;
    }
    
    public Vector2Int GetTilePosition(){ return TilePosition; }
    public IceTileEnum GetIceTileState(){ return IceTileState; }
}
