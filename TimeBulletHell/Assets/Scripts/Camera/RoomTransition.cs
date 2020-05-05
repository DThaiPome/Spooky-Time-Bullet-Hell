using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField]
    [Tooltip("left, up, down, or right")]
    private string direction;
    private string from;
    [SerializeField]
    private string to;

    void Start()
    {
        EventManager.instance.onRoomChangeEvent += this.onRoomChange;
        this.from = this.transform.parent.name;
    }

    private void onRoomChange(string id)
    {
        if (id == this.from)
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (this.direction == "left" || this.direction == "up" || this.direction == "right" || this.direction == "down")
            {
                EventManager.instance.onPlayerControlModeChanged("move " + this.direction);
                EventManager.instance.setRoomActive(to);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.onPlayerControlModeChanged("");
            EventManager.instance.onRoomChange(to);
        }
    }
}
