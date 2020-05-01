﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth;

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
            this.gameObject.SetActive(false);
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
        transform.position = origin;
    }
}

public class DefaultSpawnProperties : IMobSpawnProperty
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
