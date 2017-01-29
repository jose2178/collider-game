using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour {

    /*
     * Recordar: 
     * Implementar retraso corto de tiempo al INICIO de metodo InstantiatePoop
     * para que el poop no salga inmediatamente.
     * 
     * Reemplazar InvokeRepeating por otro metodo que funcione en base a la posicion y no al tiempo.
     * Se le puede a�adir tiempo al final de esta.
     * 
     */

    private Vector3 targetPosition;

    [SerializeField]
    private GameObject prefabPoop;

    [SerializeField]
    GameObject targetCollider;

    [SerializeField]
    Transform dogTail;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //targetPosition = new Vector3(10, 0 , 10);
        

        //Establece posicion aleatoria con tiempo de espera.
        InvokeRepeating("SetTargetPosition", 3, 5);
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        InstantiatePoop();
    }

    

    private void SetTargetPosition()
    {
        Vector3 position = RandomPosition();
        agent.SetDestination(position);

        InstantiateTargetCollider(position);
    }


    private Vector3 RandomPosition()
    {
        targetPosition = new Vector3(Random.Range(0,15), 0, Random.Range(0,15));

        return targetPosition;
    }

    private void InstantiateTargetCollider( Vector3 positionPoop)
    {
        Instantiate(targetCollider, targetPosition, Quaternion.identity);
    }

    private void  InstantiatePoop()
    {
        Instantiate(prefabPoop,new Vector3(dogTail.position.x, 0.5f, dogTail.position.z), Quaternion.identity);
    }


    
}
