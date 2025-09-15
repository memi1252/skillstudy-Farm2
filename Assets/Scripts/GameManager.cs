using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather
{
    lucidity,
    cloud,
    rain,
    storm,
    stone
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Weather currentWeather;

    public bool nomalWatergun = true;
    public int watergunCount = 0;
    public int watergunMaxCount1 = 3;
    public int watergunMaxCount2 = 20;

    public bool nomalGet = true;


    public int greenHouseCount = 0;
    public int autoGetCount = 0;

    public int seed1, seed2, seed3, seed4, seed5;
    public int farm1, farm2, farm3, farm4, farm5;

    public float inGameTime = 0;
    public int hour;
    public int minute;

    public int tilePrice = 0;

    public FarmStatsUI farmStatsUI;

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

    private void Update()
    {
        inGameTime += Time.deltaTime * 60 *12;
        if(inGameTime >= 86400)
        {
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
}
