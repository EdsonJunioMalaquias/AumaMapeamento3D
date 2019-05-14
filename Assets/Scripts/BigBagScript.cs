using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BigBagScript : MonoBehaviour
{
    //"A"+area+"L" + linha + "C" + coluna + "N" + nivel + ((ladoEsquerdo)?"E":"D");
    private string descricaoLocalização;
    private string descricaolote;
    private int peso;
    private int qtdSacasDeCafe;
    private int tagRFID;
    private int iD;
    private string tipoCafe;

    public string DescricaoLocalização { get => descricaoLocalização; set => descricaoLocalização = value; }
    public string Descricaolote { get => descricaolote; set => descricaolote = value; }
    public int Peso { get => peso; set => peso = value; }
    public int TagRFID { get => tagRFID; set => tagRFID = value; }
    public int ID { get => iD; set => iD = value; }
    public int QtdSacasDeCafe { get => qtdSacasDeCafe; set => qtdSacasDeCafe = value; }
    public string TipoCafe { get => tipoCafe; set => tipoCafe = value; }

    public void insertAllInformationsInObject( string descricaoLocalização, string descricaolote,string tipoCafe,int peso, int qtdSacasDeCafe, int tagRFID, int iD ){
        DescricaoLocalização = descricaoLocalização;
        Descricaolote = descricaolote;
        Peso = peso;
        QtdSacasDeCafe = qtdSacasDeCafe;
        TagRFID = tagRFID;
        ID = iD;
        TipoCafe = tipoCafe;
    }
}
