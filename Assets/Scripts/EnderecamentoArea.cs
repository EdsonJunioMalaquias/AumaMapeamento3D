using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderecamentoArea : MonoBehaviour
{
    private int numeroArea =1;
    [SerializeField]
    private int qtdLinhas  =6;
    [SerializeField]
    private int qtdColunas =4;
    public bool isLadoEsquerdo = true;
    public Vector3 posicoesInicialx;
    public Vector3 posicoesInicialy;
    public TextMesh textMeshGeradorLinha;
    public TextMesh textMeshGeradorColuna;

    public int NumeroArea { get => numeroArea; set => numeroArea = value; }
    public int QtdLinhas { get => qtdLinhas; set => qtdLinhas = value; }
    public int QtdColunas { get => qtdColunas; set => qtdColunas = value; }

    //public GameObject quadrado;

    // Start is called before the first frame update
    void Start()
    {
        AtributosText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AtributosText()
    {
        
        for (int j = 0; j < QtdLinhas; j++) { 
            if (j < 10)
                textMeshGeradorLinha.text = "A" + NumeroArea + " 0" + j + " " + (isLadoEsquerdo ? "E" : "D");
            else
                textMeshGeradorLinha.text = "A" + NumeroArea + " " + j + " " + (isLadoEsquerdo ? "E" : "D");

            Instantiate(textMeshGeradorLinha, new Vector3(posicoesInicialx.x, posicoesInicialx.y - (j * 2), 0), textMeshGeradorColuna.transform.rotation);

            for (int i = 0; i < QtdColunas; i++) {
                textMeshGeradorColuna.text = "" + (i + 1);

                Instantiate(textMeshGeradorColuna, new Vector3(posicoesInicialy.x + (i * 2), posicoesInicialy.y, 0), textMeshGeradorColuna.transform.rotation);

                //Instantiate(quadrado, new Vector3(posicoesInicialy.x + (j * 2), posicoesInicialy.y-2-(i*2),0), quadrado.transform.rotation);
            }
        }
    }
}
