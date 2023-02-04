using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    // Place your variables here





    // Essential variables
    #region Essential
    public string FileName;
    public JsonDateTime createdDate;
    public JsonDateTime lastSaved;

    [System.Serializable]
    public struct JsonDateTime
    {
        public long value;
        public static implicit operator System.DateTime(JsonDateTime jdt)
        {
            //Debug.Log("Converted to time");
            return System.DateTime.FromFileTime(jdt.value);
        }
        public static implicit operator JsonDateTime(System.DateTime dt)
        {
            //Debug.Log("Converted to JDT");
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTime();            
            return jdt;
        }

        public override string ToString()
        {
            return ((System.DateTime)this).ToString();
        }
    }
    #endregion
}
