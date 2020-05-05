using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private string controlMode = "";

    private float moveInput;
    private Vector2 input;
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private Vector2 velocity;
    private bool directionLocked;

    private Rigidbody2D rb;

    public static PlayerMovement player;

    void Awake()
    {
        player = this;
    }

    void Start()
    {
        this.input = new Vector2();
        this.moveDirection = new Vector2();
        this.lookDirection = new Vector2();
        this.velocity = new Vector2();
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();

        EventManager.instance.onPlayerControlModeChangedEvent += this.setControlMode;
    }

    void Update()
    {
        this.updateInputs();
    }

    private void setInputVector()
    {
        switch(this.controlMode)
        {
            case "move left":
                this.input.x = 1;
                this.input.y = 0;
                this.moveInput = 1;
                break;
            case "move right":
                this.input.x = -1;
                this.input.y = 0;
                this.moveInput = 1;
                break;
            case "move up":
                this.input.x = 0;
                this.input.y = 1;
                this.moveInput = 1;
                break;
            case "move down":
                this.input.x = 0;
                this.input.y = -1;
                this.moveInput = 1;
                break;
            default:
                this.input.x = Input.GetAxisRaw("Horizontal");
                this.input.y = Input.GetAxisRaw("Vertical");

                bool atMaxSpeed = this.moveInput == 1;
                bool stopped = this.input.magnitude == 0 && this.noKeysReleased();

                bool inMotion = false;
                if (atMaxSpeed)
                {
                    inMotion = true;
                }
                if (inMotion)
                {
                    if (stopped)
                    {
                        inMotion = false;
                    }
                }

                this.moveInput = inMotion ? 1 : Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));
                break;
        }
    }

    private void setControlMode(string mode)
    {
        this.controlMode = mode;
    }

    private void updateInputs()
    {
        this.setInputVector();

        if (input.magnitude != 0)
        {
            this.moveDirection = input / input.magnitude;
            this.correctDirection();
        }

        this.directionLocked = Input.GetKey(KeyCode.LeftShift);

        if (!this.directionLocked)
        {
            this.lookDirection = this.moveDirection;
        } else if (this.moveInput == 0)
        {
            this.moveDirection = this.lookDirection;
        }

        this.velocity = (Vector3)this.moveDirection * this.speed * this.moveInput;
    }

    private bool noKeysReleased()
    {
        return !(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow));
    }

    private void correctDirection()
    {
        float angle = Mathf.Atan2(this.moveDirection.y, this.moveDirection.x) * Mathf.Rad2Deg;
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
        this.moveDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
    }

    void FixedUpdate()
    {
        this.move();
    }

    private void move()
    {
        Vector2 newPos = (Vector2)this.transform.position + (this.velocity * Time.deltaTime);
        this.rb.MovePosition(newPos);
        this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(this.lookDirection.y, this.lookDirection.x));
    }

    public float getSpeedPercent()
    {
        return Mathf.Abs(this.moveInput);
    }
}
