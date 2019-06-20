using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heladito_scrpt : MonoBehaviour
{
    public int Sabor;
    public int daño_creador;
    public GameObject[] Explo;
    public GameObject Daño_prefab;
    public Stats creador;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        daño_creador = Mathf.RoundToInt(creador.atq[creador.lvl_atq]);
    }
    void Daño_color(GameObject GO, int daño)
    {
        if (daño <= 5)
        {
            GO.GetComponentInChildren<Text>().color = Color.white;
            GO.GetComponentInChildren<Text>().transform.localScale = new Vector3(1f, 1f, 1);
        }
        else if (daño < 10)
        {
            GO.GetComponentInChildren<Text>().color = Color.yellow;
            GO.GetComponentInChildren<Text>().transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
        else if (daño >= 10)
        {
            GO.GetComponentInChildren<Text>().color = Color.red;
            GO.GetComponentInChildren<Text>().transform.localScale = new Vector3(7.5f, 7.5f, 1);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponentInChildren<Stats>() != null)
        {
            Stats malo_stats = collision.GetComponentInChildren<Stats>();
            if (malo_stats.Equipo != creador.Equipo)
            {
                collision.GetComponent<Rigidbody>().AddForce(new Vector3(0.7f, 0.6f, 0), ForceMode.Impulse);
                int daño = Random.Range(daño_creador - 2, daño_creador + 2);
                Instantiate(Explo[Sabor], transform.position, Quaternion.identity);
                GameObject GO = Instantiate(Daño_prefab, collision.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
                GO.GetComponentInChildren<Text>().text = daño.ToString();
                Daño_color(GO, daño);
                malo_stats.ADD_Vida(-daño);
                Destroy(gameObject);
            }
        }
    }
}
