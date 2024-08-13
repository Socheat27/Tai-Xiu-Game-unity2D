using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Property;
using static Music;

public class CalculatAndBet : MonoBehaviour
{
    public void Buttons(int i)
    {
        if (Bank > 0)
        {
            switch (i)
            {
                case 1:
                    if (Bank >= 1000)
                        Bet = 1000;
                    break;
                case 2:
                    if (Bank >= 10000)
                        Bet = 10000;
                    break;
                case 3:
                    if (Bank >= 500000)
                        Bet = 500000;
                    break;
                case 4:
                    if (Bank >= 1000000)
                        Bet = 1000000;
                    break;
            }
        }
    }

    public void BetTaiFun()
    {
        music.Musics[3].Play();
        if (Bank >= Bet)
        {
            BetTai += Bet;
            Bank -= Bet;
        }
    }

    public void BetXiuFun()
    {
        music.Musics[3].Play();
        if (Bank >= Bet)
        {
            BetXiu += Bet;
            Bank -= Bet;
        }
    }

    public void Calculation()
    {
        if (Sums < 11)
        {
            StartCoroutine(CycleWinShowEffect(0));
            print("Xiu :" + Sums);
            SumPrideUp = BetXiu * 2;
        }
        else
        {
            StartCoroutine(CycleWinShowEffect(1));
            print("Tai :" + Sums);
            SumPrideUp = BetTai * 2;
        }
    }

    private IEnumerator CycleWinShowEffect(int effectIndex)
    {
        // The specific win show effect to cycle
        GameObject winEffect = Instance.WinShowEffect[effectIndex];
        GameObject upscale = Instance.UpscaleTo[effectIndex];
        // Number of cycles through the effect
        int cycleCount = 3;
        // Duration for each full cycle of rotation
        float cycleDuration = 1.0f; // Adjust this value for faster or slower rotation
        // Total duration of the effect
        float totalDuration = cycleCount * cycleDuration;
        // Target scale
        Vector3 originalScale = upscale.transform.localScale;
        Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);

        winEffect.SetActive(true);
        upscale.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < totalDuration)
        {
            // Rotate the win effect around the Z-axis
            winEffect.transform.Rotate(0f, 0f, 360f / cycleDuration * Time.deltaTime);

            // Smoothly upscale and downscale
            float t = Mathf.PingPong(Time.time, cycleDuration / 2) / (cycleDuration / 2);
            upscale.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        winEffect.SetActive(false);

        upscale.transform.localScale = originalScale;
        BetTai = 0;
        BetXiu = 0;

        if (Bank <= 0)
        {
            Instance.RedoButton.SetActive(true);
        }
        else
        {
            Instance.RedoButton.SetActive(false);
        }
    }
    public IEnumerator SumPride()
    {
        if (SumPrideUp > 0)
        {
            for (int i = 0; i < SumPrideUp; i += 1000)
            {
                yield return new WaitForSeconds(0.01f);
                Bank += 1000;
            }
            SumPrideUp = 0;
        }
        
    }
}
