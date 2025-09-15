using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public bool greenhouse =false;
    public bool autoGet = false;
    public float humidity = 50;
    public Image humiditySlider;
    public bool buy = false;
    public GameObject buyObject;
    public Text buyPriceText;
    public GameObject buyUI;
    public GameObject farmObject;


    private float humidityTime;

    private void Update()
    {
        if (!buy)
        {
            if(GameManager.Instance.tilePrice == 0)
            {
                buyPriceText.text = $"원하는 타일을\n 선택하세요";
            }
            else
            {
                buyPriceText.text = $"{GameManager.Instance.tilePrice}원";
            }
        }
        if (!greenhouse)
        {
            humiditySlider.fillAmount = humidity / 100;
            humidityTime += Time.deltaTime;
            if (humidityTime >= 5)
            {
                humidityTime = 0;
                switch (GameManager.Instance.currentWeather)
                {
                    case Weather.lucidity:
                        humidity += -1;
                        break;
                    case Weather.cloud:
                        humidity += -1;
                        break;
                    case Weather.rain:
                        humidity += +1;
                        break;
                    case Weather.storm:
                        humidity += -10;
                        break;
                    case Weather.stone:
                        humidity += -15;
                        break;
                }
            }
            if (humidity > 100)
            {
                humidity = 100;
            }
            else if (humidity < 0)
            {
                humidity = 0;
            }
        }
        if (autoGet)
        {
            foreach(var farm in GetComponentsInChildren<Farm>())
            {
                if(farm.growthStats == GrowthStats.Mature)
                {
                    farm.FarmReset();
                }
            }
        }
        
    }

    public void BuyUI()
    {
        buy = true;
        Camera.main.GetComponent<PhysicsRaycaster>().enabled = false;
        buyUI.SetActive(buy);
    }

    public void BUY()
    {
        
        if(GameManager.Instance.tilePrice == 0)
        {
            GameManager.Instance.tilePrice = 2000;
        }
        else
        {
            GameManager.Instance.tilePrice = (int)(GameManager.Instance.tilePrice * 1.5f);
        }
        buyObject.SetActive(false);
        farmObject.SetActive(true);
        Camera.main.GetComponent<PhysicsRaycaster>().enabled = true;
    }
}
