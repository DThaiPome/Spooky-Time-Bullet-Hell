using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectManager<T> : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> prefabs;
    [SerializeField]
    protected int poolCounts;

    protected Transform roomTransform;

    protected PoolMap pools;

    public static ObjectManager<T> instance;

    void Awake()
    {
        this.awake();
    }

    protected virtual void awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.start();
    }

    protected virtual void start()
    {
        EventManager.instance.onRoomChangeEvent += this.updateRoomTransform;
        this.pools = new PoolMap(this.prefabs, this.poolCounts, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {
        this.pools.recallBullets();
    }

    public T spawn(string name, ISpawnProperties<T> spawnProperties)
    {
        T t;
        GameObject g;
        if (this.pools.tryGetComponent<T>(name, out t, out g))
        {
            if (t != null)
            {
                g.transform.SetParent(this.roomTransform);
                spawnProperties.spawn(t);
            }
        }
        return t;
    }

    protected virtual void updateRoomTransform(RoomObject room)
    {
        this.roomTransform = room.transform;
    }
}
