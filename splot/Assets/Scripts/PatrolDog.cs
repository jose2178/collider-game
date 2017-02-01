using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolDog : MonoBehaviour {

    public Transform[] points;
    private int destPoint = 0;
    public NavMeshAgent agent;
    Vector3 position;
    private bool firstTime;
    public float rateTime;

    public GameObject poop;
    public Transform dogTail;
    private Vector3 lastAgentVelocity;
    private NavMeshPath lastAgentPath;

    Animator anim;
    int velocidad = Animator.StringToHash("Velocidad");

    private AudioSource sonidoCaca;
    private GameObject expresion;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        sonidoCaca = GetComponent<AudioSource>();
        expresion = transform.Find("Expresion").gameObject;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        expresion.SetActive(false);
        firstTime = true;
        rateTime = rateTime + 2;

        GotoNextPoint();

    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        
        // Set the agent to go to the currently selected destination.
        //agent.destination = points[destPoint].position;
        position = points[destPoint].position;
        agent.SetDestination(position);
        //Instantiate(prueba, pruebaPosicion.position, Quaternion.identity);
        
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        //destPoint = (destPoint + 1) % points.Length;
        StartCoroutine(TimePoint());
        

    }

    IEnumerator TimePoint()
    {
        agent.Stop();
        yield return new WaitForSeconds(rateTime);
        SetPoint();
        Instantiate(poop, dogTail.position, Quaternion.Euler(-90,0,0));
        sonidoCaca.Play();
        StartCoroutine(TimePuff());
        if (firstTime)
        {
            rateTime -= 2;
            firstTime = false;
        }
        agent.Resume();
    }

    IEnumerator TimePuff()
    {
        expresion.SetActive(true);
        yield return new WaitForSeconds(.5f);
        expresion.SetActive(false);
    }

    void SetPoint()
    {
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        expresion.transform.LookAt(Camera.main.transform.position);

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.4f)
        {
            GotoNextPoint();
        }

        if (agent.velocity.z < 0.5 && agent.velocity.z == 0)
        {
            anim.SetFloat(velocidad, -1f);
        }
        else
        {
            anim.SetFloat(velocidad, 1f);
        }


    }
}
