using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemyController : MonoBehaviour
{
    public int hp;
    public int max_hp;
    public bool isAttacked;
    public float time_static;
    public bool isAggro = false;
    public Vector3 position_original;
    public GameObject Level1_enemies;
    public GameObject Level2_enemies;
    public GameObject Level1_InactiveEnemies;
    public GameObject Level2_InactiveEnemies;
    public bool hurt;
    public float hurtTimer;

    // Start is called before the first frame update
    void Start()
    {

        hp = 1;
        max_hp = 2;
        isAttacked = false;
        hurt = false;

        //Level1_enemies = GameObject.Find("Level1_enemies");
        //Level2_enemies = GameObject.Find("Level2_enemies");

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CancellStaticMode()
    {

        time_static -= Time.deltaTime;
        if (time_static <= 0)
        {
            isAttacked = false;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Bullet")
        {

            hp -= collision.gameObject.GetComponent<bullet>().GetAtk();
            hurt = true;
            Debug.Log("I encounter with a bullet");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
            pc.BeingAttacked(1);
            Debug.Log("I encounter with a player");
        }


    }
    public void CheckDeath()
    {
        if (hp <= 0)
        {
            //GameObject.Find("Player").GetComponent<PlayerControl>().checkIfAllEnemiesElimenated();

            if((gameObject.transform.parent != null && gameObject.transform.parent.name == "Level2_enemies") || gameObject.name == "BirdBoss")
            {
                var Level2_InactiveEnemies = GameObject.Find("Level2_InactiveEnemies");
                gameObject.transform.parent = Level2_InactiveEnemies.transform;
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }


            //Destroy(this.gameObject);

        }
    }
    public void Reset()
    {
        transform.position = position_original;
        isAggro = false;
        hp = max_hp;
    }

    public void turnRed()
    {
        if (hurt == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (hurtTimer > 0)
            {
                hurtTimer -= Time.deltaTime;
            }
            else if (hurtTimer <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                hurtTimer = .2f;
                hurt = false;
            }
        }
    }

}
