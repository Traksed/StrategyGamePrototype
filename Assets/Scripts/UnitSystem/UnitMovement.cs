using UnityEngine;
using UnityEngine.AI;

namespace UnitSystem
{
    public class UnitMovement : MonoBehaviour
    {
        private UnityEngine.Camera _myCam;
        private NavMeshAgent _myAgent;
        public LayerMask ground;

        private void Start()
        {
            _myCam = UnityEngine.Camera.main;
            _myAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, ground))
                {
                    _myAgent.SetDestination(hit.point);
                }
            }
        }
    }
}
