using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Vector3 bulletTurn;

    private float speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = 100;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DeathTime());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = bulletTurn * speed;
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
