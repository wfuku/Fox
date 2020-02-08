using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchArea : MonoBehaviour
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
        enemyState.isChase = false;
        enemyState.isWait = true;
    }

    //プレイヤー発見時に追跡状態へ移行
    private void OnTriggerStay2D(Collider2D EnemySearchArea)
    {
        if(EnemySearchArea.tag == "Player")
        {
            enemyState.isChase = true;
            enemyState.isWait = false;
        }
    }
}
