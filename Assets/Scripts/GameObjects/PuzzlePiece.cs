using UnityEngine;

public enum PuzzleColor
{
    Red,
    Green,
    Blue,
    Yellow
}

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private PuzzleColor color = PuzzleColor.Red;

    private bool isCollected = false;

    void OnTriggerEnter(Collider other)
    {
        if (isCollected)
            return;

        Sock sock = other.GetComponent<Sock>();
        if (sock != null)
        {
            isCollected = true;
            animator.SetTrigger("Collect");
            Game.Manager.CurrentLevel.CollectPuzzlePiece(color);
            Destroy(gameObject, 1f);
        }
    }
}
