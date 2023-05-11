Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class ClsServiceVisitDetails
#Region "Variables"
    Public Service_Visit_Code As String = String.Empty
    Public Service_Visit_Date As String = String.Empty
    Public Status As String = String.Empty
    Public Remarks As String = String.Empty
    Public Service_Enquiry_Code As String = String.Empty
    Public Location_Code As String = String.Empty
    Public Engineer_Code As String = String.Empty
    Public Service_Place As String = String.Empty
    Public SVR_No As String = String.Empty
    Public SVR_Date As String = String.Empty
    Public DocName As String = String.Empty
    Public DOCUMENT_FILE As Byte()

    'Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public ObjMList As List(Of ClsServiceVisitMainItemDetail) = Nothing
    Dim objMDetail As New ClsServiceVisitMainItemDetail()
    Public ObjCList As List(Of ClsServiceVisitChildItemDetail) = Nothing
    Dim objCDetail As New ClsServiceVisitChildItemDetail()
#End Region
    ''
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT Service_Visit_Code AS [Code],CONVERT (VARCHAR,Service_Visit_Date ,103) AS [Document Date],CASE Status ='H' THEN 'Hold' WHEN Status='C' THEN 'Close' WHEN Status='O' THEN 'Open' AS [Status],Remarks AS [Remarks],Service_Enquiry_Code AS [Service Enquiry No],Engineer_Code AS [Route Engineer Code],Service_Place AS [Service Place],Location_Code As [Location Code],SVR_No AS [SVR No],CONVERT (VARCHAR,SVR_Date,103) AS [SVR Date],Created_By AS [Created By],CONVERT(VARCHAR,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_SERVICE_VISIT_DETAILS  "
        str = clsCommon.ShowSelectForm("SWSerVisit", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceVisitDetails)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            If ClsServiceVisitDetails.SaveData(arr, trans, Nothing) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceVisitDetails), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsServiceVisitDetails In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                clsCommon.AddColumnsForChange(coll, "Service_Visit_Date", clsCommon.GetPrintDate(obj.Service_Visit_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Service_Enquiry_Code", obj.Service_Enquiry_Code, True)
                clsCommon.AddColumnsForChange(coll, "SVR_Date", clsCommon.GetPrintDate(obj.SVR_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Engineer_Code", obj.Engineer_Code, True)
                clsCommon.AddColumnsForChange(coll, "Service_Place", obj.Service_Place)
                clsCommon.AddColumnsForChange(coll, "SVR_No", obj.SVR_No)
                '  clsCommon.AddColumnsForChange(coll, "DOCUMENT_FILE", obj.DOCUMENT_FILE)

                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SW_SERVICE_VISIT_DETAILS WHERE SERVICE_VISIT_CODE='" + obj.Service_Visit_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_SW_SERVICE_VISIT_DETAILS where SERVICE_VISIT_CODE= '" & obj.Service_Visit_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        obj.Service_Visit_Code = clsERPFuncationality.GetNextCode(trans, obj.Service_Visit_Date, clsDocType.SWServiceVisitDetails, "", "")
                        clsCommon.AddColumnsForChange(coll, "SERVICE_VISIT_CODE", obj.Service_Visit_Code)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_VISIT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                    Else

                        Throw New Exception("This Code Is Already Exist")

                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_VISIT_DETAILS", OMInsertOrUpdate.Update, "SERVICE_VISIT_CODE='" + obj.Service_Visit_Code + "'", trans)
                End If



                ClsServiceVisitMainItemDetail.SaveData(obj.Service_Visit_Code, obj.ObjMList, trans)
                ClsServiceVisitChildItemDetail.SaveData(obj.Service_Visit_Code, obj.ObjCList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteDocData(ByVal strEmpCode As String, ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = " UPDATE TSPL_SW_SERVICE_VISIT_DETAILS set document_file = null ,docname=null WHERE SERVICE_VISIT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetDocument(ByVal strCode As String) As DataTable
        Dim qry As String = " SELECT isnull(DOCUMENT_FILE,0x) As DOCUMENT_FILE,isnull(DocName,'') As DocName FROM TSPL_SW_SERVICE_VISIT_DETAILS WHERE SERVICE_VISIT_CODE ='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("DOCUMENT_FILE")
        Return dt
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsServiceVisitDetails
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsServiceVisitDetails
        Dim obj As ClsServiceVisitDetails = Nothing

        Dim qry As String = "Select * From TSPL_SW_SERVICE_VISIT_DETAILS where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SW_SERVICE_VISIT_DETAILS.Service_Visit_Code = (select MIN(Service_Visit_Code) from TSPL_SW_SERVICE_VISIT_DETAILS)"
            Case NavigatorType.Last
                qry += " and TSPL_SW_SERVICE_VISIT_DETAILS.Service_Visit_Code = (select Max(Service_Visit_Code) from TSPL_SW_SERVICE_VISIT_DETAILS)"
            Case NavigatorType.Next
                qry += " and TSPL_SW_SERVICE_VISIT_DETAILS.Service_Visit_Code = (select Min(Service_Visit_Code) from TSPL_SW_SERVICE_VISIT_DETAILS where  Service_Visit_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SW_SERVICE_VISIT_DETAILS.Service_Visit_Code = (select Max(Service_Visit_Code) from TSPL_SW_SERVICE_VISIT_DETAILS where Service_Visit_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SW_SERVICE_VISIT_DETAILS.Service_Visit_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsServiceVisitDetails()
            obj.Service_Visit_Code = clsCommon.myCstr(dt.Rows(0)("Service_Visit_Code"))
            obj.Service_Visit_Date = clsCommon.myCDate(dt.Rows(0)("Service_Visit_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Service_Enquiry_Code = clsCommon.myCstr(dt.Rows(0)("Service_Enquiry_Code"))
            obj.Engineer_Code = clsCommon.myCstr(dt.Rows(0)("Engineer_Code"))
            obj.Service_Place = clsCommon.myCstr(dt.Rows(0)("Service_Place"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.SVR_No = clsCommon.myCstr(dt.Rows(0)("SVR_No"))
            obj.DocName = clsCommon.myCstr(dt.Rows(0)("DocName"))
            obj.SVR_Date = clsCommon.myCDate(dt.Rows(0)("SVR_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))

            obj.ObjMList = ClsServiceVisitMainItemDetail.GetData(obj.Service_Visit_Code, trans)
            obj.ObjCList = ClsServiceVisitChildItemDetail.GetData(obj.Service_Visit_Code, trans)

        End If
        Return obj
    End Function
End Class

' ============================================== Main Items ====================================================
Public Class ClsServiceVisitMainItemDetail

#Region "Variables"
    Public Service_Visit_Code As String = String.Empty
    Public Main_Item_Code As String = String.Empty
    Public Serial_No As String = String.Empty
    Public Issued_Code As String = String.Empty
    Public Issue_Part_No As String = String.Empty
    Public Warranty_Status As String = String.Empty
    Public Charge_Status As String = String.Empty
    Public Service_Charge_Code As String = String.Empty
    Public Amount As Double = 0
    Public Qty As Double = 0
    Public Service_Type As String = String.Empty
    Public Item_Service_Type As String = String.Empty
    Public BOM_Revision_No As String = String.Empty
    Public FilePath As String = String.Empty
    Public FileName As String = String.Empty
    Public Remarks As String = String.Empty
#End Region
    Public Shared Function GetDocumentMain(ByVal strEnqNo As String, ByVal strItemNo As String) As DataTable
        Dim qry As String = " select FileData from TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM where Service_Enquiry_Code='" + strEnqNo + "' AND Main_Item_Code='" & strItemNo & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("FileData")
        Return dt
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM where Service_Visit_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjMList As List(Of ClsServiceVisitMainItemDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM where Service_Visit_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsServiceVisitMainItemDetail In ObjMList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Visit_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Main_Item_Code", obj.Main_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No, True)
                clsCommon.AddColumnsForChange(coll, "Issued_Code", obj.Issued_Code, True)
                clsCommon.AddColumnsForChange(coll, "Issue_Part_No", obj.Issue_Part_No)
                clsCommon.AddColumnsForChange(coll, "Warranty_Status", obj.Warranty_Status)
                clsCommon.AddColumnsForChange(coll, "Charge_Status", obj.Charge_Status)
                clsCommon.AddColumnsForChange(coll, "Service_Charge_Code", obj.Service_Charge_Code, True)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Service_Type", obj.Item_Service_Type)
                clsCommon.AddColumnsForChange(coll, "BOM_Revision_No", obj.BOM_Revision_No)
                clsCommon.AddColumnsForChange(coll, "FileName", obj.FileName)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM", OMInsertOrUpdate.Insert, "", trans)

                '' Document Saving
                If clsCommon.myLen(obj.FilePath) > 0 Then
                    Dim bData As Byte()
                    Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(obj.FilePath)))
                    bData = br.ReadBytes(br.BaseStream.Length)
                    Dim str As String
                    str = " UPDATE TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM SET FileData = @BLOBData where Main_Item_Code='" + obj.Main_Item_Code + "' AND Service_Visit_Code='" & obj.Service_Visit_Code & "'"
                    Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                    Dim prm As New SqlParameter("@BLOBData", bData)
                    cmd.Parameters.Add(prm)
                    cmd.ExecuteNonQuery()
                    br.Close() ' done by stuti reagrding memory leakage
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsServiceVisitMainItemDetail)
        Dim obj As ClsServiceVisitMainItemDetail = Nothing
        Dim ObjListMain As New List(Of ClsServiceVisitMainItemDetail)
        Dim qry As String = " select *  from TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM WHERE Service_Visit_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsServiceVisitMainItemDetail()
                obj.Service_Visit_Code = clsCommon.myCstr(dr("Service_Visit_Code"))
                obj.Main_Item_Code = clsCommon.myCstr(dr("Main_Item_Code"))
                obj.Issued_Code = clsCommon.myCstr(dr("Issued_Code"))
                obj.Issue_Part_No = clsCommon.myCstr(dr("Issue_Part_No"))
                obj.Warranty_Status = clsCommon.myCstr(dr("Warranty_Status"))
                obj.Charge_Status = clsCommon.myCstr(dr("Charge_Status"))
                obj.Service_Charge_Code = clsCommon.myCstr(dr("Service_Charge_Code"))
                obj.FileName = clsCommon.myCstr(dr("FileName"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))
                obj.Qty = clsCommon.myCdbl(dr("Qty"))
                obj.Service_Type = clsCommon.myCstr(dr("Service_Type"))
                obj.Item_Service_Type = clsCommon.myCstr(dr("Item_Service_Type"))
                obj.BOM_Revision_No = clsCommon.myCstr(dr("BOM_Revision_No"))
                ObjListMain.Add(obj)
            Next
        End If
        Return ObjListMain
    End Function
End Class

' ============================================== Child Items ====================================================
Public Class ClsServiceVisitChildItemDetail

#Region "Variables"
    Public Service_Visit_Code As String = String.Empty
    Public Main_Item_Code As String = String.Empty
    Public Child_Item_Code As String = String.Empty
    Public Serial_No As String = String.Empty
    Public Issued_Code As String = String.Empty
    Public Issue_Part_No As String = String.Empty
    Public Warranty_Status As String = String.Empty
    Public Charge_Status As String = String.Empty
    Public Service_Charge_Code As String = String.Empty
    Public Amount As Double = 0
    Public Qty As Double = 0
    Public Service_Type As String = String.Empty
    Public Item_Service_Type As String = String.Empty
    Public BOM_Revision_No As String = String.Empty
    Public FilePath As String = String.Empty
    Public FileName As String = String.Empty
    Public Remarks As String = String.Empty
#End Region
    Public Shared Function GetDocumentMain(ByVal strEnqNo As String, ByVal strItemNo As String) As DataTable
        Dim qry As String = " select FileData from TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM where Service_Enquiry_Code='" + strEnqNo + "' AND Main_Item_Code='" & strItemNo & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("FileData")
        Return dt
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_SW_SERVICE_VISIT_DETAILS_MAIN_ITEM where Service_Visit_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjCList As List(Of ClsServiceVisitChildItemDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_SW_SERVICE_VISIT_DETAILS_CHILD_ITEM where Service_Visit_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsServiceVisitChildItemDetail In ObjCList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Visit_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Main_Item_Code", obj.Main_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Child_Item_Code", obj.Child_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No, True)
                clsCommon.AddColumnsForChange(coll, "Issued_Code", obj.Issued_Code, True)
                clsCommon.AddColumnsForChange(coll, "Issue_Part_No", obj.Issue_Part_No)
                clsCommon.AddColumnsForChange(coll, "Warranty_Status", obj.Warranty_Status)
                clsCommon.AddColumnsForChange(coll, "Charge_Status", obj.Charge_Status)
                clsCommon.AddColumnsForChange(coll, "Service_Charge_Code", obj.Service_Charge_Code, True)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Service_Type", obj.Item_Service_Type)
                clsCommon.AddColumnsForChange(coll, "BOM_Revision_No", obj.BOM_Revision_No)
                clsCommon.AddColumnsForChange(coll, "FileName", obj.FileName)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_VISIT_DETAILS_CHILD_ITEM", OMInsertOrUpdate.Insert, "", trans)
                '' Document Saving
                If clsCommon.myLen(obj.FilePath) > 0 Then
                    Dim bData As Byte()
                    Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(obj.FilePath)))
                    bData = br.ReadBytes(br.BaseStream.Length)
                    Dim str As String
                    str = " UPDATE TSPL_SW_SERVICE_VISIT_DETAILS_CHILD_ITEM SET FileData = @BLOBData where Main_Item_Code='" + obj.Main_Item_Code + "' AND Service_Visit_Code='" & obj.Service_Visit_Code & "' AND Child_Item_Code='" & obj.Child_Item_Code & "'"
                    Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                    Dim prm As New SqlParameter("@BLOBData", bData)
                    cmd.Parameters.Add(prm)
                    cmd.ExecuteNonQuery()
                    br.Close() ' done by stuti reagrding memory leakage
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsServiceVisitChildItemDetail)
        Dim obj As ClsServiceVisitChildItemDetail = Nothing
        Dim ObjCList As New List(Of ClsServiceVisitChildItemDetail)
        Dim qry As String = " select *  from TSPL_SW_SERVICE_VISIT_DETAILS_CHILD_ITEM WHERE Service_Visit_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsServiceVisitChildItemDetail()
                obj.Service_Visit_Code = clsCommon.myCstr(dr("Service_Visit_Code"))
                obj.Main_Item_Code = clsCommon.myCstr(dr("Main_Item_Code"))
                obj.Child_Item_Code = clsCommon.myCstr(dr("Child_Item_Code"))
                obj.Issued_Code = clsCommon.myCstr(dr("Issued_Code"))
                obj.Issue_Part_No = clsCommon.myCstr(dr("Issue_Part_No"))
                obj.Warranty_Status = clsCommon.myCstr(dr("Warranty_Status"))
                obj.Charge_Status = clsCommon.myCstr(dr("Charge_Status"))
                obj.Service_Charge_Code = clsCommon.myCstr(dr("Service_Charge_Code"))
                obj.FileName = clsCommon.myCstr(dr("FileName"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))
                obj.Qty = clsCommon.myCdbl(dr("Qty"))
                obj.Service_Type = clsCommon.myCstr(dr("Service_Type"))
                obj.Item_Service_Type = clsCommon.myCstr(dr("Item_Service_Type"))
                obj.BOM_Revision_No = clsCommon.myCstr(dr("BOM_Revision_No"))
                ObjCList.Add(obj)
            Next
        End If
        Return ObjCList
    End Function
End Class
