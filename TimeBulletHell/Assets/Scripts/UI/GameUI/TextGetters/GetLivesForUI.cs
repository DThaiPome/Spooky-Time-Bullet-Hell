﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLivesForUI : AGetTextForUI
{
    public override string getText()
    {
        return PlayerInfo.instance.getLives().ToString();
    }
}
