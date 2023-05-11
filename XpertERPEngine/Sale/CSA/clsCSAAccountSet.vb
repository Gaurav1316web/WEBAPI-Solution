'-----------created by Monika
Imports common
Imports System.Data.SqlClient
Public Class clsCSAAccountSet
#Region "Variables"
    Public debtr_code As String = Nothing
    Public debtr_desc As String = Nothing
    Public acc_code As String = Nothing
    Public desc As String = Nothing
    Public gsoc_code As String = Nothing
    Public gsoc As String = Nothing
    Public consignmnt As String = Nothing
    Public consignmnt_name As String = Nothing
    Public gain_code As String = Nothing
    Public gain As String = Nothing
    Public loss As String = Nothing
    Public loss_code As String = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = "select Cust_Account as Code,Cust_Acct_Desc as [Description],Receivable_Control_acct as [Debitor Account],Receipts_Discount_acct as [Discount Account],Advance_acct as [Advance A/c],Write_Offs as [Write Off],Container_Deposit as [Container Deposit],SECURITY_ACCOUNT as [Security A/c],BANK_GUARANTEE as [Bank Guarantee A/c],GSOC_Acct as [GSOC A/c],Consignment_Acct as [Consignment A/c],Gain_Acct as [Gain A/c],Loss_Acct as [Loss A/c] from TSPL_CUSTOMER_ACCOUNT_SET"
            str = clsCommon.ShowSelectForm("ACCFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSAAccountSet, ByVal isNewentry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            clsCommon.AddColumnsForChange(coll, "Cust_Acct_Desc", obj.desc)
            clsCommon.AddColumnsForChange(coll, "GSOC_Acct", obj.gsoc_code)
            clsCommon.AddColumnsForChange(coll, "Consignment_Acct", obj.consignmnt)
            clsCommon.AddColumnsForChange(coll, "Gain_Acct", obj.gain_code)
            clsCommon.AddColumnsForChange(coll, "Loss_Acct", obj.loss_code)

            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ACCOUNT_SET", OMInsertOrUpdate.Update, " Cust_Account='" + obj.acc_code + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.acc_code), "TSPL_CUSTOMER_ACCOUNT_SET", "Cust_Account", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCSAAccountSet
        Try
            Dim obj As clsCSAAccountSet = GetData(strCode, NavType, Nothing)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCSAAccountSet
        Try
            Dim obj As New clsCSAAccountSet()

            Dim qry As String = "select TSPL_CUSTOMER_ACCOUNT_SET.*,gl.description,gl8.description as gsoc_name,gl9.description as consgnmnt_name,g20.description as gain_name,g21.description as loss_name from TSPL_CUSTOMER_ACCOUNT_SET "
            qry += "left join TSPL_GL_ACCOUNTS gl8 on tspl_customer_account_set.GSOC_Acct=gl8.account_code "
            qry += "left join TSPL_GL_ACCOUNTS gl on tspl_customer_account_set.receivable_control_acct=gl.account_code "
            qry += "left join TSPL_GL_ACCOUNTS gl9 on tspl_customer_account_set.Consignment_Acct=gl9.account_code "
            qry += "left join TSPL_GL_ACCOUNTS g20 on tspl_customer_account_set.Gain_Acct=g20.account_code "
            qry += "left join TSPL_GL_ACCOUNTS g21 on tspl_customer_account_set.Loss_Acct=g21.account_code "
            qry += " where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account in (select min(Cust_Account) from TSPL_CUSTOMER_ACCOUNT_SET)"
                Case NavigatorType.Last
                    qry += " and TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account in (select max(Cust_Account) from TSPL_CUSTOMER_ACCOUNT_SET)"
                Case NavigatorType.Next
                    qry += " and TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account in (select min(Cust_Account) from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account in (select max(Cust_Account) from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.acc_code = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
                obj.gsoc_code = clsCommon.myCstr(dt.Rows(0)("GSOC_Acct"))
                obj.debtr_code = clsCommon.myCstr(dt.Rows(0)("receivable_control_acct"))
                obj.debtr_desc = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.gsoc = clsCommon.myCstr(dt.Rows(0)("gsoc_name"))
                obj.consignmnt = clsCommon.myCstr(dt.Rows(0)("Consignment_Acct"))
                obj.consignmnt_name = clsCommon.myCstr(dt.Rows(0)("consgnmnt_name"))
                obj.gain_code = clsCommon.myCstr(dt.Rows(0)("Gain_Acct"))
                obj.gain = clsCommon.myCstr(dt.Rows(0)("gain_name"))
                obj.loss_code = clsCommon.myCstr(dt.Rows(0)("Loss_Acct"))
                obj.loss = clsCommon.myCstr(dt.Rows(0)("loss_name"))
                obj.desc = clsCommon.myCstr(dt.Rows(0)("Cust_Acct_Desc"))
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "update TSPL_CUSTOMER_ACCOUNT_SET set GSOC_Acct=NULL,Consignment_Acct=NULL,Gain_Acct=NULL,Loss_Acct=NULL where Cust_Account='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

''======================Below new class for commission/freight mapping for CSA,done by Monika
Public Class clsCSACommissionFreightMappingHead
#Region "variable"
    Public Doc_No As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public isNewEntry As Boolean = Nothing

    Public Arr As New List(Of clsCSACommissionFreightMappingDetail)
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_no as [Code],convert(date,TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_date,103) as [Document Date],TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.cust_code as [CSA],tspl_customer_master.customer_name as [CSA Name],TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name],TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.created_by as [Created By] " & _
            " from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.vendor_code left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.cust_code "

        str = clsCommon.myCstr(clsCommon.ShowSelectForm("VNDCMSNFRGHT", qry, "Code", whrCls, strCurrCode, "", isButtonClicked))

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSACommissionFreightMappingHead) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSACommissionFreightMappingHead, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Dim qry As String = ""
        Try

            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                If obj.isNewEntry Then
                    qry = "select max(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD"
                    obj.Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    If clsCommon.myLen(obj.Doc_No) > 0 Then
                        obj.Doc_No = clsCommon.incval(obj.Doc_No)
                    Else
                        obj.Doc_No = "CSA-CF-0000000001"
                    End If
                End If

                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                If obj.isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.Doc_No + "' ", trans)
                End If

                clsCSACommissionFreightMappingDetail.SaveData(obj.Doc_No, obj.Arr, trans)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function


   


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCSACommissionFreightMappingHead
        Dim obj As New clsCSACommissionFreightMappingHead()
        Dim dt As New DataTable()
        Try
            Dim qry As String = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.*,tspl_customer_master.customer_name,tspl_vendor_master.vendor_name from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.vendor_code " & _
                " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.cust_code where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_no in (select min(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Last
                    qry += " and TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_no in (select max(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Next
                    qry += " and TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_no in (select min(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.doc_no in (select max(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and doc_no<'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            obj.Arr = New List(Of clsCSACommissionFreightMappingDetail)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("cust_code"))
                obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("customer_name"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))

                obj.Arr = clsCSACommissionFreightMappingDetail.GetData(obj.Doc_No, trans)
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCOde As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL where doc_no='" + strCOde + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where doc_no='" + strCOde + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCSACommissionFreightMappingDetail
#Region "variables"
    Public Doc_No As String = Nothing
    Public S_No As Decimal = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Commission_Rate As Decimal = Nothing
    Public Commission_UOM As String = Nothing
    Public Commission_Type As String = Nothing
    Public Commission_AC_Code As String = Nothing
    Public Commission_AC_Desc As String = Nothing

    Public Freight_Rate As Decimal = Nothing
    Public Freight_UOM As String = Nothing
    Public Freight_Type As String = Nothing
    Public Freight_AC_Code As String = Nothing
    Public Freight_AC_Desc As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCSACommissionFreightMappingDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsCSACommissionFreightMappingDetail In Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "S_No", obj.S_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Commission_Rate", obj.Commission_Rate)
                    clsCommon.AddColumnsForChange(coll, "Commission_UOM", obj.Commission_UOM, True)
                    clsCommon.AddColumnsForChange(coll, "Commission_Type", obj.Commission_Type)
                    clsCommon.AddColumnsForChange(coll, "Commission_AC_Code", obj.Commission_AC_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Freight_Rate", obj.Freight_Rate)
                    clsCommon.AddColumnsForChange(coll, "Freight_UOM", obj.Freight_UOM, True)
                    clsCommon.AddColumnsForChange(coll, "Freight_Type", obj.Freight_Type)
                    clsCommon.AddColumnsForChange(coll, "Freight_AC_Code", obj.Freight_AC_Code, True)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCSACommissionFreightMappingDetail)
        Dim Arr As New List(Of clsCSACommissionFreightMappingDetail)
        Dim dt As New DataTable()
        Dim obj As New clsCSACommissionFreightMappingDetail()
        Try
            Dim qry As String = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.*,tspl_item_master.item_desc,tspl_gl_accounts.description as [cmmn_ac_desc],frghtacc.description as [Freight_Desc] from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.item_code " & _
                " left outer join tspl_gl_accounts on tspl_gl_accounts.account_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_AC_Code left outer join tspl_gl_accounts as frghtacc on frghtacc.account_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_AC_Code " & _
                " where TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.doc_no='" + strCode + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsCSACommissionFreightMappingDetail()

                    obj.Commission_AC_Code = clsCommon.myCstr(dr("Commission_AC_Code"))
                    obj.Commission_AC_Desc = clsCommon.myCstr(dr("cmmn_ac_desc"))
                    obj.Commission_Rate = clsCommon.myCdbl(dr("Commission_Rate"))
                    obj.Commission_Type = clsCommon.myCstr(dr("Commission_Type"))
                    obj.Commission_UOM = clsCommon.myCstr(dr("Commission_UOM"))
                    obj.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    obj.Freight_AC_Code = clsCommon.myCstr(dr("Freight_AC_Code"))
                    obj.Freight_AC_Desc = clsCommon.myCstr(dr("Freight_Desc"))
                    obj.Freight_Rate = clsCommon.myCdbl(dr("Freight_Rate"))
                    obj.Freight_Type = clsCommon.myCstr(dr("Freight_Type"))
                    obj.Freight_UOM = clsCommon.myCstr(dr("Freight_UOM"))
                    obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dr("Item_desc"))
                    obj.S_No = clsCommon.myCdbl(dr("S_No"))

                    Arr.Add(obj)
                Next
            End If

            Return Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            obj = Nothing
        End Try
    End Function
End Class