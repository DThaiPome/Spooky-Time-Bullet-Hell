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
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        if (input.magnitude != 0)
        {
            this.direction = input / input.magnitude;
        }

        this.moveInput = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

        this.velocity = (Vector3)this.direction * this.speed * this.moveInput;
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
