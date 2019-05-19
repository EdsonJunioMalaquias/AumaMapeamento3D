using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using MySql.Data.MySqlClient;

public class ItemTempFounded
{
    private string localizacao, produtor, lote;
    private BigBagScript bigBagScript;

    public string Localizacao { get => localizacao; set => localizacao = value; }
    public string Produtor { get => produtor; set => produtor = value; }
    public string Lote { get => lote; set => lote = value; }
    public BigBagScript BigBagScript { get => bigBagScript; set => bigBagScript = value; }

    public ItemTempFounded(BigBagScript bigBagScript, string renderLocalizacao, string renderProdutor, string renderLote)
    {  
        this.BigBagScript = bigBagScript;
        this.Localizacao = renderLocalizacao;
        this.Produtor = renderProdutor;
        this.Lote = renderLote;
    }

 
}

public class FoundedBags : MonoBehaviour
{
     
    public ItemFounded[] itensFounded;
    public Button next, previous;
    private List<ItemTempFounded> tempItensFounded = new List<ItemTempFounded>();
    public UIController uIController;
    private MySqlDataReader reader = null;
    private int countTemp = 0,IndexControladorPreviusAndNext =0;
    public static string LastBigBagSelected = "";

    public void cancelar()
    {
        uIController.CloseAllView();
        uIController.OpenViewFind();
    }
    public void ResultParcial()
    {
        ClearPesquisa();
        try
        {
            string uID, lote, produtor, tipoCafe;
            uID = uIController.find.FieldUID.text;
            lote = uIController.find.DropDownLote.captionText.text;
            produtor = uIController.find.DropDownProdutor.captionText.text;
            tipoCafe = uIController.find.DropDownTipoCafe.captionText.text;

            if (uID.Equals(""))
            {
                uID = null;
            }
            if (lote.Equals("Selecione"))
            {
                lote = null;
            }
            if (produtor.Equals("Selecione"))
            {
                produtor = null;
            }
            if (tipoCafe.Equals("Selecione"))
            {
                tipoCafe = null;
            }

           
            uIController.CloseAllView();
            uIController.controlerPadrao.FixedMenu.SetActive(false);
            uIController.controlerPadrao.ViewFoundParcial.SetActive(true);
            reader = uIController.controlerPadrao.dataBaseComunication.selectBigBags(
                uID, lote, produtor, tipoCafe);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string descricaoLocalizacao = (string)reader["descricaoLocalizacao"];
                    GameObject gameObject = GameObject.Find(descricaoLocalizacao);

                    if (gameObject != null)
                    {
                        ItemTempFounded itemTempFounded = new ItemTempFounded(gameObject.GetComponent<BigBagScript>(), descricaoLocalizacao, (string)reader["nome"], (string)reader["descricaoLote"]);
                        tempItensFounded.Add(itemTempFounded);
                        GameObject.Find(descricaoLocalizacao).GetComponent<BigBagScript>().ApplyArrayMaterial();
                    }

                }
                if (tempItensFounded.Count < 3)
                {
                    previous.enabled = false;
                    next.enabled = false;
                }
                else
                {
                    previous.enabled = true;
                    next.enabled = true;
                }
            }
            else
            {
                itensFounded[0].Clear();
                itensFounded[1].Clear();
                itensFounded[2].Clear();
                ClearPesquisa();
                Debug.Log("Nenhum BigBag Encontrado!");

            }
            uIController.controlerPadrao.dataBaseComunication.CloseConnection(reader);
            RenderItens();


        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }


    public void ResultCompleto()
    {
       
    }
    public void RenderItens()
    {
        for(int count = 0; count < 3;count ++){
            if (tempItensFounded.Count != 0) { 
            if (count == 0)
                {
                    IndexControladorPreviusAndNext= countTemp;
                }
                
                if (countTemp < tempItensFounded.Count)
                {
                    itensFounded[count].Clear();
                    itensFounded[count].Renderizar(tempItensFounded.ToArray()[countTemp].BigBagScript,
                        tempItensFounded.ToArray()[countTemp].Localizacao, 
                        tempItensFounded.ToArray()[countTemp].Produtor,
                        tempItensFounded.ToArray()[countTemp].Lote);
                    countTemp++;
                    
                }
                else
                {
                countTemp = 0;
                    if (count == 1)
                    {
                        itensFounded[1].Clear();
                    }
                        itensFounded[2].Clear();
                    break;
                }
            }
            else
            {
                break;
            }
        }

    }
    public void NextItens()
    {
        RenderItens();
    }
    public void PreviousItens()
    {
        int resto = tempItensFounded.Count % 3;

   
        if (IndexControladorPreviusAndNext < 3)
        {
            if (resto == 0)
            {
                countTemp = tempItensFounded.Count - 3;
            }else
            if (resto == 1)
            {
                countTemp = tempItensFounded.Count-1 ;
            }
            else
            if (resto == 2)
            {
                countTemp = tempItensFounded.Count - 2;
            }
        }
        else if (IndexControladorPreviusAndNext >= 3)
        {
            countTemp = IndexControladorPreviusAndNext -3;
        }


        RenderItens();
    }
    public void Close()
    {
        ClearPesquisa();
        uIController.controlerPadrao.FixedMenu.SetActive(true);
        uIController.controlerPadrao.ViewFoundParcial.SetActive(false);
    }
    public void FazerNovaPesquisa()
    {
        ClearPesquisa();
        uIController.controlerPadrao.ViewFoundParcial.SetActive(false);
        uIController.OpenViewFind();

    }
   void ClearPesquisa()
    {
        for (int i = 0; i < tempItensFounded.Count; i++)
        {
            GameObject.Find(tempItensFounded.ToArray()[i].Localizacao).GetComponent<BigBagScript>().ApplyInitialMaterial();
        }
        LastBigBagSelected = "";
        tempItensFounded.Clear();
        countTemp = 0;
        IndexControladorPreviusAndNext = 0;
    }
    public void Delete(int i)
    {
        if (itensFounded[i].RenderLocalizacao.text.Equals(""))
        {
            return;
        }
        uIController.controlerPadrao.bigBagControlScript.destroyBigBag(itensFounded[i].RenderLocalizacao.text);
        ClearPesquisa();
        ResultParcial();
    }
}
