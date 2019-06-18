using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ivancho_scrpt : MonoBehaviour
{

    Animator Animator;
    public int dir = 1;
    RaycastHit RH;
    public GameObject Heladito_prefab;
    public Sprite[] sprites;
    ContactFilter2D ContactFilter2D;
    Stats Stats;
    public bool ulti;
    bool ulting;
    int LYRMSK = 1<<9;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Stats = GetComponentInChildren<Stats>();
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f,LYRMSK))
        {
            if (RH.collider.tag == "Malo")
                if (ulti == false)
                {
                    atk();
                }
                else
                {
                    ultimate_atk();
                }
        }
        if (!Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f, LYRMSK) && ulti == false)
        {
            wlk();
            Debug.Log("aaa");
        }
        else if (!Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.1f) && ulting == false)
        {
            wlk();
        }
        if (Stats.Power >= 100)
        {
            ulti = true;
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
    void ultimate_atk()
    {
        Animator.SetInteger("estado", 3);
        ulting = true;
    }
    void ultimate_return()
    {
        Animator.SetInteger("estado", 0);
        Stats.Power = 0;
        ulti = false;
        ulting = false;
    }
    public void slash()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f, LYRMSK))
        {
            if (RH.collider.tag == "Malo")
                RH.collider.GetComponent<Rigidbody>().AddForce(new Vector3(2,2,0),ForceMode.Impulse);
                Stats.ADD_Power(14);
        }
    }
    public void slash_ulti()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 0.2f, LYRMSK))
        {
            if (RH.collider.tag == "Malo")
                RH.collider.GetComponent<Rigidbody>().AddForce(new Vector3(2, 2, 0), ForceMode.Impulse);
            Stats.ADD_Power(14);
        }
        transform.Translate(new Vector3(dir * 0.15f, 0, 0), Space.World);
    }
}
