using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TestTask
{
    [System.Serializable]
    public class CellsData
    {
        public int[] Cells;

        public CellsData(GameObject[] GM)
        {
            Cells = new int[GM.Length];
            for (int i = 0; i < GM.Length; i++)
            {
                if (GM[i].transform.childCount >= 1)
                {
                    Cells[i] = GM[i].transform.GetChild(0).GetComponent<DragAndDropItem>().type;
                }
                else
                {
                    Cells[i] = 0;
                }
            }
        }
    }
}

