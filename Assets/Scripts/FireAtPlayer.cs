using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody rigid;
    public GameObject player;
    public string playerObjectName = "Space Ship";
    public float lifeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerObjectName);
        Destroy(gameObject, lifeTime);

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            rigid = GetComponent<Rigidbody>();
            rigid.velocity = direction * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.velocity = transform.forward * speed;
    }
}
