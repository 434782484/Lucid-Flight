using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    string name;
    float time; 
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        name = scene.name;
        time = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the name of the current Active Scene is your first Scene.
        if (name == "Ending")
        {
            time -= Time.deltaTime;
            if (time <= 0.0f) {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        Debug.Log("You have clicked the button!");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
