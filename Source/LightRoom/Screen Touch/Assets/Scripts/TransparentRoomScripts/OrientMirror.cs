using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientMirror : MonoBehaviour {

    public int maxAngle;
    public int minAngle;
    private float angle;
    private Vector3 originalAngle;
    /* start modif */
    public Button buttonR;
    public Button buttonL;
    /* end modif */

    public GameObject miror;

    private void Start()
    {
        angle = 0;
        originalAngle = miror.transform.eulerAngles;

    }

    private void Update()
    {
        if(angle >= maxAngle)
        {
            buttonR.GetComponent<Button>().interactable = false;
            
        }
        else
        {
            buttonR.GetComponent<Button>().interactable = true;
        }
        if (angle <= minAngle)
        {
            buttonL.GetComponent<Button>().interactable = false;

        }
        else
        {
            buttonL.GetComponent<Button>().interactable = true;
        }
    }


    public void RotateRight()
    {
        if (angle < maxAngle)
        {
            miror.transform.Rotate(Vector3.down * 10f);
            angle += 10f;
        }
    }

    public void RotateLeft()
    {
        if (angle > minAngle)
        {
            miror.transform.Rotate(Vector3.up * 10f);
            angle -= 10f;
        }
    }

    public void Center()
    {
        miror.transform.eulerAngles = originalAngle;
        angle = 0f;
    }



}
