using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerThirdPersonMovementController : NetworkBehaviour {


    //Camera Properties
    public GameObject cameraPrefab;
    public Vector3 cameraOffsetDirection = new Vector3(0, 1, -1);
    public float cameraDistance = 10;
    public float camMinDist = 0.1f;
    
    //Movement Properties
    [Range(0, 1)]
    public float accelerationRate = 0.9f;
    public float airControl = 0.1f;
    public float height = 1.2f;
    public float jumpForce = 10;
    
    //Aiming Properties
    public float angLimit = 80;
    public float turnRate = 45;
    public float speed = 10;

    float camPitch = 0;
    bool grounded = false;
    Rigidbody rb;
    RaycastHit rc = new RaycastHit();
    

    //Public camera reference for other scripties
    [System.NonSerialized]
    public GameObject cam;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (!isLocalPlayer)
            return;
        cam = Instantiate(cameraPrefab);
        cam.transform.SetParent(transform);
    }
	

	// Update is called once per frame
	void Update () {
        //Only update on the local players machine
        if (!isLocalPlayer)
            return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        //if we are on the ground
        if(Physics.Raycast(transform.position, -transform.up, out rc, height, 1<<LayerMask.NameToLayer("Terrain")))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        //do a jump thing
        if(grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }


        //set the camera pitch to a clamped angle based on mouse movement
        camPitch = Mathf.Clamp(camPitch + (-Input.GetAxis("Mouse Y") * turnRate * Time.deltaTime), -angLimit, angLimit);
        
        //rotate character and camera based on mouse movement
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnRate * Time.deltaTime, 0);
        cam.transform.localRotation = Quaternion.Euler(camPitch, 0, 0);

        //spring arm calculations
        float dist = cameraDistance;
        if(Physics.Raycast(transform.position, Quaternion.Euler(camPitch, transform.eulerAngles.y, 0) * cameraOffsetDirection.normalized, out rc, cameraDistance))
        {
            //if we hit something with our raycast, move the camera in towards the player, unless the object is too close
            dist = rc.distance < camMinDist ? dist : rc.distance;
            
        }
        //move the camera to the spring arm position
        cam.transform.localPosition = cam.transform.localRotation * cameraOffsetDirection.normalized * dist;

        //move the character based on input
        input *= speed;
        input.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, transform.rotation * input, accelerationRate * (grounded ? 1 : airControl));
        rb.angularVelocity = Vector3.zero;
	}
}
