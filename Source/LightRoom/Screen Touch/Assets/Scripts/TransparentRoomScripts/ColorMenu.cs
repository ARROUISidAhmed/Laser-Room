using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMenu : MonoBehaviour {

    public GameObject menu;

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void OpenCloseMenu()
    {
        if(menu.activeInHierarchy == true)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }
}
