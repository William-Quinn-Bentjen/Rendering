using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public string itemName;
    public float price;
    public Image icon;
    public void Set(float newPrice, string newItemName = null, Image newIcon = null)
    {
        price = newPrice;
        itemName = newItemName;
        icon = newIcon;
    }
}
