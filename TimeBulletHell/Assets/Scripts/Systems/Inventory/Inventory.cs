﻿using System.Collections;
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

        EventManager.instance.onPickupCollectedEvent += this.onPickupCollected;
        EventManager.instance.addToInventoryEvent += this.addToInventory;
    }

    private void onPickupCollected(APickup ap)
    {
        if (this.nextOpen() < this.inventoryCapacity)
        {
            ap.addToInventory();
        }
    }

    private void addToInventory(InventoryItem ii)
    {
        if (this.nextOpen() < this.inventoryCapacity)
        {
            this.inventory[this.nextOpen()] = ii;
        }
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
        int prevIndex = this.selectedIndex;
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
        if (this.inventory[this.selectedIndex] == null)
        {
            if (this.inventory[prevIndex] != null)
            {
                this.selectedIndex = prevIndex;
            } else
            {
                this.selectedIndex = 0;
            }
        }
    }

    private int nextOpen()
    {
        int i;
        for(i = 0; i < this.inventoryCapacity && this.inventory[i] != null; i++) { }
        return i;
    }
}
