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
                            password = "";

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
    public MySqlDataReader selectAllBigBags()
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select bb.*, l.*, p.* " +
                                    "from bigbag as bb inner join lote as l on l.idlote = bb.idlote " +
                                    "inner join produtor as p on p.idProdutor = l.idProdutor"; 
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
    public MySqlDataReader selectLotesDescricao()
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select lote.descricao from lote";
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
    public MySqlDataReader selectProdutorNome()
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select produtor.nome from produtor";
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


    public MySqlDataReader selectOneBigBag(int id)
    {
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "SELECT * FROM bigbag where bigbag.idBigBag ='" + id + "'";
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
    public MySqlDataReader selectBigBagsLoteByLote(string lote)
    {
        int idlote = returnIdLote(lote);
        if (idlote != -1)
        {
            try
            {
                MySqlConnection conexao = openConnection();
                comando = conexao.CreateCommand();
                comando.CommandText = "select  bigbag.*, lote.*, produtor.* " +
                    "from (bigbag inner join lote on bigbag.idlote = lote.idLote " +
                    "inner join produtor on produtor.idProdutor = lote.idProdutor) " +
                    "where lote.idlote = "+idlote;
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
        return null;
    }
    public int returnIdLote(string descricaoLote)
    {
        int retorno = -1;
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = "select idlote from lote where lote.descricao = '"+descricaoLote+"'";
            reader = comando.ExecuteReader();
            while (reader.Read()) {
                retorno = (int)reader["idLote"];
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return -1;
        }

        return retorno;
    }
    public int returnRFID(int tagRFID)
    {
        int retorno = -1;
        try
        {
            MySqlConnection conexao = openConnection();
            comando = conexao.CreateCommand();
            comando.CommandText = " SELECT bigbag.codRFID FROM databasepi.bigbag where codRfid =" + tagRFID ;
            reader = comando.ExecuteReader();
            while (reader.Read())
            {

                retorno++;
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError(ex);
            CloseConnection();
            return -1;
        }

        return retorno;
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