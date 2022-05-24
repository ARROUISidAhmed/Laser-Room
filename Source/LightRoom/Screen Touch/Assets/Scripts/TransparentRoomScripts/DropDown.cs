using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour {

    public RectTransform container;
    public bool isOpen;

	// Use this for initialization
	void Start () {
        container = transform.Find("Container").GetComponent<RectTransform>();
        isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 scale = container.localScale;
        scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
        container.localScale = scale;
	}

    // add on click to change bool
    public void OpenClose()
    {
        if (isOpen)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }
    }
}
