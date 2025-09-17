using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public GameObject tvUI;
    public Text weatherText;
    public Text nextWeatherText;
    public Text wetherText2;
    public Text[] farmTexts;
    public GameObject CameraObj;

    public void OnTV()
    {
        tvUI.SetActive(true);
        CameraObj.SetActive(true);
        GameManager.Instance.dontMove = true;
        string weathers = "";
        switch (GameManager.Instance.currentWeather)
        {

            case Weather.lucidity:
                weathers = "¸¼À½";
                break;
            case Weather.cloud:
                weathers = "Èå¸²";
                break;
            case Weather.rain:
                weathers = "ºñ";
                break;
            case Weather.storm:
                weathers = "ÆøÇ³";
                break;
            case Weather.stone:
                weathers = "¿ì¹Ú";
                break;
        }
        weatherText.text = $"¿À´ÃÀÇ ³¯¾¾ : " + weathers;
        switch (GameManager.Instance.nextWeather)
        {

            case Weather.lucidity:
                weathers = "¸¼À½";
                break;
            case Weather.cloud:
                weathers = "Èå¸²";
                break;
            case Weather.rain:
                weathers = "ºñ";
                break;
            case Weather.storm:
                weathers = "ÆøÇ³";
                break;
            case Weather.stone:
                weathers = "¿ì¹Ú";
                break;
        }
        nextWeatherText.text = $"³»ÀÏÀÇ ³¯¾¾ : " + weathers;
        if (GameManager.Instance.doubleLucidity)
        {
            wetherText2.gameObject.SetActive(true);
        }
        else
        {
            wetherText2.gameObject.SetActive(false);
        }
        for (int i = 0; i < farmTexts.Length; i++)
        {
            string value = GameManager.Instance.farmPrices[i].add < 0 ? "°¨¼Ò" : "Áõ°¡";
            farmTexts[i].text = $"{GameManager.Instance.farmPrices[i].name} °¡°Ý : {GameManager.Instance.farmPrices[i].price}¿ø, " +
                $"{GameManager.Instance.farmPrices[i].add}¿ø{value}";
        }
        Invoke("Off", 5);
    }

    private void Off()
    {
        GameManager.Instance.dontMove = false;
        CameraObj.SetActive(false);
        tvUI.SetActive(false);
    }

}
