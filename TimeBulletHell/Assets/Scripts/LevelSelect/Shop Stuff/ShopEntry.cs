using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEntry : MonoBehaviour
{
    private AShopItem item;

    private Image icon;
    private Text itemName;
    private Text priceText;

    private bool available = true;

    void Start()
    {
        this.initFields();
    }

    private void initFields()
    {
        this.icon = this.transform.Find("Icon").gameObject.GetComponent<Image>();
        this.itemName = this.transform.Find("Name").gameObject.GetComponent<Text>();
        this.priceText = this.transform.Find("PurchaseButton").Find("Text").gameObject.GetComponent<Text>();
    }

    public void purchase()
    {
        if (this.available)
        {
            this.item.tryToPurchase();
        }
    }

    public void setItem(AShopItem item)
    {
        this.item = item;
        this.initFields();
        this.icon.sprite = this.item.getIcon();
        this.itemName.text = this.item.getName();
        this.priceText.text = this.item.getPrice() + "C";
    }
}
