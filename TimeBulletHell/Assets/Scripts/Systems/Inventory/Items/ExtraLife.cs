using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : InventoryItem
{
    private bool used;

    public ExtraLife()
    {
        this.used = false;
    }

    public bool shouldDiscard()
    {
        return used;
    }

    public void update()
    {
        
    }

    public void use()
    {
        EventManager.instance.extraLife(1);
        this.used = true;
    }

    public bool usedContinuously()
    {
        return false;
    }
}
