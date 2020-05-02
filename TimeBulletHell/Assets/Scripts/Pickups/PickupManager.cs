using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabs;
    [SerializeField]
    int poolCounts;

    PoolMap pools;

    public static PickupManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.pools = new PoolMap(this.prefabs, this.poolCounts, this.transform); 
    }

    // Update is called once per frame
    void Update()
    {
        this.pools.recallBullets();  
    }

    public void spawn(string pickupName, IPickupSpawnProperties spawnproperties)
    {
        APickup ap;
        if (this.pools.tryGetComponent<APickup>(pickupName, out ap))
        {
            if (ap != null)
            {
                spawnproperties.spawn(ap);
            }
        }
    }
}
