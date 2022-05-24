using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject[] menus;
    public GameObject mainMenu;
    public GameObject endMenu;

    public void goBackToMain()
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        mainMenu.SetActive(true);
    }

    public void endGameMenu()
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        mainMenu.SetActive(true);
        endMenu.SetActive(true);
    }
}
