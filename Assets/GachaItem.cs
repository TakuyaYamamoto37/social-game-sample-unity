using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaItem : MonoBehaviour
{
    public MasterGachaModel masterGachaModel;

    [SerializeField]
    Image banner;

    [SerializeField]
    Image costType;

    [SerializeField]
    Text costAmountLabel;

    [SerializeField]
    Text drawCountLabel;

    [SerializeField]
    Text periodLabel;

    [SerializeField]
    Text descriptionLabel;

    // Start is called before the first frame update
    void Start()
    {
        //Resourcesから画像を読み込む場合
        Sprite sprite = Resources.Load<Sprite>(masterGachaModel.banner_id);

        if (sprite == null)
        {
            Debug.LogError("ガチャバナーの画像がありません。");
            return;
        }
        banner.sprite = sprite;

        Sprite currencySprite = null;
        if (masterGachaModel.cost_type == (int)MasterGacha.CostType.Crystal || masterGachaModel.cost_type == (int)MasterGacha.CostType.CrystalFree)
        {
            currencySprite = Resources.Load<Sprite>("Crystal");
        }
        else if (masterGachaModel.cost_type == (int)MasterGacha.CostType.FriendCoin)
        {
            currencySprite = Resources.Load<Sprite>("FriendCoin");
        }
        if (currencySprite == null)
        {
            Debug.LogError("通貨の画像がありません。");
            return;
        }
        costType.sprite = currencySprite;

        costAmountLabel.text = masterGachaModel.cost_amount.ToString();
        drawCountLabel.text = masterGachaModel.draw_count.ToString() + "回";
        periodLabel.text = string.Format("{0}から\n{1}まで", masterGachaModel.open_at, masterGachaModel.close_at);
        descriptionLabel.text = masterGachaModel.description.ToString();
    }

    //ガチャをひくボタンをタップで呼ばれる関数
    public void PressEvent()
    {
        Action action = () =>
        {
            //レスポンス後の処理
            GameObject gachaManagerObject = GameObject.Find("GachaManager");
            if (gachaManagerObject == null)
            {
                Debug.LogError("GachaManagerが存在しません");
                return;
            }
            GachaManager gachaManager = gachaManagerObject.GetComponent<GachaManager>();
            if (gachaManager == null)
            {
                Debug.LogError("gachaManagerがアタッチされていません。");
                return;
            }
            gachaManager.GachaList.SetActive(false);
            gachaManager.GachaResult.SetActive(true);
        };

        UserProfileModel userProfileModel = UserProfile.Get();
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Debug.LogError("TitleSceneを起動してユーザー登録を行ってください。");
            return;
        }
        StartCoroutine(CommunicationManager.ConnectServer("gacha", "&user_id=" + userProfileModel.user_id + "&gacha_id=" + masterGachaModel.gacha_id, action));
    }
}
