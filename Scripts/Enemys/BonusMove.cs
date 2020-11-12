using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{

    public GameObject blueBonus;
    public GameObject greenBonus;


    public Rigidbody2D rb;

    private MainController mainTasker;

    private float speed;

    private int direction = 1;


    private void Start()
    {

        mainTasker = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();
        speed = mainTasker.bonusSpeed;

        if (mainTasker.mocvaraOn == true)
        {
            greenBonus.SetActive(true);
            blueBonus.SetActive(false);
        }
        else
        {
            greenBonus.SetActive(false);
            blueBonus.SetActive(true);
        }

        mainTasker.AliveBonus.Add(gameObject);

        int randomInt = Random.Range(0, 1);

        if(randomInt == 0)
        {
            direction = -1;
        }

    }

    private void FixedUpdate()
    {

        rb.velocity = transform.up * -speed * Time.deltaTime;

        transform.GetChild(0).transform.Rotate(0, 0, 50 * Time.deltaTime * direction);
        transform.GetChild(1).transform.Rotate(0, 0, 50 * Time.deltaTime * direction);

    }

    public void updateBonus()
    {

        if (mainTasker.mocvaraOn == true)
        {
            greenBonus.SetActive(true);
            blueBonus.SetActive(false);
        }
        else
        {
            greenBonus.SetActive(false);
            blueBonus.SetActive(true);
        }

    }

    public void destroyBonus()
    {
        mainTasker.AliveBonus.Remove(gameObject);
    }



}
