using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform startPositionTransform;

    protected Sock sock;
    protected Game gm;

    public virtual void InitLevel(Sock _sock, Game _gameManager)
    {
        sock = _sock;
        gm = _gameManager;

        if (_sock == null)
        {
            Debug.LogError("Sock is null in Level.InitLevel");
            return;
        }
        if (gm == null)
        {
            Debug.LogError("GameManager is null in Level.InitLevel");
            return;
        }

        sock.transform.position = startPositionTransform.position;
    }
}
