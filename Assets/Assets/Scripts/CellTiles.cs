using UnityEngine;

public class CellTiles : MonoBehaviour
{
    public Vector2Int Coordinates { get; set; }
    public BlockControler Block { get; set; }
    public int NumberOfBlockStates { get; set; }
    public int Number {  get; set; }
    public bool Empty => Block == null;
    public bool Occupied => Block != null;
}
