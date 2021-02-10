using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public float maxCap;
    public float minCap;
    public float floatTimer;
    public float flashTimer;
    public int UpOrDown;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        maxCap = transform.position.y + 1.0f;
        minCap = transform.position.y - 1.0f;
        floatTimer = 0.1f;
        flashTimer = 0.4f;
        UpOrDown = 1;
        speed = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashTimer > 0.2f)
        {
            flashTimer -= Time.deltaTime;
            spriteR.color = Color.white;
        }
        else if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;
            spriteR.color = Color.grey;
        }
        else {
            flashTimer = 0.4f;
        }

        if ((transform.position.y >= maxCap) || (transform.position.y <= minCap))
        {
            floatTimer -= Time.deltaTime;
            if (floatTimer <= 0)
            {
                if (transform.position.y >= maxCap)
                {
                    UpOrDown = 0;
                    transform.position += new Vector3(0f, -.005f * speed, 0f);
                }
                else
                {
                    UpOrDown = 1;
                    transform.position += new Vector3(0f, .005f * speed, 0f);
                }
            }
        }
        else if (transform.position.y < maxCap && UpOrDown == 1)
        {
            transform.position += new Vector3(0f, .005f * speed, 0f);
            floatTimer = 0.1f;
        }
        else if (transform.position.y > minCap && UpOrDown == 0)
        {
            transform.position += new Vector3(0f, -.005f * speed, 0f);
            floatTimer = 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerControl>().hp < collision.gameObject.GetComponent<PlayerControl>().max_hp)
            {
                collision.gameObject.GetComponent<PlayerControl>().hp++;
            }
            Destroy(this.gameObject);
        }
    }

}
