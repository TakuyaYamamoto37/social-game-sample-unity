using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    private void Awake()
    {
        Sqlite.CreateTable();
    }

    public void TutoralButtonEvent()
    {
        //ボタンが押されたときの処理
        UserProfileModel userProfileModel = UserProfile.Get();
        StartCoroutine(CommunicationManager.ConnectServer("quest_tutorial", "&user_id=" + userProfileModel.user_id));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
