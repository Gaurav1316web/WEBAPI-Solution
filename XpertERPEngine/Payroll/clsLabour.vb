'-Created by--[Pankaj kumar Chaudhary]-Against Ticket No-[BM00000001755]
Imports common
Imports System.Data.SqlClient

Public Class clsLabourWorkingSheet

#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Employee As String = Nothing
    Public Machine_No As String = Nothing
    Public Process_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Capacity As Integer = 0
    Public Run_Time As Integer = 0
    Public Quantity As Integer = 0
    Public In_One_Minute As Double = 0.0
    Public In_Run_Time As Integer = 0
    Public Comment As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsLabourWorkingSheet), ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(arr, isNewEntry, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsLabourWorkingSheet), ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Try
            Dim tempDocNo As String = ""
            Dim Count As Integer = 0
            Dim dtCurrent As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            For Each obj As clsLabourWorkingSheet In arr
                Dim coll As New Hashtable()
                If Count <= 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LABOUR_WORKING_SHEET WHERE Document_No='" + obj.Document_No + "'", trans)
                    If clsCommon.myLen(obj.Document_No) <= 0 Then
                        obj.Document_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  MAX(Document_No)  from TSPL_LABOUR_WORKING_SHEET", trans))
                        If clsCommon.myLen(obj.Document_No) <= 0 Then
                            obj.Document_No = "LWS00000000001"
                        Else
                            obj.Document_No = clsCommon.incval(obj.Document_No)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                        tempDocNo = obj.Document_No
                    Else
                        clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                        tempDocNo = obj.Document_No
                    End If
                Else
                    clsCommon.AddColumnsForChange(coll, "Document_No", tempDocNo)
                End If
                obj.Document_No = tempDocNo
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Employee", obj.Employee)
                clsCommon.AddColumnsForChange(coll, "Machine_No", obj.Machine_No)
                clsCommon.AddColumnsForChange(coll, "Process_Code", obj.Process_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
                clsCommon.AddColumnsForChange(coll, "Run_Time", obj.Run_Time)
                clsCommon.AddColumnsForChange(coll, "Quantity", obj.Quantity)
                clsCommon.AddColumnsForChange(coll, "In_One_Minute", obj.In_One_Minute)
                clsCommon.AddColumnsForChange(coll, "In_Run_Time", obj.In_Run_Time)
                clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LABOUR_WORKING_SHEET", OMInsertOrUpdate.Insert, "", trans)
                Count += 1
            Next
            'Dim frm As New FrmLabourWorkingSheet
            'frm.strDocumentNo = tempDocNo
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As List(Of clsLabourWorkingSheet)

        Dim obj As clsLabourWorkingSheet = Nothing
        Dim qry As String = "SELECT * from TSPL_LABOUR_WORKING_SHEET where 2=2"
        Dim whrClas As String = ""

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LABOUR_WORKING_SHEET.Document_No = (select MIN(Document_No) from TSPL_LABOUR_WORKING_SHEET where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_LABOUR_WORKING_SHEET.Document_No = (select Max(Document_No) from TSPL_LABOUR_WORKING_SHEET where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_LABOUR_WORKING_SHEET.Document_No = (select Min(Document_No) from TSPL_LABOUR_WORKING_SHEET where Document_No >'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_LABOUR_WORKING_SHEET.Document_No = (select Max(Document_No) from TSPL_LABOUR_WORKING_SHEET where Document_No <'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_LABOUR_WORKING_SHEET.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim arr As New List(Of clsLabourWorkingSheet)
        For Each dr As DataRow In dt.Rows
            obj = New clsLabourWorkingSheet()
            obj.Document_No = clsCommon.myCstr(dr("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dr("Document_Date"))
            obj.Employee = clsCommon.myCstr(dr("Employee"))
            obj.Machine_No = clsCommon.myCstr(dr("Machine_No"))
            obj.Process_Code = clsCommon.myCstr(dr("Process_Code"))
            obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            obj.Capacity = clsCommon.myCdbl(dr("Capacity"))
            obj.Run_Time = clsCommon.myCdbl(dr("Run_Time"))
            obj.Quantity = clsCommon.myCdbl(dr("Quantity"))
            obj.In_One_Minute = clsCommon.myCdbl(dr("In_One_Minute"))
            obj.In_Run_Time = clsCommon.myCdbl(dr("In_Run_Time"))
            obj.Comment = clsCommon.myCstr(dr("Comment"))
            arr.Add(obj)
        Next
        Return arr
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            isSaved = clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LABOUR_WORKING_SHEET WHERE Document_No='" + strDocNo + "'", trans)
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    ''To be Uncomment
    'Public Shared Sub PrintData(ByVal strAdjustmentNo As String, ByVal IsPreprinted As Boolean, ByVal IsEmpty As Boolean)
    '    Try
    '        Dim qry As String = "select TSPL_LABOUR_WORKING_SHEET.Document_No,TSPL_LABOUR_WORKING_SHEET.Document_Date,TSPL_LABOUR_WORKING_SHEET.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_WH_BREAKAGE_DETAIL.Unit_Code ,TSPL_WH_BREAKAGE_DETAIL.mrp,TSPL_WH_BREAKAGE_DETAIL.Item_Quantity  ,TSPL_WH_BREAKAGE_DETAIL.Breakage,TSPL_WH_BREAKAGE_DETAIL.Breakage_Qty,TSPL_WH_BREAKAGE_DETAIL.Leakage_Qty,TSPL_WH_BREAKAGE_DETAIL.Shortage_Qty "
    '        qry += ",TSPL_LABOUR_WORKING_SHEET.Created_By as [Created By] ,TSPL_LABOUR_WORKING_SHEET.Modified_By as [Modified By] from TSPL_LABOUR_WORKING_SHEET left outer join tspl_wh_breakage_detail on TSPL_LABOUR_WORKING_SHEET.Document_No=tspl_wh_breakage_detail.Document_No left outer join TSPL_LOCATION_MASTER on TSPL_LABOUR_WORKING_SHEET.Loc_code= TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_COMPANY_MASTER on TSPL_LABOUR_WORKING_SHEET.comp_code=TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_ITEM_MASTER on TSPL_WH_BREAKAGE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code where TSPL_LABOUR_WORKING_SHEET.Document_No='" + strAdjustmentNo + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        InventryViewer.funreport(dt, "crptWareHouseBreakage", "Ware Houes Breakage")

    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub
End Class
