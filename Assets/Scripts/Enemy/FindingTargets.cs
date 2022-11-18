using UnityEngine;

public class FindingTargets : MonoBehaviour
{
    [SerializeField] private float range;

    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    void Update()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
        foreach (var collider in colliderArray)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _unit.SetTarget(enemy);
            }
        }
    }
}
