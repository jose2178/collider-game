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

    Vector3 targetPosition;
    Vector3 posicionAleatoria;

    [SerializeField]
    GameObject prefabPoop;

    [SerializeField]
    Transform dogTail;

    NavMeshAgent agent;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        posicionAleatoria = agent.transform.position;
        
    }

    void Update()
    {
        
        if ((agent.transform.position.x == posicionAleatoria.x) && (agent.transform.position.z == posicionAleatoria.z))
        {
            posicionAleatoria = RandomPosition();
            StartCoroutine(TiempoEspera());           
        }
        
    }

    IEnumerator TiempoEspera()
    {        
        yield return new WaitForSeconds(4f);
        SetTargetPosition(posicionAleatoria);       
    }

    // Este metodo Asigna la posicion y mueve al Perro.
    private void SetTargetPosition(Vector3 pos)
    {        
        agent.SetDestination(pos);
        InstantiatePoop();
    }

    //Este metodo Genera un Vector3 alatorio.
    private Vector3 RandomPosition()
    {
        targetPosition = new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10));
        return targetPosition;
    }

    //Se instancia el prefab Poop en la posicion del dogTail adjunto al GameObject Dog.
    private void  InstantiatePoop()
    {
        Instantiate(prefabPoop,new Vector3(dogTail.position.x, 0.5f, dogTail.position.z), Quaternion.identity);
    }


}
