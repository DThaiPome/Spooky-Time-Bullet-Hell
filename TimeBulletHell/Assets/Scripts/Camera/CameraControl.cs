using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Dictionary<string, RoomObject> rooms;
    [SerializeField]
    private string currentRoom;
    [SerializeField]
    private float moveRate;
    [SerializeField]
    private float cameraFreedom;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        this.rooms = new Dictionary<string, RoomObject>();
        EventManager.instance.switchToRoomEvent += this.changeRoom;
        this.initRoomMap();
        EventManager.instance.switchToRoom(this.currentRoom);

        this.player = GameObject.FindGameObjectWithTag("Player").transform;

        this.cameraFreedom = 1 - Mathf.Clamp(this.cameraFreedom, 0, 1);
    }

    private void initRoomMap()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Room"))
        {
            string id = g.name;
            this.rooms.Add(id, g.GetComponent<RoomObject>());
        }
    }

    private void changeRoom(string id)
    {
        this.currentRoom = id;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        this.focusOnRoom(this.moveRate);
    }

    private void focusOnRoom(float rate)
    {
        RoomObject roomObject;
        if (!this.rooms.TryGetValue(this.currentRoom, out roomObject))
        {
            this.transform.position = new Vector3(0, 0, this.transform.position.z);
            return;
        }

        Vector2 target = this.getTargetPos(roomObject);

        float newX = Mathf.Lerp(this.transform.position.x, target.x, rate);
        float newY = Mathf.Lerp(this.transform.position.y, target.y, rate);
        Vector2 newPos = new Vector2(newX, newY);
        if (((Vector2)this.transform.position - newPos).magnitude < 0.01)
        {
            newPos = target;
        }

        this.transform.position = new Vector3(newPos.x, newPos.y, this.transform.position.z);
    }

    private Vector2 getTargetPos(RoomObject room)
    {
        Vector2 dimensions = room.getRoomDimensions();

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * (16f / 9f);

        float extraWidth = Mathf.Max(dimensions.x - this.cameraFreedom, 0);
        float extraHeight = Mathf.Max(dimensions.y - this.cameraFreedom, 0);

        float minX = room.getPosition().x - extraWidth * halfWidth;
        float maxX = room.getPosition().x + extraWidth * halfWidth;
        float minY = room.getPosition().y - extraHeight * halfHeight;
        float maxY = room.getPosition().y + extraHeight * halfHeight;

        Vector2 target = new Vector2(
            Mathf.Clamp(this.player.position.x, minX, maxX),
            Mathf.Clamp(this.player.position.y, minY, maxY));

        return target;
    }

    void OnDestroy()
    {
        EventManager.instance.switchToRoomEvent -= this.changeRoom;
    }
}
