using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBossTrigger : MonoBehaviour
{
    public GameObject float_target;
    public GameObject boss;
    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        isClear = false;   
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemies_find1 = GameObject.Find("Level1_enemies");
        GameObject enemies_find2 = GameObject.Find("Level2_enemies");

        if (enemies_find2.transform.childCount == 0 && enemies_find1.transform.childCount == 0)
        {
            isClear = true;
        }
        else {
            isClear = false;
        }
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player" && isClear)
        {
            float_target.GetComponent<Float_Manager>().speed = 0;
            boss.GetComponent<BirdBossControll>().mode = 1;
            GameObject.Find("CM vcam1").GetComponent<CameraManager>().setCamera2();
        }
    }
}
