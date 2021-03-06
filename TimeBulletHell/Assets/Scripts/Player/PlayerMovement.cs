﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private PlayerControlMode controlMode = PlayerControlMode.Default;

    private float moveInput;
    private Vector2 input;
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private Vector2 velocity;
    private bool directionLocked;

    private Rigidbody2D rb;

    public static PlayerMovement player;

    private InputAxis vertical;
    private InputAxis horizontal;
    private InputAxis lockAim;

    void Awake()
    {
        player = this;
        this.vertical = GameInput.input.getAxis("Vertical");
        this.horizontal = GameInput.input.getAxis("Horizontal");
        this.lockAim = GameInput.input.getAxis("LockAim");
    }

    void Start()
    {
        this.input = new Vector2();
        this.moveDirection = new Vector2();
        this.lookDirection = new Vector2();
        this.velocity = new Vector2();
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();

        EventManager.instance.onPlayerControlModeChangedEvent += this.setControlMode;
        EventManager.instance.endLevelEvent += this.onBossDefeated;
    }

    void Update()
    {
        this.updateInputs();
    }

    private void setInputVector()
    {
        switch(this.controlMode)
        {
            case PlayerControlMode.NoMovement:
                this.input = new Vector2();
                this.moveInput = 0;
                break;
            case PlayerControlMode.MoveLeft:
                this.input.x = -1;
                this.input.y = 0;
                this.moveInput = 1;
                break;
            case PlayerControlMode.MoveRight:
                this.input.x = 1;
                this.input.y = 0;
                this.moveInput = 1;
                break;
            case PlayerControlMode.MoveUp:
                this.input.x = 0;
                this.input.y = 1;
                this.moveInput = 1;
                break;
            case PlayerControlMode.MoveDown:
                this.input.x = 0;
                this.input.y = -1;
                this.moveInput = 1;
                break;
            default:
                this.input.x = this.horizontal.rawAxis;
                this.input.y = this.vertical.rawAxis;

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

                this.moveInput = inMotion ? 1 : Mathf.Max(Mathf.Abs(this.horizontal.axis), Mathf.Abs(this.vertical.axis));
                break;
        }
    }

    private void setControlMode(PlayerControlMode mode)
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

        this.directionLocked = this.lockAim.positive();

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
        return !(this.vertical.positiveUp() ||
            this.vertical.negativeUp() ||
            this.horizontal.negativeUp()||
            this.horizontal.positiveUp());
    }

    private void correctDirection()
    {
        float angle = Mathf.Atan2(this.moveDirection.y, this.moveDirection.x) * Mathf.Rad2Deg;
        switch (angle)
        {
            case 0:
                if (this.vertical.positiveUp())
                {
                    angle = 45;
                } else if (this.vertical.negativeUp())
                {
                    angle = -45;
                }
                break;
            case 90:
                if (this.horizontal.negativeUp())
                {
                    angle = 135;
                }
                else if (this.horizontal.positiveUp())
                {
                    angle = 45;
                }
                break;
            case 180:
                if (this.vertical.positiveUp())
                {
                    angle = 135;
                }
                else if (this.vertical.negativeUp())
                {
                    angle = -135;
                }
                break;
            case -90:
                if (this.horizontal.negativeUp())
                {
                    angle = -135;
                }
                else if (this.horizontal.positiveUp())
                {
                    angle = -45;
                }
                break;
        }
        this.moveDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
    }

    private void onBossDefeated()
    {
        EventManager.instance.onPlayerControlModeChanged(PlayerControlMode.NoMovement);
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

    void OnDestroy()
    {
        EventManager.instance.onPlayerControlModeChangedEvent -= this.setControlMode;
        EventManager.instance.onBossDefeatedEvent -= this.onBossDefeated;
    }
}

public enum PlayerControlMode
{
    Default, MoveRight, MoveUp, MoveLeft, MoveDown, NoMovement
}
