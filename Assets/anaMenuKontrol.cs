using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol : MonoBehaviour
{
    GameObject level1, level2, level3;
    GameObject lock1, lock2, lock3;
    GameObject leveller;
    void Start()
    {
        level1 = GameObject.Find("level 1");
        level2 = GameObject.Find("level 2");
        level3 = GameObject.Find("level 3");

        lock1 = GameObject.Find("lock1");
        lock2 = GameObject.Find("lock2");
        lock3 = GameObject.Find("lock3");

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        lock1.SetActive(false);
        lock2.SetActive(false);
        lock3.SetActive(false);


        leveller = GameObject.Find("leveller");
        

        for(int i = 0; i < PlayerPrefs.GetInt("kacinciLevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void buttonSec(int gelenButon)
    {
        if (gelenButon== 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (gelenButon == 2)
        {
            level1.SetActive(true);
            level2.SetActive(true);
            level3.SetActive(true);

            lock1.SetActive(true);
            lock2.SetActive(true);
            lock3.SetActive(true);

        }
        else if (gelenButon == 3)
        {
            Application.Quit();
        }
    }

    
    
}
