using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    Animator dolarAnim;
    void Start()
    {
        dolarAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dolarAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !dolarAnim.IsInTransition(0))
        {
            Destroy(gameObject);
        }
    }
}
