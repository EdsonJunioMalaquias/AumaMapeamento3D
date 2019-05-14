using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.IO.Ports;
public class BigBagControlScript : MonoBehaviour
{

    public DataBaseComunication dataBaseComunication;
    public GameObject bigBag;
    private MySqlDataReader reader = null;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        InstanteateAllBigBagsInDataBase();
    }

    public void InstanteateAndSetPosition(int area, int linha, int coluna, int nivel, bool isLeftSide)
    {

        string descricaoLocalizacao = "A" + ((area > 9) ? "" + area : "0" + area) +
                                        "L" + ((linha > 9) ? "" + linha : "0" + linha) +
                                        "C" + ((coluna > 9) ? "" + coluna : "0" + coluna) +
                                        "N" + ((nivel > 4) ? "" + 4 : "" + nivel) +
                                        ((isLeftSide) ? "E" : "D");
        reader = dataBaseComunication.selectAllBigBags();
        if (reader != null)
        {
            while (reader.Read())
            {
                GameObject bigbagbox = new GameObject(descricaoLocalizacao, typeof(MeshFilter), typeof(MeshRenderer), typeof(BigBagScript));
                bigbagbox.GetComponent<MeshFilter>().mesh = bigBag.GetComponent<MeshFilter>().sharedMesh;
                bigbagbox.GetComponent<MeshRenderer>().material = bigBag.GetComponent<MeshRenderer>().sharedMaterial;
                bigbagbox.transform.position = new Vector3((coluna * 2), (linha * -2), ((nivel * -1) + 0.5f));
                bigbagbox.GetComponent<BigBagScript>().insertAllInformationsInObject(descricaoLocalizacao,
                                                                                        reader["idLote"].ToString(),
                                                                                        (string)reader["tipo"],
                                                                                        (int)reader["pesokg"],
                                                                                        (int)reader["qtdSacasDeCafe"],
                                                                                        (int)reader["codRFID"],
                                                                                        (int)reader["idBigBag"]);
            }
        }
        dataBaseComunication.CloseConnection(reader);

    }
    public void InstanteateAllBigBagsInDataBase()
    {
        reader = dataBaseComunication.selectAllBigBags();
        while (reader.Read())
        {
            string descricaoLocalizacao = (string)reader["descricaoLocalizacao"];
            int linha = int.Parse("" + descricaoLocalizacao[4] + descricaoLocalizacao[5]);
            int coluna = int.Parse("" + descricaoLocalizacao[7] + descricaoLocalizacao[8]);
            int nivel = int.Parse("" + descricaoLocalizacao[10]);
            GameObject bigbagbox = new GameObject(descricaoLocalizacao, typeof(MeshFilter), typeof(MeshRenderer), typeof(BigBagScript));
            bigbagbox.transform.localScale=new Vector3(1, 1, 0.95f);
            bigbagbox.GetComponent<MeshFilter>().mesh = bigBag.GetComponent<MeshFilter>().sharedMesh;
            bigbagbox.GetComponent<MeshRenderer>().material = bigBag.GetComponent<MeshRenderer>().sharedMaterial;
            bigbagbox.transform.position = new Vector3((coluna * 2), (linha * -2), ((nivel * -1) + 0.5f));
            bigbagbox.GetComponent<BigBagScript>().insertAllInformationsInObject(   descricaoLocalizacao, 
                                                                                    reader["idLote"].ToString(),
                                                                                    (string)reader["tipo"],
                                                                                    (int)reader["pesokg"], 
                                                                                    (int)reader["qtdSacasDeCafe"],
                                                                                    (int)reader["codRFID"],
                                                                                    (int)reader["idBigBag"]);
        }
        dataBaseComunication.CloseConnection(reader);
        

    }
   
    public void destroyBigBag(int area, int linha, int coluna, int nivel, bool isLeftSide)
    {
        string nomeObjeto =  "A" + ((area > 9) ? "" + area : "0" + area) +
                                       "L" + ((linha > 9) ? "" + linha : "0" + linha) +
                                       "C" + ((coluna > 9) ? "" + coluna : "0" + coluna) +
                                       "N" + ((nivel > 4) ? "" + 4 : "" + nivel) +
                                       ((isLeftSide) ? "E" : "D");

        GameObject objeto = GameObject.Find(nomeObjeto);

        if (objeto != null)
        {
            int id = objeto.GetComponent<BigBagScript>().ID;
            dataBaseComunication.queryForMysql("Delete from bigbag where id = " + id);
            Destroy(objeto);
            Debug.Log("Objeto Excluido com sucesso");
        }
        else
        {
            Debug.LogError("Erro!");
        }
    }
    public void destroyBigBag(string nomeObjeto)
    {
        GameObject objeto = GameObject.Find(nomeObjeto);

        try { 
            int id = objeto.GetComponent<BigBagScript>().ID;
            dataBaseComunication.queryForMysql("DELETE from bigbag where bigbag.idBigBag=" + id);
            Destroy(objeto);
            Debug.Log("Objeto Excluido com sucesso");
        }catch(Exception ex)
        {
            Debug.LogError(ex);    
        }
    }


    public void insertBigBagInDataBase(string tipo, string descricaoLocalizacao,
                                        int codRFID,int pesokg,int idLote,int qtdSacasDeCafe,
                                        int dataEntradaCafe )
    {
        
        try
        {

            dataBaseComunication.queryForMysql("INSERT INTO databasepi.bigbag(codRFID, tipo, pesokg, dataEntradaCafe, idLote, qtdSacasDeCafe, descricaoLocalizacao)" +
                "VALUES("+ codRFID + ",'"+tipo+"',"+ pesokg + ","+ dataEntradaCafe + ","+ idLote + ","+ qtdSacasDeCafe + ",'"+ descricaoLocalizacao + "')");
            InstanteateAllBigBagsInDataBase();
            
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    public void UpdateBigBagDataBase(int id, string tipo, string descricaoLocalizacao,
                                        int codRFID, int pesokg, int idLote, int qtdSacasDeCafe,
                                        DateTime dataEntradaCafe)
    {
        try
        {

            dataBaseComunication.queryForMysql("UPDATE `databasepi`.`bigbag` SET("+ codRFID + ",'" + tipo + "'," + pesokg + "," + dataEntradaCafe + "," + idLote + "," + qtdSacasDeCafe + ",'" + descricaoLocalizacao + "') WHERE `idBigBag` =" + id);

        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }


}
