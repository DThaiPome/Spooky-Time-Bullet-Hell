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

    private BulletBehaviour bulletHit;

    void Start()
    {
        EventManager.instance.onBulletHitEvent += this.onBulletHit;
        EventManager.instance.onPlayerHurtEvent += this.onHurt;
    }

    private void onBulletHit(Transform t, BulletBehaviour bb)
    {
        if (t.Equals(this.transform) && !this.immune && bulletHit == null)
        {
            this.bulletHit = bb;
            EventManager.instance.onBulletHitsPlayer(this, this.bulletHit);
        }
    }

    private void onHurt()
    {
        this.immunity(this.tempImmunityDuration);
        this.bulletHit = null;
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
}
