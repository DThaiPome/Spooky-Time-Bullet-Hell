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

    private float timeElapsed;
    private float timeToSpawn = 2;
    private float count = 2;

    void Update()
    {
        this.pools.recallBullets();
        this.timeElapsed += GameTime.instance.deltaTime();
        while (this.timeElapsed >= this.timeToSpawn)
        {
            this.timeElapsed -= this.timeToSpawn;
            for (int i = 0; i < this.count; i++)
            {
                float xPos = Random.Range(-8.0f, 8.0f);
                float yPos = Random.Range(-3.5f, 3.5f);
                MobManager.instance.spawn("BAT", new DefaultSpawnProperties(new Vector2(xPos, yPos)));
            }
        }
    }

    public void spawn(string mobName, IMobSpawnProperty spawnProperties)
    {
        MobBehaviour mb;
        if (this.pools.tryGetComponent<MobBehaviour>(mobName, out mb))
        {
            if (mb != null)
            {
                spawnProperties.spawn(mb);
            }
        }
    }
}
