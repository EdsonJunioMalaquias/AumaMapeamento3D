using UnityEngine;
using System;
using System.Data;
using System.Text;
using MySql.Data;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

public class DataBaseComunication : MonoBehaviour
{
    private readonly string host = "localhost",
                            database = "databasepi",
                            user = "root",
                            password = "root";

    private string connectionString;
    private MySqlConnection conexao = null;
    private MySqlCommand comando = null;
    private MySqlDataReader reader = null;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.connectionString = "Server=" + this.host + ";Database=" + this.database + ";User=" + this.user + ";Password=" + this.password;
    }
    public MySqlConnection openConnection()
    {
        try
        {
            conexao = new MySqlConnection(this.connectionString);
            conexao.Open();
            return conexao;    
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return null;
        }
    }
    public MySqlDataReader selectBigBags()
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select bb.*,tc.descricaoTipoCafe, l.descricaoLote, p.nome " +
                                    " from bigbag as bb inner join tipoCafe as tc " +
                                    " on tc.idTipoCafe = bb.idTipoCafe inner join lote as l on l.idlote = bb.idlote " +
                                    " inner join produtor as p on p.idProdutor = l.idProdutor"; 
            reader = comando.ExecuteReader();
            return reader;
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return null;
        }
    }
    public MySqlDataReader selectBigBags(int id)
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select bb.*,tc.descricaoTipoCafe, l.descricaoLote, p.nome " +
                                  "from bigbag as bb innerjoin tipoCafe as tc on tc.idTipoCafe = bb.idTipoCafe" +
                                  "inner join lote as l on l.idlote = bb.idlote " +
                                  "inner join produtor as p on p.idProdutor = l.idProdutor" +
                                  "where bigbag.idBigBag = " + id ;
            reader = comando.ExecuteReader();
            return reader;
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return null;
        }
    }
    public MySqlDataReader selectBigBags(string UID, string lote, string produtor, string tipoCafe)
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select bb.*,tc.descricaoTipoCafe, l.descricaoLote, p.nome " +
                              "from bigbag as bb inner join tipoCafe as tc on tc.idTipoCafe = bb.idTipoCafe " +
                              "inner join lote as l on l.idlote = bb.idlote " +
                              "inner join produtor as p on p.idProdutor = l.idProdutor " +
                              "where ";

            if (UID != null)
            {
                comando.CommandText += "codRFID = " + UID;
            }
            if (lote != null)
            {
                comando.CommandText += ((UID == null)?"": " AND ") + " descricaoLote = '" + lote + "'";
            }
            if (produtor != null)
            {
                comando.CommandText += ((UID == null && lote == null) ? "" : " AND ") + " nome = '" + produtor + "'";
            }
            if (tipoCafe != null)
            {
                comando.CommandText += ((UID == null && lote == null) ? "" : " AND ") + " descricaoTipoCafe = '" + tipoCafe + "'";
            }

            reader = comando.ExecuteReader();
            return reader;
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return null;
        }
    }

    public MySqlDataReader selectAllInfoOfType(string type)
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select * from " + type;
            reader = comando.ExecuteReader();
            return reader;
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();

            return null;
        }
    }

    public MySqlDataReader selectAllInfoOfType(string type,string descricao)
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select * from " + type + " where ";
            type.ToLower();
            type.Trim();
            if (type.Equals("lote"))
            {
                comando.CommandText += " lote.descricaoLote = '" + descricao + "'";
            }else
            if (type.Equals("produtor"))
            {
                comando.CommandText += " produtor.nome = '" + descricao + "'";
            }else
            if (type.Equals("tipocafe"))
            {
                comando.CommandText += " tipoCafe.descricaoTipoCafe = '" + descricao + "'";
            }else
            if (type.Equals("bigbag"))
            {
                comando.CommandText += " bigbag.codRFID = " + descricao;
            }else
            if (type.Equals("bigbaglote"))
            {
                comando.CommandText = "SELECT * FROM databasepi.bigbag where bigbag.idLote = (select idLote from lote where lote.descricaoLote ='"+descricao+"');";
            }
           reader = comando.ExecuteReader();
            return reader;
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return null;
        }
    }

    public void queryForMysql(string query)
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = query;
            comando.Prepare();
            reader = comando.ExecuteReader();
            
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
        }
    }

    public void CloseConnection(MySqlDataReader msdr)
    {
        msdr.Close();
        comando = null;
        conexao.Close();
    }
    public void CloseConnection()
    {
        comando = null;
        conexao.Close();
    }
}