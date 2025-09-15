using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class FarmStatsUI : MonoBehaviour
{
    public Farm farm;
    public Slider growSlider;
    public Text growText;

    public GameObject seedWindow;
    public GameObject statsWindow;
    public Text farmNameText;
    public Text weathersText;
    public Text timeText;
    public Text currentHumidityText;
    public Text humidityText;
    public Text growSpeedText;
    public Text fieldAndSeedAndGetText;
    public Text greenHouseText;
    public Text autoGetText;

    public void Update()
    {
        if (farm != null)
        {
            if (farm.farmStats == FarmStats.Farm)
            {
                growSlider.value = farm.growCurrentTime / farm.growMaxTime;
                switch (farm.growthStats)
                {
                    case GrowthStats.Seed:
                        growText.text = "발아기";
                        break;
                    case GrowthStats.Sprout:
                        growText.text = "성장기";
                        break;
                    case GrowthStats.Mature:
                        growText.text = "성숙기";
                        break;
                }
                currentHumidityText.text = $"현재 습도 : {farm.tile.humidity}%";
                farmNameText.text = $"작물 : {farm.seedType}";
                string weathers = "";
                foreach (var weather in farm.cropWeathers)
                {
                    string ss = "";
                    if (weather == Weather.lucidity)
                    {
                        ss = "맑음";
                    } else if (weather == Weather.cloud)
                    {
                        ss = "흐림";
                    } else if (weather == Weather.rain)
                    {
                        ss = "비";
                    } else if (weather == Weather.storm)
                    {
                        ss = "폭풍";
                    } else if (weather == Weather.stone)
                    {
                        ss = "우박";
                    }
                    weathers += ss;
                    weathers += ", ";
                }
                weathersText.text = $"적합 날씨 : {weathers}";
                timeText.text = $"시간대 : {farm.cropMinTime}시 ~ {farm.cropMaxTime}시";
                humidityText.text = $"최적 습도 : {farm.cropMinhumidity}% ~ {farm.cropMaxhumidity}%";
                if (farm.growCheck())
                {
                    growSpeedText.text = $"성장 속도 : 0";
                }
                else if (farm.tile.humidity <= 30 || farm.tile.humidity >= 80)
                {
                    growSpeedText.text = $"성장 속도 : -1";
                }
                else
                {
                    switch (GameManager.Instance.currentWeather)
                    {
                        case Weather.lucidity:
                            growSpeedText.text = $"성장 속도 : 2";
                            break;
                        case Weather.cloud:
                            growSpeedText.text = $"성장 속도 : 1";
                            break;
                        case Weather.rain:
                            growSpeedText.text = $"성장 속도 : 2";
                            break;
                        case Weather.storm:
                            growSpeedText.text = $"성장 속도 : -1";
                            break;
                        case Weather.stone:
                            growSpeedText.text = $"성장 속도 : 1";
                            break;
                    }
                }

            }
            else
            {
                growSlider.value = 0;
                growText.text = "";
                farmNameText.text = $"작물 : 없음";
                weathersText.text = $"적합 날씨 : 없음";
                timeText.text = $"시간대 : 없음";
                currentHumidityText.text = $"현재 습도 : {farm.tile.humidity}%";
                humidityText.text = $"최적 습도 : 없음";
                growSpeedText.text = $"성장 속도 : 없음";
            }

            if (farm.growthStats == GrowthStats.Mature)
            {
                fieldAndSeedAndGetText.GetComponentInParent<Button>().interactable = true;
            }

        }
    }


    public void Show(Farm farm)
    {
        gameObject.SetActive(true);
        StatsOpen();
        statsWindow.SetActive(true);
        seedWindow.SetActive(false);
        this.farm = farm;
        if (farm.tile.greenhouse)
        {
            greenHouseText.text = "비닐하우스 제거";
        }
        else
        {
            greenHouseText.text = "비닐하우스 설치";
        }
        if (farm.tile.autoGet)
        {
            autoGetText.text = "자동수확기 제거";
        }
        else
        {
            autoGetText.text = "자동수확기 설치";
        }

        fieldAndSeedAndGetText.GetComponentInParent<Button>().interactable = true;
        if (farm.farmStats == FarmStats.Farm)
        {
            fieldAndSeedAndGetText.GetComponentInParent<Button>().interactable = false;
        }
        if (farm.farmStats == FarmStats.Plan)
        {
            fieldAndSeedAndGetText.text = $"밭 갈기";
        }
        else if (farm.farmStats == FarmStats.Field)
        {
            fieldAndSeedAndGetText.text = $"씨앗 선택";
        }
        else if (farm.farmStats == FarmStats.Farm)
        {
            fieldAndSeedAndGetText.text = $"수확 하기";
        }
        if (statsWindow.activeSelf)
        {

            if (farm.cropWeathers.Length > 0)
            {
                farmNameText.text = $"작물 : {farm.seedType}";
                string weathers = "";
                foreach (var weather in farm.cropWeathers)
                {
                    string ss = "";
                    if (weather == Weather.lucidity)
                    {
                        ss = "맑음";
                    }
                    else if (weather == Weather.cloud)
                    {
                        ss = "흐림";
                    }
                    else if (weather == Weather.rain)
                    {
                        ss = "비";
                    }
                    else if (weather == Weather.storm)
                    {
                        ss = "폭풍";
                    }
                    else if (weather == Weather.stone)
                    {
                        ss = "우박";
                    }
                    weathers += ss;
                    weathers += ", ";
                }
                weathersText.text = $"적합 날씨 : {weathers}";
                timeText.text = $"시간대 : {farm.cropMinTime}시 ~ {farm.cropMaxTime}시";
                humidityText.text = $"최적 습도 : {farm.cropMinhumidity}% ~ {farm.cropMaxhumidity}%";
            }


        }
    }

    public void StatsOpen()
    {
        statsWindow.SetActive(true);
        seedWindow.SetActive(false);
    }

    public void FieldAndSeedAndGet()
    {
        if (farm.farmStats == FarmStats.Plan)
        {
            farm.Feild();
            fieldAndSeedAndGetText.text = $"씨앗 선택";
        }
        else if (farm.farmStats == FarmStats.Field)
        {
            statsWindow.SetActive(false);
            seedWindow.SetActive(true);
        }
        else if (farm.farmStats == FarmStats.Farm)
        {
            fieldAndSeedAndGetText.text = $"밭 갈기";
            farm.FarmReset();
        }
    }


    public void Seed(int index)
    {

        switch (index)
        {
            case 1:
                farm.Seed(SeedType.Broccoli);
                break;
            case 2:
                farm.Seed(SeedType.Carrot);
                break;
            case 3:
                farm.Seed(SeedType.Cauiliflower);
                break;
            case 4:
                farm.Seed(SeedType.Corn);
                break;
            case 5:
                farm.Seed(SeedType.Mashroom);
                break;
        }
        farm.GrowthStage(GrowthStats.Seed);
        farm.farmStats = FarmStats.Farm;
        fieldAndSeedAndGetText.text = $"수확 하기";
        fieldAndSeedAndGetText.transform.GetComponentInParent<Button>().interactable = false;
        statsWindow.SetActive(true);
        seedWindow.SetActive(false);
    }

    public void Water()
    {
        if (GameManager.Instance.watergunCount > 0)
        {
            GameManager.Instance.watergunCount--;
            if (GameManager.Instance.nomalWatergun)
            {
                farm.tile.humidity += farm.tile.humidity * 0.5f;
            }
            else
            {
                farm.tile.humidity += farm.tile.humidity * 0.2f;
            }
        }

    }

    public void GreenHouse()
    {
        if (farm.tile.greenhouse)
        {
            greenHouseText.text = "비닐하우스 설치";
            farm.tile.greenhouse = false;
        }
        else
        {
            if (GameManager.Instance.greenHouseCount > 0)
            {
                GameManager.Instance.greenHouseCount--;
                greenHouseText.text = "비닐하우스 제거";
                farm.tile.greenhouse = true;
            }
        }
    }

    public void AutoGet()
    {
        if (farm.tile.autoGet)
        {
            autoGetText.text = "자동수확기 설치";
            farm.tile.autoGet = false;
        }
        else
        {
            if (GameManager.Instance.autoGetCount > 0)
            {
                GameManager.Instance.autoGetCount--;
                autoGetText.text = "자동수확기 제거";
                farm.tile.autoGet = true;
            }
        }
    }
}
