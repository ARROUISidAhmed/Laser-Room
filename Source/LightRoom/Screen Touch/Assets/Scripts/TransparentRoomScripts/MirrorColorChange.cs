using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorColorChange : MonoBehaviour {

    public GameObject mirror;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeColor()
    {
        Transform childMirror;
        Transform secondChild;
        // retirer toutes les couleurs du miroir pour n'afficher que la bonne
        setAllFalse();
        childMirror = mirror.transform.Find("MirrorTop");
        if (gameObject.tag == "boutonBlanc")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreen");
            secondChild.gameObject.SetActive(true);
        }
        else if (gameObject.tag == "boutonRouge")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreenRed");
            secondChild.gameObject.SetActive(true);
        }
        else if (gameObject.tag == "boutonVert")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreenGreen");
            secondChild.gameObject.SetActive(true);
        }
        else if (gameObject.tag == "boutonBleu")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreenBlue");
            secondChild.gameObject.SetActive(true);
        }
        else if (gameObject.tag == "boutonJaune")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreenYellow");
            secondChild.gameObject.SetActive(true);
        }
        else if (gameObject.tag == "boutonCyan")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreenCyan");
            secondChild.gameObject.SetActive(true);
        }
        else if (gameObject.tag == "boutonMagenta")
        {
            secondChild = childMirror.gameObject.transform.Find("MirrorScreenMagenta");
            secondChild.gameObject.SetActive(true);
        }
    }

    private void setAllFalse()
    {
        Transform childMirror;
        Transform secondChild;
        childMirror = mirror.transform.Find("MirrorTop");
        secondChild = childMirror.gameObject.transform.Find("MirrorScreen");
        secondChild.gameObject.SetActive(false);
        secondChild = childMirror.gameObject.transform.Find("MirrorScreenRed");
        secondChild.gameObject.SetActive(false);
        secondChild = childMirror.gameObject.transform.Find("MirrorScreenBlue");
        secondChild.gameObject.SetActive(false);
        secondChild = childMirror.gameObject.transform.Find("MirrorScreenGreen");
        secondChild.gameObject.SetActive(false);
        secondChild = childMirror.gameObject.transform.Find("MirrorScreenYellow");
        secondChild.gameObject.SetActive(false);
        secondChild = childMirror.gameObject.transform.Find("MirrorScreenCyan");
        secondChild.gameObject.SetActive(false);
        secondChild = childMirror.gameObject.transform.Find("MirrorScreenMagenta");
        secondChild.gameObject.SetActive(false);
    }
}
