using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterGachaModel
{
    public int gacha_id;
    public string banner_id;
    public int cost_type;
    public int cost_amount;
    public int draw_count;
    public string open_at;
    public string close_at;
    public string desctiption;
}
public static class MasterGacha
{
    public enum CostType
    {
        Crystal = 1,
        CrystalFree = 2,
        FriendCoin = 3,
    }

    public static void CreateTable()
    {
        string query = "create table if not exists master_gacha (gacha_id int, banner_id text, cost_type int, cost_amount int, draw_count int, open_at text, close_at text, description text, primary key(gacha_id));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(MasterGachaModel[] master_gacha_model_list)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        foreach (MasterGachaModel masterGachaModel in master_gacha_model_list)
        {
            string query = string.Format("insert or replace into master_gacha(gacha_id, banner_id, cost_type, cost_amount, draw_count, open_at,close_at, description) values(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', \'{6}\', \'{7}\');", masterGachaModel.gacha_id, masterGachaModel.banner_id, masterGachaModel.cost_type, masterGachaModel.cost_amount, masterGachaModel.draw_count, masterGachaModel.open_at, masterGachaModel.close_at, masterGachaModel.desctiption);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
