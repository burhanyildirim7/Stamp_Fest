using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DamgaControl : MonoBehaviour
{
    public bool canDamga = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Time.timeScale += 1*Time.deltaTime;
            if (canDamga)
            {
                DamgaBasmaFunction();
            }
         
       
        }
        else
        {
            Time.timeScale = 1;
        }
        Debug.Log(Time.timeScale);
    }

    void DamgaBasmaFunction()
    {
        transform.DOMoveY(0.45f, 1).OnComplete(()=>transform.DOMoveY(0.8f,1));
        canDamga = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "damgaYok")
        {
            other.gameObject.tag = "damgaVar";
        }
    }
}
