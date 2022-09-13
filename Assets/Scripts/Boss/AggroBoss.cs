using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBoss : MonoBehaviour
{
    [SerializeField] GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.GetComponent<BossMovement>().Wake();
            Destroy(gameObject);
        }
    }
}
