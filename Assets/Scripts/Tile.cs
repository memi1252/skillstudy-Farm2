using JetBrains.Annotations;
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
    public GameObject greenHouseObject;
    public GameObject autoGetObject;
    public int getCount = 0;
    public float animalSpawnPercent;
    public int animalCount;
    public Transform[] animalMovePoints;
    public GameObject[] animalPrefabs;
    public List<GameObject> animals;
    public GameObject[] nearTiles;
    public GameObject[] waterBukkits;


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
        Camera.main.GetComponent<PhysicsRaycaster>().enabled = false;
        buyUI.SetActive(true);
    }

    public void BUY()
    {
        
        if(GameManager.Instance.money >= GameManager.Instance.tilePrice)
        {
            GameManager.Instance.money -= GameManager.Instance.tilePrice;
        }
        else
        {
            return;
        }
        if (GameManager.Instance.tilePrice == 0)
        {
            GameManager.Instance.tilePrice = 2000;
            foreach (var tile in GameManager.Instance.allTile)
            {
                bool yes = false;
                foreach (var tile2 in nearTiles)
                {
                    if (tile2 == tile)
                    {
                        yes = true;
                    }
                }
                if (tile == gameObject)
                {
                    tile.SetActive(true);
                }
                else if (yes)
                {
                    tile.SetActive(true);
                }
                else
                {
                    tile.SetActive(false);
                }

            }
        }
        else
        {
            GameManager.Instance.tilePrice = (int)(GameManager.Instance.tilePrice * 1.5f);
            foreach (var tile in nearTiles)
            {
                tile.SetActive(true);
            }
        }
        buy = true;
        buyObject.SetActive(false);
        farmObject.SetActive(true);
        
        Camera.main.GetComponent<PhysicsRaycaster>().enabled = true;
    }

    public void NO()
    {
        buyUI.SetActive(false);
        Camera.main.GetComponent<PhysicsRaycaster>().enabled = true;
    }

    public void Get()
    {
        getCount++;
        if(getCount == 3)
        {
            animalSpawnPercent = 0.05f;
        }else if(getCount > 3)
        {
            animalSpawnPercent += 0.02f;
        }

        float ramdomValue = UnityEngine.Random.value;

        if (animalSpawnPercent > ramdomValue)
        {
            int count = Random.Range(1, 4);
            for(int i =0; i < count; i++)
            {
                var animal = Instantiate(animalPrefabs[Random.Range(0, animalPrefabs.Length)]);
                animal.transform.position = animalMovePoints[Random.Range(0, animalMovePoints.Length)].position;
                animal.GetComponent<Animal>().Set(this);
                animalCount++;
                animals.Add(animal);
            }
            GameManager.Instance.messageUI.add("동물이 출현하였습니다.", Color.green, true);
            getCount = 0;
            animalSpawnPercent = 0;
        }
    }
}
