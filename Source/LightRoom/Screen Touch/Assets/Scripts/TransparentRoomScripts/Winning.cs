using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    public GameObject exitMenu;

    public void Win()
    {
        exitMenu.SetActive(true);
    }
}
