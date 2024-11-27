using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public TextMeshProUGUI AudioOnOffText;
    public TextMeshProUGUI SFXOnOffText;
    public AudioSource RelaxingMusic;
    public bool AlternateAudio = true;
    public bool AlternateSFX = true;
    private Scene scene;
    public AudioSource Click;
    public GameObject PlayObject;
    public void Awake()
    {
        RelaxingMusic.Play();
    }
    public void LoadGame()
    {
        //StaticData.Play = true;
        RelaxingMusic.Stop();
        SceneManager.LoadScene(2);

    }
    public void AudioOnOff()
    {
        if (AlternateSFX)
        {
            Click.Play();
        }
        if (AlternateAudio == true)
        {
            RelaxingMusic.Stop();
            AudioOnOffText.text = ("OFF");
            AlternateAudio = false;

        }
        else
        {
            RelaxingMusic.Play();
            AudioOnOffText.text = ("ON");
            AlternateAudio = true;
        }
        StaticData.AudioActivate = AlternateAudio;
    }
    public void SFXOnOff()
    {
        if (AlternateSFX == true)
        {
            Click.Play();
            SFXOnOffText.text = ("OFF");
            AlternateSFX = false;
        }
        else
        {
            SFXOnOffText.text = ("ON");
            AlternateSFX = true;
        }
        StaticData.SFXActivate = AlternateSFX;
    }
}
