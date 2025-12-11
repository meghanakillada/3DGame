using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void QuitGame()
    {
        GameManager.SafeQuit();
    }
    void Update()
    {
        
    }
}
