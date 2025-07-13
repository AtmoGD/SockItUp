using UnityEngine;

public class Drum : MonoBehaviour
{
    [SerializeField] private float drumForce = 5f;
    [SerializeField] private float drumTimeout = 0.5f;

    private float lastDrumTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastDrumTime < drumTimeout)
        {
            return; // Ignore if the drum was used recently
        }

        Sock sock = other.GetComponent<Sock>();
        if (sock != null)
        {
            sock.SetJumpModifier(drumForce);
            sock.ChangeState(sock.SockJump);
            lastDrumTime = Time.time;
        }
    }
}
