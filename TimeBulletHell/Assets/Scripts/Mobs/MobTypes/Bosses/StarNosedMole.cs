using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNosedMole : BossMobWithPhases
{
    [SerializeField]
    private float coverFaceLength = 2f;
    [SerializeField]
    private Vector2 moleCountRange = new Vector2(7, 10);
    [SerializeField]
    private int moleSpawnDirections = 24;
    [SerializeField]
    private float moleDistance = 2.5f;
    [SerializeField]
    private float molesPerSecond = 4;

    private float secondsPerMole;

    private StarNosedMoleState state;

    private bool coveringFace;

    protected override void onEnable()
    {
        base.onEnable();
        this.state = StarNosedMoleState.ExposedI;

        this.secondsPerMole = 1 / this.molesPerSecond;

        this.EndCoveringFace();
    }

    //Only get hurt if its not defending
    public override void hurt(int damage)
    {
        if (!this.coveringFace)
        {
            Debug.Log("OUCH");
            base.hurt(damage);
        }
    }

    protected override void Rest()
    {
        return;
    }

    protected override void update()
    {
        base.update();
        switch(this.state)
        {
            case StarNosedMoleState.ExposedI:
                this.ExposedIUpdate();
                break;
            case StarNosedMoleState.ExposedII:

                break;
            case StarNosedMoleState.ExposedIII:

                break;
            case StarNosedMoleState.Hiding:

                break;
            case StarNosedMoleState.Tunneling:

                break;
        }
    }

    private void ExposedIUpdate()
    {
        List<string> funcs = new List<string>()
        {
            "ThrowRock",
            "CoverFace",
            "SpawnMoleWave"
        };
        List<float> odds = new List<float>()
        {
            0f,
            0f,
            1f
        };
        
        if (!this.busy)
        {
            if (this.lastAction != "CoverFace" && this.IsToDoEmpty())
            {
                this.AddRandomToDo(funcs, odds);
            }
            else
            {
                this.AddToDo("ThrowRock");
            }
        }
    }

    //Actions --

    private IEnumerator ThrowRock()
    {
        for(int i = 0; i < 5; i++)
        {
            BulletManager.instance.spawn("BasicBullet", new DefaultFireProperties(this.transform.position, this.angleToPlayer(), 4));
            yield return GameTime.instance.WaitForSeconds(0.25f);
        }
        this.EndAction(0.5f);
    }

    private IEnumerator CoverFace()
    {
        this.BeginCoverFace();
        yield return GameTime.instance.WaitForSeconds(this.coverFaceLength);
        this.EndCoveringFace();
        this.EndAction(0.1f);
    }

    private void BeginCoverFace()
    {
        this.coveringFace = true;
    }

    private void EndCoveringFace()
    {
        this.coveringFace = false;
    }

    //Spawn moles at the given distance from the player, facing them.
    //(we use a finite amount of directions to make checking for a wall less stupid)
    private IEnumerator SpawnMoleWave()
    {
        int minMoles = (int)this.moleCountRange.x;
        int maxMoles = (int)this.moleCountRange.y;
        int moleCount = Random.Range(minMoles, maxMoles + 1);

        List<Vector2> points = GetPointsInCircle(this.PlayerPosition(), this.moleDistance, this.moleSpawnDirections);

        for (int i = 0; i < moleCount && points.Count > 0; i++)
        {
            int index = Random.Range(0, points.Count);
            Vector2 spawnPoint = points[index];
            points.RemoveAt(index);
            SpawnMole(spawnPoint, this.angleToPlayerFromPosition(spawnPoint));
            yield return GameTime.instance.WaitForSeconds(this.secondsPerMole);
        }
        this.EndAction(.3f * moleCount);
    }

    private static void SpawnMole(Vector2 origin, float angle)
    {
        MobManager.instance.spawn("MarchingMole", new MobSpawnPropertiesWithRotation(origin, Quaternion.AngleAxis(angle, Vector3.forward)));
    }
    
    private static List<Vector2> GetPointsInCircle(Vector2 origin, float distance, int count)
    {
        List<Vector2> points = new List<Vector2>();
        int mask = LayerMask.GetMask("Wall");
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        float step = (2 * Mathf.PI) / (float)count;
        float angle = 0;
        for(int i = 0; i < count; i++)
        {
            Vector2 direction = VectorFromAngle(angle);
            if (!Physics2D.Raycast(origin, direction, distance, mask))
            {
                points.Add(origin + (direction * distance));
            }
            angle += step;
        }

        return points;
    }
    
    //In radians
    private static Vector2 VectorFromAngle(float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private enum StarNosedMoleState
    {
        ExposedI, ExposedII, ExposedIII, Hiding, Tunneling
    }
}
