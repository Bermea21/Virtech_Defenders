using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_mov : MonoBehaviour
{
    public bool moviendose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moviendose == true)
        {
            transform.Translate(-0.6f*Time.deltaTime,0,0);
        }
    }
}
