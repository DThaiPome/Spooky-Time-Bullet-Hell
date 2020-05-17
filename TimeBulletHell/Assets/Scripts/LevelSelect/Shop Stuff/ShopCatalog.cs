using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCatalog : MonoBehaviour
{
    [SerializeField]
    private List<AShopItem> items;
    [SerializeField]
    private GameObject shopEntryPrefab;
    private List<ShopEntry> entries;

    void Start()
    {
        this.entries = new List<ShopEntry>();
        this.initEntries();
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
}
