using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

//using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    //Start() variables
    public int hp;
    private Rigidbody2D player;
    private Collider2D coll;
    private bool isLeft = false;
    private bool game_not_end;
    public bool isGrounded = true;
    private float RangedCountDown = 1f;
    private bool inCastMode = false;
    private bool hasStagger;
    public int max_hp = 3;
    public int powerUpNum = 0;
    public int powerUp_max;
    private GameObject canvas;
    private float rangedKeyDownTime = 0.0f;
    private float currentKeyDownTime = 0.0f;
    private float attackedTime = 0.0f;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 60f;
    public GameObject bullet;
    public GameObject chargeBar;
    public GameObject pic;
    private GameObject checkpoint;
    public Transform startPositon;
    private ChargeBar ChargeBar;
    public Vector3 originalTransform;
    public Transform shootingPoint;
    public Transform attackLocation;
    public float attackRange;
    public int melee_ATK;
    public int ranged_ATK;
    public LayerMask enemies;
    public int jump_limit;
    public int max_jump;
    public string name_scene;
    public Animator anim;
    private List<string> powerUps = new List<string>();
    public bool isOnLevel2;
    public bool canControl;
    public int TotalLife;
    public PlayerStat lv1playerStat = new PlayerStat();


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        TotalLife = 3;
        isOnLevel2 = false;
        anim = GetComponent<Animator>();
        powerUp_max = 0;
        //GameObject.Find("Player").GetComponent<PowerUps>().enabled = false;
        Debug.Log("Game Start");
        transform.position = new Vector3(-10f, 0f, 0);
        //transform.position = new Vector3(51f, 0.9f, 0);
        //transform.position = new Vector3(53f, 24f, 0);
        //transform.position = new Vector3(44f, 1.1f, 0);
        //transform.position = new Vector3(10.45f, -0.69f, 0);
        //transform.position = new Vector3(22f, 8f, 0);
        //transform.position = new Vector3(15f, -.2f, 0);
        //transform.position = new Vector3(42f, 1.5f, 0);

        ChargeBar = chargeBar.GetComponent<ChargeBar>();
        ChargeBar.SetChargeBarValue(0);
        originalTransform = transform.position;
        max_jump = 1;
        jump_limit = max_jump;
        attackRange = .45f;
        canControl = true;
        game_not_end = true;
        hasStagger = true;
        hp = max_hp;
        melee_ATK = 1;
        ranged_ATK = 1;
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        name_scene = SceneManager.GetActiveScene().name;
        checkpoint = GameObject.Find("CheckPoint");
        PowerUpsInitial();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (name_scene != SceneManager.GetActiveScene().name)
        {
            transform.position = GameObject.Find("StartPosition").transform.position;
            name_scene = SceneManager.GetActiveScene().name;
        }

        if (hp <= 0 || transform.position.y < -2f)
        {
            game_not_end = false;
            Reset();
        }

        else if (game_not_end)
        {
            checkIfAllEnemiesElimenated();
            if (inCastMode)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = (mousePosition.y - shootingPoint.position.y) / (mousePosition.x - shootingPoint.position.x);
                angle = Mathf.Atan(angle) / 3.14f * 180f;
                if (isLeft == false && mousePosition.x < shootingPoint.position.x)
                {
                    transform.Rotate(0, 180, 0);
                    isLeft = true;
                }
                else if (isLeft && mousePosition.x > shootingPoint.position.x)
                {
                    transform.Rotate(0, 180, 0);
                    isLeft = false;
                }
                if (isLeft) { anim.SetFloat("shootingDegree", -1 * angle); } else { anim.SetFloat("shootingDegree", angle); }

                currentKeyDownTime = Time.time - rangedKeyDownTime;
                ChargeBar.SetChargeBarValue(currentKeyDownTime / RangedCountDown);
            }
            if (canControl)
            {
                Movement();
                Attack();
            }
            if (attackedTime >= 0f) { attackedTime -= Time.deltaTime; }
            else { anim.SetBool("isAttacked", false); }
        }

    }

    private void Reset()
    {
        if (!isOnLevel2)
        {
            SceneManager.LoadScene("The_Level", LoadSceneMode.Single);
            TotalLife--;
        }
        else
        {
            GameObject.Find("BirdBoss").GetComponent<BirdBossControll>().BossReset();
            GameObject.Find("L2_Float_7").GetComponent<Float_Manager>().Reset();
            TotalLife--;
            player.position = checkpoint.transform.position;
            powerUpNum = lv1playerStat.powerUpNum;
            powerUp_max = lv1playerStat.powerUp_max;
            melee_ATK = lv1playerStat.melee_ATK;
            ranged_ATK = lv1playerStat.ranged_ATK;
            jump_limit = lv1playerStat.jump_limit;
            max_jump = lv1playerStat.max_jump;
            max_hp = lv1playerStat.max_hp;
            hp = max_hp;
            var lv2InactiveEnemies = GameObject.Find("Level2_InactiveEnemies");
            GameObject level2Enemies = GameObject.Find("Level2_enemies");
            foreach (Transform child in lv2InactiveEnemies.transform)
            {
                child.gameObject.SetActive(true);
                child.parent = level2Enemies.transform;
            }

            foreach(Transform child in level2Enemies.transform)
            {
                var enemyStat = child.gameObject.GetComponent<AllEnemyController>();
                enemyStat.Reset();
            }



            game_not_end = true;
        }
        //max_jump = 1;
        //jump_limit = max_jump;
        //attackRange = 3.4f;
        //game_not_end = true;
        //hp = max_hp;
        //melee_ATK = 1;
        //ranged_ATK = 1;
        //powerUp_max = 0;
        //powerUpNum = powerUp_max;
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //moving left
        if (hDirection < 0 && inCastMode == false)
        {
            anim.SetBool("isMoving", true);
            Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position -= Move * Time.deltaTime * -speed;

            //player.transform.position += new Vector3(-speed, player.velocity.y);
            //transform.position +=  Time.deltaTime * -speed;
            if (isLeft == false)
            {
                transform.Rotate(0, 180, 0);
                isLeft = true;
            }
        }

        //moving right
        else if (hDirection > 0 && inCastMode == false)
        {
            anim.SetBool("isMoving", true);
            Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += Move * Time.deltaTime * speed;

            if (isLeft == true)
            {
                transform.Rotate(0, 180, 0);
                isLeft = false;
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        //jumping 
        if (Input.GetButtonDown("Jump") && jump_limit > 0)
        {
            anim.SetBool("isJumping", true);
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            jump_limit--;
            isGrounded = false;

        }




    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rangedKeyDownTime = Time.time; // initial time
            player.velocity = new Vector3(0, 0, 0);
            inCastMode = true;
            //Debug.Log(rangedKeyDownTime / RangedCountDown);
            anim.SetBool("isShooting", true);

        }

        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
            rangedKeyDownTime = Time.time - rangedKeyDownTime; // time - initial time
            //Debug.Log("Pressed for : " + rangedKeyDownTime + " Seconds");
            if (rangedKeyDownTime >= RangedCountDown)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = (mousePosition.y - shootingPoint.position.y) / (mousePosition.x - shootingPoint.position.x);
                //Debug.Log("Angle: " + angle);
                //if (angle < 0f)
                //{
                //    angle = 90f;
                //}
                angle = Mathf.Atan(angle) / 3.14f * 180f;
                Debug.Log("Angle: " + angle);
                GameObject newBullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
                newBullet.GetComponent<bullet>().SetAtk(ranged_ATK);
                if (isLeft)
                {
                    newBullet.transform.Rotate(0, 0, -1.0f * angle, Space.Self);
                }
                else
                {
                    newBullet.transform.Rotate(0, 0, angle, Space.Self);
                }
                Destroy(newBullet, 0.6f);
            }
            inCastMode = false;
            ChargeBar.SetChargeBarValue(0f);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("I waved");
            anim.SetBool("isAttacking", true);
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);
            //Debug.Log(enemiesInRange.Length);
            if (enemiesInRange.Length > 0)
            {
                Debug.Log("I hit!!!");
            }
            if (enemiesInRange.Length > 0)
            {

                foreach (var enemy in enemiesInRange)
                {
                    AllEnemyController currentEnemy = enemy.gameObject.GetComponent<AllEnemyController>();
                    Rigidbody2D enemyRb = enemy.gameObject.GetComponent<Rigidbody2D>();
                    currentEnemy.hp -= melee_ATK;
                    currentEnemy.hurt = true;
                    Debug.Log(enemy.name + " lost " + melee_ATK + " hp.");

                    if (hasStagger)
                    {
                        currentEnemy.time_static = 0.5f;
                        currentEnemy.isAttacked = true;
                        if (isLeft)
                        {
                            enemyRb.velocity = new Vector2(-1, 0);
                        }
                        else
                        {
                            enemyRb.velocity = new Vector2(1, 0);
                        }
                    }
                    //Debug.Log("enemy hp = " + currentEnemy.hp);

                }
            }

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public IEnumerator StartCountdown()
    {
        float currentTimer = RangedCountDown;
        while (currentTimer > 0)
        {
            player.velocity = new Vector2(0, 0);
            //Debug.Log("In ranged Attack animation");
            yield return new WaitForSeconds(currentTimer);
            currentTimer = 0;
            inCastMode = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        //if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        if (Physics2D.IsTouchingLayers(coll, ground))
        {
            //Debug.Log(Physics2D.IsTouchingLayers(coll, ground));
            //Debug.Log("It is a ground");
            //jump_limit = max_jump;    
            //isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        //if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        if (collision.gameObject.name == "Layer")
        {
            anim.SetBool("isJumping", false);
            jump_limit = max_jump;
            isGrounded = true;
        }
    }

    public void checkIfAllEnemiesElimenated()
    {

        GameObject enemies_find1 = GameObject.Find("Level1_enemies");
        GameObject enemies_find2 = GameObject.Find("Level2_enemies");
        if (enemies_find1.transform.childCount == 0 && enemies_find2.transform.childCount > 0 && powerUp_max == 0)
        {
            Debug.Log("Levle 1 enemies elinated");
            powerUp_max++;

        }

        if (enemies_find2.transform.childCount == 0 && enemies_find1.transform.childCount == 0 && powerUp_max == 2)
        {
            GameObject.Find("door").GetComponent<DoorManager>().isClear = true;
            powerUp_max++;
        }

        if (powerUpNum < powerUp_max)
        {
            Debug.Log("I open the power up boards");
            canvas = GameObject.Find("Canvas");
            //canvas.GetComponent<CanvasManagement>().PopupMessage("Choose 1 Power Up!");
            canvas.GetComponent<CanvasManagement>().PowerUpButtonInitial(powerUps);
            powerUpNum++;
        }
    }
    private void PowerUpsInitial()
    {
        powerUps.Add("Melee ATK Boost");
        powerUps.Add("Double Jump");
        powerUps.Add("Speed Up");
        powerUps.Add("MAX HP Boost");
    }

    public void PowerUpJump() //double jump
    {
        max_jump++;
        jump_limit = max_jump;
    }
    public void PowerUpMeleeATK() // increase melee attack
    {
        melee_ATK++;
    }

    public void PowerUpRangedATK()  //increase range attack
    {
        ranged_ATK++;
    }

    public void PowerUpHpBoost()  // Increase max hp + 1, heal to full health
    {
        max_hp++;
        hp++;
    }
    public void PowerUpSpeedUp()  // Increase max hp + 1, heal to full health
    {
        RangedCountDown = RangedCountDown *0.75f;
    }

    public void PowerUpStagger()
    {
        hasStagger = true;
    }
    public void BeingAttacked(int damage)
    {
        hp -= damage;
        anim.SetBool("isAttacked", true);
        attackedTime = 0.10f;
    }
    public void SetActivePic()
    {
        Debug.Log("Punch!");
        if (isLeft)
        {
            GameObject g = Instantiate(pic, attackLocation.position - new Vector3(attackRange - 0.15f, -0.03f, 0), transform.rotation);
            g.transform.parent = transform;
            Destroy(g, 0.15f);
        }
        else
        {
            GameObject g = Instantiate(pic, attackLocation.position + new Vector3(attackRange - 0.15f, 0.03f, 0), transform.rotation);
            Destroy(g,0.15f);
            g.transform.parent = transform;
            Destroy(g, 0.15f);
        }

    }
    public void PauseGame()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isShooting", false);
        anim.SetBool("isAttacked", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isMoving", false);
        canControl = false;
    }

    public void ResumeGame()
    {
        canControl = true;
    }

}




