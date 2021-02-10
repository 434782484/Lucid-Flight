using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject currentBoss;
    private bool bossSummoned;
    private bool boardShowed;
    // Start is called before the first frame update
    void Start()
    {
        bossSummoned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( currentBoss == null && bossSummoned && boardShowed == false)
        {
            Debug.Log("I opened the canvas in BossTrigger");
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerControl>().powerUp_max++;
            player.GetComponent<PlayerControl>().checkIfAllEnemiesElimenated();
            boardShowed = true;
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player" && !bossSummoned)
        {
            currentBoss = Instantiate(boss, new Vector3(transform.position.x - 1.3f, 0.5f, 0f), Unity.Mathematics.quaternion.identity);
            bossSummoned = true;
        }
    }

}
