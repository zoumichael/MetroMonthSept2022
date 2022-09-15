using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUnlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMakePlatform>().SetCanGerminate(true);
            collision.GetComponent<PlayerMovement>().setEndGame();
            Destroy(gameObject);
        }
    }
}
