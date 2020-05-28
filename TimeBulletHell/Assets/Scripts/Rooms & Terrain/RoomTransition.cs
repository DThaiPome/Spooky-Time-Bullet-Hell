using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    private enum RoomTransitionDirection
    {
        Left, Up, Down, Right
    }

    [SerializeField]
    private RoomTransitionDirection direction;
    private string from;
    [SerializeField]
    private string to;

    void Start()
    {
        EventManager.instance.switchToRoomEvent += this.onRoomChange;
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
            switch(this.direction)
            {
                case RoomTransitionDirection.Right:
                    EventManager.instance.onPlayerControlModeChanged(PlayerControlMode.MoveRight);
                    break;
                case RoomTransitionDirection.Up:
                    EventManager.instance.onPlayerControlModeChanged(PlayerControlMode.MoveUp);
                    break;
                case RoomTransitionDirection.Left:
                    EventManager.instance.onPlayerControlModeChanged(PlayerControlMode.MoveLeft);
                    break;
                case RoomTransitionDirection.Down:
                    EventManager.instance.onPlayerControlModeChanged(PlayerControlMode.MoveDown);
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.onPlayerControlModeChanged(PlayerControlMode.Default);
            EventManager.instance.switchToRoom(to);
        }
    }

    void OnDestroy()
    {
        EventManager.instance.switchToRoomEvent -= this.onRoomChange;
    }
}
