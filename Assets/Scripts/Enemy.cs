using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die = Animator.StringToHash("Die");

    // Component
    private Animator m_Anim;
    private NavMeshAgent m_Agent;

    //public component
    public Vector3 start_position;

    // Enemy
    private Transform m_Target;

    [SerializeField] private float max_health = 100;
    [SerializeField] private float current_health;
    [SerializeField] private float atk;
    [SerializeField] private float atk_range;
    [SerializeField] private float range;

    void Start()
    {
        current_health = max_health;
        m_Anim = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindObjectOfType<Player>().transform;
    }
    private void Update()
    {
        var distance = Vector3.Distance(m_Target.position, start_position);
        if (distance < range)
        {
            m_Agent.enabled = true;
            m_Agent.destination = m_Target.position;
        }
        else
        {
            
            m_Agent.SetDestination(start_position);
        }
       

    }
    public void TakeDamage(float damage)
    {
        current_health -= damage;
        if (current_health <= 0)
        {
            current_health = 0;
            Dead();
        }
    }
    public void Dead()
    {
        m_Anim.SetTrigger(Die);
        m_Agent.isStopped = true;
        Destroy(this);
    }
}