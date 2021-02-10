using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;



public class CanvasManagement : MonoBehaviour
{
    private Image[] images;
    public Image h1, h2, h3, h4, h5, h6, h7, h8, h9, h10;
    private GameObject player;
    public GameObject popup;
    public GameObject powerUpsBoard;
    public Text text;
    private PlayerControl pc;
    public float startTime;
    public float time_popup;
    public Button b1, b2, b3;
    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        images = new Image[] { h1, h2, h3, h4, h5, h6, h7,h8,h9,h10 };
        buttons = new Button[] { b1, b2, b3 };
        startTime = 0.0f;
        powerUpsBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            pc = player.GetComponent<PlayerControl>();
        }
        for (int i = 0; i < images.Length; i++)
        {
            // Hide all images superior to the newHealth
            if (i >= pc.hp)
                images[i].enabled = false;
            else
                images[i].enabled = true;
        }

        if (startTime > 0.0f)
        {
            startTime -= Time.deltaTime;
        }
        else {
            popup.SetActive(false);
        }
    }
    public void PopupMessage(string s)
    {
        popup.SetActive(true);
        text.text = s;
        startTime = time_popup;
    }
    public void PowerUpButtonInitial(List<string> powerUps)
    {
        powerUpsBoard.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerControl>().PauseGame();
        List<string> list_new = new List<string>();
        foreach (string s in powerUps)
        {
            list_new.Add(s);
        }

        foreach (Button b in buttons)
        {
            Debug.Log("There are " + list_new.Count + " powers ups");
            int i = Random.Range(0, list_new.Count);
            b.GetComponent<PowerUpButtonControl>().text = list_new[i];
            b.GetComponent<PowerUpButtonControl>().SetImage();
            list_new.RemoveAt(i);
        }
    }
}
