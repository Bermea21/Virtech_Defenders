using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    public int lvl_vida, lvl_atq, lvl_spd, lvl_atq_spd, lvl_escudo, lvl_empuje,lvl_robo_vida,vida_comprada = 0;
    public float Vida, Power;
    public int[] atq = { 6, 7, 9, 11, 13 }, vida_inicial = { 100, 120, 150, 170, 200 }, escudo = { 3, 5, 8, 9, 12 }, robo_vida = {0,1,3,4,6}; //hasta 5 niveles
    public float[] velocidad = { 1.3f, 1.5f, 1.7f, 1.9f, 2f }, Atq_speed = { 0.9f, 1.1f, 1.3f, 1.4f, 1.5f }, empuje = { 0, 1, 2, 3, 4 };
    public RectTransform Power_Rect, Vida_Rect;
    public string Equipo;
    Panel_scrpt panel_Scrpt;
    public bool selected;
    public GameObject Texto_daño_prefab;
    // Start is called before the first frame update
    public float[] salida_de_datos()
    {
        float[] otp = { vida_inicial[lvl_vida], atq[lvl_atq], velocidad[lvl_spd], Atq_speed[lvl_atq_spd], lvl_vida, lvl_atq, lvl_spd, lvl_atq_spd, Vida, Power };
        return otp;
    }
    void Start()
    {
        Equipo = gameObject.layer == 8 ? "A" : "B";
        GetComponentInParent<SpriteRenderer>().flipX =Equipo == "A"? false : true;
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
        if (suma < 0)
        {
            suma += escudo[lvl_escudo];
            suma = suma > 0 ? 0 : -suma;
            Vida -= suma;
            GameObject GO = Instantiate(Texto_daño_prefab, transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
            Daño_color(GO, suma);
            GO.GetComponentInChildren<Text>().text = suma.ToString();
            if(Vida <= 0)
            {
                if(selected == true)
                {
                    panel_Scrpt.close();
                }
                GetComponentInParent<Animator>().SetLayerWeight(1, 1);
                GetComponentInParent<Animator>().SetBool("muerte",true);
            }
        }
        //Curacion
        else if(suma > 0)
        {
            Vida = (Vida + suma)< vida_inicial[lvl_vida]?Vida+suma:Vida = vida_inicial[lvl_vida];
            GameObject GO = Instantiate(Texto_daño_prefab, transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.23f, 0.23f), 0), Quaternion.identity);
            GO.GetComponentInChildren<Text>().text = $"+{suma}";
            GO.GetComponentInChildren<Text>().color = Color.green;
        }
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
                if(lvl_atq < 4)
                lvl_atq++;
                break;
            case 1:
                if(lvl_vida < 4)
                lvl_vida++;
                ADD_Vida(20);
                break;
            case 2:
                if(lvl_spd < 4)
                lvl_spd++;
                break;
            case 3:
                if(lvl_atq_spd <4)
                lvl_atq_spd++;
                break;
            case 4:
                ADD_Vida(20);
                vida_comprada++;
                break;
            case 5:
                if(lvl_escudo < 4)
                lvl_escudo++;
                break;
            case 6:
                if (lvl_empuje < 4)
                    lvl_empuje++;
                break;
            case 7:
                if (lvl_robo_vida < 4)
                    lvl_robo_vida++;
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
    private void Daño_color(GameObject GO, int daño)
    {
        if (daño <= 5)
        {
            GO.GetComponentInChildren<Text>().color = Color.white;
            GO.GetComponentInChildren<Text>().transform.localScale = new Vector3(1f, 1f, 1);
        }
        else if (daño < 10)
        {
            GO.GetComponentInChildren<Text>().color = Color.yellow;
            GO.GetComponentInChildren<Text>().transform.localScale = new Vector3(5.5f, 5.5f, 1);
        }
        else if (daño >= 10)
        {
            GO.GetComponentInChildren<Text>().color = Color.red;
            GO.GetComponentInChildren<Text>().transform.localScale = new Vector3(10.5f, 10.5f, 1);
        }
    }
}
