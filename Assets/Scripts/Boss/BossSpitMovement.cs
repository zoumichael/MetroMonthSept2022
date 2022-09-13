using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpitMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public BoxCollider2D bc;

    [SerializeField] Sprite horizontal;
    [SerializeField] Sprite down;
    [SerializeField] Sprite ground;
    [SerializeField] private float destroyAfter;

    private float leftBound = 47f;
    private float rightBound = 93f;

    private enum MovementState
    {
        GOINGUP,
        GOINGDOWN,
        ONTHEGROUND
    }
    MovementState moveState = MovementState.GOINGUP;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateMovementState();
        destroyAfter -= Time.deltaTime;
        if(destroyAfter < 0)
        {
            Destroy(gameObject);
        }
        DestroyOutOfBounds();
    }

    void DestroyOutOfBounds()
    {
        if (transform.position.x < leftBound || transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
    }

    public void SetTrajectory(float hspeed, float vspeed)
    {
        rb.velocity = new Vector2(hspeed, vspeed);
        if (hspeed > 0)
            sr.flipX = true;
    }

    public void updateMovementState()
    {
        if (rb.velocity.y > 0.5f)
        {
            moveState = MovementState.GOINGUP;
            sr.sprite = down;
            sr.flipY = true;
        }
        else if(rb.velocity.y < -0.5f)
        {
            moveState = MovementState.GOINGDOWN;
            sr.sprite = down;
            sr.flipY = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("landed");
            moveState = MovementState.ONTHEGROUND;
            rb.velocity = new Vector2(0, 0);
            sr.sprite = ground;
            sr.flipY = false;

            bc.size = new Vector2(0.875646f, 0.1140336f);
            bc.offset = new Vector2(-0.007182464f, -0.3630611f);
        }
    }
}
