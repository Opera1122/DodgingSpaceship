using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject spaceShip;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(collision.gameObject);
            spaceShip.GetComponent<PlayerController>().UnArmedShield();
        }

        spaceShip.GetComponent<PlayerController>().ItemEvent(collision);
    }
}
