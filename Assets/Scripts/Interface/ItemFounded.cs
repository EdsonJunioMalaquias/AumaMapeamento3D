
using UnityEngine;
using UnityEngine.UI;


public class ItemFounded : MonoBehaviour
{
    [SerializeField]
    private Button editButton, deleteButton, selectablebutton;
    [SerializeField]
    private Text renderLocalizacao,renderProdutor, renderLote;

    public Button EditButton { get => editButton; set => editButton = value; }
    public Button DeleteButton { get => deleteButton; set => deleteButton = value; }
    public Button Selectablebutton { get => selectablebutton; set => selectablebutton = value; }
    public Text RenderLocalizacao { get => renderLocalizacao; set => renderLocalizacao = value; }
    public Text RenderProdutor { get => renderProdutor; set => renderProdutor = value; }
    public Text RenderLote { get => renderLote; set => renderLote = value; }

    // Start is called before the first frame update
    void Start()
    {

}

    // Update is called once per frame
    void Update()
    {
        
    }
}
