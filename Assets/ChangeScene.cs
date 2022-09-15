using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private string EndSceneName;

    public static float playerX;
    public static float playerY;
    public static bool change = false;

    [SerializeField] private float setPlayerX;
    [SerializeField] private float setPlayerY;

    [SerializeField] private bool endPortal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(endPortal)
            {
                Debug.Log("Is End Portal");
            }
            if (collision.GetComponent<PlayerMovement>().getEndGame())
            {
                Debug.Log("EDDDD");
            }

            if(endPortal && collision.GetComponent<PlayerMovement>().getEndGame())
            {
                Debug.Log("End Game");
                SceneManager.LoadScene(EndSceneName);
                return;
            }
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
