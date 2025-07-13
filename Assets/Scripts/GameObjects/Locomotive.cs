using UnityEngine;

public class Locomotive : MonoBehaviour
{
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private int neededMarbles = 3;

    private bool isMoving = false;
    private float timeMoving = 0f;
    private int currentMarbles = 0;

    private void Update()
    {
        if (isMoving && targetTransform != null)
        {
            timeMoving += Time.deltaTime;
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        float currentSpeed = moveCurve.Evaluate(timeMoving) * speed;
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        transform.position += direction * currentSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetTransform.position) < 0.1f)
        {
            isMoving = false; // Stop moving when close to target
            timeMoving = 0f; // Reset time moving
            currentMarbles = 0; // Reset marble count after reaching the target
        }
    }

    public void AddMarble()
    {
        currentMarbles++;
        if (currentMarbles >= neededMarbles)
        {
            isMoving = true; // Start moving when enough marbles are collected
        }
    }

    void OnDrawGizmos()
    {
        if (targetTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, targetTransform.position);
            Gizmos.DrawSphere(targetTransform.position, 0.2f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}
