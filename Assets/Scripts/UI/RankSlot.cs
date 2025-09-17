using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSlot : MonoBehaviour
{
    public Text numberText;
    public Text nameText;
    public Text timeText;

    public void Set(int index, string name, float time)
    {
        numberText.text = $"{index}.";
        nameText.text = name;
        timeText.text = $"{(int)(time/3600):D2}:{(int)((time % 3600)/60):D2}:{(int)((time % 3600) % 60):D2}";
    }
}
