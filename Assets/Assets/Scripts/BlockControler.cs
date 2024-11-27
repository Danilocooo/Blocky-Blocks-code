using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BlockControler : MonoBehaviour
{
    public TileState state { get; private set; }
    public CellTiles cell { get; private set; }
    public Color color { get; private set; }
    private Color blockColor;
    public static int RandomNum;
    private Image background;

    public Color GetBlockColor()
    {
        return blockColor;
    }
    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void SetState(TileState state)
    {

        this.state = state;
        background.color = state.backgroundColor;

    }

    public void Spawn(CellTiles cell)
    {
        if (this.cell != null)
        {
            this.cell.Block = null;
        }
        this.cell = cell;
        this.cell.Block = this;
        transform.position = cell.transform.position;

    }
    
    public void Eliminate(CellTiles cell)
    {
        if (this.cell != null)
        {
            this.cell.Block = null;
        }
        this.cell = null;

    }
    public void MoveTo(CellTiles Cell)
    {
        if (this.cell != null)
        {
            this.cell.Block = null;
        }
        this.cell = Cell;
        this.cell.Block = this;
        StartCoroutine(Animate(Cell.transform.position));
    }

    private IEnumerator Animate(Vector3 EndPosition)
    {
        float elapsed = 0.0f;
        float duration = 0.1f;
        Vector3 StartPosition = transform.position;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(StartPosition, EndPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = EndPosition;

    }
}
