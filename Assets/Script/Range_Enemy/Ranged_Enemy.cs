using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ranged_Enemy : AllEnemyController
{
    public Animator animator;
    public float speed = 3.0f;
    public float stoppingDistance = 5.0f;
    public float retreatDistance = 5.0f;
    public float aggroDistance;
    private float timeBtwShots = 1;
    public float startTimeBtwShots = 2.0f;
    public GameObject projectiles;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        max_hp = 1;
        hp = max_hp;
        position_original = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hurtTimer = .2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        OnAggroRange();
        CheckDeath();
        turnRed();
        if (isAttacked)
        {
            CancellStaticMode();
        }
        else
        {
            Facing();
            if (isAggro)
            {
                //if the distance is bigger than stopping, then move 
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                //if the distance is less than stopping and bigger then retreat, dont move
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance
                    && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                }
                //if the distance is less then retreat, move back
                else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                }

                if (timeBtwShots <= 0)
                {
                    animator.SetBool("isAttack", true);
                    Debug.Log("Is Shooting");
                    GameObject newBullet = Instantiate(projectiles, transform.position, quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                    Destroy(newBullet, 1f);

                }
                else
                {

                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }



    private void OnAggroRange()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) < aggroDistance)
            {
                isAggro = true;
            }
        }
    }

    private void Facing()
    {
        if ((transform.position.x - player.transform.position.x) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    public void StopAttack() { animator.SetBool("isAttack", false); }

}
