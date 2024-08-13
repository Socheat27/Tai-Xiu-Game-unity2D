using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Property;
using static Music;

public class RandomGame : MonoBehaviour
{
    public Text Times;
    public float countdownTime = 7f; // Duration of the countdown
    private bool isCountingDown = false;
    //public GameObject[] shadows;

    public void Randomsprit()
    {
        Instance.Value.Clear();

        for (int i = 0; i < 3; i++)
        {
            int ran = Random.Range(1, 7);
            Instance.Value.Add(ran);
        }
        for (int i = 0; i < 3; i++)
        {
            Sums += Instance.Value[i];
        }
    }

    private void Start()
    {
        Instance.Cover.Play("CoverIdle");
        foreach (var Effect in Instance.WinShowEffect)
        {
            Effect.SetActive(false);
        }
        Instance.Buttons[0].enabled = true;
        for (int i = 1; i < Instance.Buttons.Length; i++)
        {
            Instance.Buttons[i].enabled = false;
            var shadowImage = Instance.Buttons[i].GetComponent<Image>();
            shadowImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }
   
    public void PlayGame()
    {
        foreach (var Effect in Instance.WinShowEffect)
        {
            Effect.SetActive(false);
        }
        Instance.Buttons[0].enabled = false;
        var shadowImag = Instance.Buttons[0].GetComponent<Image>();
        shadowImag.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        for (int i = 1; i < Instance.Buttons.Length; i++)
        {
            Instance.Buttons[i].enabled = true;
            var shadowImage = Instance.Buttons[i].GetComponent<Image>();
            shadowImage.color = new Color(1f, 1f, 1f, 1f);
           
        }
        Instance.Cover.Play("CoverClose");
        BetTai = 0;
        BetXiu = 0;
        Sums = 0;

        if (!isCountingDown)
        {
            StartCoroutine(CountdownAndPlay());
        }
    }

    private IEnumerator CountdownAndPlay()
    {
        isCountingDown = true;
        Instance.Border.SetActive(true);
        music.Musics[1].Play();
        float remainingTime = countdownTime;
        while (remainingTime > 0)
        {
            Times.text = Mathf.Ceil(remainingTime).ToString();
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        Times.text = "Go!";
        Randomsprit();
        SwitchSprite();
        Instance.Cover.Play("CoverOpen");
        
        FindAnyObjectByType<CalculatAndBet>().Calculation();
        music.Musics[2].Play();
        Instance.Buttons[0].enabled = true;
        var shadowImag = Instance.Buttons[0].GetComponent<Image>();
        shadowImag.color = new Color(1f, 1f, 1f, 1f);
        for (int i = 1; i < Instance.Buttons.Length; i++)
        {
            Instance.Buttons[i].enabled = false;
            var shadowImage = Instance.Buttons[i].GetComponent<Image>();
            shadowImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); 
            

        }
        StartCoroutine(FindAnyObjectByType<CalculatAndBet>().SumPride());
        yield return new WaitForSeconds(1f);
        Times.text = "";

        isCountingDown = false;
        Instance.Border.SetActive(false);
    }

    public void SwitchSprite()
    {
        
        for (int i = 0; i < 3; i++)
        {
            int diceValue = Instance.Value[i];
            Instance.Dices[i].sprite = Instance.Dissprites[diceValue];
        }
    }
}
