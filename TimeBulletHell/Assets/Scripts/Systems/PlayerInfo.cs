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

    private int initialLives;

    public static PlayerInfo instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        this.initialLives = this.lives;
        EventManager.instance.onPlayerHurtEvent += this.onHurt;
        EventManager.instance.onNeutralDropPickupEvent += this.pickupNeutralDrop;
        EventManager.instance.onItemPurchaseEvent += this.onPurchase;
        EventManager.instance.queryPointsCountEvent += this.queryPointGreaterOrEqualToPrice;
        EventManager.instance.extraLifeEvent += this.extraLives;
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
    }

    private void queryPointGreaterOrEqualToPrice(System.Object o, int price)
    {
        if (this.points >= price)
        {
            EventManager.instance.returnPointCountGreaterOrEqual(o);
        } else
        {
            EventManager.instance.returnPointCountLess(o);
        }
    }

    private void extraLives(int lives)
    {
        this.lives += lives;
    }

    private void onPurchase(AShopItem item)
    {
        this.points = Mathf.Max(0, this.points - item.getPrice());
    }

    private void gameOver()
    {
        this.lives = this.initialLives;
        this.points = 0;
        EventManager.instance.allLivesLost();
        EventManager.instance.switchToLevel("levelSelect");
    }

    public int getLives()
    {
        return this.lives;
    }

    public int getCoins()
    {
        return this.points;
    }
}
