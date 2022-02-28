using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterShopModel
{
    public string shop_id;
    public int cost;
    public int amount;
}

public class MasterShop
{
    public static void CreateTable()
    {
        string query = "create table if not exists master_shop (shop_id text, amount int, cost int, primary key(shop_id));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(MasterShopModel[] master_shop_model_list)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        foreach (MasterShopModel masterShopModel in master_shop_model_list)
        {
            string query = string.Format("insert or replace into master_shop (shop_id, amount, cost) values (\'{0}\', \'{1}\', \'{2}\');", masterShopModel.shop_id, masterShopModel.amount, masterShopModel.cost);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    public static Dictionary<string, MasterShopModel> GetMasterShopList()
    {
        Dictionary<string, MasterShopModel> masterShopListModel = new Dictionary<string, MasterShopModel>();
        string query = "select * from master_shop;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        foreach (DataRow dr in dataTable.Rows)
        {
            MasterShopModel masterShopModel = new MasterShopModel();
            masterShopModel.shop_id = dr["shop_id"].ToString();
            masterShopModel.cost = int.Parse(dr["cost"].ToString());
            masterShopModel.amount = int.Parse(dr["amount"].ToString());
            masterShopListModel.Add(masterShopModel.shop_id, masterShopModel);
        }

        return masterShopListModel;
    }
}
