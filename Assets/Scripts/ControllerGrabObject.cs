using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerGrabObject : MonoBehaviour {

    #region Variables
    Animator anim;
    public string grippingAnimation;
    private GameObject collidingObject;
    private GameObject objectInHand;
    private Rigidbody rb;
  
    public string inputString;
    Vector3 previousPos, currentPos;
    Vector3 vel;
    public bool isRight;
    static public string selectedObject;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    #region Collision Logic
    private void SetCollidingObject(Collider col)
    {
        
            if (collidingObject || !col.GetComponent<Rigidbody>())
            {
            return;
            }
            collidingObject = col.gameObject;
     


    }

    private void GrabObject()
    {
     
        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        selectedObject = objectInHand.name.ToString();
    }


    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "NonInteractable")
            SetCollidingObject(other);
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag != "NonInteractable")
            SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "NonInteractable")
        {
            if (!collidingObject)
            {
                return;
            }

        collidingObject = null;
        }
    }

    private void ReleaseObject()
    {
    
        if (GetComponent<FixedJoint>())
        {
         
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
          
            objectInHand.GetComponent<Rigidbody>().velocity = (currentPos - previousPos) / Time.deltaTime;
           
            objectInHand.GetComponent<Rigidbody>().angularVelocity = rb.angularVelocity;
           
        }
        
        objectInHand = null;
        selectedObject = "";
    }

    #endregion
    #region Gripping
    void Grip()
    {
        if (Input.GetButtonDown(inputString))
        {
            anim.SetBool(grippingAnimation, true);
            if (collidingObject)
            {
               
                GrabObject();
            }
        }
        else if (Input.GetButtonUp(inputString))
        {
            anim.SetBool(grippingAnimation, false);
            if (objectInHand)
            {
                
                ReleaseObject();
            }
        }
    }
    #endregion

    void Update () {
        if (isRight) { currentPos = InputTracking.GetLocalPosition(XRNode.RightHand); } else { currentPos = InputTracking.GetLocalPosition(XRNode.LeftHand); }
        Grip();
    }

    private void LateUpdate()
    {
        previousPos = currentPos;
    }
}
