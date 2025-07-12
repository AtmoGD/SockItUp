using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class TheButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text buttonText;
    private RectTransform rect;
    private bool isMoving = false;
    private float pointerMoveThreshold = 5f;
    private Vector2 pointerStartPosition;
    private bool movedButton = false;

    private ActionButton currentActionButton;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        CheckActionButton();
    }

    void Update()
    {
        if (!isMoving || Vector2.Distance(pointerStartPosition, Input.mousePosition) < pointerMoveThreshold)
            return;

        MoveButton();
        CheckActionButton();
    }

    private void MoveButton()
    {
        Vector3 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
        movedButton = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isMoving = true;
        pointerStartPosition = Input.mousePosition;
        movedButton = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMoving = false;
    }

    public void ButtonPressed()
    {
        if (movedButton || !currentActionButton)
            return;

        currentActionButton.DoAction();
    }

    private void CheckActionButton()
    {
        List<ActionButton> actionButtons = new List<ActionButton>(FindObjectsByType<ActionButton>(FindObjectsSortMode.None));

        foreach (ActionButton actionButton in actionButtons)
        {
            if (actionButton == null || actionButton.GetRect() == null)
                continue;

            if (isOverlapping(actionButton.GetRect()))
            {
                if (currentActionButton != actionButton)
                {
                    currentActionButton?.StopOverlapping();
                    currentActionButton = actionButton;
                    currentActionButton.StartOverlapping();
                    BecomeButton();
                }

                return; // Exit after the first overlapping action button is found
            }
            else if (currentActionButton)
            {
                currentActionButton.StopOverlapping();
                currentActionButton = null;
                buttonText.text = "No Action";
            }
        }
    }

    private bool isOverlapping(RectTransform otherRect)
    {
        if (otherRect == null)
            return false;

        Vector3[] thisCorners = new Vector3[4];
        Vector3[] otherCorners = new Vector3[4];

        rect.GetWorldCorners(thisCorners);
        otherRect.GetWorldCorners(otherCorners);

        Rect thisRect = new Rect(thisCorners[0], thisCorners[2] - thisCorners[0]);
        Rect otherRectWorld = new Rect(otherCorners[0], otherCorners[2] - otherCorners[0]);

        return thisRect.Overlaps(otherRectWorld);
    }

    private void BecomeButton()
    {
        if (currentActionButton == null)
            return;


        buttonText.text = currentActionButton.GetActionName();
    }
}
