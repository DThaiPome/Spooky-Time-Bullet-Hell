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

    void Start()
    {
        this.initialLives = this.lives;
        EventManager.instance.onPlayerHurtEvent += this.onHurt;
        EventManager.instance.onNeutralDropPickupEvent += this.pickupNeutralDrop;
        EventManager.instance.onItemPurchaseEvent += this.onPurchase;
        EventManager.instance.queryPointsCountEvent += this.queryPointGreaterOrEqualToPrice;
    }

    private void onHurt()
    {
        this.lives--;
        if (this.lives < 0)
        {
            this.gameOver();
            this.lives = this.initialLives;
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

    private void onPurchase(AShopItem item)
    {
        this.points = Mathf.Max(0, this.points - item.getPrice());
        Debug.Log(this.points);
    }

    private void gameOver()
    {
        EventManager.instance.switchToLevel("levelSelect");
    }
}
