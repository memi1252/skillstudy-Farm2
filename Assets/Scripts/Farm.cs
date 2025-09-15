using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum FarmStats
{
    Plan,
    Field,
    Farm
}

public enum GrowthStats
{
    Seed,
    Sprout,
    Mature
}

public enum SeedType
{
    Broccoli,
    Carrot,
    Cauiliflower,
    Corn,
    Mashroom,
}

[System.Serializable]
public struct CropObject
{
    public string name;
    public GameObject seedObject;
    public GameObject sproutObject;
    public GameObject matureObject;
    public Weather[] weathers;
    public int minTime;
    public int MaxTime;
    public int growTime;
    public int minhumidity;
    public int maxhumidity;
}

public class Farm : MonoBehaviour
{
    public FarmStats farmStats;
    public GrowthStats growthStats;
    public SeedType seedType;
    public Mesh planMesh;
    public Mesh fieldMesh;
    public Tile tile;
    
    public CropObject[] cropObjects;
    
    private MeshFilter meshFilter;

    public float growCurrentTime;
    public float growMaxTime { get; private set; }
    public Weather[] cropWeathers { get; private set; }
    public int cropMinTime { get; private set; }
    public int cropMaxTime { get; private set; }
    public int cropMinhumidity { get; private set; }
    public float cropMaxhumidity { get; private set; }


    private void Awake()
    {
	    farmStats = FarmStats.Plan;
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        cropWeathers = new Weather[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (farmStats == FarmStats.Plan)
            {
                Feild();
            } else if (farmStats == FarmStats.Field)
            {
                GrowthStage(GrowthStats.Seed);
                farmStats = FarmStats.Farm;
            } else if (growthStats == GrowthStats.Seed)
            {
                GrowthStage(GrowthStats.Sprout);
            } else if (growthStats == GrowthStats.Sprout)
            {
                GrowthStage(GrowthStats.Mature);
            }else if(growthStats == GrowthStats.Mature)
            {
                FarmReset();
            }
        }
        
        if (farmStats == FarmStats.Farm)
        {
            if (growCheck())
            {
                switch (GameManager.Instance.currentWeather)
                {
                    case Weather.lucidity:
                        growCurrentTime += Time.deltaTime * 2;
                        break;
                    case Weather.cloud:
                        growCurrentTime += Time.deltaTime;
                        break;
                    case Weather.rain:
                        growCurrentTime += Time.deltaTime * 2;
                        break;
                    case Weather.storm:
                        growCurrentTime += -Time.deltaTime;
                        break;
                    case Weather.stone:
                        growCurrentTime += Time.deltaTime;
                        break;
                }
            }
            else
            {
                if (tile.humidity <= 30 || tile.humidity >= 80)
                {
                    growCurrentTime += -Time.deltaTime;
                }
            }
            if (growthStats == GrowthStats.Seed)
            {
                float seedTime = growMaxTime / 2;
                if (growCurrentTime >= seedTime)
                {
                    GrowthStage(GrowthStats.Sprout);
                }
            }
            else if (growthStats == GrowthStats.Sprout)
            {
                float sproutTime = growMaxTime;
                if (growCurrentTime >= sproutTime)
                {
                    GrowthStage(GrowthStats.Mature);
                }
            }
        }
    }

    public bool growCheck()
    {
        bool grow = false;
        foreach (var weather in cropWeathers)
        {
            if (weather == GameManager.Instance.currentWeather)
            {
                grow = true;
                break;
            }
        }
        if (!grow)
        {
            return false;
        }
        if (tile.greenhouse)
        {
            grow = true;
        }
        if (tile.humidity <= 30 || tile.humidity >= 80)
        {
            return false;
        }
        else if (tile.humidity > cropMinhumidity && tile.humidity < cropMaxhumidity)
        {
            grow = true;
        }
        else
        {
            return false;
        }
        if (cropMaxTime > cropMinTime)
        {
            if (GameManager.Instance.hour + 1 > cropMinTime && GameManager.Instance.hour < cropMaxTime)
            {
                grow = true;
            }
            else
            {
                return false;
            }
        }
        else if (cropMaxTime < cropMinTime)
        {
            if (cropMaxTime >= GameManager.Instance.hour || cropMinTime < GameManager.Instance.hour)
            {
                grow = true;
            }
            else
            {
                return false;
            }
        }
        return grow;
    }

