using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    public int lvl_vida, lvl_atq, lvl_spd, lvl_atq_spd, lvl_escudo;
    public float Vida, Power;
    public int[] atq = { 6, 7, 9, 11, 13 }, vida_inicial = { 100, 120, 150, 170, 200 }, escudo = {3, 5, 8, 9, 12}; //hasta 5 niveles
    public float[] velocidad = { 1.3f, 1.5f, 1.7f, 1.9f, 2f }, Atq_speed = { 0.9f, 1.1f, 1.3f, 1.4f, 1.5f };
    public RectTransform Power_Rect, Vida_Rect;
    public string Equipo;
    Panel_scrpt panel_Scrpt;
    public bool selected;
    // Start is called before the first frame update
    public float[] salida_de_datos()
    {
        float[] otp = { vida_inicial[lvl_vida], atq[lvl_atq], velocidad[lvl_spd], Atq_speed[lvl_atq_spd], lvl_vida, lvl_atq, lvl_spd, lvl_atq_spd, Vida, Power };
        return otp;
    }
    void Start()
    {
        Vida = vida_inicial[lvl_vida];
        ADD_Power(0);
        ADD_Vida(0);
        panel_Scrpt = GameObject.FindGameObjectWithTag("Panel").GetComponent<Panel_scrpt>();
    }
    private void Update()
    {
    }
    public void ADD_Power(int suma)
    {
        Power += suma;
        if (Power <= 100)
        {
            Power_Rect.anchorMax = new Vector2((Power / 100) - 1, 0.5f);
        }
        else
        {
            Power_Rect.anchorMax = new Vector2((100 / 100) - 1, 0.5f);
        }
        select_update();
    }
    public void ADD_Vida(int suma)
    {
        Vida += suma;
        if (Vida <= vida_inicial[lvl_vida])
        {
            Vida_Rect.anchorMax = new Vector2(Vida / vida_inicial[lvl_vida] - 1, 0.5f);
        }
        else
        {
            Vida_Rect.anchorMax = new Vector2(vida_inicial[lvl_vida] / vida_inicial[lvl_vida] - 1, 0.5f);
        }
        Vida_Rect.GetComponent<Image>().color = new Color(((vida_inicial[lvl_vida] - Vida) * 3) / 255, (Vida * 3) / 255, 0);
        select_update();
    }
    public void PWRUP(int caso)
    {
        switch (caso)
        {
            case 0:
                lvl_atq++;
                break;
            case 1:
                lvl_vida++;
                Vida += 20;
                break;
            case 2:
                lvl_spd++;
                break;
            case 3:
                lvl_atq_spd++;
                break;
            case 4:
                Vida += 20;
                break;
            case 5:
                lvl_escudo++;
                break;
        }
        select_update();
    }
    public void select_update()
    {
        if (selected == true)
        {
            panel_Scrpt.Get_vals();
            panel_Scrpt.Texto_update();
        }
    }
}
