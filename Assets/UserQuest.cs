using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserQuestModel
{
    public int quest_id;
    public int status;
    public int score;
    public int clear_time;
}

public class UserQuest
{
    public static void CreateTable()
    {
        string query = "create table if not exists user_quest (quest_id int, status int, score int, clear_time int, primary key (quest_id));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(UserQuestModel[] user_quest_model_list)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        foreach (UserQuestModel userQuestModel in user_quest_model_list)
        {
            string query = string.Format("insert or replace into user_quest (quest_id, status, score, clear_time) values (\'{0}\', \'{1}\', \'{2}\', \'{3}\');", userQuestModel.quest_id, userQuestModel.status, userQuestModel.score, userQuestModel.clear_time);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    public static UserQuestModel Get(int quest_id)
    {
        UserQuestModel userQuestModel = new UserQuestModel();
        string query = "select * from user_quest where quest_id = " + quest_id;
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        foreach (DataRow dr in dataTable.Rows)
        {
            userQuestModel.quest_id = int.Parse(dr["quest_id"].ToString());
            userQuestModel.status = int.Parse(dr["status"].ToString());
            userQuestModel.score = int.Parse(dr["score"].ToString());
            userQuestModel.clear_time = int.Parse(dr["clear_time"].ToString());
        }
        
        return userQuestModel;
    }
}
