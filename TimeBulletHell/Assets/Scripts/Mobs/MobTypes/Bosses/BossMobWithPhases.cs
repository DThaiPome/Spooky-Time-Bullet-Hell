using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMobWithPhases : BossMobBehaviour
{
    private Queue<string> toDo;
    protected bool busy { get; private set; }
    protected string lastAction { get; private set; }

    protected override void onEnable()
    {
        base.onEnable();
        this.ResetQueue();
        this.lastAction = "";
    }

    protected void AddToDo(string func)
    {
        this.toDo.Enqueue(func);
    }

    protected void EndAction(float restingTime)
    {
        this.Rest();
        StartCoroutine(RestBeforeNextAction(restingTime));
    }

    private IEnumerator RestBeforeNextAction(float restingTime)
    {
        yield return new WaitForSeconds(restingTime);
        this.busy = false;
    }

    protected void ResetQueue()
    {
        this.toDo = new Queue<string>();
        this.busy = true;
        this.EndAction(0.1f);
    }

    protected override void update()
    {
        base.update();
        if (!this.busy && this.toDo.Count > 0)
        {
            this.busy = true;
            string action = this.toDo.Dequeue();
            this.StartCoroutine(action);
            this.lastAction = action;
            Debug.Log(action);
        }
    }

    protected abstract void Rest();

    protected void AddRandomToDo(List<string> funcs, List<float> odds)
    {
        float r = Random.Range(0f, 1f);
        odds = this.NormalizeOdds(odds);
        for(int i = 0; i < funcs.Count && i < odds.Count; i++)
        {
            if (r <= odds[i])
            {
                this.AddToDo(funcs[i]);
                return;
            }
        }
    }

    private List<float> NormalizeOdds(List<float> odds)
    {
        List<float> result = new List<float>(odds);
        float sum = 0;
        for(int i = 0; i < result.Count; i++)
        {
            float prevSum = sum;
            sum += result[i];
            result[i] += prevSum;
        }
        for(int i = 0; i < result.Count; i++)
        {
            result[i] /= sum;
        }
        return result;
    }

    protected bool IsToDoEmpty()
    {
        return this.toDo.Count == 0;
    }
}
