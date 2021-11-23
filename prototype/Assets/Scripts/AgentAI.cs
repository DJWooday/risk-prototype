using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentAI : MonoBehaviour
{
    /// <summary>
    /// Enemy starts out wandering around, when it "sees" the player, it goes to chase,
    /// when it gets close enough, it starts blasting
    /// </summary>
    public enum AgentState { WANDER, CHASE, SHOOT}

    [SerializeField] private Transform player;
    [SerializeField] private AgentState state;
    private NavMeshAgent ai;

    [SerializeField] private float visionAngle = 50;

    [Header("Wander Settings")]
    [SerializeField] private float circleDist = 5;
    [SerializeField] private float circleRad = 3;
    [SerializeField] private float angle;
    [SerializeField] private Vector2 wanderRange = new Vector2(-1, 3);

    [Header("Shoot Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunTip;
    [SerializeField] private float gunForce;

    // Mapping states to their functions helps with simplifying code 
    private Dictionary<AgentState, Action> enter;
    private Dictionary<AgentState, Action> exit;
    private Dictionary<AgentState, Action> execute;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();

        enter = new Dictionary<AgentState, Action>() {
            { AgentState.WANDER, WanderEnter },
            { AgentState.CHASE, ChaseEnter },
            { AgentState.SHOOT, ShootEnter }
        };
        exit = new Dictionary<AgentState, Action>() {
            { AgentState.WANDER, WanderExit },
            { AgentState.CHASE, ChaseExit },
            { AgentState.SHOOT, ShootExit }
        };
        execute = new Dictionary<AgentState, Action>() {
            { AgentState.WANDER, WanderExecute },
            { AgentState.CHASE, ChaseExecute },
            { AgentState.SHOOT, ShootExecute }
        };

        StateTransition(AgentState.WANDER);
    }

    // Update is called once per frame
    void Update()
    {
        execute[state]();
    }

    // Transitions to the given state.
    // This could be extended with a dictionary of legal transitions to check.
    private void StateTransition(AgentState nextState) {
        exit[state]();
        state = nextState;
        enter[state]();
    }

    private bool CanSee() {
        Vector3 toPlayer = player.position - transform.position;
        float angToPlayer = Vector3.Angle(transform.forward, toPlayer);
        return angToPlayer < visionAngle && Vector3.Distance(transform.position, player.position) < 30;
    }

    // State Functions!
    // Each state should have an entry, exit, and execution function
    
    private void WanderEnter() {
        angle = 0;
    }
    private void WanderExit() {

    }
    private void WanderExecute() {
        // Find a point on a circle to navigate to.
        angle += UnityEngine.Random.Range(wanderRange.x, wanderRange.y);
        Vector3 posOnCircle = new Vector3(Mathf.Sin(angle) * circleRad, 0, Mathf.Cos(angle) * circleRad);
        Vector3 target = transform.position + transform.forward * circleDist + posOnCircle;

        ai.destination = target;
        transform.forward = ai.velocity.normalized;

        // Check if I see the player
        
        if (CanSee()) {
            StateTransition(AgentState.CHASE);
        }
    }

    private void ChaseEnter() {

    }
    private void ChaseExit() {

    }
    private void ChaseExecute() {
        ai.destination = player.position;
        transform.forward = ai.velocity.normalized;
        if (Vector3.Distance(transform.position, player.position) < 10)
            StateTransition(AgentState.SHOOT);
    }

    float shootAngle;
    private void ShootEnter() {
        InvokeRepeating("Shoot", 1, 1);
        shootAngle = 0;
    }
    private void ShootExit() {
        CancelInvoke("Shoot");
    }
    private void ShootExecute() {
        // Pick a spot around the player and navigate there
        shootAngle += UnityEngine.Random.Range(wanderRange.x, wanderRange.y);
        Vector3 circlePosition = new Vector3(10 * Mathf.Sin(shootAngle), 0, 10 * Mathf.Cos(shootAngle));
        Vector3 target = player.position + circlePosition;
        ai.destination = target;
        transform.LookAt(player);

        if (!CanSee())
            StateTransition(AgentState.WANDER);
    }
    private void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        bullet.GetComponent<Rigidbody>().AddForce((player.position - gunTip.position).normalized * gunForce, ForceMode.Impulse);
    }
}
