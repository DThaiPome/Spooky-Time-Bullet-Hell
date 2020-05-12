using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoInGame : MonoBehaviour
{
    public static PlayerInfoInGame instance;

    private GameObject player;
    private GameObject playerHitbox;

    void Awake()
    {
        instance = this;
        this.getObjects();
    }

    private void getObjects()
    {
        this.player = GameObject.Find("Player");
        this.playerHitbox = GameObject.Find("PlayerHitbox");
    }

    public Vector3 getPosition()
    {
        return this.player.transform.position;
    }

    public Vector3 getEulerAngles()
    {
        return this.player.transform.localEulerAngles;
    }
}
