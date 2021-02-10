using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBossControll : AllEnemyController
{
    private Collider2D coll;
    private Rigidbody2D enemyRb;
    private GameObject player;
    private bool faceLeft = true;
    private float stationaryTime;
    private float attackWaitedTime;

    private List<List<float>> positionList;
    private List<string> attackModeList;
    private float currentBossAttackWaitedTime;
    private int numAttack;
    private bool minionSummoned = false;


    public float originalStationary;
    public Animator animator;
    public GameObject projectiles;
    public GameObject minion;


    // Start is called before the first frame update
    void Start()
    {
        max_hp = 15;
        hp = max_hp;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        originalStationary = 5;
        stationaryTime = originalStationary;
        attackWaitedTime = 6;
        numAttack = 0;
        positionList = GenerateBossPosition();
        attackModeList = GenerateBossAttackMode();
        hurtTimer = .2f;
    }

    // Update is called once per frame
    void Update()
    {
        Facing();
        CheckDeath();
        WaitForTeleport();
        CheckSummonStatus();
        turnRed();
    }



    private void WaitForTeleport()
    {
        stationaryTime = stationaryTime - Time.deltaTime;
        if (stationaryTime <= 0 )
        {
            Teleport();
            stationaryTime = originalStationary;
        }
    }

    private void Teleport()
    {
        var nextPos = positionList[Random.Range(0, positionList.Count)];
        transform.position = new Vector3(nextPos[0], nextPos[1], 0);
        animator.SetBool("attack", true);
    }

    private void Attack()
    {
		//var nextattck = attackmodelist[random.range(0, positionlist.count)];
		//if (nextattck == "range")
		//{
		//}

		//else
		//{
		//	attack

		//}



        if (hp >= max_hp / 2) 
        {
            GameObject newBullet = Instantiate(projectiles, transform.position, Unity.Mathematics.quaternion.identity);
            newBullet.GetComponent<BossBullet>().SetTarget(0);
            Destroy(newBullet, 3f);
        }
        else
        {
	        GameObject newBullet = Instantiate(projectiles, transform.position, Unity.Mathematics.quaternion.identity);
            newBullet.GetComponent<BossBullet>().SetTarget(0);
            Destroy(newBullet, 3f);
            GameObject newBullet2 = Instantiate(projectiles, transform.position, Unity.Mathematics.quaternion.identity);
            newBullet2.GetComponent<BossBullet>().SetTarget(1);
            Destroy(newBullet2, 3f);
            GameObject newBullet3 = Instantiate(projectiles, transform.position, Unity.Mathematics.quaternion.identity);
            newBullet3.GetComponent<BossBullet>().SetTarget(-1);
            Destroy(newBullet3, 3f);
            GameObject newBullet4 = Instantiate(projectiles, transform.position, Unity.Mathematics.quaternion.identity);
            newBullet4.GetComponent<BossBullet>().SetTarget(-2);
            Destroy(newBullet4, 3f);
            GameObject newBullet5 = Instantiate(projectiles, transform.position, Unity.Mathematics.quaternion.identity);
            newBullet5.GetComponent<BossBullet>().SetTarget(2);
            Destroy(newBullet5, 3f);
        }

    }

    private List<List<float>> GenerateBossPosition()
    {
        return new List<List<float>>()
        {
            new List<float>() { 18f,.6f },
            new List<float>() { 16f,.4f },
            new List<float>() { 18f,-.15f },
            new List<float>() { 15f,-.3f },
        };
    }

    private List<string> GenerateBossAttackMode()
    {
        return new List<string>() { "MultiRange", "Range" };
    }

    private void Facing()
    {
        if((transform.position.x - player.transform.position.x) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    
    private void CheckSummonStatus()
    {
        if (hp <= 10 && hp > 5 && minionSummoned == false)
        {
            Debug.Log("summon7");
            GameObject newEnemy = Instantiate(minion, new Vector3(transform.position.x-1f, 0.015f, 0f), Unity.Mathematics.quaternion.identity);
            minionSummoned = true;
        }
        if (hp <= 5 && minionSummoned == true)
        {
            Debug.Log("summon3");

            GameObject newEnemy = Instantiate(minion, new Vector3(transform.position.x - 1f, 0.1f, 0f), Unity.Mathematics.quaternion.identity);
            GameObject newEnemy2 = Instantiate(minion, new Vector3(transform.position.x - 1.1f, 0.01f, 0f), Unity.Mathematics.quaternion.identity);
            minionSummoned = false;
        }

    }
    public void AttackFinish() 
    {
        animator.SetBool("attack", false);
    }
}
