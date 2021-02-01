﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    //PlayerGeneratorのScriptの取得
    private PlayerGenerator PlayerGeneratorScr;
    //Animatorの取得
    private Animator myAnimator;
    //Transformの取得
    private Transform myTransform;
    //Rigidbodyの取得
    private Rigidbody myRigidbody;
    //BulletEffectの取得
    public GameObject Bullet;
    //BulletEffect生成位置の指定
    private float BulletPosX = 0.8f;
    private float BulletPosY = 1.2f;
    //Animation状態の取得
    private bool Idle;
    private bool Run;
    private bool StandingJump;
    private bool RunningJump;
    private bool Attack;
    private bool Damege;
    private bool Change;
    //方向転換時のRotation.Yの値
    private float RightRotationY = 90.0f;
    private float LeftRotationY = -90.0f;
    //移動速度の設定
    private float GuroundSpeed = 4.0f;
    private float SkySpeed = 2.0f;
    private float MoveSpeed;
    //X軸移動限度
    private float MinX = -7.8f;
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
    //Attackの条件･攻撃スパン用変数
    private bool AttackStart;
    private float AttackTime = 0.3f;
    //Attackモーション保持条件･モーション時間用変数
    private bool AttackMove;
    private float AttackMoveTime = 0.45f;
    //Changeモーション保持条件/モーション時間用変数
    private bool ChangeMove;
    private float ChangeMoveTime = 1.0f;
    //ChangeEffect取得/生成時のposition修正値/生成までの時間/生成状況確認用
    public GameObject ChangeEffectGun;
    private float ChangeEffectPosY = 1.0f;
    private float ChangeEffectPosZ = -2.0f;
    private float ChangeEffectTime = 0.5f;
    private bool ChangeEffectGenerate;
    //時間計算用変数
    private float Delta;
    private float AttackDelta;
    private float ChangeDelta;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();      //Animatorの取得
        myTransform = GetComponent<Transform>();    //Transformの取得
        myRigidbody = GetComponent<Rigidbody>();    //Rigidbodyの取得
        PlayerGeneratorScr = GameObject.FindWithTag("PlayerGenerator").GetComponent<PlayerGenerator>();    //PlayerGeneratorのScriptの取得
    }

    // Update is called once per frame
    void Update()
    {
        //←矢印が押されたら､Player移動
        if (LeftButton == true && ChangeMove != true && myTransform.position.x >= MinX)
        {
            GetAnimation();                                                                     //現在のアニメーション状態を取得
            //地上で攻撃中･ダメージ動作中は､移動しない
            if (Damege != true)
            {
                myTransform.Translate(-MoveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);    //Player左移動
                myTransform.rotation = Quaternion.Euler(0.0f, LeftRotationY, 0.0f);             //Player左を向く
            }
            //Idle中とJump中に着地している時のみ､AnimationをRunにする
            if ((Idle == true || StandingJump == true || Attack == true) && Ground == true)
            {
                myAnimator.SetBool("RunBool", true);                                            //Run
            }
        }
        //→が押されたら､Player移動
        else if (RightButton == true && ChangeMove != true)
        {
            GetAnimation();                                                                     //現在のアニメーション状態を取得
            //地上で攻撃中･ダメージ動作中は､移動しない
            if (Damege != true)
            {
                myTransform.Translate(MoveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);     //Player右移動
                myTransform.rotation = Quaternion.Euler(0.0f, RightRotationY, 0.0f);            //Player右を向く
            }
            //Idle中とJump中に着地している時のみ､AnimationをRunにする
            if ((Idle == true || StandingJump == true || Attack == true) && Ground == true)
            {
                myAnimator.SetBool("RunBool", true);                                            //Run
            }
        }
        //UpButtonがtrueになり､着地しているとき
        if (UpButton == true && Ground == true && Change != true)
        {
            GetAnimation();                                                     //現在のアニメーション状態を取得
            //Idle中･Jump終了後に移動入力が入っていない時のみ動作
            if ((Idle == true || StandingJump == true || RunningJump == true) && (LeftButton != true && RightButton != true))
            {
                myAnimator.Play("StandingJump", 0, 0.3f);                       //StandingJump
                myRigidbody.velocity = new Vector2(0, StandingJumpVelocity);    //ジャンプ
            }
            //Run中･Idle中/Jump終了後に移動入力が入っている時のみ動作
            else if (Run == true || ((Idle == true || RunningJump == true || StandingJump == true) && (LeftButton == true || RightButton == true)))
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
            else if (RunningJump == true)
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
        //AttackStartがtrueになったら
        if(AttackStart == true || AttackMove == true)
        {
            AttackDelta += Time.deltaTime;                        //時間計算開始
            //Attack開始後0.3秒
            if (AttackDelta >= AttackTime)
            {
                AttackStart = false;                              //Attack条件解除
                //Attack開始後0.45秒
                if(AttackDelta >= AttackMoveTime)
                {
                    AttackMove = false;                           //Attackモーション自己保持解除
                    //移動条件がfalseの場合
                    if(RightButton != true && LeftButton != true)
                    {
                        myAnimator.Play("Idle", 0, 0.0f);         //Attackモーション終了
                    }
                }
            }
        }
        //Change命令された時
        if (ChangeMove == true)
        {
            ChangeDelta += Time.deltaTime;
            //Chengeモーション中にダメージを受けた時
            if (Damege == true)
            {
                ChangeMove = false;                                           //Change条件リセット
                ChangeDelta = 0.0f;                                           //時間計算リセット
            }
            //Changeモーション0.7秒経過時､Effect生成なしの場合(重複生成防止)
            else if (ChangeDelta >= ChangeEffectTime && ChangeEffectGenerate != true)
            {
                GameObject ChangeEffectObj = Instantiate(ChangeEffectGun);    //Effect生成
                ChangeEffectObj.transform.position = new Vector3(myTransform.position.x, myTransform.position.y + ChangeEffectPosY, ChangeEffectPosZ);    //EffectPosition
                ChangeEffectGenerate = true;                                  //Effect生成状況更新
            }
            //Changeモーションが完遂(1.2秒経過)した時
            if (ChangeDelta >= ChangeMoveTime)
            {
                Destroy(gameObject);                                          //自身を破壊
                PlayerGeneratorScr.PlayerSwordGenerate();                     //PlayerGeneratorにPlayerGunGenerateを命令
            }
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
    public void GetAnimation()
    {
        // アニメーション状態取得
        Idle = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Idle"));
        Run = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Run"));
        StandingJump = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("StandingJump"));
        RunningJump = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("RunningJump"));
        Attack = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Attack"));
        Damege = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Damege"));
        Change = myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Change"));
    }
    public void LeftButtonDown()
    {
        LeftButton = true;                                 //LeftButtonが押されているとtrue
        MoveSpeed = (Ground) ? GuroundSpeed : SkySpeed;    //空中では移動速度を落とす
    }
    public void LeftButtonUp()
    {
        LeftButton = false;     //LeftButtonが離されるとfalse
        RunEnd();               //Run終了
    }
    public void RightButtonDown()
    {
        RightButton = true;                                //RightButtonが押されているとtrue
        MoveSpeed = (Ground) ? GuroundSpeed : SkySpeed;    //空中では移動速度を落とす
    }
    public void RightButtonUp()
    {
        RightButton = false;    //RightButtonが離されるとfalse
        RunEnd();               //Run終了
    }
    public void JumpButtonDown()
    {
        UpButton = true;       //UpButtonが押されるとtrue
    }
    public void JumpButtonUp()
    {
        UpButton = false;      //UpButtonが離されるとfalse
    }
    void RunEnd()
    {
        myAnimator.SetBool("RunBool", false);    //Run終了
    }
    public void AttackButtonDwon()
    {
        GetAnimation();                                                                                               //現在のアニメーション状態を取得
        //Damege･Change中･Attack後0.3秒は､動作しない
        if (Damege != true && AttackStart != true && Change != true)
        {
            Vector3 NowPos = myTransform.position;                                                                    //現在位置を取得
            GameObject BulletEffect = Instantiate(Bullet);                                                            //Bullet生成
            //発砲時のRotation.yの値が0より大きい場合右に発砲
            if (myTransform.rotation.y >= 0)
            {
                BulletEffect.transform.rotation = Quaternion.Euler(0.0f, RightRotationY, 0.0f);                       //右に発砲
                BulletEffect.transform.position = new Vector3(NowPos.x + BulletPosX, NowPos.y + BulletPosY, 0.0f);    //BulletEffectのpositionを指定
            }
            else
            {
                BulletEffect.transform.rotation = Quaternion.Euler(0.0f, LeftRotationY, 0.0f);                        //左に発砲
                BulletEffect.transform.position = new Vector3(NowPos.x - BulletPosX, NowPos.y + BulletPosY, 0.0f);    //BulletEffectのpositionを指定
            }
            myAnimator.Play("Attack", 0, 0.8f);                                                                       //Attackモーション開始
            AttackMove = true;                                                                                        //Attackモーション自己保持
            AttackStart = true;                                                                                       //Attack条件true
            AttackDelta = 0.0f;                                                                                       //Attack時間計算リセット
        }
    }
    //ChengeButtonが押さたとき
    public void ChangeButtonDown()
    {
        GetAnimation();                            //現在のアニメーション状態を取得
        //Idle時､着地しているとき
        if (Ground == true && Idle == true)
        {
            ChangeMove = true;                     //Change開始条件true
            myAnimator.Play("Change", 0, 0.0f);    //Changeモーション開始
        }
    }
    //当たり判定(接触時）
    void OnCollisionEnter(Collision other)
    {
        //地面についた瞬間
        if (other.gameObject.tag == "Ground")
        {
            Ground = true;                                     //Groundを着地した瞬間にtrue
            MoveSpeed =GuroundSpeed;                           //移動速度のリセット
            UpButton = false;                                  //UpButtonを着地した瞬間にfalse
            myAnimator.SetFloat("JumpSpeed", 1.0f);            //Jumpモーションを再開
            Delta = 0.0f;                                      //Deltaリセット
            //着地時のRotation.yの値が0より大きい場合右を向く
            if (myTransform.rotation.y >= 0)                   //StandingJump時に回転する分を修正
            {
                myTransform.rotation = Quaternion.Euler(0.0f, RightRotationY, 0.0f);    //Player右を向く
            }
            else
            {
                myTransform.rotation = Quaternion.Euler(0.0f, LeftRotationY, 0.0f);     //Player左を向く
            }
        }
        //敵に接触
        if(other.gameObject.tag == "Enemy" && ChangeEffectGenerate != true)
        {
            myAnimator.Play("Damege", 0, 0.0f);    //Damegeモーション開始
        }
    }
    //当たり判定(離れた時）
    void OnCollisionExit(Collision other)
    {
        //地面から離れた瞬間
        if (other.gameObject.tag == "Ground")
        {
            Ground = false;    //地面から離れた瞬間false
            RunEnd();          //Run終了
        }
    }
    //Change前遅延コルーチン
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);    //遅延1秒
        Destroy(gameObject);                              //自身を破壊
        PlayerGeneratorScr.PlayerSwordGenerate();         //PlayerGeneratorにPlayerSwordGenerateを命令
    }
}