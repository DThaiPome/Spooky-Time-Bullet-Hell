using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float moveInput;
    [SerializeField]
    private Vector2 input;
    private Vector2 direction;
    private float dirAngle;
    private Vector2 velocity;

    private Rigidbody2D rb;

    void Start()
    {
        this.input = new Vector2();
        this.direction = new Vector2();
        this.velocity = new Vector2();
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.updateInputs();
    }

    private void updateInputs()
    {
        this.input.x = this.horizontalInput();
        this.input.y = this.verticalInput();
        if (input.magnitude != 0)
        {
            this.direction = input / input.magnitude;
            this.correctDirection();
        }

        this.moveInput = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

        this.velocity = (Vector3)this.direction * this.speed * this.moveInput;
    }

    private float horizontalInput()
    {
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        return left || right ? (right ? 1 : -1) : 0;
    }

    private float verticalInput()
    {
        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        return up || down ? (up ? 1 : -1) : 0;
    }

    private void correctDirection()
    {
        float angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
        switch (angle)
        {
            case 0:
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    angle = 45;
                } else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    angle = -45;
                }
                break;
            case 90:
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    angle = 135;
                }
                else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    angle = 45;
                }
                break;
            case 180:
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    angle = 135;
                }
                else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    angle = -135;
                }
                break;
            case -90:
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    angle = -135;
                }
                else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    angle = -45;
                }
                break;
        }
        this.direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
    }

    void FixedUpdate()
    {
        this.move();
    }

    private void move()
    {
        Vector2 newPos = (Vector2)this.transform.position + (this.velocity * Time.deltaTime);
        this.rb.MovePosition(newPos);
        this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(this.direction.y, this.direction.x));
    }

    public float getSpeedPercent()
    {
        return Mathf.Abs(this.moveInput);
    }
}
