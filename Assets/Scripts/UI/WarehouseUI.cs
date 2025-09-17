using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ShopItem
{
    public string Name;
    public int price;
    public bool stack;
}

[System.Serializable]
public struct BoxItem
{
    public string Name;
    public bool sell;
}

public class WarehouseUI : MonoBehaviour
{
    public GameObject shopSlot;
    public Transform shopGrid;
    public ShopItem[] shopItems;
    public GameObject boxSlot;
    public Transform boxGrid;
    public BoxItem[] boxItems;

    private List<GameObject> slots = new List<GameObject>();



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Show();
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        foreach (var shopItem in shopItems)
        {
            var slot = Instantiate(shopSlot);
            slot.transform.SetParent(shopGrid);
            if(slot.TryGetComponent<ShopSlot>(out var shopslot))
            {
                shopslot.itemName = shopItem.Name;
                shopslot.orizenPrice = shopItem.price;
                shopslot.stack = shopItem.stack;
                shopslot.price = shopItem.price;
            }
            slots.Add(slot);
        }
        foreach (var boxItem in boxItems)
        {
            var slot = Instantiate (boxSlot);
            slot.transform.SetParent(boxGrid);
            if(slot.TryGetComponent<BoxSlot>(out var boxslot))
            {
                boxslot.itemName = boxItem.Name;
                boxslot.sell = boxItem.sell;
                if(boxslot.itemName == "농사용 일반 도구")
                {
                    if (!GameManager.Instance.nomalGet)
                    {
                        boxslot.gameObject.SetActive(false);
                    }
                }else if (boxslot.itemName == "농사용 고급 도구")
                {
                    if (GameManager.Instance.nomalGet)
                    {
                        boxslot.gameObject.SetActive(false);
                    }
                }
                else if (boxslot.itemName == "일반 물뿌리개")
                {
                    if (!GameManager.Instance.nomalWatergun)
                    {
                        boxslot.gameObject.SetActive(false);
                    }
                }
                else if (boxslot.itemName == "고급 물뿌리개")
                {
                    if (GameManager.Instance.nomalWatergun)
                    {
                        boxslot.gameObject.SetActive(false);
                    }
                }
            }
            slots.Add(slot);
        }
    }

    public void Hide()
    {
        if(slots.Count > 0)
        {
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                Destroy(slots[i]);
            }
            slots.Clear();
        }
        gameObject.SetActive(false);
    }

}
