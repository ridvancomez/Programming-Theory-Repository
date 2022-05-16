using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    [SerializeField]
    private GameObject bullet; // ateþ edeceði obje

    [SerializeField]
    private float bulletDamage; // merminin hasar miktarý

    [SerializeField]
    private float reloadMagazine; // mermi dolum süresi


    // Start is called before the first frame update
    void Start()
    {
        DoFunction = false; // ilk doðduðunda player in yanýnda doðduysa hasar vurmasýn diye

        GetComponent<Renderer>().material.color = RenderingColor;

        StartCoroutine("Fire"); // her mermi dolum süresi dolduktan sonra ateþ edecek
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocationControl();
        Move();

        StandbyTime -= Time.deltaTime; // bekleme süresi dolduktan sonra hasar vermeye baþlayacak

        if (StandbyTime <= 0)
        {
            DoFunction = true;
        }
    }

    IEnumerator Fire()
    {
        while(true)
        {
            GameObject generatedBullet = Instantiate(bullet);
            generatedBullet.transform.position = transform.position + new Vector3(0, -0.91f, 0);
            generatedBullet.transform.localScale = new Vector3(2, 2, 5);
            yield return new WaitForSeconds(reloadMagazine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageAndDeath(other.tag, DamageAmount, Score, other.gameObject);
    }
}
