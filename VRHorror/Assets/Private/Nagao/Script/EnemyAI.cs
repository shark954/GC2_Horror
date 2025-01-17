using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgent���A�^�b�`
    public Transform[] patrolPoints; // ����|�C���g�̔z��
    public Transform player; // �v���C���[��Transform
    [SerializeField]
    [Tooltip("�v���C���[�̃^�O")]
    private string Player_tag;

    private int currentPatrolIndex; // ���݂̏���|�C���g�̃C���f�b�N�X
    public bool isChasing; // �v���C���[��ǐՂ��Ă��邩

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // NavMesh��ɃX�i�b�v
        }
        else
        {
            Debug.LogError("�L�����N�^�[��NavMesh�ɏ���Ă��܂���B");
        }
    }

    void Update()
    {
        if (isChasing)
        {
            // �v���C���[��ǐ�
            agent.SetDestination(player.position);
        }
        else
        {
            // ���񏈗�
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextPatrolPoint();
            }
        }

        int Randomer;
        Randomer = Random.Range(0, 500);
        if(Randomer==499)
        {
            isChasing = true;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        // ���̏���|�C���g��
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Player_tag)
        {
            isChasing = true;
        }
    }
}