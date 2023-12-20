using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SingleplayerPage : MonoBehaviour
{
    public UnityEvent onEasyButtonClick;
    public UnityEvent onHardButtonClick;
    public UnityEvent onStartButtonClick;
    public UnityEvent onBackButtonClick;
    public UnityEvent onRandomizeSeedClick;

    [SerializeField]
    private TMP_InputField seedText;

    private void Start()
    {
        seedText.text = getRandomSeed().ToString();
    }

    public void easyButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onEasyButtonClick.Invoke();
    }
    public void hardButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onHardButtonClick.Invoke();
    }
    public void startButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onStartButtonClick.Invoke();
    }    
    public void backButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onBackButtonClick.Invoke();
    }
    public void randomizeSeedButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        seedText.text = getRandomSeed().ToString();
        onRandomizeSeedClick.Invoke();
    }
    public void seedChanged()
    {
        string numbers = new string(seedText.text.Where(char.IsDigit).ToArray());
        seedText.text = numbers;
        PlayerPrefs.SetInt(PlayerPrefsKey.SEED_KEY, int.Parse(numbers));
    }

    private int getRandomSeed()
    {
        return Random.Range(1, int.MaxValue);
    }
}