    public void Plan()
    {
        meshFilter.mesh = planMesh;
        farmStats = FarmStats.Plan;
    }

    public void Feild()
    {
        meshFilter.mesh = fieldMesh;
        farmStats = FarmStats.Field;
    }

    public void GrowthStage(GrowthStats stage)
    {
        growthStats = stage;
        switch (stage)
        {
            case GrowthStats.Seed:
                Seed(seedType);
                break;
            case GrowthStats.Sprout:
                Sprout(seedType);
                break;
            case GrowthStats.Mature:
                Mature(seedType);
                break;
        }
    }

    public void Seed(SeedType seedType)
    {
        this.seedType = seedType;
        switch (seedType)
        {
            case SeedType.Broccoli:
                cropObjects[0].seedObject.SetActive(true);
                growMaxTime = cropObjects[0].growTime;
                cropWeathers = cropObjects[0].weathers;
                cropMinTime = cropObjects[0].minTime;
                cropMaxTime = cropObjects[0].MaxTime;
                cropMaxhumidity = cropObjects[0].maxhumidity;
                cropMinhumidity = cropObjects[0].minhumidity;
                break;
            case SeedType.Carrot:
                cropObjects[1].seedObject.SetActive(true);
                growMaxTime = cropObjects[1].growTime;
                cropWeathers = cropObjects[1].weathers;
                cropMinTime = cropObjects[1].minTime;
                cropMaxTime = cropObjects[1].MaxTime;
                cropMaxhumidity = cropObjects[1].maxhumidity;
                cropMinhumidity = cropObjects[1].minhumidity;
                break;
            case SeedType.Cauiliflower:
                cropObjects[2].seedObject.SetActive(true);
                growMaxTime = cropObjects[2].growTime;
                cropWeathers = cropObjects[2].weathers;
                cropMinTime = cropObjects[2].minTime;
                cropMaxTime = cropObjects[2].MaxTime;
                cropMaxhumidity = cropObjects[2].maxhumidity;
                cropMinhumidity = cropObjects[2].minhumidity;
                break;
            case SeedType.Corn:
                cropObjects[3].seedObject.SetActive(true);
                growMaxTime = cropObjects[3].growTime;
                cropWeathers = cropObjects[3].weathers;
                cropMinTime = cropObjects[3].minTime;
                cropMaxTime = cropObjects[3].MaxTime;
                cropMaxhumidity = cropObjects[3].maxhumidity;
                cropMinhumidity = cropObjects[3].minhumidity;
                break;
            case SeedType.Mashroom:
                cropObjects[4].seedObject.SetActive(true);
                growMaxTime = cropObjects[4].growTime;
                cropWeathers = cropObjects[4].weathers;
                cropMinTime = cropObjects[4].minTime;
                cropMaxTime = cropObjects[4].MaxTime;
                cropMaxhumidity = cropObjects[4].maxhumidity;
                cropMinhumidity = cropObjects[4].minhumidity;
                break;
        }
    }
    public void Sprout(SeedType seedType)
    {
        foreach (var obj in cropObjects)
        {
            obj.seedObject.SetActive(false);
        }
        switch (seedType)
        {
            case SeedType.Broccoli:
                cropObjects[0].sproutObject.SetActive(true);
                break;
            case SeedType.Carrot:
                cropObjects[1].sproutObject.SetActive(true);
                break;
            case SeedType.Cauiliflower:
                cropObjects[2].sproutObject.SetActive(true);
                break;
            case SeedType.Corn:
                cropObjects[3].sproutObject.SetActive(true);
                break;
            case SeedType.Mashroom:
                cropObjects[4].sproutObject.SetActive(true);
                break;
        }
    }
    public void Mature(SeedType seedType)
    {
        foreach (var obj in cropObjects)
        {
            obj.sproutObject.SetActive(false);
        }
        switch (seedType)
        {
            case SeedType.Broccoli:
                cropObjects[0].matureObject.SetActive(true);
                break;
            case SeedType.Carrot:
                cropObjects[1].matureObject.SetActive(true);
                break;
            case SeedType.Cauiliflower:
                cropObjects[2].matureObject.SetActive(true);
                break;
            case SeedType.Corn:
                cropObjects[3].matureObject.SetActive(true);
                break;
            case SeedType.Mashroom:
                cropObjects[4].matureObject.SetActive(true);
                break;
        }
    }

