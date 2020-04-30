using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private GameObject prefab;
    private Queue<Poolable> poolables;
    private List<Poolable> activePoolables;
    private Transform poolableParent;

    public Pool(GameObject prefab, int poolCount, Transform poolableParent)
    {
        this.prefab = prefab;
        this.poolables = new Queue<Poolable>();
        this.activePoolables = new List<Poolable>();
        this.poolableParent = poolableParent;
        for(int i = 0; i < poolCount; i++)
        {
            this.enqueue(this.clone());
        }
    }

    private Poolable clone()
    {
        GameObject g = Object.Instantiate(this.prefab);
        g.transform.SetParent(poolableParent);
        return g.GetComponent<Poolable>();
    }

    private void enqueue(Poolable p)
    {
        this.initPoolable(p);
        this.poolables.Enqueue(p);
    }

    private void initPoolable(Poolable p)
    {
        p.gameObject.SetActive(false);
    }

    //Dequeue, make active, and return a Poolable 
    public Poolable pick()
    {
        Poolable p;
        if (this.poolables.Count > 0)
        {
            p = this.poolables.Dequeue();
        } else
        {
            p = this.clone();
            this.initPoolable(p);
        }
        p.gameObject.SetActive(true);
        this.activePoolables.Add(p);
        return p;
    }

    public void recallPoolables()
    {
        for(int i = 0; i < this.activePoolables.Count; i++)
        {
            Poolable p = this.activePoolables[i];
            if (!p.gameObject.activeInHierarchy)
            {
                this.activePoolables.RemoveAt(i);
                i--;
                this.enqueue(p);
            }
        }
    }
}
