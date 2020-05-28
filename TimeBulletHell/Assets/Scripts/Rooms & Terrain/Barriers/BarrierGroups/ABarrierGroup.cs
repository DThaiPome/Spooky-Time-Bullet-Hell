using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABarrierGroup : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> barrierObjects;
    [SerializeField]
    protected bool active;

    protected List<ABarrier> barriers;

    void Awake()
    {
        this.awake();
    }

    protected virtual void awake()
    {
        
    }

    void Start()
    {
        this.start();
    }

    protected virtual void start()
    {
        this.barriers = new List<ABarrier>();
        foreach (GameObject g in this.barrierObjects)
        {
            ABarrier ab = g.GetComponent<ABarrier>();
            this.barriers.Add(ab);
        }
        this.matchBarriersToActive();
    }

    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {
        this.matchBarriersToActive();
    }

    void OnDestroy()
    {
        this.onDestroy();
    }

    protected virtual void onDestroy()
    {

    }

    private void matchBarriersToActive()
    {
        if (this.active)
        {
            this.setBarriers();
        } else
        {
            this.removeBarriers();
        }
    }

    public void setBarriers()
    {
        foreach(ABarrier ab in this.barriers)
        {
            ab.setBarrier();
        }
        this.active = true;
    }

    public void removeBarriers()
    {
        foreach(ABarrier ab in this.barriers)
        {
            ab.removeBarrier();
        }
        this.active = false;
    }
}
