using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterQuestModel
{
    public int quest_id;
    public string quest_name;
    public string open_at;
    public string close_at;
    public int item_type;
    public int item_count;
}

public class MasterQuest
{
    public enum ItemType
    {
        Crystal = 1,
        CrystalFree = 2,
        FriendCoin = 3,
    }
    public static void CreateTable()
    {
        string query = "create table if not exists master_quest (quest_id int, quest_name text, open_at text, close_at text, item_type int, item_count int, primary key(quest_id));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(MasterQuestModel[] master_quest_model_list)
    {
        foreach (MasterQuestModel masterQuestModel in master_quest_model_list)
        {
            string query = string.Format("insert or replace into master_quest (quest_id, quest_name, open_at, close_at, item_type, item_count) values (\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\');", masterQuestModel.quest_id, masterQuestModel.quest_name, masterQuestModel.open_at, masterQuestModel.close_at, masterQuestModel.item_type, masterQuestModel.item_count);
            SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    public static MasterQuestModel GetMasterQuest(int quest_id)
    {
        MasterQuestModel masterQuestModel = new MasterQuestModel();
        string query = string.Format("select * from master_quest where quest_id = {0};", quest_id);
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        foreach (DataRow dr in dataTable.Rows)
        {
            masterQuestModel.quest_id = int.Parse(dr["quest_id"].ToString());
            masterQuestModel.quest_name = dr["quest_name"].ToString();
            masterQuestModel.open_at = dr["open_at"].ToString();
            masterQuestModel.close_at = dr["close_at"].ToString();
            masterQuestModel.item_type = int.Parse(dr["item_type"].ToString());
            masterQuestModel.item_count = int.Parse(dr["item_count"].ToString());
        }

        return masterQuestModel;
    }
}
