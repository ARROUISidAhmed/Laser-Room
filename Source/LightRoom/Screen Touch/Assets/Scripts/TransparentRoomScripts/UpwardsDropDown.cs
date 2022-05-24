using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpwardsDropDown : MonoBehaviour {

    public string[] colors;
    public DropDown dropDown;

	// Use this for initialization
	void Start () {
        foreach (string color in colors)
        {
            dropDown.GetComponent(color);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // add 20 for each button added to "Pos Y"
}
