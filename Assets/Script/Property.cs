using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Property : MonoBehaviour
{
    public static Property Instance;
    public List<Sprite> Dissprites;
    public List<Image> Dices;
    public List<int> Value = new List<int>();
   // public GameObject cardPrefab;
    //public Transform[] cardParent;
    public Button[] BitButtons;
    public GameObject Border, RedoButton;
    //public GameObject[] shadows;
    public Text BankText, BetTaiText, BetXiuText;
    public static int Bank, BetTai, BetXiu, Bet, Sums,SumPrideUp;
    public Animator Cover;
    public Button[] Buttons;
    public GameObject[] WinShowEffect;
    public GameObject[] UpscaleTo;
    public GameObject[] ButtonBets;

    private const string BankKey = "Bank";

    private void Awake()
    {
        Instance = GetComponent<Property>();
        LoadBank();
    }
    private void Start()
    {
        ScreenOrientation landscapeRight = ScreenOrientation.LandscapeRight;
        Screen.orientation = landscapeRight;
    }
    private void Update()
    {
        BetXiuText.text = BetXiu.ToString();
        BetTaiText.text = BetTai.ToString();

        if (Bank > 999)
        {
            BankText.text = Bank.ToString("#,###");
        }
        else if (Bank >= 1000000)
        {
            BankText.text = Bank.ToString("#,###,###");
        }
        else if (Bank >= 1000000000)
        {
            BankText.text = Bank.ToString("#,###,###,###");
        }
        else
        {
            BankText.text =  Bank.ToString();
        }
    }

    public void RedoPride()
    {
        Bank += 1000000;
        SaveBank();
        RedoButton.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        SaveBank();
    }

    private void SaveBank()
    {
        PlayerPrefs.SetInt(BankKey, Bank);
        PlayerPrefs.Save();
    }

    private void LoadBank()
    {
        if (PlayerPrefs.HasKey(BankKey))
        {
            Bank = PlayerPrefs.GetInt(BankKey);
        }
        else
        {
            Bank = 1000000; // Initial value if no saved data
        }
    }
    public void HomeGame()
    {
        SceneManager.LoadScene("Home");
    }
}
