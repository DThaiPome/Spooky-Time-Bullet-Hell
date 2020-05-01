using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    protected float direction;
    protected float speed;

    void Update()
    {
        this.disableIfReady();
    }

    void FixedUpdate()
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
        Vector3 step = new Vector3(Mathf.Cos(Mathf.Deg2Rad * this.direction), Mathf.Sin(Mathf.Deg2Rad * this.direction), 0.0f) * this.speed * GameTime.deltaTime;
        this.transform.position += step;
    }

    protected virtual void disableIfReady()
    {
        if (this.transform.position.magnitude >= 10)
        {
            this.gameObject.SetActive(false);
        }
    }
}

public class DefaultFireProperties : IFireProperties
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

    public virtual void fire(BulletBehaviour bb)
    {
        bb.fire(this.origin, this.direction, this.speed);
    }
}
