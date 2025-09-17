using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoxSlot : MonoBehaviour
{
    public Text nameText;
    public Text priceText;
    public Text allCountText;
    public Text countText;

    public int count = 1;
    public int price = 0;
    public int orizenPrice = 0;
    public string itemName;
    public bool sell;


    private void Update()
    {
        countText.text = count.ToString();
        priceText.text = $"ÆÇ¸Å°¡°Ý : {price}¿ø";
        nameText.text = itemName.ToString();
        if (!sell)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
        }
        switch (itemName)
        {
            case "ºê·ÎÄÝ¸® ¾¾¾Ñ":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.seed1}°³";
                break;
            case "´ç±Ù ¾¾¾Ñ":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.seed2}°³";
                break;
            case "¾ç¹èÃß ¾¾¾Ñ":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.seed3}°³";
                break;
            case "¿Á¼ö¼ö ¾¾¾Ñ":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.seed4}°³";
                break;
            case "¹ö¼¸ ¾¾¾Ñ":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.seed5}°³";
                break;
            case "ºñ´ÒÇÏ¿ì½º":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.greenHouseCount}°³";
                break;
            case "ÀÚµ¿ ¼öÈ®±â":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.autoGetCount}°³";
                break;
            case "Æ÷È¹±â":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.animalGetCount}°³";
                break;
            case "³ó»ç¿ë ÀÏ¹Ý µµ±¸":
            case "³ó»ç¿ë °í±Þ µµ±¸":
            case "ÀÏ¹Ý ¹°»Ñ¸®°³":
            case "°í±Þ ¹°»Ñ¸®°³":
                    allCountText.text = $"ÃÑ °¹¼ö : 1°³";
                break;
            case "ºê·ÎÄÝ¸®":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.farm1}°³";
                priceText.text = $"ÆÇ¸Å °¡°Ý : {GameManager.Instance.farmPrices[0].price}";
                break;
            case "´ç±Ù":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.farm2}°³";
                priceText.text = $"ÆÇ¸Å °¡°Ý : {GameManager.Instance.farmPrices[1].price}";
                break;
            case "¾ç¹èÃß":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.farm3}°³";
                priceText.text = $"ÆÇ¸Å °¡°Ý : {GameManager.Instance.farmPrices[2].price}";
                break;
            case "¿Á¼ö¼ö":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.farm4}°³";
                priceText.text = $"ÆÇ¸Å °¡°Ý : {GameManager.Instance.farmPrices[3].price}";
                break;
            case "¹ö¼¸":
                allCountText.text = $"ÃÑ °¹¼ö : {GameManager.Instance.farm5}°³";
                priceText.text = $"ÆÇ¸Å °¡°Ý : {GameManager.Instance.farmPrices[4].price}";
                break;
        }
    }


    public void Plus()
    {
        count++;
        price += orizenPrice;
    }

    public void Miuse()
    {
        if (count <= 1) return;
        count--;
        price -= orizenPrice;
    }

    public void Sell()
    {
        switch (itemName)
        {
            case "ºê·ÎÄÝ¸®":
                if (GameManager.Instance.farm1 >= count)
                {
                    GameManager.Instance.farm1 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[0].price * count;
                }
                else 
                {
                    GameManager.Instance.messageUI.add("ºê·ÎÄÝ¸®°¡ ºÎÁ·ÇÕ´Ï´Ù", Color.red, true);
                }
                break;
            case "´ç±Ù":
                if (GameManager.Instance.farm2 >= count)
                {
                    GameManager.Instance.farm2 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[1].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("´ç±ÙÀÌ ºÎÁ·ÇÕ´Ï´Ù", Color.red, true);
                }
                break;
            case "¾ç¹èÃß":
                if (GameManager.Instance.farm3 >= count)
                {
                    GameManager.Instance.farm3 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[2].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("¾ç¹èÃß°¡ ºÎÁ·ÇÕ´Ï´Ù", Color.red, true);
                }
                break;
            case "¿Á¼ö¼ö":
                if (GameManager.Instance.farm4 >= count)
                {
                    GameManager.Instance.farm4 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[3].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("¿Á¼ö¼ö°¡ ºÎÁ·ÇÕ´Ï´Ù", Color.red, true);
                }
                break;
            case "¹ö¼¸":
                if (GameManager.Instance.farm5 >= count)
                {
                    GameManager.Instance.farm5 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[3].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("¹ö¼¸ÀÌ ºÎÁ·ÇÕ´Ï´Ù", Color.red, true);
                }
                break;
            
        }
    }
}
