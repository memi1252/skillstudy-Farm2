using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text moneyText;
    public Text weatherText;
    public Text inGameTimeText;

    private void Update()
    {
        moneyText.text = $"{GameManager.Instance.money}¿ø";
        string weather = "";
        Color weatherColor = Color.white;
        switch (GameManager.Instance.currentWeather)
        {
            case Weather.lucidity:
                weather = "¸¼À½";
                weatherColor = Color.yellow;
                break;
            case Weather.cloud:
                weather = "Èå¸²";
                weatherColor = Color.gray;
                break;
            case Weather.rain:
                weather = "ºñ";
                weatherColor = Color.cyan;
                break;
            case Weather.storm:
                weather = "ÆøÇ³";
                weatherColor = Color.gray;
                break;
            case Weather.stone:
                weather = "¿ì¹Ú";
                weatherColor = Color.gray;
                break;
        }
        weatherText.text = weather;
        weatherText.color = weatherColor;
        inGameTimeText.text = $"{GameManager.Instance.hour:D2}:{GameManager.Instance.minute:D2}";
    }
}
