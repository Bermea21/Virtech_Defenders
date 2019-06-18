using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    public float Power;
    public float Vida;
    float vida_inicial;
    float poder_inicial;
    public RectTransform Power_Rect, Vida_Rect;
    // Start is called before the first frame update
    void Start()
    {
        vida_inicial = Vida;
        poder_inicial = Power;
        ADD_Power(0);
        ADD_Vida(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ADD_Vida(-25);
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
        if (Vida <= vida_inicial)
        {
            Vida_Rect.anchorMax = new Vector2((Vida / vida_inicial) - 1, 0.5f);
        }
        else
        {
            Vida_Rect.anchorMax = new Vector2((vida_inicial / vida_inicial) - 1, 0.5f);
        }
        Vida_Rect.GetComponent<Image>().color = new Color(((vida_inicial - Vida)*3)/255,(Vida*3)/255,0);
        Debug.Log(new Color((vida_inicial - Vida) / 255, Vida / 255, 0));
    }
}
