using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentItem : MonoBehaviour
{
    public UserPresentModel userPresentModel;
    [SerializeField]
    Image typeSprite;

    [SerializeField]
    Text amountLabel;

    [SerializeField]
    Text descriptionLabel;

    [SerializeField]
    Text limitedLabel;

    private void Start()
    {
        //商品imageの設定
        if (userPresentModel.item_type == (int)UserPresent.ItemType.Crystal || userPresentModel.item_type == (int)UserPresent.ItemType.CrystalFree)
        {
            typeSprite.sprite = Resources.Load<Sprite>("Crystal");
        }
        else if (userPresentModel.item_type == (int)UserPresent.ItemType.FriendCoin)
        {
            typeSprite.sprite = Resources.Load<Sprite>("FriendCoin");
        }

        amountLabel.text = "x" + userPresentModel.item_count.ToString();
        descriptionLabel.text = userPresentModel.description.ToString();
        limitedLabel.text = "期限：" + userPresentModel.limited_at.ToString() + "まで";
    }

    public void PresentButtonEvent()
    {
        Action action = () => 
        {
            //プレゼント獲得後のアクションを記述
            GameObject presentManagerObject = GameObject.Find("PresentManager");
            if (presentManagerObject == null)
            {
                Debug.LogError("PresentManagerが見つかりませんでした");
                return;
            }

            PresentManager presentManager = presentManagerObject.GetComponent<PresentManager>();
            if (presentManager == null)
            {
                Debug.LogError("PresentManagerがアタッチされていません。");
                return;
            }

            presentManager.dialog.SetActive(true);

        };
        UserProfileModel userProfileModel = UserProfile.Get();
        StartCoroutine(CommunicationManager.ConnectServer("present", "&user_id=" + userProfileModel.user_id + "&present_id=" + userPresentModel.present_id, action));
    }
}
