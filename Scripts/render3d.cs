using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class render3d : MonoBehaviour
{


    public int sortingOrder;            //The sorting order

    void Start()
    {

        gameObject.GetComponent<Renderer>().sortingOrder = sortingOrder;

    }


}
