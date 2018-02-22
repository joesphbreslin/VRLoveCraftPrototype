using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oar : MonoBehaviour {

    public float speed;
    public GameObject boat;
    Rigidbody boatRB;
    Vector3 previousPos, currentPos;
    public float maxAngularVel = 10;
    public bool useCamera = false;
    [Range(0,5)]
    float vel;
   
    public float maxSpeed = 1f;//Replace with your max speed
    int state;

    private void Start()
    {
       
        boatRB = boat.GetComponent<Rigidbody>();
        boatRB.maxAngularVelocity = maxAngularVel;
        //Vector3 cameraDirection = Camera.main.gameObject.transform.forward;
    }

   
    void FixedUpdate()
    {
        MoveBoat(state);
    }

    void MoveBoat(int mult)
    {
        if (ControllerGrabObject.selectedObject == gameObject.name)
        {
            if (mult == 0)
            {
                return;
            }
            else
            { if (boatRB.velocity.z < maxSpeed && boatRB.velocity.x < maxSpeed)
                {
                    boatRB.AddForce(boat.transform.forward * vel * speed * Time.deltaTime);
                }
                boatRB.AddTorque(Vector3.up * vel * Time.deltaTime * mult);
              
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RightOarTrigger")
        {
            previousPos = transform.position;
        }
        if (other.tag == "LeftOarTrigger")
        {
            previousPos = transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RightOarTrigger")
        {
            currentPos = transform.position;
            vel = (currentPos - previousPos).magnitude / Time.deltaTime;
            state = 1;
            
        }
        if (other.tag == "LeftOarTrigger")
        {
            currentPos = transform.position;
            vel = ((currentPos - previousPos).magnitude / Time.deltaTime);
            state = -1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RightOarTrigger")
        {

        }
        if (other.tag == "LeftOarTrigger")
        {

        }
    }


}
