using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUps : MonoBehaviour
{
    private PlayerControl player;
    private bool temp = true;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("powerup");
        if (temp)
        {
            player.max_jump ++;
            temp = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "ForeGround")
        {
            Debug.Log("power up");
            player.jump_limit++;
        }


    }
}
