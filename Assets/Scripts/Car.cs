using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Enemy
{


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        DoFunction = false;


        GetComponent<Renderer>().material.color = RenderingColor;



        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        StandbyTime -= Time.deltaTime;

        if (StandbyTime <= 0)
        {
            DoFunction = true;
        }



        PlayerLocationControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageAndDeath(other.tag, DamageAmount, Score, other.gameObject);
    }
}
