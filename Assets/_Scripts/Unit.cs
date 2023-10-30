using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField] private LineRenderer renderer;
    [SerializeField] private NavMeshAgent agent;

    public HexRenderer currentNode;

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
                landTypeText.text = hex.temperature.ToString();

                currentNode = hex;
            }
        }

    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }

    protected void UpdateLineRenderer(List<HexTile> tiles)
    {
        if (renderer == null)
            return;

        List<Vector3> points = new List<Vector3>();
        foreach (HexTile tile in tiles)
        {
            points.Add(tile.transform.position + new Vector3(0, 0.5f, 0));

            renderer.positionCount = points.Count;
            renderer.SetPositions(points.ToArray());
        }
    }

    public void HandleMovement()
    {
        //if (currentPath == null || currentPath <= 1)
        //{
        //    nextTile = null;

        //    if (currentTile != null && currentPath > 0)
        //    {
        //        currentTile = currentPath[0];
        //        nextTile = currentTile;
        //    }

        //    gotPath = false;

        //    UpdateLineRenderer(new List<HexTile>());
        //}
    }

    public void MoveUnit(Vector3 destination)
    {
        //transform.Translate(destination);

        agent.SetDestination(destination);
    }
}
