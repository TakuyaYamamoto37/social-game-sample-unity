using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    Text userID;

    private UserProfileModel userProfileModel;

    string dbPath;

    void Awake()
    {
        //string DBPath = Application.persistentDataPath + "/Service.db";
        //string DBPath = Application.dataPath + "/StreamingAssets/Service.db";
        this.dbPath = Application.dataPath + "/Service.db";

        if (!File.Exists(dbPath))
        {
            File.Create(dbPath);
        }

        UserProfile.CreateTable(dbPath);
        UserLogin.CreateTable(dbPath);
        MasterLoginItem.CreateTable(dbPath);
    }


    // Start is called before the first frame update
    void Start()
    {
        userProfileModel = UserProfile.Get(dbPath);
        if (!string.IsNullOrEmpty(userProfileModel.user_id))
        {
            userID.text = "ID : " + userProfileModel.user_id;
        }
    }

    public void LoginButtonEvent()
    {
        userProfileModel = UserProfile.Get(dbPath);
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Action action = () =>
            {
                Debug.Log("登録完了しました");
            };
            StartCoroutine(CommunicationManager.ConnectServer("registration", "", action));
        }
        else
        {
            Action action = () =>
            {
                //ログイン後の処理
            };
            StartCoroutine(CommunicationManager.ConnectServer("login", "&user_id=" + userProfileModel.user_id, action));
        }
    }
}
