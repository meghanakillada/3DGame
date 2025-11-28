using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneChangeButton : MonoBehaviour
{
    [Tooltip("The name of the scene that the button will take the player to")]
    public string goalScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        //Switches to the goal scene
        if (goalScene != null)
        {
            SceneManager.LoadScene(goalScene);
        }
    }
}
