using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : ObjectManager<MobBehaviour>
{
    private float timeElapsed;
    private float timeToSpawn = 2;
    private float count = 2;

    protected override void update()
    {
        base.update();
        /*this.timeElapsed += GameTime.instance.deltaTime();
        while (this.timeElapsed >= this.timeToSpawn)
        {
            this.timeElapsed -= this.timeToSpawn;
            for (int i = 0; i < this.count; i++)
            {
                float xPos = Random.Range(-8.0f, 8.0f);
                float yPos = Random.Range(-3.5f, 3.5f);
                MobManager.instance.spawn("BAT", new DefaultSpawnProperties(new Vector2(xPos, yPos)));
            }
        }*/
    }
}
