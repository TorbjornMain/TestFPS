using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerActivationController : NetworkBehaviour {

    RaycastHit rc = new RaycastHit();
    PlayerMovementController pc;

    public float activationDistance = 1;

    void Start()
    {
        pc = GetComponent<PlayerMovementController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;

        if (Physics.Raycast(pc.cam.transform.position, pc.cam.transform.forward, out rc, activationDistance, 1 << LayerMask.NameToLayer("Activatable")))
        {
            rc.collider.SendMessage("Highlight");
            SendMessage("Prompt", rc.collider.gameObject);
            if(Input.GetButtonDown("Activate"))
            {
                CmdActivateObject(pc.cam.transform.position, pc.cam.transform.forward);
            }
        }
        else
        {
            SendMessage("NoPrompt");
        }
	}

    [Command]
    void CmdActivateObject(Vector3 camPos, Vector3 camForward)
    {
        Physics.Raycast(camPos, camForward, out rc, activationDistance, 1 << LayerMask.NameToLayer("Activatable"));
        rc.collider.SendMessage("Activate");
    }
}
