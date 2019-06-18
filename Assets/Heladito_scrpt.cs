using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heladito_scrpt : MonoBehaviour
{
    public int Sabor;
    public GameObject[] Explo;
    public GameObject Daño_prefab;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            int daño = Random.Range(5,8);
            Instantiate(Explo[Sabor], transform.position, Quaternion.identity);
            GameObject GO = Instantiate(Daño_prefab, collision.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
            GO.GetComponentInChildren<Text>().text = daño.ToString();
            Destroy(gameObject);
        }
    }
}
