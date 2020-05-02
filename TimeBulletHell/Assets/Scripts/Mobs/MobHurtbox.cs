using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHurtbox : MonoBehaviour
{
    private MobBehaviour mb;

    void Start()
    {
        this.mb = this.transform.GetComponentInParent<MobBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.hurtPlayer();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.hurtPlayer();
        }
    }
}
