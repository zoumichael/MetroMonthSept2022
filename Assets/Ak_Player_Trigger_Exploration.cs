using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak_Player_Trigger_Exploration : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        AkSoundEngine.PostEvent("Test", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
