using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexRingBug : MobBehaviour
{
    [SerializeField]
    private string bulletName = "BasicBullet";
    [SerializeField]
    private int bulletCount = 8;
    [SerializeField]
    private float bulletSpeed = 3;
    [SerializeField]
    private float bulletsPerSecond = 4;

    private float secondsPerBullet;
    private float timeElapsed;

    private Transform player;

    protected override void awake()
    {
        base.awake();
        this.secondsPerBullet = this.bulletsPerSecond > 0 ? 1f / (float)this.bulletsPerSecond : 1;
    }

    protected override void onEnable()
    {
        base.onEnable();
        this.timeElapsed = 0;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void update()
    {
        base.update();
        this.LookAtPlayer();
        this.AdvanceTime();
    }

    private void LookAtPlayer()
    {
        Vector2 toPlayer = (Vector2)(this.player.position - this.transform.position).normalized;
        Quaternion atPlayer = Quaternion.LookRotation(Vector3.forward, toPlayer);
        Quaternion fixAxis = Quaternion.AngleAxis(90, Vector3.forward);
        this.transform.rotation = atPlayer * fixAxis;
    }

    private void AdvanceTime()
    {
        this.timeElapsed += GameTime.instance.deltaTime();
        while(this.timeElapsed >= this.secondsPerBullet)
        {
            this.timeElapsed -= this.secondsPerBullet;
            this.Fire();
        }
    }

    private void Fire()
    {
        float angle = this.angleToPlayer();
        float step = 360f / (float)this.bulletCount;

        for(int i = 0; i < this.bulletCount; i++)
        {
            angle += step;
            BulletManager.instance.spawn(this.bulletName, new DefaultFireProperties(this.transform.position, angle, this.bulletSpeed));
        }
    }
}
