using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PatrolEnemy : MonoBehaviour {

    public Transform[] points;
    public float rateTime;
    private bool firstTime;
    private int destPoint = 0;
    public NavMeshAgent agent;
    Vector3 position;

    private PlayerController player;

    [HideInInspector]
    public int counterLifes;

    private Vector3 lastAgentVelocity;
    private NavMeshPath lastAgentPath;

    //private GameController gameController;

    private AudioSource soundEnemy;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        soundEnemy = GetComponent<AudioSource>();
        //gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        counterLifes = player.lifes;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        firstTime = true;
        rateTime = rateTime + 2;
        GotoNextPoint();
    }

    void Update()
    {       
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.4f)
        {
            GotoNextPoint();            
        }
        Debug.DrawLine(agent.transform.position, position, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        soundEnemy.Play();
        Destroy(other.gameObject);
        counterLifes--;
        
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

        StartCoroutine(TimePoint());
    }

    IEnumerator TimePoint()
    {
        agent.Stop();
        yield return new WaitForSeconds(rateTime);
        if (firstTime)
        {
            rateTime -= 2;
            firstTime = false;
        }
        SetPoint();
        agent.Resume();
    }

    void SetPoint()
    {
        destPoint = (destPoint + 1) % points.Length;
    }

}
