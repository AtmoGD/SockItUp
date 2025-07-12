using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelActions
{
    public ActionButtonAction action;
    public bool infiniteUse;
}

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startPositionTransform;
    [SerializeField] private List<LevelActions> characterActions = new List<LevelActions>();
    [SerializeField] private List<LevelActions> levelActions = new List<LevelActions>();
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
        sock.ChangeState(sock.SockIdle);
    }
}
