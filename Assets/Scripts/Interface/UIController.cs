using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System;
using System.Text;
[Serializable]
public class Cadastro
{
    [SerializeField]
    private InputField fieldUID, fieldDateDay, fieldDateMonth, fieldDateYear, fieldLinha, fieldColuna, fieldPeso, fieldQuantidadeSacasCafe, fieldDateSaidaDay, fieldDateSaidaMonth, fieldDateSaidaYear,fieldCadastroLote,fieldCadastroProdutor,fieldCadastroTipoCafe;
    [SerializeField]
    private Dropdown dropDownArea, dropDownNivel, dropDownLote, dropDownProdutor, dropDownTipoCafe, dropdownProdutorCadastroLote;
    [SerializeField]
    private Text mensagemErro;
    private bool isLeftSide = true;

    public InputField FieldUID { get => fieldUID; set => fieldUID = value; }
    public InputField FieldDateDay { get => fieldDateDay; set => fieldDateDay = value; }
    public InputField FieldDateMonth { get => fieldDateMonth; set => fieldDateMonth = value; }
    public InputField FieldDateYear { get => fieldDateYear; set => fieldDateYear = value; }
    public InputField FieldLinha { get => fieldLinha; set => fieldLinha = value; }
    public InputField FieldColuna { get => fieldColuna; set => fieldColuna = value; }
    public InputField FieldPeso { get => fieldPeso; set => fieldPeso = value; }
    public InputField FieldQuantidadeSacasCafe { get => fieldQuantidadeSacasCafe; set => fieldQuantidadeSacasCafe = value; }
    public Dropdown DropDownArea { get => dropDownArea; set => dropDownArea = value; }
    public Dropdown DropDownNivel { get => dropDownNivel; set => dropDownNivel = value; }
    public Dropdown DropDownLote { get => dropDownLote; set => dropDownLote = value; }
    public Dropdown DropDownProdutor { get => dropDownProdutor; set => dropDownProdutor = value; }
    public Dropdown DropDownTipoCafe { get => dropDownTipoCafe; set => dropDownTipoCafe = value; }
    public Text MensagemErro { get => mensagemErro; set => mensagemErro = value; }
    public bool IsLeftSide { get => isLeftSide; set => isLeftSide = value; }
    public InputField FieldDateSaidaDay { get => fieldDateSaidaDay; set => fieldDateSaidaDay = value; }
    public InputField FieldDateSaidaMonth { get => fieldDateSaidaMonth; set => fieldDateSaidaMonth = value; }
    public InputField FieldDateSaidaYear { get => fieldDateSaidaYear; set => fieldDateSaidaYear = value; }
    public Dropdown DropdownProdutorCadastroLote { get => dropdownProdutorCadastroLote; set => dropdownProdutorCadastroLote = value; }
    public InputField FieldCadastroLote { get => fieldCadastroLote; set => fieldCadastroLote = value; }
    public InputField FieldCadastroProdutor { get => fieldCadastroProdutor; set => fieldCadastroProdutor = value; }
    public InputField FieldCadastroTipoCafe { get => fieldCadastroTipoCafe; set => fieldCadastroTipoCafe = value; }
}
[Serializable]
public class ControlerPadrao
{

    public DataBaseComunication dataBaseComunication;
    public EnderecamentoArea enderecamentoArea;
    public BigBagControlScript bigBagControlScript;
    private List<string> areaList = new List<string> { "1" };
    private List<string> nivelList = new List<string> { "Selecione","1", "2", "3", "4" };
    private List<string> tipoCafeList = new List<string> { "MK", "Mk14" };
    private List<string> produtorList = new List<string>();
    private List<string> loteList = new List<string>();
    private List<string> descricaoLotes;
    private MySqlDataReader reader = null;

    [SerializeField]
    private GameObject fixedMenu, mainPanel,viewNewBigBag, viewNewLote,viewNewProdutor,viewNewTypeCoff, viewFind, viewFound, viewFoundParcial;

