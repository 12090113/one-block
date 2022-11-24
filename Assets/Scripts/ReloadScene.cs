using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public GameObject canvas,win,lose,menu,stats,help,pause;
    public bool ingame = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ingame && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void End(bool won)
    {
        if(won)
            win.SetActive(true);
        else
            lose.SetActive(true);
        Time.timeScale = 0;
        ingame = false;
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        stats.SetActive(true);
        Time.timeScale = 1;
        ingame = true;
    }

    public void KeepGoing()
    {
        win.SetActive(false);
        lose.SetActive(false);
        Time.timeScale = 1;
        ingame = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Scene");
        ingame = false;
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

    public void TogglePause()
    {
        if (pause.activeSelf == true)
        {
            pause.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pause.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
