using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Dictionary<string, Vector2> rooms;
    [SerializeField]
    private string currentRoom;
    [SerializeField]
    private float moveRate;

    // Start is called before the first frame update
    void Start()
    {
        this.rooms = new Dictionary<string, Vector2>();
        EventManager.instance.switchToRoomEvent += this.changeRoom;
        this.initRoomMap();
        EventManager.instance.switchToRoom(this.currentRoom);
    }

    private void initRoomMap()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Room"))
        {
            string id = g.name;
            this.rooms.Add(id, g.transform.position);
        }
    }

    private void changeRoom(string id)
    {
        this.currentRoom = id;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        this.focusOnRoom(this.moveRate);
    }

    private void focusOnRoom(float rate)
    {
        Vector2 target;
        if (!this.rooms.TryGetValue(this.currentRoom, out target))
        {
            target = new Vector2();
        }

        float newX = Mathf.Lerp(this.transform.position.x, target.x, rate);
        float newY = Mathf.Lerp(this.transform.position.y, target.y, rate);
        Vector2 newPos = new Vector2(newX, newY);
        if (((Vector2)this.transform.position - newPos).magnitude < 0.01)
        {
            newPos = target;
        }

        this.transform.position = new Vector3(newPos.x, newPos.y, this.transform.position.z);
    }

    void OnDestroy()
    {
        EventManager.instance.switchToRoomEvent -= this.changeRoom;
    }
}
