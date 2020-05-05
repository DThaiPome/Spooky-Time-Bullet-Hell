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
        this.pools = new PoolMap(this.prefabs, this.poolCounts, this.roomTransform);
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

    public void spawn(string name, ISpawnProperties<T> spawnProperties)
    {
        T t;
        if (this.pools.tryGetComponent<T>(name, out t))
        {
            if (t != null)
            {
                spawnProperties.spawn(t);
            }
        }
    }

    protected virtual void updateRoomTransform(string roomName)
    {
        GameObject roomObject = GameObject.Find("roomName");
        this.roomTransform = roomObject.transform;
    }
}
