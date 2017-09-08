using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SlidingDoor : NetworkBehaviour {
    public Vector3 openPos;
    Vector3 basePos;
    public float openTime = 0.5f;
    public bool open = false;
    bool moving = false;
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        basePos = transform.position;
        if (open)
            transform.position = openPos;
    }

    public void UseDoor()
    {
        if (!isServer)
            return;
        if (!moving)
        {
            if(open)
            {
                rb.velocity = (-openPos) / openTime;
            }
            else
            {
                rb.velocity = (openPos) / openTime;
            }
            open = !open;
            moving = true;
            StartCoroutine(FinishOpen());
        }
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + openPos);        
    }

    IEnumerator FinishOpen()
    {
        yield return new WaitForSeconds(openTime);
        rb.velocity = Vector3.zero;
        moving = false;
    }

}
