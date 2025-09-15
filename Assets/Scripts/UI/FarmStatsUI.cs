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
                        growText.text = "�߾Ʊ�";
                        break;
                    case GrowthStats.Sprout:
                        growText.text = "�����";
                        break;
                    case GrowthStats.Mature:
                        growText.text = "������";
                        break;
                }
                currentHumidityText.text = $"���� ���� : {farm.tile.humidity}%";
                farmNameText.text = $"�۹� : {farm.seedType}";
                string weathers = "";
                foreach (var weather in farm.cropWeathers)
                {
                    string ss = "";
                    if (weather == Weather.lucidity)
                    {
                        ss = "����";
                    } else if (weather == Weather.cloud)
                    {
                        ss = "�帲";
                    } else if (weather == Weather.rain)
                    {
                        ss = "��";
                    } else if (weather == Weather.storm)
                    {
                        ss = "��ǳ";
                    } else if (weather == Weather.stone)
                    {
                        ss = "���";
                    }
                    weathers += ss;
                    weathers += ", ";
                }
                weathersText.text = $"���� ���� : {weathers}";
                timeText.text = $"�ð��� : {farm.cropMinTime}�� ~ {farm.cropMaxTime}��";
                humidityText.text = $"���� ���� : {farm.cropMinhumidity}% ~ {farm.cropMaxhumidity}%";
                if (farm.growCheck())
                {
                    growSpeedText.text = $"���� �ӵ� : 0";
                }
                else if (farm.tile.humidity <= 30 || farm.tile.humidity >= 80)
                {
                    growSpeedText.text = $"���� �ӵ� : -1";
                }
                else
                {
                    switch (GameManager.Instance.currentWeather)
                    {
                        case Weather.lucidity:
                            growSpeedText.text = $"���� �ӵ� : 2";
                            break;
                        case Weather.cloud:
                            growSpeedText.text = $"���� �ӵ� : 1";
                            break;
                        case Weather.rain:
                            growSpeedText.text = $"���� �ӵ� : 2";
                            break;
                        case Weather.storm:
                            growSpeedText.text = $"���� �ӵ� : -1";
                            break;
                        case Weather.stone:
                            growSpeedText.text = $"���� �ӵ� : 1";
                            break;
                    }
                }

            }
            else
            {
                growSlider.value = 0;
                growText.text = "";
                farmNameText.text = $"�۹� : ����";
                weathersText.text = $"���� ���� : ����";
                timeText.text = $"�ð��� : ����";
                currentHumidityText.text = $"���� ���� : {farm.tile.humidity}%";
                humidityText.text = $"���� ���� : ����";
                growSpeedText.text = $"���� �ӵ� : ����";
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
            greenHouseText.text = "����Ͽ콺 ����";
        }
        else
        {
            greenHouseText.text = "����Ͽ콺 ��ġ";
        }
        if (farm.tile.autoGet)
        {
            autoGetText.text = "�ڵ���Ȯ�� ����";
        }
        else
        {
            autoGetText.text = "�ڵ���Ȯ�� ��ġ";
        }

        fieldAndSeedAndGetText.GetComponentInParent<Button>().interactable = true;
        if (farm.farmStats == FarmStats.Farm)
        {
            fieldAndSeedAndGetText.GetComponentInParent<Button>().interactable = false;
        }
        if (farm.farmStats == FarmStats.Plan)
        {
            fieldAndSeedAndGetText.text = $"�� ����";
        }
        else if (farm.farmStats == FarmStats.Field)
        {
            fieldAndSeedAndGetText.text = $"���� ����";
        }
        else if (farm.farmStats == FarmStats.Farm)
        {
            fieldAndSeedAndGetText.text = $"��Ȯ �ϱ�";
        }
        if (statsWindow.activeSelf)
        {

            if (farm.cropWeathers.Length > 0)
            {
                farmNameText.text = $"�۹� : {farm.seedType}";
                string weathers = "";
                foreach (var weather in farm.cropWeathers)
                {
                    string ss = "";
                    if (weather == Weather.lucidity)
                    {
                        ss = "����";
                    }
                    else if (weather == Weather.cloud)
                    {
                        ss = "�帲";
                    }
                    else if (weather == Weather.rain)
                    {
                        ss = "��";
                    }
                    else if (weather == Weather.storm)
                    {
                        ss = "��ǳ";
                    }
                    else if (weather == Weather.stone)
                    {
                        ss = "���";
                    }
                    weathers += ss;
                    weathers += ", ";
                }
                weathersText.text = $"���� ���� : {weathers}";
                timeText.text = $"�ð��� : {farm.cropMinTime}�� ~ {farm.cropMaxTime}��";
                humidityText.text = $"���� ���� : {farm.cropMinhumidity}% ~ {farm.cropMaxhumidity}%";
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
            fieldAndSeedAndGetText.text = $"���� ����";
        }
        else if (farm.farmStats == FarmStats.Field)
        {
            statsWindow.SetActive(false);
            seedWindow.SetActive(true);
        }
        else if (farm.farmStats == FarmStats.Farm)
        {
            fieldAndSeedAndGetText.text = $"�� ����";
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
        fieldAndSeedAndGetText.text = $"��Ȯ �ϱ�";
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
            greenHouseText.text = "����Ͽ콺 ��ġ";
            farm.tile.greenhouse = false;
        }
        else
        {
            if (GameManager.Instance.greenHouseCount > 0)
            {
                GameManager.Instance.greenHouseCount--;
                greenHouseText.text = "����Ͽ콺 ����";
                farm.tile.greenhouse = true;
            }
        }
    }

    public void AutoGet()
    {
        if (farm.tile.autoGet)
        {
            autoGetText.text = "�ڵ���Ȯ�� ��ġ";
            farm.tile.autoGet = false;
        }
        else
        {
            if (GameManager.Instance.autoGetCount > 0)
            {
                GameManager.Instance.autoGetCount--;
                autoGetText.text = "�ڵ���Ȯ�� ����";
                farm.tile.autoGet = true;
            }
        }
    }
}
