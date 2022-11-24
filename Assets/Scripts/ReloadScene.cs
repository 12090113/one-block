using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public GameObject canvas,win,lose,menu,stats,help;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        stats.SetActive(true);
        Time.timeScale = 1;
    }

    public void KeepGoing()
    {
        win.SetActive(false);
        lose.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Scene");
    }

    public void ToggleHelp()
    {
        if (help.activeSelf == true)
        {
            help.SetActive(false);
        } else
        {
            help.SetActive(true);
        }
    }
}
