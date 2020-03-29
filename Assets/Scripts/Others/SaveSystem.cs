using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace TestTask
{
    public static class SaveSystem
    {

        public static void SaveField(GameObject[] Cells)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Cells.task";
            FileStream stream = new FileStream(path, FileMode.Create);

            CellsData save = new CellsData(Cells);

            formatter.Serialize(stream, save);
            stream.Close();
        }

        public static CellsData LoadField()
        {
            string path = Application.persistentDataPath + "/Cells.task";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                CellsData save = formatter.Deserialize(stream) as CellsData;
                stream.Close();
                return save;
            }
            else
            {
                Debug.LogError("PathNotFound");
                return null;
            }
        }
    }
}

