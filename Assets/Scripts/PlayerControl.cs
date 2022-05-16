using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Score;
    public float healt { get; set; }
    private float speed;
    public bool Fired;

    [SerializeField]
    GameObject bullet;

    public bool platformGenerated { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        healt = 100;
        speed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        ColorTransition();
        Move();
        Fire();
    }



    private void Move()
    {
        float horizontal;
        horizontal = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontal * speed * Time.deltaTime, 0, 20 * Time.deltaTime));

        if (transform.position.x < -42)
        {
            transform.position = new Vector3(-42, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 42)
        {
            transform.position = new Vector3(42, transform.position.y, transform.position.z);
        }
    }


    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !Fired)
        {
            GameObject generatedBullet = Instantiate(bullet);
            generatedBullet.transform.position = transform.position + new Vector3(0, -0.91f, 0);
            generatedBullet.transform.localScale = new Vector3(2, 2, 5);

            Fired = true;
            StartCoroutine(ShotDown());
        }
    }

    private void ColorTransition() // healt a göre renk deðiþtiriyor
    {
        if (healt == 100)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (healt <= 66 && healt > 34)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        else if (healt <= 33)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

    }

    public void IncreaseScore(float score)
    {
        this.Score += score;
    }


    IEnumerator ShotDown()
    {
        yield return new WaitForSeconds(1);
        Fired = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GenerateLine":
                platformGenerated = true;
                break;
            case "TankBullet":
                healt -= 10;
                Destroy(other.gameObject);
                break;
        }

    }
}
