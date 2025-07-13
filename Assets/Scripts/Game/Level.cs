using System;
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

    [SerializeField] private float levelRotationSpeed = 1f;
    private List<PuzzleColor> collectedPuzzleColors = new List<PuzzleColor>();


    protected Sock sock;
    protected Game gm;

    private float rotaionAngleLeft = 0f;

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

        gm.TheButton.ResetButton();

        gm.Sock.transform.parent = transform;
    }

    public void DestroyLevel()
    {
        gm.Sock.transform.parent = null;
    }

    void Update()
    {

        if (Mathf.Abs(rotaionAngleLeft) > 0f)
        {
            float rotationThisFrame = levelRotationSpeed * Time.deltaTime;
            float rotationStep = Mathf.Min(rotationThisFrame, Mathf.Abs(rotaionAngleLeft));
            float direction = Mathf.Sign(rotaionAngleLeft);

            Quaternion deltaRotation = Quaternion.Euler(0f, 0f, rotationStep * direction);
            transform.rotation = deltaRotation * transform.rotation;

            gm.Sock.transform.rotation = Quaternion.Euler(0f, 0f, -rotationStep * direction) * gm.Sock.transform.rotation;

            rotaionAngleLeft -= rotationStep * direction;

            if (Mathf.Abs(rotaionAngleLeft) < 0.01f)
            {
                rotaionAngleLeft = 0f;
            }
        }
    }

    public void RotateLevel(float _dir, float _angle = 90)
    {
        if (_dir == 0f)
            return;

        rotaionAngleLeft += _angle * _dir;
    }

    public void CollectPuzzlePiece(PuzzleColor _color)
    {
        if (collectedPuzzleColors.Contains(_color))
        {
            Debug.LogWarning("Puzzle piece of color " + _color + " already collected.");
            return;
        }
        collectedPuzzleColors.Add(_color);
        Game.Manager.GameUIController.UpdatePuzzlePieceCount(collectedPuzzleColors);

        if (collectedPuzzleColors.Count == 4)
        {
            Game.Manager.EndLevel(true);
        }
    }
}
