using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCatalog : ALevelSelectWindowContents
{
    [SerializeField]
    private List<AShopItem> items;
    [SerializeField]
    private GameObject shopEntryPrefab;
    private List<ShopEntry> entries;

    void Awake()
    {
        this.entries = new List<ShopEntry>();
        this.initEntries();
    }

    void Start()
    {
        
    }

    private void initEntries()
    {
        foreach(AShopItem item in this.items) {
            GameObject g = Object.Instantiate(shopEntryPrefab);
            ShopEntry se = g.GetComponent<ShopEntry>();
            se.transform.SetParent(this.transform);
            se.setItem(item);
            this.entries.Add(se);
        }
    }

    public override void hideContents()
    {
        foreach(ShopEntry entry in this.entries)
        {
            entry.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public override void showContents()
    {
        foreach(ShopEntry entry in this.entries)
        {
            entry.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);
    }
}
