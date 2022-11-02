using System.Collections.Generic;
using UnityEngine;

namespace UnitSystem
{
    public class UnitSelections : MonoBehaviour
    {
        public List<GameObject> unitList = new List<GameObject>();
        public List<GameObject> unitsSelected = new List<GameObject>();

        private static UnitSelections _instance;
        public static UnitSelections Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void ClickSelect(GameObject unitToAdd)
        {
            DeselectAll();
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }

        public void DragSelect(GameObject unitToAdd)
        {
            if (!unitsSelected.Contains(unitToAdd))
            {
                unitsSelected.Add(unitToAdd);
                unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
                unitToAdd.GetComponent<UnitMovement>().enabled = true;
            }
        }

        public void DeselectAll()
        {
            foreach (var unit in unitsSelected)
            {
                unit.GetComponent<UnitMovement>().enabled = false;
                unit.transform.GetChild(0).gameObject.SetActive(false);
            }
            unitsSelected.Clear();
        }
    }
}
