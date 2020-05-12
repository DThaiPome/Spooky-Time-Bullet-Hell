using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : ObjectManager<BulletBehaviour>
{
    protected override void start()
    {
        base.start();
        EventManager.instance.onWarpedTickEvent += this.onWarpedTick;
    }

    private int direction = 0;

    void onWarpedTick()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            /*BulletManager.instance.fire("BubbleBullet", new BubbleBulletFireProperties(new Vector2(0, 0), direction, 5, 1, "BasicBullet", 5, 4));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction - 5, 4));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction - 5, 2));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction + 5, 4));
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(new Vector2(0, 0), direction + 5, 2));
            direction += 24;
            direction %= 360;*/
        }
    }

    void OnDestroy()
    {
        EventManager.instance.onWarpedTickEvent -= this.onWarpedTick;
    }
}
