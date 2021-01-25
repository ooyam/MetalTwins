using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //PlayerSwordのscriptを取得
    private PlayerSwordController PlayerSwordScr;
    //生成状況監視用変数
    private bool Sword;
    private bool Gun;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    //PlayerGeneratorがPlayerSwordの生成を行ったとき
    public void PlayerSwordGenerate()
    {
        PlayerSwordScr = GameObject.Find("PlayerSword(Clone)").GetComponent<PlayerSwordController>();    //PlayerSwordのScript取得
        Sword = true;                                                                                    //Sword監視変数をtrue
        Gun = false;                                                                                     //Gun監視変数をfalse
    }
    //LeftButtonが押されたら
    public void LeftButtonDown()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.LeftButtonDown();    //PlayerSwordのLeftButtonDown関数を起動する
        }
        //Gun生成中
        else if (Gun == true)
        {

        }
    }
    //LeftButtonが離されたら
    public void LeftButtonUp()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.LeftButtonUp();      //PlayerSwordのLeftButtonUp関数を起動する
        }
    }
    //RightButtonが押されたら
    public void RightButtonDown()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.RightButtonDown();   //PlayerSwordのRightButtonDown関数を起動する
        }
    }
    //RightButtonが離されたら
    public void RightButtonUp()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.RightButtonUp();     //PlayerSwordのRightButtonUp関数を起動する
        }
    }
    //JumpButtonが押されたら
    public void JumpButtonDown()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.JumpButtonDown();    //PlayerSwordのJumpButtonDown関数を起動する
        }
    }
    //JumpButtonが離されたら
    public void JumpButtonUp()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.JumpButtonUp();      //PlayerSwordのJumpButtonUp関数を起動する
        }
    }
    //AttackButtonが押されたら
    public void AttackButtonDwon()
    {
        //Sword生成中
        if (Sword == true)
        {
            PlayerSwordScr.AttackButtonDwon();      //PlayerSwordのJumpButtonUp関数を起動する
        }
    }
}
