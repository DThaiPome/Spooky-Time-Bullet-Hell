using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingMole : MobBehaviour
{
    [SerializeField]
    private float moveSpeed = 3;

    private Rigidbody2D rb;

    private Vector2 destination;

    protected override void onEnable()
    {
        base.onEnable();
        this.InitDestination();
    }

    private void InitDestination()
    {
        float maxDistance = 1000;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Wall"));
        filter.useTriggers = false;
        if (Physics2D.Raycast(this.transform.position, this.transform.right, filter, hits, maxDistance) > 0)
        {
            this.destination = hits[0].point;
        }
    }

    protected override void start()
    {
        base.start();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    protected override void update()
    {
        base.update();
        this.FaceDestination();
        this.MoveForward();
        this.OnArrive();
    }

    private void FaceDestination()
    {
        Vector2 toDestination = (this.destination - (Vector2)this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, toDestination);
        Quaternion fixRotation = Quaternion.AngleAxis(90, Vector3.forward);

        this.transform.rotation = lookRotation * fixRotation;
    }

    private void MoveForward()
    {
        Vector2 offset = this.transform.right * this.moveSpeed * Time.deltaTime;
        Vector2 newPos = (Vector2)this.transform.position + offset;

        this.rb.MovePosition(newPos);
    }

    private void OnArrive()
    {
        float maxDistance = .1f;
        if (Vector2.Distance((Vector2)this.transform.position, destination) <= maxDistance)
        {
            this.BurrowIntoWall();
        }
    }

    private void BurrowIntoWall()
    {
        this.gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        if (this.destination != null)
        {
            Gizmos.DrawWireSphere(this.destination, 1);
        }
    }
}
