
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemFounded :  MonoBehaviour
{
    [SerializeField]

    private BigBagScript bigBagScript = null;
    [SerializeField]
    private Text renderLocalizacao, renderProdutor, renderLote;
    [SerializeField]
    public Button btn;
    public Text RenderLocalizacao { get => renderLocalizacao; set => renderLocalizacao = value; }
    public Text RenderProdutor { get => renderProdutor; set => renderProdutor = value; }
    public Text RenderLote { get => renderLote; set => renderLote = value; }
    public BigBagScript BigBagScript { get => bigBagScript; set => bigBagScript = value; }

    public ItemFounded(BigBagScript bigBagScript, string renderLocalizacao, string renderProdutor, string renderLote)
    {
        Debug.Log(bigBagScript + "\n" + renderLocalizacao + "\n" + renderProdutor + "\n" + renderLote);
        this.BigBagScript = bigBagScript;

    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Renderizar(BigBagScript bigBagScript,string localizacao, string produtor,string lote)
    {
        this.bigBagScript = bigBagScript;
        this.renderLocalizacao.text = localizacao;
        this.renderProdutor.text = produtor;
        this.renderLote.text = lote;
    }
    public void Clear()
    {
        this.renderLocalizacao.text = "";
        this.renderProdutor.text = "";
        this.renderLote.text = "";
    }
    public void SelectBigBag()
    {
        if (!FoundedBags.LastBigBagSelected.Equals("")) {
            GameObject.Find(FoundedBags.LastBigBagSelected).GetComponent<BigBagScript>().ApplyArrayMaterial();
        }
        if (this.renderLocalizacao.text.Equals(""))
        {
            return;
        }
        FoundedBags.LastBigBagSelected = this.renderLocalizacao.text;
        GameObject objetoSelecionado = GameObject.Find(this.renderLocalizacao.text);
        objetoSelecionado.GetComponent<BigBagScript>().ApplySelectableMaterial();


    }




}
