using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour
{
    
    [SerializeField] private GameObject actionCameraGameObject;

    [SerializeField] private GameObject monumentFoundTimelineGameObject;
    [SerializeField] private PlayableDirector monumentFoundTimeline;
    [SerializeField] private MonumentFoundVisual monumentFoundVisual;

    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        GameEventsHandler.Instance.OnMonumentFound += Instance_OnMonumentFound;

        monumentFoundTimeline.stopped += MonumentFoundTimeline_stopped;

        HideActionCamera();
    }


    private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGameObject.SetActive(false);
    }

    private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();

                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;

                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount;

                Vector3 actionCameraPosition =
                    shooterUnit.GetWorldPosition() +
                    cameraCharacterHeight +
                    shoulderOffset +
                    (shootDir * -1);

                actionCameraGameObject.transform.position = actionCameraPosition;
                actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
                
                ShowActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }


    private void Instance_OnMonumentFound(object sender, EventArgs e)
    {
        GameObject monument = sender as GameObject;
        //monumentFoundTimelineGameObject.transform.SetLocalPositionAndRotation(monument.transform.position, Quaternion.identity);
        //monumentFoundTimelineGameObject.SetActive(true);

        monumentFoundTimeline.gameObject.SetActive(true);
        monumentFoundTimeline.transform.SetLocalPositionAndRotation(monument.transform.position, Quaternion.identity);
        monumentFoundTimeline.Play();

        monumentFoundVisual.ShowMonumentFoundText();
    }

    private void MonumentFoundTimeline_stopped(PlayableDirector obj)
    {
        monumentFoundTimeline.gameObject.SetActive(false);

        monumentFoundVisual.HideMonumentFoundText();
    }
}
