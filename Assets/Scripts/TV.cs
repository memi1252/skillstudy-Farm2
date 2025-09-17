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
                weathers = "����";
                break;
            case Weather.cloud:
                weathers = "�帲";
                break;
            case Weather.rain:
                weathers = "��";
                break;
            case Weather.storm:
                weathers = "��ǳ";
                break;
            case Weather.stone:
                weathers = "���";
                break;
        }
        weatherText.text = $"������ ���� : " + weathers;
        switch (GameManager.Instance.nextWeather)
        {

            case Weather.lucidity:
                weathers = "����";
                break;
            case Weather.cloud:
                weathers = "�帲";
                break;
            case Weather.rain:
                weathers = "��";
                break;
            case Weather.storm:
                weathers = "��ǳ";
                break;
            case Weather.stone:
                weathers = "���";
                break;
        }
        nextWeatherText.text = $"������ ���� : " + weathers;
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
            string value = GameManager.Instance.farmPrices[i].add < 0 ? "����" : "����";
            farmTexts[i].text = $"{GameManager.Instance.farmPrices[i].name} ���� : {GameManager.Instance.farmPrices[i].price}��, " +
                $"{GameManager.Instance.farmPrices[i].add}��{value}";
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
