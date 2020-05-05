using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    protected float direction;
    protected float speed;

    void Start()
    {
        this.start();
    }

    protected virtual void start()
    {
        EventManager.instance.onBulletHitsPlayerEvent += this.onHitPlayer;
    }

    void OnEnable()
    {
        this.onEnable();
    }

    protected virtual void onEnable()
    {

    }

    void OnDisable()
    {
        this.onDisable();
    }

    protected virtual void onDisable()
    {

    }

    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {
        this.disableIfReady();
    }

    void FixedUpdate()
    {
        this.fixedUpdate();
    }

    protected virtual void fixedUpdate()
    {
        this.move();
    }

    public virtual void fire(Vector2 origin, float direction, float speed)
    {
        this.transform.position = origin;
        this.direction = direction;
        this.speed = speed;
        this.transform.localEulerAngles = new Vector3(0, 0, direction);
    }

    protected virtual void move()
    {
        Vector3 step = new Vector3(Mathf.Cos(Mathf.Deg2Rad * this.direction), Mathf.Sin(Mathf.Deg2Rad * this.direction), 0.0f) * this.speed * this.fixedDeltaTime();
        this.transform.position += step;
    }

    protected virtual void disableIfReady()
    {
        if (this.transform.position.magnitude >= 10)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.onTriggerEnter2D(other);
    }

    protected virtual void onTriggerEnter2D(Collider2D other)
    {
        EventManager.instance.onBulletHit(other.transform, this);
        this.gameObject.SetActive(false);
    }

    protected virtual void onHitPlayer(PlayerHitbox ph, BulletBehaviour bb)
    {
        if (bb.Equals(this))
        {
            this.playerEffect(ph);
        }
    }

    protected virtual void playerEffect(PlayerHitbox ph)
    {

    }

    protected virtual float fixedDeltaTime()
    {
        return GameTime.instance.fixedDeltaTime();
    }
}

public class DefaultFireProperties : ISpawnProperties<BulletBehaviour>
{
    protected Vector3 origin;
    protected float direction;
    protected float speed;

    public DefaultFireProperties(Vector2 origin, float direction, float speed)
    {
        this.origin = origin;
        this.direction = direction;
        this.speed = speed;
    }

    public virtual void spawn(BulletBehaviour bb)
    {
        bb.fire(this.origin, this.direction, this.speed);
    }
}
