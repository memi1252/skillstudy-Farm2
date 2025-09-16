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
        priceText.text = $"{price}��";
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
                case "����ݸ� ����":
                    GameManager.Instance.seed1 += count;
                    break;
                case "��� ����":
                    GameManager.Instance.seed2 += count;
                    break;
                case "����� ����":
                    GameManager.Instance.seed3 += count;
                    break;
                case "������ ����":
                    GameManager.Instance.seed4 += count;
                    break;
                case "���� ����":
                    GameManager.Instance.seed5 += count;
                    break;
                case "����Ͽ콺":
                    GameManager.Instance.greenHouseCount += count;
                    break;
                case "�ڵ� ��Ȯ��":
                    GameManager.Instance.autoGetCount += count;
                    break;
                case "��ȹ��":
                    GameManager.Instance.animalGetCount += count;
                    break;
                case "���� ��� ����":
                    GameManager.Instance.nomalGet = false;
                    break;
                case "��� ���Ѹ���":
                    GameManager.Instance.nomalWatergun = false;
                    break;
            }
        }
        else
        {
            GameManager.Instance.messageUI.add("���� �����մϴ�", Color.red, true);
        }
    }
}
