using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : BulletBehaviour
{
    protected Vector2 origin;
    protected float duration;

    private float timeElapsed;

    public void fire(Vector3 origin, float direction, float speed, float duration)
    {
        this.fire(origin, direction, speed);
        this.duration = duration;
        this.origin = origin;
        this.timeElapsed = 0;
    }

    protected override void move()
    {
        base.move();
        this.timeElapsed += Time.deltaTime;
        this.speed = Mathf.Lerp(this.speed, this.duration - this.timeElapsed, 0.05f);
        if (this.timeElapsed >= this.duration)
        {
            this.burst();
        }
    }

    private void burst()
    {
        BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(this.transform.position, this.direction + 45, 5));
        BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(this.transform.position, this.direction + 135, 5));
        BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(this.transform.position, this.direction + 225, 5));
        BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(this.transform.position, this.direction + 315, 5));
        this.gameObject.SetActive(false);
    }

    protected override void disableIfReady()
    {
        
    }
}

public class BubbleBulletFireProperties : DefaultFireProperties
{
    protected float duration;

    public BubbleBulletFireProperties(Vector2 origin, float direction, float speed, float distance) : base(origin, direction, speed)
    {
        this.duration = distance;
    }

    public override void fire(BulletBehaviour bb)
    {
        BubbleBullet bubble = null;
        try
        {
            bubble = (BubbleBullet)bb;
        }
        catch (Exception e) { }
        if (bubble != null)
        {
            bubble.fire(origin, direction, speed, duration);
        }
    }
}
