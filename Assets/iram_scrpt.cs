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
    int LYRMSK = 1 << 9;
    bool atacando = false;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Stats = GetComponentInChildren<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.right * dir, out RH, 5, LYRMSK))
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
            else
            {
                atacando = false;
            }
        }
        else if(atacando == false)
        {
            wlk();
        }
        if(Stats.Power >= 100)
        {
            ulti = true;
        }
    }
    void wlk()
    {
        Animator.SetInteger("estado", 0);
        transform.Translate(new Vector3( dir * Time.deltaTime * 1.5f,0,0),Space.World);
    }
    void atk()
    {
        Animator.SetInteger("estado", 1);
    }
    public void atk_return()
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
    public void proyectil()
    {
        GameObject GO = Instantiate(Heladito_prefab,transform.position + new Vector3(dir*0.2f,-0.2f,0),Quaternion.identity);
        int sabor = Random.Range(0, 4);
        GO.GetComponent<SpriteRenderer>().sprite = sprites[sabor];
        GO.GetComponent<Heladito_scrpt>().Sabor = sabor;
        GO.GetComponent<Rigidbody>().AddForce(new Vector3( 4*dir,2,0),ForceMode.Impulse);
        Stats.ADD_Power(8);
    }
}
