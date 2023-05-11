Imports common
Imports System.Data.SqlClient
Public Class clsVSPIncentiveTagging
#Region "Variable"
    Public Doc_Code As String = Nothing
    Public VSP_CODE As String = Nothing
    Public VSP_Name As String = Nothing
    Public VLC_CODE As String = Nothing
    Public VLC_Name As String = Nothing
    Public Mcc_Name As String = Nothing
    Public Incentive_Name As String = Nothing
    Public INCENTIVE_CODE As String = Nothing
    Public MCC_Code As String = Nothing
    Public Shared arr As ArrayList = Nothing
    Public Shared arrIncentive As ArrayList = Nothing
    Public Route_CODE As String = Nothing
    Public StartDate As String = Nothing
    Public StartShift As String = Nothing
    Public EndDate As String = Nothing
    Public EndShift As String = Nothing
    Public VSPSelect As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal VSP As clsVSPIncentiveTagging, ByVal arr As List(Of clsVSPIncentiveTagging), ByVal isnewentry As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim qry As String = "select count(*) from TSPL_VSP_INCENTIVE_Detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Doc_Code ='" + VSP.Doc_Code + "'"
            Dim isexist As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            Dim strcode As String = ""
            If clsCommon.myLen(VSP.Doc_Code) <= 0 Then
                qry = "select Doc_Code from TSPL_VSP_INCENTIVE_Detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Mcc_Code ='" + VSP.MCC_Code + "' and Incentive_Code ='" + VSP.INCENTIVE_CODE + "' and VSP_Code='" & VSP.VSP_CODE & "'"
                Dim isexist_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                If clsCommon.myLen(isexist_Code) > 0 Then
                    clsCommon.MyMessageBoxShow("This Combination of VSP,Incentive and Mcc is Already exits.Check Code [" & isexist_Code & "]")
                    trans.Rollback()
                    Return False
                    Exit Function
                End If
            End If

            If isexist = 0 Then
                isnewentry = True
                strcode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.VSPIncentiveTagging, "", VSP.MCC_Code)
                VSP.Doc_Code = strcode
            Else
                isnewentry = False
                strcode = VSP.Doc_Code
                qry = "delete from TSPL_VSP_INCENTIVE_Detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Doc_Code ='" + VSP.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If (arr IsNot Nothing AndAlso arr.Count > 0) Then
                For Each Code As clsVSPIncentiveTagging In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strcode)
                    clsCommon.AddColumnsForChange(coll, "VSP_CODE", Code.VSP_CODE)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", Code.INCENTIVE_CODE)
                    clsCommon.AddColumnsForChange(coll, "MCC_Code", Code.MCC_Code)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "StartDate", Code.StartDate)
                    clsCommon.AddColumnsForChange(coll, "StartShift", Code.StartShift)
                    clsCommon.AddColumnsForChange(coll, "EndDate", Code.EndDate)
                    clsCommon.AddColumnsForChange(coll, "EndShift", Code.EndShift)
                    clsCommon.AddColumnsForChange(coll, "VSPSelect", Code.VSPSelect)
                    'qry = "select count(*) from TSPL_VSP_INCENTIVE_Detail where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Doc_Code ='" + VSP.Doc_Code + "' and vsp_code='" & Code.VSP_CODE & "' and Mcc_code='" & Code.MCC_Code & "' and Incentive_code='" & Code.INCENTIVE_CODE & "'"
                    'isexist = clsDBFuncationality.getSingleValue(qry, trans)
                    'If isexist <= 0 Then 'If isnewentry Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE_Detail", OMInsertOrUpdate.Insert, "", trans)
                    qry = "update TSPL_VENDOR_MASTER set incentive=null,Apply_Mult_Incentive=1 where Vendor_Code='" & Code.VSP_CODE & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_VSP_INCENTIVE where VENDOR_CODE='" & Code.VSP_CODE & "' and INCENTIVE_CODE='" & Code.INCENTIVE_CODE & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "VENDOR_CODE", Code.VSP_CODE)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", Code.INCENTIVE_CODE)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE", OMInsertOrUpdate.Insert, "", trans)                   
                Next Code

            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function
    Public Shared Function SaveDataDetail(ByVal strTragetCode As String, ByVal arr As List(Of clsVSPIncentiveTagging), ByVal trans As SqlTransaction) As Boolean
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each Code As clsVSPIncentiveTagging In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "VENDOR_CODE", Code.VSP_CODE)
                clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", Code.INCENTIVE_CODE)
                clsCommon.AddColumnsForChange(coll, "StartDate", Code.StartDate)
                clsCommon.AddColumnsForChange(coll, "StartShift", Code.StartShift)
                clsCommon.AddColumnsForChange(coll, "EndDate", Code.EndDate)
                clsCommon.AddColumnsForChange(coll, "EndShift", Code.EndShift)
                clsCommon.AddColumnsForChange(coll, "VSPSelect", Code.VSPSelect)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select Distinct Doc_code as Code,INCENTIVE_CODE,Mcc_code from TSPL_VSP_INCENTIVE_Detail"
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_VSP_INCENTIVE_Detail.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_VSP_INCENTIVE_Detail.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("PPVIT", qry, "Code", whrCls, currCode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As List(Of clsVSPIncentiveTagging)
        Dim obj As New clsVSPIncentiveTagging()
        Dim objtr As New clsVSPIncentiveTagging()

        Dim ObjList As New List(Of clsVSPIncentiveTagging)
        arr = New ArrayList
        Dim qry As String = "select  TSPL_VSP_INCENTIVE_Detail.Doc_Code,TSPL_VSP_INCENTIVE_Detail.Mcc_Code,Mcc_Name," _
        & " tspl_Incentive_Master_Head.description as Incentive_name,TSPL_VSP_INCENTIVE_Detail.Incentive_Code,VLC_Code,VLC_Name,TSPL_VENDOR_MASTER.Vendor_Code as VSP_Code," _
        & " Vendor_Name,TSPL_VLC_MASTER_Head.Route_Code,TSPL_VSP_INCENTIVE_Detail.StartDate,TSPL_VSP_INCENTIVE_Detail.StartShift,TSPL_VSP_INCENTIVE_Detail.EndDate,TSPL_VSP_INCENTIVE_Detail.EndShift,TSPL_VSP_INCENTIVE_Detail.VSPSelect from TSPL_MCC_MASTER inner join TSPL_VSP_INCENTIVE_Detail on TSPL_VSP_INCENTIVE_Detail.mcc_code=TSPL_MCC_MASTER.mcc_code inner join " _
        & " tspl_Incentive_Master_Head on tspl_Incentive_Master_Head.incentive_Code=TSPL_VSP_INCENTIVE_Detail.incentive_Code inner join TSPL_VLC_MASTER_Head " _
        & " on " _
        & " TSPL_VLC_MASTER_Head.mcc = TSPL_MCC_MASTER.MCC_Code inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_Head.VSP_Code and TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE_Detail.VSP_Code "
        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_VSP_INCENTIVE_Detail)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(INCENTIVE_CODE) from TSPL_VSP_INCENTIVE_Detail)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(INCENTIVE_CODE) from TSPL_VSP_INCENTIVE_Detail where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(INCENTIVE_CODE) from TSPL_VSP_INCENTIVE_Detail where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each row As DataRow In dt.Rows
                obj = New clsVSPIncentiveTagging
                obj.INCENTIVE_CODE = row.Item("INCENTIVE_CODE")
                obj.Doc_Code = clsCommon.myCstr(row.Item("Doc_Code"))
                obj.MCC_Code = clsCommon.myCstr(row.Item("Mcc_Code"))
                obj.VSP_CODE = clsCommon.myCstr(row.Item("VSP_CODE"))
                obj.Route_CODE = clsCommon.myCstr(row.Item("Route_Code"))
                arr.Add(obj.VSP_CODE)
                obj.VSP_Name = clsCommon.myCstr(row.Item("Vendor_name"))
                obj.VLC_CODE = clsCommon.myCstr(row.Item("VLC_Code"))
                obj.VLC_Name = clsCommon.myCstr(row.Item("Vlc_Name"))
                obj.Mcc_Name = clsCommon.myCstr(row.Item("Mcc_Name"))
                obj.Incentive_Name = clsCommon.myCstr(row.Item("Incentive_Name"))
                If Not IsDBNull(row("StartDate")) Then
                    obj.StartDate = clsCommon.myCDate(row("StartDate"))
                End If
                obj.StartShift = clsCommon.myCstr(row.Item("StartShift"))
                If Not IsDBNull(row("EndDate")) Then
                    obj.EndDate = clsCommon.myCDate(row("EndDate"))
                End If
                obj.EndShift = clsCommon.myCstr(row.Item("EndShift"))
                obj.VSPSelect = clsCommon.myCBool(row.Item("VSPSelect"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
   End Class
