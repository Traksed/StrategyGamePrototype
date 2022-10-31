using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(unitPrefab, transform);
        }
    }
}
