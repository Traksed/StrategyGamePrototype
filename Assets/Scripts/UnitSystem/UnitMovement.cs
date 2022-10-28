using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private UnityEngine.Camera _myCam;
    private NavMeshAgent _myAgent;
    public LayerMask groud;

    private void Start()
    {
        _myCam = UnityEngine.Camera.main;
        _myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groud))
            {
                _myAgent.SetDestination(hit.point);
            }
        }
    }
}
