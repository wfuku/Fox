using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public GameObject panelScript;
    public Collider2D enemyBody;
    public float Hp,RunSpeed=1.5f,AttackPoint,freezeTime=15f,fuckTime;//体力、敵の移動速度、攻撃力、攻撃間隔、モーション段階移行までの時間
    public bool Dead = false, isWalk,isWait = true,isChase,isAttack1,isAttack2,EMotion;
    public int ELevel = 0;
    private float  elapsedTime, waitTime;
    public string state = "IDLE";
   

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        //panelScript.GetComponent<PanelEffect>();
        waitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        if (EMotion)
        {
            IsEmotion();
        }
        animator.SetInteger("ELevel", ELevel);

        //死亡演出に移行
        if (Hp == 0) { EnemyDead(); }
    }

    void Chase()
    {

        //　見回りまたはキャラクターを追いかける状態
        if (isChase == false)
        {
            this.rb.velocity = transform.right * RunSpeed * -1;
        }
        if (isChase == true)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                animator.SetBool("isAttack1", true);
                waitTime = freezeTime;
            }
        }
    }
    //ダメージをうけた
    public void ReceiveDamage()
    {
        Hp -= 1;
        //ノックバックを予定
    }
    //EMotion
    public void EMotionState()
    {
        waitTime = fuckTime;
        ELevel = 0;
        enemyBody.enabled = false;
        animator.SetBool("EMotion", true);
        EMotion = true;
        RunSpeed = 0;
    }
    //EMotion中に一定時間たつごとにモーション段階進行
    public void IsEmotion()
    {

        waitTime -= Time.deltaTime;
        if(waitTime <= 0 && ELevel < 2)//モーション段階によって書き換え
        {
            ++ELevel;
            waitTime = fuckTime;
        }

    }
    //EMotionの終了
    public void EMotionEnd()
    {
        enemyBody.enabled = true;
        EMotion = false;
        animator.SetBool("EMotion", false);
        RunSpeed = 1.5f;
        ELevel = 0;

        Debug.Log("end");
    }
    //フィニッシュ時の暗転処理
    public void EMotionFinish()
    {
        //panelScript.GetComponent<PanelEffect>().BlackOut(3f, 0.02f);
    }

    //死亡
    void EnemyDead() {
        Destroy(gameObject);
    }

}
