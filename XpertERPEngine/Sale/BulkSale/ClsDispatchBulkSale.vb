'--------Created By Richa 04/08/2014 Against Ticket No BM00000003248
Imports System.Data.SqlClient
Imports common

Public Class ClsDispatchBulkSale
#Region "Variable"
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Customer_Code As String = Nothing
    Public Transporter As String = Nothing
    Public QC_Code As String = Nothing
    Public Tanker_Code As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Location_Code As String = Nothing
    Public Dip_marking As String = Nothing
    Public Challan_No As String = Nothing
    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Price_Code As String = Nothing
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Total_Amt As Double = 0
    Public ApprovalRequired As String = Nothing
    Public Approved As String = Nothing
    Public Status As String = Nothing
    Public CreditLimit As Double = 0
    Public Is_Create_Auto_Invoice As Integer = 0
    Public Created_Date As Date?
    Public Modified_Date As Date?
    Public ReverseFlag As String = Nothing
    Public SalesOrder_Code As String = String.Empty
    Public Insurance_No As String = Nothing
    Public Seal_No As String = Nothing
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public EWayBillNo As String = Nothing
    Public EWayBillDate As Date?
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Document_Amount As Double = 0

    Public arrDispatchDetailBulkSale As List(Of clsDispatchDetailBulkSale) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No As Code ,TSPL_Dispatch_BulkSale.Document_Date as Date from TSPL_Dispatch_BulkSale "
        Return clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function
    Public Shared Function SaveData(ByVal obj As ClsDispatchBulkSale, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsDispatchBulkSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        '  Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim ApplyTSPriceAtBulkSale As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, trans)) = 1, True, False))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmDispatchBulkSale, obj.Location_Code, obj.Document_Date, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_Dispatch_BulkSale", "Document_No", "TSPL_Dispatch_Detail_BulkSale", "Document_No", trans)
            End If
            qry = "delete from TSPL_Dispatch_Detail_BulkSale where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DispatchBulkSale, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_Code", obj.Tanker_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Dip_marking", obj.Dip_marking)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Insurance_No", obj.Insurance_No)
            clsCommon.AddColumnsForChange(coll, "Seal_No", obj.Seal_No)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.Transporter)
            If clsCommon.myLen(obj.Price_Code) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Invoice", obj.Is_Create_Auto_Invoice)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ApprovalRequired", obj.ApprovalRequired)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
            clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
            clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
            If obj.EWayBillDate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)

            If isNewEntry Then
                'clsCommon.AddColumnsForChange(coll, "Status", "Open")
                ''richa 31/12/2014
                If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.FrmDispatchBulkSale, trans) Then
                    If clsCommon.CompairString(obj.Status, "Pending") = CompairStringResult.Equal Then
                        qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                        "values ('Bulk Dispatch Order','" & clsUserMgtCode.FrmDispatchBulkSale & "','" & obj.Document_No & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                ''-----------------------------
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Dispatch_BulkSale where QC_Code ='" & obj.QC_Code & "' ", trans) < 1 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("Document already created for QC No " & obj.QC_Code & "")

                End If
            Else
                ''richa 31/12/2014
                If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.FrmDispatchBulkSale, trans) Then
                    If clsCommon.CompairString(obj.Status, "Pending") = CompairStringResult.Equal Then
                        Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.FrmDispatchBulkSale & "' and Document_No='" & obj.Document_No & "' ", trans))
                        If intExist = 0 Then
                            qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                         "values ('Bulk Dispatch Order','" & clsUserMgtCode.FrmDispatchBulkSale & "','" & obj.Document_No & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    End If
                End If
                ''-----------------------------
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale", OMInsertOrUpdate.Update, "TSPL_Dispatch_BulkSale.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsDispatchDetailBulkSale.saveData(obj.arrDispatchDetailBulkSale, obj.Document_No, trans)
            If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Approved from TSPL_Dispatch_BulkSale where Document_No='" + obj.Document_No + "'", trans), "Y") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(obj.CreditLimit) > 0 Then
                qry = "update TSPL_CUSTOMER_MASTER set Credit_Limit=Credit_Limit+" + clsCommon.myCstr(obj.CreditLimit) + " where Cust_Code='" + obj.Customer_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            ' trans.Commit()
        Catch err As Exception
            ' trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function SaveDataHistory(ByVal obj As ClsDispatchBulkSale, ByVal isHistory As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_Code", obj.Tanker_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Dip_marking", obj.Dip_marking)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Seal_No", obj.Seal_No)
            clsCommon.AddColumnsForChange(coll, "Insurance_No", obj.Insurance_No)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Invoice", obj.Is_Create_Auto_Invoice)
            clsCommon.AddColumnsForChange(coll, "ReverseFlag", obj.ReverseFlag)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ApprovalRequired", obj.ApprovalRequired)
            ''richa BHA/02/11/18-000663
            clsCommon.AddColumnsForChange(coll, "Approved", obj.Approved)
            clsCommon.AddColumnsForChange(coll, "Is_Update_Customer", 0)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)

            If isHistory Then

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale_History", OMInsertOrUpdate.Insert, "", trans)
            End If
            clsDispatchDetailBulkSale.saveDataHistory(obj.arrDispatchDetailBulkSale, obj.Document_No, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsDispatchBulkSale
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsDispatchBulkSale
        Dim obj As ClsDispatchBulkSale = Nothing
        Dim Arr As List(Of ClsDispatchBulkSale) = Nothing
        Dim qry As String = "Select ChangedTCSBaseAmount,ActualTCSBaseAmount,EWayBillDate,EWayBillNo, SalesOrder_Code,Document_No,Document_Date,Customer_Code,QC_Code,Tanker_Code,Location_Code,Dip_marking,Challan_No,Insurance_No,Seal_No,Tare_Weight,ApprovalRequired,Approved,Status,Gross_Weight,Net_Weight,Price_Code,Total_Amt,Posted,Is_Create_Auto_Invoice,Posting_Date,Modified_Date,Created_Date,ReverseFlag,Fat_Weightage=isnull(Fat_Weightage,0),Snf_Weightage=isnull(Snf_Weightage,0),Fat_Ratio=isnull(Fat_Ratio,0),Snf_Ratio  =isnull(Snf_Ratio ,0),TSPL_Dispatch_BulkSale.Tax_Group,TSPL_Dispatch_BulkSale.TAX1,TSPL_Dispatch_BulkSale.TAX1_Rate,TSPL_Dispatch_BulkSale.TAX1_Amt,TSPL_Dispatch_BulkSale.TAX1_Base_Amt,TSPL_Dispatch_BulkSale.TAX2,TSPL_Dispatch_BulkSale.TAX2_Rate,TSPL_Dispatch_BulkSale.TAX2_Amt,TSPL_Dispatch_BulkSale.TAX2_Base_Amt,TSPL_Dispatch_BulkSale.TAX3,TSPL_Dispatch_BulkSale.TAX3_Rate,TSPL_Dispatch_BulkSale.TAX3_Amt,TSPL_Dispatch_BulkSale.TAX3_Base_Amt,TSPL_Dispatch_BulkSale.TAX4,TSPL_Dispatch_BulkSale.TAX4_Rate,TSPL_Dispatch_BulkSale.TAX4_Amt,TSPL_Dispatch_BulkSale.TAX4_Base_Amt,TSPL_Dispatch_BulkSale.TAX5,TSPL_Dispatch_BulkSale.TAX5_Rate,TSPL_Dispatch_BulkSale.TAX5_Amt,TSPL_Dispatch_BulkSale.TAX5_Base_Amt,TSPL_Dispatch_BulkSale.Total_Tax_Amt,TSPL_Dispatch_BulkSale.Document_Amount,TSPL_Dispatch_BulkSale.Tax_Calculation_Type,TSPL_Dispatch_BulkSale.Transporter  from TSPL_Dispatch_BulkSale where 2=2 "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Dispatch_BulkSale.Document_No = (select MIN(Document_No) from TSPL_Dispatch_BulkSale WHERE 1=1 " + whrclas + " and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_Dispatch_BulkSale.Document_No = (select Max(Document_No) from TSPL_Dispatch_BulkSale WHERE 1=1 " + whrclas + " and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_Dispatch_BulkSale.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_Dispatch_BulkSale.Document_No = (select Min(Document_No) from TSPL_Dispatch_BulkSale where Document_No>'" + strCode + "' " + whrclas + " and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_Dispatch_BulkSale.Document_No = (select Max(Document_No) from TSPL_Dispatch_BulkSale where Document_No<'" + strCode + "' " + whrclas + " and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDispatchBulkSale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.QC_Code = clsCommon.myCstr(dt.Rows(0)("QC_Code"))
            obj.Tanker_Code = clsCommon.myCstr(dt.Rows(0)("Tanker_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Dip_marking = clsCommon.myCstr(dt.Rows(0)("Dip_marking"))
            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            obj.Insurance_No = clsCommon.myCstr(dt.Rows(0)("Insurance_No"))
            obj.Seal_No = clsCommon.myCstr(dt.Rows(0)("Seal_No"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.ApprovalRequired = clsCommon.myCstr(dt.Rows(0)("ApprovalRequired"))
            obj.Approved = clsCommon.myCstr(dt.Rows(0)("Approved"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.ReverseFlag = clsCommon.myCstr(dt.Rows(0)("ReverseFlag"))
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))
            obj.Is_Create_Auto_Invoice = clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Invoice"))
            obj.Transporter = clsCommon.myCstr(dt.Rows(0)("Transporter"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If

            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("EWayBillDate")), "dd/MMM/yyyy")
            End If
            obj.Fat_Weightage = clsCommon.myCstr(dt.Rows(0)("Fat_Weightage"))
            obj.Snf_Weightage = clsCommon.myCstr(dt.Rows(0)("Snf_Weightage"))
            obj.Fat_Ratio = clsCommon.myCstr(dt.Rows(0)("Fat_Ratio"))
            obj.Snf_Ratio = clsCommon.myCstr(dt.Rows(0)("Snf_Ratio"))

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))
            obj.arrDispatchDetailBulkSale = clsDispatchDetailBulkSale.getData(obj.Document_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_Dispatch_BulkSale where Document_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

        End If

        Try
            Dim qry As String = ""
            'clsDispatchDetailBulkSale.deleteData(strDocNo)
            ''richa agarwal ERO/11/01/19-000467 delete tanker out(if created) if we delete after reverse and unpost of dispatch
            qry = " delete from TSPL_GATEOUT_SALE where GateEntryNo in ( select GateEntry_Document_No  from TSPL_Quality_Check_BulkSale where QC_No in ( SELECT QC_Code FROM TSPL_Dispatch_BulkSale where Document_No='" + strDocNo + "'))"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Dispatch_BulkSale", "Document_No", "TSPL_Dispatch_Detail_BulkSale", "Document_No", trans)

            qry = "delete from TSPL_Dispatch_Detail_BulkSale where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Dispatch_BulkSale where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Dispatch No not found to Post")
            End If
            Dim obj As ClsDispatchBulkSale = ClsDispatchBulkSale.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmDispatchBulkSale, obj.Location_Code, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            CreateInventoryMovement(obj, trans)


            ''richa 09/03/2015
            If clsCommon.CompairString(obj.ReverseFlag, "Y") = CompairStringResult.Equal Then
                CreateJournalEntry(obj.Document_No, arrLoc, trans, "", True)
            Else
                CreateJournalEntry(obj.Document_No, arrLoc, trans, "")
            End If

            ''
            Dim ProformainVoiceNo As String = Nothing
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyMultiChamberInBulkWeighmentEntry, clsFixedParameterCode.ApplyMultiChamberInBulkWeighmentEntry, trans)) = 1, True, False)) Then
                ProformainVoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ProformaInvoiceBulkSale, "", obj.Location_Code)
            Else
                ProformainVoiceNo = ""
            End If

            Dim qry = "Update TSPL_Dispatch_BulkSale set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' , ProformaInvoice_No='" + clsCommon.myCstr(ProformainVoiceNo) + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Dispatch_BulkSale", "Document_No", trans)

            ''richa 15/09/2014
            If obj.Is_Create_Auto_Invoice Then
                Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'", trans))
                If AmountLimitInvoiceBulkSale > 0 Then
                    If obj.Total_Amt > AmountLimitInvoiceBulkSale Then
                        Throw New Exception("You cannot post this Dispatch because amount limit of invoice is less than Document amount")
                    End If
                End If
                Dim objSI As ClsInvoiceBulkSale = ConvertDispatchToSaleInvoice(obj)
                ClsInvoiceBulkSale.SaveData(objSI, True, trans)
                ClsInvoiceBulkSale.PostData("", arrLoc, objSI.Document_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Sub CreateInventoryMovement(ByVal obj As ClsDispatchBulkSale, ByVal trans As SqlTransaction)
        CreateInventoryMovement(obj, trans, Nothing)
    End Sub
    Public Shared Sub CreateInventoryMovement(ByVal obj As ClsDispatchBulkSale, ByVal trans As SqlTransaction, ByVal arrBulkSaleDoc As ArrayList)
        ''richa 08/08/2014 for INventory movement
        Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
        Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        For Each objTr As clsDispatchDetailBulkSale In obj.arrDispatchDetailBulkSale
            intCounter = intCounter + 1
            Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
            Dim strItemTypeToSave As String = ""
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                strItemTypeToSave = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                strItemTypeToSave = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                strItemTypeToSave = "FT"
            Else
                strItemTypeToSave = strItemType
            End If

            Dim objLocationDetails As New clsItemLocationDetails()

            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
            If ConvFac = 0 Then
                Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
            End If
            objLocationDetails.Item_Code = objTr.Item_Code
            objLocationDetails.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
            objLocationDetails.Location_Code = obj.Location_Code
            objLocationDetails.Location_Desc = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "' ", trans)
            objLocationDetails.Item_Qty = -1 * objTr.Qty
            objLocationDetails.Amount = -1 * objTr.Amount
            objLocationDetails.MRP = 0 * ConvFac
            objLocationDetails.ItemType = strItemTypeToSave
            ArrLocationDetails.Add(objLocationDetails)

            Dim SubLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select IsNull(Silo_No,'')  from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No=(Select LoadingTanker_No  from TSPL_Quality_Check_BulkSale where QC_No=(Select QC_Code  from TSPL_Dispatch_BulkSale where Document_No='" + obj.Document_No + "'))", trans))

            Dim objInventoryMovemnt As New clsInventoryMovementNew()
            objInventoryMovemnt.InOut = "O"
            If clsCommon.myLen(SubLocation) > 0 Then
                objInventoryMovemnt.Location_Code = SubLocation
                objInventoryMovemnt.main_location = obj.Location_Code
            Else
                objInventoryMovemnt.Location_Code = obj.Location_Code
            End If
            objInventoryMovemnt.Cust_Code = obj.Customer_Code
            objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(obj.Customer_Code, trans)

            objInventoryMovemnt.Item_Code = objTr.Item_Code
            objInventoryMovemnt.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
            Dim UseKGLitreConversionInBulkSaleAsperCLRCalculation As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UseKGLitreConversionInBulkSaleAsperCLRCalculation, clsFixedParameterCode.UseKGLitreConversionInBulkSaleAsperCLRCalculation, trans)) = 1, True, False))
            If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                objInventoryMovemnt.Qty = objTr.Qty_in_Ltr
                objInventoryMovemnt.UOM = "Ltr"
            Else
                objInventoryMovemnt.Qty = objTr.Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
            End If

            objInventoryMovemnt.MRP = objTr.Amount / objTr.Qty
            objInventoryMovemnt.Add_Cost = 0
            Dim settTankerDispatchAvgFATSNFPer As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, trans)) = 1)
            If settTankerDispatchAvgFATSNFPer Then
                ''Here exclude all document of same day.
                Dim extraWhr As String = ""
                If arrBulkSaleDoc IsNot Nothing AndAlso arrBulkSaleDoc.Count > 0 Then
                    extraWhr += " and Source_Doc_No not in (" + clsCommon.GetMulcallString(arrBulkSaleDoc) + ")"
                End If
                'Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, True, False, False, extraWhr, "MI", objTr.Item_Code, SubLocation, 1, objInventoryMovemnt.UOM, 1, 1, obj.Document_Date, obj.Document_Date, False, trans, obj.Document_No)
                Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, True, False, False, extraWhr, "MI", objTr.Item_Code, SubLocation, objInventoryMovemnt.Qty, objInventoryMovemnt.UOM, 1, 1, obj.Document_Date, obj.Document_Date, False, trans, obj.Document_No)
                objInventoryMovemnt.FAT_Per = Math.Round(objMCT.FAT_Per, 2, MidpointRounding.ToEven)
                objInventoryMovemnt.SNF_Per = Math.Round(objMCT.SNF_Per, 2, MidpointRounding.ToEven)
                objInventoryMovemnt.FAT_KG = clsBOM.GetFatSNFKG_AfterConversion(objTr.Item_Code, objInventoryMovemnt.UOM, objInventoryMovemnt.Qty, objMCT.FAT_Per, trans)
                objInventoryMovemnt.SNF_KG = clsBOM.GetFatSNFKG_AfterConversion(objTr.Item_Code, objInventoryMovemnt.UOM, objInventoryMovemnt.Qty, objMCT.SNF_Per, trans)
                objInventoryMovemnt.Net_Cost = Math.Round((objMCT.FAT_Cost * objInventoryMovemnt.FAT_KG) + (objMCT.SNF_Cost * objInventoryMovemnt.SNF_KG), 2)
                objInventoryMovemnt.Fat_Rate = objMCT.FAT_Cost
                objInventoryMovemnt.SNF_Rate = objMCT.SNF_Cost
                objInventoryMovemnt.Fat_Amt = clsCommon.myFormat(objMCT.FAT_Cost * objInventoryMovemnt.FAT_KG)
                objInventoryMovemnt.SNF_Amt = clsCommon.myFormat(objMCT.SNF_Cost * objInventoryMovemnt.SNF_KG)
                objInventoryMovemnt.Avg_Cost = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                objInventoryMovemnt.CalculateAvgCost = False
                objInventoryMovemnt.DonNotCalculateAvgFATSNFCost = True
            Else
                objInventoryMovemnt.FAT_Per = objTr.FatPer
                objInventoryMovemnt.SNF_Per = objTr.SNFPer
                objInventoryMovemnt.FAT_KG = objTr.Fat_KG
                objInventoryMovemnt.SNF_KG = objTr.SNF_KG
                objInventoryMovemnt.Net_Cost = objTr.Amount
                objInventoryMovemnt.Fat_Rate = objTr.FatRate
                objInventoryMovemnt.SNF_Rate = objTr.SNFRate
                objInventoryMovemnt.Fat_Amt = objTr.FatAmount
                objInventoryMovemnt.SNF_Amt = objTr.SNFAmount
            End If
            objInventoryMovemnt.Basic_Cost = objTr.Amount / objTr.Qty


            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "FT"
            End If
            objInventoryMovemnt.ItemType = strItemTypeToSave
            ArrInventoryMovement.Add(objInventoryMovemnt)
            ' End If
        Next
        clsInventoryMovementNew.SaveData("DispatchBS", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        'If settTankerDispatchAvgFATSNFPer Then
        '    Dim qry As String = " "
        '    qry = "select Item_Code,sum(Qty) as Qty,sum(Stock_Qty) as Stock_Qty,(case when sum(Qty)=0 then 0 else sum(Fat_KG)*100/sum(Qty) end) as Fat_Per,case when sum(Fat_KG)=0 then 0 else sum(Fat_Amt)/sum(Fat_KG) end as Fat_Rate,sum(Fat_KG) as Fat_KG,sum(Fat_Amt) as Fat_Amt,(case when sum(Qty)=0 then 0 else sum(SNF_KG)*100/sum(Qty) end) as SNF_Per,  case when sum(SNF_KG)=0 then 0 else sum(SNF_Amt)/sum(SNF_KG) end as SNF_Rate,sum(SNF_KG) as SNF_KG,sum(SNF_Amt) as SNF_Amt,sum(Amount) as Amount from (" + Environment.NewLine +
        '    "select Location_Code,Item_Code,Qty,UOM,Stock_Qty,Stock_UOM,Fat_Per,Fat_Rate,Fat_KG,Fat_Amt,SNF_Per,SNF_Rate,SNF_KG,SNF_Amt,(FAT_amt+ SNF_Amt) as Amount from tspl_inventory_movement_new where Trans_Type='DispatchBS' and Source_Doc_No='" + obj.Document_No + "'" + Environment.NewLine +
        '    ")xx group by Item_Code"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Dim coll As New Hashtable()

        '        For Each drSumm As DataRow In dt.Rows
        '            coll = New Hashtable()
        '            clsCommon.AddColumnsForChange(coll, "FatPer", Math.Round(clsCommon.myCdbl(drSumm("Fat_Per")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "SNFPer", Math.Round(clsCommon.myCdbl(drSumm("SNF_Per")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "Fat_KG", Math.Round(clsCommon.myCdbl(drSumm("Fat_KG")), 3, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "SNF_KG", Math.Round(clsCommon.myCdbl(drSumm("SNF_KG")), 3, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "FatRate", Math.Round(clsCommon.myCdbl(drSumm("Fat_Rate")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "SNFRate", Math.Round(clsCommon.myCdbl(drSumm("SNF_Rate")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "FatAmount", Math.Round(clsCommon.myCdbl(drSumm("Fat_Amt")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "SNFAmount", Math.Round(clsCommon.myCdbl(drSumm("SNF_Amt")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "Amount", Math.Round(clsCommon.myCdbl(drSumm("Amount")), 2, MidpointRounding.ToEven))
        '            If clsCommon.myCdbl(drSumm("Qty")) > 0 Then
        '                clsCommon.AddColumnsForChange(coll, "NetMilkRate", Math.Round(clsCommon.myCdbl(drSumm("Amount")) / clsCommon.myCdbl(drSumm("Qty")), 2, MidpointRounding.ToEven))
        '            Else
        '                clsCommon.AddColumnsForChange(coll, "NetMilkRate", 0)
        '            End If
        '            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_Detail_BulkSale", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "' and Item_Code='" + clsCommon.myCstr(drSumm("Item_Code")) + "'", trans)


        '            coll = New Hashtable()
        '            clsCommon.AddColumnsForChange(coll, "DispatchFatPer", Math.Round(clsCommon.myCdbl(drSumm("Fat_Per")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "DispatchSNFPer", Math.Round(clsCommon.myCdbl(drSumm("SNF_Per")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "InvoiceFatKG", Math.Round(clsCommon.myCdbl(drSumm("Fat_KG")), 3, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "InvoiceSNFKG", Math.Round(clsCommon.myCdbl(drSumm("SNF_KG")), 3, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "InvoiceAmount", Math.Round(clsCommon.myCdbl(drSumm("Amount")), 2, MidpointRounding.ToEven))
        '            clsCommon.AddColumnsForChange(coll, "DispatchAmount", Math.Round(clsCommon.myCdbl(drSumm("Amount")), 2, MidpointRounding.ToEven))
        '            If clsCommon.myCdbl(drSumm("Qty")) > 0 Then
        '                clsCommon.AddColumnsForChange(coll, "InvoiceRate", Math.Round(clsCommon.myCdbl(drSumm("Amount")) / clsCommon.myCdbl(drSumm("Qty")), 2, MidpointRounding.ToEven))
        '                clsCommon.AddColumnsForChange(coll, "DispatchRate", Math.Round(clsCommon.myCdbl(drSumm("Amount")) / clsCommon.myCdbl(drSumm("Qty")), 2, MidpointRounding.ToEven))
        '            Else
        '                clsCommon.AddColumnsForChange(coll, "InvoiceRate", 0)
        '                clsCommon.AddColumnsForChange(coll, "DispatchRate", 0)
        '            End If
        '            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_DETAIL_BulKSALE", OMInsertOrUpdate.Update, "Dispatch_Code='" + obj.Document_No + "' and Item_Code='" + clsCommon.myCstr(drSumm("Item_Code")) + "'", trans)

        '            qry = "update TSPL_INVOICE_MASTER_BULKSAlE set Total_Amt=x.totAmt from ( " + Environment.NewLine +
        '            "SELECT sum(InvoiceAmount) as totAmt,Document_No FROM TSPL_INVOICE_DETAIL_BulKSALE where Dispatch_Code='" + obj.Document_No + "' group by Document_No)x " + Environment.NewLine +
        '            "inner join TSPL_INVOICE_MASTER_BULKSAlE on TSPL_INVOICE_MASTER_BULKSAlE.Document_No=x.Document_No"
        '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '        Next
        '    End If
        'End If

    End Sub
    ''richa ERO/11/01/19-000467 14 Jan,2019
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_Dispatch_BulkSale where Document_No='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If
            Dim Qry As String = "select Posted  from TSPL_Dispatch_BulkSale where Document_No ='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select distinct Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Bulk Dispatch is used in following Bulk Sale Invoice -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Document_No"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select distinct Document_No  from TSPL_SALE_RETURN_MASTER_BULKSALE where Against ='Bulk Dispatch' and DispatchNo='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Bulk Dispatch is used in following Sale return -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Document_No"))
                Next
                Throw New Exception(Qry)
            End If


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='DS-BS' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_Dispatch_BulkSale set Posted = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_Dispatch_BulkSale", "Document_No", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Sub updateJournalEntry(ByVal Source_type As String, ByVal Doc_No As String, ByVal amount As Double, ByVal trans As SqlTransaction)
        Dim sQuery As String = String.Empty
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_journal_master where source_code='" & Source_type & "' and source_doc_no='" & Doc_No & "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
            sQuery = "update tspl_journal_master set total_debit_amt=" & amount & ",total_credit_amt=" & amount & ",modify_by='" & objCommonVar.CurrentUserCode & "',modify_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "',posting_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss") & "' where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            sQuery = "update tspl_journal_details set amount=case when coalesce(amount,0)<0 then -1 else 1 end*" & amount & " where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dt.Rows(0).Item("voucher_No")), "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
        End If
    End Sub
    Private Shared Function ConvertDispatchToSaleInvoice(ByVal objDispatch As ClsDispatchBulkSale) As ClsInvoiceBulkSale
        Dim obj As New ClsInvoiceBulkSale()
        obj = New ClsInvoiceBulkSale()
        'obj.Document_No = objDispatch.Document_No
        obj.Document_Date = objDispatch.Document_Date
        obj.Customer_Code = objDispatch.Customer_Code
        obj.Location_Code = objDispatch.Location_Code
        obj.InvoiceAgainst = "Against Dispatch"
        obj.fromdate = objDispatch.Document_Date
        obj.todate = objDispatch.Document_Date
        ' obj.Total_Amt = objDispatch.Total_Amt


        obj.Tax_Group = objDispatch.Tax_Group
        obj.TAX1 = objDispatch.TAX1
        obj.TAX1_Rate = objDispatch.TAX1_Rate
        obj.TAX1_Base_Amt = objDispatch.TAX1_Base_Amt
        obj.TAX1_Amt = objDispatch.TAX1_Amt
        obj.TAX2 = objDispatch.TAX2
        obj.TAX2_Rate = objDispatch.TAX2_Rate
        obj.TAX2_Base_Amt = objDispatch.TAX2_Base_Amt
        obj.TAX2_Amt = objDispatch.TAX2_Amt
        obj.TAX3 = objDispatch.TAX3
        obj.TAX3_Base_Amt = objDispatch.TAX3_Base_Amt
        obj.TAX3_Rate = objDispatch.TAX3_Rate
        obj.TAX3_Amt = objDispatch.TAX3_Amt
        obj.TAX4 = objDispatch.TAX4
        obj.TAX4_Rate = objDispatch.TAX4_Rate
        obj.TAX4_Base_Amt = objDispatch.TAX4_Base_Amt
        obj.TAX4_Amt = objDispatch.TAX4_Amt
        obj.TAX5 = objDispatch.TAX5
        obj.TAX5_Rate = objDispatch.TAX5_Rate
        obj.TAX5_Base_Amt = objDispatch.TAX5_Base_Amt
        obj.TAX5_Amt = objDispatch.TAX5_Amt
        obj.Total_Tax_Amt = objDispatch.Total_Tax_Amt
        obj.Document_Amount = objDispatch.Document_Amount
        obj.Tax_Calculation_Type = IIf(objDispatch.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
        obj.ActualTCSBaseAmount = objDispatch.ActualTCSBaseAmount
        obj.ChangedTCSBaseAmount = objDispatch.ChangedTCSBaseAmount

        If Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0) > clsCommon.myCdbl(objDispatch.Total_Amt) Then
            'obj.RoundOffAmount = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(clsCommon.myCdbl(objDispatch.Total_Amt)) - Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)), 2)
            obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0) - clsCommon.myCdbl(objDispatch.Total_Amt), 2)
            obj.Total_Amt = Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)
        Else
            'obj.RoundOffAmount = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(objDispatch.Total_Amt) - Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt))), 2)
            obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt)) - clsCommon.myCdbl(objDispatch.Total_Amt), 2)
            obj.Total_Amt = Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)

        End If

        If (objDispatch.arrDispatchDetailBulkSale IsNot Nothing AndAlso objDispatch.arrDispatchDetailBulkSale.Count > 0) Then
            obj.arrInvoiceDetailBulkSale = New List(Of ClsInvoiceDetailBulkSale)
            Dim objTr As ClsInvoiceDetailBulkSale
            For Each objDispatchDetail As clsDispatchDetailBulkSale In objDispatch.arrDispatchDetailBulkSale
                objTr = New ClsInvoiceDetailBulkSale
                objTr.Dispatch_Code = objDispatchDetail.Document_No
                objTr.Dispatch_Date = objDispatch.Document_Date
                objTr.Item_Code = objDispatchDetail.Item_Code
                objTr.Unit_code = objDispatchDetail.Unit_code
                objTr.Tanker_Code = objDispatch.Tanker_Code
                objTr.DispatchQty = objDispatchDetail.Qty
                objTr.DispatchFatPer = objDispatchDetail.FatPer
                objTr.DispatchSNFPer = objDispatchDetail.SNFPer
                objTr.DispatchRate = objDispatchDetail.NetMilkRate

                objTr.DispatchAmount = objDispatchDetail.Amount
                objTr.InvoiceFatPer = objDispatchDetail.FatPer
                objTr.InvoiceSNFPer = objDispatchDetail.SNFPer
                ''richa Agarwal 12/01/2014
                objTr.InvoiceFatKG = objDispatchDetail.Fat_KG
                objTr.InvoiceSNFKG = objDispatchDetail.SNF_KG
                ''---------------------
                objTr.InvoiceQty = objDispatchDetail.Qty
                objTr.InvoiceRate = objDispatchDetail.NetMilkRate
                objTr.InvoiceAmount = objDispatchDetail.Amount
                objTr.CLR = objDispatchDetail.CLR
                ''richa agarwal 4 feb,2019
                objTr.DispatchQty_in_Ltr = objDispatchDetail.Qty_in_Ltr
                objTr.InvoiceQty_in_Ltr = objDispatchDetail.Qty_in_Ltr

                objTr.TAX1 = objDispatchDetail.TAX1
                objTr.TAX1_Base_Amt = objDispatchDetail.TAX1_Base_Amt
                objTr.TAX1_Rate = objDispatchDetail.TAX1_Rate
                objTr.TAX1_Amt = objDispatchDetail.TAX1_Amt
                objTr.TAX2 = objDispatchDetail.TAX2
                objTr.TAX2_Base_Amt = objDispatchDetail.TAX2_Base_Amt
                objTr.TAX2_Rate = objDispatchDetail.TAX2_Rate
                objTr.TAX2_Amt = objDispatchDetail.TAX2_Amt
                objTr.TAX3 = objDispatchDetail.TAX3
                objTr.TAX3_Base_Amt = objDispatchDetail.TAX3_Base_Amt
                objTr.TAX3_Rate = objDispatchDetail.TAX3_Rate
                objTr.TAX3_Amt = objDispatchDetail.TAX3_Amt
                objTr.TAX4 = objDispatchDetail.TAX4
                objTr.TAX4_Base_Amt = objDispatchDetail.TAX4_Base_Amt
                objTr.TAX4_Rate = objDispatchDetail.TAX4_Rate
                objTr.TAX4_Amt = objDispatchDetail.TAX4_Amt
                objTr.TAX5 = objDispatchDetail.TAX5
                objTr.TAX5_Base_Amt = objDispatchDetail.TAX5_Base_Amt
                objTr.TAX5_Rate = objDispatchDetail.TAX5_Rate
                objTr.TAX5_Amt = objDispatchDetail.TAX5_Amt
                objTr.Total_Tax_Amt = objDispatchDetail.Total_Tax_Amt
                objTr.Item_Net_Amt = objDispatchDetail.Item_Net_Amt

                obj.arrInvoiceDetailBulkSale.Add(objTr)
            Next
        End If
        Return obj
    End Function

    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal strVourcherNoForRecreateOnly As String, Optional ByVal isGLUpdate As Boolean = False)
        Dim RecoControlACC As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            RecoControlACC = "I"
        End If
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
            Dim obj As New ClsDispatchBulkSale
            obj = ClsDispatchBulkSale.GetData(strCode, arrLoc, NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strInventoryControlAc As String = ""
            Dim strShipmentClearingAC As String = ""
            Dim dblTotalCost As Double = 0
            Dim dblCogsCost As Double = 0




            strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.arrDispatchDetailBulkSale.Item(0).Item_Code + "'", trans)
            strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location_Code, trans)

            If clsCommon.myLen(strShipmentClearingAC) = 0 Then
                Throw New Exception("Please set Shipment clearing Account for first item")
            End If

            dblCogsCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_No & "'", trans))

            Dim Acc() As String = {strShipmentClearingAC, dblCogsCost, "", "", "", "", "", "", "H"}
            ArryLstGLAC.Add(Acc)



            Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_No & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                    strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Location_Code, trans)

                    If clsCommon.myLen(strInventoryControlAc) = 0 Then
                        Throw New Exception("Please set Inventory Control Account for first item")
                    End If
                    Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", RecoControlACC}
                    ArryLstGLAC.Add(Acc1)
                    If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                        ''TEC/14/02/19-000426 by Richa on 14/02/2019
                        clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(strCode), "DispatchBS", clsCommon.myCstr(dr("Item_Code")), "", strInventoryControlAc, "", trans)
                        ''------------------
                    End If
                Next
            End If
            '' BHA/30/10/18-000646 RICHA AGARWAL SEND CUSTOMER CODE AND CUSTOMER NAME INTO JOURNAL ENTRY AND TYPE C instead of O 30 Oct,2018
            ''richa 09/03/2015

            If isGLUpdate Then
                ''richa BHA/12/11/18-000672
                Dim qry1 As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + obj.Document_No + "' "
                Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, strVoucherNo, trans, obj.Document_Date, "Journal Entry Against Dispatch Bulk Sale for Document No " + obj.Document_No + " ", "DS-BS", "DISPATCH Bulk Sale", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")
                'ClsDispatchBulkSale.updateJournalEntry("DS-BS", obj.Document_No, dblCogsCost, trans)
            ElseIf clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, strVourcherNoForRecreateOnly, trans, obj.Document_Date, "Journal Entry Against Dispatch Bulk Sale for Document No " + obj.Document_No + " ", "DS-BS", "DISPATCH Bulk Sale", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, "Journal Entry Against Dispatch Bulk Sale for Document No " + obj.Document_No + " ", "DS-BS", "DISPATCH Bulk Sale", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")
            End If

        End If
    End Sub

    Public Shared Function CheckCustomerOutstandingAmount(ByVal strCode As String, ByVal strCustomer As String, ByVal TotAmt As Double) As Double
        Dim dblAmt As Double = 0
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0


            ' dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            '"select SUM(isnull(TSPL_Dispatch_BulkSale.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_Dispatch_BulkSale " & _
            '"where TSPL_Dispatch_BulkSale.Posted=1  and TSPL_Dispatch_BulkSale.Customer_Code='" & strCustomer & "' " & _
            '" union all " & _
            '"select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
            '"TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
            '"where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> '' and Customer_Code='" & strCustomer & "' ) xxx "))
            ''richa 10/10/2014
            Dim qry As String = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            "select SUM(isnull(TSPL_Dispatch_BulkSale.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_Dispatch_BulkSale " & _
            "where TSPL_Dispatch_BulkSale.Posted=1  and TSPL_Dispatch_BulkSale.Customer_Code='" & strCustomer & "' " & _
            " union all " & _
            "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
            "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
            "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> '' and Customer_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "' ) xxx "
            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            ''=================
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'"))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and SecurityDeposit='Y' and SecurityDepositType not in ('C') and isnull(Posted,'')='Y' and Cust_Code='" & strCustomer & "'"))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_Dispatch_BulkSale where  Posted=0 and Customer_Code='" & strCustomer & "' and Document_No<>'" + strCode + "'"))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
            Return dblAmt

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dblAmt
    End Function
