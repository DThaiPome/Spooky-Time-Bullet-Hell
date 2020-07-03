using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int inventoryCapacity;
    [SerializeField]
    private int selectedIndex;

    private InventoryItem[] inventory;

    private bool inventoryActive;

    private InputAxis fire;
    private List<InputAxis> inventorySlots;

    void Start()
    {
        this.inventoryCapacity = this.inventoryCapacity > 1 ? this.inventoryCapacity : 1;
        this.selectedIndex = 0;
        this.inventory = new InventoryItem[this.inventoryCapacity];
        this.inventory[0] = new DefaultGun();

        this.inventoryActive = SceneManager.GetActiveScene().name != "AlphaLevelSelect";

        EventManager.instance.onInventoryItemCollectedEvent += this.onPickupCollected;
        EventManager.instance.addToInventoryEvent += this.addToInventory;
        EventManager.instance.onLevelSwitchedEvent += this.levelSwitched;
        EventManager.instance.queryInventoryAvailabilityEvent += this.queryInventoryAvailability;
        EventManager.instance.allLivesLostEvent += this.onAllLivesLost;

        this.fire = GameInput.input.getAxis("Fire");
        this.inventorySlots = new List<InputAxis>();
        this.inventorySlots.Insert(0, GameInput.input.getAxis("DefaultInventorySlot"));
        for(int i = 1; i < this.inventoryCapacity; i++)
        {
            this.inventorySlots.Insert(i, GameInput.input.getAxis("InventorySlot" + i));
        }
    }

    private void levelSwitched(string level)
    {
        this.inventoryActive = level != "levelSelect";
    }

    private void onPickupCollected(InventoryPickup ap)
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

    private void queryInventoryAvailability(System.Object o)
    {
        if (this.nextOpen() < this.inventoryCapacity)
        {
            EventManager.instance.returnInventoryNotFull(o);
        } else
        {
            EventManager.instance.returnInventoryFull(o);
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
            if (ii != null && this.inventoryActive)
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
        if (this.inventoryActive && ii != null)
        {
            if (ii.usedContinuously() && this.fire.positive())
            {
                ii.use();
            } else if (!ii.usedContinuously() && this.fire.positiveDown())
            {
                ii.use();
            }
        }
    }

    private void updateSelectedIndex()
    {
        int prevIndex = this.selectedIndex;
        for(int i = 0; i < this.inventoryCapacity; i++)
        {
            InputAxis slotInput = this.inventorySlots[i];
            if (slotInput.positiveDown())
            {
                this.selectedIndex = i;
                break;
            }
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

    private void onAllLivesLost()
    {
        this.inventory = new InventoryItem[this.inventoryCapacity];
        this.inventory[0] = new DefaultGun();
    }
}
