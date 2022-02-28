using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public MasterShopModel masterShopModel;

    [SerializeField]
    Text costLabel;

    [SerializeField]
    Text amountLabel;

    // Start is called before the first frame update
    void Start()
    {
        costLabel.text = masterShopModel.cost + " 円";
        amountLabel.text = "x " + masterShopModel.amount;
    }

    //商品タップで呼ばれる関数
    public void PressEvent()
    {
        //決済処理

        //決済処理後の通信
        Action action = () =>
        {
            GameObject shopManagerObject = GameObject.Find("ShopManager");
            if (shopManagerObject == null)
            {
                Debug.LogError("ShopManagerが存在しません");
                return;
            }

            ShopManager shopManager = shopManagerObject.GetComponent<ShopManager>();
            if (shopManager == null)
            {
                Debug.LogError("ShopManagerがアタッチされていません。");
                return;
            }

            shopManager.Dialog.SetActive(true);
            shopManager.DialogCrystalAmount.text = "x " + masterShopModel.amount;
        };

        UserProfileModel userProfileModel = UserProfile.Get();
        StartCoroutine(CommunicationManager.ConnectServer("shop", "&user_id=" + userProfileModel.user_id + "&shop_id=" + masterShopModel.shop_id, action));
    }
}
