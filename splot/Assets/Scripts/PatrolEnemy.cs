using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PatrolEnemy : MonoBehaviour {

    public Transform[] points;
    //public float rateTime;
    //private bool firstTime;
    private int destPoint = 0;
    public NavMeshAgent agent;
    Vector3 position;

    private PlayerController player;

    [HideInInspector]
    //public int counterLifes;

    private Vector3 lastAgentVelocity;
    private NavMeshPath lastAgentPath;

    //private GameController gameController;

    private AudioSource soundEnemy;

    Animator anim;

    private GameObject expresion;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        soundEnemy = GetComponent<AudioSource>();
        //gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        expresion = transform.Find("Expresion").gameObject;
        //counterLifes = player.lifes;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;
        //firstTime = true;
        //rateTime = rateTime + 2;
        GotoNextPoint();

        expresion.SetActive(false);
        
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.4f)
        {

            GotoNextPoint();
        }

        expresion.transform.LookAt(Camera.main.transform.position);

        //if (agent.velocity.x != 0 || agent.velocity.z != 0)
        //{
        //    anim.SetBool("caminar", true);

        //}
        //else if (agent.velocity.x  == 0 || agent.velocity.z == 0)
        //{

        //    anim.SetBool("caminar", false);
        //}

        Debug.DrawLine(agent.transform.position, position, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        expresion.SetActive(true);
        
        soundEnemy.Play();
        StartCoroutine(TimeGroser());
        Destroy(other.gameObject);

        player.lifes--;
        
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

        //StartCoroutine(TimePoint());
        SetPoint();
    }

    //IEnumerator TimePoint()
    //{
    //    agent.Stop();
    //    yield return new WaitForSeconds(rateTime);
    //    if (firstTime)
    //    {
    //        rateTime = 0;
    //        firstTime = false;
    //    }
    //    SetPoint();
    //    agent.Resume();
    //}

    IEnumerator TimeGroser()
    {
        yield return new WaitForSeconds(1f);
        expresion.SetActive(false);
    }

    void SetPoint()
    {
        destPoint = (destPoint + 1) % points.Length;
    }

}
