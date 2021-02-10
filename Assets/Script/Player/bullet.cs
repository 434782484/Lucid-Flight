using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D rb;
    private int atk;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ForeGround")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetAtk(int attack)
    {
        atk = attack;
    }
    public int GetAtk()
    {
        return atk;
    }
}
