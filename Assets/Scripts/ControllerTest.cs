using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

    Animator anim;
    public string grippingAnimation;
    public string inputString;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	

    void Grip(bool grip)
    {
        if (grip)
        {
            anim.SetBool(grippingAnimation, true);
        }
        else
        {
            anim.SetBool(grippingAnimation, false);
        }
    }
	// Update is called once per frame
	void Update () {
        Grip(Input.GetButton(inputString));

        //Vector3 leftPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        //Quaternion leftRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
    }
}
