using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ActivatableItem : NetworkBehaviour {

    public UnityEvent ActivateEvent;

    void Activate()
    { 
        if(ActivateEvent != null)
        {
            ActivateEvent.Invoke();
        }
    }
}
