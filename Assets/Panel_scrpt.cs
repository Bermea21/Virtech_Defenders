using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_scrpt : MonoBehaviour
{
    RaycastHit RaycastHit;
    public string Equipo;
    public float vida, vida_max, power, power_max, atq, spd, atq_spd, escudo, empuje;
    public int lvl_atq, lvl_vida, lvl_spd, lvl_atq_spd,lvl_escudo,lvl_empuje;
    public GameObject Selected;
    public Button[] Mejoras;
    Stats stats_selected;
    Animator animator;
    public Text lvl_atq_txt,lvl_vida_txt,lvl_spd_txt,lvl_atq_spd_txt, lvl_escudo_txt, vida_txt,power_txt,vida_max_txt,spd_txt,atq_txt,atq_spd_txt,escudo_txt,lvl_empuje_txt,empuje_txt;
    cam_zoomscrpt cam_Zoomscrpt;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam_Zoomscrpt = GameObject.Find("Zoom_cam").GetComponent<cam_zoomscrpt>();
    }

    // Update is called once per frame
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
    }
    public void Get_vals()
    {
        lvl_atq = stats_selected.lvl_atq;
        lvl_spd = stats_selected.lvl_spd;
        lvl_vida = stats_selected.lvl_vida;
        lvl_atq_spd = stats_selected.lvl_atq_spd;
        lvl_escudo = stats_selected.lvl_escudo;
        lvl_empuje = stats_selected.lvl_empuje;
        vida_max = stats_selected.vida_inicial[lvl_vida];
        vida = stats_selected.Vida;
        power = stats_selected.Power;
        atq = stats_selected.atq[lvl_atq];
        spd = stats_selected.velocidad[lvl_spd];
        atq_spd = stats_selected.Atq_speed[stats_selected.lvl_atq_spd];
        escudo = stats_selected.escudo[lvl_escudo];
        empuje = stats_selected.empuje[lvl_empuje];
        Valores_update();
        Mejoras[0].interactable = lvl_atq == 4 ? false : true;
        Mejoras[1].interactable = lvl_spd == 4 ? false : true;
        Mejoras[2].interactable = lvl_vida == 4 ? false : true;
        Mejoras[3].interactable = lvl_atq_spd == 4 ? false : true;
        Mejoras[4].interactable = lvl_escudo == 4 ? false : true;
        Mejoras[5].interactable = lvl_empuje == 4 ? false : true;
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
    }
    public void PWRUP(int ent)
    {
        stats_selected.PWRUP(ent);
    }
    public void close()
    {
        animator.SetBool("On", false);
        stats_selected.selected = false;
        Selected = null;
    }
}
