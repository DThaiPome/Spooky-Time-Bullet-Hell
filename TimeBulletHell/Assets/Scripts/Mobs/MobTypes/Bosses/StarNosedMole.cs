using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNosedMole : BossMobWithPhases
{
    [SerializeField]
    private float coverFaceLength = 2f;

    private StarNosedMoleState state;

    private bool coveringFace;

    protected override void onEnable()
    {
        base.onEnable();
        this.state = StarNosedMoleState.ExposedI;
        this.EndCoveringFace();
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
            "CoverFace"
        };
        List<float> odds = new List<float>()
        {
            5f,
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

    public override void hurt(int damage)
    {
        if (!this.coveringFace)
        {
            Debug.Log("OUCH");
            base.hurt(damage);
        }
    }

    private void BeginCoverFace()
    {
        this.coveringFace = true;
    }

    private void EndCoveringFace()
    {
        this.coveringFace = false;
    }

    private enum StarNosedMoleState
    {
        ExposedI, ExposedII, ExposedIII, Hiding, Tunneling
    }
}
