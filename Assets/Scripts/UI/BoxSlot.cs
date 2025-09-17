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
        priceText.text = $"�ǸŰ��� : {price}��";
        nameText.text = itemName.ToString();
        if (!sell)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
        }
        switch (itemName)
        {
            case "����ݸ� ����":
                allCountText.text = $"�� ���� : {GameManager.Instance.seed1}��";
                break;
            case "��� ����":
                allCountText.text = $"�� ���� : {GameManager.Instance.seed2}��";
                break;
            case "����� ����":
                allCountText.text = $"�� ���� : {GameManager.Instance.seed3}��";
                break;
            case "������ ����":
                allCountText.text = $"�� ���� : {GameManager.Instance.seed4}��";
                break;
            case "���� ����":
                allCountText.text = $"�� ���� : {GameManager.Instance.seed5}��";
                break;
            case "����Ͽ콺":
                allCountText.text = $"�� ���� : {GameManager.Instance.greenHouseCount}��";
                break;
            case "�ڵ� ��Ȯ��":
                allCountText.text = $"�� ���� : {GameManager.Instance.autoGetCount}��";
                break;
            case "��ȹ��":
                allCountText.text = $"�� ���� : {GameManager.Instance.animalGetCount}��";
                break;
            case "���� �Ϲ� ����":
            case "���� ��� ����":
            case "�Ϲ� ���Ѹ���":
            case "��� ���Ѹ���":
                    allCountText.text = $"�� ���� : 1��";
                break;
            case "����ݸ�":
                allCountText.text = $"�� ���� : {GameManager.Instance.farm1}��";
                priceText.text = $"�Ǹ� ���� : {GameManager.Instance.farmPrices[0].price}";
                break;
            case "���":
                allCountText.text = $"�� ���� : {GameManager.Instance.farm2}��";
                priceText.text = $"�Ǹ� ���� : {GameManager.Instance.farmPrices[1].price}";
                break;
            case "�����":
                allCountText.text = $"�� ���� : {GameManager.Instance.farm3}��";
                priceText.text = $"�Ǹ� ���� : {GameManager.Instance.farmPrices[2].price}";
                break;
            case "������":
                allCountText.text = $"�� ���� : {GameManager.Instance.farm4}��";
                priceText.text = $"�Ǹ� ���� : {GameManager.Instance.farmPrices[3].price}";
                break;
            case "����":
                allCountText.text = $"�� ���� : {GameManager.Instance.farm5}��";
                priceText.text = $"�Ǹ� ���� : {GameManager.Instance.farmPrices[4].price}";
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
            case "����ݸ�":
                if (GameManager.Instance.farm1 >= count)
                {
                    GameManager.Instance.farm1 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[0].price * count;
                }
                else 
                {
                    GameManager.Instance.messageUI.add("����ݸ��� �����մϴ�", Color.red, true);
                }
                break;
            case "���":
                if (GameManager.Instance.farm2 >= count)
                {
                    GameManager.Instance.farm2 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[1].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("����� �����մϴ�", Color.red, true);
                }
                break;
            case "�����":
                if (GameManager.Instance.farm3 >= count)
                {
                    GameManager.Instance.farm3 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[2].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("����߰� �����մϴ�", Color.red, true);
                }
                break;
            case "������":
                if (GameManager.Instance.farm4 >= count)
                {
                    GameManager.Instance.farm4 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[3].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("�������� �����մϴ�", Color.red, true);
                }
                break;
            case "����":
                if (GameManager.Instance.farm5 >= count)
                {
                    GameManager.Instance.farm5 -= count;
                    GameManager.Instance.money += GameManager.Instance.farmPrices[3].price * count;
                }
                else
                {
                    GameManager.Instance.messageUI.add("������ �����մϴ�", Color.red, true);
                }
                break;
            
        }
    }
}