End Class

Public Class clsDispatchDetailBulkSale
    Public Document_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public HSN_code As String = Nothing
    Public SO_Unit_code As String = String.Empty
    Public Qty As Double = 0
    Public SO_Qty As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public Fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public CLR As Double = 0
    Public NetMilkRate As Double = 0
    Public StandardRate As Double = 0
    Public Seal_No As String = Nothing
    Public Type As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Qty_in_Ltr As Double = 0
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0

    Public Shared Function saveData(ByVal arrObj As List(Of clsDispatchDetailBulkSale), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsDispatchDetailBulkSale In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    If clsCommon.myLen(obj.SO_Unit_code) <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "SO_Unit_code", obj.Unit_code)
                    Else
                        clsCommon.AddColumnsForChange(coll, "SO_Unit_code", obj.SO_Unit_code)
                    End If
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    If obj.SO_Qty > 0 Then
                        clsCommon.AddColumnsForChange(coll, "SO_Qty", obj.SO_Qty)
                    Else
                        clsCommon.AddColumnsForChange(coll, "SO_Qty", obj.Qty)
                    End If

                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "NetMilkRate", obj.NetMilkRate)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "StandardRate", obj.StandardRate)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", obj.Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                    clsCommon.AddColumnsForChange(coll, "Qty_in_Ltr", obj.Qty_in_Ltr)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)

                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_Detail_BulkSale", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function saveDataHistory(ByVal arrObj As List(Of clsDispatchDetailBulkSale), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsDispatchDetailBulkSale In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    If clsCommon.myLen(obj.SO_Unit_code) <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "SO_Unit_code", obj.Unit_code)
                    Else
                        clsCommon.AddColumnsForChange(coll, "SO_Unit_code", obj.SO_Unit_code)
                    End If
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    If obj.SO_Qty > 0 Then
                        clsCommon.AddColumnsForChange(coll, "SO_Qty", obj.SO_Qty)
                    Else
                        clsCommon.AddColumnsForChange(coll, "SO_Qty", obj.Qty)
                    End If
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "NetMilkRate", obj.NetMilkRate)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "StandardRate", obj.StandardRate)
                    clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", obj.Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                    clsCommon.AddColumnsForChange(coll, "Qty_in_Ltr", obj.Qty_in_Ltr)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)

                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_Detail_BulkSale_History", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsDispatchDetailBulkSale)
        Try
            Dim arrObj As List(Of clsDispatchDetailBulkSale) = Nothing
            Dim obj As clsDispatchDetailBulkSale = Nothing
            Dim qry As String = "Select Qty_in_Ltr,SO_Qty,SO_Unit_code,Document_No,Item_Code,Qty,FatPer,SNFPer,Fat_KG,SNF_KG,FatAmount,SNFAmount,Amount,CLR,NetMilkRate,FatRate,SNFRate,Unit_Code,StandardRate,Seal_No,Type,Chamber_Desc,TSPL_Dispatch_Detail_BulkSale.TAX1,TSPL_Dispatch_Detail_BulkSale.TAX1_Rate,TSPL_Dispatch_Detail_BulkSale.TAX1_Amt,TSPL_Dispatch_Detail_BulkSale.TAX1_Base_Amt,TSPL_Dispatch_Detail_BulkSale.TAX2,TSPL_Dispatch_Detail_BulkSale.TAX2_Rate,TSPL_Dispatch_Detail_BulkSale.TAX2_Amt,TSPL_Dispatch_Detail_BulkSale.TAX2_Base_Amt,TSPL_Dispatch_Detail_BulkSale.TAX3,TSPL_Dispatch_Detail_BulkSale.TAX3_Rate,TSPL_Dispatch_Detail_BulkSale.TAX3_Amt,TSPL_Dispatch_Detail_BulkSale.TAX3_Base_Amt,TSPL_Dispatch_Detail_BulkSale.TAX4,TSPL_Dispatch_Detail_BulkSale.TAX4_Rate,TSPL_Dispatch_Detail_BulkSale.TAX4_Amt,TSPL_Dispatch_Detail_BulkSale.TAX4_Base_Amt,TSPL_Dispatch_Detail_BulkSale.TAX5,TSPL_Dispatch_Detail_BulkSale.TAX5_Rate,TSPL_Dispatch_Detail_BulkSale.TAX5_Amt,TSPL_Dispatch_Detail_BulkSale.TAX5_Base_Amt,TSPL_Dispatch_Detail_BulkSale.Total_Tax_Amt,TSPL_Dispatch_Detail_BulkSale.Item_Net_Amt from TSPL_Dispatch_Detail_BulkSale where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsDispatchDetailBulkSale)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDispatchDetailBulkSale()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.SO_Qty = clsCommon.myCdbl(dt.Rows(i)("SO_Qty"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
                    obj.SO_Unit_code = clsCommon.myCstr(dt.Rows(0)("SO_Unit_code"))
                    obj.Fat_KG = clsCommon.myCdbl(dt.Rows(i)("Fat_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.FatAmount = clsCommon.myCdbl(dt.Rows(i)("FatAmount"))
                    obj.SNFAmount = clsCommon.myCdbl(dt.Rows(i)("SNFAmount"))
                    obj.HSN_code = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(i)("Item_Code")), trans)

                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.FatRate = clsCommon.myCdbl(dt.Rows(i)("FatRate"))
                    obj.SNFRate = clsCommon.myCdbl(dt.Rows(i)("SNFRate"))

                    obj.CLR = clsCommon.myCdbl(dt.Rows(i)("CLR"))
                    obj.NetMilkRate = clsCommon.myCdbl(dt.Rows(i)("NetMilkRate"))
                    obj.StandardRate = clsCommon.myCdbl(dt.Rows(i)("StandardRate"))
                    obj.Chamber_Desc = clsCommon.myCstr(dt.Rows(i)("Chamber_Desc"))
                    obj.Seal_No = clsCommon.myCstr(dt.Rows(i)("Seal_No"))
                    obj.Type = clsCommon.myCstr(dt.Rows(i)("Type"))
                    obj.Qty_in_Ltr = clsCommon.myCdbl(dt.Rows(i)("Qty_in_Ltr"))
                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))

                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Shared Function deleteData(ByVal strQCNo As String) As Boolean
    '    Try
    '        Dim isDeleted As Boolean = True
    '        Dim qry As String = "delete from TSPL_Dispatch_Detail_BulkSale where Document_No='" & strQCNo & "'"
    '        isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
    '        Return isDeleted
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Function

End Class
