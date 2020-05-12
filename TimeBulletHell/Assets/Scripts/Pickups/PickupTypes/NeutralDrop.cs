using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralDrop : APickup
{
    [SerializeField]
    protected float moveRate;

    protected GameObject player;
    //private Rigidbody2D rb;

    protected override void onPickup()
    {
        EventManager.instance.onNeutralDropPickup();
        this.gameObject.SetActive(false);
    }

    protected override InventoryItem getInventoryItem()
    {
        return null;
    }

    protected override void start()
    {
        base.start();
        EventManager.instance.onBossDefeatedEvent += this.onBossDefeated;
        this.player = GameObject.Find("Player");
        //this.rb = this.transform.GetComponentInChildren<Rigidbody2D>();
    }

    protected override void update()
    {
        base.update();
        Vector2 toPlayer = this.player.transform.position - this.transform.position;
        float distanceToPlayer = toPlayer.magnitude;
        toPlayer /= distanceToPlayer;
        float speed = (1 / Mathf.Pow(distanceToPlayer, 2)) * this.moveRate;

        if (speed >= 0.1)
        {
            Vector2 step = toPlayer * (1 / Mathf.Pow(distanceToPlayer, 2)) * this.moveRate * GameTime.instance.deltaTime();
            if (!Physics2D.Raycast(this.transform.position, toPlayer, step.magnitude, LayerMask.GetMask("Wall")))
            {
                this.transform.position += (Vector3)step;
            }
        }
        //this.rb.MovePosition(step);
    }

    protected void onBossDefeated()
    {
        this.onPickup();
    }

    void OnDestroy()
    {
        EventManager.instance.onBossDefeatedEvent -= this.onBossDefeated;
    }
}
