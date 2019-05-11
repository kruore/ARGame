using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using UnityEngine.Android;
public class GameDataBase : Singleton<GameDataBase>
{
    public List<GpsData> places = new List<GpsData>();
    public List<GpsData> currentquadplaces = new List<GpsData>();
    public List<Item> cards = new List<Item>();
    public List<Item> cards_Inventory = new List<Item>();
    public List<Item> cards_Deck = new List<Item>();
    public static string conn = string.Empty;


    public void Awake()
    {
        Caching.ClearCache();
        Setplatform();
        Ds_CopyDB();
        DS_InventorySetting();
        CardSetting();
        DS_DeckSetting();
        StartCoroutine(Permissioncheck());

    }
    void Setplatform()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            conn = "URI=file:" + Application.persistentDataPath + "/testDB.sqlite";
        }
        else
        {
            conn = "URI=file:" + Application.dataPath + "/StreamingAssets/testDB.sqlite";
        }
    }
    private IEnumerator Permissioncheck()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            yield return null;
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return null;
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            yield return null;
        }
    }



    //사용플렛폼이 안드로이드 일경우에 일반적인 경로로 DB접근이 되지않아(permission) DB를 복사하여 사용가능한 위치에 놓아준다.
    void Ds_CopyDB()
    {
        //string filepath = string.Empty;
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    filepath = Application.persistentDataPath + "/testDB.sqlite";
        //    if (!File.Exists(filepath))
        //    {
        //        string path="jar:file://" + Application.dataPath + "/assets/testDB.sqlite";
        //        WWW loadDB = new WWW(path);
        //        loadDB.bytesDownloaded.ToString();
        //        while (!loadDB.isDone) { }
        //        File.WriteAllBytes(filepath, loadDB.bytes);
        //    }
        //}
        //else
        //{
        //    filepath = Application.dataPath + "/testDB.sqlite";
        //    if (!File.Exists(filepath))
        //    {
        //        File.Copy(Application.streamingAssetsPath + "/testDB.sqlite", filepath);
        //    }
        //}
        string conn = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {

            conn = Application.persistentDataPath + "/testDB.sqlite";
            if (!File.Exists(conn))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "/assets/testDB.sqlite");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(conn, loadDB.bytes);
                Debug.Log("successExists");
            }

        }
        else
        {
            Debug.Log("failExists");
        }
    }


    //GpsplaceSetting
    public void DS_InventorySetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Inventory";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int Num = cards_Inventory.Count;
            int Cost = reader.GetInt32(1);
            int Hp = reader.GetInt32(2);
            int Damage = reader.GetInt32(3);
            string Name = reader.GetString(4);
            Element element = (Element)reader.GetInt32(5);

            cards_Inventory.Add(new Item(Num, Cost, Hp, Damage, Name, element));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void DS_DeckSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Deck";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int Num = cards_Inventory.Count;
            int Cost = reader.GetInt32(1);
            int Hp = reader.GetInt32(2);
            int Damage = reader.GetInt32(3);
            string Name = reader.GetString(4);
            Element element = (Element)reader.GetInt32(5);

            cards_Deck.Add(new Item(Num, Cost, Hp, Damage, Name, element));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void DS_DeckReSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "delete FROM Deck";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Close();
        reader = null;
        for (int i = 0; cards_Deck.Count < i; i++)
        {
            sqlQuery = string.Empty;
            sqlQuery = "insert into deck values(" +cards_Deck[i].cardInventoryNum +","+ cards_Deck[i].cardCost+ ","+ cards_Deck[i].cardHp + ","+ cards_Deck[i] .cardDamage+ ","+ cards_Deck[i].cardName+ ","+ cards_Deck[i].cardElement + "); ";

            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
            reader.Close();
            reader = null;
        }
        
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
            int Num = cards_Inventory.Count;
            int Cost = reader.GetInt32(1);
            int Hp = reader.GetInt32(2);
            int Damage = reader.GetInt32(3);
            string Name = reader.GetString(4);
            Element element = (Element)reader.GetInt32(5);

            cards.Add(new Item(Num, Cost, Hp, Damage, Name, element));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void Ds_InventoryInsert(int DBnum)
    {
        Debug.Log(DBnum);
        Debug.Log(cards_Inventory.Count + 1);
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "insert into Inventory select* from Cards where num = " + DBnum + "; ";
        sqlQuery += "update Inventory set Num = " + (cards_Inventory.Count + 1) + " where Num = " + DBnum + "; ";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        //while (reader.Read())
        //{
        //    int Num = reader.GetInt32(0);
        //    string Races = reader.GetString(1);
        //    string Rarelty = reader.GetString(2);
        //    string Type = reader.GetString(3);
        //    int HP = reader.GetInt32(4);
        //    int Depensive = reader.GetInt32(5);
        //    int MoveRange = reader.GetInt32(6);
        //    int AttackRange = reader.GetInt32(7);
        //    inventory_places.Add(new GpsData(Num, Races, Rarelty, Type, HP, Depensive, MoveRange, AttackRange));
        //}
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void DS_GpsplaceFinding(string quadtree)
    {
        currentquadplaces.Clear();
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Gpsplace WHERE quad=\"" + quadtree + "\"";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int Num = currentquadplaces.Count;
            string Name = reader.GetString(1);
            double Latitude = reader.GetDouble(2);
            double Longitude = reader.GetDouble(3);
            Element element = (Element)reader.GetInt32(4);
            string quad = reader.GetString(5);
            currentquadplaces.Add(new GpsData(Num, Name, Latitude, Longitude, element, quad));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        Debug.Log(currentquadplaces.Count.ToString());
    }

    //public void DS_DbSetting()
    //{
    //    {
    //        IDbConnection dbconn;
    //        IDbCommand dbcmd;
    //        string sqlQuery;
    //        IDataReader reader;

    //        if (Application.platform == RuntimePlatform.Android)
    //        {
    //            conn = "URI=file:" + Application.persistentDataPath + "/testDB.sqlite";
    //        }
    //        else
    //        {
    //            conn = "URI=file:" + Application.dataPath + "/StreamingAssets/testDB.sqlite";
    //        }
    //            dbconn = (IDbConnection)new SqliteConnection(conn);
    //            dbconn.Open();

    //            dbcmd = dbconn.CreateCommand();
    //            sqlQuery = "SELECT * FROM Card";
    //            dbcmd.CommandText = sqlQuery;
    //            reader = dbcmd.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                int Num = reader.GetInt32(0);
    //                string Name = reader.GetString(1);
    //                double Latitude = reader.GetDouble(2);
    //                double Longitude = reader.GetDouble(3);
    //                string quad = reader.GetString(4);
    //                places.Add(new GpsData(Num, Name, Latitude, Longitude, quad));
    //            }
    //            reader.Close();
    //            reader = null;
    //            dbcmd.Dispose();
    //            dbcmd = null;
    //            dbconn.Close();
    //            dbconn = null;
    //    }
    //}
}