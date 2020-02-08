using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoriBody : MonoBehaviour
{
    public GameObject HitEnemy;
    private MinoriState MinoriState;

    // Start is called before the first frame update
    void Start()
    {
        MinoriState = GetComponentInParent<MinoriState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void OnTriggerEnter2D(Collider2D MinoriHit)
    {
        string tag = MinoriHit.gameObject.tag;
        if (MinoriHit.gameObject.tag == "Enemy")
        {
            HitEnemy = MinoriHit.transform.root.gameObject;
            MinoriState.Yarare();
        }
    }


    ////敵の攻撃を受ける
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    string tag = other.gameObject.tag;
    //    Debug.Log(tag);
    //    if (other.gameObject.tag == "EnemyAttackArea" && !isMuteki)
    //    {
    //        hp -= 10;
    //        animator.SetTrigger("isDamage");
    //        this.rb.velocity = transform.right * -6;

    //    }
    //}
}
