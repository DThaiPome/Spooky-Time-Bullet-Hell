using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected List<MobDropData> drops;

    protected int health;
    
    void Awake()
    {
        this.awake();
    }

    protected virtual void awake()
    {

    }

    void OnEnable()
    {
        this.onEnable();
    }

    protected virtual void onEnable()
    {
        this.health = this.maxHealth;
    }

    void OnDisable()
    {
        this.onDisable();
    }

    protected virtual void onDisable()
    {

    }

    void Start()
    {
        this.start();
    }

    protected virtual void start()
    {
        EventManager.instance.onPlayerBulletHitEvent += this.onPlayerBulletHit;
        this.initDrops();
    }

    protected virtual void initDrops()
    {
        float totalFrequency = 0;
        foreach(MobDropData mdd in this.drops)
        {
            float frequency = mdd.frequency;
            mdd.frequency += totalFrequency;
            totalFrequency += frequency;
        }
        foreach(MobDropData mdd in this.drops)
        {
            mdd.frequency /= totalFrequency;
        }
    }

    protected virtual void onPlayerBulletHit(Transform t, PlayerBullet pb)
    {
        if (t.Equals(this.transform))
        {
            pb.damageMob(this);
        }
    }

    public virtual void hurt(int damage)
    {
        this.health -= damage;
    }

    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {
        this.disableIfReady();
    }

    protected virtual void disableIfReady()
    {
        if (this.health <= 0)
        {
            this.onDeath();
            this.gameObject.SetActive(false);
        }
    }

    protected virtual void onDeath()
    {
        this.spawnDrops(Random.value);
    }

    protected virtual void spawnDrops(float rand)
    {
        foreach(MobDropData mdd in this.drops)
        {
            if (rand < mdd.frequency)
            {
                mdd.spawnPickups(this.transform.position);
                return;
            }
        }
    }

    void FixedUpdate()
    {
        this.fixedUpdate();
    }

    protected virtual void fixedUpdate()
    {

    }

    public virtual void spawn(Vector3 origin)
    {
        transform.localPosition = origin;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.onTriggerEnter2D(other);
    }

    protected virtual void onTriggerEnter2D(Collider2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        this.onTriggerStay2D(other);
    }

    protected virtual void onTriggerStay2D(Collider2D other)
    {

    }
}

public class DefaultSpawnProperties : ISpawnProperties<MobBehaviour>
{
    private Vector2 origin;

    public DefaultSpawnProperties(Vector2 origin)
    {
        this.origin = origin;
    }

    public void spawn(MobBehaviour mb)
    {
        mb.spawn(this.origin);
    }
}
