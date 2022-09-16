using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Ak_Player_FootSteps : MonoBehaviour {
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    public void PlayFootstepSound() {
        MyEvent.Post(gameObject);
    }
}