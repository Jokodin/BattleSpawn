using UnityEngine;

public class Unit : PlayerObject {

    public UnityEngine.AI.NavMeshAgent agent;
    public bool isGrounded;
    public float moveSpeed;
    protected Animator animator;


    new protected virtual void Start () {
        base.Start();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        animator = GetComponent<Animator>();
        animator.SetFloat("moveSpeed", moveSpeed);
        agent.speed = moveSpeed;

    }

    protected virtual void FixedUpdate()
    {

        if (agent.isOnNavMesh)
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }
    }
}
