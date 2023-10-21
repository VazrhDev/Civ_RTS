using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float health = 10;
    [SerializeField] private float attack = 1;
    [SerializeField] private float defence = 1;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenceText;
    [SerializeField] private TMP_Text landTypeText;

    public LayerMask hitLayer;

    // Start is called before the first frame update
    void Start()
    {
        UnitSelection.Instance.unitList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStats()
    {
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
        defenceText.text = defence.ToString();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up * -1f, out hit, 10f, hitLayer))
        {

            if (hit.transform.TryGetComponent(out HexRenderer hex))
            {
                Debug.Log(hex.colorNum);

                switch (hex.colorNum)
                {
                    case 3:
                        landTypeText.text = "C";

                        break;

                    case 4:
                        landTypeText.text = "W";

                        break;

                    default:
                        landTypeText.text = "N";

                        break;
                }
            }
        }

    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent(out HexRenderer hex))
    //    {
    //        switch (hex.colorNum)
    //        {

    //            case 2:
    //                landTypeText.text = "C";

    //                break;

    //            case 3:
    //                landTypeText.text = "W";

    //                break;

    //            default:
    //                landTypeText.text = "N";

    //                break;
    //        }
    //    }
    //}


}
