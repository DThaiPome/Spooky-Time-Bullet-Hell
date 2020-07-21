using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBat : MobBehaviour
{
    [SerializeField]
    private float bulletsPerSecond = 0.5f;
    [SerializeField]
    private float bulletSpeed = 4;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float moveRadius = 1;
    [SerializeField]
    private float maxDegreesPerTick = 30;

    private float secondsPerBullet;
    private float timeElapsed;

    private float rotationalSpeed;
    private float orientation;
    private Rigidbody2D rb;
    private Vector2 initialPos;

    protected override void onEnable()
    {
        base.onEnable();
        this.timeElapsed = 0;
        this.orientation = UnityEngine.Random.Range(0, 360);
        this.initialPos = this.transform.position;
    }

    protected override void start()
    {
        base.start();
        this.secondsPerBullet = this.bulletsPerSecond == 0 ? 1 : 1 / this.bulletsPerSecond;
        this.rb = this.GetComponent<Rigidbody2D>();

        EventManager.instance.onWarpedTickEvent += this.updateRotationalSpeedPerWarpedTick;
    }

    protected override void onDestroy()
    {
        base.onDestroy();
        EventManager.instance.onWarpedTickEvent -= this.updateRotationalSpeedPerWarpedTick;
    }

    protected override void update()
    {
        base.update();
        this.turn();
        this.fireIfReady();
        this.move();
    }

    private void turn()
    {
        this.rotateOrientation(this.rotationalSpeed * GameTime.instance.deltaTime());
    }

    private void move()
    {
        Vector2 offset = new Vector2(Mathf.Cos(this.orientation * Mathf.Deg2Rad), Mathf.Sin(this.orientation * Mathf.Deg2Rad)) 
            * this.moveSpeed * GameTime.instance.deltaTime();
        Vector2 newPos = (Vector2)this.transform.position + offset;

        this.rb.MovePosition(newPos);
    }

    private void rotateOrientation(float degrees)
    {
        this.orientation += degrees;
        this.orientation %= 360;
    }

    private void updateRotationalSpeedPerWarpedTick()
    {
        if (Vector2.Distance(this.transform.position, this.initialPos) >= this.moveRadius)
        {
            this.rotationalSpeed = this.rotationalSpeed > 0 ? this.maxDegreesPerTick : -this.maxDegreesPerTick;
        }
        else
        {
            this.updateRotationalSpeed(UnityEngine.Random.Range(-this.maxDegreesPerTick, this.maxDegreesPerTick));
        }
    }

    private void updateRotationalSpeed(float degreesPerSecond)
    {
        this.rotationalSpeed = Mathf.Clamp(this.rotationalSpeed + degreesPerSecond, -this.maxDegreesPerTick, this.maxDegreesPerTick);
    }

    private void fireIfReady()
    {
        this.timeElapsed += GameTime.instance.deltaTime();
        while(this.timeElapsed >= this.secondsPerBullet)
        {
            this.fire();
            this.timeElapsed -= this.secondsPerBullet;
        }
    }

    private void fire()
    {
        float direction = this.angleToPlayer();

        BulletManager.instance.spawn("BasicBullet", new DefaultFireProperties(this.transform.position, direction, this.bulletSpeed));
    }
}
