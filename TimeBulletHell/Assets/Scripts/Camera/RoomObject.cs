using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    void Start()
    {
        EventManager.instance.setRoomActiveEvent += this.setActive;
        EventManager.instance.switchToRoomEvent += this.onRoomChange;
    }

    private void setActive(string name)
    {
        if (name == this.name)
        {
            this.gameObject.SetActive(true);
        }
    }

    private void onRoomChange(string name)
    {
        if (name == this.name)
        {
            this.gameObject.SetActive(true);
            EventManager.instance.onRoomChange(this);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}
