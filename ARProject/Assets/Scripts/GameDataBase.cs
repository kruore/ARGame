using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;

public class GameDataBase : Singleton<GameDataBase>
{
    public Item item;
    public List<GpsData> places = new List<GpsData>();
    public List<GpsData> inventory_places = new List<GpsData>();
    public List<Item> cards = new List<Item>();
    public List<Item> inventory_cards = new List<Item>();
    string conn = string.Empty;
    public void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            conn = "URI=file:" + Application.persistentDataPath + "/testDB.sqlite";
        }
        else
        {
            conn = "URI=file:" + Application.dataPath + "/StreamingAssets/testDB.sqlite";
        }
        Ds_CopyDB();
        DS_GpsplaceSetting();
        CardSetting();
        InventoryCardSetting();
    }
    //사용플렛폼이 안드로이드 일경우에 일반적인 경로로 DB접근이 되지않아 DB를 복사하여 사용가능한 위치에 놓아준다.
    void Ds_CopyDB()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            conn = Application.persistentDataPath + "/testDB.sqlite";
            if (!File.Exists(conn))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "/assets/testDB.sqlite");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone)
                {
                    File.WriteAllBytes(conn, loadDB.bytes);
                }
            }
        }
    }
    //GpsplaceSetting
    public void DS_GpsplaceSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Gpsplace";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int Num = inventory_places.Count;
            string Name = reader.GetString(1);
            double Latitude = reader.GetDouble(2);
            double Longitude = reader.GetDouble(3);
            Element element = (Element)reader.GetInt32(4);
            string quad = reader.GetString(5);
            inventory_places.Add(new GpsData(Num, Name, Latitude, Longitude, element, quad));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void CardSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Cards";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int num = reader.GetInt32(0);
            int Cost = reader.GetInt32(1);
            int Damage = reader.GetInt32(2);
            string Name = reader.GetString(3);
            Element element = (Element)reader.GetInt32(4);
            int Rank = reader.GetInt32(5);
            cards.Add(new Item(num, Cost, Damage, Name, element,Rank));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void InventoryCardSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM inventory";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            
            int num = reader.GetInt32(0);
            int Cost = reader.GetInt32(1);
            int Damage = reader.GetInt32(2);
            string Name = reader.GetString(3);
            Element element = (Element)reader.GetInt32(4);
            int Rank = reader.GetInt32(5);
            inventory_cards.Add(new Item(num,Cost,Damage,Name,element,Rank));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void InventoryInsert(int number)
    {

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM cards WHERE num=\"" + number + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        int num = inventory_cards.Count + 1;
        int Cost = reader.GetInt32(1);
        int Damage = reader.GetInt32(2);
        string Name = reader.GetString(3);
        Element element = (Element)reader.GetInt32(4);
        int Rank = reader.GetInt32(5);
        inventory_cards.Add(new Item(num, Cost, Damage, Name, element,Rank));
        reader.Close();
        reader = null;
        
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        dbcmd= dbconn.CreateCommand();
        string sqlQuery2 = "insert into Inventory select* from Cards where Num = " + number + "; ";
        sqlQuery2 += "update Inventory set Num = " + (inventory_cards.Count + 1) + " where Num = " + number + "; ";
        dbcmd.CommandText = sqlQuery2;
        IDataReader reader2 = dbcmd.ExecuteReader();
        reader2.Read();
        reader2.Close();
        reader2 = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

}
