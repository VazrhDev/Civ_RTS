using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    public GameObject infoPanel;

    private static UnitSelection instance;
    public static UnitSelection Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();

        unitSelected.Add(unitToAdd);

        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

        infoPanel.SetActive(true);

        if (unitToAdd.TryGetComponent(out Unit unit))
        {
            unit.UpdateStats();
        }

    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            infoPanel.SetActive(true);

        }
        else
        {
            unitSelected.Remove(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);

        }
    }

    public void DragSelect(GameObject unitToAdd)
    {

    }

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            unit.transform.GetChild(0).gameObject.SetActive(false);
        }

        unitSelected.Clear();

        infoPanel.SetActive(false);
    }

    public void Deselect(GameObject unitToDeselect)
    {

    }
}
