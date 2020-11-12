using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    private MainController mainCont;

    public Animator anim;

    public float speed = 200;

    public float startY;
    public float endY;

    private void Start()
    {

        mainCont = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();

    }

    private void FixedUpdate()
    {

        transform.Translate(Vector2.up * -speed * Time.deltaTime);

        if(transform.position.y <= endY)
        {

            transform.position = new Vector2(transform.position.x, startY);

            mainCont.checkGroundTime();

        }

    }


    public void AnimationUpdate()
    {

        anim.SetBool("MocvaraOn", !(anim.GetBool("MocvaraOn")));

    }

}
