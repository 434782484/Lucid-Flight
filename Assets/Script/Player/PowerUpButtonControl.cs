using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUpButtonControl : MonoBehaviour
{
    //public Text text;
    public string text;
    private Image image;
    public GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        Debug.Log("I click A BUTTON");
        Debug.Log(text);
        if (text == "Speed Up")
        {
            GameObject.Find("Player").GetComponent<PlayerControl>().PowerUpSpeedUp();
        }
        else if (text == "Melee ATK Boost") { GameObject.Find("Player").GetComponent<PlayerControl>().PowerUpRangedATK(); }
        else if (text == "Double Jump") { GameObject.Find("Player").GetComponent<PlayerControl>().PowerUpJump(); }
        else if (text == "MAX HP Boost") { GameObject.Find("Player").GetComponent<PlayerControl>().PowerUpHpBoost(); }
        GameObject.Find("Player").GetComponent<PlayerControl>().ResumeGame();
        popup.SetActive(false);
    }
    public void SetImage() {
        Sprite[] ps = Resources.LoadAll<Sprite>("Art3/Beta/Powerup_Elements");
        if (text == "Speed Up")
        {
            image.sprite = ps[3];
        }
        else if (text == "Melee ATK Boost") { image.sprite = ps[1]; }
        else if (text == "Double Jump") { image.sprite = ps[4]; }
        else if (text == "MAX HP Boost") { image.sprite = ps[2]; }
    }

}