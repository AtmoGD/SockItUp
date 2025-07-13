using UnityEngine;

public class MarbleCollisionController : MonoBehaviour
{
    [SerializeField] private Locomotive locomotive = null;

    private Transform marbleTransform;

    void Update()
    {
        if (marbleTransform != null)
        {
            // If the marble is a child of the locomotive, update its position to match the locomotive
            marbleTransform.position = Vector3.Lerp(marbleTransform.position, transform.position, Time.deltaTime * 10f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble") && marbleTransform == null)
        {
            marbleTransform = other.transform; // Get the marble transformq
            marbleTransform.SetParent(transform); // Set marble as child of locomotive
            marbleTransform.GetComponent<Rigidbody>().isKinematic = true; // Make marble kinematic to avoid physics interactions

            locomotive.AddMarble(); // Notify the locomotive to add the marble
        }
    }
}
