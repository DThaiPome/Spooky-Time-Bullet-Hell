using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem
{
    //Use the item
    void use();

    //Should the item be discarded?
    bool shouldDiscard();

    //Call every Update
    void update();

    //Is this item able to be used continuously? Or once per press?
    bool usedContinuously();
}
