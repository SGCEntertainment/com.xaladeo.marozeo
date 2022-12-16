using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vaganaetka"))
        {
            Destroy(gameObject);
        }
    }
}
