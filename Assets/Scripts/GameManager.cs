using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public enum Weather
{
    lucidity,
    cloud,
    rain,
    storm,
    stone
}

[System.Serializable]
public struct farmPrice
{
    public int maxPrice;
    public int minPrice;
    public int price;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Weather currentWeather;

    public int money = 0;

    public bool nomalWatergun = true;
    public int watergunCount = 0;
    public int watergunMaxCount1 = 3;
    public int watergunMaxCount2 = 20;

    public bool nomalGet = true;


    public int greenHouseCount = 0;
    public int autoGetCount = 0;
    public int animalGetCount = 0;
    public float animalDestroyPercent;

    public int seed1, seed2, seed3, seed4, seed5;
    public int farm1, farm2, farm3, farm4, farm5;
    public farmPrice[] farmPrices;

    public float inGameTime = 0;
    public int hour;
    public int minute;

    public int tilePrice = 0;

    public FarmStatsUI farmStatsUI;
    public MessageUI messageUI;
    public GameObject[] allTile;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //CursorShow(false);
    }

    private void Update()
    {
        inGameTime += Time.deltaTime * 60 *12;
        if(inGameTime >= 86400)
        {

            for(int i = 0; i < farmPrices.Length; i++)
            {
                int add = Random.Range(1000, 2001);
                if (farmPrices[i].price == farmPrices[i].minPrice)
                {
                    farmPrices[i].price += add;
                }
                else if (farmPrices[i].price == farmPrices[i].maxPrice)
                {
                    farmPrices[i].price -= add;
                }
                else
                {
                    int pluseMiuse = Random.Range(0, 2);
                    if (pluseMiuse == 0)
                    {
                        farmPrices[i].price -= add;
                    }
                    else
                    {
                        farmPrices[i].price += add;
                    }
                }
                if(farmPrices[i].price < farmPrices[i].minPrice)
                {
                    farmPrices[i].price = farmPrices[i].minPrice;
                }else if (farmPrices[i].price > farmPrices[i].maxPrice)
                {
                    farmPrices[i].price = farmPrices[i].maxPrice;
                }
            }
            
            inGameTime = 0;
            int WeatherIndex = Random.Range(0, 5);
            switch (WeatherIndex)
            {
                case 0:
                    currentWeather = Weather.lucidity;
                    break;
                case 1:
                    currentWeather = Weather.cloud;
                    break;
                case 2:
                    currentWeather = Weather.rain;
                    break;
                case 3:
                    currentWeather = Weather.storm;
                    break;
                case 4:
                    currentWeather = Weather.stone;
                    break;
            }
        }
        hour = (int)(inGameTime / 3600);
        minute = (int)((inGameTime % 3600)/60);
    }

    public void CursorShow(bool show)
    {
        if (show)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
