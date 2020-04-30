using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : BulletBehaviour
{
    protected Vector2 origin;
    protected float duration;
    protected string burstType;
    protected float burstCount;
    protected float burstSpeed;

    private float timeElapsed;

    public void fire(Vector3 origin, float direction, float speed, float duration, string burstType, float burstCount, float burstSpeed)
    {
        this.fire(origin, direction, speed);
        this.duration = duration;
        this.origin = origin;
        this.burstType = burstType;
        this.burstCount = burstCount;
        this.burstSpeed = burstSpeed;
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
        float diff = 360 / this.burstCount;
        for(int i = 0; i < this.burstCount; i++)
        {
            BulletManager.instance.fire(this.burstType, new DefaultFireProperties(this.transform.position, diff * (i + 0.5f), this.burstSpeed));
        }
        this.gameObject.SetActive(false);
    }

    protected override void disableIfReady()
    {
        
    }
}

public class BubbleBulletFireProperties : DefaultFireProperties
{
    protected float duration;
    protected string burstType;
    protected float burstCount;
    protected float burstSpeed;

    public BubbleBulletFireProperties(Vector2 origin, float direction, float speed, float distance, string burstType, float burstCount, float burstSpeed) 
        : base(origin, direction, speed)
    {
        this.duration = distance;
        this.burstType = burstType;
        this.burstCount = burstCount;
        this.burstSpeed = burstSpeed;
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
            bubble.fire(this.origin, this.direction, this.speed, this.duration, this.burstType, this.burstCount, this.burstSpeed);
        }
    }
}
