using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABarrier : MonoBehaviour
{
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

    }

    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {

    }

    void OnDestroy()
    {
        this.onDestroy();
    }

    protected virtual void onDestroy()
    {

    }

    public abstract void setBarrier();

    public abstract void removeBarrier();
}
