using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToGame : MonoBehaviour
{
    public void GoBackToGameAction()
    {
        SceneManager.LoadScene(2);
    }
}
