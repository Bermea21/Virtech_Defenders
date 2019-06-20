using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    public float Vida;
    public float Power;
    public int lvl_vida,lvl_atq,lvl_spd;
    public float[] vida_inicial = {100,120,140},velocidad = {1.3f,1.5f,1.8f},Daño = {6,8,10};
    float poder_inicial;
    public RectTransform Power_Rect, Vida_Rect;
    // Start is called before the first frame update
    void Start()
    {
        vida_inicial[lvl_vida] = Vida;
        poder_inicial = Power;
        ADD_Power(0);
        ADD_Vida(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PWR_UP(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PWR_UP(1);
        }
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
    }
    public void ADD_Vida(int suma)
    {
        Vida += suma;
        if (Vida <= vida_inicial[lvl_vida])
        {
            Vida_Rect.anchorMax = new Vector2((Vida / vida_inicial[lvl_vida]) - 1, 0.5f);
        }
        else
        {
            Vida_Rect.anchorMax = new Vector2((vida_inicial[lvl_vida] / vida_inicial[lvl_vida]) - 1, 0.5f);
        }
        Vida_Rect.GetComponent<Image>().color = new Color(((vida_inicial[lvl_vida] - Vida)*3)/255,(Vida*3)/255,0);
        Debug.Log(new Color((vida_inicial[lvl_vida] - Vida) / 255, Vida / 255, 0));
    }
    public void PWR_UP(int clase)
    {
        switch (clase)
        {
            case 0:
                lvl_vida++;
                ADD_Vida(20);
                break;
            case 1:
                lvl_atq++;
                break;
            case 2:
                lvl_spd++;
                break;
        }
    }
}
