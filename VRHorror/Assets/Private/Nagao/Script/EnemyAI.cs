using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgentをアタッチ
    public Transform[] patrolPoints; // 巡回ポイントの配列
    public Transform player; // プレイヤーのTransform
    [SerializeField]
    [Tooltip("プレイヤーのタグ")]
    private string Player_tag;

    private int currentPatrolIndex; // 現在の巡回ポイントのインデックス
    public bool isChasing; // プレイヤーを追跡しているか

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // NavMesh上にスナップ
        }
        else
        {
            Debug.LogError("キャラクターがNavMeshに乗っていません。");
        }
    }

    void Update()
    {
        if (isChasing)
        {
            // プレイヤーを追跡
            agent.SetDestination(player.position);
        }
        else
        {
            // 巡回処理
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

        // 次の巡回ポイントへ
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