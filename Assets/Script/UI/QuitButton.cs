﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public GameObject lantern;
    private GameObject currentLantern;
    private Image textImage;
    // Start is called before the first frame update
    void Start()
    {
        textImage = GetComponent<Image>();
        textImage.color = Color.gray;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        Application.Quit();
    }

    public void ButtonOnHover()
    {
        Debug.Log("Mouse is over GameObject.");
        currentLantern = Instantiate(lantern, new Vector3(transform.position.x, transform.position.y + 60), Quaternion.identity, transform.parent);
        textImage.color = Color.yellow;
    }

    public void ButtonExitHover()
    {

        Debug.Log("Mouse is Exit.");
        Destroy(currentLantern);
        textImage.color = Color.grey;


    }
}