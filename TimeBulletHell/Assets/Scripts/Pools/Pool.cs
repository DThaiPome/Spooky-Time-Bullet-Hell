using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A queue that holds clones of an object. When a clone taken out of the queue
//becomes inactive, the pool retrieves it.
public class Pool
{
    private GameObject prefab;
    private Queue<Poolable> poolables;
    private List<Poolable> activePoolables;
    private Transform poolableParent;

    //Clone the prefab poolCount times, and set its parent to poolableParent
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

    //Instantiates the object
    private Poolable clone()
    {
        GameObject g = Object.Instantiate(this.prefab);
        return g.GetComponent<Poolable>();
    }

    //Pool the object
    private void enqueue(Poolable p)
    {
        this.initPoolable(p);
        p.transform.SetParent(poolableParent);
        this.poolables.Enqueue(p);
    }

    //Make sure the object is deactivated
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
        //p.gameObject.SetActive(true);
        this.activePoolables.Add(p);
        return p;
    }

    //Retrieve any inactive objects from this pool
    public void recallPoolables()
    {
        for(int i = 0; i < this.activePoolables.Count; i++)
        {
            Poolable p = this.activePoolables[i];
            if (!p.gameObject.activeSelf)
            {
                this.activePoolables.RemoveAt(i);
                i--;
                this.enqueue(p);
            }
        }
    }
}
