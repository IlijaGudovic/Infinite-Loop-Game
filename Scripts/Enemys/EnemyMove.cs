using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public GameObject purpleKomarac;
    public GameObject greenKomarac;


    public Rigidbody2D rb;

    private MainController mainTasker;

    private float speed;

    public Animation anim1;

    private void Start()
    {
        
        mainTasker = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();
        speed = mainTasker.EnemySpeed;

        if(mainTasker.mocvaraOn == true)
        {
            greenKomarac.SetActive(true);
            purpleKomarac.SetActive(false);
        }
        else
        {
            greenKomarac.SetActive(false);
            purpleKomarac.SetActive(true);
        }

        mainTasker.AliveEnemys.Add(gameObject);

    }

    private void FixedUpdate()
    {

        rb.velocity = transform.up * - speed  * Time.deltaTime;

    }

    public void updateEnemy()
    {

        if (mainTasker.mocvaraOn == true)
        {
            greenKomarac.SetActive(true);
            purpleKomarac.SetActive(false);
        }
        else
        {
            greenKomarac.SetActive(false);
            purpleKomarac.SetActive(true);
        }

    }

    public void destroyEnemy()
    {
        mainTasker.AliveEnemys.Remove(gameObject);
    }












}
