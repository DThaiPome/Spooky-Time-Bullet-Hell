using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBat : MobBehaviour
{
    [SerializeField]
    private float bulletsPerSecond = 1;
    [SerializeField]
    private float bulletSpeed = 5;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float moveRadius = 1;

    private float secondsPerBullet;
    private float timeElapsed;

    private float orientation;
    private Rigidbody2D rb;
    private Vector2 initialPos;

    protected override void onEnable()
    {
        this.timeElapsed = 0;
        this.orientation = UnityEngine.Random.Range(0, 360);
        this.initialPos = this.transform.position;
    }

    protected override void start()
    {
        this.secondsPerBullet = this.bulletsPerSecond == 0 ? 1 : 1 / this.bulletsPerSecond;
        this.rb = this.GetComponent<Rigidbody2D>();

        EventManager.instance.onWarpedTickEvent += this.turn;
    }

    protected override void onDestroy()
    {
        EventManager.instance.onWarpedTickEvent -= this.turn;
    }

    protected override void update()
    {
        base.update();
        this.fireIfReady();
        this.move();
    }

    private void turn()
    {
        this.rotateOrientation(UnityEngine.Random.Range(-90, 90) * GameTime.instance.deltaTime());

    }

    private void move()
    {
        Vector2 offset = new Vector2(Mathf.Cos(this.orientation * Mathf.Deg2Rad), Mathf.Sin(this.orientation * Mathf.Deg2Rad)) 
            * this.moveSpeed * GameTime.instance.deltaTime();
        Vector2 newPos = (Vector2)this.transform.position + offset;

        if (Vector2.Distance(this.initialPos, newPos) > this.moveRadius)
        {
            this.rotateOrientation(90);
            Debug.Log("oops");
            this.move();
        } else
        {
            this.rb.MovePosition(newPos);
        }
    }

    private void rotateOrientation(float degrees)
    {
        this.orientation += degrees;
        this.orientation %= 360;
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
