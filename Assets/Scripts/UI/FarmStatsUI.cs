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
                    growSpeedText.text = $"성장 속도 : 1";
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

                    if (!farm.growCheck())
                    {
                        growSpeedText.text = $"성장 속도 : 0";
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
        //GameManager.Instance.CursorShow(true);
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
        if(farm.farmStats == FarmStats.autoGet)
        {
            return;
        }
        else if (farm.farmStats == FarmStats.Plan)
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
                if(GameManager.Instance.seed1 > 0)
                {
                    GameManager.Instance.seed1--;
                    farm.Seed(SeedType.Broccoli);
                    farm.GrowthStage(GrowthStats.Seed);
                    farm.farmStats = FarmStats.Farm;
                    fieldAndSeedAndGetText.text = $"수확 하기";
                    fieldAndSeedAndGetText.transform.GetComponentInParent<Button>().interactable = false;
                    statsWindow.SetActive(true);
                    seedWindow.SetActive(false);

                }
                else
                {
                    GameManager.Instance.messageUI.add("브로콜리의 씨앗이 부족합니다",Color.red, true);
                }
                    break;
            case 2:
                if (GameManager.Instance.seed2 > 0)
                {
                    GameManager.Instance.seed2--;
                    farm.Seed(SeedType.Carrot);
                    farm.GrowthStage(GrowthStats.Seed);
                    farm.farmStats = FarmStats.Farm;
                    fieldAndSeedAndGetText.text = $"수확 하기";
                    fieldAndSeedAndGetText.transform.GetComponentInParent<Button>().interactable = false;
                    statsWindow.SetActive(true);
                    seedWindow.SetActive(false);
                }
                else
                {
                    GameManager.Instance.messageUI.add("당근의 씨앗이 부족합니다", Color.red, true);
                }
                break;
            case 3:
                if (GameManager.Instance.seed3 > 0)
                {
                    GameManager.Instance.seed3--;
                    farm.Seed(SeedType.Cauiliflower);
                    farm.GrowthStage(GrowthStats.Seed);
                    farm.farmStats = FarmStats.Farm;
                    fieldAndSeedAndGetText.text = $"수확 하기";
                    fieldAndSeedAndGetText.transform.GetComponentInParent<Button>().interactable = false;
                    statsWindow.SetActive(true);
                    seedWindow.SetActive(false);
                }
                else
                {
                    GameManager.Instance.messageUI.add("양배추의 씨앗이 부족합니다", Color.red, true);
                }
                break;
            case 4:
                if (GameManager.Instance.seed4 > 0)
                {
                    GameManager.Instance.seed4--;
                    farm.Seed(SeedType.Corn);
                    farm.GrowthStage(GrowthStats.Seed);
                    farm.farmStats = FarmStats.Farm;
                    fieldAndSeedAndGetText.text = $"수확 하기";
                    fieldAndSeedAndGetText.transform.GetComponentInParent<Button>().interactable = false;
                    statsWindow.SetActive(true);
                    seedWindow.SetActive(false);
                }
                else
                {
                    GameManager.Instance.messageUI.add("옥수수의 씨앗이 부족합니다", Color.red, true);
                }
                break;
            case 5:
                if (GameManager.Instance.seed5 > 0)
                {
                    GameManager.Instance.seed5--;
                    farm.Seed(SeedType.Mashroom);
                    farm.GrowthStage(GrowthStats.Seed);
                    farm.farmStats = FarmStats.Farm;
                    fieldAndSeedAndGetText.text = $"수확 하기";
                    fieldAndSeedAndGetText.transform.GetComponentInParent<Button>().interactable = false;
                    statsWindow.SetActive(true);
                    seedWindow.SetActive(false);
                }
                else
                {
                    GameManager.Instance.messageUI.add("버섯의 씨앗이 부족합니다", Color.red, true);
                }
                break;
        }
        
    }

    public void Water()
    {
        if (GameManager.Instance.watergunCount > 0)
        {
            GameManager.Instance.watergunCount--;
            if (GameManager.Instance.nomalWatergun)
            {
                farm.tile.humidity += farm.tile.humidity * 0.5f;
                var water = Instantiate(farm.tile.waterBukkits[0]);
                water.transform.position = farm.tile.transform.position + new Vector3(0, 2, 0);
                StartCoroutine(waterDestroy(water));
            }
            else
            {
                farm.tile.humidity += farm.tile.humidity * 0.2f;
                var water = Instantiate(farm.tile.waterBukkits[1]);
                water.transform.position = farm.tile.transform.position + new Vector3(0, 2, 0);
                StartCoroutine(waterDestroy(water));
            }
        }
        else
        {
            GameManager.Instance.messageUI.add("물이 부족합니다", Color.red, true);
        }

    }

    IEnumerator waterDestroy(GameObject water)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(water);
    }

    public void GreenHouse()
    {
        if (farm.tile.greenhouse)
        {
            greenHouseText.text = "비닐하우스 설치";
            farm.tile.greenhouse = false;
            farm.tile.greenHouseObject.SetActive(false);
        }
        else
        {
            if (GameManager.Instance.greenHouseCount > 0)
            {
                GameManager.Instance.greenHouseCount--;
                greenHouseText.text = "비닐하우스 제거";
                farm.tile.greenhouse = true;
                farm.tile.greenHouseObject.SetActive(true);
            }
            else
            {
                GameManager.Instance.messageUI.add("비닐하우스가 부족합니다", Color.red, true);
            }
        }
    }

    public void AutoGet()
    {
        if (farm.tile.autoGet)
        {
            autoGetText.text = "자동수확기 설치";
            farm.tile.autoGet = false;
            farm.tile.autoGetObject.SetActive(false);
        }
        else
        {
            if(farm.farmStats == FarmStats.Field || farm.farmStats == FarmStats.Farm)
            {
                return;
            }
            if (GameManager.Instance.autoGetCount > 0)
            {
                GameManager.Instance.autoGetCount--;
                autoGetText.text = "자동수확기 제거";
                farm.tile.autoGetObject = farm.autoGetObject;
                farm.farmStats = FarmStats.autoGet;
                farm.tile.autoGetObject.SetActive(true);
                farm.tile.autoGet = true;
            }
            else
            {
                GameManager.Instance.messageUI.add("자동수확기가 부족합니다", Color.red, true);
            }
        }
    }

    public void AnimalGet()
    {
        if(GameManager.Instance.animalGetCount > 0)
        {
            int count = Random.Range(1, 4);
            for (int i = 0; i < count; i++)
            {
                if (farm.tile.animals.Count ==0)
                {
                    return;
                }
                farm.tile.animalCount--;
                Destroy(farm.tile.animals[0]);
                farm.tile.animals.RemoveAt(0);
            }
            float randomValue = Random.value;
            if(GameManager.Instance.animalDestroyPercent > randomValue)
            {
                GameManager.Instance.animalGetCount--;
                GameManager.Instance.animalDestroyPercent = 0;
                GameManager.Instance.messageUI.add("포획기가 파괴되었습니다", Color.red, true);
            }
            else
            {
                GameManager.Instance.animalDestroyPercent += 0.2f;
            }
        }
        else
        {
            GameManager.Instance.messageUI.add("포획기가 부족합니다", Color.red, true);
        }
    }
}
