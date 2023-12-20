using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> unitsPreviewList;
    [SerializeField] private int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < unitsPreviewList.Count; i++)
        {
            if (i + 1 <= health)
            {
                unitsPreviewList[i].SetActive(true);
            }
            else
            {
                unitsPreviewList[i].SetActive(false);
            }
        }
    }
}
