using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using System.Text;
public class BigBagControlScript : MonoBehaviour
{

    public DataBaseComunication dataBaseComunication;
    public GameObject bigBag;
    private MySqlDataReader reader = null;
    public Material materialInitialBigBags, materialSelectableBigBags, materialArrayBigBag;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
    void Start()
    {
        InstanteateAndSetPosition();
    }

    public void InstanteateAndSetPosition()
    {
        reader = dataBaseComunication.selectBigBags();
        InstanteateAllBigBagsInDataBase();
    }
    public void InstanteateAndSetPosition(int id)
    {
        reader = dataBaseComunication.selectBigBags(id);
        InstanteateAllBigBagsInDataBase();
    }
    private void InstanteateAllBigBagsInDataBase()
    {
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string descricaoLocalizacao = (string)reader["descricaoLocalizacao"];
                if (!GameObject.Find(descricaoLocalizacao)) {
                    
                    int linha = int.Parse("" + descricaoLocalizacao[4] + descricaoLocalizacao[5]);
                    int coluna = int.Parse("" + descricaoLocalizacao[7] + descricaoLocalizacao[8]);
                    int nivel = int.Parse("" + descricaoLocalizacao[10]);
                    GameObject bigbagbox = new GameObject(descricaoLocalizacao, typeof(MeshFilter), typeof(MeshRenderer), typeof(BigBagScript));
                    bigbagbox.transform.localScale = new Vector3(1, 1, 0.95f);
                    bigbagbox.GetComponent<MeshFilter>().mesh = bigBag.GetComponent<MeshFilter>().sharedMesh;
                    bigbagbox.GetComponent<MeshRenderer>().material = materialInitialBigBags;
                    bigbagbox.GetComponent<BigBagScript>().materialSelectable =  materialSelectableBigBags;
                    bigbagbox.GetComponent<BigBagScript>().materialInitial = materialInitialBigBags;
                    bigbagbox.GetComponent<BigBagScript>().materialArray = materialArrayBigBag;

                    bigbagbox.transform.position = new Vector3((coluna * 2), (linha * -2), ((nivel * -1) + 0.5f));
                    bigbagbox.GetComponent<BigBagScript>().InsertAllInformationsInObject(descricaoLocalizacao,
                                                                                            (string)reader["descricaoLote"],
                                                                                            (string)reader["descricaoTipoCafe"],
                                                                                            (int)reader["pesokg"],
                                                                                            (int)reader["qtdSacasDeCafe"],
                                                                                            (int)reader["codRFID"],
                                                                                            (int)reader["idBigBag"]);
                }
            }
        }
        else
        {
            Debug.Log("Nenhum BigBag Encontrado!");
        }
        dataBaseComunication.CloseConnection(reader);
    }
   

    public void destroyBigBag(string nomeObjeto)
    {
        int nivel = int.Parse("" + nomeObjeto[10]);
        StringBuilder sb = new StringBuilder(nomeObjeto);
        if ((nivel) < 4)
        {
            sb.Remove(10, 1);
            sb.Insert(10, (nivel + 1));
            string VerificacaoNivel = sb.ToString();
            if (GameObject.Find(VerificacaoNivel))
            {
                Debug.Log("Não é possivel removever um big um BigBag se tem um no nivel acima.");
                return;
            }
        }
        GameObject objeto = GameObject.Find(nomeObjeto);

        try { 
            int id = objeto.GetComponent<BigBagScript>().ID;
            dataBaseComunication.queryForMysql("DELETE from bigbag where bigbag.idBigBag=" + id);
            Destroy(objeto);
        }catch(Exception ex)
        {
            Debug.LogError(ex);    
        }
    }
    public void destroyBigBag(int area, int linha, int coluna, int nivel, bool isLeftSide)
    {
        string nomeObjeto = "A" + ((area > 9) ? "" + area : "0" + area) +
                                       "L" + ((linha > 9) ? "" + linha : "0" + linha) +
                                       "C" + ((coluna > 9) ? "" + coluna : "0" + coluna) +
                                       "N" + ((nivel > 4) ? "" + 4 : "" + nivel) +
                                       ((isLeftSide) ? "E" : "D");
        StringBuilder sb = new StringBuilder(nomeObjeto);
        if ((nivel + 1 )<4 )
        {
            sb.Remove(10, 1);
            sb.Insert(10,(nivel + 1));
            string VerificacaoNivel = sb.ToString();
            if (GameObject.Find(VerificacaoNivel))
            {
               Debug.Log( "Não é possivel removever um big um BigBag se tem um no nivel acima.");
                return;
            }
        }
        GameObject objeto = GameObject.Find(nomeObjeto);

        try
        {
            int id = objeto.GetComponent<BigBagScript>().ID;
            dataBaseComunication.queryForMysql("DELETE from bigbag where bigbag.idBigBag=" + id);
            Destroy(objeto);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }


    public void InsertBigBag( int idTipoCafe, string descricaoLocalizacao,
                                        int codRFID,int pesokg,int idLote,int qtdSacasDeCafe,
                                        int dataEntradaCafe )
    {
        try
        {

            dataBaseComunication.queryForMysql("INSERT INTO databasepi.bigbag(codRFID, idTipoCafe, pesokg, dataEntradaCafe, idLote, qtdSacasDeCafe, descricaoLocalizacao)" +
                "VALUES("+ codRFID + ","+idTipoCafe+","+ pesokg + ","+ dataEntradaCafe + ","+ idLote + ","+ qtdSacasDeCafe + ",'"+ descricaoLocalizacao + "')");
            InstanteateAndSetPosition();
            
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    public void InsertLote(string descricao, int idProdutor)
    {
        try
        {
            dataBaseComunication.queryForMysql("INSERT INTO databasepi.lote(descricaoLote, idProdutor)" +
                "VALUES('"+descricao+"',"+idProdutor + ")");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    public void InsertTypeDescricao(string type, string descricao)
    {
        try
        {
            type.Trim();
            type.ToLower();
            string comando = "INSERT INTO databasepi.";

            if (type.Equals("produtor"))
            {
                comando += "produtor(nome)";
            }
            else if (type.Equals("tipocafe"))
            {
                comando += "tipoCafe(descricaoTipoCafe)";
            }

            comando += " VALUES('" + descricao + "')";
       
            dataBaseComunication.queryForMysql(comando);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
  
    public void UpdateBigBag(int id, int idTipoCafe, string descricaoLocalizacao, int codRFID, int pesokg, int idLote, int qtdSacasDeCafe, int dataEntradaCafe)
    {
        try
        {

            dataBaseComunication.queryForMysql("UPDATE `databasepi`.`bigbag` SET("+ codRFID + "," + idTipoCafe + "," + pesokg + "," + 
                                                                                dataEntradaCafe + "," + idLote + "," + qtdSacasDeCafe +
                                                                                ",'" + descricaoLocalizacao + "') " +
                                                                                "WHERE `idBigBag` =" + id);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }


}
