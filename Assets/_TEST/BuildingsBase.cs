using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingsBase : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshPro remainingConstructionText;

    private GridPosition gridPosition;

    private int remainingConstruction = 3;
    private Action onInteractionComplete;
    private float timer;
    private bool isActive;

    public bool IsConstructed {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        remainingConstructionText.text = "Remaining Construction: " + remainingConstruction.ToString();
        IsConstructed = false;

        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActive = false;
            onInteractionComplete();
        }
    }


    public void Interact(Action onInteractionComplete)
    {
        this.onInteractionComplete = onInteractionComplete;
        isActive = true;
        timer = .5f;


        UpdateContruction();
    }


    private void UpdateContruction()
    {
        if (IsConstructed)
            return;

        remainingConstruction--;
        remainingConstructionText.text = "Remaining Construction: " + remainingConstruction.ToString();

        if (remainingConstruction <= 0)
        {
            IsConstructed = true;
            remainingConstructionText.gameObject.SetActive(false);
        }

    }
}
