using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DamgaControl : MonoBehaviour
{
    public bool canDamga = false;
    public float combatSpeed;
    public ParticleSystem earnPoint;
    public int damgaHakký;

    GameObject paperControl;
    void Start()
    {
        paperControl = GameObject.FindGameObjectWithTag("PaperControl");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Time.timeScale += combatSpeed*Time.deltaTime;
            if (canDamga)
            {
                DamgaBasmaFunction();
            }
         
       
        }
        else
        {
            Time.timeScale = 1;
        }

        if (damgaHakký<paperControl.GetComponent<PaperControl>().totalPoint)
        {
          
            // FÝNÝSH BURAYA GELECEK
            Time.timeScale = 0;
        }
    
    }

    void DamgaBasmaFunction()
    {
        transform.DOMoveY(0.45f, 0.5f).OnComplete(()=>transform.DOMoveY(0.8f,0.5f));
        
        canDamga = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "damgaYok")
        {
            earnPoint.Play();
            other.gameObject.tag = "damgaVar";
        }
    }
}
