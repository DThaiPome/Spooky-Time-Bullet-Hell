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
    private int ticksPerSecond;

    private float secondsPerTick;
    private float timeElapsed;

    void Start()
    {
        this.secondsPerTick = 1.0f / this.ticksPerSecond;
    }

    void FixedUpdate()
    {
        this.timeElapsed += GameTime.instance.fixedDeltaTime();
        int i = 0;
        while(this.timeElapsed >= this.secondsPerTick)
        {
            this.fire();
            this.timeElapsed -= this.secondsPerTick;
            if (this.timeElapsed >= this.secondsPerTick)
            {
                i++;
                Debug.Log("Overlap " + i);
            }
        }
    }

    private void fire()
    {
        float minAngle = -this.sprayAngle / 2;
        float dAngle = this.bulletCount > 0 ? this.sprayAngle / (this.bulletCount - 1) : -minAngle;

        for(int i = 0; i < this.bulletCount; i++)
        {
            BulletManager.instance.fire(this.bulletType, new DefaultFireProperties(this.transform.position, this.transform.localEulerAngles.z + minAngle + (i * dAngle), this.bulletSpeed));
        }
    }
}
