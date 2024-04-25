using UnityEngine;

public class GoalGeneratorController : MonoBehaviour
{
    [SerializeField] private GoalController goalController;
    [SerializeField] private LayerMask floorLayerMask;
    [SerializeField] private BoxCollider generateArea;

    void Start()
    {
        changeGoalPosition();
    }

    public void changeGoalPosition()
    {
        goalController.transform.position = getNextGoalPosition();
    }
    private Vector3 getNextGoalPosition()
    {
        RaycastHit hit;
        do
        {
            Physics.Raycast(RandomPointInBounds(generateArea.bounds), Vector3.down, out hit, float.MaxValue, floorLayerMask);
        }while (hit.Equals(null) || !hit.normal.Equals(Vector3.up));

        return hit.point;
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
