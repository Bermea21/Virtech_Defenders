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
    int LYRMSK;
    bool atacando = false;
    bool aire_b = false;
    public GameObject Daño_prefab;
    int daño;
    string equipo_malo;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Stats = GetComponentInChildren<Stats>();
        equipo_malo = gameObject.layer == 8? "B" : "A";
        LYRMSK = gameObject.layer == 8 ? LYRMSK = 1 << 9 : LYRMSK = 1 << 8 ;
        dir = gameObject.layer == 8 ? 1 : -1;
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, new Vector3(dir,0,0), out RH, 0.5f, LYRMSK))
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
        if (!Physics.Raycast(transform.position, Vector3.down, 0.8f) && ulti == false)
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
        Collider[] collis = Physics.OverlapBox(transform.position + new Vector3(dir * 0.2f,0), new Vector3(0.4f, 0.2f, 0), Quaternion.identity);
        foreach (Collider collider in collis)
        {
            daño = Mathf.RoundToInt(Random.Range(Stats.atq[Stats.lvl_atq] - 1, Stats.atq[Stats.lvl_atq] + 1));
            if (collider.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats malo_stats = collider.GetComponentInChildren<Stats>();
                if (malo_stats.Equipo != Stats.Equipo)
                {
                    collider.GetComponent<Rigidbody>().AddForce(new Vector3(Stats.empuje[Stats.lvl_empuje]*dir, Stats.empuje[Stats.lvl_empuje], 0), ForceMode.Impulse);
                    Stats.ADD_Power(7);
                    malo_stats.ADD_Vida(-daño);
                    int robo_vida = daño > Stats.robo_vida[Stats.lvl_robo_vida] ? (Stats.robo_vida[Stats.lvl_robo_vida] - daño) + daño : daño;
                    Stats.ADD_Vida(robo_vida);
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
            daño = Mathf.RoundToInt(Random.Range(Stats.atq[Stats.lvl_atq] - 1, Stats.atq[Stats.lvl_atq] + 2));
            if (collider.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats malo_stats = collider.GetComponentInChildren<Stats>();
                if (malo_stats.Equipo != Stats.Equipo)
                {
                    collider.GetComponent<Rigidbody>().AddForce(new Vector3(Stats.empuje[Stats.lvl_empuje] * dir, Stats.empuje[Stats.lvl_empuje], 0), ForceMode.Impulse);
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
    void aire()
    {
        Animator.SetInteger("estado", 2);
        atacando = false;
    }
    public void Dest()
    {
        Destroy(gameObject);
    }
}
