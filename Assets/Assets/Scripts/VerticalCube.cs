using UnityEngine;

public class VerticalCube : MonoBehaviour
{
    public float Velocity = 50f;
    int x = -400;
    int RealX = 545;
    int y = 1350;
    int RealY = 1170;
    void Start()
    {
        gameObject.transform.position = new Vector3Int(x + RealX, y + RealY, 0);
    }
    void Update()
    {
        if (gameObject.transform.position.x >= -400 + RealX)
        {
            gameObject.transform.position = transform.position + new Vector3(0, -10 * Time.deltaTime * Velocity, 0);
            if (gameObject.transform.position.y <= -1400 + RealY)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 100f, 1350 + RealY, 0);
            }
        }
        if (gameObject.transform.position.x == 400 + RealX)
        {
            x = -400;
            gameObject.transform.position = new Vector3Int(x + RealX, y + RealY, 0);
        }
    }
}