    public void FarmReset()
    {
        Plan();
        growthStats = GrowthStats.Seed;
        foreach (var corn in cropObjects)
        {
            corn.matureObject.SetActive(false);
        }
        SeedFarmAdd();
        growCurrentTime = 0;
    }

    public void SeedFarmAdd()
    {
        switch (seedType)
        {
            case SeedType.Broccoli:
                if (tile.autoGet)
                {
                    GameManager.Instance.farm1+=2;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.4)
                    {
                        GameManager.Instance.seed1++;
                    }
                } 
                else if (!GameManager.Instance.nomalGet)
                {
                    GameManager.Instance.farm1++;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.2)
                    {
                        GameManager.Instance.seed1++;
                    }
                }
                else
                {
                    int count = UnityEngine.Random.Range(1, 4);
                    GameManager.Instance.farm1 += count;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.5)
                    {
                        GameManager.Instance.seed1++;
                    }
                }
                break;
            case SeedType.Carrot:
                if (tile.autoGet)
                {
                    GameManager.Instance.farm2 += 2;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.4)
                    {
                        GameManager.Instance.seed2++;
                    }
                }
                else if (!GameManager.Instance.nomalGet)
                {
                    GameManager.Instance.farm2++;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.2)
                    {
                        GameManager.Instance.seed2++;
                    }
                }
                else
                {
                    int count = UnityEngine.Random.Range(1, 4);
                    GameManager.Instance.farm2 += count;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.5)
                    {
                        GameManager.Instance.seed2++;
                    }
                }
                break;
            case SeedType.Cauiliflower:
                if (tile.autoGet)
                {
                    GameManager.Instance.farm3 += 2;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.4)
                    {
                        GameManager.Instance.seed3++;
                    }
                }
                else if (!GameManager.Instance.nomalGet)
                {
                    GameManager.Instance.farm3++;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.2)
                    {
                        GameManager.Instance.seed3++;
                    }
                }
                else
                {
                    int count = UnityEngine.Random.Range(1, 4);
                    GameManager.Instance.farm3 += count;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.5)
                    {
                        GameManager.Instance.seed3++;
                    }
                }
                break;
            case SeedType.Corn:
                if (tile.autoGet)
                {
                    GameManager.Instance.farm4 += 2;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.4)
                    {
                        GameManager.Instance.seed4++;
                    }
                }
                else if (!GameManager.Instance.nomalGet)
                {
                    GameManager.Instance.farm4++;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.2)
                    {
                        GameManager.Instance.seed4++;
                    }
                }
                else
                {
                    int count = UnityEngine.Random.Range(1, 4);
                    GameManager.Instance.farm4 += count;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.5)
                    {
                        GameManager.Instance.seed4++;
                    }
                }
                break;
            case SeedType.Mashroom:
                if (tile.autoGet)
                {
                    GameManager.Instance.farm5 += 2;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.4)
                    {
                        GameManager.Instance.seed5++;
                    }
                }
                else if (!GameManager.Instance.nomalGet)
                {
                    GameManager.Instance.farm5++;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.2)
                    {
                        GameManager.Instance.seed5++;
                    }
                }
                else
                {
                    int count = UnityEngine.Random.Range(1, 4);
                    GameManager.Instance.farm5 += count;
                    float index = UnityEngine.Random.value;
                    if (index <= 0.5)
                    {
                        GameManager.Instance.seed5++;
                    }
                }
                break;

        }
    }

    public void StatsOpen()
    {
        GameManager.Instance.farmStatsUI.Show(this);
    }

}
