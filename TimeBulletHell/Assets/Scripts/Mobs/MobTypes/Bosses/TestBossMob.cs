using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossMob : BossMobBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected bool useTurnSpeed;
    [SerializeField]
    protected float turnSpeed;
    [SerializeField]
    protected float bulletsPerSecond;
    [SerializeField]
    protected int bulletCount;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected int bulletSprayAngle;
    [SerializeField]
    protected float burstsPerSecond;
    [SerializeField]
    protected int burstCount;
    [SerializeField]
    protected float burstSpeed;
    [SerializeField]
    protected float burstDuration;
    [SerializeField]
    protected float burstSprayAngle;
    [SerializeField]
    protected int burstBulletCount;
    [SerializeField]
    protected float burstBulletSpeed;

    protected float secondsPerBullet;
    protected float bulletTimeElapsed;

    protected float secondsPerBurst;
    protected float burstTimeElapsed;

    protected float currentDirection;

    protected override void onEnable()
    {
        base.onEnable();
        this.secondsPerBullet = this.bulletsPerSecond == 0 ? 1 : 1 / this.bulletsPerSecond;
        this.secondsPerBurst = this.burstsPerSecond == 0 ? 1 : 1 / this.burstsPerSecond;

        this.bulletTimeElapsed = (this.secondsPerBullet * 3) / 4;
        this.burstTimeElapsed = (this.secondsPerBurst * 3) / 4;

        this.currentDirection = this.angleToPlayer();
    }

    protected override void update()
    {
        base.update();
        this.tickBullets();
        this.tickBursts();
    }

    protected override void fixedUpdate()
    {
        base.fixedUpdate();
        this.moveTowardsPlayer();
    }

    protected virtual void moveTowardsPlayer()
    {
        float angle = this.angleToPlayer();
        float clockwiseRotation = (this.currentDirection - (this.turnSpeed * GameTime.instance.fixedDeltaTime())) % 360;
        float counterClockwiseRotation = (this.currentDirection + (this.turnSpeed * GameTime.instance.fixedDeltaTime())) % 360;
        if (Mathf.Abs(angle - clockwiseRotation) < Mathf.Abs(angle - counterClockwiseRotation))
        {
            this.currentDirection = clockwiseRotation;
        } else
        {
            this.currentDirection = counterClockwiseRotation;
        }
        if (!this.useTurnSpeed || Mathf.Abs(this.currentDirection - angle) < this.turnSpeed * GameTime.instance.fixedDeltaTime())
        {
            this.currentDirection = angle;
        }
        Vector2 toPlayer = new Vector2(Mathf.Cos(Mathf.Deg2Rad * this.currentDirection), Mathf.Sin(Mathf.Deg2Rad * this.currentDirection));
        this.GetComponent<Rigidbody2D>().MovePosition(this.transform.position + (Vector3)toPlayer * this.moveSpeed * GameTime.instance.fixedDeltaTime());
    }

    protected virtual void tickBullets()
    {
        this.bulletTimeElapsed += GameTime.instance.deltaTime();
        while(this.bulletTimeElapsed >= this.secondsPerBullet)
        {
            this.fireBullet();
            this.bulletTimeElapsed -= this.secondsPerBullet;
        }
    }

    protected virtual void fireBullet()
    {
        float direction = this.angleToPlayer();

        float minAngle = this.bulletCount > 1 ? -this.bulletSprayAngle / 2 : 0;
        float dAngle = this.bulletCount > 1 ? this.bulletSprayAngle / (this.bulletCount - 1) : -minAngle;

        for (int i = 0; i < this.bulletCount; i++)
        {
            BulletManager.instance.spawn("BasicBullet", new DefaultFireProperties(this.transform.position, direction + minAngle + (i * dAngle), this.bulletSpeed));
        }
    }

    protected virtual void tickBursts()
    {
        this.burstTimeElapsed += GameTime.instance.deltaTime();
        while (this.burstTimeElapsed >= this.secondsPerBurst)
        {
            this.fireBurst();
            this.burstTimeElapsed -= this.secondsPerBurst;
        }
    }

    protected virtual void fireBurst()
    {
        float direction = this.angleToPlayer();

        float minAngle = this.burstCount > 1 ? -this.burstSprayAngle / 2 : 0;
        float dAngle = this.burstCount > 1 ? this.burstSprayAngle / (this.burstCount - 1) : -minAngle;

        for (int i = 0; i < this.burstCount; i++)
        {
            BulletManager.instance.spawn("BubbleBullet", 
                new BubbleBulletFireProperties(
                    this.transform.position, direction + minAngle + (i * dAngle), 
                this.burstSpeed, this.burstDuration, 
                "BasicBullet", 
                this.burstBulletCount, 
                this.burstBulletSpeed));
        }
    }
}
