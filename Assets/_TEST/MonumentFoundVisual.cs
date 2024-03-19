using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonumentFoundVisual : MonoBehaviour
{
    [SerializeField] private GameObject monumentFoundTextContainer;
    [SerializeField] private TextMeshProUGUI monumentFoundText;

    // Start is called before the first frame update
    void Start()
    {
        monumentFoundText.text = "Monument Found";
    }

    public void ShowMonumentFoundText()
    {
        monumentFoundTextContainer.SetActive(true);
    }

    public void HideMonumentFoundText()
    {
        monumentFoundTextContainer.SetActive(false);
    }
}
