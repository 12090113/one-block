using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    public GameObject destroy;
    // Start is called before the first frame update
    void Start()
    {
        destroy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAgain()
    {
        destroy.SetActive(false);
        Time.timeScale = 1;
    }
}
