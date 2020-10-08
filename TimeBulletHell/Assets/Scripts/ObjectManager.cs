using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wraps all pooling functionality - creates pools for all objects, and holds them
//"T" would be something like a Mob, Bullet, or Pickup
public abstract class ObjectManager<T> : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> prefabs;
    [SerializeField]
    protected int poolCounts;

    //All spawned objects are added to the current room, so that they are
    //hidden when the room is changed
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

    //Initialize stuff
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

    //Check for inactive pool objects that need to be retrieved
    protected virtual void update()
    {
        this.pools.recallBullets();
    }

    //Spawn whatever this manager manages, activate them, and initialize them based on
    //the given spawn properties
    public virtual T spawn(string name, ISpawnProperties<T> spawnProperties)
    {
        T t;
        GameObject g;
        if (this.pools.tryGetComponent<T>(name, out t, out g))
        {
            if (t != null)
            {
                g.transform.SetParent(this.roomTransform);
                spawnProperties.spawn(t);
                g.SetActive(true);
            }
        }
        return t;
    }

    //Change rooms
    protected virtual void updateRoomTransform(RoomObject room)
    {
        this.roomTransform = room.transform;
    }

    void OnDestroy()
    {
        this.onDestroy();
    }

    //It's not like an object manager will ever be destroyed, but if it did the events would break
    //unless this is done. Just a failsafe.
    protected virtual void onDestroy()
    {
        EventManager.instance.onRoomChangeEvent -= this.updateRoomTransform;
    }
}
