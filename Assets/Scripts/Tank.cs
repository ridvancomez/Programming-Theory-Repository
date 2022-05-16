using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    [SerializeField]
    private GameObject bullet; // ate� edece�i obje

    [SerializeField]
    private float bulletDamage; // merminin hasar miktar�

    [SerializeField]
    private float reloadMagazine; // mermi dolum s�resi


    // Start is called before the first frame update
    void Start()
    {
        DoFunction = false; // ilk do�du�unda player in yan�nda do�duysa hasar vurmas�n diye

        GetComponent<Renderer>().material.color = RenderingColor;

        StartCoroutine("Fire"); // her mermi dolum s�resi dolduktan sonra ate� edecek
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocationControl();
        Move();

        StandbyTime -= Time.deltaTime; // bekleme s�resi dolduktan sonra hasar vermeye ba�layacak

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
