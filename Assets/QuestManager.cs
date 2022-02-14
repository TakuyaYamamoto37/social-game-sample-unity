using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    Text questNameText;

    private UserProfileModel userProfileModel;
    private int quest_id = 1;

    void Awake()
    {
        Sqlite.CreateTable();
    }

    // Start is called before the first frame update
    void Start()
    {
        userProfileModel = UserProfile.Get();
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Debug.LogError("TitleSceneを起動してユーザー登録を行ってください。");
        }

        MasterQuestModel masterQuestModel = MasterQuest.GetMasterQuest(quest_id); //quest_idが1を取得
        if (masterQuestModel.quest_id == 0)
        {
            Debug.LogError("MasterQuestのマスターデータを設定してください。");
            return;
        }
        questNameText.text = masterQuestModel.quest_name;
    }

    public void StartButtonEvent()
    {
        //クエスト開始時の処理
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Debug.LogError("TitleSceneを起動してユーザー登録を行ってください。");
            return;

        }
        Action action = () =>
        {
            //クエスト開始時の処理
        };
        StartCoroutine(CommunicationManager.ConnectServer("quest_start", "&user_id=" + userProfileModel.user_id + "&quest_id=" + quest_id, action));
        //StartCoroutine(CommunicationManager.ConnectServer("quest_end", "&user_id=" + userProfileModel.user_id + "&quest_id=" + quest_id + "&score=300&clear_time=300", action));
    }
}
