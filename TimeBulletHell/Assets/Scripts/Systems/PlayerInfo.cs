using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int lives;
    [SerializeField]
    private int points;

    private int initialLives;

    void Start()
    {
        this.initialLives = this.lives;
        EventManager.instance.onPlayerHurtEvent += this.onHurt;
    }

    private void onHurt()
    {
        this.lives--;
        Debug.Log(this.lives);
    }

    private void addLife()
    {
        this.lives++;
    }
}
