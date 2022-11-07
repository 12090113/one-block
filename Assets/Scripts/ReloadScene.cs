using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAgain()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
}
