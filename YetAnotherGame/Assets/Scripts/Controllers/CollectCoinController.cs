using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
