using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Ak_Music_Combat_States : MonoBehaviour {
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    public void Music_Combat_State() {
        MyEvent.Post(gameObject);
    }
}