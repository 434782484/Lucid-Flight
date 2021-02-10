using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorManager : MonoBehaviour
{
    public float maxCap;
    public float minCap;
    public int HorOrVer;
    public float floatTimer;
    public int UpOrDown;
    public float speed;
    public string name_nextScene;
    public bool isClear;
    // Start is called before the first frame update
    void Start()
    {
        //if this variable is 1 then move vertically
        isClear = false;
        floatTimer = 0.1f;
        UpOrDown = 1;
        speed = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject enemies_find1 = GameObject.Find("Level1_enemies");
        GameObject enemies_find2 = GameObject.Find("Level2_enemies");

        if (enemies_find2.transform.childCount == 0 && enemies_find1.transform.childCount == 0 )
        {
            isClear = true;
        }
        if (maxCap != minCap)
        {
            if (HorOrVer == 1)
            {
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
            else
            {
                if ((transform.position.x >= maxCap) || (transform.position.x <= minCap))
                {
                    floatTimer -= Time.deltaTime;
                    if (floatTimer <= 0)
                    {
                        if (transform.position.x >= maxCap)
                        {
                            UpOrDown = 0;
                            transform.position += new Vector3(-.005f * speed, 0f, 0f);
                        }
                        else
                        {
                            UpOrDown = 1;
                            transform.position += new Vector3(.005f * speed, 0f, 0f);
                        }
                    }
                }
                else if (transform.position.x < maxCap && UpOrDown == 1)
                {
                    transform.position += new Vector3(.005f * speed, 0f, 0f);
                    floatTimer = 0.1f;
                }
                else if (transform.position.x > minCap && UpOrDown == 0)
                {
                    transform.position += new Vector3(-.005f * speed, 0f, 0f);
                    floatTimer = 0.1f;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {


        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        //if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        if (collision.gameObject.tag =="Player" )
        {
            if (isClear)
            {
                if (name_nextScene != "StartMenu")
                {
                    SceneManager.LoadScene(name_nextScene, LoadSceneMode.Single);
                }
                else
                {
                    Destroy(GameObject.Find("Player"));
                    SceneManager.LoadScene(name_nextScene, LoadSceneMode.Single);
                }
            }
            else {
                GameObject.Find("Canvas").GetComponent<CanvasManagement>().PopupMessage("Please Clear enemies.");
            }
        }


    }
}
