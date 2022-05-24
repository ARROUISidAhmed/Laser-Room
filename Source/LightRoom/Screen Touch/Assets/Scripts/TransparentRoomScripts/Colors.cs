using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colors : MonoBehaviour {

    public Image image;
    public float nbColor;
    public GameObject[] boutonsCouleur;
    // public GameObject mirror;

    private Vector2 imSize;
    private Vector2 imPos;
    private Vector2 buttonPos;


	// Use this for initialization
	void Start () {
        // Change size of the image depending on the number of colors
        imSize = image.rectTransform.localScale;
        imSize.y = imSize.y * nbColor;
        image.rectTransform.localScale = imSize;
        // Adjust the position of the image depending on the size
        imPos = image.rectTransform.anchoredPosition;
        imPos.y += (nbColor - 1) * 25;
        image.rectTransform.anchoredPosition = imPos;
        // ajout des boutons
        int i = 0;
        foreach(GameObject btC in boutonsCouleur)
        {
            buttonPos = btC.transform.localPosition;
            buttonPos.y += i * 45;
            btC.transform.localPosition = buttonPos;
            i++;
        }

    }
	
	// Update is called once per frame
	void Update () {

    }

}
