using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniJSON;
using UnityEngine;

[RequireComponent (typeof (TableDisplayable))]
public class TableController : MonoBehaviour
{
    [SerializeField] private string _jsonFileName = "";
    private TableDisplayable _displayable = null;

    public string Title { get; private set; }
    public List<Dictionary<string, string>> RowsData { get; private set; }

    private string JsonFilePath => Path.Combine (Application.streamingAssetsPath, _jsonFileName);

    private void Awake () 
    {
        TryGetComponent (out _displayable);
        RowsData = new List<Dictionary<string, string>> ();
    }

    private void Start () 
    {
        UpdateData ();
    }

    public void UpdateData () 
    {
        Dictionary<string, object> deserializedTableData = (Dictionary<string, object>)Json.Deserialize (FileUtils.Read (JsonFilePath));

        // Title
        Title = (string)deserializedTableData["Title"];
        _displayable.SetTitle (Title);

        // Column headers
        List<object> columnHeaders = (List<object>)deserializedTableData["ColumnHeaders"];
        string[] columnHeaderKeys = new string[columnHeaders.Count];

        _displayable.ClearHeader ();

        for (int i = 0; i < columnHeaderKeys.Length; i++) 
        {
            columnHeaderKeys[i] = (string) columnHeaders[i];
        }

        _displayable.SetColumnHeaders (columnHeaderKeys);

        // Rows
        List<object> rowsData = (List<object>)deserializedTableData["Data"];

        RowsData.Clear ();
        _displayable.EmptyRows ();

        for (int i = 0; i < rowsData.Count; i++) 
        {
            Dictionary<string, object> rowData = (Dictionary<string, object>) rowsData[i];
            Dictionary<string, string> parsedRowData = new Dictionary<string, string> ();

            foreach (string columnKey in columnHeaderKeys) 
            {
                // Iterate the row data in case any key is missing in the .json file
                rowData.TryGetValue (columnKey, out object value);
                parsedRowData.Add (columnKey, (string) value);
            }

            _displayable.SetRowData (parsedRowData.Values.ToArray(), i);
            RowsData.Add (parsedRowData);
        }
    }

}