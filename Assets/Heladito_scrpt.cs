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
    public string team;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        daño_creador = Mathf.RoundToInt(creador.atq[creador.lvl_atq]);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer.ToString() != team)
        {
            Debug.Log(collision.gameObject.name);
            int daño = Random.Range(daño_creador-2,daño_creador+2);
            Instantiate(Explo[Sabor], transform.position, Quaternion.identity);
            GameObject GO = Instantiate(Daño_prefab, collision.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
            GO.GetComponentInChildren<Text>().text = daño.ToString();
            Destroy(gameObject);
        }
    }
}
