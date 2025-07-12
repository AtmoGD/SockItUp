using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionData
{
    public ActionButtonAction action;
    public string actionName;
    public bool infiniteUse;
}

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startPositionTransform;

    [SerializeField] private List<ActionData> characterActions = new List<ActionData>();
    public List<ActionData> CharacterActions { get => characterActions; }
    [SerializeField] private List<ActionData> levelActions = new List<ActionData>();
    public List<ActionData> LevelActions { get => levelActions; }

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

        gm.GameUIController.InitLevel(this);
    }
}
