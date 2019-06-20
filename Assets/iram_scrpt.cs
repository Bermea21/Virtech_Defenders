using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iram_scrpt : MonoBehaviour
{
    Animator Animator;
    public int dir = 1;
    RaycastHit RH;
    public GameObject Heladito_prefab;
    public Sprite[] sprites;
    ContactFilter2D ContactFilter2D;
    Stats Stats;
    public bool ulti;
    bool aire_b;
    int LYRMSK = 1 << 9;
    bool atacando = false;
    string equipo_malo;
    void Start()
    {
        Animator = GetComponent<Animator>();
        Stats = GetComponentInChildren<Stats>();
        if (Stats.Equipo == "A")
        {
            equipo_malo = "B";
            LYRMSK = 1 << 9;
        }
        else if (Stats.Equipo == "B")
        {
            equipo_malo = "A";
            LYRMSK = 1 << 8;
        }
        GetComponent<SpriteRenderer>().flipX = dir == 1 ? false : true;
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 8,LYRMSK))
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
        Animator.speed = 1;
        transform.Translate(new Vector3( dir * Time.deltaTime * Stats.velocidad[Stats.lvl_spd],0,0),Space.World);
    }
    void atk()
    {
        Animator.SetInteger("estado", 1);
        Animator.speed = Stats.Atq_speed[Stats.lvl_atq_spd];
    }
    public void atk_return()
    {
        atacando = false;
    }
    void ultimate_atk()
    {
        Animator.SetInteger("estado", 3);
        Animator.speed = Stats.Atq_speed[Stats.lvl_atq_spd];
    }
    void ultimate_return()
    {
        Animator.SetInteger("estado", 0);
        Stats.Power = 0;
        ulti = false;
        atacando = false;
        Stats.ADD_Power(0);
    }
    public void proyectil()
    {
        GameObject GO = Instantiate(Heladito_prefab,transform.position + new Vector3(dir*0.2f,-0.2f,0),Quaternion.identity);
        int sabor = Random.Range(0, 4);
        GO.GetComponent<SpriteRenderer>().sprite = sprites[sabor];
        Heladito_scrpt heladito_Scrpt = GO.GetComponent<Heladito_scrpt>();
        heladito_Scrpt.Sabor = sabor;
        heladito_Scrpt.creador = Stats;
        GO.GetComponent<Rigidbody>().AddForce(new Vector3( Random.Range(4,6)*dir,Random.Range(1.8f,2.6f),0),ForceMode.Impulse);
        Stats.ADD_Power(8);
    }
    void No_coll()
    {
        Collider[] colli = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.6f, 0.1f));
        foreach (Collider col in colli)
        {
            if(col.gameObject.GetComponentInChildren<Stats>() != null)
            {
                Stats stats_amigo = col.gameObject.GetComponentInChildren<Stats>();
                if(stats_amigo.Equipo == Stats.Equipo)
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
}
