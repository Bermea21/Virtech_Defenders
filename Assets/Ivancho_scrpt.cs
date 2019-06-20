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
    public Collider[] collis;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Stats = GetComponentInChildren<Stats>();
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f, LYRMSK))
        {
            if (RH.collider.tag == "Malo")
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
        }
        else if (atacando == false)
        {
            wlk();
        }
        if (Stats.Power >= 100)
        {
            ulti = true;
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
    }
    void wlk()
    {
        Animator.SetInteger("estado", 0);
        transform.Translate(new Vector3(dir * Time.deltaTime * 1.5f, 0, 0), Space.World);
    }
    void atk()
    {
        Animator.SetInteger("estado", 1);
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
        /*if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f, LYRMSK))
        {
            if (RH.collider.tag == "Malo")
                RH.collider.GetComponent<Rigidbody>().AddForce(new Vector3(2,2,0),ForceMode.Impulse);
                Stats.ADD_Power(14);
        }*/
        collis = null;
        collis = Physics.OverlapBox(transform.position + new Vector3(dir * 0.15f, 0, 0), new Vector3(0.2f, 0.3f, 0), Quaternion.identity,LYRMSK);
        foreach (Collider collider in collis)
        {
            daño = Mathf.RoundToInt(Random.Range(Stats.Daño[Stats.lvl_atq] - 3, Stats.Daño[Stats.lvl_atq] + 3));
            if (collider.tag == "Malo")
            {
                collider.GetComponent<Rigidbody>().AddForce(new Vector3(2, 1, 0), ForceMode.Impulse);
                Stats.ADD_Power(7);
                GameObject GO = Instantiate(Daño_prefab, collider.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
                GO.GetComponentInChildren<Text>().text = daño.ToString();
                Stats stats_malo = collider.GetComponentInChildren<Stats>();
                stats_malo.ADD_Vida(-daño);
            }
        }
        collis = null;
    }
    public void slash_ulti()
    {
        /* if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f, LYRMSK))
         {
             if (RH.collider.tag == "Malo")
                 RH.collider.GetComponent<Rigidbody>().AddForce(new Vector3(2, 2, 0), ForceMode.Impulse);
             Stats.ADD_Power(14);
         }
         transform.Translate(new Vector3(dir * 0.15f, 0, 0), Space.World);*/
        collis = null;
        collis = Physics.OverlapBox(transform.position + new Vector3(dir * 0.15f, 0, 0), new Vector3(0.4f, 0.3f, 0), Quaternion.identity,LYRMSK);
        foreach (Collider collider in collis)
        {
            daño = Mathf.RoundToInt(Random.Range(Stats.Daño[Stats.lvl_atq] - 1, Stats.Daño[Stats.lvl_atq] + 6));
            if (collider.tag == "Malo")
            {
                collider.GetComponent<Rigidbody>().AddForce(new Vector3(5, 1, 0), ForceMode.Impulse);
                Stats.ADD_Power(7);
                GameObject GO = Instantiate(Daño_prefab, collider.transform.position + new Vector3(Random.Range(-0.55f, 0.55f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
                GO.GetComponentInChildren<Text>().text = daño.ToString();
                Stats stats_malo = collider.GetComponentInChildren<Stats>();
                stats_malo.ADD_Vida(-daño);
            }
        }
        collis = null;
    }
    void aire()
    {
        Animator.SetInteger("estado", 2);
        atacando = false;
    }
}
