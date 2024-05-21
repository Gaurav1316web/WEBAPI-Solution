Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Public Class clsContractTankerHead
    Public TANKER_CODE As String = Nothing
    Public TANKER_NO As String = Nothing
    Public Vendor_Code As String = String.Empty
    Public Vendor_Name As String = String.Empty
    Public NO_OF_CHAMBER As Integer = 0
    Public isNewEntry As Boolean = False
    Public Arr As List(Of clsContractTankerDetail) = Nothing
    Public Arrvendor As List(Of clsContractTankerVendorDetail) = Nothing
    Public Function SaveData(ByVal obj As clsContractTankerHead) As Boolean
        Dim issaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_CONTRACT_TANKER_DETAIL where TANKER_CODE='" + obj.TANKER_CODE + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CONTRACT_TANKER_VENDOR_DETAIL where TANKER_CODE='" + obj.TANKER_CODE + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            'If obj.isNewEntry Then
            '    obj.TANKER_CODE = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.ContractTanker, "", "")

            'End If
            'If (clsCommon.myLen(obj.TANKER_CODE) <= 0) Then
            '    Throw New Exception("Error in Document Code Generation")
            'End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TANKER_CODE", obj.TANKER_CODE)
            clsCommon.AddColumnsForChange(coll, "TANKER_NO", obj.TANKER_NO)
            clsCommon.AddColumnsForChange(coll, "NO_OF_CHAMBER", obj.NO_OF_CHAMBER)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_MASTER", OMInsertOrUpdate.Update, "TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE='" + obj.TANKER_CODE + "'", trans)
            End If
            issaved = issaved And clsContractTankerDetail.SaveData(obj.TANKER_CODE, obj.Arr, trans)
            issaved = issaved And clsContractTankerVendorDetail.SaveData(obj.TANKER_CODE, obj.Arrvendor, trans)
            If issaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType) As clsContractTankerHead
        Dim obj As clsContractTankerHead = Nothing
        Dim Qry As String = "Select TSPL_CONTRACT_TANKER_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE,TSPL_CONTRACT_TANKER_MASTER.TANKER_NO,TSPL_CONTRACT_TANKER_MASTER.NO_OF_CHAMBER from TSPL_CONTRACT_TANKER_MASTER  left outer join TSPL_VENDOR_MASTER ON TSPL_CONTRACT_TANKER_MASTER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where 2=2"
        Dim whrClas As String = ""


        Select Case NavType
            Case NavigatorType.First
                Qry += " and TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE = (select MIN(TANKER_CODE) from TSPL_CONTRACT_TANKER_MASTER where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                Qry += " and TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE = (select Max(TANKER_CODE) from TSPL_CONTRACT_TANKER_MASTER where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                Qry += " and TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE = (select Min(TANKER_CODE) from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                Qry += " and TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE = (select Max(TANKER_CODE) from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                Qry += " and TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsContractTankerHead
            obj.TANKER_CODE = clsCommon.myCstr(dt.Rows(0)("TANKER_CODE"))
            obj.TANKER_NO = clsCommon.myCstr(dt.Rows(0)("TANKER_NO"))
            obj.NO_OF_CHAMBER = clsCommon.myCdbl(dt.Rows(0)("NO_OF_CHAMBER"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))

            Qry = "Select TSPL_CONTRACT_TANKER_DETAIL.Line_No,TSPL_CONTRACT_TANKER_DETAIL.CHAMBER_DESC from TSPL_CONTRACT_TANKER_DETAIL where TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE='" & obj.TANKER_CODE & "' order by TSPL_CONTRACT_TANKER_DETAIL.Line_No asc "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(Qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsContractTankerDetail)
                Dim objTr As clsContractTankerDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsContractTankerDetail
                    objTr.Line_No = clsCommon.myCdbl(clsCommon.myCdbl(dr("Line_No")))
                    objTr.CHAMBER_DESC = clsCommon.myCstr(clsCommon.myCstr(dr("CHAMBER_DESC")))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

    
        Return obj
    End Function
    Public Shared Function getTankerFinderBasedonVendor(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'Dim qry As String = " select TANKER_CODE as Code,TANKER_NO as [Tanker No],NO_OF_CHAMBER as [No Of Chamber],TSPL_CONTRACT_TANKER_MASTER.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name]  from TSPL_CONTRACT_TANKER_MASTER left outer join TSPL_VENDOR_MASTER ON TSPL_CONTRACT_TANKER_MASTER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code "
        Dim Qry As String = "select  TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE as Code,TSPL_CONTRACT_TANKER_MASTER.TANKER_NO as [Tanker No],NO_OF_CHAMBER as [No Of Chamber],TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name]  from TSPL_CONTRACT_TANKER_MASTER left outer join TSPL_CONTRACT_TANKER_VENDOR_DETAIL on TSPL_CONTRACT_TANKER_MASTER.TANKER_CODE=TSPL_CONTRACT_TANKER_VENDOR_DETAIL.TANKER_CODE left outer join TSPL_VENDOR_MASTER ON TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code "
        str = clsCommon.ShowSelectForm("ContracttabkerMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TANKER_CODE as Code,TANKER_NO as [Tanker No],NO_OF_CHAMBER as [No Of Chamber]  from TSPL_CONTRACT_TANKER_MASTER  "
        str = clsCommon.ShowSelectForm("ContracttabkerMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "TSPL_CONTRACT_TANKER_MASTER.Created_Date")
        Return str
    End Function
    Public Shared Function getTankerNo(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CONTRACT_TANKER_MASTER.TANKER_NO from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE='" & strCode & "'", trans))
        Return strDesc
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Tanker Code not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = "delete from TSPL_CONTRACT_TANKER_DETAIL where TANKER_CODE='" + strCode + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "delete from TSPL_CONTRACT_TANKER_MASTER where TANKER_CODE='" + strCode + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If (isSaved) Then
            trans.Commit()
        Else
            trans.Rollback()
        End If

        Return isSaved
    End Function
End Class
Public Class clsContractTankerDetail
    Public Line_No As Integer = Nothing
    Public CHAMBER_DESC As String = Nothing
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsContractTankerDetail), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each obj As clsContractTankerDetail In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TANKER_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "CHAMBER_DESC", obj.CHAMBER_DESC)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return issaved
    End Function

End Class
Public Class clsContractTankerVendorDetail
    Public Line_No As Integer = Nothing
    Public Vendor_Code As String = Nothing
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsContractTankerVendorDetail), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each obj As clsContractTankerVendorDetail In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TANKER_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTRACT_TANKER_VENDOR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return issaved
    End Function
End Class