    public List<string> AreaList { get => areaList; set => areaList = value; }
    public List<string> NivelList { get => nivelList; set => nivelList = value; }
    public List<string> TipoCafeList { get => tipoCafeList; set => tipoCafeList = value; }
    public List<string> ProdutorList { get => produtorList; set => produtorList = value; }
    public List<string> LoteList { get => loteList; set => loteList = value; }
    public List<string> DescricaoLotes { get => descricaoLotes; set => descricaoLotes = value; }
    public MySqlDataReader Reader { get => reader; set => reader = value; }
    public GameObject FixedMenu { get => fixedMenu; set => fixedMenu = value; }
    public GameObject MainPanel { get => mainPanel; set => mainPanel = value; }
    public GameObject ViewNewBigBag { get => viewNewBigBag; set => viewNewBigBag = value; }
    public GameObject ViewNewLote { get => viewNewLote; set => viewNewLote = value; }
    public GameObject ViewNewProdutor { get => viewNewProdutor; set => viewNewProdutor = value; }
    public GameObject ViewNewTypeCoff { get => viewNewTypeCoff; set => viewNewTypeCoff = value; }
    public GameObject ViewFind { get => viewFind; set => viewFind = value; }
    public GameObject ViewFound { get => viewFound; set => viewFound = value; }
    public GameObject ViewFoundParcial { get => viewFoundParcial; set => viewFoundParcial = value; }
}
[Serializable]
public class Find
{
    [SerializeField]
    private InputField fieldUID, fieldDateDay, fieldDateMonth, fieldDateYear;
    [SerializeField]
    private Dropdown dropDownLote, dropDownProdutor, dropDownTipoCafe;
    [SerializeField]
    private Text title;
    
    public InputField FieldUID { get => fieldUID; set => fieldUID = value; }
    public InputField FieldDateDay { get => fieldDateDay; set => fieldDateDay = value; }
    public InputField FieldDateMonth { get => fieldDateMonth; set => fieldDateMonth = value; }
    public InputField FieldDateYear { get => fieldDateYear; set => fieldDateYear = value; }
    public Dropdown DropDownLote { get => dropDownLote; set => dropDownLote = value; }
    public Dropdown DropDownProdutor { get => dropDownProdutor; set => dropDownProdutor = value; }
    public Dropdown DropDownTipoCafe { get => dropDownTipoCafe; set => dropDownTipoCafe = value; }
    public Text Title { get => title; set => title = value; }
}
[Serializable]
public class Alert
{
    [SerializeField]
    private Text alertMensage;
    [SerializeField]
    private GameObject panelAlert;
    public static int controlador =0 ;
    public Text AlertMensage { get => alertMensage; set => alertMensage = value; }
    public GameObject PanelAlert { get => panelAlert; set => panelAlert = value; }
}


public class UIController : MonoBehaviour
{
    public Alert alert;
    public Find find;
    public ControlerPadrao controlerPadrao;
    public Cadastro cadastro;

    void Start()
    {
        CloseAllView();
        
        List<int> i = new List<int>();

    }

