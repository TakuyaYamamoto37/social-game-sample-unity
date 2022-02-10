using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public class UserProfileModel
{
    public string user_id;
    public string user_name;
    public int crystal;
    public int crystal_free;
    public int friend_coin;
    public int tutorial_progress;
}

 public static class UserProfile
 {
    public const int TUTORIAL_START = 0;
    public const int TUTORIAL_QUEST = 10;
    public const int TUTORIAL_GACHA = 20;
    public const int TUTORIAL_FINISH = 999;

    public static void CreateTable(string dbPath)
     {
         string query = "create table if not exists user_profile (user_id text, user_name text, crystal int, crystal_free int, friend_coin int, tutorial_progress int, primary key (user_id));";
         SqliteDatabase sqlDB = new SqliteDatabase(dbPath);
         sqlDB.ExecuteQuery(query);
     }

    public static void Set(UserProfileModel userProfileModel)
    {
        string query = string.Format("insert or replace into user_profile (user_id, user_name, crystal, crystal_free, friend_coin, tutorial_progress) values (\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\');", userProfileModel.user_id, userProfileModel.user_name, userProfileModel.crystal, userProfileModel.crystal_free, userProfileModel.friend_coin, userProfileModel.tutorial_progress);
        SqliteDatabase sqlDB = new SqliteDatabase(Application.dataPath + "/Service.db");
        sqlDB.ExecuteQuery(query);
    }

    public static UserProfileModel Get()
    {
        string query = "select * from user_profile;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        UserProfileModel userProfileModel = new UserProfileModel();

        foreach (DataRow dr in dataTable.Rows)
        {
            userProfileModel.user_id = dr["user_id"].ToString();
            userProfileModel.user_name = dr["user_name"].ToString();
            userProfileModel.crystal = int.Parse(dr["crystal"].ToString());
            userProfileModel.crystal_free = int.Parse(dr["crystal_free"].ToString());
            userProfileModel.friend_coin = int.Parse(dr["friend_coin"].ToString());
            userProfileModel.tutorial_progress = int.Parse(dr["tutorial_progress"].ToString());
        }
        return userProfileModel;
    }
}

