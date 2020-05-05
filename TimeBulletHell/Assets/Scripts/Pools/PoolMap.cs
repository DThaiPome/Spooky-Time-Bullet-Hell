using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMap
{
    private Dictionary<string, Pool> pools;

    public PoolMap(List<GameObject> prefabs, int poolCounts, Transform t)
    {
        this.pools = new Dictionary<string, Pool>();
        foreach (GameObject g in prefabs)
        {
            this.pools.Add(g.name, new Pool(g, poolCounts, t));
        }
    }

    public void recallBullets()
    {
        Dictionary<string, Pool>.Enumerator enumerator = this.pools.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (this.pools.TryGetValue(enumerator.Current.Key, out Pool p))
            {
                p.recallPoolables();
            }
        }
    }

    public bool tryGetValue(string key, out Pool p)
    {
        return this.pools.TryGetValue(key, out p);
    }

    public bool tryGetComponent<T>(string key, out T t, out GameObject g)
    {
        Pool p;
        if (this.tryGetValue(key, out p))
        {
            g = p.pick().gameObject;
            t = g.GetComponent<T>();
            return true;
        } else
        {
            g = null;
            t = default(T);
            Debug.Log(key + " does not exist!");
            return false;
        }
    }
}
