using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    public int lvl_vida, lvl_atq, lvl_spd;
    public float Vida,Power;
    public int[] atq,vida_inicial; //hasta 3 niveles
    public float[] velocidad;
    public RectTransform Power_Rect, Vida_Rect;
    // Start is called before the first frame update

    void Start()
    {
        Vida = vida_inicial[lvl_vida];
        ADD_Power(0);
        ADD_Vida(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PWRUP(0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ADD_Vida(0);
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
            Vida_Rect.anchorMax = new Vector2(Vida / vida_inicial[lvl_vida] - 1, 0.5f);
        }
        else
        {
            Vida_Rect.anchorMax = new Vector2(vida_inicial[lvl_vida] / vida_inicial[lvl_vida] - 1, 0.5f);
        }
        Vida_Rect.GetComponent<Image>().color = new Color(((vida_inicial[lvl_vida] - Vida)*3)/255,(Vida*3)/255,0);
        Debug.Log(Vida / vida_inicial[lvl_vida] - 1);
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
        }
    }
}
