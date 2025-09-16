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
        moneyText.text = $"{GameManager.Instance.money}��";
        string weather = "";
        Color weatherColor = Color.white;
        switch (GameManager.Instance.currentWeather)
        {
            case Weather.lucidity:
                weather = "����";
                weatherColor = Color.yellow;
                break;
            case Weather.cloud:
                weather = "�帲";
                weatherColor = Color.gray;
                break;
            case Weather.rain:
                weather = "��";
                weatherColor = Color.cyan;
                break;
            case Weather.storm:
                weather = "��ǳ";
                weatherColor = Color.gray;
                break;
            case Weather.stone:
                weather = "���";
                weatherColor = Color.gray;
                break;
        }
        weatherText.text = weather;
        weatherText.color = weatherColor;
        inGameTimeText.text = $"{GameManager.Instance.hour:D2}:{GameManager.Instance.minute:D2}";
    }
}
