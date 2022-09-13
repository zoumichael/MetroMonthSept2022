using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpitMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] Sprite horizontal;
    [SerializeField] Sprite down;
    [SerializeField] Sprite ground;
    [SerializeField] private float destroyAfter;

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
        else
        {
            if(moveState == MovementState.GOINGDOWN)
            {
                Debug.Log("landed");
                moveState = MovementState.ONTHEGROUND;
                rb.velocity = new Vector2(0, 0);
                sr.sprite = ground;
                sr.flipY = false;
            }
        }
        //animator.SetInteger("moveState", (int) moveState);
    }
}
