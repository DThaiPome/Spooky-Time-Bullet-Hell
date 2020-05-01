using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static float deltaTime;

    [SerializeField]
    private int ticksPerSecond;
    [SerializeField]
    private float minMulitiplier;
    [SerializeField]
    private float maxMultiplier;

    private PlayerMovement pm;

    private float secondsPerTick;
    private float timeElapsed;

    void Start()
    {
        if (this.ticksPerSecond == 0)
            this.ticksPerSecond = 1;
        this.pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        this.secondsPerTick = 1.0f / this.ticksPerSecond;
    }

    void Update()
    {
        deltaTime = Mathf.Lerp(this.minMulitiplier, this.maxMultiplier, this.pm.getSpeedPercent()) * Time.deltaTime;
        this.timeElapsed += deltaTime;
        while (this.timeElapsed >= this.secondsPerTick)
        {
            EventManager.instance.onWarpedTick();
            this.timeElapsed -= this.secondsPerTick;
        }
    }
}
