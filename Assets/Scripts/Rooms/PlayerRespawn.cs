using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float respawnX;
    [SerializeField] private float respawnY;

    [SerializeField] private GameObject hpManager;

    private Rigidbody2D rb;

    [SerializeField] private float recoil;

    // After taking damage, prevent you from taking damage after a while.
    [SerializeField] private float damageLockOut;
    private float damageLockOutCounter = 0;
    private bool immune = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        damageLockOutCounter -= Time.deltaTime;
        if(immune && damageLockOutCounter < 0)
        {
            immune = false;
            Physics.IgnoreLayerCollision(8, 13, false);
            Physics.IgnoreLayerCollision(11, 13, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyAttack")) && damageLockOutCounter < 0)
        {
            Debug.Log("Collided with Enemy");
            TakeDamage(collision);
            damageLockOutCounter = damageLockOut;

            immune = true;
            Physics.IgnoreLayerCollision(8, 13);
            Physics.IgnoreLayerCollision(11, 13);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Danger"))
        {
            hpManager.GetComponent<ManageHP>().takeDamage();
            Debug.Log("Collided with Danger");
            //RespawnPlayer();
        }

        if(collision.gameObject.name == "PlatformPowerup")
        {
            GetComponent<PlayerMakePlatform>().SetCanGerminate(true);
            Destroy(collision.gameObject);
        }

        
    }

    


    public void RespawnPlayer()
    {
        rb.transform.position = new Vector3(respawnX, respawnY, 0);
    }

    public void TakeDamage(Collision2D collision)
    {
        // Update HP Bar
        hpManager.GetComponent<ManageHP>().takeDamage();

        if(hpManager.GetComponent<ManageHP>().getCurrHP() <= 0)
        {
            hpManager.GetComponent<ManageHP>().healToFull();
            RespawnPlayer();
            return;
        }

        // Add a recoil effect.
        float XOffset = transform.position.x - collision.gameObject.transform.position.x;
        float YOffset = transform.position.y - collision.gameObject.transform.position.y;
        Debug.Log("X: " + XOffset + " Y: " + YOffset);


        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            if(YOffset < -1.5)
            {
                rb.velocity = new Vector3(XOffset / Mathf.Abs(XOffset) * recoil * 3, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(XOffset / Mathf.Abs(XOffset) * recoil, recoil * 2, 0);
            }
        }
        else
        {
            rb.velocity = new Vector3(XOffset / Mathf.Abs(XOffset) * recoil, YOffset / Mathf.Abs(YOffset) * recoil, 0);
        }

        // Lock Player Movement
        GetComponent<PlayerMovement>().damaged();
    }



    public void SetRespawnX(float val) { respawnX = val; }
    public void SetRespawnY(float val) { respawnY = val; }

}
