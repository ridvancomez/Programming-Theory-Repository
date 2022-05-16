using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    protected bool DoFunction; // bu playerin önünde hemen belirince hasar vurmasýn diye koyuldu
    protected float StandbyTime = 1;
    protected GameObject Player
    {
        get
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }


    [SerializeField]
    protected float Speed;

    [SerializeField]
    protected float DamageAmount;

    [SerializeField]
    protected float Score;

    [SerializeField]
    protected Color RenderingColor;

    protected virtual void PlayerLocationControl()  // virtual olmasýnýn sebebi
    {                                               // biraz daha destroy iþlemi biraz daha esnek bir þey olsun diye
        if (Player.transform.position.z > transform.position.z + 12.5f)
        {
            Destroy(gameObject);
        }
    }

    protected void Move()
    {
        GetComponent<Rigidbody>().velocity = -Vector3.forward * Speed;
    }

    protected void DamageAndDeath(string tag, float damageAmount, float score, GameObject other)
    { // böyle yazýyoruz çünkü hem car hem de tankta ayný yapýyý kullanýyoruz
        switch (tag)
        {
            case "Player":
                if (DoFunction)
                {
                    Player.GetComponent<PlayerControl>().healt -= damageAmount;
                    Destroy(gameObject);
                }
                break;

            case "Bullet":

                Player.GetComponent<PlayerControl>().IncreaseScore(score);
                Destroy(other.gameObject);
                Destroy(gameObject);

                break;

            case "EnemyDiedLine":
                Destroy(gameObject);
                break;
        }
    }
}
