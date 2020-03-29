using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    public class FieldControl : MonoBehaviour
    {
        public GameObject[] Cells;

        private void Start()
        {
            Cells = GameObject.FindGameObjectsWithTag("FieldCell");
        }

        public void ClearField()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                if (Cells[i].transform.childCount >= 1)
                {
                    Destroy(Cells[i].transform.GetChild(0).gameObject);
                }
            }
        }

        public void SaveCells()
        {
            SaveSystem.SaveField(Cells);
        }

        public void LoadCells()
        {
            ClearField();
            CellsData Load = SaveSystem.LoadField();

            for(int i = 0; i < Cells.Length; i++)
            {
                if(Load.Cells[i] > 0)
                {
                    GameObject instance = Instantiate(Resources.Load("Item"), Cells[i].transform) as GameObject;
                    instance.GetComponent<DragAndDropItem>().Data = Resources.Load("Data/" + (ItemType)Load.Cells[i]) as ItemData;
                    instance.transform.SetParent(Cells[i].transform);
                    instance.transform.localPosition = new Vector3(0, 0, 0);
                }
                else if(Cells[i].transform.childCount >= 1)
                {
                    Destroy(Cells[i].transform.GetChild(0).gameObject);
                }
            }
        }
    }
}

