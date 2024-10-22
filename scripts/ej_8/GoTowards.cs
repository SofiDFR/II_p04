using UnityEngine;
using UnityEngine.AI;  // Necesario para usar NavMeshAgent

public class GoTowards : MonoBehaviour
{
    public Transform target;      // El objetivo al que el bicho seguirá
    public float speed = 3f;      // Velocidad inicial del bicho
    private NavMeshAgent agent;   // Referencia al NavMeshAgent

    void Start()
    {
        // Obtener el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;  // Establecer la velocidad inicial del agente
    }

    void Update()
    {
        if (target != null)
        {
            // Establecer el destino del agente para que se mueva hacia el objetivo
            agent.SetDestination(target.position);
        }
    }

    // Método para asignar un nuevo objetivo
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Método para aumentar la velocidad del NavMeshAgent
    public void IncreaseSpeed(float speedToAdd)
    {
        agent.speed += speedToAdd;  // Incrementar la velocidad del agente
    }
}
