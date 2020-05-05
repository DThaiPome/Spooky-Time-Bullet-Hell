using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    private string bulletType;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int bulletCount;
    [SerializeField]
    private float sprayAngle;
    [SerializeField]
    private int shotsPerSecond;

    private float secondsPerShot;
    private float timeElapsed;

    void Start()
    {
        this.secondsPerShot = 1.0f / this.shotsPerSecond;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) || this.timeElapsed < this.secondsPerShot)
        {
            this.timeElapsed += GameTime.instance.fixedDeltaTime();
        } else if (this.timeElapsed >= this.secondsPerShot)
        {
            this.timeElapsed = this.secondsPerShot;
        }
        while(Input.GetKey(KeyCode.Space) && this.timeElapsed >= this.secondsPerShot)
        {
            this.fire();
            this.timeElapsed -= this.secondsPerShot;
        }
    }

    private void fire()
    {
        float minAngle = this.bulletCount > 1 ? -this.sprayAngle / 2 : 0;
        float dAngle = this.bulletCount > 1 ? this.sprayAngle / (this.bulletCount - 1) : -minAngle;

        for(int i = 0; i < this.bulletCount; i++)
        {
            BulletManager.instance.spawn(this.bulletType, new DefaultFireProperties(this.transform.position, this.transform.localEulerAngles.z + minAngle + (i * dAngle), this.bulletSpeed));
        }
    }
}
