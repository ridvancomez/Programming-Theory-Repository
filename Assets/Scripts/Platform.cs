using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyPlatform());
    }


    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(10); // bir platformu 10 saniyede bitiriyor
        Destroy(gameObject);
    }
}
