using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGeneratorController : MonoBehaviour
{
    [SerializeField] private LayerMask floorLayerMask;
    [SerializeField] private Goal goalAsset;
    private Goal goal;
    private BoxCollider validArea;
    void Start()
    {
        validArea = GetComponent<BoxCollider>();
        goal = Instantiate(goalAsset,getNextGoalPosition(),Quaternion.identity,null);
    }

    public Vector3 getNextGoalPosition()
    {
        RaycastHit hit;
        if(Physics.Raycast(RandomPointInBounds(validArea.bounds),Vector3.down, out hit,float.MaxValue, floorLayerMask))
        {
            if (hit.normal.Equals(Vector3.up))
            {
                return hit.point;
            }
            Debug.LogWarning("Donde cay� el rayo estaba en pendiente");
        }

        Debug.LogWarning("No se encontr� donde poner una porter�a, lanzando otro rayo");
        return getNextGoalPosition();
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
