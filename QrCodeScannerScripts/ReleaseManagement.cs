using UnityEngine;
using GoogleSheetsToUnity;
using Google.GData.Extensions;

public class ReleaseManagement : MonoBehaviour
{
    private string associatedSheet = "1C1zd5BJMwcOSQBgDgO8BZjzruSRQb_qYnlDcn8yjdJc";
    private string associatedWorksheet = "testSheet";
    public bool isRelease = false;
    public int placeAmount = -1;
    public AGV_ControlTower agvCtr;

    public void ReleaseGoogleSheets(string placeName)
    {
        if (isRelease == true)
            return;
        isRelease = true;
        GSTU_Search search = new GSTU_Search(associatedSheet, associatedWorksheet);
        SpreadsheetManager.Read(search, (GstuSpreadSheet ss) =>
        {
            int placeIndex = GetPlaceIndex(placeName);
                ss.rows[1][8].UpdateCellValue(associatedSheet, associatedWorksheet, "Release");
            for (int i = 2; i < 6; i++)
            {
                for (int j = 2; j < 7; j++)
                {
                    if (j != placeIndex)
                    {
                        string cellValue = ss.rows[i][j].value;
                        ss.rows[i][j + 7].UpdateCellValue(associatedSheet, associatedWorksheet, cellValue);
                    }
                    else if (j == placeIndex)
                    {
                        ss.rows[i][j + 7].UpdateCellValue(associatedSheet, associatedWorksheet, 0.ToString());
                    }
                }
            }
            placeAmount = int.Parse(ss.rows[8][placeIndex].value);
            Debug.Log(placeAmount.ToString());
            Debug.Log(placeName);
            agvCtr.AGVOrder(placeAmount, placeName);
        });
    }

    public void AmountRelocation(string placeName)
    {
        GSTU_Search search = new GSTU_Search(associatedSheet, associatedWorksheet);
        SpreadsheetManager.Read(search, (GstuSpreadSheet ss) =>
        {

            ss.rows[1][8].UpdateCellValue(associatedSheet, associatedWorksheet, "Release");
            if (ss.columns.ContainsKey(placeName))
            {
                for (int i = 2; i < 6; i++)
                {
                    for (int j = 2; j < 7; j++)
                    {
                        string cellValue = ss.rows[i][j + 7].value;
                        ss.rows[i][j].UpdateCellValue(associatedSheet, associatedWorksheet, cellValue);
                    }
                }
                ss.rows[1][8].UpdateCellValue(associatedSheet, associatedWorksheet, "notRelease");
            }
        });
        isRelease = false;
    }

    private int GetPlaceIndex(string placeName)
    {
        switch (placeName.ToLower())
        {
            case "seoul": return 2;
            case "daejeon": return 3;
            case "busan": return 4;
            case "gwangju": return 5;
            case "wonju": return 6;
            default: return -1; 
        }
    }
}
