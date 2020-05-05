using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int lives;
    [SerializeField]
    private int points;
    [SerializeField]
    private int pointsToExtraLife;

    private int initialLives;

    void Start()
    {
        this.initialLives = this.lives;
        EventManager.instance.onPlayerHurtEvent += this.onHurt;
        EventManager.instance.onNeutralDropPickupEvent += this.pickupNeutralDrop;
    }

    private float timeElapsed;
    private int counter;

    void Update()
    {
        this.timeElapsed += GameTime.instance.deltaTime();
        while(this.timeElapsed >= 1)
        {
            this.timeElapsed -= 1;
            counter++;
            Debug.Log(counter);
        }
    }

    private void onHurt()
    {
        this.lives--;
        if (this.lives < 0)
        {
            this.gameOver();
        }
    }

    private void addLife()
    {
        this.lives++;
    }

    private void pickupNeutralDrop()
    {
        this.points++;
        if (this.points >= this.pointsToExtraLife)
        {
            this.addLife();
            this.points = 0;
        }
    }

    private void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
