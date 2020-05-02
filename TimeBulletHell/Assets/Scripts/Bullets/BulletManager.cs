using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs;
    [SerializeField]
    private int poolCounts;

    private PoolMap pools;

    public static BulletManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EventManager.instance.onWarpedTickEvent += this.onWarpedTick;
        this.pools = new PoolMap(this.prefabs, this.poolCounts, this.transform);
    }

    void Update()
    {
        this.pools.recallBullets();
    }

    private int direction = 0;

    void onWarpedTick()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            /*BulletManager.instance.fire("BubbleBullet", new BubbleBulletFireProperties(new Vector2(0, 0), direction, 5, 1, "BasicBullet", 5, 4));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction - 5, 4));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction - 5, 2));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction + 5, 4));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction + 5, 2));
            direction += 24;
            direction %= 360;*/
        }
    }

    public void fire(string bulletName, IFireProperties fireProperties)
    {
        BulletBehaviour bb;
        if (this.pools.tryGetComponent<BulletBehaviour>(bulletName, out bb))
        {
            if (bb != null)
            {
                fireProperties.fire(bb);
            }
        }
    }
}
