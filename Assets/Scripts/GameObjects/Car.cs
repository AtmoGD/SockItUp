using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private AnimationCurve driveCurve;
    [SerializeField] private float driveSpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform coliderTransform;
    [SerializeField] private Vector3 marbleOffset;
    private bool isDriving = false;
    private float driveTime = 0f;

    private Transform marbleTransform;
    void Update()
    {
        if (marbleTransform)
        {
            marbleTransform.position = Vector3.Lerp(marbleTransform.position, coliderTransform.position + marbleOffset, Time.deltaTime * 10f);
        }


        if (!isDriving)
            return;


        driveTime += Time.deltaTime;

        float curentSpeed = driveCurve.Evaluate(driveTime) * driveSpeed;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * curentSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            isDriving = false; // Stop driving when close to target
            driveTime = 0f; // Reset drive time
        }
    }

    void DriveToTarget()
    {
        if (isDriving)
            return;

        isDriving = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if tag is marble
        if (other.CompareTag("Marble"))
        {
            marbleTransform = other.transform; // Get the marble transform
            marbleTransform.SetParent(transform); // Set marble as child of car
            marbleTransform.GetComponent<Rigidbody>().isKinematic = true; // Make marble kinematic to avoid physics interactions
            DriveToTarget();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
        Gizmos.DrawSphere(target.position, 0.1f); // Draw a small sphere at the target position
    }
}
