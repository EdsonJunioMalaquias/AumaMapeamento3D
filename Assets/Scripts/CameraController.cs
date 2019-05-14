using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // velocidade da movimentação
    public float panSpeed = 0.5f;
    // velocidade do zoom
    public float zoomSpeed = 0.8f;

    // variavel para receber posicao original do mouse
    private Vector3 mouseOrigin;
    private bool isPanning;
    private bool isZooming;


    void Update()
    {

        //butao esquerdo
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // butao do meio
        if (Input.GetMouseButtonDown(2))
        {
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        //se soltar ele para de movimentar
        if (!Input.GetMouseButton(0)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;

        //move no eixo x e y
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * -1* panSpeed, pos.y *-1* panSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        //move no zoom
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }
    }
}