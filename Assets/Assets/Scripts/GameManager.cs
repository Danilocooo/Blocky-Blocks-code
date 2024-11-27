using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestText;
    public CanvasGroup MenuImage;
    public Button menu;
    public Button GoToMain;
    public Button NewGameButton;
    public Button Help;
    public Button Audio;
    public int score;
    public int choose = 0;
    public CanvasGroup HelpMenu;
    public bool CanPlayAudio = true;
    public bool CanPlaySFX = true;
    public AudioSource click;
    public AudioSource GameOverSound;
    public AudioSource Swipe;
    public AudioSource GameMusic1;
    public AudioSource GameMusic2;
    public AudioSource Spawn;
    public AudioSource Delete;
    public bool Score20Activator = false;
    public TextMeshProUGUI AudioOnOffText;
    public bool alternate = true;
    //public Play playScript;
    public int addActivator = 0;
    public AdsManager googleAds;
    public Graphic HelpGraphic;
    public Graphic GoToMainGraphic;
    public Graphic NewGameGraphic;
    public Graphic AudioGraphic;
    private void Awake()
    {
        CanPlayAudio = StaticData.AudioActivate;
        CanPlaySFX = StaticData.SFXActivate;
        
        //if (!googleAds.isActiveAndEnabled) { Debug.LogWarning("GoogleAds no activo"); } else { Debug.Log("GoogleAds is enabled at GameManager.Awake");  }
        
        //googleAds = Object.FindAnyObjectByType<AdsManager>();

        /*if (googleAds == null)
        {
            GameObject adsManagerObject = new GameObject("AdsManager");
            googleAds = adsManagerObject.AddComponent<AdsManager>();
        }

        googleAds.LoadInterstitialAd();*/
        
        HelpMenu.alpha = 0f;
        MenuImage.alpha = 0f;
        
        if (!CanPlayAudio)
        {
            AudioOnOffText.text = "OFF";
            GameMusic1.Stop();
            GameMusic2.Stop();
            alternate = false;
        }

        NewGame();
    }

    public void NewGameButtonVoid()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        NewGame();
    }
    public void NewGame()
    {
        Score20Activator = false;

        
        /*googleAds.DestroyAd();
        googleAds.LoadAd();
        if (addActivator == 2)
        {
            GameMusic1.Stop();
            GameMusic2.Stop();
            googleAds.ShowInterstitialAd();

            googleAds.RegisterReloadHandler(googleAds._interstitialAd);
            addActivator = 0;
        }
        else
        {
            if (CanPlayAudio)
            {
                GameMusic1.Play();
            }
            addActivator++;
        }*/
        

        if (CanPlayAudio)
        {
            GameMusic1.Play();
        }
        else
        {
            GameMusic1.Stop();
            GameMusic2.Stop();
        }
        MenuImage.interactable = false;
        GoToMain.interactable = false;
        Help.interactable = false;
        Audio.interactable = false;
        NewGameButton.interactable = false;

        HelpGraphic.raycastTarget = false;
        GoToMainGraphic.raycastTarget = false;
        AudioGraphic.raycastTarget = false;
        NewGameGraphic.raycastTarget = false;

        menu.enabled = true;
        SetScore(0);
        BestText.text = LoadHighScore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        MenuImage.alpha = 0f;


        board.ClearBoard();
        board.CreateBlock();
        board.CreateBlock();
        board.enabled = true;
    }
    public void GameOver()
    {
        
        if (CanPlayAudio == true)
        {
            GameMusic1.Stop();
            GameMusic2.Stop();
            GameOverSound.Play();
        }
        MenuImage.interactable = false;
        GoToMain.interactable = false;
        Help.interactable = false;
        Audio.interactable = false;
        NewGameButton.interactable = false;

        HelpGraphic.raycastTarget = false;
        GoToMainGraphic.raycastTarget = false;
        AudioGraphic.raycastTarget = false;
        NewGameGraphic.raycastTarget = false;

        gameOver.interactable = true;
        menu.enabled = true;

        board.enabled = false;
        StartCoroutine(GameOverAnimation(gameOver, 1f, 1f));
        
    }
    private IEnumerator GameOverAnimation(CanvasGroup CanvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = CanvasGroup.alpha;
        while (elapsed < duration)
        {
            CanvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        CanvasGroup.alpha = to;
    }
    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }
    private void SetScore(int score)
    {
        this.score = score;
        ScoreText.text = score.ToString();
        if (CanPlayAudio == true)
        {
            if (score >= 20000)
            {
                GameMusic1.Stop();
                GameMusic2.Play();
            }
        }
        else
        {
            GameMusic1.Stop();
            GameMusic2.Stop();
        }
        if (score >= 20000)
        {
            Score20Activator = true;
        }
        SaveHighScore();
    }
    private void SaveHighScore()
    {
        int highscore = LoadHighScore();

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }
    public void LoadMainMenu()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        //googleAds.DestroyAd();
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        MenuImage.interactable = true;
        GoToMain.interactable = true;
        Help.interactable = true;
        Audio.interactable = true;
        NewGameButton.interactable = true;

        HelpGraphic.raycastTarget = true;
        GoToMainGraphic.raycastTarget = true;
        AudioGraphic.raycastTarget = true;
        NewGameGraphic.raycastTarget = true;
        if (MenuImage.alpha == 0)
        {
            choose = 0;
        }
        else
        {
            choose = 1;
        }
        if (choose == 0)
        {
            StartCoroutine(MenuAnimation(MenuImage, 1f, 0.2f));
            gameOver.interactable = false;
            board.enabled = false;
            choose++;
        }
        else
        {
            StartCoroutine(MenuAnimation(MenuImage, 0f, 0f));
            gameOver.interactable = true;
            board.enabled = true;

            MenuImage.interactable = true;
            GoToMain.interactable = true;
            Help.interactable = true;
            Audio.interactable = true;
            NewGameButton.interactable = true;

            HelpGraphic.raycastTarget = true;
            GoToMainGraphic.raycastTarget = true;
            AudioGraphic.raycastTarget = true;
            NewGameGraphic.raycastTarget = true;
            choose--;
        }

    }
    private IEnumerator MenuAnimation(CanvasGroup Canvas, float end, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.3f;
        float start = Canvas.alpha;
        while (elapsed < duration)
        {
            Canvas.alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Canvas.alpha = end;
    }
    public void GoToLegalNotice()
    {
        //googleAds.DestroyAd();
        SceneManager.LoadScene(3);
    }
    public void GoToHelp()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        StartCoroutine(HelpAnimation(HelpMenu, 1f, 0.2f));
        HelpMenu.interactable = true;

        GoToMain.interactable = false;
        Help.interactable = false;
        Audio.interactable = false;
        NewGameButton.interactable = false;

        HelpGraphic.raycastTarget = false;
        GoToMainGraphic.raycastTarget = false;
        AudioGraphic.raycastTarget = false;
        NewGameGraphic.raycastTarget = false;
    }
    private IEnumerator HelpAnimation(CanvasGroup Canvas, float end, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.3f;
        float start = Canvas.alpha;
        while (elapsed < duration)
        {
            Canvas.alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Canvas.alpha = end;
    }
    public void ReverseGoToHelp()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        StartCoroutine(HelpAnimation(HelpMenu, 0f, 0f));
        HelpMenu.interactable = false;

        GoToMain.interactable = true;
        Help.interactable = true;
        Audio.interactable = true;
        NewGameButton.interactable = true;

        HelpGraphic.raycastTarget = true;
        GoToMainGraphic.raycastTarget = true;
        AudioGraphic.raycastTarget = true;
        NewGameGraphic.raycastTarget = true;
    }
    public void GoToRight()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        StartCoroutine(MoveToRightOrLeft(Vector2Int.right));
    }
    public void GoToLeft()
    {
        if (CanPlaySFX)
        {
            click.Play();
        }
        StartCoroutine(MoveToRightOrLeft(Vector2Int.left));
    }
    private IEnumerator MoveToRightOrLeft(Vector2Int direction)
    {
        Vector3 position;
        Vector3 StartPosition;
        int MoveTo;
        int vel = 1;
        if (direction == Vector2Int.right)
        {
            MoveTo = -1500;
            StartPosition = HelpMenu.transform.position;
            position = StartPosition;
            while (position.x <= StartPosition.x + MoveTo)
            {
                position = HelpMenu.transform.position;
                HelpMenu.transform.position = HelpMenu.transform.position + new Vector3(-1 * Time.deltaTime * vel, StartPosition.y, 0);
            }
            int loadX = (int) StartPosition.x;
            int loadY = (int)StartPosition.y;
            HelpMenu.transform.position = new Vector3Int(loadX + MoveTo, loadY, 0);
        }
        else
        {
            MoveTo = 1500;
            StartPosition = HelpMenu.transform.position;
            position = StartPosition;
            while (position.x >= StartPosition.x + MoveTo)
            {
                position = HelpMenu.transform.position;
                HelpMenu.transform.position = HelpMenu.transform.position + new Vector3(1 * Time.deltaTime * vel, StartPosition.y, 0);
            }
            int loadX = (int)StartPosition.x;
            int loadY = (int)StartPosition.y;
            HelpMenu.transform.position = new Vector3Int(loadX + MoveTo, loadY, 0);
        }
        yield return null;

    }
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    public void AudioOnOff()
    {
        if (alternate)
        {
            GameMusic1.Stop();
            GameMusic2.Stop();
            CanPlayAudio = false;
            AudioOnOffText.text = ("OFF");
            alternate = false;
        }
        else
        {
            if (score >= 20000)
            {
                GameMusic2.Play();
            }
            else
            {
                GameMusic1.Play();
            }
            CanPlayAudio = true;
            AudioOnOffText.text = ("ON");
            alternate = true;
        }
    }
}
