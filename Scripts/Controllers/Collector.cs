using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D target)
    {

        if(target.gameObject.tag == ("Enemy"))
        {

            target.gameObject.GetComponent<EnemyMove>().destroyEnemy();
            Destroy(target.gameObject);

        }
        else if (target.gameObject.tag == ("Bonus"))
        {

            target.gameObject.GetComponent<BonusMove>().destroyBonus();
            Destroy(target.gameObject);

        }
        else if (target.gameObject.tag == ("Effect"))
        {
            Destroy(target.gameObject);
        }

    }



}
