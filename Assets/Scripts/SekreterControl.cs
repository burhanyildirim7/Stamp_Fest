using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SekreterControl : MonoBehaviour
{
    Animator sekreterAnim;
    bool isWalk = false;

     Transform targetTablePoint;
     Transform leavePerspectivePoint;
     public Vector3 sekreterSpawnPoint;
     void Start()
    {
        sekreterAnim = GetComponent<Animator>();
        targetTablePoint = GameObject.FindGameObjectWithTag("tablePoint").transform;
        leavePerspectivePoint = GameObject.FindGameObjectWithTag("door").transform;
       
        WalkToTable();
    }

  
    void WalkToTable()
    {

    
            sekreterAnim.SetBool("switchWalkToIdle", true);
            transform.LookAt(targetTablePoint);
        transform.DOMove(targetTablePoint.transform.position, 3).OnComplete(() => {
        isWalk = false;
      
        StartCoroutine(HandsUp());
           
        });
           
    
      
    }

    void WalkToDoor()
    {
        transform.LookAt(leavePerspectivePoint);
        sekreterAnim.SetBool("switchWalkToIdle", true);
        transform.DOMove(leavePerspectivePoint.transform.position, 3).OnComplete(()=> {
            Destroy(gameObject);
       
        });
    }

    IEnumerator HandsUp()
    {
        transform.LookAt(new Vector3(0,-10,0));
        sekreterAnim.SetBool("handsUp", true);
      

        yield return new WaitForSeconds(0.5f);
        GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().DamgaliKagitlarSekretere();
        sekreterAnim.SetBool("switchWalkToIdle", false);
        WalkToDoor();
    }
}
