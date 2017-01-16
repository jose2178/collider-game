using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]

public class PlayerMovementNavMesh : MonoBehaviour {

    private Vector3 targetPosition;

    const int LEFT_MOUSE_BUTTON = 0;

    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
	// Use this for initialization
	void Start () {
        targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(LEFT_MOUSE_BUTTON))
        {
            SetTargetPosition();
        }

        MovePlayer();
	}

    private void SetTargetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            targetPosition = ray.GetPoint(point);
        }

    }

    private void MovePlayer()
    {
        agent.SetDestination(targetPosition);

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }
}
