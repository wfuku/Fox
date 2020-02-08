using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private EnemyState enemyState;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = GetComponentInParent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //攻撃を受けた、プレイヤーに触れた
    void OnTriggerEnter2D(Collider2D EnemyHit)
    {
        string tag = EnemyHit.gameObject.tag;
        if (EnemyHit.gameObject.tag == "AttackArea")
        {
            enemyState.ReceiveDamage();
        }else if(EnemyHit.gameObject.tag == "Player")
        {
            enemyState.EMotionState();
        }
    }

}
