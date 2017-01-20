using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [SerializeField][Range(1, 300)]
    private float speed = 10;               // Que tan rapido se mueve el jugador.

    private Vector3 targetPosition;         // posicion objetivo.
    private bool isMoving;                  // Se alterna para comprobar si nos estamos moviendo o no.

    //const int LEFT_MOUSE_BUTTON = 0;        // Codigo visual del boton izquierdo del raton.

    public float tiempoMovimiento = 2f;

    public GameObject poop;
    public Transform instancePoop;

	// Use this for initialization
	void Start () {
        targetPosition = transform.position;    // Establece la posicion inicial del jugador.
        isMoving = false;                       // Establece el movimiento a falso.
        InvokeRepeating("SetTargetPosition", 3, 3);
    }
	
	// Update is called once per frame
	void Update () {
        // si el jugador hace click en la pantalla, encuentra a donde.
        //if (Input.GetMouseButton(LEFT_MOUSE_BUTTON))
        //{
        //    SetTargetPosition();
        //}
        //// if se sigue moviendo, entonces mueve al jugador.
        //if (isMoving)
        //{
        //    MovePlayer();
        //}

        

        //if(Time.time > tiempoMovimiento)
        //{
        //    SetTargetPosition();
            
        //}

        if (isMoving)
        {
            MovePlayer();
            
        }

	}

    /*
     * Establece la posicion a donde el jugador va a ir.
     */
    private void SetTargetPosition()
    {
        //Plane plane = new Plane(Vector3.up, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float point = 0f;

        //if(plane.Raycast(ray, out point))
        //{
        //    targetPosition = ray.GetPoint(point);
        //}
        targetPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), transform.position.y, UnityEngine.Random.Range(-10f, 10f));

        // Establece al jugador para que se mueva.
        isMoving = true;

        Invoke("InstantiatePoop", 3f);
    }


    /*
     * Mueve al jugador en la direccion correcta y tambien lo rota para mirar hacia el objetivo.
     * Cuando el jugador llega al objetivo, se detiene.
     */
    private void MovePlayer()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            isMoving = false;
        }

        

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }

    private void InstantiatePoop()
    {
        Instantiate(poop, targetPosition, Quaternion.identity);
    }
}
