using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinoriState : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;


    public float hp = 100f;
    public float ep = 100f; //HPとEP
    public float runSpeed = 3f,interval;       // 走っている間の速度
    public bool isGround = true;        // 地面と接地しているか管理するフラグ
    public bool isAttack1 = false;      // 攻撃を管理するフラグ
    public bool isGuard = false;        // 防御を管理するフラグ
    public bool isStop = false;         // 停止を管理するフラグ
    public bool tane,biyaku,tamago,yarare,isMuteki;   //状態異常を管理するフラグ
    public bool YDamage, RDamage;
    int key = 0;                        // 左右の入力管理
    public Collider2D AttackArea;//攻撃範囲
    public Collider2D bodyCollider;//キャラ範囲

    public string state,weahter;// プレイヤーの状態,天気
    string prevState;            // 前の状態を保存
    float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数


    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        animator.SetFloat("HP", hp);//HP、EPのアニメーションへの反映
        animator.SetFloat("EP", ep);

        if (yarare == false)
        {
            GetInputKey();          // 入力を取得
            Move();                 // 入力に応じて移動する
            ChangeAnimation();      // 状態に応じてアニメーションを変更する
            Condition();　　　　　　//状態異常について
        }
        else if (yarare == true) {
            isYarare();
        }


    }

    //操作
    void GetInputKey()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (isGround == true && isAttack1 != true)
            {
                state = "RUN";
                key = 1;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (isGround == true && isAttack1 != true)
            {
                state = "BACK";
                key = -1;
            }
        }
        else if (isGround == true && isAttack1 != true)
        {
            state = "IDLE";
            key = 0;
        }


    }

    void Move()
    {
        // 接地している時にUpキーでジャンプ
        if (isGround)
        {
            //guard
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && state != "JUMP" && isGround == true)
            {
                state = "GUARD";
                isGuard = true;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                animator.speed = 1;
                isGuard = false;
            }
            //自慰
            else if (Input.GetKey(KeyCode.X))
            {
                state = "ZII";
                if(ep > 0)ep -= 0.1f;
                if(hp < 100)hp += 0.01f;
                stateEffect = 0f;
            }else if (Input.GetKeyUp(KeyCode.X))
            {
                stateEffect = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && isAttack1 != true && (key != 0|| state == "IDLE"))//移動と待機以外ではジャンプできない
            {
                state = "JUMP";
                isGround = false;
                isGuard = false;
                animator.speed = 1;
            }
            //攻撃
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                //isAttack1 = true;
                state = "ATTACK1";
                isAttack1 = true;
            }
        }
            this.rb.velocity = transform.right * key * this.runSpeed * stateEffect;
    }

    //攻撃開始、終了
    void OnAttackArea()
    {
        AttackArea.enabled = true;
    }
    void OffAttackArea()
    {
        isAttack1 = false;
        AttackArea.enabled = false;
    }
    void GuardStart() //
    {
        if (isGuard == true)
        {
            animator.speed = 0;
        }
    }

    //G終了
    void ZiiEnd()
    {
        ep = 100;
        stateEffect = 1f;
        animator.SetBool("isZii",false);
    }

    //ジャンプ中
    void JumpStart()
    {
        isGround = false;
        stateEffect = 2f;
    }
    void JumpEnd()
    {
        isGround = true;
    }
    //状態異常
    void Condition()
    {
        if (biyaku){ animator.SetBool("isBiyakuMode",true);}
    }

    //やられ開始、やられ、やられ終了
    public void Yarare()
    {
        sprite.enabled = false;
        bodyCollider.enabled = false;
        rb.simulated = false;
        state = "Yarare";
        yarare = true;
    }
    void isYarare()
    {
        if (ep > 0)
        {
            ep -= 00.1f;
            hp -= 0.05f;
            if (Input.GetKeyDown(KeyCode.Space) && hp < 0f) { YarareEnd(); }//やられ後スペースで解除
        }
        else YarareDead();
    }

    public void YarareEnd()
    {
        Invoke("ReceiveDamage", 2f);
        sprite.enabled = true;
        rb.simulated = true;
        yarare = false;
        animator.SetTrigger("isDamage");
        transform.GetChild(1).gameObject.GetComponent<MinoriBody>().HitEnemy.GetComponent<EnemyState>().EMotionEnd();
        this.rb.AddForce(new Vector2(-400f, 150f),ForceMode2D.Impulse);
        StartCoroutine("Blink");

    }
    //やられ中EPが尽きたとき
    private void YarareDead()
    {
        transform.GetChild(1).gameObject.GetComponent<MinoriBody>().HitEnemy.GetComponent<EnemyState>().EMotionFinish();
    }
    public void ReceiveDamage()
    {
        bodyCollider.enabled = true;
        state = "IDLE";
    }

    //アニメーション、状態遷移
    void ChangeAnimation()
    {
        animator.SetBool("isJump", false);
        animator.SetBool("isAttack1", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isBack", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isGuard", false);
        animator.SetBool("isZii", false);

        switch (state)
        {
            case "JUMP":
                animator.SetBool("isJump", true);
                break;

            case "BACK":
                animator.SetBool("isBack", true);
                if (biyaku) { stateEffect = 0.2f; }else{stateEffect = 0.7f;}
                break;

            case "RUN":

                animator.SetBool("isRun", true);
                if (biyaku) { stateEffect = 0.2f; }else{stateEffect = 1.5f;}
                break;

            case "ATTACK1":
                animator.SetBool("isAttack1", true);


                stateEffect = 0.1f;
                break;

            case "GUARD":
                animator.SetBool("isGuard", true);
                stateEffect = 0f;
                break;

            case "ZII":
                animator.SetBool("isZii", true);
                break;

            case "Yarare":
                stateEffect = 0f;
                break;

            case "IDLE":
                animator.SetBool("isIdle", true);
                stateEffect = 1f;
                break;

            default:
                animator.SetBool("isIdle", true);
                stateEffect = 1f;
                break;

        }
    }

    //点滅
    IEnumerator Blink()
    {
        int i = 0;
        while (true)
        {
        if(i<=10){
            ++i;
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(interval);
        }
        else {
            sprite.enabled = true;
            break;
        }
        }
    }
}