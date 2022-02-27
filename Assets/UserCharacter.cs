using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserCharacterModel
{
    public int id;
    public int character_id;
}

public class UserCharacter
{
    public static void CreateTable()
    {
        string query = "create table if not exists user_character (id int, character_id int, primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(UserCharacterModel[] user_character_model_list)
    {
        //キャラクターが売却されてもデータが残り続けないように一度ドロップする
        string dropQuery = "drop table if exists user_character;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(dropQuery);

        CreateTable();

        foreach (UserCharacterModel userCharacterModel in user_character_model_list)
        {
            string query = string.Format("insert or replace into user_character(id, character_id) values({0}, {1});", userCharacterModel.id, userCharacterModel.character_id);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    public static Dictionary<int, UserCharacterModel> GetUserCharacterList()
    {
        Dictionary<int, UserCharacterModel> userCharacterListModel = new Dictionary<int, UserCharacterModel>();
        string query = "select * from user_character;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        foreach (DataRow dr in dataTable.Rows)
        {
            UserCharacterModel userCharacterModel = new UserCharacterModel();
            userCharacterModel.id = int.Parse(dr["id"].ToString());
            userCharacterModel.character_id = int.Parse(dr["character_id"].ToString());
            userCharacterListModel.Add(userCharacterModel.id, userCharacterModel);
        }

        return userCharacterListModel;
    }

    public static UserCharacterModel GetLatestUserCharacter()
    {
        UserCharacterModel userCharacterModel = new UserCharacterModel();

        string query = "select * from user_character order by id desc;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        foreach (DataRow dr in dataTable.Rows)
        {
            userCharacterModel.id = int.Parse(dr["id"].ToString());
            userCharacterModel.character_id = int.Parse(dr["character_id"].ToString());
            return userCharacterModel;
        }

        return null;
    }
}
