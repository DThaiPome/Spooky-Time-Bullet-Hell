using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs;
    [SerializeField]
    private int poolCounts;

    private Dictionary<string, Pool> pools;

    public static BulletManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EventManager.instance.onWarpedTickEvent += this.onWarpedTick;
        this.pools = new Dictionary<string, Pool>();
        this.initPools();
    }

    void Update()
    {
        this.recallBullets();
    }

    private void recallBullets()
    {
        Dictionary<string, Pool>.Enumerator enumerator = this.pools.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (this.pools.TryGetValue(enumerator.Current.Key, out Pool p)) {
                p.recallPoolables();
            }
        }
    }

    private int direction = 0;

    void onWarpedTick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            BulletManager.instance.fire("BubbleBullet", new BubbleBulletFireProperties(new Vector2(0, 0), direction, 5, 1, "BasicBullet", 10, 4));
            //BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction - 5, 4));
            //BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction - 5, 2));
            //BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction + 5, 4));
            //BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction + 5, 2));
            direction += 25;
            direction %= 360;
        }
    }

    private void initPools()
    {
        foreach(GameObject g in prefabs)
        {
            this.pools.Add(g.name, new Pool(g, this.poolCounts, this.transform));
        }
    }

    public void fire(string bulletName, IFireProperties fireProperties)
    {
        Pool pool;
        if (this.pools.TryGetValue(bulletName, out pool))
        {
            Poolable p = pool.pick();
            BulletBehaviour bb = p.gameObject.GetComponent<BulletBehaviour>();
            if (bb != null)
            {
                fireProperties.fire(bb);
            }
        } else
        {
            Debug.Log(bulletName + " doesn't exist!");
        }
    }
}
