using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die = Animator.StringToHash("Die");

    [SerializeField] private LayerMask m_Enemy;
    [SerializeField] private LayerMask m_Ground;
    [SerializeField] private float max_health = 100;
    [SerializeField] private float current_health;
    [SerializeField] private float atk;
    [SerializeField] private float atk_range;

    // Component
    private Animator m_Anim;
    private NavMeshAgent m_Agent;

    // Enemy
    private Transform m_Target;


    public Enemy enemy;

    void Start()
    {
        current_health = max_health;
        m_Anim = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Left Click on Enemy
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, m_Enemy))
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(atk);

                }
                m_Target = hit.collider.transform;
                m_Anim.Play(Attack,-1,-1);
                

                m_Agent.SetDestination(m_Target.position);
            }
        }

        // Right Click on Ground
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_Ground))
            {
                m_Target = null;
                m_Anim.Play(Walk);

                m_Agent.SetDestination(hit.point);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        current_health -= damage;
        if (current_health <= 0)
        {
            current_health = 0;
       
        }
    }
   
}