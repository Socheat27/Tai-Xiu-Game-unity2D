
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource[] Musics;
    public Sprite[] sprites = new Sprite[2];
    public Image Images;
    public bool IsOn = true;
    private const string SoundKey = "SoundValue";
    public static Music music;

    public void Awake()
    {
        music = this;
    }

    void Start()
    {
        LoadSoundValue(); // Load the sound value when the game starts
    }

    public void SFXSound()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            Images.sprite = sprites[0];
            Images.sprite = sprites[0];
            SetMuteState(false);
        }
        else
        {
            Images.sprite = sprites[1];
            Images.sprite = sprites[1];
            SetMuteState(true);
        }

        SaveSoundValue(); // Save the sound value when the button is clicked
    }

    void SetMuteState(bool isMuted)
    {
        foreach (var music in Musics)
        {
            music.mute = isMuted;
        }
    }

    void SaveSoundValue()
    {
        // Save the sound value
        PlayerPrefs.SetInt(SoundKey, IsOn ? 1 : 0);
    }

    void LoadSoundValue()
    {
        // Load the sound value and set the state accordingly
        if (PlayerPrefs.HasKey(SoundKey))
        {
            int savedValue = PlayerPrefs.GetInt(SoundKey);
            IsOn = savedValue == 1;
            SetMuteState(!IsOn);
            Images.sprite = IsOn ? sprites[0] : sprites[1];
            Images.sprite = IsOn ? sprites[0] : sprites[1];
        }
    }
}
