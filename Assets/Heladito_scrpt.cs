using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heladito_scrpt : MonoBehaviour
{
    public int Sabor;
    public int daño_creador,retroceso;
    public GameObject[] Explo;
    public GameObject Daño_prefab;
    public Stats creador;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        daño_creador = Mathf.RoundToInt(creador.atq[creador.lvl_atq]);
        retroceso = Mathf.RoundToInt(creador.empuje[creador.lvl_empuje]);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponentInChildren<Stats>() != null)
        {
            Stats malo_stats = collision.GetComponentInChildren<Stats>();
            if (malo_stats.Equipo != creador.Equipo)
            {
                collision.GetComponent<Rigidbody>().AddForce(new Vector3(retroceso, retroceso, 0), ForceMode.Impulse);
                int daño = Random.Range(daño_creador - 2, daño_creador + 2);
                Instantiate(Explo[Sabor], transform.position, Quaternion.identity);
                malo_stats.ADD_Vida(-daño);
                Destroy(gameObject);
            }
        }
        else if(collision.tag == "Suelo")
        {
            Instantiate(Explo[Sabor], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
