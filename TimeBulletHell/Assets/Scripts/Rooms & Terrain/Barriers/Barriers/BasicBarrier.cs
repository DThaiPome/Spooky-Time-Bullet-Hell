using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBarrier : ABarrier
{
    public override void removeBarrier()
    {
        this.gameObject.SetActive(false);
    }

    public override void setBarrier()
    {
        this.gameObject.SetActive(true);
    }
}
