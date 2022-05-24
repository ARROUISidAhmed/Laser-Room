using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopNiveau : MonoBehaviour {

    public int lvl;
    public Transform button;
    private int topLvl;


    void Start () {
        topLvl = PlayerPrefs.GetInt("TopLvl", 1);
	}

    private void Update()
    {
        if (lvl <= topLvl)
        {
            button.GetComponent<Button>().interactable = true;
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
        }
    }


    public void passLvl()
    {
        if (lvl >= topLvl)
        {
            topLvl++;
            PlayerPrefs.SetInt("TopLvl", topLvl);
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("TopLvl");
    }

    /* fonction de test a delet. */
    public void LvlUp()
    {
        topLvl++;
        PlayerPrefs.SetInt("TopLvl", topLvl);
    }

    public void loadNextLevel()
    {
        int nextLvl = lvl + 1;
        passLvl();
        SceneManager.LoadScene(nextLvl);
    }

    

}
