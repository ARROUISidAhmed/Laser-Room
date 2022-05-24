using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    public float speed = 1f;

	void Update () {
		transform.position += transform.forward * Time.deltaTime  * speed;
	}

}
