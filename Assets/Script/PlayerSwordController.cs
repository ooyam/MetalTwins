using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordController : MonoBehaviour
{
    //Animatorの取得
    private Animator myAnimator;
    //Transformの取得
    private Transform myTransform;
    //Rigidbodyの取得
    private Rigidbody myRigidbody;
    //Animation状態の取得
    private bool Idle;
    private bool Run;
    private bool StandingJump;
    private bool RunningJump;
    private bool Attack;
    private bool Damege;
    //移動速度の設定
    private float MoveSpeed = 4.0f;
    //StandingJump初期速度の設定
    private float StandingJumpVelocity = 6.5f;
    //RunningJump初期速度の設定
    private float RunningJumpVelocity = 7.5f;
    //Jumpの減速
    private float Decelerate = 0.8f;
    //Jump時の一時停止時間(着地まで空中姿勢保持)
    private float StandingJumpPauseTime = 0.7f;
    private float RunningJumpPauseTime = 2.0f;
    //LeftButtonの状態監視
    private bool LeftButton;
    //RightButtonの状態監視
    private bool RightButton;
    //UpButtonの状態監視
    private bool UpButton;
    //着地しているかの監視
    private bool Ground;
    //時間計算用変数
    private float Delta;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();      //Animatorの取得
        myTransform = GetComponent<Transform>();    //Transformの取得
        myRigidbody = GetComponent<Rigidbody>();    //Rigidbodyの取得
    }

    // Update is called once per frame
    void Update()
    {
        //←矢印が押されたら､Player移動
        if (LeftButton == true)
        {
            GetAnimation();                                                                     //現在のアニメーション状態を取得
            //攻撃･ダメージ動作中は､移動しない
            if (Attack != true || Damege != true)
            {
                myTransform.Translate(-MoveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);    //Player左移動
                myTransform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);                    //Player左を向く
            }
            //Idle中とJump中に着地している時のみ､AnimationをRunにする
            if (Idle == true || (StandingJump == true && Ground == true))
            {
                myAnimator.SetBool("RunBool", true);                                            //Run
            }
        }
        //→が押されたら､Player移動
        else if (RightButton == true)
        {
            GetAnimation();                                                                     //現在のアニメーション状態を取得
            //攻撃･ダメージ動作中は､移動しない
            if (Attack != true || Damege != true)
            {
                myTransform.Translate(MoveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);     //Player右移動
                myTransform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);                     //Player右を向く
            }
            //Idle中とJump中に着地している時のみ､AnimationをRunにする
            if (Idle == true || (StandingJump == true && Ground == true))
            {
                myAnimator.SetBool("RunBool", true);                                            //Run
            }
        }
        //UpButtonがtrueになり､着地しているとき
        if (UpButton == true && Ground == true)
        {
            GetAnimation();                                                     //現在のアニメーション状態を取得
            //Idle中に移動入力が入っていない時のみ動作
            if (Idle == true && (LeftButton != true || RightButton != true))
            {
                myAnimator.Play("StandingJump", 0, 0.3f);                       //StandingJump
                myRigidbody.velocity = new Vector2(0, StandingJumpVelocity);    //ジャンプ
            }
            //Run中かIdle中に移動入力が入っている時のみ動作
            else if (Run == true || ((Idle == true || RunningJump == true) && (LeftButton != true || RightButton != true)))
            {
                myAnimator.Play("RunningJump", 0, 0.0f);                        //RunningJump
                myRigidbody.velocity = new Vector2(0, RunningJumpVelocity);     //ジャンプ
            }
        }
        //空中でUpButtonが離された時
        if (UpButton == false && myRigidbody.velocity.y > 0)
        {
            myRigidbody.velocity *= Decelerate;                                 //ジャンプの減速
        }
        //地面から離れているとき
        if (Ground != true)
        {
            GetAnimation();                                                     //現在のアニメーション状態を取得
            //StandingJumpモーション時
            if (StandingJump == true)
            {
                Delta += Time.deltaTime;                                        //時間計算
                float StandingJumpSpeed = 1.0f;                                 //StandingJump速度の初期化
                StandingJumpSpeed -= Delta;                                     //StandingJumpモーション減速計算
                myAnimator.SetFloat("JumpSpeed", StandingJumpSpeed);            //減速
                //StandingJumpモーション開始0.7秒後
                if (Delta >= StandingJumpPauseTime)
                {
                    myAnimator.SetFloat("JumpSpeed", 0.0f);                     //StandingJumpモーションを一時停止
                }
            }
            //RunningJumpモーション時
            else if(RunningJump == true)
            {
                Delta += Time.deltaTime;                                        //時間計算
                float RunningJumpSpeed = 1.5f;                                  //RunningJump速度の初期化
                RunningJumpSpeed -= Delta;                                      //RunningJumpモーション減速計算
                myAnimator.SetFloat("JumpSpeed", RunningJumpSpeed);             //減速
                //RunningJumpモーション開始2.0秒後
                if (Delta >= RunningJumpPauseTime)
                {
                    myAnimator.SetFloat("JumpSpeed", 0.0f);                     //RunningJumpモーションを一時停止
                }
            }
        }
        //矢印を離した時､Animationを止める
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            RunEnd();
        }
        //各方向キーをButtonと同期させる(エディタ用)
        //←
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            LeftButtonUp();
        }
        //→
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            RightButtonUp();
        }
        //↑
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            JumpButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            JumpButtonUp();
        }
    }
    void GetAnimation()
    {
        // アニメーション状態取得
        Idle = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Idle"));
        Run = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Run"));
        StandingJump = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("StandingJump"));
        RunningJump = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("RunningJump"));
        Attack = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Attack"));
        Damege = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Damege"));
    }
    public void LeftButtonDown()
    {
        LeftButton = true;      //LeftButtonが押されているとtrue
    }
    public void LeftButtonUp()
    {
        LeftButton = false;     //LeftButtonが離されるとfalse
        RunEnd();
    }
    public void RightButtonDown()
    {
        RightButton = true;     //RightButtonが押されているとtrue
    }
    public void RightButtonUp()
    {
        RightButton = false;    //RightButtonが離されるとfalse
        RunEnd();
    }
    void RunEnd()
    {
        myAnimator.SetBool("RunBool", false);    //Run終了
    }
    public void JumpButtonDown()
    {
        UpButton = true;    //UpButtonが押されるとtrue
    }
    public void JumpButtonUp()
    {
        UpButton = false;    //UpButtonが離されるとfalse
    }
    public void AttackButtonDwon()
    {
        GetAnimation();                            //現在のアニメーション状態を取得
        //Damege･Attack動作中は､動作しない
        if (Damege != true && Attack != true)
        {
            myAnimator.Play("Attack", 0, 0.2f);    //Attack
        }
    }
    //地面についた瞬間
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Ground = true;                                     //Groundを着地した瞬間にtrue
            UpButton = false;                                  //UpButtonを着地した瞬間にfalse
            myAnimator.SetFloat("JumpSpeed", 1.0f);            //Jumpモーションを再開
            Delta = 0.0f;                                      //Deltaリセット
        }
    }
    //地面から離れた瞬間
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Ground = false;                                    //地面から離れた瞬間false
        }
    }
}
