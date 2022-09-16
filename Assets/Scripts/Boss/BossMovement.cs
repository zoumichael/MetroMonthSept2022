using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Other Components
    Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer sprite;

    // Boss HP
    [SerializeField] private float maxHP;
    public float currHP;
    [SerializeField] private float hpThreshold1;
    [SerializeField] private float hpThreshold2;

    // Boss Attack Parameters
    private float attackCounter;
    [SerializeField] private float attackCounterMax;

    // Dash Attack
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private bool dashing = false;

    // Player
    [SerializeField] private GameObject player;

    // Spit Attack
    [SerializeField] private GameObject spitAttack;
    [SerializeField] private GameObject spitFloorAttack;

    bool faceRight = false;

    bool attacking = false;

    int spitType = 1;

    bool aggroed = false;

    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;

    [SerializeField] private float flashDuration = 0.2f;
    private float flashCounter = 0.0f;

    private enum MovementState
    {
        IDLE,
        SPIT,
        DASH,
        SLEEP,
        WAKE
        //idle=0,spit=1,dash=2,sleep=3
    }
    private MovementState moveState = MovementState.SLEEP;

    private void Start()
    {
        currHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aggroed)
        {
            if (currHP < hpThreshold1)
            {
                UpdateHighHP();
            }
            else if (currHP < hpThreshold2)
            {
                UpdateHighHP();
            }
            else
            {
                UpdateHighHP();
            }
            if (!dashing)
                UpdateDirection();
        }
        animator.SetInteger("moveState", (int) moveState);
        if (flashCounter > 0f)
            flashCounter -= Time.deltaTime;
        else
            sprite.color = new Color(255, 255, 255);

        KeepInBounds();
    }

    void KeepInBounds()
    {
        if (transform.position.x < leftBound)
        {
            transform.position = new Vector2(leftBound, transform.position.y);
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector2(rightBound, transform.position.y);
        }
    }

    void UpdateDirection()
    {
        if(player.transform.position.x > transform.position.x)
        {
            faceRight = true;
            sprite.flipX = true;
        }
        else
        {
            faceRight = false;
            sprite.flipX = false;
        }
    }

    void UpdateLowHP()
    {
        attackCounter += Time.deltaTime;
        if(attackCounter > attackCounterMax)
        {
            attackCounter = 0;
        }
    }

    void UpdateMidHP()
    {
        attackCounter += Time.deltaTime;
        if (attackCounter > attackCounterMax)
        {
            attackCounter = 0;
            
        }
    }

    void UpdateHighHP()
    {
        if(!attacking || dashing)
            attackCounter += Time.deltaTime; 

        if (!dashing && attackCounter > attackCounterMax)
        {
            attacking = true;
            int attackType = Random.Range(0, 3);
            //int attackType = 2;

            if(attackType == 0)
            {
                Debug.Log("Dash Attack!");
                DashTowardsPlayer();
                attackCounter = 0;
            }
            else if (attackType == 1)
            {
                Debug.Log("Spit Attack!");
                moveState = MovementState.SPIT;
                spitType = 0;
                //SpitAttack();
                attackCounter = 0;
            }
            else if (attackType == 2)
            {
                Debug.Log("Floor Attack!");
                moveState = MovementState.SPIT;
                spitType = 1;
                attackCounter = 0;
            }
        }

        if (attackCounter > dashTime)
        {
            StopDashing();
            attackCounter = 0;
        }
    }

    void DashTowardsPlayer()
    {
        dashing = true;
        if(player.transform.position.x > transform.position.x)
        {
            Debug.Log("Dash Right. Player: " + player.transform.position.x + " Me: " + transform.position.x);
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
        else
        {
            Debug.Log("Dash Left");
            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
        }
        moveState = MovementState.DASH;
    }

    void StopDashing()
    {
        dashing = false;
        attacking = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        moveState = MovementState.IDLE;
    }

    void SpitAttack()
    {
        Vector3 spitSpawnLocation = new Vector3(transform.position.x, transform.position.y + 2f , transform.position.z);

        GameObject temp;
        if (spitType == 0)
        {
            temp = Instantiate(spitAttack, spitSpawnLocation, Quaternion.identity);
            if (faceRight)
            {
                temp.GetComponent<BossSpitMovement>().SetTrajectory(10f, 5f);
            }
            else
            {
                temp.GetComponent<BossSpitMovement>().SetTrajectory(-10f, 5f);
            }
        }
        else if (spitType == 1) 
        { 
            temp = Instantiate(spitFloorAttack, spitSpawnLocation, Quaternion.identity);
            if (faceRight)
            {
                temp.GetComponent<BossFloorSpitMovement>().SetTrajectory(5f, 5f);
            }
            else
            {
                temp.GetComponent<BossFloorSpitMovement>().SetTrajectory(-5f, 5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Debug.Log("Boss Hit");
            currHP -= 10;
            if (currHP <= 0)
            {
			AkSoundEngine.PostEvent("Boss1_Die", gameObject);
                Destroy(gameObject);
            }
            Destroy(collision);
            sprite.color = new Color(255, 0, 0);
            flashCounter = flashDuration;
        }
    }

    public void TEST()
    {
        Debug.Log("TESTING");
    }

    public void StopAttack()
    {
        Debug.Log("Attack Stopped");
        attacking = false;
        moveState = MovementState.IDLE;
    }

    public void Wake()
    {
        moveState = MovementState.WAKE;
    }

    public void Aggro()
    {
        aggroed = true;
    }
}
