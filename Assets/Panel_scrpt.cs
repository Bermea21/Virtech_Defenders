using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_scrpt : MonoBehaviour
{
    RaycastHit RaycastHit;
    public char Equipo;
    public float vida, vida_max, power, power_max, lvl_atq, lvl_vida, lvl_spd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit, 10))
            {
                if (RaycastHit.collider.gameObject.layer == Equipo)
                {
                    switch (RaycastHit.collider.tag)
                    {
                        case "Ivancho":

                            break;
                    }
                }
            }
        }
    }
}
