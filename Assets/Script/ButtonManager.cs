using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //自身のTransform取得
    private Transform myTransform;
    //各ButtonのImageを取得
    private Image LeftButton;
    private Image RightButton;
    private Image JumpButton;
    private Image AttackButton;
    private Image ChangeButton;
    //Button用Color Sword/Gun
    private Color SwordColor = new Color32(255, 0, 0, 230);
    private Color GunColor = new Color32(0, 80, 255, 230);
    //PlayerSword･Gunのscriptを取得
    private PlayerSwordController PlayerSwordScr;
    private PlayerGunController PlayerGunScr;
    //PlayerGeneratorのScriptを取得
    private PlayerGenerator PlayerGeneratorScr;
    //生成状況監視用変数
    private bool Sword;
    private bool Gun;

    // Start is called before the first frame update
    void Start()
    {
        PlayerGeneratorScr = GameObject.FindWithTag("PlayerGenerator").GetComponent<PlayerGenerator>();    //PlayerGeneratorのScriptを取得
        myTransform = transform;                                                                           //Transformの取得
        LeftButton = myTransform.Find("LeftButton").gameObject.GetComponent<Image>();                      //LeftButtonのImage取得
        RightButton = myTransform.Find("RightButton").gameObject.GetComponent<Image>();                    //RightButtonのImage取得
        JumpButton = myTransform.Find("JumpButton").gameObject.GetComponent<Image>();                      //JumpButtonのImage取得
        AttackButton = myTransform.Find("AttackButton").gameObject.GetComponent<Image>();                  //AttackButtonのImage取得
        ChangeButton = myTransform.Find("ChangeButton").gameObject.GetComponent<Image>();                  //LChamgeButtonのImage取得
    }

    // Update is called once per frame
    void Update()
    {

    }
    //PlayerGeneratorがPlayerSwordの生成を行ったとき
    public void PlayerSwordGenerate()
    {
        PlayerSwordScr = GameObject.FindWithTag("PlayerSword").GetComponent<PlayerSwordController>();    //PlayerSwordのScript取得
        Sword = PlayerGeneratorScr.Sword;                                                                //PlayerGeneratorと生成状況の共有
        Gun = PlayerGeneratorScr.Gun;                                                                    //PlayerGeneratorと生成状況の共有
        LeftButton.color = SwordColor;                                                                   //各Button色変更Sword
        RightButton.color = SwordColor;
        JumpButton.color = SwordColor;
        AttackButton.color = SwordColor;
        ChangeButton.color = SwordColor;
    }
    //PlayerGeneratorがPlayerGunの生成を行ったとき
    public void PlayerGunGenerate()
    {
        PlayerGunScr = GameObject.FindWithTag("PlayerGun").GetComponent<PlayerGunController>();          //PlayerGunのScript取得
        Sword = PlayerGeneratorScr.Sword;                                                                //PlayerGeneratorと生成状況の共有
        Gun = PlayerGeneratorScr.Gun;                                                                    //PlayerGeneratorと生成状況の共有
        LeftButton.color = GunColor;                                                                     //各Button色変更Gun
        RightButton.color = GunColor;
        JumpButton.color = GunColor;
        AttackButton.color = GunColor;
        ChangeButton.color = GunColor;
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
            PlayerGunScr.LeftButtonDown();      //PlayerGunのLeftButtonDown関数を起動する
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
        //Gun生成中
        else if (Gun == true)
        {
            PlayerGunScr.LeftButtonUp();        //PlayerGunのLeftButtonUp関数を起動する
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
        //Gun生成中
        else if (Gun == true)
        {
            PlayerGunScr.RightButtonDown();     //PlayerGunのRightButtonDown関数を起動する
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
        //Gun生成中
        else if (Gun == true)
        {
            PlayerGunScr.RightButtonUp();       //PlayerGunのRightButtonUp関数を起動する
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
        //Gun生成中
        else if (Gun == true)
        {
            PlayerGunScr.JumpButtonDown();      //PlayerGunのJumpButtonDown関数を起動する
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
        //Gun生成中
        else if (Gun == true)
        {
            PlayerGunScr.JumpButtonUp();        //PlayerGunのJumpButtonUp関数を起動する
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
        //Gun生成中
        else if (Gun == true)
        {
            PlayerGunScr.AttackButtonDwon();        //PlayerGunのJumpButtonUp関数を起動する
        }
    }
}
