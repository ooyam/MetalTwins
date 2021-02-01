using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    //各GameObjectPrefabを取得
    public GameObject PlayerSword;
    public GameObject PlayerGun;
    //各GameObjectを取得
    private GameObject PlayerSwordObj;
    private GameObject PlayerGunObj;
    //PlayerSword･Gunのscriptを取得
    private PlayerSwordController PlayerSwordScr;
    private PlayerGunController PlayerGunScr;
    //PlayerSword･GunのTransformを取得
    private Transform PlayerTra;
    //ButtonManagerのScriptの取得
    private ButtonManager ButtonManagerScr;
    //MainCameraのScriptの取得
    private CameraController CameraScr;
    //生成状況監視用変数
    public bool Sword;
    public bool Gun;

    //Playerの初期位置
    private float DefaultPosX = -7.0f;
    private float DefaultPosY = -3.3f;
    //Playerの現在値･Y軸の角度取得用変数
    private Vector3 NowPosition;
    private float NowRotationY;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSwordObj = Instantiate(PlayerSword);                                                   //PlayerSwordを生成する
        PlayerSwordObj.transform.position = new Vector3(DefaultPosX, DefaultPosY, 0);                //PlayerSwordの生成位置指定
        PlayerSwordScr = PlayerSwordObj.GetComponent<PlayerSwordController>();                       //PlayerSwordのScript取得
        PlayerTra = PlayerSwordObj.transform;                                                        //PlayerSwordのTransform取得
        Sword = true;                                                                                //Sword生成状況をtrue
        ButtonManagerScr = GameObject.FindWithTag("Canvas").GetComponent<ButtonManager>();           //ButtonManagerのScriptの取得
        CameraScr = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();           //MainCameraのScriptの取得
        ButtonManagerScr.PlayerSwordGenerate();                                                      //PlayerSwordの生成完了をButtonManagerに共有
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ChangeButtonが押された瞬間
    public void ChangeButtonDown()
    {
        if(Sword == true)
        {
            NowPosition = PlayerTra.position;        //Playerの現在値を取得
            NowRotationY = PlayerTra.rotation.y;     //PlayerのY軸角度を取得
            PlayerSwordScr.ChangeButtonDown();       //PlayerSwordにChange命令
        }
        else if(Gun == true)
        {
            NowPosition = PlayerTra.position;        //Playerの現在値を取得
            NowRotationY = PlayerTra.rotation.y;     //PlayerのY軸角度を取得
            PlayerGunScr.ChangeButtonDown();         //PlayerGunにChange命令
        }
    }
    //PlayerSwordがChange完遂時
    public void PlayerGunGenerate()
    {
        PlayerGunObj = Instantiate(PlayerGun);                                    //PlayerGunを生成する
        PlayerTra = PlayerGunObj.transform;                                       //PlayerGunのTransform取得
        PlayerTra.position = NowPosition;                                         //PlayerGunの生成位置指定
        PlayerTra.rotation = Quaternion.Euler(0.0f, NowRotationY, 0.0f);          //PlayerGunの生成角度指定
        PlayerGunScr = PlayerGunObj.GetComponent<PlayerGunController>();          //PlayerGunのScript取得
        Gun = true;                                                               //Gun生成状況をtrue
        Sword = false;                                                            //Sword生成状況をfalse
        ButtonManagerScr.PlayerGunGenerate();                                     //PlayerGunの生成完了をButtonManagerに共有
        CameraScr.PlayerGunGenerate();                                            //PlayerGunの生成完了をMainCameraに共有
        Time.timeScale = 1.0f;                                                    //時間再開
    }
    //PlayerGunがChange完遂時
    public void PlayerSwordGenerate()
    {
        PlayerSwordObj = Instantiate(PlayerSword);                                //PlayerSwordを生成する
        PlayerTra = PlayerSwordObj.transform;                                     //PlayerSwordのTransform取得
        PlayerTra.position = NowPosition;                                         //PlayerSwordの生成位置指定
        PlayerTra.rotation = Quaternion.Euler(0.0f, NowRotationY, 0.0f);          //PlayerSwordの生成角度指定
        PlayerSwordScr = PlayerSwordObj.GetComponent<PlayerSwordController>();    //PlayerSwordのScript取得
        Sword = true;                                                             //Sword生成状況をture
        Gun = false;                                                              //Gun生成状況をfalse
        ButtonManagerScr.PlayerSwordGenerate();                                   //PlayerSwordの生成完了をButtonManagerに共有
        CameraScr.PlayerSwordGenerate();                                          //PlayerSwordの生成完了をMainCameraに共有
        Time.timeScale = 1.0f;                                                    //時間再開
    }
}
