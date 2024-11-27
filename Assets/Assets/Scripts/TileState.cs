using UnityEngine;

[CreateAssetMenu(menuName = "Tile State")]
public class TileState : ScriptableObject
{
    /*
     * Variable per a assignar color al BlockControler
     * 
     * Per defecte asigna color blanc rgba(1,1,1,1)
     */
    public Color backgroundColor = Color.white;
}
