using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoinsForUI : AGetTextForUI
{
    public override string getText()
    {
        return PlayerInfo.instance.getCoins().ToString();
    }
}