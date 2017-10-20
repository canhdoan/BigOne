﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _AgentController : MonoBehaviour {

    public List<NavPoint> wayPointList;
    public Transform eyes;

    [HideInInspector] public Transform[] lookAtPositions;
    [HideInInspector] public Transform lookAtPositionCentral;
    [HideInInspector] public Transform[] wayPointListTransform;
    [HideInInspector] public bool hasHeardPlayer = false;
    [HideInInspector] public bool hasSeenPlayer = false;

    [HideInInspector] public bool isPlayerInSight = false;
    [HideInInspector] public bool isSuspicious = false, isAlarmed = false;

    [HideInInspector] public int nextWayPoint = 0;
    [HideInInspector] public int checkingWayPoint = 0;
    [HideInInspector] public bool isCheckingNavPoint = false;
    [HideInInspector] public float checkNavPointTime = 0f, navPointTimer = 0f;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public NavMeshAgent m_NavMeshAgent;
    [HideInInspector] public MyAgentStats agentStats;
    [HideInInspector] public float sightPercentage = 0f;

    [HideInInspector] public Animator m_Animator;

    [HideInInspector] public GuardSaveComponent m_SaveComponent;

    [Space(10)]
    [Header("Agent States Parameters")]
    public MyAgentStats patrolStats;
    public MyAgentStats checkForPositionStats;
    public MyAgentStats checkNavPointStats;
    public MyAgentStats chaseStats;

    private MyAgentStats loadingStats;
    private PerceptionBar perceptionBar;

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        GameObject[] lookAtPositionsObj = GameObject.FindGameObjectsWithTag("LookAtPosition");
        lookAtPositions = new Transform[lookAtPositionsObj.Length];
        for (int i = 0; i < lookAtPositionsObj.Length; i++)
        {
            lookAtPositions[i] = lookAtPositionsObj[i].transform;
        }
        lookAtPositionCentral = GameObject.FindGameObjectsWithTag("LookAtPositionCentral")[0].transform;
        UpdateStats(patrolStats);
        perceptionBar = GetComponentInChildren<PerceptionBar>();
        wayPointListTransform = new Transform[wayPointList.Count];
        for (int i = 0; i < wayPointListTransform.Length; i++)
        {
            wayPointListTransform[i] = wayPointList[i].transform;
        }

        m_SaveComponent = gameObject.GetComponent<GuardSaveComponent>();
        m_Animator = GetComponent<Animator>();
    }

    public void LoadNavmeshStats()
    {
        m_NavMeshAgent.speed = agentStats.speed;
        m_NavMeshAgent.angularSpeed = agentStats.angularSpeed;
        m_NavMeshAgent.acceleration = agentStats.acceleration;
        m_NavMeshAgent.stoppingDistance = agentStats.stoppingDistance;
    }

    public void UpdateStats(MyAgentStats stats)
    {
        agentStats = stats;
        LoadNavmeshStats();
    }

    public void UpdateStats(string statsName)
    {
        switch (statsName)
        {
            case "patrol":
                agentStats = patrolStats;
                break;
            case "checkForNoise":
                agentStats = patrolStats;
                break;
            case "checkNavPoint":
                agentStats = checkNavPointStats;
                break;
            case "chase":
                agentStats = chaseStats;
                break;
            default:
                break;
        }

        LoadNavmeshStats();

    }

    void Update()
    {
        bool noRaycastHitting = true;
        if (isPlayerInSight && sightPercentage < 100f)
        {
            Vector3 direction;

            for (int i = 0; i < lookAtPositions.Length; i++)
            {
                direction = (lookAtPositions[i].position - transform.position).normalized;

                if (Physics.Raycast(eyes.position, direction))
                {
                    sightPercentage += agentStats.fillingSpeed * Time.deltaTime;
                    noRaycastHitting = false;
                }
            }

            direction = (lookAtPositionCentral.position - transform.position).normalized;
            if (Physics.Raycast(eyes.position, direction))
            {
                sightPercentage += agentStats.fillingSpeed * agentStats.torsoMultiplier * Time.deltaTime;
                noRaycastHitting = false;
            }
        }

        if (noRaycastHitting && sightPercentage > 0f)
        {
            sightPercentage -= agentStats.fillingSpeed * agentStats.noSeeMultiplier * Time.deltaTime;
        }

        sightPercentage = Mathf.Clamp(sightPercentage, 0f, 100f);
        perceptionBar.SetFillingPerc(sightPercentage);

    }

}
