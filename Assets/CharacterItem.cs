using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public UserCharacterModel userCharacterModel;
    private MasterCharacterModel masterCharacterModel;

    [SerializeField]
    Image characterAsset;

    [SerializeField]
    Image typeAsset;

    // Start is called before the first frame update
    void Start()
    {
        masterCharacterModel = MasterCharacter.GetMasterCharacter(userCharacterModel.character_id);
        if (masterCharacterModel.character_id == 0)
        {
            Debug.LogError("キャラクターのマスターデータを設定してください。");
            return;
        }

        //Resourcesから画像を読み込む場合
        Sprite characterSprite = Resources.Load<Sprite>(masterCharacterModel.asset_id);
        if (characterSprite == null)
        {
            Debug.LogError("キャラクターの画像を設定してください。");
            return;
        }
        characterAsset.sprite = characterSprite;
        Sprite typeSprite = Resources.Load<Sprite>("CharacterTypeFrame_" + masterCharacterModel.type);
        if (typeSprite == null)
        {
            Debug.LogError("キャラクターのタイプ画像を設定してください。");
        }
        typeAsset.sprite = typeSprite;
    }

    public void SellButtonEvent()
    {
        Action action = () =>
        {
            GameObject characterManagerObject = GameObject.Find("CharacterManager");
            if (characterManagerObject == null)
            {
                Debug.LogError("CharacterManagerが存在しません。");
                return;
            }
            CharacterManager characterManager = characterManagerObject.GetComponent<CharacterManager>();
            if (characterManager == null)
            {
                Debug.LogError("CharacterManagerがアタッチされていません。");
                return;
            }
            characterManager.CharacterList.SetActive(false);
            characterManager.Dialog.SetActive(true);
            characterManager.DialogSellPoint.text = "x " + masterCharacterModel.sell_point;
        };
        UserProfileModel userProfileModel = UserProfile.Get();
        StartCoroutine(CommunicationManager.ConnectServer("character_sell", "&user_id=" + userProfileModel.user_id + "&id=" + userCharacterModel.id, action));
    }
}
