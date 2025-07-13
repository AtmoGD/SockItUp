using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class TheButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject innerButton;
    [SerializeField] private float snapSpeed = 10f;
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
        if (!isMoving && currentActionButton)
        {
            SnapToButton();
        }

        if (isMoving && Vector2.Distance(pointerStartPosition, Input.mousePosition) > pointerMoveThreshold)
        {
            MoveButton();
            CheckActionButton();
        }

        SetButtonActive(currentActionButton);
    }

    public void ResetButton()
    {
        isMoving = false;
        movedButton = false;
        pointerStartPosition = Vector2.zero;
        currentActionButton?.StopOverlapping();
        currentActionButton = null;
        SetButtonActive(false);
    }

    private void SnapToButton()
    {
        if (currentActionButton == null || currentActionButton.GetRect() == null)
            return;

        Vector3 targetPosition = currentActionButton.GetRect().position;
        Vector3 currentPosition = rect.position;

        rect.position = Vector3.Lerp(currentPosition, targetPosition, snapSpeed * Time.deltaTime);

        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
            rect.position = targetPosition;
    }

    private void SetButtonActive(bool active)
    {
        innerButton.SetActive(active);
        buttonText.gameObject.SetActive(active);
        if (active)
        {
            buttonText.text = currentActionButton.GetActionName();
        }
        else
        {
            buttonText.text = "";
        }
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

        bool foundActionButton = false;

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
                    foundActionButton = true;
                    // SetButtonActive(true);
                }

                return; // Exit after the first overlapping action button is found
            }

        }

        if (!foundActionButton && currentActionButton)
        {
            currentActionButton.StopOverlapping();
            currentActionButton = null;
            // SetButtonActive(false);
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
}
