using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Text nameText;
    public Text priceText;
    public Text countText;

    public int count = 1;
    public int price = 0;
    public int orizenPrice = 0;
    public string itemName;
    public bool stack;


    private void Update()
    {
        countText.text = count.ToString();
        priceText.text = $"{price}¿ø";
        nameText.text = itemName.ToString();
    }


    public void Plus()
    {
        if (!stack) return;
        count++;
        price += orizenPrice;
    }

    public void Miuse()
    {
        if (!stack) return;
        if(count <= 0) return;
        count--;
        price -= orizenPrice;
    }

    public void Buy()
    {
        
        if(GameManager.Instance.money >= price)
        {
            GameManager.Instance.money -= price;
            switch (itemName)
            {
                case "ºê·ÎÄÝ¸® ¾¾¾Ñ":
                    GameManager.Instance.seed1 += count;
                    break;
                case "´ç±Ù ¾¾¾Ñ":
                    GameManager.Instance.seed2 += count;
                    break;
                case "¾ç¹èÃß ¾¾¾Ñ":
                    GameManager.Instance.seed3 += count;
                    break;
                case "¿Á¼ö¼ö ¾¾¾Ñ":
                    GameManager.Instance.seed4 += count;
                    break;
                case "¹ö¼¸ ¾¾¾Ñ":
                    GameManager.Instance.seed5 += count;
                    break;
                case "ºñ´ÒÇÏ¿ì½º":
                    GameManager.Instance.greenHouseCount += count;
                    break;
                case "ÀÚµ¿ ¼öÈ®±â":
                    GameManager.Instance.autoGetCount += count;
                    break;
                case "Æ÷È¹±â":
                    GameManager.Instance.animalGetCount += count;
                    break;
                case "³ó»ç¿ë °í±Þ µµ±¸":
                    GameManager.Instance.nomalGet = false;
                    break;
                case "°í±Þ ¹°»Ñ¸®°³":
                    GameManager.Instance.nomalWatergun = false;
                    break;
            }
        }
        else
        {
            GameManager.Instance.messageUI.add("µ·ÀÌ ºÎÁ·ÇÕ´Ï´Ù", Color.red, true);
        }
    }
}
