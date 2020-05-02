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
            float xPos = Random.Range(-8.0f, 8.0f);
            MobManager.instance.spawn("BAT", new DefaultSpawnProperties(new Vector2(xPos, 3.5f)));
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
