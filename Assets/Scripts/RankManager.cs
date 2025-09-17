using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

[System.Serializable]
public class RankData
{
    public string Name;
    public float Time;
}

[System.Serializable]
public class RankDataList
{
    public List<RankData> rankDataList = new List<RankData>();
}



public class RankManager : MonoBehaviour
{
    public static RankManager Instance { get; private set; }

    public List<RankData> data = new List<RankData>();
    public string SaveName = "SaveData.json";

    public GameObject rankUI;
    public GameObject rankSlot;
    public Transform rankPenal;
    public List<GameObject> rankSlotList = new List<GameObject>();
    public GameObject rankAddUI;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        RankDataList dataList = new RankDataList();
        dataList.rankDataList = data;
        
        Debug.Log(data);
        Debug.Log(dataList);
        string json = JsonUtility.ToJson(dataList);

#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, SaveName);
#else
        string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, SaveName);
#endif
        File.WriteAllText(path, json);
    }

    public void Load()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, SaveName);
#else
        string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, SaveName);
#endif
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            RankDataList dataList = JsonUtility.FromJson<RankDataList>(json);
            data.Clear();
            data = dataList.rankDataList;
            data =data.OrderBy(x => x.Time).ToList();
        }
        else
        {
            Save();
        }
    }

    public void AddRank(string name, float time)
    {
        RankData data = new RankData();
        data.Name = name;
        data.Time = time;
        this.data.Add(data);
        Save();
    }

    public void RankUIOpen()
    {
        rankUI.SetActive(true);
        if(rankSlotList.Count > 0)
        {
            for (int i = rankSlotList.Count-1;i >= 0;i--)
            {
                Destroy(rankSlotList[i]);
            }
            rankSlotList.Clear();
        }
        int index = 1;
        foreach (var rankData in data)
        {
            if(index >= 6)
            {
                return;
            }
            var slotObj = Instantiate(rankSlot, rankPenal, true);
            if(slotObj.TryGetComponent<RankSlot>(out var slot))
            {
                slot.Set(index, rankData.Name, rankData.Time);
            }
            rankSlotList.Add(slotObj);
            index++;
        }
    }

    public void RankAddUI()
    {
        rankAddUI.SetActive(true);
    }
}
