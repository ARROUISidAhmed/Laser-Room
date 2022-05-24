using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollow : MonoBehaviour {

    public Transform target;
    public float followSpeed = 1f;
    private Transform myTransform;
	void Start () {
        myTransform = transform;
	}
	
	void LateUpdate () {
        myTransform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - myTransform.position), followSpeed * Time.deltaTime);
	}
}
