using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public const int numberOfItem = 3;
    public Image[] itemImages = new Image[numberOfItem];
    public Text[] detail = new Text[numberOfItem];
    public Item newItem;
    public Item newItem2;
    private void Start()
    {
        addItem(newItem);
        addItem(newItem);
        addItem(newItem2);
        addItem(newItem2);
    }


    public void addItem(Item newItem) {
        for (int i = 0; i < numberOfItem; i ++) {
            if (itemImages[i].sprite == newItem.sprite)
            {
                
                detail[i].text = (int.Parse(detail[i].text)+1).ToString();
                break;
            }
            /*else if (Items[i] == null) {
                Items[i] = newItem;
                itemImages[i].sprite = newItem.sprite;
                itemImages[i].enabled = true;
                detail[i].text = (int.Parse(detail[i].text) + 1).ToString();
                return;
            }*/
        }
    }
    public void RemoveItem(Sprite oldItem) {
        for (int i = 0; i < numberOfItem; i++) {
            int count = int.Parse(detail[i].text);
            if (itemImages[i].sprite == oldItem && count >= 1)
            {

                count -= 1;
                detail[i].text = count.ToString();
                
                break;
            }
        }
    }
    public bool detectEmptyPotion(Sprite potionType) {
        for (int i = 0; i < numberOfItem; i++)
        {
            if (itemImages[i].sprite == potionType)
            {
                if (detail[i].text == "0")
                {
                    return true;
                }

            }
        }
        return false;
    }


    
    
}
