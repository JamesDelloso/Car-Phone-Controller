using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float steeringSensitivity;

    public bool isAccelerating = false;
    public bool isBraking = false;
    private bool isGrounded = true;
    public Vector3 userAcceleration;
    public float drift;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded && PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (isAccelerating)
            {
                rigidbody.drag = 0;
                rigidbody.AddForce(rigidbody.transform.forward * acceleration, ForceMode.Force);
            }
            if (isBraking)
            {
                rigidbody.drag = 0.35f;
            }
            else if(!isAccelerating && !isBraking)
            {
                rigidbody.drag = 0;
            }
            Vector3 velocity = rigidbody.transform.InverseTransformDirection(rigidbody.velocity);
            velocity.y = 0;
            velocity.x = velocity.x * drift;
            velocity.z = Mathf.Clamp(velocity.z, 0, maxSpeed);
            rigidbody.velocity = rigidbody.transform.TransformDirection(velocity);
            transform.eulerAngles += new Vector3(0, userAcceleration.x * steeringSensitivity, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    public void restart()
    {
        rigidbody.transform.position = new Vector3(0, -0.01701516f, 25);
        rigidbody.transform.eulerAngles = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
    }
}
