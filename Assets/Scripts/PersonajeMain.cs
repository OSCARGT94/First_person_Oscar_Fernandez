using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersonajeMain : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float velocidad;
    [SerializeField] float gravedad;
    [Header("Dtección suelo")]
    [SerializeField] float radioDeteccion;
    [SerializeField] Transform pies;
    [SerializeField] LayerMask queEsSuelo;

    // Sirve tanto de gravedad como de salto.
    Vector3 movVertical;

    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
         Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        moveryRotar();
        AplicarGravedad();
        if (enSuelo())
        {
            //cancelamos la grabedad cuando aterrizamos.

            movVertical.y = 0;
        }
        
    }

    private void moveryRotar()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(x, 0, z).normalized;


        // Si el juegadopr toca las teclas.
        if (input.magnitude > 0)
        {

            //Calculo angulo al que rotarme.
            float anguloRotacion = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + Camera.main.transform.rotation.eulerAngles.y;

            //Orientación del cuerpo.
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            //Mi movimimiento a quedado rotado en base al angulo calculado.
            Vector3 movimiento =  Quaternion.Euler(0, anguloRotacion,0) * Vector3.forward;

            //Me muevo hacia donde esta orientado.
            controller.Move(movimiento * velocidad * Time.deltaTime);
        }
        



    }
    void AplicarGravedad()
    {
        //Mi velocidad vertical va en aumento a cieto factor por segundo.
        movVertical.y += gravedad * Time.deltaTime;
        controller.Move( movVertical * Time.deltaTime);
    }
    bool enSuelo()
    {
        //Tirar una esfera de detección en los pies con cierto radio.
        bool suelo = Physics.CheckSphere(pies.position, radioDeteccion, queEsSuelo);
        return suelo;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }
}
