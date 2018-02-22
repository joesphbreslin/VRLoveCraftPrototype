using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoat : MonoBehaviour {

    public Transform boat;
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = boat.position;
    }
}
