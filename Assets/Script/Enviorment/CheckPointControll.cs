using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<PlayerControl>();
            player.hp = player.max_hp;
            player.isOnLevel2 = true;
            player.hp = player.max_hp;
            player.lv1playerStat = new PlayerStat()
            {
                max_hp = player.max_hp,
                hp = player.max_hp,
                jump_limit = player.jump_limit,
                max_jump = player.max_jump,
                melee_ATK = player.melee_ATK,
                powerUpNum = player.powerUpNum,
                powerUp_max = player.powerUp_max,
                ranged_ATK = player.ranged_ATK
            };
            var cameraControl = GameObject.Find("CM vcam1").GetComponent<CameraManager>();
            cameraControl.setCamera();

        }
    }
}
