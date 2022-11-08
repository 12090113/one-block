using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public GameObject canvas,win,lose;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeepGoing()
    {
        win.SetActive(false);
        lose.SetActive(false);
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        Debug.Log("Is in fact pressing");
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene");
    }
}
