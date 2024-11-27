using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlackCanvas : MonoBehaviour
{
    public CanvasGroup canvas;
    void Start()
    {
        canvas.alpha = 1f;
        Animate();
    }
    public void Animate()
    {
        StartCoroutine(AnimationCorrutine(canvas, 0f, 3f));

    }
    private IEnumerator AnimationCorrutine(CanvasGroup canvas, float end, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 1f;
        float Start = canvas.alpha;
        while (elapsed < duration)
        {
            canvas.alpha = Mathf.Lerp(Start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = end;
        SceneManager.LoadScene(1);
    }
}
