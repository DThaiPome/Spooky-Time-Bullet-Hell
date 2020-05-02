using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int inventoryCapacity;
    [SerializeField]
    private int selectedIndex;

    private InventoryItem[] inventory;

    void Start()
    {
        this.inventoryCapacity = this.inventoryCapacity > 1 ? this.inventoryCapacity : 1;
        this.selectedIndex = 0;
        this.inventory = new InventoryItem[this.inventoryCapacity];
        this.inventory[0] = new DefaultGun();
    }

    // Update is called once per frame
    void Update()
    {
        this.updateSelectedIndex();
        this.updateItems();
        this.useSelectedItem();
        this.discardItems();
    }

    private void updateItems()
    {
        foreach(InventoryItem ii in this.inventory)
        {
            if (ii != null)
            {
                ii.update();
            }
        }
    }

    private void discardItems()
    {
        for (int i = 0; i < this.inventoryCapacity; i++)
        {
            InventoryItem ii = this.inventory[i];
            if (ii != null)
            {
                if (ii.shouldDiscard())
                {
                    this.inventory[i] = null;
                    this.selectedIndex = 0;
                }
            }
        }
    }

    private void useSelectedItem()
    {
        InventoryItem ii = this.inventory[this.selectedIndex];
        if (ii != null)
        {
            if (ii.usedContinuously() && Input.GetKey(KeyCode.Space))
            {
                ii.use();
            } else if (!ii.usedContinuously() && Input.GetKeyDown(KeyCode.Space))
            {
                ii.use();
            }
        }
    }

    private void updateSelectedIndex()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.selectedIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            this.selectedIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            this.selectedIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            this.selectedIndex = 3;
        }
    }
}
