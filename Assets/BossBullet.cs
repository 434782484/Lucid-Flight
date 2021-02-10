using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Enemy_bullet
{
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //target = new Vector2(player.position.x, player.position.y);
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                Destroy(gameObject);
            }
        }
    }
    public void StartMoving() {
        isMoving = true;
    }
    public void SetTarget(int index) {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        target += new Vector2(index * 0.7f, 0f); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
            pc.BeingAttacked(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ForeGround")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
