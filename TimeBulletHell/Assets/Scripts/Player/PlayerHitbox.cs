using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField]
    private float tempImmunityDuration;

    private float immunityTimeElapsed;
    private float immunityDuration;
    private bool immune;
    private bool active;

    private BulletBehaviour bulletHit;

    void Start()
    {
        this.active = true;
        EventManager.instance.onBulletHitEvent += this.onBulletHit;
        EventManager.instance.hurtPlayerEvent += this.hurtPlayer;
        EventManager.instance.onPlayerHurtEvent += this.onHurt;
        EventManager.instance.onBossDefeatedEvent += this.onBossDefeated;
    }

    private void onBulletHit(Transform t, BulletBehaviour bb)
    {
        if (t.Equals(this.transform) && bulletHit == null)
        {
            this.bulletHit = bb;
            EventManager.instance.onBulletHitsPlayer(this, this.bulletHit);
            EventManager.instance.hurtPlayer();
        }
    }

    private void hurtPlayer()
    {
        if (this.active && !this.immune)
        {
            Debug.Log("OUCH");
            EventManager.instance.onPlayerHurt();
        }
        this.bulletHit = null;
    }

    private void onHurt()
    {
        this.immunity(this.tempImmunityDuration);
    }

    private void immunity(float duration)
    {
        this.immunityTimeElapsed = 0;
        this.immunityDuration = duration;
        this.immune = true;
    }

    void Update()
    {
        this.updateImmunity();
        //this.makeImmune();
    }

    private void makeImmune()
    {
        Collider2D collider = this.gameObject.GetComponent<Collider2D>();
        collider.enabled = this.immune;
    }

    private void updateImmunity()
    {
        if (this.immune)
        {
            this.immunityTimeElapsed += GameTime.instance.deltaTime();
            if (this.immunityTimeElapsed >= this.immunityDuration)
            {
                this.immune = false;
            }
        }
    }

    private void onBossDefeated()
    {
        this.active = false;
    }

    void OnDestroy()
    {
        EventManager.instance.onBulletHitEvent -= this.onBulletHit;
        EventManager.instance.hurtPlayerEvent -= this.hurtPlayer;
        EventManager.instance.onPlayerHurtEvent -= this.onHurt;
        EventManager.instance.onBossDefeatedEvent -= this.onBossDefeated;
    }
}
