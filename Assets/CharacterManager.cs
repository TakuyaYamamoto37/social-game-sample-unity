using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    GameObject Button;

    [SerializeField]
    public GameObject CharacterList;

    [SerializeField]
    GameObject contents;

    [SerializeField]
    GameObject characterItemPrefab;

    [SerializeField]
    public GameObject Dialog;

    [SerializeField]
    public Text DialogSellPoint;

    private int contentsCount = 3;
    private float contentsWidth = 210.0f;

    void Awake()
    {
        Sqlite.CreateTable();
    }

    private void Start()
    {
        Button.SetActive(true);
        CharacterList.SetActive(false);
        characterItemPrefab.SetActive(false);
    }

    public void CharacterListButtonEvent()
    {
        UserProfileModel userProfileModel = UserProfile.Get();
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Debug.LogError("TitleSceneを起動してユーザー登録を行ってください。");
            return;
        }

        Action action = () =>
        {
            Dictionary<int, UserCharacterModel> userCharacterModelList = UserCharacter.GetUserCharacterList();
            if (userCharacterModelList.Count == 0)
            {
                Debug.LogError("キャラクターがいません。データベースのuser_characterテーブルにレコードを入れてください。");
                return;
            }
            int i = 0;
            foreach (UserCharacterModel userCharacterModel in userCharacterModelList.Values)
            {
                GameObject characterItemObject = Instantiate(characterItemPrefab) as GameObject;
                characterItemObject.transform.SetParent(contents.transform);
                characterItemObject.transform.localPosition = new Vector3(85.0f + i % contentsCount * contentsWidth, -100.0f - i / contentsCount * contentsWidth, 0.0f);
                CharacterItem characterItem = characterItemObject.GetComponent<CharacterItem>();
                if (characterItem == null)
                {
                    Debug.LogError("CharacterItemがアタッチされていません。");

                    break;
                }
                characterItem.userCharacterModel = userCharacterModel;
                characterItemObject.SetActive(true);
                i++;
            }
            CharacterList.SetActive(true);
        };

        StartCoroutine(CommunicationManager.ConnectServer("character", "&user_id=" + userProfileModel.user_id, action));
    }
}
