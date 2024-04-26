using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGoalController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    ParticleSystem.ShapeModule _editableShape;
    private void Start()
    {
        _editableShape = particleSystem.shape;
    }

    public void generateParticles(Vector3 position, Vector3 rotation) { 
        print("Collision");
        _editableShape.position = transform.position - position - Vector3.forward * transform.localScale.z;
        _editableShape.rotation = rotation;

        particleSystem.Emit(100);
    }
}
