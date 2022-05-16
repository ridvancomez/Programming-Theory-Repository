using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
   [SerializeField]
    private GameObject[] enemyCars;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("SendCar");
    }

    private IEnumerator SendCar()
    {
        while(enemyCars.Length > 0)
        {
            yield return new WaitForSeconds(0.6f);
             
            GameObject go = Instantiate(enemyCars[Random.Range(0, enemyCars.Length)]);
            go.transform.position = new Vector3(Random.Range(-42, 43), go.transform.position.y, transform.position.z);
        }
    }

}
