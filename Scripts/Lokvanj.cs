using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lokvanj : MonoBehaviour
{

    public float timeForTriger;
    private float timeCounter;

    public Color myColor;



    private void OnTriggerStay2D(Collider2D target)
    {

        if (target.tag == ("Player"))
        {

            if(timeCounter > timeForTriger)
            {

                target.gameObject.GetComponent<FolowMouse>().changeColor(myColor);

            }
            else
            {
                timeCounter += Time.deltaTime; 
            }

        }

    }

    private void OnTriggerExit2D(Collider2D target)
    {

        if (target.tag == ("Player"))
        {

            timeCounter = 0;

        }

    }


}
