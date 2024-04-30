using UnityEngine;

public class SphereRaycast : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private float maxDistance = 10f;

    [SerializeField] private TurnManager turnManager;

    public bool ground;

    void Update()
    {
        // Definimos el origen del SphereCast como el centro de la esfera
        Vector3 origin = transform.position;
        
        // Dirección del SphereCast (por defecto, hacia adelante en el eje Z)
        Vector3 direction = transform.forward;
        
        RaycastHit hit; // Esta estructura contendrá información sobre lo que golpeó el SphereCast
        
        // Lanzamos el SphereCast
        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance))
        {
            ground = true;
            turnManager.players[turnManager.currentPlayerIndex].GetComponent<Rigidbody>().AddForce(turnManager.players[turnManager.currentPlayerIndex].GetComponent<Transform>().position.normalized * -1,ForceMode.Force);
        }
        else 
        {
            ground = false;
        }
    }
}
