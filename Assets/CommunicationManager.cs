using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;


[Serializable]
public class ResponseObjects
{
    public int master_data_version;
    public UserProfileModel user_profile;
    public UserLoginModel user_login;
    public MasterLoginItemModel[] master_login_item;
    public MasterQuestModel[] master_quest;
    public UserQuestModel[] user_quest;
    public MasterCharacterModel[] master_character;
    public UserCharacterModel[] user_character;
}

public class CommunicationManager : MonoBehaviour
{
    public const string URL = "http://192.168.3.16:8000/";
    private const string ERROR_MASTER_DATA_UPDATE = "1";
    private const string ERROR_DB_UPDATE = "2";
    private const string ERROR_INVALID_DATA = "3";
    private const string ERROR_INVALID_SCHEDULE = "4";


    public static IEnumerator ConnectServer(string endpoint, string paramater, Action action = null)
    {
        //マスターデータバージョンを取得
        MasterDataVersionModel masterDataVersion = MasterDataVersion.GetMasterDataVersion();

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(URL + endpoint + "?client_master_version=" + masterDataVersion.master_data_version + paramater);
        
        yield return unityWebRequest.SendWebRequest();

        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            //error
            Debug.LogError(unityWebRequest.error);
        }

        //レスポンス取得
        string text = unityWebRequest.downloadHandler.text;


        ResponseObjects responseObjects = new ResponseObjects();

        if (text.All(char.IsNumber))
        {
            //エラーの場合
            switch (text)
            {
                case ERROR_MASTER_DATA_UPDATE:

                    Debug.Log("マスターデータが古いため更新します。");
                    UnityWebRequest masterDataRequest = UnityWebRequest.Get(URL + "master_data");
                    yield return masterDataRequest.SendWebRequest();


                    //レスポンスを取得
                    string masterText = masterDataRequest.downloadHandler.text;

                    responseObjects = JsonUtility.FromJson<ResponseObjects>(masterText);

                    //MasterデータをSQLiteへ保存
                    MasterDataVersion.Set(responseObjects.master_data_version);

                    if (responseObjects.master_login_item != null)
                    {
                        MasterLoginItem.Set(responseObjects.master_login_item);
                    }

                    if (responseObjects.master_quest != null)
                    {
                        MasterQuest.Set(responseObjects.master_quest);
                    }

                    if (responseObjects.master_character != null)
                    {
                        MasterCharacter.Set(responseObjects.master_character);
                    }

                    Debug.Log("マスターデータの更新が完了しました。");
                    /*
                    //マスターデータのバージョンはローカルに保存
                    PlayerPrefs.SetInt("master_data_version", responseObjects.master_data_version);
                    */

                    break;
                case ERROR_DB_UPDATE:
                    Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
                    break;

                case ERROR_INVALID_DATA:
                    Debug.LogError("サーバーでエラーが発生しました。[不正なデータ]");
                    break;

                case ERROR_INVALID_SCHEDULE:
                    Debug.LogError("サーバーでエラーが発生しました。[期間外]");
                        break;

                default:
                    break;
            }
        }
        else
        {
            responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
            
            //SQLiteへ保存
                if (!string.IsNullOrEmpty(responseObjects.user_profile.user_id))
                {
                    UserProfile.Set(responseObjects.user_profile);
                }

                if (!string.IsNullOrEmpty(responseObjects.user_login.user_id))
                {
                    UserLogin.Set(responseObjects.user_login);
                }
                if (responseObjects.user_quest != null)
                {
                    UserQuest.Set(responseObjects.user_quest);
                }
                if (responseObjects.user_character != null)
                {
                    UserCharacter.Set(responseObjects.user_character);
                }

                if (action != null)
                {
                    action();
                }
        }
    }
}



