using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : AllEnemyController
{
    public float leftCap;
    public float rightCap;

    private Collider2D coll;
    private Rigidbody2D enemyRb;
    private GameObject player;
    public float aggroRange;
    private float aggroVerticleRange;
    private float aggroMovementSpeed;
    private float speed;
    private bool faceLeft = true;
    public Animator animator;

    public float prepareTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        max_hp = 2;
        hp = max_hp;
        position_original = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        aggroVerticleRange = 2f;
        aggroMovementSpeed = 1.5f;
        speed = 0.3f;
        hurtTimer = .2f;


    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        CheckDeath();

        OnAggroRange();
        
        if (isAttacked == false)
        {
            if (isAggro)
            {
                if (prepareTime > 0)
                {
                    animator.SetBool("prepare", true);
                    prepareTime -= Time.deltaTime;
                    enemyRb.velocity = new Vector2(0, 0);
                }
                else
                {
                    if (transform.position.x - player.transform.position.x > 0) // enemy chase left
                    {
                        animator.SetBool("isAngry", true);
                        enemyRb.velocity = new Vector2(-aggroMovementSpeed, 0);
                    }
                    else //enemy chase right
                    {
                        animator.SetBool("isAngry", true);
                        enemyRb.velocity = new Vector2(aggroMovementSpeed, 0);
                    }
                }

            }
            else
            {
                prepareTime = 1.5f;
                animator.SetBool("prepare", false);
                animator.SetBool("isAngry", false);
                if (faceLeft)
                {
                    if (transform.position.x > leftCap)
                    {
                        if (transform.localScale.x != 1)
                        {
                            transform.localScale = new Vector3(1, 1);
                        }
                        enemyRb.velocity = new Vector2(-speed, 0);
                    }
                    else
                    {
                        faceLeft = false;
                    }
                }
                else
                {
                    if (transform.position.x < rightCap)
                    {
                        if (transform.localScale.x != -1)
                        {
                            transform.localScale = new Vector3(-1, 1);
                        }
                        enemyRb.velocity = new Vector2(speed, 0);
                    }
                    else
                    {
                        faceLeft = true;
                    }
                }
            }
        }
        else
        {
            CancellStaticMode();
        }
        
        turnRed();
    }





    void OnAggroRange()
    {
        if (player != null)
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) < aggroRange && Mathf.Abs(transform.position.y - player.transform.position.y) < aggroVerticleRange)
            {
                isAggro = true;
            }
            else
            {
                isAggro = false;

            }
        }
    }


}

