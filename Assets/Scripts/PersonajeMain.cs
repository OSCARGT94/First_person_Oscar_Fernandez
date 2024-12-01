using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMain : MonoBehaviour
{
    [SerializeField] float velocidad;

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
    }

    private void moveryRotar()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(x, 0, z).normalized;

        //Calculo angulo al que rotarme.
        float anguloRotacion = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + Camera.main.transform.rotation.eulerAngles.y;

        //Orientación del cuerpo.
        transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

        // Si el juegadopr toca las teclas.
        if (input.magnitude > 0)
        {
           

            //Mi movimimiento a quedado rotado en base al angulo calculado.
            Vector3 movimiento =  Quaternion.Euler(0, anguloRotacion,0) * Vector3.forward;

            //Me muevo hacia donde esta orientado.
            controller.Move(movimiento * velocidad * Time.deltaTime);
        }




    }
}
