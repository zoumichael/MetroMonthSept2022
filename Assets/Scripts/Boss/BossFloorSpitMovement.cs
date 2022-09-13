using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloorSpitMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    public SpriteRenderer sr;

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
                onTheGround = true;
                moveState = MovementState.ONTHEGROUND;
                rb.velocity = new Vector2(0, 0);
                sr.sprite = ground;
                sr.flipY = false;
            }
        }
        //animator.SetInteger("moveState", (int) moveState);
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
