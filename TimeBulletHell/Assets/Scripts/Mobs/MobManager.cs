using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs;
    [SerializeField]
    private int poolCounts;

    private PoolMap pools;

    public static MobManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.pools = new PoolMap(this.prefabs, this.poolCounts, this.transform);    
    }

    void Update()
    {
        this.pools.recallBullets();
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            float xPos = Random.Range(-8, 8);
            MobManager.instance.spawn("BAT", new DefaultSpawnProperties(new Vector2(xPos, 3.5f)));
        }
    }

    public void spawn(string mobName, IMobSpawnProperty spawnProperties)
    {
        Pool pool;
        if (this.pools.tryGetValue(mobName, out pool))
        {
            Poolable p = pool.pick();
            MobBehaviour mb = p.gameObject.GetComponent<MobBehaviour>();
            if (mb != null)
            {
                spawnProperties.spawn(mb);
            }
        }
        else
        {
            Debug.Log(mobName + " doesn't exist!");
        }
    }
}