    public void FindBestPosition()
    {
        if (cadastro.DropDownLote.value == 0)
        {
            cadastro.MensagemErro.color = new Color(255, 0, 0, 255);
            cadastro.MensagemErro.text = "Selecione um lote valido!";
            return;
        }
        controlerPadrao.DescricaoLotes = new List<string>();
        controlerPadrao.Reader = null;
        string loteText = cadastro.DropDownLote.captionText.text;

        controlerPadrao.Reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("bigbaglote", loteText);
        while (controlerPadrao.Reader.Read() && controlerPadrao.Reader != null)
        {
            string descricaoLocalizacao = (string)controlerPadrao.Reader["descricaoLocalizacao"];
            controlerPadrao.DescricaoLotes.Add(descricaoLocalizacao);

            for (int i = 1; i <= 4; i++)
            {
                if (int.Parse("" + descricaoLocalizacao[10]) == i)
                {
                    if (i < 4)
                    {
                        StringBuilder sb = new StringBuilder(descricaoLocalizacao);
                        sb.Remove(10, 1);
                        sb.Insert(10, i + 1);
                        string localizacao = sb.ToString();
                        if (!GameObject.Find(localizacao))
                        {
                            cadastro.FieldColuna.text = ("" + descricaoLocalizacao[7] + descricaoLocalizacao[8]);
                            cadastro.FieldLinha.text = ("" + descricaoLocalizacao[4] + descricaoLocalizacao[5]);
                            cadastro.DropDownNivel.value = i + 1;
                            return;
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder(descricaoLocalizacao);
                        sb.Remove(10, 1);
                        sb.Insert(10, 1);
                        int coluna = int.Parse("" + descricaoLocalizacao[7] + descricaoLocalizacao[8]);
                        if (coluna <= controlerPadrao.enderecamentoArea.QtdColunas)
                        {
                            if (coluna + 1 < 10)
                            {
                                sb.Remove(8, 1);
                                sb.Insert(8, coluna + 1);
                            }
                            else
                            {
                                sb.Remove(7, 1);
                                sb.Remove(7, 1);
                                sb.Insert(7, coluna + 1);
                            }

                            string localizacao = sb.ToString();
                            if (!GameObject.Find(localizacao))
                            {
                                Debug.Log(localizacao);
                                cadastro.FieldColuna.text = ("" + localizacao[7] + localizacao[8]);
                                cadastro.FieldLinha.text = ("" + descricaoLocalizacao[4] + descricaoLocalizacao[5]);
                                cadastro.DropDownNivel.value = 1;
                                return;
                            }
                        }
                        else
                        {
                            int linha = int.Parse("" + descricaoLocalizacao[4] + descricaoLocalizacao[5]);
                            if (linha <= controlerPadrao.enderecamentoArea.QtdLinhas)
                            {
                                sb.Remove(7, 1);
                                sb.Remove(7, 1);
                                sb.Insert(7, 1);
                                sb.Insert(7, 0);

                                if (linha + 1 < 10)
                                {
                                    sb.Remove(5, 1);
                                    sb.Insert(5, coluna + 1);
                                }
                                else
                                {
                                    sb.Remove(4, 1);
                                    sb.Remove(4, 1);
                                    sb.Insert(4, coluna + 1);
                                }
                                string localizacao = sb.ToString();
                                Debug.Log(localizacao);
                                if (!GameObject.Find(localizacao))
                                {
                                    
                                    cadastro.FieldColuna.text = ("" + localizacao[7] + localizacao[8]);
                                    cadastro.FieldLinha.text = ("" + localizacao[4] + localizacao[5]);
                                    cadastro.DropDownNivel.value = 1;
                                    return;
                                }
                            }
                        }

                    }

                }
            }
        }
        int area = 1;
        bool isLeftSide = true;
        int nivel = 1;
        for (int linha = 0; linha <= controlerPadrao.enderecamentoArea.QtdLinhas; linha++)
        {
            for (int coluna = 1; coluna <= controlerPadrao.enderecamentoArea.QtdColunas; coluna++)
            {
                string descricaoLocalizacao = "A" + ((area > 9) ? "" + area : "0" + area) +
                                "L" + ((linha > 9) ? "" + linha : "0" + linha) +
                                "C" + ((coluna > 9) ? "" + coluna : "0" + coluna) +
                                "N" + nivel +
                                ((isLeftSide) ? "E" : "D");

                StringBuilder sb = new StringBuilder(descricaoLocalizacao);
                sb.Remove(10, 1);
                sb.Insert(10, nivel);
                string localizacao = sb.ToString();
                Debug.Log(!GameObject.Find(localizacao) + "\n" + localizacao + "\n" + coluna + "\n" + linha);
                if (!GameObject.Find(localizacao))
                {
                    cadastro.FieldColuna.text = ("" + descricaoLocalizacao[7] + descricaoLocalizacao[8]);
                    cadastro.FieldLinha.text = ("" + descricaoLocalizacao[4] + descricaoLocalizacao[5]);
                    cadastro.DropDownNivel.value = nivel;
                    return;
                }

                else
                {
                    if (linha < controlerPadrao.enderecamentoArea.QtdLinhas)
                    {
                        if (coluna == controlerPadrao.enderecamentoArea.QtdColunas)
                        {
                            coluna = 1;
                            linha++;
                        }
                    }
                    else
                    {
                        Debug.Log("Está Area atingio a lotação Maxima");
                        return;
                    }
                }

            }
        }
    }
    
    

    public void Refresh()
    {
        cadastro.DropDownArea.ClearOptions();
        cadastro.DropDownNivel.ClearOptions();
        cadastro.DropDownLote.ClearOptions();
        cadastro.DropDownProdutor.ClearOptions();
        cadastro.DropDownTipoCafe.ClearOptions();
        cadastro.DropdownProdutorCadastroLote.ClearOptions();
        
        cadastro.DropDownArea.AddOptions(controlerPadrao.AreaList);
        cadastro.DropDownNivel.AddOptions(controlerPadrao.NivelList);
        cadastro.DropDownLote.AddOptions(controlerPadrao.LoteList);
        cadastro.DropDownProdutor.AddOptions(controlerPadrao.ProdutorList);
        cadastro.DropDownTipoCafe.AddOptions(controlerPadrao.TipoCafeList);
        cadastro.DropdownProdutorCadastroLote.AddOptions(controlerPadrao.ProdutorList);

        cadastro.FieldDateDay.text = ((DateTime.Now.Day<10)? "0" + DateTime.Now.Day : "" + DateTime.Now.Day);
        cadastro.FieldDateMonth.text = ((DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month : "" + DateTime.Now.Month);
        cadastro.FieldDateYear.text = "" + DateTime.Now.Year;

        find.DropDownLote.ClearOptions();
        find.DropDownLote.AddOptions(controlerPadrao.LoteList);
        find.DropDownProdutor.ClearOptions();
        find.DropDownProdutor.AddOptions(controlerPadrao.ProdutorList);
        find.DropDownTipoCafe.ClearOptions();
        find.DropDownTipoCafe.AddOptions(controlerPadrao.TipoCafeList);
    }
    public void RefreshList()
    {   
        controlerPadrao.LoteList = new List<String> { "Selecione" };
        controlerPadrao.ProdutorList = new List<String> { "Selecione" };
        controlerPadrao.TipoCafeList = new List<string> { "Selecione" };

        controlerPadrao.Reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("lote");
        while (controlerPadrao.Reader.Read())
        {
            controlerPadrao.LoteList.Add((string)(controlerPadrao.Reader["descricaoLote"]));
        }
        controlerPadrao.dataBaseComunication.CloseConnection(controlerPadrao.Reader);

        controlerPadrao.Reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("produtor");
        while (controlerPadrao.Reader.Read())
        {
            controlerPadrao.ProdutorList.Add((string)(controlerPadrao.Reader["nome"]));
        }
        controlerPadrao.dataBaseComunication.CloseConnection(controlerPadrao.Reader);
        controlerPadrao.Reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("tipocafe");
        while (controlerPadrao.Reader.Read())
        {
            controlerPadrao.TipoCafeList.Add((string)(controlerPadrao.Reader["descricaotipocafe"]));
        }

        controlerPadrao.dataBaseComunication.CloseConnection(controlerPadrao.Reader);
        controlerPadrao.Reader = null;

    }
    public void Confirmar()
    {
        cadastro.MensagemErro.text = "";
        //verificação de erro        
        {
            cadastro.MensagemErro.color = new Color(255, 0, 0, 255);
            if (cadastro.FieldUID.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha a Tag RFID";
                return;
            }
            if (cadastro.DropDownLote.value == 0)
            {
                cadastro.MensagemErro.text = "Selecione um Lote";
                return;
            }
            if (cadastro.DropDownProdutor.value == 0)
            {
                cadastro.MensagemErro.text = "Selecione um Produtor";
                return;
            }
            if (cadastro.DropDownTipoCafe.value == 0)
            {
                cadastro.MensagemErro.text = "Selecione o tipo do café";
                return;
            }

            if (cadastro.FieldPeso.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha o campo Peso.";
                return;
            }
            if (int.Parse(cadastro.FieldPeso.text) < 0 || int.Parse(cadastro.FieldPeso.text) > 1500)
            {
                cadastro.MensagemErro.text = "Digite um peso valido";
                return;
            }
            if (cadastro.FieldQuantidadeSacasCafe.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha o campo Quantidade sacas de café.";
                return;
            }
            if (int.Parse(cadastro.FieldQuantidadeSacasCafe.text) < 0 || int.Parse(cadastro.FieldQuantidadeSacasCafe.text) >25)
            {
                cadastro.MensagemErro.text = "Digite uma quandidade de sacas de café valida";
                return;
            }
            if (cadastro.FieldDateDay.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha o campo data entrada dia.";
                return;
            }
            if (int.Parse(cadastro.FieldDateDay.text) < 0 || int.Parse(cadastro.FieldDateDay.text) > 31)
            {
                cadastro.MensagemErro.text = "Digite um dia valido";
                return;
            }
            if (cadastro.FieldDateMonth.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha o campo data entrada mês.";
                return;
            }
            if (int.Parse(cadastro.FieldDateMonth.text) < 0 || int.Parse(cadastro.FieldDateMonth.text) > 12)
            {
                cadastro.MensagemErro.text = "Digite um Mês valido";
                return;
            }
            if (cadastro.FieldDateYear.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha o campo data entrada ano.";
                return;
            }
            if (int.Parse(cadastro.FieldDateYear.text) < 2000 || int.Parse(cadastro.FieldDateYear.text) > DateTime.Now.Year + 1)
            {
                cadastro.MensagemErro.text = "Digite um ano valido";
                return;
            }
            if (cadastro.FieldLinha.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha a area de linha ou pressione o botão para verificar a melhor posição. ";
                return;
            }
            if (cadastro.FieldLinha.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha a area de Linha ou pressione o botão para verificar a melhor posição. ";
                return;
            }

            if (int.Parse(cadastro.FieldLinha.text) > controlerPadrao.enderecamentoArea.QtdLinhas || int.Parse(cadastro.FieldLinha.text) < 0)
            {
                cadastro.MensagemErro.text = "Linha Invalida";
                return;
            }
            if (cadastro.FieldColuna.text.Equals(""))
            {
                cadastro.MensagemErro.text = "Preencha a area de coluna ou pressione o botão para verificar a melhor posição. ";
                return;
            }
            if (int.Parse(cadastro.FieldLinha.text) > controlerPadrao.enderecamentoArea.QtdColunas || int.Parse(cadastro.FieldLinha.text) < 0)
            {
                cadastro.MensagemErro.text = "Coluna Invalida";
                return;
            }
            if (cadastro.DropDownNivel.value == 0)
            {
                cadastro.MensagemErro.text = "Selecione um nivel valido ou pressione o botão para verificar a melhor posição.";
                return;
            }
        }
        try{
            MySqlDataReader reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("tipocafe", cadastro.DropDownTipoCafe.captionText.text.ToString());

            int idTipoCafe = -1;
            while (reader.Read())
            {
                idTipoCafe = (int)reader["idTipoCafe"];
                break;
            }
            if (idTipoCafe == -1)
            {
                cadastro.MensagemErro.text = "Não foi possivel Ler o Tipo do cafe";
                return;
            }
            reader.Close();
            int area = int.Parse(cadastro.DropDownArea.captionText.text);
            int linha = int.Parse(cadastro.FieldLinha.text);
            int coluna = int.Parse(cadastro.FieldColuna.text);
            string tipoCafe = cadastro.DropDownTipoCafe.captionText.text;
            string nomeObjeto = "A" + ((area > 9) ? "" + area : "0" + area) +
                                "L" + ((linha > 9) ? "" + linha : "0" + linha) +
                                "C" + ((coluna > 9) ? "" + coluna : "0" + coluna) +
                                "N" + cadastro.DropDownNivel.captionText.text +
                                ((cadastro.IsLeftSide) ? "E" : "D");
            StringBuilder sb = new StringBuilder(nomeObjeto);
            if (int.Parse(cadastro.DropDownNivel.captionText.text) - 1 > 0) {
                sb.Remove(10, 1);
                sb.Insert(10, int.Parse(cadastro.DropDownNivel.captionText.text) - 1);
                string VerificacaoNivel = sb.ToString();
                if (!GameObject.Find(VerificacaoNivel))
                {
                    cadastro.MensagemErro.text = "Não é possivel inserir um BigBag sem ter um no nivel abaixo.";
                    return;
                }
            }
            if (GameObject.Find(nomeObjeto))
            {
                cadastro.MensagemErro.text = "Este Possição já esta sendo ocupada tente outra";
                return;
            }

            
            if (controlerPadrao.dataBaseComunication.selectAllInfoOfType("bigbag", cadastro.FieldUID.text.ToString()).HasRows)
            {
                cadastro.MensagemErro.text = "Esta tag RFID já esta sendo utilizada Insira outra";
                return;
            }
            reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("lote", cadastro.DropDownLote.captionText.text.ToString());

            int idLote =-1;
            while (reader.Read())
            {
                 idLote = (int)reader["idLote"];
                break;
            }
            if(idLote == -1)
            {
                cadastro.MensagemErro.text = "Não foi possivel Ler o Lote";
                return;
            }
            int dateTime = int.Parse(""+cadastro.FieldDateYear.text+ cadastro.FieldDateMonth.text+ cadastro.FieldDateDay.text);
            reader.Close();
            controlerPadrao.bigBagControlScript.InsertBigBag(idTipoCafe, nomeObjeto, int.Parse(cadastro.FieldUID.text), int.Parse(cadastro.FieldPeso.text), idLote, int.Parse(cadastro.FieldQuantidadeSacasCafe.text), dateTime);   
        cadastro.MensagemErro.color = new Color(0, 255, 0, 255);
        cadastro.MensagemErro.text = "Cadastro realizado com sucesso!";
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return;
        }
    }
    public void Limpar()
    {
        cadastro.DropDownArea.value = 0;
        cadastro.DropDownLote.value = 0;
        cadastro.DropDownNivel.value = 0;
        cadastro.DropDownProdutor.value = 0;
        cadastro.DropDownTipoCafe.value = 0;
        cadastro.FieldColuna.text = "";
        cadastro.FieldDateDay.text = "";
        cadastro.FieldDateMonth.text = "";
        cadastro.FieldDateSaidaDay.text = "";
        cadastro.FieldDateSaidaMonth.text = "";
        cadastro.FieldDateSaidaYear.text = "";
        cadastro.FieldDateYear.text = "";
        cadastro.FieldLinha.text = "";
        cadastro.FieldPeso.text = "";
        cadastro.FieldQuantidadeSacasCafe.text = "";
        cadastro.FieldUID.text = "";
        StartCoroutine(limparMensagem(1f));
    }
    public void LimparFind()
    {
        find.DropDownLote.value = 0;
        find.DropDownProdutor.value = 0;
        find.DropDownTipoCafe.value = 0;
        /*
          find.FieldDateDay.text = "";
          find.FieldDateMonth.text = "";
          find.FieldDateYear.text = "";
         */
        find.FieldUID.text = "";
        cadastro.MensagemErro.text = "";
    }
    public IEnumerator limparMensagem(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        cadastro.MensagemErro.text = "";
        alert.AlertMensage.text = "";
    }
    public void OpenViewCadastroBigBag()
    {
        Limpar();
        cadastro.MensagemErro.text = "";
        RefreshList();
        Refresh();
        controlerPadrao.FixedMenu.SetActive(false);
        controlerPadrao.MainPanel.SetActive(true);
        controlerPadrao.ViewNewBigBag.SetActive(true);
    }
    public void OpenViewFind()
    {

        RefreshList();
        Refresh();
        LimparFind();
        controlerPadrao.FixedMenu.SetActive(false);
        controlerPadrao.MainPanel.SetActive(true);
        controlerPadrao.ViewFind.SetActive(true);
    }
    public void OpenViewFound()
    {
        controlerPadrao.ViewFind.SetActive(false);
        controlerPadrao.ViewFind.SetActive(true);
    }
    public void OpenViewCadastroLote()
    {
        RefreshList();
        Refresh();
        cadastro.MensagemErro.text = "";
        controlerPadrao.ViewNewBigBag.SetActive(false);
        controlerPadrao.ViewNewLote.SetActive(true);

    }

    public void OpenViewCadastroProdutor()
    {
        cadastro.MensagemErro.text = "";
        controlerPadrao.ViewNewBigBag.SetActive(false);
        controlerPadrao.ViewNewProdutor.SetActive(true);
    }
    public void OpenViewCadastroTipoCafe()
    {
        cadastro.MensagemErro.text = "";
        controlerPadrao.ViewNewBigBag.SetActive(false);
        controlerPadrao.ViewNewTypeCoff.SetActive(true);
    }
    public void CloseViewCadastro()
    {
        RefreshList();
        Refresh();
        controlerPadrao.ViewNewTypeCoff.SetActive(false);
        controlerPadrao.ViewNewProdutor.SetActive(false);
        controlerPadrao.ViewNewLote.SetActive(false);
        controlerPadrao.ViewNewBigBag.SetActive(true);
      
   
    }
    public void CloseAllView()
    {
       
        alert.PanelAlert.SetActive(false);
        controlerPadrao.FixedMenu.SetActive(true);
        controlerPadrao.ViewNewBigBag.SetActive(false);
        controlerPadrao.ViewFind.SetActive(false);
        controlerPadrao.ViewNewLote.SetActive(false);
        controlerPadrao.ViewNewProdutor.SetActive(false);
        controlerPadrao.ViewNewTypeCoff.SetActive(false);
        controlerPadrao.ViewFound.SetActive(false);
        controlerPadrao.MainPanel.SetActive(false);
        controlerPadrao.ViewFoundParcial.SetActive(false);
        alert.AlertMensage.text = "";
        cadastro.MensagemErro.text = "";
    }

    public void CadastroProdutor()
    {
        cadastro.MensagemErro.color = new Color(255, 0, 0, 255);
        if (cadastro.FieldCadastroProdutor.text.Equals(""))
        {
            cadastro.MensagemErro.text = "Preencha o nome do produtor!";
            return;
        }
        try
        {
            if (controlerPadrao.dataBaseComunication.selectAllInfoOfType("produtor", cadastro.FieldCadastroProdutor.text).HasRows)
            {
                cadastro.MensagemErro.text = "Este nome de produtor já esta sendo utilizado!";
                cadastro.FieldCadastroProdutor.text = "";
                controlerPadrao.dataBaseComunication.CloseConnection();
                return;
            }

            controlerPadrao.bigBagControlScript.InsertTypeDescricao("produtor", cadastro.FieldCadastroProdutor.text);
            cadastro.MensagemErro.color = new Color(0, 255, 0, 255);
            cadastro.MensagemErro.text = "Cadastro realizado com sucesso!";
            StartCoroutine(limparMensagem(1f));
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return;
        }
    }

    public void CadastroLote()
    {
        cadastro.MensagemErro.color = new Color(255, 0, 0, 255);
        if (cadastro.DropdownProdutorCadastroLote.value == 0)
        {
            cadastro.MensagemErro.text = "Selecione um Produtor";
            return;
        }
        if (cadastro.FieldCadastroLote.text.Equals(""))
        {
            cadastro.MensagemErro.text = "Preencha o nome do produtor!";
            return;
        }
        try
        {
            MySqlDataReader reader = controlerPadrao.dataBaseComunication.selectAllInfoOfType("produtor", cadastro.DropdownProdutorCadastroLote.captionText.text.ToString());
            int idProdutor = -1;
            while (reader.Read())
            {
                idProdutor = (int)reader["idProdutor"];
                break;
            }
            if (idProdutor == -1)
            {
                cadastro.MensagemErro.text = "Não foi possivel encontrar o produtor.";
                reader.Close();
                return;
            }
            if (controlerPadrao.dataBaseComunication.selectAllInfoOfType("lote", cadastro.FieldCadastroLote.text).HasRows)
            {
                cadastro.MensagemErro.text = "Este lote já está sendo cadastrado!";
                cadastro.FieldCadastroLote.text = "";
                controlerPadrao.dataBaseComunication.CloseConnection();
                return;
            }

            controlerPadrao.bigBagControlScript.InsertLote(cadastro.FieldCadastroLote.text,idProdutor );
            cadastro.MensagemErro.color = new Color(0, 255, 0, 255);
            cadastro.MensagemErro.text = "Cadastro realizado com sucesso!";
            StartCoroutine(limparMensagem(1f));
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return;
        }
    }

    public void CadastroTipoCafe()
    {
        cadastro.MensagemErro.color = new Color(255, 0, 0, 255);
        if (cadastro.FieldCadastroTipoCafe.text.Equals(""))
        {
            cadastro.MensagemErro.text = "Preencha o Tipo do café!";
            return;
        }
        try
        {
            if (controlerPadrao.dataBaseComunication.selectAllInfoOfType("tipocafe", cadastro.FieldCadastroTipoCafe.text).HasRows)
            {
                cadastro.MensagemErro.text = "Este tipo de café já está cadastrado!";
                cadastro.FieldCadastroTipoCafe.text = "";
                controlerPadrao.dataBaseComunication.CloseConnection();
                return;
            }

            controlerPadrao.bigBagControlScript.InsertTypeDescricao("tipocafe", cadastro.FieldCadastroTipoCafe.text);
            cadastro.MensagemErro.color = new Color(0, 255, 0, 255);
            cadastro.MensagemErro.text = "Cadastro realizado com sucesso!";
            StartCoroutine(limparMensagem(1f));
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return;
        }
    }

}

