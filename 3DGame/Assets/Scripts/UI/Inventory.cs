using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int numberOfItem = 12;
    public Image[] itemImages = new Image[numberOfItem];
    public Text[] detail = new Text[numberOfItem];
    public Item[] Items = new Item[numberOfItem];
    public int count;
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
            if (Items[i] == newItem)
            {
                
                detail[i].text = (int.Parse(detail[i].text)+1).ToString();
                break;
            }
            else if (Items[i] == null) {
                Items[i] = newItem;
                itemImages[i].sprite = newItem.sprite;
                itemImages[i].enabled = true;
                detail[i].text = (int.Parse(detail[i].text) + 1).ToString();
                return;
            }
        }
    }
    public void RemoveItem(Item oldItem) {
        for (int i = 0; i < numberOfItem; i++) {
            int count = int.Parse(detail[i].text);
            if (Items[i] == oldItem && count > 1)
            {
                if (count == 2)
                {
                    
                    detail[i].text = "1";
                }
                else
                {
                    count -= 1;
                    detail[i].text = count.ToString();
                }
                break;
            }
            else if (Items[i] == oldItem) {
                Items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                count -= 1;
                detail[i].text = count.ToString();
                return;
            }
        }
    }


    
    
}
