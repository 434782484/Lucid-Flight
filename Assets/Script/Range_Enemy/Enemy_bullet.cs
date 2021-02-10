using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    public float speed;

    public Transform player;
    public Vector2 target;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
            pc.BeingAttacked(damage);
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
    public void SetDamage(int d) { damage = d; }
}
