using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public GameObject messageSlot;
    public Transform panel;



    public void add(string text)
    {
        var slot = Instantiate(messageSlot);
        slot.transform.SetParent(panel);
        MessageSlot MSlot = slot.GetComponent<MessageSlot>();
        Text text1 = MSlot.Set();
        text1.text = text;
    }
    public void add(string text, Color color)
    {
        var slot = Instantiate(messageSlot);
        slot.transform.SetParent(panel);
        MessageSlot MSlot = slot.GetComponent<MessageSlot>();
        Text text1 = MSlot.Set();
        text1.text = text;
        text1.color = color;
    }

    public void add(string text, Color color, bool bold)
    {
        var slot = Instantiate(messageSlot);
        slot.transform.SetParent(panel);
        MessageSlot MSlot = slot.GetComponent<MessageSlot>();
        Text text1 = MSlot.Set();
        text1.text = text;
        text1.color = color;
        if (bold)
        {
            text1.fontStyle = FontStyle.Bold;
        }
    }
}
