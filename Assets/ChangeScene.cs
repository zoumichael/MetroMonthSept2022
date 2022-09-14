using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string SceneName;

    public static float playerX;
    public static float playerY;
    public static bool change = false;

    [SerializeField] private float setPlayerX;
    [SerializeField] private float setPlayerY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerX = setPlayerX;
            playerY = setPlayerY;
            change = true;
            SceneManager.LoadScene(SceneName);            
        }
    }

    public float getPlayerX() { return playerX; }
    public float getPlayerY() { return playerY; }
    public bool getChange() { return change; }
}
