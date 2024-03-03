using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPrefab;

    private GameObject selectedBuilding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 mouseLocation = MouseWorld.GetPosition();
            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(mouseLocation);

            Instantiate(selectedBuilding, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
        }
    }

    public void UpdateSelectedBuilding(int index)
    {
        selectedBuilding = buildingPrefab[index];
    }
}
