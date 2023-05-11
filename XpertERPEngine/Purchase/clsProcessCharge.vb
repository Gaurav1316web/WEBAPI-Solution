Imports System
Imports System.Data.SqlClient
Imports common
Imports System.Windows.Forms
Imports Telerik.WinControls
Public Class clsProcessCharge
#Region "veriables"
    Public Code As String = Nothing
    Public desc As String
    Public Account_Code As String
    Public Unit_Code As String = Nothing
    Public Account_Description As String
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String
    Public freight As String = Nothing
    Public abtment As Decimal = Nothing
    Public Reverse_Charge_Per As Decimal = Nothing
    Public specification As String = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String

        Dim str As String = ""
        Dim qry As String = " select Code as [Code],Description,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modify By],Modified_Date as [Modify Date],Comp_Code as [Company Code],Account_Code as [Account Code],Account_Description as [Account Description],FreightCharges as [Freight Charges],Abatement,Specification from TSPL_Process_Charges   "
        str = clsCommon.ShowSelectForm("RPTADCHGFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    ''For Save Data in AdditionalChrges Table
    Public Function SaveData(ByVal obj As clsProcessCharge, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
            clsCommon.AddColumnsForChange(coll, "Account_Description", obj.Account_Description)
            clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "FreightCharges", obj.freight)
            clsCommon.AddColumnsForChange(coll, "abatement", obj.abtment)
            clsCommon.AddColumnsForChange(coll, "Reverse_Charge_Per", obj.Reverse_Charge_Per)
            clsCommon.AddColumnsForChange(coll, "specification", obj.specification)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                Dim qry As String = "select COUNT (*) from TSPL_Process_Charges where code = '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Process_Charges", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This value has already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Process_Charges", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)

            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved

    End Function
    Public Shared Function GetData(ByVal code As String, ByVal NavType As common.NavigatorType) As clsProcessCharge
        Return GetData(code, NavType, Nothing)

    End Function
    Public Shared Function GetData(ByVal code As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsProcessCharge
        Dim obj As clsProcessCharge = Nothing
        Dim qry As String = "SELECT Code,Unit_Code, description,Account_Code,Account_Description ,freightCharges,specification,abatement,Reverse_Charge_Per from TSPL_Process_Charges where  2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Process_Charges.code =(select MIN(Code) from TSPL_Process_Charges)"
            Case NavigatorType.Last
                qry += "  and TSPL_Process_Charges.code =(select Max(Code) from TSPL_Process_Charges)"
            Case NavigatorType.Next
                qry += " and TSPL_Process_Charges.code=(select Min(Code) from TSPL_Process_Charges where Code > '" + code + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Process_Charges.code=(select Max(Code) from TSPL_Process_Charges where Code < '" + code + "')"
            Case NavigatorType.Current
                qry += " and TSPL_Process_Charges.code='" + code + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsProcessCharge()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.desc = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Account_Code = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
            obj.Account_Description = clsCommon.myCstr(dt.Rows(0)("Account_Description"))
            obj.freight = clsCommon.myCstr(dt.Rows(0)("freightCharges"))
            obj.abtment = clsCommon.myCdbl(dt.Rows(0)("abatement"))
            obj.Reverse_Charge_Per = clsCommon.myCdbl(dt.Rows(0)("Reverse_Charge_Per"))
            obj.specification = clsCommon.myCstr(dt.Rows(0)("specification"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
        End If

        Return obj
    End Function

    Public Shared Function SaveData(ByVal Code As String, ByVal Arr As List(Of clsProcessCharge), ByVal trans As SqlTransaction) As Boolean
        Dim Desc As String = ""
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProcessCharge In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", Code)
                clsCommon.AddColumnsForChange(coll, "Description", Desc)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Process_Charges", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function

    Public Shared Function DeleteData(ByVal Code As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(Code) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim obj As clsProcessCharge = clsProcessCharge.GetData(Code, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                Dim qry As String = "delete from TSPL_Process_Charges where Code='" + Code + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Process_Charges where Code='" + Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
End Class
