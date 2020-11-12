using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowMouse : MonoBehaviour
{

    public float minX, maxX;

    public float maxDistance = 1;

    public Vector2 startPostion;
    public Vector2 endPostion;

    public float speed = 20;
    private float speedOpetion = 3;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRender;

    public GameObject joystick;

    [Header("Colors")]
    public Color grey;
    public Color green;

    public float lerpDuration = 2;
    private float lerpTime;
    private bool oneTimeLerp;

    public float timeForChange;
    private float timeCounter;

    private MainController mainTasker;

    public int wichController;


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        spriteRender = GetComponent<SpriteRenderer>();

        mainTasker = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();

        wichController = PlayerPrefs.GetInt("Controller" , 0);

    }


    private void FixedUpdate()
    {

        if (wichController == 0)
        {
            opetionOne();
        }
        else if (wichController == 1)
        {
            optionTwo();
        }


        if (transform.position.x < minX + 0.6)
        {

            if(spriteRender.color != grey)
            {

                if (timeCounter > timeForChange)
                {
                    changeColor(grey);
                }
                else
                {
                    timeCounter += Time.deltaTime;
                }

            }

        }
        else if (transform.position.x > maxX - 0.6)
        {

            if (spriteRender.color != green)
            {

                if (timeCounter > timeForChange)
                {
                    changeColor(green);
                }
                else
                {
                    timeCounter += Time.deltaTime;
                }

            }

        }

    }


    //Go towards finger
    private void opetionOne()
    {

        for (int i = 0; i < Input.touchCount; ++i)
        {

            Touch touch = Input.GetTouch(i);

            endPostion = Camera.main.ScreenToWorldPoint(touch.position);

        }


        if (Input.touchCount > 0)
        {

            //endPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anim.SetBool("Run", true);

            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < minX)
            {

                endPostion.x = minX;

            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > maxX)
            {
                endPostion.x = maxX;
            }

            if (transform.position.y < endPostion.y)
            {
                speedOpetion = speed;
            }
            else
            {
                speedOpetion = speed * 2;
            }

            if (endPostion.y < - Screen.height * 0.5f + 0.2f) //Pre bilo - 3.8
            {

                endPostion.y = -Screen.height * 0.5f + 0.2f; //Isto

                if (endPostion.x == startPostion.x)
                {
                    rb.velocity = Vector2.zero;
                    return;
                }

            }
            else if (endPostion.y > Screen.height * 0.5f - 0.5f) //Pre bilo 5.5
            {

                endPostion.y = Screen.height * 0.5f - 0.5f; //Isto

                if (endPostion.x == startPostion.x)
                {
                    rb.velocity = Vector2.zero;
                    return;
                }

            }
            

            if (Vector2.Distance(transform.position, endPostion) > 0.5)
            {

                Vector2 direction = endPostion - (Vector2)transform.position;

                transform.up = direction;
                rb.velocity = transform.up * speedOpetion * Time.deltaTime;

            }
            else
            {

                rb.velocity = Vector2.zero;

                anim.SetBool("Run", false);

            }

        }

        if (Vector2.Distance(transform.position, endPostion) < 0.5)
        {

            rb.velocity = Vector2.zero;

            anim.SetBool("Run", false);

        }

    }


    //Go with joistic
    private void optionTwo()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch touch = Input.GetTouch(i);

            endPostion = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began) // Poceo
            {

                startPostion = endPostion;

                joystick.SetActive(true);
                joystick.transform.position = startPostion;

                anim.SetBool("Run", true);

            }
            else if (touch.phase == TouchPhase.Moved) //Pomera se
            {

                //endPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (startPostion.y < endPostion.y)
                {
                    speedOpetion = speed;
                }
                else
                {
                    speedOpetion = speed * 2;
                }


                if (startPostion != endPostion)
                {

                    if (Vector2.Distance(startPostion, endPostion) > maxDistance)
                    {

                        joystick.transform.GetChild(0).transform.position = endPostion;

                        if (transform.position.x < minX)
                        {
                            if (endPostion.x < startPostion.x)
                            {
                                endPostion.x = startPostion.x;
                            }
                        }
                        else if (transform.position.x > maxX)
                        {
                            if (endPostion.x > startPostion.x)
                            {
                                endPostion.x = startPostion.x;
                            }
                        }

                        if (transform.position.y < - Camera.main.orthographicSize + 1.2f) //Pre bilo - 3.8
                        {
                            if (endPostion.y < startPostion.y)
                            {
                                endPostion.y = startPostion.y;

                                speedOpetion = speed / 1.2f;

                                if (endPostion.x == startPostion.x)
                                {
                                    rb.velocity = Vector2.zero;
                                    return;
                                }
                            }
                        }
                        else if (transform.position.y > Camera.main.orthographicSize - 0.5f) //Pre bilo 5.5
                        {
                            if (endPostion.y > startPostion.y)
                            {
                                endPostion.y = startPostion.y;

                                if (endPostion.x == startPostion.x)
                                {
                                    rb.velocity = Vector2.zero;
                                    return;
                                }
                            }
                        }

                        Vector2 direction = startPostion - endPostion;

                        transform.up = -direction;

                        rb.velocity = transform.up * speedOpetion * Time.deltaTime;

                    }
                    else
                    {

                        rb.velocity = Vector2.zero;

                        joystick.transform.GetChild(0).transform.position = startPostion;

                        anim.SetBool("Run", false);

                    }

                }


            }
            else if (touch.phase == TouchPhase.Ended) // zavrsio
            {

                rb.velocity = Vector2.zero;

                joystick.SetActive(false);

                anim.SetBool("Run", false);

            }

        }


        

    }


    public void changeColor(Color color)
    {

        if (oneTimeLerp == false)
        {

            oneTimeLerp = true;

            lerpTime = 0;
            Invoke("returnLerp", 2);

        }

        spriteRender.color = Color.Lerp(spriteRender.color, color, lerpTime);

        if (lerpTime < 1)
        {
            lerpTime += Time.deltaTime / lerpDuration;
        }

    }

    private void returnLerp()
    {
        oneTimeLerp = false;
        timeCounter = 0;
    }


    private void OnCollisionEnter2D(Collision2D target)
    {

        if (target.gameObject.tag == ("Enemy") || target.gameObject.tag == ("Bonus"))
        {

            if (shieldOn == true)
            {

                StopCoroutine(coroutin);

                mainTasker.AliveEnemys.Remove(target.gameObject);
                mainTasker.AliveBonus.Remove(target.gameObject);

                Destroy(target.gameObject);
                shield.SetActive(false);
                shieldOn = false;

            }
            else
            {
                mainTasker.diedPlayer();
            }

        }

    }

    [Header("Shield")]
    private bool shieldOn;
    public GameObject shield;

    public float timeForDisable = 5;

    private IEnumerator coroutin;

    private void OnTriggerEnter2D(Collider2D target)
    {

        if (target.gameObject.tag == ("Effect"))
        {

            shieldOn = true;

            shield.SetActive(true);

            if (coroutin != null)
            {
                StopCoroutine(coroutin);
            }

            coroutin = disebleShield();

            StartCoroutine(coroutin);

            Destroy(target.gameObject);

        }

    }

    IEnumerator disebleShield()
    {

        yield return new WaitForSeconds(timeForDisable);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.5f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.5f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.3f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.3f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.2f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.2f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.2f);

        shield.SetActive(!shield.activeInHierarchy);
        yield return new WaitForSeconds(0.2f);

        shield.SetActive(false);
        shieldOn = false;

    }



}
