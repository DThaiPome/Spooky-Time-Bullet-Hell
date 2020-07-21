using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDisabledBarrierGroup : ABarrierGroup
{
    [SerializeField]
    private List<GameObject> objects;

    protected override void update()
    {
        base.update();
        
        foreach(GameObject g in this.objects)
        {
            if (g.activeSelf)
            {
                return;
            }
        }

        this.removeBarriers();
    }
}
