using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMenu : MonoBehaviour {

    private int backFromGame;
    public GameObject mainM;
    public GameObject lvlM;

    public void Start()
    {
        backFromGame = PlayerPrefs.GetInt("BackFromGame", 0);
        
    }

    public void Update()
    {
        if (backFromGame != 0)
        {
            mainM.SetActive(false);
            lvlM.SetActive(true);
        }
    }

    public void ReturnMenu()
    {
        backFromGame = 1;
        PlayerPrefs.SetInt("BackFromGame", backFromGame);
        SceneManager.LoadScene("MenuPrincipale");
    }

    public void ResetOnExit()
    {
        backFromGame = 0;
        PlayerPrefs.SetInt("BackFromGame", backFromGame);
    }
}
