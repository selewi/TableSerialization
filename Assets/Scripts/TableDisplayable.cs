using UnityEngine;
using UnityEngine.UI;

public class TableDisplayable : MonoBehaviour 
{
    [SerializeField] private Text _headerDisplayablePrefab = null;
    [SerializeField] private Text _dataDisplayablePrefab = null;
    [Space]
    [SerializeField] private Text _titleText = null;
    [SerializeField] private Transform _headerGroupTransform = null;
    [SerializeField] private Transform _dataGroupTransform = null;
    [Space]
    [SerializeField] private float _fixedSpaceBetweenColumns = 10f;
    [SerializeField] private float _fixedSpaceBetweenRows = 5f;

    public void SetTitle (string newTitle) 
    {
        _titleText.text = newTitle;
    }

    public void SetColumnHeaders (string[] headerTexts) 
    {
        for (int i = 0; i < headerTexts.Length; i++) 
        {
            Text headerInstance = Instantiate (_headerDisplayablePrefab, _headerGroupTransform);
            headerInstance.TryGetComponent (out RectTransform headerInstanceRectTransform);

            headerInstance.text = headerTexts[i];

            headerInstanceRectTransform.anchorMin = Vector2.up;
            headerInstanceRectTransform.anchorMax = Vector2.up;
            headerInstanceRectTransform.pivot = Vector2.up;
            headerInstanceRectTransform.anchoredPosition =
                new Vector2
                (
                    _fixedSpaceBetweenColumns / 2 + _fixedSpaceBetweenColumns * i,
                    0
                );
        }
    }

    public void SetRowData (string[] rowDataTexts, int rowIndex) 
    {
        for (int i = 0; i < rowDataTexts.Length; i++) {
            Text dataValueInstance = Instantiate (_dataDisplayablePrefab, _dataGroupTransform);
            dataValueInstance.TryGetComponent (out RectTransform headerInstanceRectTransform);

            dataValueInstance.text = rowDataTexts[i];

            headerInstanceRectTransform.anchorMin = Vector2.up;
            headerInstanceRectTransform.anchorMax = Vector2.up;
            headerInstanceRectTransform.pivot = Vector2.up;
            headerInstanceRectTransform.anchoredPosition =
                new Vector2
                (
                    _fixedSpaceBetweenColumns / 2 + _fixedSpaceBetweenColumns * i,
                    -(_fixedSpaceBetweenRows / 2 * rowIndex)
                );
        }
    }

    public void ClearHeader () {
        for (int i = 0; i < _headerGroupTransform.childCount; i++) 
        {
            Destroy (_headerGroupTransform.GetChild (i).gameObject);
        }
    }

    public void EmptyRows () {
        for (int i = 0; i < _dataGroupTransform.childCount; i++) 
        {
            Destroy (_dataGroupTransform.GetChild (i).gameObject);
        }
    }

}
