using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heladito_scrpt : MonoBehaviour
{
    public int Sabor;
    public GameObject[] Explo;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            Instantiate(Explo[Sabor], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
