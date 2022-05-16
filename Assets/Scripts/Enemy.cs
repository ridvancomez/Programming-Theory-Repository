using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    protected bool DoFunction; // bu playerin �n�nde hemen belirince hasar vurmas�n diye koyuldu
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

    protected virtual void PlayerLocationControl()  // virtual olmas�n�n sebebi
    {                                               // biraz daha destroy i�lemi biraz daha esnek bir �ey olsun diye
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
    { // b�yle yaz�yoruz ��nk� hem car hem de tankta ayn� yap�y� kullan�yoruz
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
