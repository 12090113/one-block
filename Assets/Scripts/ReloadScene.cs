using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAgain()
    {
        winScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        loseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
