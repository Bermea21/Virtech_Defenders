using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ivancho_scrpt : MonoBehaviour
{

    Animator Animator;
    public int dir = 1;
    RaycastHit RH;
    Stats Stats;
    public bool ulti;
    int LYRMSK = 1 << 9;
    bool atacando = false;
    public bool aire_b = false;
    public GameObject Daño_prefab;
    int daño;
    string equipo_malo;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Stats = GetComponentInChildren<Stats>();
        if (Stats.Equipo == "A")
        {
            equipo_malo = "B";
        }
        else if (Stats.Equipo == "B")
        {
            equipo_malo = "A";
        }
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.5f, LYRMSK))
        {
            if (RH.collider.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats stats_malo = RH.collider.GetComponentInChildren<Stats>();
                if (stats_malo.Equipo == equipo_malo)
                {
                    if (ulti == false)
                    {
                        atk();
                    }
                    else
                    {
                        ultimate_atk();
                    }
                    atacando = true;
                }
                else if (atacando == false)
                {
                    wlk();
                }
            }
        }
        else if (atacando == false)
        {
            wlk();
        }
        if (Stats.Power >= 100)
        {
            ulti = true;
            Stats.Power = 100;
        }
        if (!Physics.Raycast(transform.position, Vector3.down, 0.7f))
        {
            aire_b = true;
            aire();
        }
        else
        {
            aire_b = false;
        }
        No_coll();
    }
    void wlk()
    {
        Animator.SetInteger("estado", 0);
        transform.Translate(new Vector3(dir * Time.deltaTime * Stats.velocidad[Stats.lvl_spd], 0, 0), Space.World);
        Animator.speed = 1;
    }
    void atk()
    {
        Animator.SetInteger("estado", 1);
        Animator.speed = Stats.Atq_speed[Stats.lvl_atq_spd];
    }
    void atk_return()
    {
        atacando = false;
    }
    void ultimate_atk()
    {
        Animator.SetInteger("estado", 3);
    }
    void ultimate_return()
    {
        Animator.SetInteger("estado", 0);
        Stats.Power = 0;
        ulti = false;
        atacando = false;
        Stats.ADD_Power(0);
    }
    public void slash()
    {
        Collider[] collis = Physics.OverlapBox(transform.position + new Vector3(dir * 0.15f, 0, 0), new Vector3(0.1f, 0.2f, 0), Quaternion.identity);
        foreach (Collider collider in collis)
        {
            daño = Mathf.RoundToInt(Random.Range(Stats.atq[Stats.lvl_atq] - 3, Stats.atq[Stats.lvl_atq] + 4));
            if (collider.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats malo_stats = collider.GetComponentInChildren<Stats>();
                if (malo_stats.Equipo != Stats.Equipo)
                {
                    collider.GetComponent<Rigidbody>().AddForce(new Vector3(2, 1, 0), ForceMode.Impulse);
                    Stats.ADD_Power(7);
                    GameObject GO = Instantiate(Daño_prefab, collider.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
                    Daño_color(GO,daño);
                    GO.GetComponentInChildren<Text>().text = daño.ToString();
                    malo_stats.ADD_Vida(-daño);
                    Stats.select_update();
                }
            }
        }
    }
    public void slash_ulti()
    {
        Collider[] collis = Physics.OverlapBox(transform.position + new Vector3(dir * 0.2f, 0, 0), new Vector3(0.4f, 0.9f, 0), Quaternion.identity);
        foreach (Collider collider in collis)
        {
            daño = Mathf.RoundToInt(Random.Range(Stats.atq[Stats.lvl_atq] - 2, Stats.atq[Stats.lvl_atq] + 7));
            if (collider.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats malo_stats = collider.GetComponentInChildren<Stats>();
                if (malo_stats.Equipo != Stats.Equipo)
                {
                    collider.GetComponent<Rigidbody>().AddForce(new Vector3(2, 1, 0), ForceMode.Impulse);
                    GameObject GO = Instantiate(Daño_prefab, collider.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
                    GO.GetComponentInChildren<Text>().text = daño.ToString();
                    malo_stats.ADD_Vida(-daño);
                    Stats.select_update();
                }
            }
        }
    }
    void No_coll()
    {
        Collider[] colli = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.6f, 0.1f));
        foreach (Collider col in colli)
        {
            if (col.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats stats_amigo = col.gameObject.GetComponentInChildren<Stats>();
                if (stats_amigo.Equipo == Stats.Equipo)
                {
                    Physics.IgnoreCollision(col, GetComponent<Collider>());
                }
            }
        }
    }
    void Daño_color(GameObject GO, int Daño)
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
    void aire()
    {
        Animator.SetInteger("estado", 2);
        atacando = false;
    }
}
