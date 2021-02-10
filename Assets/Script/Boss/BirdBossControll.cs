using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBossControll : AllEnemyController
{
    private float stationaryTime;
    public float originalStationaryTime;
    public Animator animator;
    public float speed;
    private Transform playerTransform;
    private Vector2 target;
    private Vector2 playerOrigialPosition;
    private bool chargeMode;
    private bool towardsLeft;
    private bool towardsTop;
    private float additionalTransform;
    public int mode; // 0 means peace; 1 means come to catch the string; 2 means back; 3 means fight; 
    public Transform catchPoint;
    public GameObject float_target;


    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        position_original = transform.position;
        max_hp = 6;
        hp = max_hp;
        originalStationaryTime = 5f;
        speed = 7;
        additionalTransform = 3f;
        stationaryTime = originalStationaryTime;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        hurtTimer = .2f;

    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        if (mode == 3)
        {
            CheckDeath();
            Charge();
            turnRed();
        }
        else if (mode == 2)
        {
            if (Vector3.Distance(position_original, transform.position) <= 0.01f)
            {
                mode = 3;
                transform.GetChild(0).parent = null;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, position_original, 3*speed * Time.deltaTime);
            }
        }
        else if (mode == 1) 
        {
            if (Vector3.Distance(catchPoint.position,transform.position) <= 0.01f)
            {
                float_target.transform.parent = transform;
                mode = 2;
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position, catchPoint.position, 3*speed * Time.deltaTime);
            }

        }
    }

	public void BossReset()
	{
        mode = 0;
        transform.position = position_original;
        hp = max_hp;
        originalStationaryTime = 5f;
        speed = 7;
        additionalTransform = 2.5f;
        stationaryTime = originalStationaryTime;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }
	private void CheckForCharge()
    {

        stationaryTime = stationaryTime - Time.deltaTime;
        if (stationaryTime <= 0)
        {
            target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            playerOrigialPosition = new Vector2(target.x, target.y);
            towardsLeft = transform.position.x - target.x < 0 ? false : true;
            towardsTop = transform.position.y - target.y < 0 ? true : false;
            playerOrigialPosition += AdditionalPosition();
            chargeMode = true;
            stationaryTime = originalStationaryTime;
        }

    }

    private void Charge()
    {
        CheckForCharge();
        animator.SetBool("Dash", chargeMode);
        if (chargeMode)
        {
            if ((towardsLeft && (transform.position.x - target.x <= -additionalTransform)) || (!towardsLeft && (transform.position.x - target.x >= additionalTransform)))
            {
                Debug.Log("exiting chase");
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                chargeMode = false;

                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, playerOrigialPosition, speed * Time.deltaTime);
        }
        else {
            if (Vector3.Distance(playerTransform.position, transform.position) > 2.7f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 3*speed * Time.deltaTime);
            }
        }
    }

    private Vector2 AdditionalPosition()
    {

        if (towardsLeft)
        {
            if (towardsTop) // towards lefy + top
            {
                transform.rotation = Quaternion.Euler(180f, 180f, 0f);
                return new Vector2(additionalTransform * -1, additionalTransform);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                return new Vector2(-additionalTransform, -additionalTransform);

            }
        }
        else
        {
            if (towardsTop) // towards right + top
            {
                transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                return new Vector2(additionalTransform, additionalTransform);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                return new Vector2(additionalTransform, additionalTransform * -1);
            }

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
            pc.BeingAttacked(1);
            Debug.Log("I encounter with a player");
        }

        if (collision.gameObject.tag == "Bullet")
        {

            hp -= collision.gameObject.GetComponent<bullet>().GetAtk();
            Debug.Log("I encounter with a bullet");
        }
    }
}
