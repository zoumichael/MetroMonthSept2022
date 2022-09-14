using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDew : MonoBehaviour
{
    public GameObject dewPrefab;

    [SerializeField] private float dewInitialY;
    [SerializeField] private float dewXRange;

    public void SpawnRandomDew(int val)
    {
        Vector2 loc = new Vector2(transform.position.x, transform.position.y + 1f);
        GameObject newDew = Instantiate(dewPrefab, loc, Quaternion.identity);
        newDew.GetComponent<DewMain>().InitializeDew(val, Random.Range(-dewXRange, dewXRange), dewInitialY);
    }
}
