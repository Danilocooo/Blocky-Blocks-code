using UnityEngine;

public class HoritzontalCube : MonoBehaviour
{
    public float Velocity = 50f;
    int x = -750;
    int RealX = 545;
    int y = 1000;
    int RealY = 1170;
    int z = 0;
    void Start()
    {
        gameObject.transform.position = new Vector3Int(x + RealX, y + RealY, z);
    }
    private void Update()
    {
        if (gameObject.transform.position.y > -1500 + RealY)
        {
           gameObject.transform.position = transform.position + new Vector3(1000 * Time.deltaTime * Velocity, 0 , 0);
            if (gameObject.transform.position.x >= 720 + RealX)
            {
                gameObject.transform.position = new Vector3(-720 + RealX, gameObject.transform.position.y - 500f, z);
            }
        }
        if (gameObject.transform.position.y == -1500 + RealY)
        {
            y = 1000;
            gameObject.transform.position = new Vector3Int(x + RealX, y + RealY, z);
        }
    }
}
