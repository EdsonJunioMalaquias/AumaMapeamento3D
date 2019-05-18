using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FoundedBags : MonoBehaviour
{

    public List<ItemFounded> ItensFounded;
    public UIController uIController;
     
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cancelar()
    {

    }
    public void ResultParcial()
    {
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

            uIController.controlerPadrao.dataBaseComunication.selectBigBags(
                uID, lote, produtor, tipoCafe);
            RenderResultParcial();
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void RenderResultParcial()
    {
        uIController.ConfirmFindBags();
        
    }

    public void ResultCompleto()
    {
       
    }

}
