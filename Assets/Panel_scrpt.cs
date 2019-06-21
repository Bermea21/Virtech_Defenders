using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_scrpt : MonoBehaviour
{
    RaycastHit RaycastHit;
    public string Equipo;
    public float vida, vida_max, power, power_max, atq, spd, atq_spd, escudo, empuje,robar_vida,vircoins_cosecha, vircoins_cosecha_multip = 1;
    public int lvl_atq, lvl_vida, lvl_spd, lvl_atq_spd,lvl_escudo,lvl_empuje, vircoins_i,lvl_rv;
    public GameObject Selected;
    public Button[] Mejoras;
    Stats stats_selected;
    Animator animator;
    public Text lvl_atq_txt,lvl_vida_txt,lvl_spd_txt,lvl_atq_spd_txt, lvl_escudo_txt, vida_txt,power_txt,vida_max_txt,spd_txt,atq_txt,atq_spd_txt,escudo_txt,lvl_empuje_txt,lvl_robar_vida,empuje_txt,robar_vida_txt,vircoins_txt,produccion_txt;
    public Text costo_atq_t, costo_vida_t, costo_spd_t, costo_spd_atq_t,costo_add_vida_t, costo_escudo_t, costo_empuje_t, costo_rv_t;
    cam_zoomscrpt cam_Zoomscrpt;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam_Zoomscrpt = GameObject.Find("Zoom_cam").GetComponent<cam_zoomscrpt>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit, 13))
            {
                if (RaycastHit.collider.GetComponentInChildren<Stats>() != null)
                {
                    Debug.Log(RaycastHit.collider.name);
                    if(RaycastHit.collider.GetComponentInChildren<Stats>().Equipo == Equipo)
                    {
                        stats_selected = RaycastHit.collider.GetComponentInChildren<Stats>();
                        Selected = RaycastHit.collider.gameObject;
                        stats_selected.selected = true;
                        Get_vals();
                        costos();
                        Texto_update();
                        animator.SetBool("On",true);
                        cam_Zoomscrpt.target = RaycastHit.collider.transform;
                    }
                }
            }
        }
        if(stats_selected != null)
        {
            if (Input.GetKeyDown(KeyCode.Q)){
                stats_selected.PWRUP(0);
                Texto_update();
            }
        }
        vircoins_cosecha += Time.deltaTime * vircoins_cosecha_multip;
        vircoins_i = Mathf.RoundToInt(vircoins_cosecha);
        vircoins_txt.text = vircoins_i.ToString();
        produccion_txt.text = $"{vircoins_cosecha_multip} VT/s";
        Hotkeys();
    }
    private void Hotkeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Compra(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Compra(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Compra(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Compra(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Compra(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            Compra(5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            Compra(6);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            Compra(7);
    }
    public void Get_vals()
    {
        lvl_atq = stats_selected.lvl_atq;
        lvl_spd = stats_selected.lvl_spd;
        lvl_vida = stats_selected.lvl_vida;
        lvl_atq_spd = stats_selected.lvl_atq_spd;
        lvl_escudo = stats_selected.lvl_escudo;
        lvl_empuje = stats_selected.lvl_empuje;
        lvl_rv = stats_selected.lvl_robo_vida;
        vida_max = stats_selected.vida_inicial[lvl_vida];
        vida = stats_selected.Vida;
        power = stats_selected.Power;
        atq = stats_selected.atq[lvl_atq];
        spd = stats_selected.velocidad[lvl_spd];
        atq_spd = stats_selected.Atq_speed[stats_selected.lvl_atq_spd];
        escudo = stats_selected.escudo[lvl_escudo];
        empuje = stats_selected.empuje[lvl_empuje];
        robar_vida = stats_selected.robo_vida[lvl_rv];
        Valores_update();
        Mejoras[0].interactable = lvl_atq == 4 ? false : true;
        Mejoras[1].interactable = lvl_spd == 4 ? false : true;
        Mejoras[2].interactable = lvl_vida == 4 ? false : true;
        Mejoras[3].interactable = lvl_atq_spd == 4 ? false : true;
        Mejoras[4].interactable = lvl_escudo == 4 ? false : true;
        Mejoras[5].interactable = lvl_empuje == 4 ? false : true;
        Mejoras[6].interactable = lvl_rv == 4 ? false : true;
    }
    public void Texto_update()
    {
        if (lvl_atq == 4)
        {
            lvl_atq_txt.text = $"Ataque LV MAX:";
            lvl_atq_txt.color = Color.red + Color.black;
        }
        else
        {
            lvl_atq_txt.text = $"Ataque LV {(lvl_atq + 1)}:";
            lvl_atq_txt.color = Color.black;
        }
        if (lvl_vida == 4)
        {
            lvl_vida_txt.text = $"Vida LV MAX:";
            lvl_vida_txt.color = Color.red + Color.black;
        }
        else
        {
            lvl_vida_txt.text = $"Vida LV {(lvl_vida + 1)}:";
            lvl_vida_txt.color = Color.black;
        }
        if (lvl_spd == 4)
        {
            lvl_spd_txt.text = $"Velocidad mov MAX:";
            lvl_spd_txt.color = Color.red + Color.black;
        }
        else
        {
            lvl_spd_txt.text = $"Velocidad mov LV {(lvl_spd + 1)}:";
            lvl_spd_txt.color = Color.black;
        }
        if (lvl_atq_spd == 4)
        {
            lvl_atq_spd_txt.text = $"Velocidad atq LV MAX:";
            lvl_atq_spd_txt.color = Color.red + Color.black;
        }
        else
        {
            lvl_atq_spd_txt.text = $"Velocidad atq LV {(lvl_atq_spd + 1)}:";
            lvl_atq_spd_txt.color = Color.black;
        }
        if(lvl_escudo == 4)
        {
            lvl_escudo_txt.text = $"Escudo LV MAX:";
            lvl_escudo_txt.color = Color.red + Color.black;
        }
        else
        {
            lvl_escudo_txt.text = $"Escudo LV {(lvl_escudo + 1)}:";
            lvl_escudo_txt.color = Color.black;
        }
        if(lvl_empuje == 4)
        {
            lvl_empuje_txt.text = $"Empuje LV MAX";
            lvl_empuje_txt.color = Color.red + Color.black;
        }
        else
        {
            lvl_empuje_txt.text = $"Empuje LV {lvl_empuje + 1}:";
            lvl_empuje_txt.color = Color.black;
        }
        if (lvl_rv == 4)
        {
            lvl_robar_vida.text = $"Robo vida LV MAX";
            lvl_robar_vida.color = Color.red + Color.black;
        }
        else
        {
            lvl_robar_vida.text = $"Robo vida LV {lvl_empuje + 1}:";
            lvl_robar_vida.color = Color.black;
        }
    }
    public void Valores_update()
    {
        vida_txt.text = $"{vida}/{vida_max}";
        power_txt.text = $"{power}%";
        vida_max_txt.text = vida_max.ToString();
        atq_txt.text = atq.ToString();
        atq_spd_txt.text = atq_spd.ToString();
        escudo_txt.text = escudo.ToString();
        spd_txt.text = spd.ToString();
        empuje_txt.text = empuje.ToString();
        robar_vida_txt.text = robar_vida.ToString();
    }
    public void costos()
    {
        costo_atq_t.text = $"{nuevo_precio((int)Precios.atq_lvl, lvl_atq)}";
        costo_vida_t.text = $"{nuevo_precio((int)Precios.vida_lvl, lvl_vida)}";
        costo_spd_t.text = $"{nuevo_precio((int)Precios.spd_lvl, lvl_spd)}";
        costo_spd_atq_t.text = $"{nuevo_precio((int)Precios.atq_spd, lvl_atq_spd)}";
        costo_add_vida_t.text = $"{nuevo_precio((int)Precios.add_vida, stats_selected.vida_comprada)}";
        costo_escudo_t.text = $"{nuevo_precio((int)Precios.escudo_lvl, lvl_escudo)}";
        costo_empuje_t.text = $"{nuevo_precio((int)Precios.empuje_lvl, lvl_empuje)}";
        costo_rv_t.text = $"{nuevo_precio((int)Precios.rv_lvl, lvl_rv)}";
    }
    public void PWRUP(int ent)
    {
        costos();
        stats_selected.PWRUP(ent);
    }
    public void Compra(int compra_int)
    {
        switch (compra_int)
        {
            case 0:
                if (vircoins_i >= nuevo_precio((int)Precios.atq_lvl,lvl_atq))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.atq_lvl, lvl_atq);
                    PWRUP(compra_int);
                }
                break;
            case 1:
                if (vircoins_i >= nuevo_precio((int)Precios.vida_lvl, lvl_vida))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.vida_lvl, lvl_vida);
                    PWRUP(compra_int);
                }
                break;
            case 2:
                if (vircoins_i >= nuevo_precio((int)Precios.spd_lvl, lvl_spd))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.spd_lvl, lvl_spd);
                    PWRUP(compra_int);
                }
                break;
            case 3:
                if (vircoins_i >= nuevo_precio((int)Precios.atq_spd, lvl_atq_spd))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.atq_spd, lvl_atq_spd);
                    PWRUP(compra_int);
                }
                break;
            case 4:
                if (vircoins_i >= nuevo_precio((int)Precios.add_vida, stats_selected.vida_comprada))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.add_vida, stats_selected.vida_comprada);
                    PWRUP(compra_int);
                }
                break;
            case 5:
                if (vircoins_i >= nuevo_precio((int)Precios.escudo_lvl, lvl_escudo))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.escudo_lvl, lvl_escudo);
                    PWRUP(compra_int);
                }
                break;
            case 6:
                if (vircoins_i >= nuevo_precio((int)Precios.empuje_lvl, lvl_empuje))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.empuje_lvl, lvl_empuje);
                    PWRUP(compra_int);
                }
                break;
            case 7:
                if (vircoins_i >= nuevo_precio((int)Precios.rv_lvl, lvl_rv))
                {
                    vircoins_cosecha -= nuevo_precio((int)Precios.rv_lvl, lvl_rv);
                    PWRUP(compra_int);
                }
                break;
        }
        costos();
    }
    float nuevo_precio(float precio,int lvl)
    {
        int otp = lvl > 0? Mathf.RoundToInt(precio * lvl * (1.5f+lvl/2)):Mathf.RoundToInt(precio);
        return otp;
    }
    enum Precios
    {
        vida_lvl = 30,
        atq_lvl = 25,
        spd_lvl = 15,
        rv_lvl = 35,
        atq_spd = 50,
        escudo_lvl = 35,
        empuje_lvl = 20,
        add_vida = 50
    }
    public void close()
    {
        animator.SetBool("On", false);
        stats_selected.selected = false;
        Selected = null;
    }
}
