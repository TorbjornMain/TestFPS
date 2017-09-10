using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementController : NetworkBehaviour {

    public float speed = 10;
    public Vector3 cameraOffset = new Vector3(0, 0.7f, 0);
    [Range(0, 1)]
    public float accelerationRate = 0.9f;
    public float airControl = 0.1f;
    public float height = 0.5f;
    public float radius = 0.5f;
    public float jumpForce = 10;
    float camPitch = 0;
    public float angLimit = 80;
    public float turnRate = 45;
    bool grounded = false;
    Rigidbody rb;
    RaycastHit rc = new RaycastHit();
    public GameObject cameraPrefab;
    [System.NonSerialized]
    public GameObject cam;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (!isLocalPlayer)
            return;
        cam = Instantiate(cameraPrefab);
        cam.transform.SetParent(transform);
        cam.transform.localPosition = cameraOffset;

    }
	

	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if(Physics.Raycast(transform.position, -transform.up, out rc, height))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if(grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        camPitch = Mathf.Clamp(camPitch + (-Input.GetAxis("Mouse Y") * turnRate * Time.deltaTime), -angLimit, angLimit);
        
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnRate * Time.deltaTime, 0);
        cam.transform.localRotation = Quaternion.Euler(camPitch, 0, 0);

        input *= speed;
        input.y = 0;
        rb.AddForce(((transform.rotation * input) - new Vector3(rb.velocity.x, 0, rb.velocity.z)) * accelerationRate * (grounded ? 1 : airControl), ForceMode.VelocityChange);
        rb.angularVelocity = Vector3.zero;
	}
}
