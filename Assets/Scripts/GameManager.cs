using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject generatedObject; // platform

    private PlayerControl playerScript;
    private float platformZPos;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindObjectOfType<PlayerControl>();
        platformZPos = 100; // platform z yönünden 100 birim
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.platformGenerated)
        {
            GameObject go = Instantiate(generatedObject);
            go.transform.position = new Vector3(0, 0, platformZPos);
            platformZPos += 100;
            playerScript.platformGenerated = false;
        }
    }
}
