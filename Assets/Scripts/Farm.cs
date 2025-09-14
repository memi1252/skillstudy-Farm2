using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmStats
{
    Plan,
    Field
}

public enum growthStage
{
    Seed,
    Sprout,
    Mature
}

public enum seedType
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
}

public class Farm : MonoBehaviour
{
    public FarmStats farmStats;
    public growthStage growthStage;
    public seedType seedType;
    public Mesh planMesh;
    public Mesh fieldMesh;
    
    public CropObject[] cropObjects;
    
    private MeshFilter meshFilter;
    
    private void Awake()
    {
	    farmStats = FarmStats.Plan;
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (farmStats == FarmStats.Plan)
            {
                Feild();
            }else if (farmStats == FarmStats.Field)
            {
                GrowthStage(growthStage.Seed);
            }
            
        }
    }

    public void Feild()
    {
        meshFilter.mesh = fieldMesh;
        farmStats = FarmStats.Field;
    }

    public void GrowthStage(growthStage stage)
    {
        growthStage = stage;
        switch (stage)
        {
            case growthStage.Seed:
                if (seedType == seedType.Broccoli)
                {
                    cropObjects[0].seedObject.SetActive(true);
                    cropObjects[1].seedObject.SetActive(false);
                    cropObjects[2].seedObject.SetActive(false);
                }
                break;
            case growthStage.Sprout:
                if (seedType == seedType.Broccoli)
                {
                    cropObjects[0].seedObject.SetActive(false);
                    cropObjects[1].seedObject.SetActive(true);
                    cropObjects[2].seedObject.SetActive(false);
                }
                break;
            case growthStage.Mature:
                if (seedType == seedType.Broccoli)
                {
                    cropObjects[0].seedObject.SetActive(false);
                    cropObjects[1].seedObject.SetActive(false);
                    cropObjects[2].seedObject.SetActive(true);
                }
                break;
        }
    }
}
