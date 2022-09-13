using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloorSpitMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    public SpriteRenderer sr;
    public BoxCollider2D bc;

    [SerializeField] Sprite horizontal;
    [SerializeField] Sprite down;
    [SerializeField] Sprite ground;

    [SerializeField] private float destroyAfter;

    private bool onTheGround = false;
    [SerializeField] private float destroyAfterOnGround;

    [SerializeField] private float timeToSpawnLeftRight;
    private float spawnCounter;
    private bool spawned = false;
    private bool spawnLeft = false;
    private bool spawnRight = false;

    [SerializeField] private GameObject spitFloorAttack;

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
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateMovementState();
        if (!onTheGround)
        {
            destroyAfter -= Time.deltaTime;
            if (destroyAfter < 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            destroyAfterOnGround -= Time.deltaTime;
            if(destroyAfterOnGround < 0)
            {
                Destroy(gameObject);
            }
            spawnCounter += Time.deltaTime;
            if(spawnCounter > timeToSpawnLeftRight)
            {
                if (!spawned)
                {
                    if (spawnLeft)
                        ExpandLeft();
                    else if (spawnRight)
                        ExpandRight();
                    else
                        ExpandLeftRight();
                    spawned = true;
                }
            }
        }
        DestroyOutOfBounds();
    }

    void DestroyOutOfBounds()
    {
        if(transform.position.x < leftBound || transform.position.x > rightBound)
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
                /*
                Debug.Log("landed");
                onTheGround = true;
                moveState = MovementState.ONTHEGROUND;
                rb.velocity = new Vector2(0, 0);
                sr.sprite = ground;
                sr.flipY = false;

                bc.size = new Vector2(0.875646f, 0.1140336f);
                bc.offset = new Vector2(-0.007182464f, -0.3630611f);
                */
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Debug.Log("landed");
            onTheGround = true;
            moveState = MovementState.ONTHEGROUND;
            rb.velocity = new Vector2(0, 0);
            sr.sprite = ground;
            sr.flipY = false;

            bc.size = new Vector2(0.875646f, 0.1140336f);
            bc.offset = new Vector2(-0.007182464f, -0.3630611f);
        }
    }

    void ExpandLeftRight()
    {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 spitSpawnLeft = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
        GameObject left = Instantiate(spitFloorAttack, spitSpawnLeft, Quaternion.identity);
        left.GetComponent<BossFloorSpitMovement>().SetDestroyAfterOnGround(destroyAfterOnGround);
        left.GetComponent<BossFloorSpitMovement>().SetSpawnLeft(true);

        Vector3 spitSpawnRight = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
        GameObject right = Instantiate(spitFloorAttack, spitSpawnRight, Quaternion.identity);
        right.GetComponent<BossFloorSpitMovement>().SetDestroyAfterOnGround(destroyAfterOnGround);
        right.GetComponent<BossFloorSpitMovement>().SetSpawnRight(true);
    }

    void ExpandLeft()
    {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 spitSpawnLeft = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
        GameObject left = Instantiate(spitFloorAttack, spitSpawnLeft, Quaternion.identity);
        left.GetComponent<BossFloorSpitMovement>().SetDestroyAfterOnGround(destroyAfterOnGround);
        left.GetComponent<BossFloorSpitMovement>().SetSpawnLeft(true);
    }

    void ExpandRight()
    {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 spitSpawnRight = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
        GameObject right = Instantiate(spitFloorAttack, spitSpawnRight, Quaternion.identity);
        right.GetComponent<BossFloorSpitMovement>().SetDestroyAfterOnGround(destroyAfterOnGround);
        right.GetComponent<BossFloorSpitMovement>().SetSpawnRight(true);
    }

    void SetDestroyAfterOnGround(float val)
    {
        onTheGround = true;
        destroyAfterOnGround = val;
    }

    void SetSpawnLeft(bool val)
    {
        spawnLeft = val;
    }

    void SetSpawnRight(bool val)
    {
        spawnRight = val;
    }
}
