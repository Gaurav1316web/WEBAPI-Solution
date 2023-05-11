Imports common
Imports System.Data.SqlClient
Public Class clsTransferMaster

#Region "Variable"
    Public Transfer_No As String = Nothing
    Public Transfer_Date As Date = Nothing
    Public Posting_Date As Date = Nothing
    Public Transfer_Type As String = Nothing
    Public Load_Out_No As String = Nothing
    Public From_Location As String = Nothing
    Public To_Location As String = Nothing
    Public Price_Date As Date = Nothing
    Public Tax_Group As String = Nothing
    Public Reference As String = Nothing
    Public description As String = Nothing
    Public Route_No As String = Nothing
    Public Salesmancode As String = Nothing
    Public Price_Code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public Mode_Of_Transport As String = Nothing
    Public Km_Reading As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = Nothing
    Public TAX1_Amt As Double = Nothing
    Public Tax1_Assessable_Amt As Double = Nothing
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = Nothing
    Public TAX2_Amt As Double = Nothing
    Public Tax2_Assessable_Amt As Double = Nothing
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = Nothing
    Public TAX3_Amt As Double = Nothing
    Public Tax3_Assessable_Amt As Double = Nothing
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = Nothing
    Public TAX4_Amt As Double = Nothing
    Public Tax4_Assessable_Amt As Double = Nothing
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = Nothing
    Public TAX5_Amt As Double = Nothing
    Public Tax5_Assessable_Amt As Double = Nothing
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = Nothing
    Public TAX6_Amt As Double = Nothing
    Public Tax6_Assessable_Amt As Double = Nothing
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = Nothing
    Public TAX7_Amt As Double = Nothing
    Public Tax7_Assessable_Amt As Double = Nothing
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = Nothing
    Public TAX8_Amt As Double = Nothing
    Public Tax8_Assessable_Amt As Double = Nothing
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = Nothing
    Public TAX9_Amt As Double = Nothing
    Public Tax9_Assessable_Amt As Double = Nothing
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = Nothing
    Public TAX10_Amt As Double = Nothing
    Public Tax10_Assessable_Amt As Double = Nothing
    Public Item_Amount As Double = Nothing
    Public Total_Tax_Amount As Double = Nothing
    Public Total_Item_Amount As Double = Nothing
    Public Post As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date = Nothing
    Public Level1_User_code As String = Nothing
    Public Level2_User_code As String = Nothing
    Public Level3_User_code As String = Nothing
    Public Level4_User_code As String = Nothing
    Public Level5_User_code As String = Nothing
    Public Comp_Code As String = Nothing
    Public Load_Out_Date As Date = Nothing
    Public Is_Shipped As String = Nothing
    Public Trip_No As String = Nothing
    Public Item_Type As String = Nothing
    Public Date_Time_Removal As DateTime = Nothing
    Public Is_Complete As String = Nothing
    Public HOS As String = Nothing
    Public TDM As String = Nothing
    Public ADC As String = Nothing
    Public CE As String = Nothing
    Public EntryDateTime As DateTime = Nothing
    Public FromLoc_Desc As String = Nothing
    Public ToLoc_Desc As String = Nothing
    Public Route_Desc As String = Nothing
    Public Price_Desc As String = Nothing
    Public Vehicle_Desc As String = Nothing
    Public Printed As String = Nothing
    Public Total_Transfer_Amount As Double = 0
    Public Total_Transfer_QtyInCase As Double = 0
    Public Quick_Settlement As String = Nothing
    Public Sale_Invoice_Completed As Integer = 0
    Public Is_AgainstFormF As Integer = 0
    Public Location_Type As String = Nothing
    Public Route_Type_Id As String = Nothing
    Public Tax_Group_Type As String = Nothing
    Public Trans_Type As String = Nothing
    Public is_Auto_Created_Trans As Boolean = False
    Public Reference_Doc_No As String = Nothing
    Public Reference_Doc_No_DPL As String = Nothing
    Public Total_Basic_Amt As Double = 0
    Public Against_Indent_No As String = ""
    Public Arr As List(Of clsTransferDetails) = Nothing

    Public Location As String = Nothing
    Public Cust_Account As String = Nothing
    Public ManualTransferNo As String = "" ''Not a table column.

    Private Shared TotalCredit As Decimal = 0
    Private Shared TotalDebit As Decimal = 0
    Private Shared strExcisable As String
    Private Shared strFromLType As String
    Private Shared strToLType As String
#End Region

    Public Function DeepCopyObject(ByVal obj As clsTransferMaster, ByVal arr As List(Of clsTransferMaster)) As clsTransferMaster
        Dim objNew As clsTransferMaster = New clsTransferMaster()
        objNew.Transfer_No = obj.Transfer_No
        objNew.Transfer_Date = obj.Transfer_Date
        objNew.Posting_Date = obj.Posting_Date
        objNew.Transfer_Type = obj.Transfer_Type
        objNew.Load_Out_No = obj.Load_Out_No
        objNew.From_Location = obj.From_Location
        objNew.To_Location = obj.To_Location
        objNew.Price_Date = obj.Price_Date
        objNew.Tax_Group = obj.Tax_Group
        objNew.Reference = obj.Reference
        objNew.description = obj.description
        objNew.Route_No = obj.Route_No
        objNew.Salesmancode = obj.Salesmancode
        objNew.Price_Code = obj.Price_Code
        objNew.Vehicle_Code = obj.Vehicle_Code
        objNew.Vehicle_No = obj.Vehicle_No
        objNew.Mode_Of_Transport = obj.Mode_Of_Transport
        objNew.Km_Reading = obj.Km_Reading
        objNew.TAX1 = obj.TAX1
        objNew.TAX1_Rate = obj.TAX1_Rate
        objNew.TAX1_Amt = obj.TAX1_Amt
        objNew.Tax1_Assessable_Amt = obj.Tax1_Assessable_Amt
        objNew.TAX2 = obj.TAX2
        objNew.TAX2_Rate = obj.TAX2_Rate
        objNew.TAX2_Amt = obj.TAX2_Amt
        objNew.Tax2_Assessable_Amt = obj.Tax2_Assessable_Amt
        objNew.TAX3 = obj.TAX3
        objNew.TAX3_Rate = obj.TAX3_Rate
        objNew.TAX3_Amt = obj.TAX3_Amt
        objNew.Tax3_Assessable_Amt = obj.Tax3_Assessable_Amt
        objNew.TAX4 = obj.TAX4
        objNew.TAX4_Rate = obj.TAX4_Rate
        objNew.TAX4_Amt = obj.TAX4_Amt
        objNew.Tax4_Assessable_Amt = obj.Tax4_Assessable_Amt
        objNew.TAX5 = obj.TAX5
        objNew.TAX5_Rate = obj.TAX5_Rate
        objNew.TAX5_Amt = obj.TAX5_Amt
        objNew.Tax5_Assessable_Amt = obj.Tax5_Assessable_Amt
        objNew.TAX6 = obj.TAX6
        objNew.TAX6_Rate = obj.TAX6_Rate
        objNew.TAX6_Amt = obj.TAX6_Amt
        objNew.Tax6_Assessable_Amt = obj.Tax6_Assessable_Amt
        objNew.TAX7 = obj.TAX7
        objNew.TAX7_Rate = obj.TAX7_Rate
        objNew.TAX7_Amt = obj.TAX7_Amt
        objNew.Tax7_Assessable_Amt = obj.Tax7_Assessable_Amt
        objNew.TAX8 = obj.TAX8
        objNew.TAX8_Rate = obj.TAX8_Rate
        objNew.TAX8_Amt = obj.TAX8_Amt
        objNew.Tax8_Assessable_Amt = obj.Tax8_Assessable_Amt
        objNew.TAX9 = obj.TAX9
        objNew.TAX9_Rate = obj.TAX9_Rate
        objNew.TAX9_Amt = obj.TAX9_Amt
        objNew.Tax9_Assessable_Amt = obj.Tax9_Assessable_Amt
        objNew.TAX10 = obj.TAX10
        objNew.TAX10_Rate = obj.TAX10_Rate
        objNew.TAX10_Amt = obj.TAX10_Amt
        objNew.Tax10_Assessable_Amt = obj.Tax10_Assessable_Amt
        objNew.Item_Amount = obj.Item_Amount
        objNew.Total_Tax_Amount = obj.Total_Tax_Amount
        objNew.Total_Item_Amount = obj.Total_Item_Amount
        objNew.Post = obj.Post
        objNew.Created_By = obj.Created_By
        objNew.Created_Date = obj.Created_Date
        objNew.Modify_By = obj.Modify_By
        objNew.Modify_Date = obj.Modify_Date
        objNew.Level1_User_code = obj.Level1_User_code
        objNew.Level2_User_code = obj.Level2_User_code
        objNew.Level3_User_code = obj.Level3_User_code
        objNew.Level4_User_code = obj.Level4_User_code
        objNew.Level5_User_code = obj.Level5_User_code
        objNew.Comp_Code = obj.Comp_Code
        objNew.Load_Out_Date = obj.Load_Out_Date
        objNew.Is_Shipped = obj.Is_Shipped
        objNew.Trip_No = obj.Trip_No
        objNew.Item_Type = obj.Item_Type
        objNew.Date_Time_Removal = obj.Date_Time_Removal
        objNew.Is_Complete = obj.Is_Complete
        objNew.HOS = obj.HOS
        objNew.TDM = obj.TDM
        objNew.ADC = obj.ADC
        objNew.CE = obj.CE
        objNew.EntryDateTime = obj.EntryDateTime
        objNew.FromLoc_Desc = obj.FromLoc_Desc
        objNew.ToLoc_Desc = obj.ToLoc_Desc
        objNew.Route_Desc = obj.Route_Desc
        objNew.Price_Desc = obj.Price_Desc
        objNew.Vehicle_Desc = obj.Vehicle_Desc
        objNew.Printed = obj.Printed
        objNew.Total_Transfer_Amount = obj.Total_Transfer_Amount
        objNew.Total_Transfer_QtyInCase = obj.Total_Transfer_QtyInCase
        objNew.Quick_Settlement = obj.Quick_Settlement
        objNew.Sale_Invoice_Completed = obj.Sale_Invoice_Completed
        objNew.Is_AgainstFormF = obj.Is_AgainstFormF
        objNew.Location_Type = obj.Location_Type
        objNew.Route_Type_Id = obj.Route_Type_Id
        objNew.Tax_Group_Type = obj.Tax_Group_Type
        objNew.Trans_Type = obj.Trans_Type
        objNew.is_Auto_Created_Trans = obj.is_Auto_Created_Trans
        objNew.Reference_Doc_No = obj.Reference_Doc_No
        objNew.Total_Basic_Amt = obj.Total_Basic_Amt
        objNew.Against_Indent_No = obj.Against_Indent_No
        objNew.Location = obj.Location
        objNew.Cust_Account = obj.Cust_Account
        objNew.Arr = New List(Of clsTransferDetails)()
        AddDetailobject(objNew, obj)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objAnother As clsTransferMaster In arr
                AddDetailobject(objNew, objAnother)
            Next
        End If
        Return objNew
    End Function
    Private Function AddDetailobject(ByRef objNew As clsTransferMaster, ByVal obj As clsTransferMaster)
        For Each objtr As clsTransferDetails In obj.Arr
            If objNew.Arr IsNot Nothing AndAlso objNew.Arr.Count > 0 Then
                Dim isfound As Boolean = False
                For ii As Integer = 0 To objNew.Arr.Count - 1
                    If clsCommon.CompairString(objtr.Item_Code, objNew.Arr(ii).Item_Code) = CompairStringResult.Equal AndAlso objtr.MRP = objNew.Arr(ii).MRP Then
                        objNew.Arr(ii).Item_Qty += objtr.Item_Qty
                        isfound = True
                        Exit For
                    End If
                Next
                If isfound Then
                    Continue For
                End If
            End If

           
            Dim objtrNew As New clsTransferDetails()
            objtrNew.Line_No = objNew.Arr.Count + 1
            objtrNew.Transfer_No = objtr.Transfer_No
            objtrNew.Item_Code = objtr.Item_Code
            objtrNew.Item_Desc = objtr.Item_Desc
            objtrNew.Price_Date = objtr.Price_Date
            objtrNew.Item_Qty = objtr.Item_Qty
            objtrNew.MRP = objtr.MRP
            objtrNew.Item_Price = objtr.Item_Price
            objtrNew.Amount = objtr.Amount
            objtrNew.Disc_Perc = objtr.Disc_Perc
            objtrNew.Disc_Amount = objtr.Disc_Amount
            objtrNew.Net_Amount = objtr.Net_Amount
            objtrNew.Pending_Qty = objtr.Pending_Qty
            objtrNew.TAX1 = objtr.TAX1
            objtrNew.TAX1_Rate = objtr.TAX1_Rate
            objtrNew.TAX1_Amt = objtr.TAX1_Amt
            objtrNew.Tax1_Assessable_Amt = objtr.Tax1_Assessable_Amt
            objtrNew.TAX2 = objtr.TAX2
            objtrNew.TAX2_Rate = objtr.TAX2_Rate
            objtrNew.TAX2_Amt = objtr.TAX2_Amt
            objtrNew.Tax2_Assessable_Amt = objtr.Tax2_Assessable_Amt
            objtrNew.TAX3 = objtr.TAX3
            objtrNew.TAX3_Rate = objtr.TAX3_Rate
            objtrNew.TAX3_Amt = objtr.TAX3_Amt
            objtrNew.Tax3_Assessable_Amt = objtr.Tax3_Assessable_Amt
            objtrNew.TAX4 = objtr.TAX4
            objtrNew.TAX4_Rate = objtr.TAX4_Rate
            objtrNew.TAX4_Amt = objtr.TAX4_Amt
            objtrNew.Tax4_Assessable_Amt = objtr.Tax4_Assessable_Amt
            objtrNew.TAX5 = objtr.TAX5
            objtrNew.TAX5_Rate = objtr.TAX5_Rate
            objtrNew.TAX5_Amt = objtr.TAX5_Amt
            objtrNew.Tax5_Assessable_Amt = objtr.Tax5_Assessable_Amt
            objtrNew.TAX6 = objtr.TAX6
            objtrNew.TAX6_Rate = objtr.TAX6_Rate
            objtrNew.TAX6_Amt = objtr.TAX6_Amt
            objtrNew.Tax6_Assessable_Amt = objtr.Tax6_Assessable_Amt
            objtrNew.TAX7 = objtr.TAX7
            objtrNew.TAX7_Rate = objtr.TAX7_Rate
            objtrNew.TAX7_Amt = objtr.TAX7_Amt
            objtrNew.Tax7_Assessable_Amt = objtr.Tax7_Assessable_Amt
            objtrNew.TAX8 = objtr.TAX8
            objtrNew.TAX8_Rate = objtr.TAX8_Rate
            objtrNew.TAX8_Amt = objtr.TAX8_Amt
            objtrNew.Tax8_Assessable_Amt = objtr.Tax8_Assessable_Amt
            objtrNew.TAX9 = objtr.TAX9
            objtrNew.TAX9_Rate = objtr.TAX9_Rate
            objtrNew.TAX9_Amt = objtr.TAX9_Amt
            objtrNew.Tax9_Assessable_Amt = objtr.Tax9_Assessable_Amt
            objtrNew.TAX10 = objtr.TAX10
            objtrNew.TAX10_Rate = objtr.TAX10_Rate
            objtrNew.TAX10_Amt = objtr.TAX10_Amt
            objtrNew.Tax10_Assessable_Amt = objtr.Tax10_Assessable_Amt
            objtrNew.Total_Tax = objtr.Total_Tax
            objtrNew.Total_Item_Amt = objtr.Total_Item_Amt
            objtrNew.Complete = objtr.Complete
            objtrNew.Assessable_Amt = objtr.Assessable_Amt
            objtrNew.LoadIn_Qty = objtr.LoadIn_Qty
            objtrNew.Uom = objtr.Uom
            objtrNew.Breakage = objtr.Breakage
            objtrNew.Basic_Price = objtr.Basic_Price
            objtrNew.Batch_No = objtr.Batch_No
            objtrNew.BasicPrice_WithTax = objtr.BasicPrice_WithTax
            objtrNew.Empty_Value = objtr.Empty_Value
            objtrNew.TPT_Value = objtr.TPT_Value
            objtrNew.Burst = objtr.Burst
            objtrNew.Leak = objtr.Leak
            objtrNew.Shortage = objtr.Shortage
            objtrNew.Pending_Balance_In_Bottle = objtr.Pending_Balance_In_Bottle
            objtrNew.Total_Item_Cost = objtr.Total_Item_Cost
            objtrNew.MRP_In_Bottle = objtr.MRP_In_Bottle
            objtrNew.Total_QtyInCase = objtr.Total_QtyInCase
            objtrNew.Basic_Amt = objtr.Basic_Amt
            objNew.Arr.Add(objtrNew)
        Next
        Return objNew
    End Function
    Public Function SaveData(ByVal obj As clsTransferMaster, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsTransferMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Transfer(Load-Out)", obj.Location, obj.Transfer_Date, trans)
            ElseIf clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Transfer(Load-In)", obj.To_Location, obj.Transfer_Date, trans)
            End If
            
            Dim qry As String = ""
            If clsCommon.myLen(obj.Transfer_No) > 0 Then
                qry = "select Post from TSPL_TRANSFER_HEAD where Transfer_No='" + obj.Transfer_No + "'"
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Already Posted Trasaction")
                End If
            End If
            qry = "delete from TSPL_TRANSFER_DETAIL where Transfer_No='" + obj.Transfer_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.myLen(obj.ManualTransferNo) > 0 Then
                    obj.Transfer_No = obj.ManualTransferNo
                Else
                    If obj.is_Auto_Created_Trans AndAlso clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                        obj.Transfer_No = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.LoadOut, clsDocTransactionType.TransferAutoRoute, obj.From_Location)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "Empty") = CompairStringResult.Equal Then
                        obj.Transfer_No = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, IIf(clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal, clsDocType.LoadOut, clsDocType.LoadIn), clsDocTransactionType.TranferEmpty, obj.From_Location)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "Full") = CompairStringResult.Equal Then
                        Dim LType As String = connectSql.RunScalar(trans, "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.From_Location + "' ")
                        If IIf(clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal, clsDocType.LoadOut, clsDocType.LoadIn) = clsDocType.LoadOut AndAlso (LType = "Y" OrElse LType = "T") Then
                            obj.Transfer_No = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.SaleInvoice, clsDocTransactionType.SaleInvoiceExcise, obj.From_Location)
                        Else
                            If clsCommon.myLen(obj.Route_No) > 0 Then
                                If clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                                    obj.Transfer_No = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, IIf(clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal, clsDocType.LoadOut, clsDocType.LoadIn), clsDocTransactionType.TransferFull, obj.To_Location)
                                Else
                                    obj.Transfer_No = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, IIf(clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal, clsDocType.LoadOut, clsDocType.LoadIn), clsDocTransactionType.TransferFull, obj.From_Location)
                                End If
                            Else
                                obj.Transfer_No = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, IIf(clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal, clsDocType.LoadOut, clsDocType.LoadIn), clsDocTransactionType.TransferFull, obj.From_Location)
                            End If
                        End If
                    End If
                End If
            End If
            If clsCommon.myLen(obj.Transfer_No) <= 0 Then
                Throw New Exception("Transfer No not found")
            End If



            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Transfer_Date", clsCommon.GetPrintDate(obj.EntryDateTime, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "EntryDateTime", clsCommon.GetPrintDate(obj.EntryDateTime, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Date_Time_Removal", clsCommon.GetPrintDate(obj.EntryDateTime, "hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Transfer_Type", obj.Transfer_Type)
            clsCommon.AddColumnsForChange(coll, "Load_Out_No", obj.Load_Out_No)
            If clsCommon.myLen(obj.Load_Out_No) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Load_Out_Date", clsCommon.GetPrintDate(obj.Load_Out_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Load_Out_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Reference_Doc_No", obj.Reference_Doc_No)
            clsCommon.AddColumnsForChange(coll, "Reference_Doc_No_DPL", obj.Reference_Doc_No_DPL)
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.From_Location)
            clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Location)
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "description", obj.description)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Salesmancode", obj.Salesmancode)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Km_Reading", obj.Km_Reading)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax1_Assessable_Amt", obj.Tax1_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax2_Assessable_Amt", obj.Tax2_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax3_Assessable_Amt", obj.Tax3_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax4_Assessable_Amt", obj.Tax4_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax5_Assessable_Amt", obj.Tax5_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax6_Assessable_Amt", obj.Tax6_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax7_Assessable_Amt", obj.Tax7_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax8_Assessable_Amt", obj.Tax8_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax9_Assessable_Amt", obj.Tax9_Assessable_Amt)

            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax10_Assessable_Amt", obj.Tax10_Assessable_Amt)


            clsCommon.AddColumnsForChange(coll, "Item_Amount", obj.Item_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amount", obj.Total_Tax_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Amount", obj.Total_Item_Amount)
            clsCommon.AddColumnsForChange(coll, "Post", obj.Post)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            clsCommon.AddColumnsForChange(coll, "Is_Shipped", obj.Is_Shipped)
            clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)

            clsCommon.AddColumnsForChange(coll, "Is_Complete", obj.Is_Complete)
            clsCommon.AddColumnsForChange(coll, "HOS", obj.HOS)
            clsCommon.AddColumnsForChange(coll, "TDM", obj.TDM)
            clsCommon.AddColumnsForChange(coll, "ADC", obj.ADC)
            clsCommon.AddColumnsForChange(coll, "CE", obj.CE)



            clsCommon.AddColumnsForChange(coll, "FromLoc_Desc", obj.FromLoc_Desc)
            clsCommon.AddColumnsForChange(coll, "ToLoc_Desc", obj.ToLoc_Desc)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "Price_Desc", obj.Price_Desc)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Desc", obj.Vehicle_Desc)
            clsCommon.AddColumnsForChange(coll, "Printed", obj.Printed)
            clsCommon.AddColumnsForChange(coll, "Total_Transfer_Amount", obj.Total_Transfer_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Transfer_QtyInCase", obj.Total_Transfer_QtyInCase)
            clsCommon.AddColumnsForChange(coll, "Quick_Settlement", obj.Quick_Settlement)
            clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Completed", obj.Sale_Invoice_Completed)
            clsCommon.AddColumnsForChange(coll, "Is_AgainstFormF", obj.Is_AgainstFormF)
            clsCommon.AddColumnsForChange(coll, "Location_Type", obj.Location_Type)
            clsCommon.AddColumnsForChange(coll, "Route_Type_Id", obj.Route_Type_Id)
            clsCommon.AddColumnsForChange(coll, "Tax_Group_Type", obj.Tax_Group_Type)
            clsCommon.AddColumnsForChange(coll, "is_Auto_Created_Trans", IIf(obj.is_Auto_Created_Trans, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
            clsCommon.AddColumnsForChange(coll, "Against_Indent_No", obj.Against_Indent_No, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Transfer_No", obj.Transfer_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSFER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSFER_HEAD", OMInsertOrUpdate.Update, "TSPL_TRANSFER_HEAD.Transfer_No='" + obj.Transfer_No + "'", trans)
            End If
            clsTransferDetails.SaveData(obj.Transfer_No, obj.Arr, trans)

            If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                UpdateBalanceQtyOfTransferLO(obj.Transfer_No, trans)
            End If

            If Not obj.is_Auto_Created_Trans AndAlso clsCommon.myLen(obj.Route_No) > 0 AndAlso clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Trans_Type, "Excise") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Trans_Type, "Depot") = CompairStringResult.Equal Then
                    Dim strSalesmanLocation As String = clsFixedParameter.GetData(clsFixedParameterType.SalesmanPhysicalLocation, clsFixedParameterCode.SalesmanPhysicalLocation, trans)
                    If clsCommon.myLen(strSalesmanLocation) <= 0 Then
                        Throw New Exception("Please set Salesman physical location fixed parameter")
                    End If
                    Dim objSMLI As clsTransferMaster = createLoadIn(obj, strSalesmanLocation, isNewEntry, trans)

                    'Dim objSMLO As clsTransferMaster = createLoadout(obj, strSalesmanLocation, isNewEntry, trans)

                    'qry = "Update TSPL_TRANSFER_HEAD set Reference_Doc_No='" + objSMLO.Transfer_No + "' where Transfer_No='" + obj.Transfer_No + "'"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function createLoadIn(ByVal obj As clsTransferMaster, ByVal strToLocation As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As clsTransferMaster
        ''Create Load in Transaction
        Dim objSMLI As clsTransferMaster = obj.DeepCopyObject(obj, Nothing)
        Dim qry As String = " select Transfer_No from  TSPL_TRANSFER_HEAD where Load_Out_No ='" + obj.Transfer_No + "'"
        objSMLI.Transfer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        objSMLI.From_Location = obj.To_Location
        objSMLI.FromLoc_Desc = clsLocation.GetName(obj.To_Location, trans)
        objSMLI.To_Location = strToLocation
        objSMLI.ToLoc_Desc = clsLocation.GetName(strToLocation, trans)
        objSMLI.Transfer_Type = "LI"
        objSMLI.Load_Out_No = obj.Transfer_No
        objSMLI.Reference_Doc_No = ""
        objSMLI.Item_Amount = 0
        objSMLI.Total_Tax_Amount = 0
        objSMLI.Total_Item_Amount = 0
        objSMLI.Against_Indent_No = ""
        objSMLI.Tax_Group = ""
        objSMLI.ManualTransferNo = ""
        objSMLI.TAX1 = ""
        objSMLI.TAX1_Amt = 0
        objSMLI.Tax1_Assessable_Amt = 0
        objSMLI.TAX1_Rate = 0

        objSMLI.TAX2 = ""
        objSMLI.TAX2_Amt = 0
        objSMLI.Tax2_Assessable_Amt = 0
        objSMLI.TAX2_Rate = 0

        objSMLI.TAX3 = ""
        objSMLI.TAX3_Amt = 0
        objSMLI.Tax3_Assessable_Amt = 0
        objSMLI.TAX3_Rate = 0

        objSMLI.TAX4 = ""
        objSMLI.TAX4_Amt = 0
        objSMLI.Tax4_Assessable_Amt = 0
        objSMLI.TAX4_Rate = 0

        objSMLI.TAX5 = ""
        objSMLI.TAX5_Amt = 0
        objSMLI.Tax5_Assessable_Amt = 0
        objSMLI.TAX5_Rate = 0

        objSMLI.TAX6 = ""
        objSMLI.TAX6_Amt = 0
        objSMLI.Tax6_Assessable_Amt = 0
        objSMLI.TAX6_Rate = 0
        objSMLI.is_Auto_Created_Trans = True
        For ii As Integer = 0 To objSMLI.Arr.Count - 1
            objSMLI.Arr(ii).LoadIn_Qty = objSMLI.Arr(ii).Item_Qty
            objSMLI.Arr(ii).BasicPrice_WithTax = objSMLI.Arr(ii).Basic_Price + (objSMLI.Arr(ii).Total_Tax / objSMLI.Arr(ii).Item_Qty)

            objSMLI.Arr(ii).TAX1 = ""
            objSMLI.Arr(ii).TAX1_Amt = 0
            objSMLI.Arr(ii).Tax1_Assessable_Amt = 0
            objSMLI.Arr(ii).TAX1_Rate = 0

            objSMLI.Arr(ii).TAX2 = ""
            objSMLI.Arr(ii).TAX2_Amt = 0
            objSMLI.Arr(ii).Tax2_Assessable_Amt = 0
            objSMLI.Arr(ii).TAX2_Rate = 0

            objSMLI.Arr(ii).TAX3 = ""
            objSMLI.Arr(ii).TAX3_Amt = 0
            objSMLI.Arr(ii).Tax3_Assessable_Amt = 0
            objSMLI.Arr(ii).TAX3_Rate = 0

            objSMLI.Arr(ii).TAX4 = ""
            objSMLI.Arr(ii).TAX4_Amt = 0
            objSMLI.Arr(ii).Tax4_Assessable_Amt = 0
            objSMLI.Arr(ii).TAX4_Rate = 0

            objSMLI.Arr(ii).TAX5 = ""
            objSMLI.Arr(ii).TAX5_Amt = 0
            objSMLI.Arr(ii).Tax5_Assessable_Amt = 0
            objSMLI.Arr(ii).TAX5_Rate = 0

            objSMLI.Arr(ii).TAX6 = ""
            objSMLI.Arr(ii).TAX6_Amt = 0
            objSMLI.Arr(ii).Tax6_Assessable_Amt = 0
            objSMLI.Arr(ii).TAX6_Rate = 0

            objSMLI.Arr(ii).Amount = objSMLI.Arr(ii).LoadIn_Qty * objSMLI.Arr(ii).MRP
            objSMLI.Arr(ii).Net_Amount = objSMLI.Arr(ii).Amount
            objSMLI.Arr(ii).Total_Item_Amt = objSMLI.Arr(ii).Amount
            objSMLI.Arr(ii).Total_Item_Cost = 0
           
            objSMLI.Arr(ii).Total_Tax = 0
            ''Calculate Header amount
            objSMLI.Item_Amount += objSMLI.Arr(ii).Amount
            objSMLI.Total_Item_Amount += objSMLI.Arr(ii).Amount
            ''End of Calculate Header amount
        Next
        objSMLI.SaveData(objSMLI, isNewEntry, trans)
        ''End of Create Load in Transaction

        Return objSMLI
    End Function
    'Public Shared Function createLoadout(ByVal obj As clsTransferMaster, ByVal strFromLocation As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As clsTransferMaster
    '    ''Create Load Out Transaction

    '    Dim qry As String = "select Transfer_No from TSPL_TRANSFER_HEAD where Transfer_Type='LO' and Transfer_Date='" + clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy") + "' and Route_No='" + obj.Route_No + "' and Salesmancode='" + obj.Salesmancode + "' and  From_Location in ('DPL','GNT') and Transfer_No not in ('" + obj.Transfer_No + "')"
    '    Dim dtAL As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    Dim strAnotherTransfer As String = ""
    '    If dtAL IsNot Nothing AndAlso dtAL.Rows.Count > 0 Then
    '        If dtAL.Rows.Count > 1 Then
    '            Throw New Exception("Can't create more than 2 loadout")
    '        End If
    '        strAnotherTransfer = clsCommon.myCstr(dtAL.Rows(0)("Transfer_No"))
    '    End If


    '    Dim objAnother As clsTransferMaster = Nothing
    '    If clsCommon.myLen(strAnotherTransfer) > 0 Then
    '        objAnother = clsTransferMaster.GetData(strAnotherTransfer, trans)
    '        isNewEntry = False
    '    End If

    '    Dim objSMLO As clsTransferMaster = obj.DeepCopyObject(obj, objAnother)
    '    objSMLO.Transfer_No = obj.Reference_Doc_No

    '    If clsCommon.CompairString(obj.Trans_Type, "Excise") = CompairStringResult.Equal Then
    '        objSMLO.Reference_Doc_No = obj.Transfer_No
    '        If objAnother IsNot Nothing Then
    '            objSMLO.Reference_Doc_No_DPL = objAnother.Transfer_No
    '            objSMLO.Transfer_No = objAnother.Reference_Doc_No
    '        End If
    '    Else
    '        objSMLO.Reference_Doc_No_DPL = obj.Transfer_No
    '        If objAnother IsNot Nothing Then
    '            objSMLO.Reference_Doc_No = objAnother.Transfer_No
    '            objSMLO.Transfer_No = objAnother.Reference_Doc_No
    '        End If
    '    End If

    '    ''Handle for delete data function
    '    If clsCommon.CompairString(objSMLO.Reference_Doc_No_DPL, objSMLO.Transfer_No) = CompairStringResult.Equal Then
    '        objSMLO.Reference_Doc_No_DPL = ""
    '    End If
    '    If clsCommon.CompairString(objSMLO.Reference_Doc_No, objSMLO.Transfer_No) = CompairStringResult.Equal Then
    '        objSMLO.Reference_Doc_No = ""
    '    End If
    '    objSMLO.Post = "N"
    '    ''End of Handle for delete data function

    '    objSMLO.From_Location = strFromLocation
    '    objSMLO.FromLoc_Desc = clsLocation.GetName(strFromLocation, trans)
    '    qry = "Select employee_code,NonPrice_Code from TSPL_ROUTE_MASTER where route_no = '" + obj.Route_No + "' "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '        Throw New Exception("Route details not found for route " + obj.Route_No)
    '    End If

    '    objSMLO.To_Location = obj.Salesmancode

    '    If clsCommon.myLen(objSMLO.To_Location) <= 0 Then
    '        Throw New Exception("Salesman not found ")
    '    End If
    '    objSMLO.ToLoc_Desc = clsEmployeeMaster.GetName(obj.Salesmancode, trans)


    '    objSMLO.Price_Code = clsCommon.myCstr(dt.Rows(0)("NonPrice_Code"))
    '    If clsCommon.myLen(objSMLO.Price_Code) <= 0 Then
    '        Throw New Exception("Non Excisable Price not found for route No " + obj.Route_No)
    '    End If
    '    objSMLO.Location_Type = "Logical"
    '    objSMLO.Trans_Type = "Route"
    '    objSMLO.Item_Amount = 0
    '    objSMLO.Total_Tax_Amount = 0
    '    objSMLO.Total_Item_Amount = 0
    '    objSMLO.ManualTransferNo = ""
    '    objSMLO.Tax_Group = ""
    '    objSMLO.Against_Indent_No = ""
    '    objSMLO.TAX1 = ""
    '    objSMLO.TAX1_Amt = 0
    '    objSMLO.Tax1_Assessable_Amt = 0
    '    objSMLO.TAX1_Rate = 0

    '    objSMLO.TAX2 = ""
    '    objSMLO.TAX2_Amt = 0
    '    objSMLO.Tax2_Assessable_Amt = 0
    '    objSMLO.TAX2_Rate = 0

    '    objSMLO.TAX3 = ""
    '    objSMLO.TAX3_Amt = 0
    '    objSMLO.Tax3_Assessable_Amt = 0
    '    objSMLO.TAX3_Rate = 0

    '    objSMLO.TAX4 = ""
    '    objSMLO.TAX4_Amt = 0
    '    objSMLO.Tax4_Assessable_Amt = 0
    '    objSMLO.TAX4_Rate = 0

    '    objSMLO.TAX5 = ""
    '    objSMLO.TAX5_Amt = 0
    '    objSMLO.Tax5_Assessable_Amt = 0
    '    objSMLO.TAX5_Rate = 0

    '    objSMLO.TAX6 = ""
    '    objSMLO.TAX6_Amt = 0
    '    objSMLO.Tax6_Assessable_Amt = 0
    '    objSMLO.TAX6_Rate = 0
    '    objSMLO.Total_Basic_Amt = 0
    '    objSMLO.Total_Transfer_Amount = 0
    '    objSMLO.is_Auto_Created_Trans = True
    '    For ii As Integer = 0 To objSMLO.Arr.Count - 1
    '        objSMLO.Arr(ii).LoadIn_Qty = 0
    '        objSMLO.Arr(ii).TAX1 = ""
    '        objSMLO.Arr(ii).TAX1_Amt = 0
    '        objSMLO.Arr(ii).Tax1_Assessable_Amt = 0
    '        objSMLO.Arr(ii).TAX1_Rate = 0

    '        objSMLO.Arr(ii).TAX2 = ""
    '        objSMLO.Arr(ii).TAX2_Amt = 0
    '        objSMLO.Arr(ii).Tax2_Assessable_Amt = 0
    '        objSMLO.Arr(ii).TAX2_Rate = 0

    '        objSMLO.Arr(ii).TAX3 = ""
    '        objSMLO.Arr(ii).TAX3_Amt = 0
    '        objSMLO.Arr(ii).Tax3_Assessable_Amt = 0
    '        objSMLO.Arr(ii).TAX3_Rate = 0

    '        objSMLO.Arr(ii).TAX4 = ""
    '        objSMLO.Arr(ii).TAX4_Amt = 0
    '        objSMLO.Arr(ii).Tax4_Assessable_Amt = 0
    '        objSMLO.Arr(ii).TAX4_Rate = 0

    '        objSMLO.Arr(ii).TAX5 = ""
    '        objSMLO.Arr(ii).TAX5_Amt = 0
    '        objSMLO.Arr(ii).Tax5_Assessable_Amt = 0
    '        objSMLO.Arr(ii).TAX5_Rate = 0

    '        objSMLO.Arr(ii).TAX6 = ""
    '        objSMLO.Arr(ii).TAX6_Amt = 0
    '        objSMLO.Arr(ii).Tax6_Assessable_Amt = 0
    '        objSMLO.Arr(ii).TAX6_Rate = 0

    '        objSMLO.Arr(ii).Total_Tax = 0

    '        objSMLO.Arr(ii).Amount = objSMLO.Arr(ii).Item_Qty * objSMLO.Arr(ii).MRP
    '        objSMLO.Arr(ii).Net_Amount = objSMLO.Arr(ii).Amount
    '        objSMLO.Arr(ii).Total_Item_Amt = objSMLO.Arr(ii).Amount
    '        objSMLO.Arr(ii).Total_Item_Cost = 0

    '        qry = "select top 1 TSPL_ITEM_PRICE_MASTER.Start_Date,TSPL_ITEM_PRICE_MASTER.Abatement,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,"
    '        qry += " case when TSPL_ITEM_MASTER.NoMRP=1 then  NetLTPT+(isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0) +isnull(TAX3_Amt,0) +isnull(TAX4_Amt,0) +isnull(TAX5_Amt,0) +isnull(TAX6_Amt,0) +isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) else NetLTPT end as BasicPrice_WithTax,"
    '        qry += " isnull((TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle+TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell),0) as Empty_Value,"
    '        qry += " (case when PC1.TPT_Type='Y' then ISNULL(Price_Amount1,0) else 0 end"
    '        qry += " +case when PC2.TPT_Type='Y' then ISNULL(Price_Amount2,0) else 0 end "
    '        qry += " + case when PC3.TPT_Type='Y' then ISNULL(Price_Amount3,0) else 0 end "
    '        qry += " + case when PC4.TPT_Type='Y' then ISNULL(Price_Amount4,0) else 0 end "
    '        qry += " + case when PC5.TPT_Type='Y' then ISNULL(Price_Amount5,0) else 0 end "
    '        qry += " + case when PC6.TPT_Type='Y' then ISNULL(Price_Amount6,0) else 0 end "
    '        qry += " + case when PC7.TPT_Type='Y' then ISNULL(Price_Amount7,0) else 0 end "
    '        qry += " + case when PC8.TPT_Type='Y' then ISNULL(Price_Amount8,0) else 0 end "
    '        qry += " + case when PC9.TPT_Type='Y' then ISNULL(Price_Amount9,0) else 0 end "
    '        qry += " + case when PC10.TPT_Type='Y' then ISNULL(Price_Amount10,0) else 0 end ) as TPT_Value"
    '        qry += " from TSPL_ITEM_PRICE_MASTER "
    '        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC1 on PC1.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp1"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC2 on PC2.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp2"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC3 on PC3.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp3"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC4 on PC4.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp4"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC5 on PC5.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp5"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC6 on PC6.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp6"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC7 on PC7.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp7"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC8 on PC8.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp8"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC9 on PC9.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp9"
    '        qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC10 on PC10.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp10"
    '        qry += " where TSPL_ITEM_PRICE_MASTER.Item_Code='" + objSMLO.Arr(ii).Item_Code + "' and TSPL_ITEM_PRICE_MASTER.Price_Code='" + objSMLO.Price_Code + "' and TSPL_ITEM_PRICE_MASTER.UOM='" + objSMLO.Arr(ii).Uom + "' and TSPL_ITEM_PRICE_MASTER.Start_Date<='" + clsCommon.GetPrintDate(objSMLO.Transfer_Date, "dd/MMM/yyyy") + "' AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net='" + clsCommon.myCstr(objSMLO.Arr(ii).MRP) + "' order by TSPL_ITEM_PRICE_MASTER.Start_Date desc"
    '        dt = clsDBFuncationality.GetDataTable(qry, trans)

    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            Throw New Exception("Price Details not found for item " + objSMLO.Arr(ii).Item_Code + " and price code" + objSMLO.Price_Code + " and UOM " + objSMLO.Arr(ii).Uom + " start date on or before " + clsCommon.GetPrintDate(objSMLO.Transfer_Date, "dd/MMM/yyyy"))
    '        End If
    '        objSMLO.Arr(ii).Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Abatement"))

    '        objSMLO.Arr(ii).Basic_Price = clsCommon.myCdbl(dt.Rows(0)("Item_Basic_Price"))
    '        objSMLO.Arr(ii).Price_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))


    '        objSMLO.Arr(ii).BasicPrice_WithTax = clsCommon.myCdbl(dt.Rows(0)("BasicPrice_WithTax"))
    '        objSMLO.Arr(ii).Basic_Amt = objSMLO.Arr(ii).Item_Qty * objSMLO.Arr(ii).BasicPrice_WithTax
    '        objSMLO.Arr(ii).Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
    '        objSMLO.Arr(ii).TPT_Value = clsCommon.myCdbl(dt.Rows(0)("TPT_Value"))


    '        ''Calculate Header amount
    '        objSMLO.Item_Amount += objSMLO.Arr(ii).Amount
    '        objSMLO.Total_Item_Amount += objSMLO.Arr(ii).Amount
    '        objSMLO.Total_Basic_Amt += objSMLO.Arr(ii).Basic_Amt
    '        objSMLO.Total_Transfer_Amount += objSMLO.Arr(ii).Item_Qty * (objSMLO.Arr(ii).BasicPrice_WithTax + objSMLO.Arr(ii).Empty_Value)
    '        ''End of Calculate Header amount
    '    Next
    '    objSMLO.SaveData(objSMLO, isNewEntry, trans)
    '    Return objSMLO
    'End Function

    Private Function UpdateBalanceQtyOfTransferLO(ByVal strTransferNo As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(strTransferNo) > 0 Then
            Dim qry As String = "Update TSPL_TRANSFER_DETAIL set TSPL_TRANSFER_DETAIL.Pending_Qty=xxx.BalanceQty ,TSPL_TRANSFER_DETAIL.Pending_Balance_In_Bottle=ROUND(xxx.BalanceINBottelQty ,0)  from TSPL_TRANSFER_DETAIL inner Join ("
            qry += " select xxxx.Transfer_No,MAX(xxxx.Salesmancode) as Salesmancode,MAX(xxxx.SalemanName) as SalemanName,MAX(xxxx.Route_No) as Route_No,MAX(xxxx.Route_Desc) as Route_Desc,xxxx.Item_Code,Price_Date,sum(xxxx.Item_Qty * case when RI =1 then 1 else case when RI in (2,3,4) then -1 else 0 end end) as BalanceQty,"
            qry += " ROUND((sum(xxxx.Item_Qty * case when RI =1 then 1 else case when RI in (2,3,4) then -1 else 0 end end)*(select Top 1 Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  TSPL_ITEM_UOM_DETAIL.Item_Code=xxxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code in('FB','EB'))),0) as BalanceINBottelQty,"
            qry += " sum(Pending_Qty) as BalanceQtyInDataBase,xxxx.MRP"
            qry += "  from ("
            qry += " select TSPL_TRANSFER_DETAIL.Transfer_No,TSPL_TRANSFER_DETAIL.Transfer_No as DocNo,Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,Item_Qty,1 as RI,1 as Chk,TSPL_TRANSFER_DETAIL.Pending_Qty,TSPL_TRANSFER_HEAD.Salesmancode,TSPL_EMPLOYEE_MASTER.Emp_Name as SalemanName,TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Route_Desc ,MRP"
            qry += " from TSPL_TRANSFER_DETAIL "
            qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No"
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_TRANSFER_HEAD.Salesmancode"
            qry += " where  TSPL_TRANSFER_HEAD.Transfer_Type='LO' and  TSPL_TRANSFER_HEAD.Transfer_No='" + strTransferNo + "'"
            qry += " union all "
            qry += " select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No ,TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,(ISNULL( TSPL_TRANSFER_DETAIL.Burst,0)+isnull(TSPL_TRANSFER_DETAIL.Leak,0)+isnull(TSPL_TRANSFER_DETAIL.Shortage,0)+TSPL_TRANSFER_DETAIL.LoadIn_Qty) /Conversion_Factor  as Item_Qty  ,4 as RI,0 as Chk,0 as Pending_Qty,'' as Salesmancode,'' as SalemanName,'' as Route_No,'' as Route_Desc ,((MRP * Conversion_Factor )+CASE when TSPL_TRANSFER_DETAIL.Uom='EB' then 100 else 0 end)  AS MRP"
            qry += " from TSPL_TRANSFER_DETAIL "
            qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom"
            qry += " where Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Load_Out_No='" + strTransferNo + "'"
            qry += " ) xxxx group by Transfer_No,Item_Code,Price_Date,MRP having SUM(chk)>0 "
            qry += " )xxx on xxx.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and  xxx.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and xxx.Price_Date=TSPL_TRANSFER_DETAIL.Price_Date  and xxx.MRP=TSPL_TRANSFER_DETAIL.MRP"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal Trans As SqlTransaction) As clsTransferMaster
        Dim obj As clsTransferMaster = Nothing
        Dim qry As String = "select * from tspl_transfer_head where transfer_no = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsTransferMaster
            If dt.Rows(0)("Transfer_Date") IsNot DBNull.Value Then
                obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"))
            End If
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            obj.Transfer_Type = clsCommon.myCstr(dt.Rows(0)("Transfer_Type"))
            obj.Load_Out_No = clsCommon.myCstr(dt.Rows(0)("Load_Out_No"))
            obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
            obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
            obj.Price_Date = clsCommon.myCstr(dt.Rows(0)("Price_Date"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Salesmancode = clsCommon.myCstr(dt.Rows(0)("Salesmancode"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Km_Reading = clsCommon.myCstr(dt.Rows(0)("Km_Reading"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))

            obj.Reference_Doc_No = clsCommon.myCstr(dt.Rows(0)("Reference_Doc_No"))
            obj.Reference_Doc_No_DPL = clsCommon.myCstr(dt.Rows(0)("Reference_Doc_No_DPL"))

            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.Tax1_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax1_Assessable_Amt"))

            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.Tax2_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax2_Assessable_Amt"))

            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.Tax3_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax3_Assessable_Amt"))

            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.Tax4_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax4_Assessable_Amt"))

            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.Tax5_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax5_Assessable_Amt"))

            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.Tax6_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax6_Assessable_Amt"))

            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.Tax7_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax7_Assessable_Amt"))

            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.Tax8_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax8_Assessable_Amt"))

            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.Tax9_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax9_Assessable_Amt"))

            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Tax10_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax10_Assessable_Amt"))


            obj.Item_Amount = clsCommon.myCdbl(dt.Rows(0)("Item_Amount"))
            obj.Total_Tax_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amount"))
            obj.Total_Item_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Amount"))
            obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
            obj.Modify_Date = clsCommon.myCDate(dt.Rows(0)("Modify_Date"))
            obj.Level1_User_code = clsCommon.myCstr(dt.Rows(0)("Level1_User_code"))
            obj.Level2_User_code = clsCommon.myCstr(dt.Rows(0)("Level2_User_code"))
            obj.Level3_User_code = clsCommon.myCstr(dt.Rows(0)("Level3_User_code"))
            obj.Level4_User_code = clsCommon.myCstr(dt.Rows(0)("Level4_User_code"))
            obj.Level5_User_code = clsCommon.myCstr(dt.Rows(0)("Level5_User_code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))

            If dt.Rows(0)("Load_Out_Date") IsNot DBNull.Value Then
                obj.Load_Out_Date = clsCommon.myCDate(dt.Rows(0)("Load_Out_Date"))
            End If


            obj.Is_Shipped = clsCommon.myCstr(dt.Rows(0)("Is_Shipped"))
            obj.Trip_No = clsCommon.myCstr(dt.Rows(0)("Trip_No"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Date_Time_Removal = clsCommon.myCstr(dt.Rows(0)("Date_Time_Removal"))
            obj.Is_Complete = clsCommon.myCstr(dt.Rows(0)("Is_Complete"))
            obj.HOS = clsCommon.myCstr(dt.Rows(0)("HOS"))
            obj.TDM = clsCommon.myCstr(dt.Rows(0)("TDM"))
            obj.ADC = clsCommon.myCstr(dt.Rows(0)("ADC"))
            obj.CE = clsCommon.myCstr(dt.Rows(0)("CE"))
            obj.EntryDateTime = clsCommon.myCDate(dt.Rows(0)("EntryDateTime"))
            obj.FromLoc_Desc = clsCommon.myCstr(dt.Rows(0)("FromLoc_Desc"))
            obj.ToLoc_Desc = clsCommon.myCstr(dt.Rows(0)("ToLoc_Desc"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Price_Desc = clsCommon.myCstr(dt.Rows(0)("Price_Desc"))
            obj.Vehicle_Desc = clsCommon.myCstr(dt.Rows(0)("Vehicle_Desc"))
            obj.Total_Transfer_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Transfer_Amount"))
            obj.Total_Basic_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Basic_Amt"))
            obj.Against_Indent_No = clsCommon.myCstr(dt.Rows(0)("Against_Indent_No"))

            obj.is_Auto_Created_Trans = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Auto_Created_Trans")) = 1, True, False)
            qry = "select * from TSPL_TRANSFER_detail where Transfer_No='" + strCode + "' order by Line_No"
            dt = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsTransferDetails)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsTransferDetails = New clsTransferDetails()
                    objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objtr.Transfer_No = clsCommon.myCstr(dr("Transfer_No"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    objtr.Item_Qty = clsCommon.myCdbl(dr("Item_Qty"))
                    objtr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objtr.Item_Price = clsCommon.myCdbl(dr("Item_Price"))
                    objtr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objtr.Disc_Perc = clsCommon.myCdbl(dr("Disc_Perc"))
                    objtr.Disc_Amount = clsCommon.myCdbl(dr("Disc_Amount"))
                    objtr.Net_Amount = clsCommon.myCdbl(dr("Net_Amount"))
                    objtr.Pending_Qty = clsCommon.myCdbl(dr("Pending_Qty"))
                    objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objtr.Tax1_Assessable_Amt = clsCommon.myCdbl(dr("TAX1_Assessable_Amt"))
                    objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objtr.Tax2_Assessable_Amt = clsCommon.myCdbl(dr("TAX2_Assessable_Amt"))
                    objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objtr.Tax3_Assessable_Amt = clsCommon.myCdbl(dr("TAX3_Assessable_Amt"))
                    objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objtr.Tax4_Assessable_Amt = clsCommon.myCdbl(dr("TAX4_Assessable_Amt"))
                    objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objtr.Tax5_Assessable_Amt = clsCommon.myCdbl(dr("TAX5_Assessable_Amt"))
                    objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objtr.Tax6_Assessable_Amt = clsCommon.myCdbl(dr("TAX6_Assessable_Amt"))
                    objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objtr.Tax7_Assessable_Amt = clsCommon.myCdbl(dr("TAX7_Assessable_Amt"))
                    objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objtr.Tax8_Assessable_Amt = clsCommon.myCdbl(dr("TAX8_Assessable_Amt"))
                    objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objtr.Tax9_Assessable_Amt = clsCommon.myCdbl(dr("TAX9_Assessable_Amt"))
                    objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objtr.Tax10_Assessable_Amt = clsCommon.myCdbl(dr("TAX10_Assessable_Amt"))
                    objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objtr.Total_Tax = clsCommon.myCdbl(dr("Total_Tax"))
                    objtr.Total_Item_Amt = clsCommon.myCdbl(dr("Total_Item_Amt"))
                    objtr.Complete = clsCommon.myCstr(dr("Complete"))
                    objtr.Assessable_Amt = clsCommon.myCdbl(dr("Assessable_Amt"))
                    objtr.LoadIn_Qty = clsCommon.myCdbl(dr("LoadIn_Qty"))
                    objtr.Uom = clsCommon.myCstr(dr("Uom"))
                    objtr.Breakage = clsCommon.myCdbl(dr("Breakage"))
                    objtr.Burst = clsCommon.myCdbl(dr("Burst"))
                    objtr.Basic_Price = clsCommon.myCdbl(dr("Basic_Price"))
                    objtr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    objtr.BasicPrice_WithTax = clsCommon.myCdbl(dr("BasicPrice_WithTax"))
                    objtr.Empty_Value = clsCommon.myCdbl(dr("Empty_Value"))
                    objtr.TPT_Value = clsCommon.myCdbl(dr("TPT_Value"))
                    objtr.Leak = clsCommon.myCdbl(dr("Leak"))
                    objtr.Shortage = clsCommon.myCdbl(dr("Shortage"))
                    objtr.Pending_Balance_In_Bottle = clsCommon.myCdbl(dr("Pending_Balance_In_Bottle"))
                    objtr.Total_Item_Cost = clsCommon.myCdbl(dr("Total_Item_Cost"))
                    objtr.MRP_In_Bottle = clsCommon.myCdbl(dr("MRP_In_Bottle"))

                    objtr.Basic_Amt = clsCommon.myCdbl(dr("Basic_Amt"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function postTransfer(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postTransfer(strCode, trans)
            'Throw New Exception("exception")
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postTransfer(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim sql As String = Nothing
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Transfer No. not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            Dim obj As clsTransferMaster = clsTransferMaster.GetData(strCode, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Transfer_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Transfer(Load-Out)", obj.Location, obj.Transfer_Date, trans)
            ElseIf clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Transfer(Load-In)", obj.To_Location, obj.Transfer_Date, trans)
            End If

            If clsCommon.CompairString(obj.Post, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Modify_Date, "dd/MM/yyyy"))
            End If
            If clsCommon.CompairString("Y", clsFixedParameter.GetData(clsFixedParameterType.PrintVerify, clsFixedParameterCode.Transfer, trans)) = CompairStringResult.Equal Then
                sql = "select isnull(printed,'N') from tspl_transfer_head where transfer_no='" + strCode + "'"
                Dim PrintStatus As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans))
                If PrintStatus = "N" Then
                    Throw New Exception("Print '" + strCode + "' Transfer Report Before To Post.")
                End If
            End If

           


            If Not postTransferNew(trans, obj, False) Then
                Throw New Exception("Error in posting")
            End If
            If clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                sql = "update TSPL_TRANSFER_HEAD set Is_Complete ='Y' where  Transfer_Type ='LO' and Transfer_No ='" + obj.Transfer_No + "' and Post ='Y'"
                connectSql.RunSqlTransaction(trans, sql)
            End If
            sql = "UPDATE tspl_transfer_head SET post='Y',Posting_Date=Transfer_Date ,Modify_By='" + obj.Modify_By + "' ,Modify_Date='" + clsCommon.GetPrintDate(obj.Modify_Date, "dd/MMM/yyyy") + "' WHERE Transfer_No='" + obj.Transfer_No + "'"
            connectSql.RunSqlTransaction(trans, sql)
            If clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                Dim objtemp As New clsTransferMaster()
                objtemp.UpdateBalanceQtyOfTransferLO(obj.Load_Out_No, trans)
            End If

            If Not obj.is_Auto_Created_Trans AndAlso clsCommon.myLen(obj.Route_No) > 0 AndAlso clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Trans_Type, "Excise") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Trans_Type, "Depot") = CompairStringResult.Equal Then
                    ''Posting Loadin
                    sql = " select Transfer_No from  TSPL_TRANSFER_HEAD where Load_Out_No ='" + obj.Transfer_No + "'"
                    Dim strLoadinNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans))
                    postTransfer(strLoadinNo, trans)
                    ''End of Posting Loadin no

                    ''Posting Loadout
                    'postTransfer(obj.Reference_Doc_No, trans)
                    ''End of Posting Loadout
                End If
            End If
            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function postTransferNew(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster, ByVal isForJournalEntryOnly As Boolean) As Boolean
        Dim LoadInAmt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Total_Transfer_Amount from TSPL_TRANSFER_HEAD where Transfer_No='" + obj.Transfer_No + "'", trans))
        Dim strTranferDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Transfer_Date from tspl_transfer_head WHERE Transfer_No='" + obj.Transfer_No + "'", trans)), "dd/MMM/yyyy")
        Dim Sql As String = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
        strExcisable = connectSql.RunScalar(trans, Sql)
        Sql = "select Location_Type from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'"
        strToLType = connectSql.RunScalar(trans, Sql)
        Sql = "select Location_Type from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
        strFromLType = connectSql.RunScalar(trans, Sql)

        If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Transfer(Load-Out)", obj.Location, obj.Transfer_Date, trans)
        ElseIf clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Transfer(Load-In)", obj.Location, obj.Transfer_Date, trans)
        End If

        If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
            If strExcisable = "T" Then
                Dim strItemType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type  from TSPL_TRANSFER_HEAD where Transfer_No='" + obj.Transfer_No + "'", trans))
                If clsCommon.CompairString(obj.Item_Type, "Empty") = CompairStringResult.Equal Then
                    If Not postTransferLogical(trans, obj, strTranferDate, isForJournalEntryOnly) Then
                        Return False
                    End If
                Else
                    If Not postTransfer(trans, obj, strTranferDate, isForJournalEntryOnly) Then
                        Return False
                    End If
                End If
                If Not isForJournalEntryOnly Then
                    If Not funitem_inventoryinsert(trans, obj, strTranferDate) Then
                        Return False
                    End If
                End If

            ElseIf strExcisable = "F" AndAlso strToLType = "Physical" Then
                If Not postTransferLogical(trans, obj, strTranferDate, isForJournalEntryOnly) Then
                    Return False
                End If
                If Not isForJournalEntryOnly Then
                    If Not funitem_inventoryinsert_loadIN(trans, obj, strTranferDate) Then
                        Return False
                    End If
                End If

            ElseIf strExcisable = "F" AndAlso strToLType = "Logical" Then
                If Not postTransferLogical(trans, obj, strTranferDate, isForJournalEntryOnly) Then
                    Return False
                End If
                If Not isForJournalEntryOnly Then
                    If Not funitem_inventoryinsert_loadIN(trans, obj, strTranferDate) Then
                        Return False
                    End If
                End If

            End If
        ElseIf clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal AndAlso LoadInAmt > 0 Then
            If strExcisable = "F" AndAlso strToLType = "Physical" Then
                If Not postTransferLogical(trans, obj, strTranferDate, isForJournalEntryOnly) Then
                    Return False
                End If
                If Not isForJournalEntryOnly Then
                    If Not funitem_inventoryinsert_loadIN(trans, obj, strTranferDate) Then
                        Return False
                    End If
                End If

            ElseIf strExcisable = "F" AndAlso strToLType = "Logical" Then
                If Not postTransferLogical(trans, obj, strTranferDate, isForJournalEntryOnly) Then
                    Return False
                End If
                If Not isForJournalEntryOnly Then
                    If Not funitem_inventoryinsert_loadIN(trans, obj, strTranferDate) Then
                        Return False
                    End If
                End If

            End If
        End If

        Return True
    End Function

    

    Private Shared Function postTransferLogical(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster, ByVal strTranferDate As String, ByVal isForJournalEntryOnly As Boolean) As Boolean
        Dim count As Integer = 0
        Dim Sql As String
        Dim AccSet As String
        Dim lineNo As Integer = 1
        Dim fromItemQty As Decimal = 0
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim TotalAmt As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0
        Dim toShipmentCogs As Decimal = 0
        'Dim frmj As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        'Dim StrVoucher As String = transportSql.fnAutoGenerateNo(trans, obj.Transfer_Date, False, obj.From_Location, False)
        Dim StrVoucher As String = transportSql.fnAutoGenerateNo(trans, obj.Transfer_Date, clsDocTransactionType.JournalEntryOther, obj.From_Location, False)
        Sql = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'MM-TF'"
        Dim strSourceDesc As String = connectSql.RunScalar(trans, Sql)
        Dim strInvoiceNo As String = obj.Transfer_No
        ' Dim tolocation As String
        Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
        Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1

        Dim frmloc As String = FunReturnLocation(obj.From_Location, trans)
        Dim toloc As String = FunReturnLocation(obj.To_Location, trans)
        strTranferDate = clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy")

        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "MM-TF"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", obj.description), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", obj.description), New SqlParameter("@Comments", obj.description), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", obj.To_Location), New SqlParameter("@CustVend_Name", obj.ToLoc_Desc), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", obj.Total_Item_Amount), New SqlParameter("@Total_Credit_Amt", obj.Total_Item_Amount), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentUserCode))
        Dim voucherdesc As String = Nothing
        If obj.Transfer_Type = "LO" Then
            voucherdesc = "Transfer For Load Out: " + clsCommon.myCstr(obj.Transfer_No) + " from " + obj.From_Location + " and to " + obj.To_Location + " "
        Else
            voucherdesc = "Transfer For Load In: " + clsCommon.myCstr(obj.Transfer_No) + " from " + obj.From_Location + " and to " + obj.To_Location + " "
        End If
        connectSql.RunSqlTransaction(trans, "update TSPL_JOURNAL_MASTER set Voucher_Desc ='" + voucherdesc + "' where Voucher_No = '" + StrVoucher + "'")
        Dim IsCreateJEForTransfer As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrreateTransferShipmentJE, clsFixedParameterCode.CrreateTransferShipmentJE, trans)) = 1, True, False)
        IsCreateJEForTransfer = IsCreateJEForTransfer AndAlso clsCommon.CompairString(obj.Trans_Type, "Route") = CompairStringResult.Equal

        If obj.Transfer_Type = "LI" Then

            If Not isForJournalEntryOnly Then
                If Not FunItemLocationUpdateLoadIn(trans, obj) Then
                    Return False
                End If
            End If

            Dim isApplyShell As Boolean = False
            Sql = "select 1 from TSPL_TRANSFER_DETAIL where Transfer_No='" + obj.Transfer_No + "' and Uom='SH'"
            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                isApplyShell = True
            End If

            Dim strFromInvAcc As String = ""
            Dim strFromInvAccDesc As String = ""
            Dim strToInvAcc As String = ""
            Dim strToInvAccDesc As String = ""
            Dim strShpClrAcc As String = ""
            Dim strBrkgAcc As String = ""
            Dim strBrkgAccDesc As String = ""
            Dim strShrtgAcc As String = ""
            Dim strShrtgAccDesc As String = ""
            Dim strShpClrAccDesc As String = ""
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
            Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.To_Location) + "'"
            Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)

            If strExcisable = "F" AndAlso strFromLType = "Logical" Then
                Sql = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
            Else
                Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                              " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                               " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
            End If

            If obj.Transfer_Type = "LI" Then
                strFromInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            Else
                strFromInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If


            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
            strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
            If strFromInvAccDesc Is Nothing Then
                Throw New Exception("Inventory Control Account not found.")

            End If
            If strExcisable = "F" AndAlso strToLType = "Logical" Then
                Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
           " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
            " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"

                strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            Else
                If IsCreateJEForTransfer Then
                    Sql = "select TSPL_SALES_ACCOUNTS.Suspence_Account from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code  where TSPL_ITEM_MASTER.Item_Code='" + obj.Arr.Item(0).Item_Code + "'"
                Else
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                   " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
                End If
                strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            End If
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
            strToInvAccDesc = connectSql.RunScalar(trans, Sql)
            If strToInvAccDesc Is Nothing Then
                Throw New Exception("Reserve Stock Account not found.")
            End If

            Sql = "SELECT PA.Breakage_Gl_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
               " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
            strBrkgAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strBrkgAcc + "'"
            strBrkgAccDesc = connectSql.RunScalar(trans, Sql)
            If strToInvAccDesc Is Nothing Then
                Throw New Exception("Reserve Stock Account not found.")
            End If

            Sql = "SELECT PA.Assembly_Cost_Credit FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
              " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
               " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
            strShrtgAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strShrtgAcc + "'"
            strShrtgAccDesc = connectSql.RunScalar(trans, Sql)
            If strToInvAccDesc Is Nothing Then
                Throw New Exception("Reserve Stock Account not found.")
            End If

            Dim totItmQty As Decimal = 0
            Dim totBrkgQty As Decimal = 0
            Dim totShrtgQty As Decimal = 0
            Dim totBrkgCost As Decimal = 0
            Dim totShrtgCost As Decimal = 0
            Dim totShellCost As Decimal = 0
            Dim totBrkgCostWithEmpty As Decimal = 0
            Dim totShrtgCostWithEmpty As Decimal = 0
            Dim Uom As String = Nothing
            Dim totAmtWithBrkgLkg As Decimal = 0
            Dim ShtgCostQrgnl As Decimal = 0
            Dim BrkgCostQrgnl As Decimal = 0
            Dim itmQty As Decimal
            Dim ItmCost As String
            Dim BrkgQty As Decimal
            'Dim BrkgCost As Decimal
            Dim ShrtgQty As Decimal
            'Dim ShrtgCost As Decimal
            Dim LNo As Integer = 1
            For Each objTr As clsTransferDetails In obj.Arr
                ItmCost = objTr.BasicPrice_WithTax
                If ItmCost = "" Or ItmCost = Nothing Then
                    ItmCost = CDec(objTr.BasicPrice_WithTax)
                End If
                itmQty = objTr.LoadIn_Qty
                BrkgQty = objTr.Burst + objTr.Leak
                ShrtgQty = objTr.Shortage
                Dim dh As String = objTr.Item_Code
                Dim CnvrsnFctr As String = connectSql.RunScalar(trans, "select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and UOM_Code = '" + Convert.ToString(objTr.Uom) + "' AND UM.Create_Price = 'Y'")
                Uom = CStr(objTr.Uom)
                If Not Uom = "SH" OrElse clsCommon.CompairString(obj.Item_Type, "Empty") = CompairStringResult.Equal Then
                    totItmQty = totItmQty + itmQty
                    totBrkgQty = totBrkgQty + BrkgQty
                    totShrtgQty = totShrtgQty + ShrtgQty
                    LNo += 1
                    TotalAmt = TotalAmt + Math.Round((CDec(ItmCost) * itmQty), 2)
                    totBrkgCost = totBrkgCost + Math.Round((CDec(ItmCost) * BrkgQty), 2)
                    totShrtgCost = totShrtgCost + Math.Round((CDec(ItmCost) * ShrtgQty), 2)
                End If
                Dim tRate As Double = 0 ' clsCommon.myCdbl(IIf(clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal, objTr.BasicPrice_WithTax, objTr.Empty_Value))
                If clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal Then
                    totBrkgCostWithEmpty += Math.Round(CDec((objTr.Burst) * tRate), 2)
                    totShrtgCostWithEmpty += Math.Round(CDec((objTr.Shortage) * tRate), 2)
                Else
                    totBrkgCostWithEmpty += Math.Round((CDec(ItmCost) * BrkgQty), 2) + Math.Round(CDec((objTr.Burst) * tRate), 2)
                    totShrtgCostWithEmpty += Math.Round((CDec(ItmCost) * ShrtgQty), 2) + Math.Round(CDec((objTr.Shortage) * tRate), 2)
                End If
            Next
            totAmtWithBrkgLkg = TotalAmt + totBrkgCost + totShrtgCost

            If clsCommon.CompairString(obj.Trans_Type, "Route") = CompairStringResult.Equal Then
                TotalAmt += totBrkgCost + totShrtgCost
                totBrkgCost = 0
                totShrtgCost = 0
            End If


            Dim EmptyAmt As Decimal = 0
            Dim EntrySheLLDiff As Decimal = 0
            Dim ActualEmptyAmt As Decimal = 0
            If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                For Each objtr As clsTransferDetails In obj.Arr
                    Dim tcost As Double = IIf(clsCommon.CompairString(objtr.Uom, "SH") = CompairStringResult.Equal, objtr.BasicPrice_WithTax, objtr.Empty_Value)
                    EmptyAmt = EmptyAmt + Math.Round(CDec((objtr.Item_Qty) * tcost), 2)
                    ''ActualEmptyAmt = ActualEmptyAmt + Math.Round(CDec((objtr.LoadIn_Qty + objtr.Leak) * tcost), 2)
                Next
                ActualEmptyAmt = EmptyAmt
                EntrySheLLDiff = (totAmtWithBrkgLkg + EmptyAmt) - (TotalAmt + totBrkgCostWithEmpty + totShrtgCostWithEmpty + ActualEmptyAmt)
                totShrtgCostWithEmpty = totShrtgCostWithEmpty
                ShtgCostQrgnl = totShrtgCostWithEmpty
                BrkgCostQrgnl = totBrkgCostWithEmpty
            Else
                ShtgCostQrgnl = totShrtgCost
                BrkgCostQrgnl = totBrkgCost
            End If

            If obj.Item_Type = "Empty" Then
                ShtgCostQrgnl = totShrtgCost
                BrkgCostQrgnl = totBrkgCost
            End If



            strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, IIf(clsCommon.CompairString(obj.Trans_Type, "Route") = CompairStringResult.Equal, obj.To_Location, obj.From_Location), trans)
            Dim obj3 As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
            strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
            If clsCommon.myCdbl(totAmtWithBrkgLkg) > 0 Then
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", (totAmtWithBrkgLkg) * (-1)), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj3.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj3.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj3.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj3.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj3.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj3.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj3.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj3.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj3.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj3.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj3.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj3.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj3.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj3.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj3.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj3.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj3.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj3.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj3.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj3.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj3.Account_Seg_Desc10))
                lineNo = lineNo + 1
            End If

            'Dim dblTotalLOExciseAmt As Double = 0
            'If clsCommon.CompairString(obj.Trans_Type, "Excise") = CompairStringResult.Equal Then
            '    Dim objLO As clsTransferMaster = clsTransferMaster.GetData(obj.Load_Out_No, trans)
            '    If obj IsNot Nothing Then
            '        Sql = " select AC,max(Tax_Net_Payable) as Tax_Net_Payable, -1*sum(Amount) as Amount from ("
            '        Sql += " select SUBSTRING( TSPL_JOURNAL_DETAILS.Account_code,0,LEN(TSPL_JOURNAL_DETAILS.Account_code)-3) as AC, TSPL_JOURNAL_DETAILS.Account_code,'' as Tax_Net_Payable,Amount,0 as CHK from TSPL_JOURNAL_DETAILS "
            '        Sql += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No"
            '        Sql += " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + objLO.Transfer_No + "' and Source_Code='MM-TF' "
            '        Sql += " union all "
            '        Sql += " select SUBSTRING( Tax_Liability_Account,0,LEN(Tax_Liability_Account)-3) as AC,Tax_Liability_Account as Account_code ,Tax_Net_Payable,0 as Amount,1 as CHK from TSPL_TAX_MASTER where Tax_Code in ('" + objLO.TAX1 + "','" + objLO.TAX2 + "','" + objLO.TAX3 + "')"
            '        Sql += " )xxx group by AC having SUM(CHK)>0"
            '        Dim dtLO As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
            '        For Each drLO As DataRow In dtLO.Rows
            '            Dim strNewAC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(drLO("Tax_Net_Payable")), obj.To_Location, trans)
            '            Dim obj123 As Accountsegment = Accountsegment.Getaccountcodedesc(strNewAC, trans)
            '            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNewAC + "'"
            '            strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
            '            dblTotalLOExciseAmt += clsCommon.myCdbl(drLO("Amount"))
            '            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNewAC), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", clsCommon.myCdbl(drLO("Amount"))), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj123.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj123.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj123.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj123.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj123.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj123.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj123.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj123.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj123.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj123.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj123.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj123.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj123.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj123.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj123.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj123.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj123.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj123.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj123.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj123.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj123.Account_Seg_Desc10))
            '            lineNo = lineNo + 1
            '        Next
            '    End If
            'End If

            strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.To_Location, trans)
            Dim obj4 As Accountsegment = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
            TotalAmt = TotalAmt '+ IIf(isApplyShell, EntrySheLLDiff, 0)
            If clsCommon.myCdbl(TotalAmt) > 0 Then
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", TotalAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj4.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj4.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj4.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj4.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj4.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj4.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj4.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj4.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj4.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj4.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj4.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj4.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj4.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj4.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj4.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj4.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj4.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj4.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj4.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj4.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj4.Account_Seg_Desc10))
                lineNo = lineNo + 1
            End If

            If BrkgCostQrgnl > 0 Then
                If obj.Transfer_Type = "LI" Then
                    strBrkgAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBrkgAcc, obj.To_Location, trans)
                Else
                    strBrkgAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBrkgAcc, obj.From_Location, trans)
                End If
                obj3 = Accountsegment.Getaccountcodedesc(strBrkgAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strBrkgAcc), New SqlParameter("@Account_Desc", strBrkgAccDesc), New SqlParameter("@Amount", BrkgCostQrgnl), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj3.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj3.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj3.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj3.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj3.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj3.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj3.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj3.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj3.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj3.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj3.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj3.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj3.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj3.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj3.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj3.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj3.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj3.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj3.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj3.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj3.Account_Seg_Desc10))
                lineNo = lineNo + 1
            End If

            If ShtgCostQrgnl > 0 Then
                If obj.Transfer_Type = "LI" Then
                    strShrtgAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strShrtgAcc, obj.To_Location, trans)
                Else
                    strShrtgAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strShrtgAcc, obj.From_Location, trans)
                End If
                obj3 = Accountsegment.Getaccountcodedesc(strShrtgAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strShrtgAcc), New SqlParameter("@Account_Desc", strShrtgAccDesc), New SqlParameter("@Amount", ShtgCostQrgnl), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj3.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj3.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj3.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj3.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj3.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj3.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj3.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj3.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj3.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj3.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj3.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj3.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj3.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj3.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj3.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj3.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj3.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj3.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj3.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj3.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj3.Account_Seg_Desc10))
                lineNo = lineNo + 1
            End If


            '***************By Manoj: To add GL-Entry Transfer Filled and Empty Account
            If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                If obj.Item_Type = "Full" Then
                    Dim strFrmFilledAcc As String = Nothing
                    Dim Loc1 As String = "select From_Location  from TSPL_TRANSFER_HEAD where Transfer_No ='" + obj.Transfer_No + "'"
                    Loc1 = connectSql.RunScalar(trans, Loc1)
                    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc1 + "'"
                    Dim strFrmFilledAccFirst As String = connectSql.RunScalar(trans, Sql)
                    If strFrmFilledAccFirst Is Nothing Or strFrmFilledAccFirst = "" Then
                        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
                    Else
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                        Dim strFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                        Dim obj8 As Accountsegment = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                        If clsCommon.myCdbl(totAmtWithBrkgLkg) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", totAmtWithBrkgLkg), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj8.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj8.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj8.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj8.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj8.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj8.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj8.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj8.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj8.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj8.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj8.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj8.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj8.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj8.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj8.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj8.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj8.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj8.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj8.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj8.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj8.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
                    Dim strToFilledAcc As String = connectSql.RunScalar(trans, Sql)
                    If strToFilledAcc Is Nothing Then
                        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
                    Else
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                        Dim strTOFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                        Dim obj7 As Accountsegment = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                        If clsCommon.myCdbl(totAmtWithBrkgLkg) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", totAmtWithBrkgLkg * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj7.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj7.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj7.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj7.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj7.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj7.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj7.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj7.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj7.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj7.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj7.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj7.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj7.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj7.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj7.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj7.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj7.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj7.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj7.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj7.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj7.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                ElseIf obj.Item_Type = "Empty" Then
                    Dim strFrmFilledAcc As String = Nothing
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
                    Dim strFrmFilledAccFirst As String = connectSql.RunScalar(trans, Sql)
                    If strFrmFilledAccFirst Is Nothing Or strFrmFilledAccFirst = "" Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)

                    Else
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                        Dim strFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                        Dim obj8 As Accountsegment = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                        If clsCommon.myCdbl(totAmtWithBrkgLkg) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", totAmtWithBrkgLkg), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj8.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj8.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj8.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj8.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj8.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj8.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj8.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj8.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj8.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj8.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj8.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj8.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj8.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj8.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj8.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj8.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj8.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj8.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj8.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj8.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj8.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
                    Dim strToFilledAcc As String = connectSql.RunScalar(trans, Sql)
                    If strToFilledAcc Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
                    Else
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                        Dim strTOFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                        Dim obj7 As Accountsegment = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                        If clsCommon.myCdbl(totAmtWithBrkgLkg) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", totAmtWithBrkgLkg * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj7.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj7.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj7.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj7.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj7.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj7.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj7.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj7.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj7.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj7.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj7.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj7.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj7.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj7.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj7.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj7.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj7.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj7.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj7.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj7.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj7.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                End If

            End If
            '*************** End : Manoj

            '************************************* Add Empty Entry
            If obj.Item_Type <> "Empty" Then

                If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                    Dim objSeg As Accountsegment

                    Dim Loc2 As String = "select From_Location  from TSPL_TRANSFER_HEAD where Transfer_No ='" + obj.Transfer_No + "'"
                    Loc2 = connectSql.RunScalar(trans, Loc2)
                    Dim strFrmPurchaseAcc As String = Nothing
                    AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + obj.Arr.Item(0).Item_Code.ToString() + "'"))
                    Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                    Dim strFrmPurchaseAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmPurchaseAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
                    Else
                        'If strFrmPurchaseAccFirst.Length > 4 Then
                        '    strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                        'End If
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                        Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, obj.From_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If



                    Dim strFrmEmptyAcc As String = Nothing
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc2 + "'"
                    Dim strFrmEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmEmptyAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
                    Else
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                        Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, obj.From_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If


                    strFrmEmptyAcc = Nothing
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc2 + "'"
                    Dim strToEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmEmptyAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
                    Else
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strToEmptyAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                        Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, obj.To_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If

                    strFrmPurchaseAcc = Nothing
                    AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + obj.Arr.Item(0).Item_Code.ToString() + "'"))
                    Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                    strFrmPurchaseAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmPurchaseAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
                    Else
                        ''If strFrmPurchaseAccFirst.Length > 4 Then
                        ''    strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                        ''End If
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                        Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, obj.To_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                        If clsCommon.myCdbl(ActualEmptyAmt) > 0 Then
                            ActualEmptyAmt = ActualEmptyAmt '+ IIf(isApplyShell, EntrySheLLDiff, 0)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", ActualEmptyAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                End If
            End If
            '************* End
            Dim strBalanceChk As Decimal = clsDBFuncationality.getSingleValue("select isnull(SUM(Amount),0)  from TSPL_JOURNAL_DETAILS where Voucher_No='" + StrVoucher + "'", trans)
            If strBalanceChk = 0 Then
                Dim dt12345 As DataTable = clsDBFuncationality.GetDataTable("select * from  TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + StrVoucher + "'", trans)
                Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
                connectSql.RunSqlTransaction(trans, Sql)
            Else
                Throw New Exception(transportSql.GetJounalEntryException("TSPL_JOURNAL_DETAILS", StrVoucher, trans))
            End If
            ''Throw New Exception(transportSql.GetJounalEntryException(StrVoucher, trans))
            Return True

        Else

            If Not isForJournalEntryOnly Then
                If Not FunItemLocationUpdate(obj, trans) Then
                    Return False
                End If
            End If

            Dim strFromInvAcc As String = ""
            Dim strFromInvAccDesc As String = ""
            Dim strToInvAcc As String = ""
            Dim strToInvAccDesc As String = ""
            Dim strShpClrAcc As String = ""
            Dim strShpClrAccDesc As String = ""
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
            Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.To_Location) + "'"
            Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)

            If IsCreateJEForTransfer AndAlso strExcisable = "F" AndAlso strToLType = "Logical" Then
                Sql = "select TSPL_SALES_ACCOUNTS.Suspence_Account from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code  where TSPL_ITEM_MASTER.Item_Code='" + obj.Arr.Item(0).Item_Code + "'"
            Else
                Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
           " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"

            End If
            strFromInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)

            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
            strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
            If strFromInvAccDesc Is Nothing Then
                Throw New Exception("Inventory Control Account not found.")

            End If
            If strExcisable = "F" AndAlso strToLType = "Logical" Then
                Sql = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                 " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
                ''strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)''Changet to locaseg code to from locaction seg code as ask by amit sir---Balwinder
                strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            Else
                Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
               " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
                strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
            End If
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
            strToInvAccDesc = connectSql.RunScalar(trans, Sql)
            If strToInvAccDesc Is Nothing Then
                Throw New Exception("Reserve Stock Account not found.")

            End If
            Dim TotalItemCost As Decimal = 0
            Dim TotalItemCostShell As Decimal = 0
            Dim EmptyAmt As Decimal = 0

            For Each objTr As clsTransferDetails In obj.Arr
                Dim tCost As Double = 0
                If clsCommon.CompairString(obj.Item_Type, "Empty") = CompairStringResult.Equal Then
                    tCost = objTr.BasicPrice_WithTax
                ElseIf Not clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal Then
                    ' tCost = objTr.Basic_Price ''Change on 24/July/2013 with Rakesh Sir
                    tCost = objTr.BasicPrice_WithTax
                End If
                '' TotalItemCost = TotalItemCost + (objTr.Item_Qty * IIf((clsCommon.CompairString(obj.Item_Type, "Empty") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal), objTr.BasicPrice_WithTax, objTr.Item_Price))
                TotalItemCost = TotalItemCost + (objTr.Item_Qty * tCost)


                If clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal Then
                    TotalItemCostShell = TotalItemCostShell + (objTr.Item_Qty * objTr.BasicPrice_WithTax)
                End If

                If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                    EmptyAmt = EmptyAmt + Math.Round(CDec((objTr.Item_Qty) * CDec(objTr.Empty_Value)), 2)
                End If
            Next

            EmptyAmt = EmptyAmt + TotalItemCostShell


            ''If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
            ''    For Each objtr As clsTransferDetails In obj.Arr
            ''        EmptyAmt = EmptyAmt + Math.Round(CDec((objtr.Item_Qty) * CDec(objtr.Empty_Value)), 2)
            ''    Next
            ''End If


            strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, obj.From_Location, trans)
            Dim obj5 As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
            If clsCommon.myCdbl(TotalItemCost) > 0 Then
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", CDec(TotalItemCost) * (-1)), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj5.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj5.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj5.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj5.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj5.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj5.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj5.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj5.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj5.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj5.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj5.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj5.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj5.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj5.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj5.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj5.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj5.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj5.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj5.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj5.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj5.Account_Seg_Desc10))
                lineNo = lineNo + 1
            End If


            If strExcisable = "F" AndAlso strToLType = "Logical" Then
                strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.From_Location, trans)
            Else
                strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.To_Location, trans)
            End If
            Dim obj6 As Accountsegment = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
            If clsCommon.myCdbl(TotalItemCost) > 0 Then
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", CDec(TotalItemCost)), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj6.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj6.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj6.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj6.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj6.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj6.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj6.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj6.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj6.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj6.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj6.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj6.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj6.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj6.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj6.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj6.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj6.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj6.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj6.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj6.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj6.Account_Seg_Desc10))
                lineNo = lineNo + 1
            End If



            '***************By Manoj: To add GL-Entry Transfer Filled and Empty Account
            If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                If obj.Item_Type = "Full" Then
                    Dim strFrmFilledAcc As String = Nothing
                    Dim Loc1 As String = "select From_Location  from TSPL_TRANSFER_HEAD where Transfer_No ='" + obj.Transfer_No + "'"
                    Loc1 = connectSql.RunScalar(trans, Loc1)
                    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc1 + "'"
                    Dim strFrmFilledAccFirst As String = connectSql.RunScalar(trans, Sql)
                    If strFrmFilledAccFirst Is Nothing Or strFrmFilledAccFirst = "" Then
                        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
                    Else
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                        Dim strFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                        Dim obj8 As Accountsegment = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                        If clsCommon.myCdbl(TotalItemCost) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", TotalItemCost), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj8.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj8.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj8.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj8.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj8.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj8.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj8.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj8.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj8.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj8.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj8.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj8.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj8.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj8.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj8.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj8.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj8.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj8.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj8.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj8.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj8.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                    Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
                    Dim strToFilledAcc As String = connectSql.RunScalar(trans, Sql)
                    If strToFilledAcc Is Nothing Then
                        Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
                    Else
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                        Dim strTOFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                        Dim obj7 As Accountsegment = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                        If clsCommon.myCdbl(TotalItemCost) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", TotalItemCost * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj7.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj7.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj7.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj7.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj7.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj7.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj7.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj7.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj7.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj7.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj7.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj7.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj7.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj7.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj7.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj7.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj7.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj7.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj7.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj7.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj7.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                ElseIf obj.Item_Type = "Empty" Then
                    Dim strFrmFilledAcc As String = Nothing
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
                    Dim strFrmFilledAccFirst As String = connectSql.RunScalar(trans, Sql)
                    If strFrmFilledAccFirst Is Nothing Or strFrmFilledAccFirst = "" Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)

                    Else
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                        Dim strFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                        Dim obj8 As Accountsegment = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                        If clsCommon.myCdbl(TotalItemCost) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", TotalItemCost), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj8.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj8.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj8.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj8.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj8.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj8.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj8.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj8.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj8.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj8.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj8.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj8.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj8.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj8.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj8.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj8.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj8.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj8.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj8.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj8.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj8.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
                    Dim strToFilledAcc As String = connectSql.RunScalar(trans, Sql)
                    If strToFilledAcc Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
                    Else
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                        Dim strTOFilledAccDesc As String = connectSql.RunScalar(trans, Sql)
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                        Dim obj7 As Accountsegment = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                        If clsCommon.myCdbl(TotalItemCost) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", TotalItemCost * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj7.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj7.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj7.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj7.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj7.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj7.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj7.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj7.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj7.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj7.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj7.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj7.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj7.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj7.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj7.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj7.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj7.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj7.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj7.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj7.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj7.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                End If

            End If
            '*************** End : Manoj

            ''''''Added by balwinder for add empty account for depot transaction filled on 03/Nov/2012 

            '************************************* Add Empty Entry
            If obj.Item_Type <> "Empty" Then

                If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                    Dim objSeg As Accountsegment

                    Dim Loc2 As String = "select From_Location  from TSPL_TRANSFER_HEAD where Transfer_No ='" + obj.Transfer_No + "'"
                    Loc2 = connectSql.RunScalar(trans, Loc2)
                    Dim strFrmPurchaseAcc As String = Nothing
                    AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + obj.Arr.Item(0).Item_Code.ToString() + "'"))
                    Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                    Dim strFrmPurchaseAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmPurchaseAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
                    Else
                        'If strFrmPurchaseAccFirst.Length > 4 Then
                        '    strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                        'End If
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                        Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, obj.From_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If



                    Dim strFrmEmptyAcc As String = Nothing
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc2 + "'"
                    Dim strFrmEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmEmptyAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
                    Else
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, obj.From_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                        Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, obj.From_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If


                    strFrmEmptyAcc = Nothing
                    Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc2 + "'"
                    Dim strToEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmEmptyAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
                    Else
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strToEmptyAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                        Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, obj.To_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If

                    strFrmPurchaseAcc = Nothing
                    AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + obj.Arr.Item(0).Item_Code.ToString() + "'"))
                    Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                    strFrmPurchaseAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                    If strFrmPurchaseAccFirst Is Nothing Then
                        Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
                    Else
                        ''If strFrmPurchaseAccFirst.Length > 4 Then
                        ''    strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                        ''End If
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAccFirst, obj.To_Location, False, trans)
                        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                        Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                        Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                        strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, obj.To_Location, trans)
                        objSeg = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                        If clsCommon.myCdbl(EmptyAmt) > 0 Then ''ActualEmptyAmt
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                End If
            End If
            '************* End


            Dim strBalanceChk As Decimal = clsDBFuncationality.getSingleValue("select isnull(SUM(Amount),0)  from TSPL_JOURNAL_DETAILS where Voucher_No='" + StrVoucher + "'", trans)
            If strBalanceChk = 0 Then
                Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
                connectSql.RunSqlTransaction(trans, Sql)
            Else
                Throw New Exception(transportSql.GetJounalEntryException("TSPL_JOURNAL_DETAILS", StrVoucher, trans))
            End If
            'Throw New Exception(transportSql.GetJounalEntryException(StrVoucher, trans))
            Return True

        End If
    End Function

    Private Shared Function FunItemLocationUpdateLoadIn(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster) As Boolean
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0
        Dim toShipmentCogs As Decimal = 0
        Dim Count As Decimal = 0
        Dim ItemType As String
        Dim Sql As String = String.Empty
        Dim ItemDs As New DataSet()
        Dim ChkItemVerifyDs As New DataSet()
        Dim itmcost As Decimal
        Dim LoadOutItmCost As Decimal
        Dim mrpprice1, pndqty, totalloadin1, LoadOutAmtPerItem, LoadOutEmptyValPerItem, LoadInEmptyValPerItem As Decimal
        Try
            If (strToLType = "Logical" OrElse strFromLType = "Logical") = True Then
                connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set  LoadIn_EmptyValue=0, Shortage=0, Amount=0, LoadInQty = 0, Breakage =0, Leak = 0 where Transfer_No = '" + obj.Load_Out_No + "' ")
            End If
            For Each objTr As clsTransferDetails In obj.Arr
                LoadInEmptyValPerItem = 0
                Dim Conversion As Decimal = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
                If CDec(objTr.LoadIn_Qty) > 0 Or CDec(objTr.Burst) > 0 Or CDec(objTr.Shortage) > 0 Or CDec(objTr.Leak) > 0 Then
                    Count = Count + 1
                    If obj.Item_Type = "Full" Then
                        mrpprice1 = CDec(objTr.MRP * Conversion)
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
                        " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                        LoadOutItmCost = clsCommon.myCdbl(connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Load_Out_No + "' and Item_Code ='" + objTr.Item_Code.ToString() + "'" & _
                             " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "))
                        pndqty = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'"))
                    Else
                        If CDec(Conversion) <> 1 Then
                            mrpprice1 = CDec(CDec(objTr.MRP) * Conversion) + 100
                            pndqty = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'"))
                        Else
                            mrpprice1 = CDec(objTr.MRP * Conversion)
                            pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'")
                        End If
                        LoadOutItmCost = clsCommon.myCdbl(connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "' and Item_Code ='" + objTr.Item_Code.ToString() + "'" & _
                              " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "))
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                         " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "
                    End If
                    ItemDs.Clear()
                    ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                    totalloadin1 = Math.Round(objTr.LoadIn_Qty / Conversion, 2)
                    Dim loadintem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select loadinqty  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim breakagetem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select  breakage  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim leaktem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select leak from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim Amount As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select isnull(Amount,0) from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim Shortage As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Shortage from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    LoadInEmptyValPerItem = clsCommon.myCdbl(connectSql.RunScalar(trans, "select LoadIn_EmptyValue from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    loadintem = Math.Round(loadintem + objTr.LoadIn_Qty / Conversion, 2)
                    breakagetem = Math.Round(breakagetem + objTr.Burst / Conversion, 2)
                    leaktem = Math.Round(leaktem + objTr.Leak / Conversion, 2)
                    Shortage = Math.Round(Shortage + objTr.Shortage / Conversion, 2)
                    Amount = Math.Round(Amount + (CDec(objTr.LoadIn_Qty) + CDec(objTr.Burst) + CDec(objTr.Leak) + CDec(objTr.Shortage)) * (CDec(objTr.BasicPrice_WithTax) + CDec(objTr.TPT_Value) + CDec(objTr.Empty_Value)), 2)
                    Dim FlvrCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                    Dim PackCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                    Dim ItmChkQry As String = "select * from TEMP_PROVISIONAL_SALES where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' "
                    LoadInEmptyValPerItem = LoadInEmptyValPerItem + (objTr.LoadIn_Qty + objTr.Leak + objTr.Burst + objTr.Shortage) * objTr.Empty_Value
                    LoadOutAmtPerItem = objTr.Item_Qty * (objTr.BasicPrice_WithTax + objTr.TPT_Value + objTr.Empty_Value)
                    LoadOutEmptyValPerItem = objTr.Item_Qty * objTr.Empty_Value
                    ChkItemVerifyDs.Clear()
                    ChkItemVerifyDs = connectSql.RunSQLReturnDS(trans, ItmChkQry)


                    ''Out From from Location
                    If clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal Then
                        totalloadin1 = objTr.Item_Qty
                    End If

                    If totalloadin1 <> 0 Then
                        If (strToLType = "Logical" OrElse strFromLType = "Logical") = True Then
                            If ChkItemVerifyDs.Tables(0).Rows.Count > 0 Then
                                connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set  LoadIn_EmptyValue=" + LoadInEmptyValPerItem.ToString() + ", Shortage=" + Shortage.ToString() + ", Amount=" + Amount.ToString() + ", LoadInQty = '" + Convert.ToString(loadintem) + "', Breakage ='" + Convert.ToString(breakagetem) + "', Leak = '" + Convert.ToString(leaktem) + "',Post='Y' where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
                            Else
                                Dim q As String = "insert TEMP_PROVISIONAL_SALES values ('" + obj.Load_Out_No + "','" + Format(obj.Transfer_Date, "yyyy-MM-dd") + "','" + obj.Vehicle_Code + "','" + obj.To_Location + "','null','" + obj.Salesmancode + "','" + obj.FromLoc_Desc + "','" + objTr.Item_Code + "','" + objTr.Item_Desc + "',0,0,0,'',0,0," + objTr.MRP.ToString() + ",'" + objTr.Uom + "','" + obj.Vehicle_Code + "','" + PackCode + "','" + FlvrCode + "'," + Amount.ToString() + ",0,'" + obj.Route_No + "'," + LoadOutAmtPerItem.ToString() + "," + LoadOutEmptyValPerItem.ToString() + "," + LoadInEmptyValPerItem.ToString() + ",'" + obj.HOS + "','" + obj.TDM + "','" + obj.ADC + "','" + obj.CE + "','" + obj.Comp_Code + "','Y','')"
                                connectSql.RunSqlTransaction(trans, q)
                            End If
                        End If
                        Dim absvalue As Decimal = 0
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            Dim amt As Decimal = CDec(ItemDs.Tables(0).Rows(0)("Amount"))
                            Dim qty As Decimal = totalloadin1
                            itmcost = LoadOutItmCost
                            If objTr.Uom = "SH" Then
                                itmcost = CDec(objTr.Total_Item_Cost)
                            End If
                            Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                                  " Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * objTr.Total_Item_Cost)).ToString() + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                  " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.From_Location) + "'," & _
                           " '" + obj.FromLoc_Desc + "','" + totalloadin1.ToString() + "','" + (totalloadin1 * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.MRP * Conversion) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "', 'FM')"
                            connectSql.RunSqlTransaction(trans, Sql)
                        End If
                    End If



                    ''In into To Location
                    totalloadin1 = Math.Round(objTr.LoadIn_Qty / Conversion, 2)
                    If obj.Item_Type = "Full" Then
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                        " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                    Else
                        If CDec(Conversion) <> 1 Then
                            mrpprice1 = CDec(CDec(objTr.MRP) * CDec(Conversion)) + 100
                        Else
                            mrpprice1 = CDec(objTr.MRP * CDec(Conversion))
                        End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                        " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "
                    End If
                    ItemDs.Clear()
                    ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                    Dim applyqty As Decimal = objTr.LoadIn_Qty / Conversion
                    If ItemDs.Tables(0).Rows.Count > 0 Then
                        applyqty = applyqty * itmcost
                        Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                              "Amount='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Amount")) * applyqty) + "' where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                              " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                        connectSql.RunSqlTransaction(trans, Sql)
                    Else
                        ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                        Dim itmc As Decimal = objTr.Total_Item_Cost
                        ' Dim newc As Decimal
                        Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                       " '','" + totalloadin1.ToString() + "','" + (totalloadin1 * Math.Round(objTr.Total_Item_Cost, 2)).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.MRP * Conversion) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "', '" + ItemType + "')"
                        connectSql.RunSqlTransaction(trans, Sql)
                    End If

                    ''Handliing Leak Qty
                    Dim S As String = CDec(objTr.Leak)
                    If objTr.Leak > 0 Then
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                              " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        totalloadin1 = Math.Round(objTr.Leak / Conversion, 2)
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                            "Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * objTr.Total_Item_Cost).ToString()).ToString() + "' where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                            " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(objTr.Item_Code) + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.From_Location) + "'," & _
                            " '" + obj.FromLoc_Desc + "','" + objTr.LoadIn_Qty.ToString() + "','" + (objTr.Item_Qty * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + objTr.MRP.ToString() + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "', '" + ItemType + "')"
                            connectSql.RunSqlTransaction(trans, Sql)
                        End If



                        Dim fathercode As String = connectSql.RunScalar(trans, "SELECT Father_Code  FROM TSPL_ITEM_MASTER WHERE Item_Code = '" + Convert.ToString(objTr.Item_Code) + "'")
                        Dim fathedesc As String = connectSql.RunScalar(trans, "select Item_Desc from tspl_item_master where item_code= '" + fathercode + "'")
                        If clsCommon.CompairString(fathercode, "NIL") = CompairStringResult.Equal OrElse clsCommon.myLen(fathercode) <= 0 Then
                            Dim strmessage As String = "Father Code not Defined" + Environment.NewLine
                            strmessage += "for the Item " + Convert.ToString(objTr.Item_Code)
                            Throw New Exception(strmessage)
                        End If


                        Sql = "select top 1 Item_Basic_Net from TSPL_ITEM_PRICE_MASTER where Item_Code='" + fathercode + "' and UOM='EC' "
                        Dim FMRP As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sql, trans))
                        If FMRP <= 0 Then
                            Throw New Exception("Item Rate not found for item " + fathercode + "")
                        End If

                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                        " AND location_code='" + obj.To_Location + "' AND MRP='" + clsCommon.myCstr(FMRP) + "' and batch_no = '" + Convert.ToString(objTr.Batch_No) + "'"
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                                  "Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) + CDec(totalloadin1 * objTr.Total_Item_Cost)).ToString() + "' where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                                  " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + clsCommon.myCstr(FMRP) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(fathercode) + "','" + fathedesc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                                    " '" + obj.ToLoc_Desc + "','" + totalloadin1.ToString() + "','" + (totalloadin1 * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.myCstr(FMRP) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "', 'E')"
                            connectSql.RunSqlTransaction(trans, Sql)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function FunInsertTempProvLoadIn(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster) As Boolean
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0
        Dim toShipmentCogs As Decimal = 0
        Dim Count As Decimal = 0
        Dim ItemType As String
        Dim Sql As String = String.Empty
        Dim ItemDs As New DataSet()
        Dim ChkItemVerifyDs As New DataSet()
        Dim itmcost As Decimal
        Dim LoadOutItmCost As Decimal
        Dim mrpprice1, pndqty, totalloadin1, LoadOutAmtPerItem, LoadOutEmptyValPerItem, LoadInEmptyValPerItem As Decimal
        Try
            connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set  LoadIn_EmptyValue=0, Shortage=0, Amount=0, LoadInQty = 0, Breakage =0, Leak = 0 where Transfer_No = '" + obj.Load_Out_No + "' ")


            For Each objTr As clsTransferDetails In obj.Arr
                LoadInEmptyValPerItem = 0
                Dim Conversion As Decimal = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
                ' If objTr.Uom <> "SH" Then
                If CDec(objTr.LoadIn_Qty) > 0 Or CDec(objTr.Burst) > 0 Or CDec(objTr.Shortage) > 0 Or CDec(objTr.Leak) > 0 Then
                    Count = Count + 1
                    If obj.Item_Type = "Full" Then
                        mrpprice1 = CDec(objTr.MRP * Conversion)
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
    " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                        LoadOutItmCost = clsCommon.myCdbl(connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Load_Out_No + "' and Item_Code ='" + objTr.Item_Code.ToString() + "'" & _
                             " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "))
                        pndqty = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'"))
                    Else
                        If CDec(Conversion) <> 1 Then
                            mrpprice1 = CDec(CDec(objTr.MRP) * Conversion) + 100
                            pndqty = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'"))
                        Else
                            mrpprice1 = CDec(objTr.MRP * Conversion)
                            pndqty = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'"))

                        End If
                        LoadOutItmCost = clsCommon.myCdbl(connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "' and Item_Code ='" + objTr.Item_Code.ToString() + "'" & _
                              " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "))
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                         " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "

                    End If
                    ItemDs.Clear()
                    ItemDs = connectSql.RunSQLReturnDS(trans, Sql)

                    ' totalloadin1 = Math.Round(grow.Cells("dgvloadinqty").Value / Conversion + grow.Cells("dgvbreakage").Value / Conversion + grow.Cells("shortage").Value / Conversion, 2)
                    totalloadin1 = Math.Round(objTr.LoadIn_Qty / Conversion, 2)
                    'Dim BalanceQty As Decimal = pndqty - totalloadin1 - Math.Round(grow.Cells("leak").Value / Conversion, 2)
                    'connectSql.RunSqlTransaction(trans, "update TSPL_TRANSFER_DETAIL set pending_qty='" + Convert.ToString(BalanceQty) + "' where Transfer_No='" + obj.Transfer_No + "' and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and price_date = '" + CDate(grow.Cells("dgvcombopricedate").Value).ToString("yyyy-MM-dd") + "' and MRP='" + Convert.ToString(mrpprice1) + "'")


                    Dim loadintem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select loadinqty  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim breakagetem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select  breakage  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim leaktem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select leak from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim Amount As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select isnull(Amount,0) from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim Shortage As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Shortage from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    LoadInEmptyValPerItem = clsCommon.myCdbl(connectSql.RunScalar(trans, "select LoadIn_EmptyValue from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    loadintem = Math.Round(loadintem + objTr.LoadIn_Qty / Conversion, 2)
                    breakagetem = Math.Round(breakagetem + objTr.Burst / Conversion, 2)
                    leaktem = Math.Round(leaktem + objTr.Leak / Conversion, 2)
                    Shortage = Math.Round(Shortage + objTr.Shortage / Conversion, 2)
                    Amount = Math.Round(Amount + (CDec(objTr.LoadIn_Qty) + CDec(objTr.Burst) + CDec(objTr.Leak) + CDec(objTr.Shortage)) * (CDec(objTr.BasicPrice_WithTax) + CDec(objTr.TPT_Value) + CDec(objTr.Empty_Value)), 2)
                    Dim FlvrCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                    Dim PackCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                    Dim ItmChkQry As String = "select * from TEMP_PROVISIONAL_SALES where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' "
                    LoadInEmptyValPerItem = LoadInEmptyValPerItem + (objTr.LoadIn_Qty + objTr.Leak + objTr.Burst + objTr.Shortage) * objTr.Empty_Value
                    LoadOutAmtPerItem = objTr.Item_Qty * (objTr.BasicPrice_WithTax + objTr.TPT_Value + objTr.Empty_Value)
                    LoadOutEmptyValPerItem = objTr.Item_Qty * objTr.Empty_Value
                    ChkItemVerifyDs.Clear()
                    ChkItemVerifyDs = connectSql.RunSQLReturnDS(trans, ItmChkQry)
                    If (strToLType = "Logical" OrElse strFromLType = "Logical") = True Then
                        If ChkItemVerifyDs.Tables(0).Rows.Count > 0 Then
                            connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set  LoadIn_EmptyValue=" + LoadInEmptyValPerItem.ToString() + ", Shortage=" + Shortage.ToString() + ", Amount=" + Amount.ToString() + ", LoadInQty = '" + Convert.ToString(loadintem) + "', Breakage ='" + Convert.ToString(breakagetem) + "', Leak = '" + Convert.ToString(leaktem) + "' where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
                        Else
                            Dim q As String = "insert TEMP_PROVISIONAL_SALES values ('" + obj.Load_Out_No + "','" + Format(obj.Transfer_Date, "yyyy-MM-dd") + "','" + obj.Vehicle_Code + "','" + obj.To_Location + "','null','" + obj.Salesmancode + "','" + obj.FromLoc_Desc + "','" + objTr.Item_Code + "','" + objTr.Item_Desc + "',0,0,0,'',0,0," + objTr.MRP.ToString() + ",'" + objTr.Uom + "','" + obj.Vehicle_Code + "','" + PackCode + "','" + FlvrCode + "'," + Amount.ToString() + ",0,'" + obj.Route_No + "'," + LoadOutAmtPerItem.ToString() + "," + LoadOutEmptyValPerItem.ToString() + "," + LoadInEmptyValPerItem.ToString() + ",'" + obj.HOS + "','" + obj.TDM + "','" + obj.ADC + "','" + obj.CE + "','" + obj.Comp_Code + "','N','')"
                            connectSql.RunSqlTransaction(trans, q)
                        End If
                    End If
                    Dim absvalue As Decimal = 0
                    If ItemDs.Tables(0).Rows.Count > 0 Then
                        '************Edited By: Manoj for Update Item Cost On Location Detail
                        Dim amt As Decimal = CDec(ItemDs.Tables(0).Rows(0)("Amount"))
                        Dim qty As Decimal = totalloadin1
                        'itmcost = CDec(ItemDs.Tables(0).Rows(0)("Amount")) / CDec(ItemDs.Tables(0).Rows(0)("Item_Qty"))
                        itmcost = LoadOutItmCost
                        'Dim newAmt As Decimal = amt - (qty * itmcost)
                        '     Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                        '"Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * itmcost)).ToString() + "' where Item_Code='" + objTr.Item_Code + "' " & _
                        '    " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "

                        '*******************************

                        '  before to manoj    Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                        '"Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * grow.Cells("dgvitemcost").Value).ToString()).ToString() + "' where Item_Code='" + objTr.Item_Code + "' " & _
                        '    " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.Item_Price * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                        'connectSql.RunSqlTransaction(trans, Sql)
                    Else
                        ' ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                        ' Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.From_Location) + "'," & _
                        '" '" + obj.FromLoc_Desc + "','" + totalloadin1.ToString() + "','" + (totalloadin1 * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.MRP * Conversion) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', 'FM')"
                        ' connectSql.RunSqlTransaction(trans, Sql)
                    End If
                    If obj.Item_Type = "Full" Then
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
    " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                    Else
                        If CDec(Conversion) <> 1 Then
                            mrpprice1 = CDec(CDec(objTr.MRP) * CDec(Conversion)) + 100
                        Else
                            mrpprice1 = CDec(objTr.MRP * CDec(Conversion))
                        End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
    " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "
                    End If
                    ItemDs.Clear()
                    ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                    'Dim applyqty As Decimal = grow.Cells("dgvloadinqty").Value / Conversion + grow.Cells("dgvbreakage").Value / Conversion + grow.Cells("shortage").Value / Conversion
                    Dim applyqty As Decimal = objTr.LoadIn_Qty / Conversion
                    ' applyqty = applyqty * grow.Cells("dgvitemcost").Value
                    If ItemDs.Tables(0).Rows.Count > 0 Then
                        '********** Edited By Manoj For Updation Into Item Location Deatil
                        applyqty = applyqty * itmcost
                        'Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                        '        "Amount='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Amount")) + applyqty) + "' where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                        '            " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                        'connectSql.RunSqlTransaction(trans, Sql)

                        '************* End Manoj

                        'Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                        '         "Amount='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Amount")) + applyqty) + "' where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                        '             " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.Item_Price * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "

                    Else
                        ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")

                        '****** Edited By Manoj For Updation Into Item Location Deatil
                        Dim itmc As Decimal = objTr.Total_Item_Cost
                        ' Dim newc As Decimal
                        ' Dim itmcost1 As Decimal = CDec(ItemDs.Tables(0).Rows(0)("Amount")) / CDec(ItemDs.Tables(0).Rows(0)("Item_Qty"))
                        ' Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                        '" '','" + totalloadin1.ToString() + "','" + (totalloadin1 * Math.Round(itmcost, 2)).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.MRP * Conversion) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"

                        '************************************************
                        'Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + grow.Cells("dgvItemname").Value + "','" + Convert.ToString(obj.To_Location) + "'," & _
                        '" '','" + totalloadin1.ToString() + "','" + (totalloadin1 * grow.Cells("dgvitemcost").Value).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.Item_Price * Conversion) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"
                        connectSql.RunSqlTransaction(trans, Sql)

                    End If
                    Dim S As String = CDec(objTr.Leak)
                    If CDec(objTr.Leak) > 0 Then
                        Dim fathercode As String = connectSql.RunScalar(trans, "SELECT Father_Code  FROM TSPL_ITEM_MASTER WHERE Item_Code = '" + Convert.ToString(objTr.Item_Code) + "'")
                        Dim fathedesc As String = connectSql.RunScalar(trans, "select Item_Desc from tspl_item_master where item_code= '" + fathercode + "'")
                        If fathercode = "NIL" Then
                            Dim strmessage As String = "Father Code not defined " + Environment.NewLine
                            strmessage += "for this Item " + Convert.ToString(objTr.Item_Code)
                            'common.clsCommon.MyMessageBoxShow(strmessage)
                            Throw New Exception(strmessage)
                            trans.Rollback()
                            Return False
                        End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                      " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        totalloadin1 = Math.Round(objTr.Leak / Conversion, 2)
                        'If ItemDs.Tables(0).Rows.Count > 0 Then
                        '    Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                        '"Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * objTr.Total_Item_Cost).ToString()).ToString() + "' where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                        '    " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                        '    connectSql.RunSqlTransaction(trans, Sql)
                        'Else
                        '    ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                        '    Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(objTr.Item_Code) + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.From_Location) + "'," & _
                        '   " '" + obj.FromLoc_Desc + "','" + objTr.LoadIn_Qty.ToString() + "','" + (objTr.Item_Qty * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + objTr.MRP.ToString() + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"
                        '    connectSql.RunSqlTransaction(trans, Sql)
                        'End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                 " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + CDec(objTr.MRP * CDec(Conversion)).ToString() + "' and batch_no = '" + Convert.ToString(objTr.Batch_No) + "'"
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            '    Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                            '             "Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) + CDec(totalloadin1 * objTr.Total_Item_Cost)).ToString() + "' where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                            '                 " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                            '    connectSql.RunSqlTransaction(trans, Sql)
                            'Else
                            '    'Sql = "SELECT Item_Basic_Net ,UOM,Empty_Value_Bottle FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code ='" + Convert.ToString(fathercode) + "'"
                            '    'Dim DS As DataSet = connectSql.RunSQLReturnDS(trans, Sql)

                            '    Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(fathercode) + "','" + fathedesc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                            '    " '" + obj.ToLoc_Desc + "','" + totalloadin1.ToString() + "','" + (totalloadin1 * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + ((CDec(objTr.MRP)) * CDec(Conversion)).ToString() + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', 'E')"
                            '    connectSql.RunSqlTransaction(trans, Sql)
                        End If
                    End If
                End If
                ' Else

                ' End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function FunItemLocationUpdateLoadInRrvs(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster) As Boolean
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0
        Dim toShipmentCogs As Decimal = 0
        Dim Count As Decimal = 0
        Dim ItemType As String
        Dim Sql As String = String.Empty
        Dim ItemDs As New DataSet()
        Dim ChkItemVerifyDs As New DataSet()
        Dim itmcost As Decimal
        Dim LoadOutItmCost As Decimal
        Dim mrpprice1, pndqty, totalloadin1 As Decimal
        Try
            For Each objTr As clsTransferDetails In obj.Arr
                Dim Conversion As Decimal = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
                ' If objTr.Uom <> "SH" Then
                If CDec(objTr.LoadIn_Qty) > 0 Or CDec(objTr.Burst) > 0 Or CDec(objTr.Shortage) > 0 Or CDec(objTr.Leak) > 0 Then
                    Count = Count + 1
                    If obj.Item_Type = "Full" Then
                        mrpprice1 = CDec(objTr.MRP * Conversion)
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
    " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                        Dim t As String = " select  Total_Item_Cost from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Load_Out_No + "' and Item_Code ='" + objTr.Item_Code.ToString() + "'" & _
                             " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "
                        LoadOutItmCost = clsCommon.myCdbl(connectSql.RunScalar(trans, t))
                        pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'")
                    Else
                        If CDec(Conversion) <> 1 Then
                            mrpprice1 = CDec(CDec(objTr.MRP) * Conversion) + 100
                            pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'")
                        Else
                            mrpprice1 = CDec(objTr.MRP * Conversion)
                            pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "'and Item_Code ='" + objTr.Item_Code + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "'")

                        End If
                        LoadOutItmCost = connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_TRANSFER_DETAIL where Transfer_No ='" + obj.Transfer_No + "' and Item_Code ='" + objTr.Item_Code.ToString() + "'" & _
                              " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' ")
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                         " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "

                    End If
                    ItemDs.Clear()
                    ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                    ' totalloadin1 = Math.Round(grow.Cells("dgvloadinqty").Value / Conversion + grow.Cells("dgvbreakage").Value / Conversion + grow.Cells("shortage").Value / Conversion, 2)
                    totalloadin1 = Math.Round(objTr.LoadIn_Qty / Conversion, 2)
                    'Dim BalanceQty As Decimal = pndqty - totalloadin1 - Math.Round(grow.Cells("leak").Value / Conversion, 2)
                    'connectSql.RunSqlTransaction(trans, "update TSPL_TRANSFER_DETAIL set pending_qty='" + Convert.ToString(BalanceQty) + "' where Transfer_No='" + obj.Transfer_No + "' and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and price_date = '" + CDate(grow.Cells("dgvcombopricedate").Value).ToString("yyyy-MM-dd") + "' and MRP='" + Convert.ToString(mrpprice1) + "'")
                    Dim loadintem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select loadinqty  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim breakagetem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select  breakage  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim leaktem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select leak from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim Amount As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select isnull(Amount,0) from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    Dim Shortage As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Shortage from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'"))
                    loadintem = Math.Round(loadintem + objTr.LoadIn_Qty / Conversion, 2)
                    breakagetem = Math.Round(breakagetem + objTr.Burst / Conversion, 2)
                    leaktem = Math.Round(leaktem + objTr.Leak / Conversion, 2)
                    Shortage = Math.Round(Shortage + objTr.Shortage / Conversion, 2)
                    Amount = Math.Round(Amount + (CDec(objTr.LoadIn_Qty) + CDec(objTr.Burst) + CDec(objTr.Leak) + CDec(objTr.Shortage)) * (CDec(objTr.BasicPrice_WithTax) + CDec(objTr.TPT_Value) + CDec(objTr.Empty_Value)), 2)
                    Dim FlvrCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                    Dim PackCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                    Dim ItmChkQry As String = "select * from TEMP_PROVISIONAL_SALES where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' "
                    ChkItemVerifyDs.Clear()
                    ChkItemVerifyDs = connectSql.RunSQLReturnDS(trans, ItmChkQry)
                    'If (strToLType = "Logical" OrElse strFromLType = "Logical") = True Then
                    '    If ChkItemVerifyDs.Tables(0).Rows.Count > 0 Then
                    '        connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set Shortage=" + Shortage.ToString() + ", Amount=" + Amount.ToString() + ", LoadInQty = '" + Convert.ToString(loadintem) + "', Breakage ='" + Convert.ToString(breakagetem) + "', Leak = '" + Convert.ToString(leaktem) + "' where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
                    '    Else
                    '        connectSql.RunSqlTransaction(trans, "insert TEMP_PROVISIONAL_SALES values ('" + obj.Load_Out_No + "','" + Format(obj.Transfer_Date, "yyyy-MM-dd") + "','" + obj.Vehicle_Code + "','" + obj.To_Location + "','null','" + obj.Salesmancode + "','" + obj.FromLoc_Desc + "','" + objTr.Item_Code + "','" + objTr.Item_Desc + "',0,0,0,'',0,0," + objTr.MRP.ToString() + ",'" + objTr.Uom + "','" + obj.Vehicle_Code + "','" + PackCode + "','" + FlvrCode + "'," + Amount.ToString() + ",0)")
                    '    End If
                    'End If
                    Dim absvalue As Decimal = 0


                    If ItemDs.Tables(0).Rows.Count > 0 Then
                        '************Edited By: Manoj for Update Item Cost On Location Detail
                        Dim amt As Decimal = CDec(ItemDs.Tables(0).Rows(0)("Amount"))
                        Dim qty As Decimal = totalloadin1
                        'itmcost = CDec(ItemDs.Tables(0).Rows(0)("Amount")) / CDec(ItemDs.Tables(0).Rows(0)("Item_Qty"))
                        itmcost = LoadOutItmCost
                        'Dim newAmt As Decimal = amt - (qty * itmcost)
                        Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                   "Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) + (totalloadin1 * itmcost).ToString()).ToString() + "' where Item_Code='" + objTr.Item_Code + "' " & _
                       " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "

                        '*******************************

                        '  before to manoj    Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                        '"Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * grow.Cells("dgvitemcost").Value).ToString()).ToString() + "' where Item_Code='" + objTr.Item_Code + "' " & _
                        '    " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.Item_Price * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                        connectSql.RunSqlTransaction(trans, Sql)
                    Else
                        ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                        ' Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.From_Location) + "'," & _
                        '" '','" + totalloadin1.ToString() + "','" + (totalloadin1 * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.MRP * Conversion) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', 'FM')"

                        Sql = "delete from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
                       " AND location_code='" + Convert.ToString(obj.From_Location) + "' and itemtype='" + ItemType + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "'  "
                        If ItemDs.Tables(0) IsNot Nothing AndAlso ItemDs.Tables(0).Rows.Count > 0 Then
                            Sql += "  and batch_no = '" + clsCommon.myCstr(ItemDs.Tables(0).Rows(0)("batch_no")) + "'"
                        End If

                        connectSql.RunSqlTransaction(trans, Sql)
                    End If



                    If obj.Item_Type = "Full" Then
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
    " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                    Else
                        If CDec(Conversion) <> 1 Then
                            mrpprice1 = CDec(CDec(objTr.MRP) * CDec(Conversion)) + 100
                        Else
                            mrpprice1 = CDec(objTr.MRP * CDec(Conversion))
                        End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
    " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + objTr.Batch_No + "' "
                    End If
                    ItemDs.Clear()
                    ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                    'Dim applyqty As Decimal = grow.Cells("dgvloadinqty").Value / Conversion + grow.Cells("dgvbreakage").Value / Conversion + grow.Cells("shortage").Value / Conversion
                    Dim applyqty As Decimal = objTr.LoadIn_Qty / Conversion
                    ' applyqty = applyqty * grow.Cells("dgvitemcost").Value
                    If ItemDs.Tables(0).Rows.Count > 0 Then
                        '********** Edited By Manoj For Updation Into Item Location Deatil
                        applyqty = applyqty * itmcost
                        Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                                "Amount='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Amount")) - applyqty) + "' where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                                    " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                        connectSql.RunSqlTransaction(trans, Sql)

                        '************* End Manoj

                        'Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                        '         "Amount='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Amount")) + applyqty) + "' where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                        '             " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + Convert.ToString(objTr.Item_Price * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "

                    Else
                        ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")

                        '****** Edited By Manoj For Updation Into Item Location Deatil
                        Dim itmc As Decimal = objTr.Total_Item_Cost
                        ' Dim newc As Decimal
                        ' Dim itmcost1 As Decimal = CDec(ItemDs.Tables(0).Rows(0)("Amount")) / CDec(ItemDs.Tables(0).Rows(0)("Item_Qty"))
                        ' Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                        '" '','" + totalloadin1.ToString() + "','" + (totalloadin1 * Math.Round(itmcost, 2)).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.MRP * Conversion) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"


                        Sql = "delete from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code.ToString() + "' " & _
                                    " AND location_code='" + Convert.ToString(obj.To_Location) + "' and itemtype='" + ItemType + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' "


                        '************************************************
                        'Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + grow.Cells("dgvItemname").Value + "','" + Convert.ToString(obj.To_Location) + "'," & _
                        '" '','" + totalloadin1.ToString() + "','" + (totalloadin1 * grow.Cells("dgvitemcost").Value).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Convert.ToString(objTr.Item_Price * Conversion) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"
                        connectSql.RunSqlTransaction(trans, Sql)

                    End If
                    Dim S As String = CDec(objTr.Leak)
                    If CDec(objTr.Leak) > 0 Then
                        Dim fathercode As String = connectSql.RunScalar(trans, "SELECT Father_Code  FROM TSPL_ITEM_MASTER WHERE Item_Code = '" + Convert.ToString(objTr.Item_Code) + "'")
                        Dim fathedesc As String = connectSql.RunScalar(trans, "select Item_Desc from tspl_item_master where item_code= '" + fathercode + "'")
                        If fathercode = "NIL" Then
                            Dim strmessage As String = "Father Code not defined " + Environment.NewLine
                            strmessage += "for this Item " + Convert.ToString(objTr.Item_Code)
                            'common.clsCommon.MyMessageBoxShow(strmessage)
                            Throw New Exception(strmessage)
                            trans.Rollback()
                            Return False
                        End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                      " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + Convert.ToString(objTr.MRP * Conversion) + "' and batch_no = '" + objTr.Batch_No + "' "
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        totalloadin1 = Math.Round(objTr.Leak / Conversion, 2)
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                        "Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) + (totalloadin1 * objTr.Total_Item_Cost).ToString()).ToString() + "' where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                            " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND MRP='" + (objTr.MRP * Conversion).ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code.ToString() + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            ' Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(objTr.Item_Code) + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.From_Location) + "'," & _
                            '" '','" + objTr.LoadIn_Qty.ToString() + "','" + (objTr.Item_Qty * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + objTr.MRP.ToString() + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"

                            Sql = "delete from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(objTr.Item_Code) + "' " & _
                            " AND location_code='" + Convert.ToString(obj.From_Location) + "' AND  itemtype='" + ItemType + "' and MRP='" + objTr.MRP.ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "


                            connectSql.RunSqlTransaction(trans, Sql)
                        End If
                        Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                 " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + CDec(objTr.MRP * CDec(Conversion)).ToString() + "' and batch_no = '" + Convert.ToString(objTr.Batch_No) + "'"
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            If (CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) = 0 Then
                                Sql = "delete from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                                 " AND location_code='" + Convert.ToString(obj.To_Location) + "' and itemtype='E' AND MRP='" + (objTr.MRP * Conversion).ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' "
                            Else
                                Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(CDec(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                                                                    "Amount='" + (CDec(ItemDs.Tables(0).Rows(0)("Amount")) - CDec(totalloadin1 * objTr.Total_Item_Cost)).ToString() + "' where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                                                                        " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + (objTr.MRP * Conversion).ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                            End If
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            'Sql = "SELECT Item_Basic_Net ,UOM,Empty_Value_Bottle FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code ='" + Convert.ToString(fathercode) + "'"
                            'Dim DS As DataSet = connectSql.RunSQLReturnDS(trans, Sql)

                            'Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(fathercode) + "','" + fathedesc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                            '" '','" + objTr.Leak.ToString() + "','" + (objTr.Leak * objTr.Total_Item_Cost).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + ((CDec(objTr.MRP)) * CDec(Conversion)).ToString() + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(objTr.Batch_No) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', 'E')"
                            Sql = "delete from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                                  " AND location_code='" + Convert.ToString(obj.To_Location) + "' and itemtype='E' AND MRP='" + (objTr.MRP * Conversion).ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' "
                            connectSql.RunSqlTransaction(trans, Sql)
                        End If
                    End If
                End If
                ' Else

                ' End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function funitem_inventoryinsert_loadIN(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster, ByVal strTranferDate As String) As Boolean
        Dim ArrInventoryMovementOut As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim addcost As Decimal = 0.0
        ' Dim strQuery As String
        Dim trnasfer As Decimal = 0
        Dim PunchingTime As DateTime = New DateTime(obj.Transfer_Date.Year, obj.Transfer_Date.Month, obj.Transfer_Date.Day, obj.Date_Time_Removal.Hour, obj.Date_Time_Removal.Minute, obj.Date_Time_Removal.Second)
        For Each objTr As clsTransferDetails In obj.Arr
            If obj.Transfer_Type = "LO" Then
                trnasfer = objTr.Item_Qty
            ElseIf obj.Transfer_Type = "LI" Then
                trnasfer = objTr.LoadIn_Qty
            End If
            Dim basicost As Decimal = objTr.Basic_Price
            Dim Qry As String = "select DISTINCT total_item_cost from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No" & _
                         " where H.Transfer_No  ='" + obj.Load_Out_No + "'  and L.Item_Code ='" + objTr.Item_Code + "' "
            Dim itemcost As String = clsCommon.myCstr(connectSql.RunScalar(trans, Qry))
            If itemcost = "" Or itemcost = Nothing Then
                itemcost = CDec(objTr.Total_Item_Cost)
            End If
            Dim reccost As Decimal = 0.0
            Dim netcost As Decimal = trnasfer * CDec(itemcost)

            If strFromLType <> "Logical" Then
                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = obj.From_Location
                objInventoryMovemnt.Other_Location_Code = obj.To_Location
                objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.To_Location, trans)
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = trnasfer
                objInventoryMovemnt.UOM = objTr.Uom
                objInventoryMovemnt.MRP = objTr.MRP
                objInventoryMovemnt.Add_Cost = addcost
                objInventoryMovemnt.Net_Cost = netcost
                objInventoryMovemnt.ItemType = "FM"
                objInventoryMovemnt.Basic_Cost = netcost
                objInventoryMovemnt.Rec_Cost = reccost
                objInventoryMovemnt.Punching_Date = PunchingTime
                ArrInventoryMovementOut.Add(objInventoryMovemnt)
            End If
            If strToLType <> "Logical" Then
                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = obj.To_Location
                objInventoryMovemnt.Other_Location_Code = obj.From_Location
                objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.From_Location, trans)
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = trnasfer
                objInventoryMovemnt.UOM = objTr.Uom
                objInventoryMovemnt.MRP = objTr.MRP
                objInventoryMovemnt.Add_Cost = addcost
                objInventoryMovemnt.Net_Cost = netcost
                objInventoryMovemnt.ItemType = "FM"
                objInventoryMovemnt.Basic_Cost = netcost
                objInventoryMovemnt.Rec_Cost = 0
                objInventoryMovemnt.Punching_Date = PunchingTime
                ArrInventoryMovementIn.Add(objInventoryMovemnt)
            End If
        Next
        clsInventoryMovement.SaveData("Transfer", obj.Transfer_No, PunchingTime, clsCommon.GetPrintDate(PunchingTime, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)
        clsInventoryMovement.SaveData("Transfer", obj.Transfer_No, PunchingTime, clsCommon.GetPrintDate(PunchingTime, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)
        Return True
    End Function

    Private Shared Function funitem_inventoryinsert(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster, ByVal strTranferDate As String) As Boolean
        Dim ArrInventoryMovementOut As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim strQuery As String = Nothing
        Dim addcost As Decimal = 0.0
        Dim PunchingTime As DateTime = New DateTime(obj.Transfer_Date.Year, obj.Transfer_Date.Month, obj.Transfer_Date.Day, obj.Date_Time_Removal.Hour, obj.Date_Time_Removal.Minute, obj.Date_Time_Removal.Second)

        For Each ObjTr As clsTransferDetails In obj.Arr
            If CDec(ObjTr.Item_Qty) > 0 Then
                Dim trnasfer As Decimal = ObjTr.Item_Qty
                Dim itemcost As Decimal = Math.Round(CDec(ObjTr.Item_Price), 2)
                Dim reccost As Decimal = Math.Round(ObjTr.Total_Tax, 2, MidpointRounding.ToEven)
                Dim netcost As Decimal = Math.Round((trnasfer * itemcost) + reccost, 2)
                Dim uom As String = ObjTr.Uom
                Dim netcostWithOutExcise As Decimal = 0
                Dim checkexcisable As String = connectSql.RunScalar(trans, "select Excisable  from TSPL_LOCATION_MASTER where location_code = '" + obj.From_Location + "'")
                If checkexcisable = "T" Then
                    netcostWithOutExcise = Math.Round((trnasfer * itemcost), 2)
                Else
                    netcostWithOutExcise = netcost
                End If

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = obj.From_Location
                objInventoryMovemnt.Other_Location_Code = obj.To_Location
                objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.To_Location, trans)
                objInventoryMovemnt.Item_Code = ObjTr.Item_Code
                objInventoryMovemnt.Item_Desc = ObjTr.Item_Desc
                objInventoryMovemnt.Qty = trnasfer
                objInventoryMovemnt.UOM = ObjTr.Uom
                objInventoryMovemnt.MRP = ObjTr.MRP
                objInventoryMovemnt.Add_Cost = addcost
                objInventoryMovemnt.Net_Cost = netcostWithOutExcise
                objInventoryMovemnt.ItemType = "FM"
                objInventoryMovemnt.Basic_Cost = netcostWithOutExcise
                objInventoryMovemnt.Rec_Cost = reccost
                ArrInventoryMovementOut.Add(objInventoryMovemnt)

                objInventoryMovemnt = New clsInventoryMovement()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = obj.To_Location
                objInventoryMovemnt.Other_Location_Code = obj.From_Location
                objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.From_Location, trans)
                objInventoryMovemnt.Item_Code = ObjTr.Item_Code
                objInventoryMovemnt.Item_Desc = ObjTr.Item_Desc
                objInventoryMovemnt.Qty = trnasfer
                objInventoryMovemnt.UOM = ObjTr.Uom
                objInventoryMovemnt.MRP = ObjTr.MRP
                objInventoryMovemnt.Add_Cost = addcost
                objInventoryMovemnt.Net_Cost = netcost
                objInventoryMovemnt.ItemType = "FM"
                objInventoryMovemnt.Basic_Cost = netcost
                objInventoryMovemnt.Rec_Cost = 0
                ArrInventoryMovementIn.Add(objInventoryMovemnt)
            End If
        Next
        clsInventoryMovement.SaveData("Transfer", obj.Transfer_No, PunchingTime, clsCommon.GetPrintDate(PunchingTime, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)
        clsInventoryMovement.SaveData("Transfer", obj.Transfer_No, PunchingTime, clsCommon.GetPrintDate(PunchingTime, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)
        Return True
    End Function

    Private Shared Function postTransfer(ByVal trans As SqlTransaction, ByVal obj As clsTransferMaster, ByVal strTranferDate As String, ByVal isForJournalEntryOnly As Boolean)
        Dim Sql As String
        Dim lineNo As Integer = 1
        Dim fromItemQty As Decimal = 0
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0.0
        Dim toShipmentCogs As Decimal = 0.0
        Dim basicAmt As Decimal = 0.0
        'Dim frmj As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        'Dim StrVoucher As String = transportSql.fnAutoGenerateNo(trans, obj.Transfer_Date, False, obj.From_Location, False)
        Dim StrVoucher As String = transportSql.fnAutoGenerateNo(trans, obj.Transfer_Date, clsDocTransactionType.JournalEntryOther, obj.From_Location, False)
        Sql = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'MM-TF'"
        Dim strSourceDesc As String = connectSql.RunScalar(trans, Sql)
        Dim strInvoiceNo As String = obj.Transfer_No
        Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
        Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1
        Dim dt As String = connectSql.serverDate(trans)
        Dim frmloc As String = FunReturnLocation(obj.From_Location, trans)
        Dim toloc As String = FunReturnLocation(obj.To_Location, trans)
        strTranferDate = clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy")
        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "MM-TF"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", clsCommon.myCstr(obj.description)), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", clsCommon.myCstr(obj.description)), New SqlParameter("@Comments", clsCommon.myCstr(obj.description)), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", obj.To_Location), New SqlParameter("@CustVend_Name", obj.ToLoc_Desc), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", obj.Total_Item_Amount), New SqlParameter("@Total_Credit_Amt", obj.Total_Item_Amount), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentUserCode))
        Dim voucherdesc As String = Nothing
        If obj.Transfer_Type = "LO" Then
            voucherdesc = "Transfer For Load Out: " + clsCommon.myCstr(obj.Transfer_No) + " from " + obj.From_Location + " and to " + obj.To_Location + " "
        Else
            voucherdesc = "Transfer For Load In: " + clsCommon.myCstr(obj.Transfer_No) + " from " + obj.From_Location + " and to " + obj.To_Location + " "
        End If
        connectSql.RunSqlTransaction(trans, "update TSPL_JOURNAL_MASTER set Voucher_Desc ='" + voucherdesc + "' where Voucher_No = '" + StrVoucher + "'")

        If Not isForJournalEntryOnly Then
            If FunItemLocationUpdate(obj, trans) = False Then
                trans.Rollback()
                Return False
            End If
        End If

        For Each objTr As clsTransferDetails In obj.Arr
            fromShipmentCogs = fromShipmentCogs + (objTr.Item_Qty * objTr.Basic_Price)
        Next
        toShipmentCogs = fromShipmentCogs + CDec(obj.Total_Tax_Amount)
        Dim strFromInvAcc As String = ""
        Dim strFromInvAccDesc As String = ""
        Dim strToInvAcc As String = ""
        Dim strToInvAccDesc As String = ""
        Dim strShpClrAcc As String = ""
        Dim strShpClrAccDesc As String = ""
        Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.From_Location) + "'"
        Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
        Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(obj.To_Location) + "'"
        Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)
        Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
       " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'"
        strFromInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
        strToInvAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), toLocSegCode)
        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
        strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
        If strFromInvAccDesc Is Nothing Then
            Throw New Exception("Inventory Control Account not found.")
        End If
        Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
        strToInvAccDesc = connectSql.RunScalar(trans, Sql)
        If strToInvAccDesc Is Nothing Then
            Throw New Exception("Stock Reserve Account not found." + Environment.NewLine + " Account Code " + strToInvAcc)

        End If
        strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, obj.From_Location, trans)
        Dim objSeg As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
        If clsCommon.myCdbl(fromShipmentCogs) > 0 Then
            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
            lineNo = lineNo + 1
            TotalCredit = TotalCredit + fromShipmentCogs
        End If



        Dim taxAmt As Decimal
        Dim ttlTotalTaxAmt As Decimal = 0
        Dim strTaxCode As String
        Dim strNetPayAcc As String = ""
        Dim strNetPayAccDesc As String = ""

        '''' Tax1 ''''''***********************
        strTaxCode = obj.TAX1
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX1_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX1_Amt), 2)
            Else
                If obj.TAX1_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX1_Amt), 2)
                End If
            End If
            ''For Credit side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                If clsCommon.myCdbl(taxAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + taxAmt
                End If
            End If
            ''For Debit side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                If clsCommon.myCdbl(taxAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + taxAmt
                End If
            End If

            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax1

        '''' Tax2 ''''''***********************
        strTaxCode = obj.TAX2
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX2_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX2_Amt), 2)
            Else
                If obj.TAX2_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX2_Amt), 2)
                End If
            End If
            ''For Credit Side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                If clsCommon.myCdbl(taxAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + taxAmt
                End If
            End If

            ''For Debit Side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                If clsCommon.myCdbl(taxAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + taxAmt
                End If
            End If
            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax2

        '''' Tax3 ''''''***********************
        strTaxCode = obj.TAX3
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX3_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX3_Amt), 2)
            Else
                If obj.TAX3_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX3_Amt), 2)
                End If
            End If
            ''For Credit Side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ''For debit Side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax3

        '''' Tax4 ''''''***********************
        strTaxCode = obj.TAX4
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX4_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX4_Amt), 2)
            Else
                If obj.TAX4_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX4_Amt), 2)
                End If
            End If
            ''For Credit Side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ''For Debit Side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax4

        '''' Tax5 ''''''***********************
        strTaxCode = obj.TAX5
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX5_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX5_Amt), 2)
            Else
                If obj.TAX5_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX5_Amt), 2)
                End If
            End If
            ''For Credit Side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ''For Debit Side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax5

        '''' Tax6 ''''''***********************
        strTaxCode = obj.TAX6
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX6_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX6_Amt), 2)
            Else
                If obj.TAX6_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX6_Amt), 2)
                End If
            End If
            ''For Credit Side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ''For Debit Side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax6

        '''' Tax7 ''''''***********************
        strTaxCode = obj.TAX7
        If clsCommon.myLen(strTaxCode) > 0 Then
            If obj.TAX7_Amt.ToString().Substring(0, 1) = "-" Then
                taxAmt = Math.Round(CDec(obj.TAX7_Amt), 2)
            Else
                If obj.TAX7_Amt = 0 Then
                    taxAmt = 0
                Else
                    taxAmt = Math.Round(CDec(obj.TAX7_Amt), 2)
                End If
            End If
            ''For Credit Side
            Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ''For Debit Side
            Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
            If Not connectSql.RunScalar(trans, Sql).ToString() = "" Then
                strNetPayAcc = connectSql.RunScalar(trans, Sql).Replace(connectSql.RunScalar(trans, Sql).ToString().Substring(connectSql.RunScalar(trans, Sql).ToString().Length - 3, 3), fromLocSegCode)
            End If
            If Not strNetPayAcc = "" Then
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                strNetPayAccDesc = connectSql.RunScalar(trans, Sql)
            End If
            If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, obj.From_Location, trans)
                Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt), New SqlParameter("@Description", clsCommon.myCstr(obj.description)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj1.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                lineNo = lineNo + 1
                TotalCredit = TotalCredit + taxAmt
            End If
            ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
        End If
        '*********** End Tax7

        ''fromShipmentCogs = fromShipmentCogs + ttlTotalTaxAmt

        strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, obj.To_Location, trans)
        objSeg = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
        If clsCommon.myCdbl(fromShipmentCogs) > 0 Then
            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
            lineNo = lineNo + 1
            TotalDebit = TotalDebit + fromShipmentCogs
        End If


        '***************By Manoj: To add GL-Entry Transfer Filled and Empty Account
        If obj.Item_Type = "Full" Then
            Dim strFrmFilledAcc As String = Nothing
            Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
            Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strFrmFilledAccFirst Is Nothing Then
                Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)

            Else
                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.From_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                If clsCommon.myCdbl(fromShipmentCogs) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalDebit = TotalDebit + fromShipmentCogs
                End If
            End If
            Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
            Dim strToFilledAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strToFilledAcc Is Nothing Then
                Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
            Else
                strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                Dim strTOFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                If clsCommon.myCdbl(fromShipmentCogs) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + fromShipmentCogs
                End If
            End If
        ElseIf obj.Item_Type = "Empty" Then
            Dim strFrmFilledAcc As String = Nothing
            Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
            Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strFrmFilledAccFirst Is Nothing Then
                Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
            Else
                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.From_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, obj.To_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                If clsCommon.myCdbl(fromShipmentCogs) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalDebit = TotalDebit + fromShipmentCogs
                End If
            End If
            Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.To_Location + "'"
            Dim strToFilledAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strToFilledAcc Is Nothing Then
                Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
            Else
                strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, obj.To_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                Dim strTOFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, obj.To_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                If clsCommon.myCdbl(fromShipmentCogs) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + fromShipmentCogs
                End If
            End If
        End If
        '*************** End : Manoj

        '************************************* Add Empty Entry
        If Not (obj.Item_Type = "Empty") Then

            Dim EmptyAmt As Decimal = 0
            For Each objTr As clsTransferDetails In obj.Arr
                EmptyAmt = EmptyAmt + Math.Round(CDec(objTr.Item_Qty * CDec(objTr.Empty_Value)), 2)
            Next

            Dim strFrmPurchaseAcc As String = Nothing
            Dim AccSet As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + obj.Arr.Item(0).Item_Code.ToString() + "'"))
            Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
            Dim strFrmPurchaseAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strFrmPurchaseAccFirst Is Nothing Then
                Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)

            Else
                'If strFrmPurchaseAccFirst.Length > 4 Then
                '    strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                'End If
                strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAccFirst, obj.From_Location, False, trans)
                'strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, obj.From_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, obj.From_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                If clsCommon.myCdbl(EmptyAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + EmptyAmt
                End If
            End If



            Dim strFrmEmptyAcc As String = Nothing
            Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
            Dim strFrmEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strFrmEmptyAccFirst Is Nothing Then
                Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
            Else
                strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, obj.From_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, obj.From_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                If clsCommon.myCdbl(EmptyAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalDebit = TotalDebit + EmptyAmt
                End If
            End If


            strFrmEmptyAcc = Nothing
            Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + obj.From_Location + "'"
            strFrmEmptyAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strFrmEmptyAccFirst Is Nothing Then
                Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
            Else
                strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, obj.To_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, obj.To_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                If clsCommon.myCdbl(EmptyAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalCredit = TotalCredit + EmptyAmt
                End If
            End If


            strFrmPurchaseAcc = Nothing
            AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + obj.Arr.Item(0).Item_Code.ToString() + "'"))
            Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
            strFrmPurchaseAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
            If strFrmPurchaseAccFirst Is Nothing Then
                Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
            Else
                'If strFrmPurchaseAccFirst.Length > 4 Then
                '    strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                'End If
                strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAccFirst, obj.To_Location, False, trans)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                Dim loc As String = connectSql.RunScalar(trans, "select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'")
                Dim Locdesc As String = connectSql.RunScalar(trans, "select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, obj.To_Location, trans)
                objSeg = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                If clsCommon.myCdbl(EmptyAmt) > 0 Then
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", obj.description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objSeg.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                    TotalDebit = TotalDebit + EmptyAmt
                End If
            End If
            '************* End
        End If

        Dim strBalanceChk As Decimal = clsDBFuncationality.getSingleValue("select isnull(SUM(Amount),0)  from TSPL_JOURNAL_DETAILS where Voucher_No='" + StrVoucher + "'", trans)
        If strBalanceChk = 0 Then
            Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
            connectSql.RunSqlTransaction(trans, Sql)
        Else
            Throw New Exception(transportSql.GetJounalEntryException("TSPL_JOURNAL_DETAILS", StrVoucher, trans))
        End If
        ''Throw New Exception(transportSql.GetJounalEntryException(StrVoucher, trans))
        Return True
    End Function

    Private Shared Function FunItemLocationUpdate(ByVal obj As clsTransferMaster, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim SqlQuery, BatchNumber, flavourcodetem, packcodetem, empname, ItemType, strUnitCOde As String
        Dim ItemDs, ToItemDs As New DataSet()

        Dim MfgDate, ExpiryDate As String
        Dim transferdate As Date = obj.Transfer_Date
        '''' added by priti
        SqlQuery = "delete from TEMP_PROVISIONAL_SALES where Transfer_No ='" + obj.Transfer_No + "' "
        clsDBFuncationality.ExecuteNonQuery(SqlQuery, trans)
        '''' end by priti

        Dim strRouteType As String
        SqlQuery = "select Type from TSPL_ROUTE_MASTER where Route_No='" & obj.Route_No & "'"
        strRouteType = connectSql.RunScalar(trans, SqlQuery)

        For Each objTr As clsTransferDetails In obj.Arr
            Dim ItemQty, LoadOutAmtPerItem, LoadOutEmptyValPerItem, Cogs, UnitCogs, ApplyQty, ShippedQty, ItemLocationQty, Amount, COnverSion As Decimal
            If objTr.Item_Qty > 0 Then
                COnverSion = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
                strUnitCOde = objTr.Uom
                flavourcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                packcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                empname = connectSql.RunScalar(trans, "select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + obj.Salesmancode + "'")
                LoadOutAmtPerItem = objTr.Item_Qty * (objTr.BasicPrice_WithTax + objTr.TPT_Value + objTr.Empty_Value)
                LoadOutEmptyValPerItem = objTr.Item_Qty * objTr.Empty_Value

                If (strToLType = "Logical" OrElse strFromLType = "Logical") And strUnitCOde <> "SH" = True Then
                    connectSql.RunSpTransaction(trans, "sp_TEMP_PROVISIONAL_SALES_insert_update_delete", New SqlParameter("@operation", "insert"), New SqlParameter("@Comp_Code", obj.Comp_Code), New SqlParameter("@CE", obj.CE), New SqlParameter("@ADC", obj.ADC), New SqlParameter("@TDM", obj.TDM), New SqlParameter("@HOS", obj.HOS), New SqlParameter("@LoadIn_EmptyValue", 0), New SqlParameter("@LoadOut_EmptyValue", LoadOutEmptyValPerItem), New SqlParameter("@Loadout_Amount", LoadOutAmtPerItem), New SqlParameter("@RouteNo", obj.Route_No), New SqlParameter("@transferno", obj.Transfer_No), New SqlParameter("@transferdate", transferdate), New SqlParameter("@vehiclecode", obj.Vehicle_Code), New SqlParameter("@loadoutlocation", obj.From_Location), New SqlParameter("@loadinlocation", "null"), New SqlParameter("Salesmancode", obj.Salesmancode), New SqlParameter("@empname", empname), New SqlParameter("@itemcode", objTr.Item_Code), New SqlParameter("@itemdesc", objTr.Item_Desc), New SqlParameter("@loadoutqty", objTr.Item_Qty), New SqlParameter("@loadinqty", "0"), New SqlParameter("@conversionfactor", clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)), New SqlParameter("@loadinno", "null"), New SqlParameter("@breakage", "0"), New SqlParameter("@leak", "0"), New SqlParameter("@mrp", Convert.ToString(objTr.MRP)), New SqlParameter("@unitcode", Convert.ToString(objTr.Uom)), New SqlParameter("@vehicleno", obj.Vehicle_Code), New SqlParameter("@packcode", packcodetem), New SqlParameter("@flavourcode", flavourcodetem))
                    SqlQuery = "Update TEMP_PROVISIONAL_SALES set Post='N',Route_Type_Id='" & strRouteType & "' where Transfer_No='" & objTr.Transfer_No & "'"
                    connectSql.RunSqlTransaction(trans, SqlQuery)
                End If
                Dim isFirstTime As Boolean = True
                ShippedQty = Math.Round(CDec(objTr.Item_Qty / COnverSion), 2)
                SqlQuery = "SELECT Item_Qty, Amount,Batch_No,case when BATCH_NO='" + objTr.Batch_No + "' then 1 else 0 end tempSeqNO FROM TSPL_ITEM_LOCATION_DETAILS where  Item_Qty>0 and  Item_Code='" + objTr.Item_Code + "' " & _
                            " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' order by tempSeqNO desc, Expiry_Date"
                ItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                If ItemDs.Tables(0).Rows.Count > 0 Then
                    For count As Integer = 0 To ItemDs.Tables(0).Rows.Count - 1
                        If clsCommon.myCdbl(ItemDs.Tables(0).Rows(count)("Item_Qty")) <> 0 Then
                            ItemQty = CDec(ItemDs.Tables(0).Rows(count)("Item_Qty"))
                            Cogs = CDec(ItemDs.Tables(0).Rows(count)("Amount"))
                            UnitCogs = Math.Round(Cogs / ItemQty, 2)
                            BatchNumber = Convert.ToString(ItemDs.Tables(0).Rows(count)("Batch_No"))
                            If ShippedQty > ItemQty Then
                                ApplyQty = ItemQty
                                ShippedQty = ShippedQty - ItemQty
                            Else
                                ShippedQty = ShippedQty - ItemQty
                                ApplyQty = (ShippedQty + ItemQty)
                            End If
                            If ShippedQty >= 0 Then
                                ItemLocationQty = ItemQty - ShippedQty
                                Amount = Cogs - (UnitCogs * ItemLocationQty)
                                ItemLocationQty = 0
                                Amount = 0
                                Dim tim As String = objTr.Item_Code
                                SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                    "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                    " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' and batch_no = '" + BatchNumber + "'"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            Else
                                ItemLocationQty = ShippedQty * -1
                                Amount = UnitCogs * ItemLocationQty
                                ItemLocationQty = Math.Round(ItemLocationQty, 2)
                                Amount = Math.Round(Amount, 2)
                                Dim tim As String = objTr.Item_Code
                                SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                    "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                    " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' and batch_no = '" + BatchNumber + "'"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            End If
                        End If

                        If isFirstTime Then
                            SqlQuery = "SELECT Item_Qty, Amount FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
                                " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' and batch_no='" + objTr.Batch_No + "' "
                            ToItemDs.Clear()
                            ToItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                            If ToItemDs.Tables(0).Rows.Count > 0 Then
                                ItemQty = CDec(ToItemDs.Tables(0).Rows(0)(0).ToString())
                                Cogs = CDec(ToItemDs.Tables(0).Rows(0)(1).ToString())
                                If ItemQty = 0 Then
                                    UnitCogs = 0
                                Else
                                    UnitCogs = Math.Round(Cogs / ItemQty, 2)
                                End If
                                Dim TransferCogs As Decimal = ApplyQty * objTr.Item_Price
                                Dim taxamt As Decimal = connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_TRANSFER_DETAIL where Transfer_No =   '" + Convert.ToString(obj.Transfer_No) + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP) + "' and Price_Date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                                'taxamt = taxamt / Math.Round(CDec(objTr.Item_Qty / COnverSion), 2) * ApplyQty
                                'Dim we As String = objTr.Item_Code
                                SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + (ItemQty + (objTr.Item_Qty / COnverSion)).ToString() + "', " & _
                                        "Amount='" & (Cogs + TransferCogs + IIf(UnitCogs = 0, 0, taxamt)).ToString() & "' where Item_Code='" + objTr.Item_Code + "' " & _
                                        " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "'  AND  BATCH_NO='" + objTr.Batch_No + "'"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            Else
                                Dim taxamt As Decimal = connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_TRANSFER_DETAIL where Transfer_No =   '" + Convert.ToString(obj.Transfer_No) + "' and Item_Code = '" + objTr.Item_Code + "' and MRP = '" + Convert.ToString(objTr.MRP) + "' and Price_Date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                                'taxamt = taxamt / Math.Round(CDec(objTr.Item_Qty / COnverSion), 2) * ApplyQty

                                MfgDate = Date.Now.ToString("yyyy-MM-dd")
                                ExpiryDate = Date.Now.ToString("yyyy-MM-dd")
                                ItemType = connectSql.RunScalar(trans, "select ItemType  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + objTr.Batch_No + "'")
                                SqlQuery = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                                " '" + obj.ToLoc_Desc + "','" + clsCommon.myCstr(objTr.Item_Qty / COnverSion) + "','" + Convert.ToString(objTr.Item_Qty * UnitCogs + IIf(UnitCogs = 0, 0, taxamt)) + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + objTr.MRP.ToString() + "','" + MfgDate.ToString() + "','" + objTr.Batch_No + "','" + ExpiryDate.ToString() + "', '" + ItemType.Trim() + "')"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            End If
                        End If


                        If ShippedQty <= 0 Then
                            Exit For
                        End If
                        isFirstTime = False
                    Next count

                    If ShippedQty > 0 Then
                        Throw New Exception("Item location details Item - " + objTr.Item_Code + " .Item Qty " + clsCommon.myCstr(objTr.Item_Qty) + " and Remaining Qty " + clsCommon.myCstr(ShippedQty))
                    End If
                Else
                    Throw New Exception("Item - " + objTr.Item_Code + " and batch - " + objTr.Batch_No + " not found at item location details")
                End If
            End If
        Next
        Return True
    End Function

    Public Shared Function FunInsertTempProvLoadOut(ByVal obj As clsTransferMaster, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim SqlQuery, BatchNumber, flavourcodetem, packcodetem, empname, ItemType, strUnitCOde As String
        Dim ItemDs, ToItemDs As New DataSet()
        Dim ItemQty, LoadOutAmtPerItem, LoadOutEmptyValPerItem, Cogs, UnitCogs, ApplyQty, ShippedQty, ItemLocationQty, Amount, COnverSion As Decimal
        Dim MfgDate, ExpiryDate As String
        Dim transferdate As Date = obj.Transfer_Date

        '''' added by priti
        SqlQuery = "delete from TEMP_PROVISIONAL_SALES where Transfer_No ='" + obj.Transfer_No + "' "
        clsDBFuncationality.ExecuteNonQuery(SqlQuery, trans)
        '''' end by priti

        For Each objTr As clsTransferDetails In obj.Arr
            If objTr.Item_Qty > 0 Then
                COnverSion = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
                strUnitCOde = objTr.Uom
                flavourcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                packcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                empname = connectSql.RunScalar(trans, "select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + obj.Salesmancode + "'")
                LoadOutAmtPerItem = objTr.Item_Qty * (objTr.BasicPrice_WithTax + objTr.TPT_Value + objTr.Empty_Value)
                LoadOutEmptyValPerItem = objTr.Item_Qty * objTr.Empty_Value
                ''''' added by priti
                'SqlQuery = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No ='" + obj.Transfer_No + "' "
                'clsDBFuncationality.ExecuteNonQuery(SqlQuery, trans)
                ''''' enb by priti
                SqlQuery = "select Location_Type from TSPL_LOCATION_MASTER where Location_Code='" + obj.To_Location + "'"
                strToLType = connectSql.RunScalar(trans, SqlQuery)
                SqlQuery = "select Location_Type from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location + "'"
                strFromLType = connectSql.RunScalar(trans, SqlQuery)
                Dim strRouteType As String
                SqlQuery = "select Type from TSPL_ROUTE_MASTER where Route_No='" & obj.Route_No & "'"
                strRouteType = connectSql.RunScalar(trans, SqlQuery)
                If (strToLType = "Logical" OrElse strFromLType = "Logical") And strUnitCOde <> "SH" = True Then
                    connectSql.RunSpTransaction(trans, "sp_TEMP_PROVISIONAL_SALES_insert_update_delete", New SqlParameter("@operation", "insert"), New SqlParameter("@Comp_Code", obj.Comp_Code), New SqlParameter("@CE", obj.CE), New SqlParameter("@ADC", obj.ADC), New SqlParameter("@TDM", obj.TDM), New SqlParameter("@HOS", obj.HOS), New SqlParameter("@LoadIn_EmptyValue", 0), New SqlParameter("@LoadOut_EmptyValue", LoadOutEmptyValPerItem), New SqlParameter("@Loadout_Amount", LoadOutAmtPerItem), New SqlParameter("@RouteNo", obj.Route_No), New SqlParameter("@transferno", obj.Transfer_No), New SqlParameter("@transferdate", transferdate), New SqlParameter("@vehiclecode", obj.Vehicle_Code), New SqlParameter("@loadoutlocation", obj.From_Location), New SqlParameter("@loadinlocation", "null"), New SqlParameter("Salesmancode", obj.Salesmancode), New SqlParameter("@empname", empname), New SqlParameter("@itemcode", objTr.Item_Code), New SqlParameter("@itemdesc", objTr.Item_Desc), New SqlParameter("@loadoutqty", objTr.Item_Qty), New SqlParameter("@loadinqty", "0"), New SqlParameter("@conversionfactor", clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)), New SqlParameter("@loadinno", "null"), New SqlParameter("@breakage", "0"), New SqlParameter("@leak", "0"), New SqlParameter("@mrp", Convert.ToString(objTr.MRP)), New SqlParameter("@unitcode", Convert.ToString(objTr.Uom)), New SqlParameter("@vehicleno", obj.Vehicle_Code), New SqlParameter("@packcode", packcodetem), New SqlParameter("@flavourcode", flavourcodetem))

                    SqlQuery = "Update TEMP_PROVISIONAL_SALES set Post='N',Route_Type_Id='" & strRouteType & "' where Transfer_No='" & objTr.Transfer_No & "'"
                    connectSql.RunSqlTransaction(trans, SqlQuery)
                End If
                ShippedQty = Math.Round(CDec(objTr.Item_Qty / COnverSion), 2)
                SqlQuery = "SELECT Item_Qty, Amount,Batch_No FROM TSPL_ITEM_LOCATION_DETAILS where Item_Qty>0 and  Item_Code='" + objTr.Item_Code + "' " & _
    " AND location_code='" + Convert.ToString(obj.From_Location) + "'  AND BATCH_NO='" + objTr.Batch_No + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' order by Expiry_Date asc"
                ItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                If ItemDs.Tables(0).Rows.Count > 0 Then
                    For count As Integer = 0 To ItemDs.Tables(0).Rows.Count - 1
                        If Not CDec(ItemDs.Tables(0).Rows(count)("Item_Qty")) = 0 And Not CDec(ItemDs.Tables(0).Rows(count)("amount")) = 0 Then
                            ItemQty = CDec(ItemDs.Tables(0).Rows(count)("Item_Qty"))
                            Cogs = CDec(ItemDs.Tables(0).Rows(count)("Amount"))
                            UnitCogs = Math.Round(Cogs / ItemQty, 2)
                            BatchNumber = Convert.ToString(ItemDs.Tables(0).Rows(count)("Batch_No"))
                            If ShippedQty > ItemQty Then
                                ApplyQty = ItemQty
                                ShippedQty = ShippedQty - ItemQty
                            Else
                                ShippedQty = ShippedQty - ItemQty
                                ApplyQty = (ShippedQty + ItemQty)
                            End If
                            If ShippedQty >= 0 Then
                                ItemLocationQty = ItemQty - ShippedQty
                                Amount = Cogs - (UnitCogs * ItemLocationQty)
                                ItemLocationQty = 0
                                Amount = 0
                                Dim tim As String = objTr.Item_Code
                                'SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                '    "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                '    " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' and batch_no = '" + objTr.Batch_No + "'"
                                'connectSql.RunSqlTransaction(trans, SqlQuery)
                            Else
                                ItemLocationQty = ShippedQty * -1
                                Amount = UnitCogs * ItemLocationQty
                                ItemLocationQty = Math.Round(ItemLocationQty, 2)
                                Amount = Math.Round(Amount, 2)
                                Dim tim As String = objTr.Item_Code
                                'SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                '    "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                '    " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' and batch_no = '" + objTr.Batch_No + "'"
                                'connectSql.RunSqlTransaction(trans, SqlQuery)
                            End If
                        End If
                        SqlQuery = "SELECT Item_Qty, Amount FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
" AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' and batch_no='" + objTr.Batch_No + "' "
                        ToItemDs.Clear()
                        ToItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                        If ToItemDs.Tables(0).Rows.Count > 0 Then
                            ItemQty = CDec(ToItemDs.Tables(0).Rows(0)(0).ToString())
                            Cogs = CDec(ToItemDs.Tables(0).Rows(0)(1).ToString())
                            If ItemQty = 0 Then
                                UnitCogs = 0
                            Else
                                UnitCogs = Math.Round(Cogs / ItemQty, 2)
                            End If
                            Dim TransferCogs As Decimal = ApplyQty * objTr.Item_Price
                            Dim taxamt As Decimal = connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_TRANSFER_DETAIL where Transfer_No =   '" + Convert.ToString(obj.Transfer_No) + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP) + "' and Price_Date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            taxamt = taxamt / Math.Round(CDec(objTr.Item_Qty / COnverSion), 2) * ApplyQty
                            Dim we As String = objTr.Item_Code
                            '                   SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + (ItemQty + CDec(ApplyQty)).ToString() + "', " & _
                            '"Amount='" & (Cogs + TransferCogs + taxamt).ToString() & "' where Item_Code='" + objTr.Item_Code + "' " & _
                            '" AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "'  AND  BATCH_NO='" + objTr.Batch_No + "'"
                            '                   connectSql.RunSqlTransaction(trans, SqlQuery)
                        Else
                            Dim taxamt As Decimal = connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_TRANSFER_DETAIL where Transfer_No =   '" + Convert.ToString(obj.Transfer_No) + "' and Item_Code = '" + objTr.Item_Code + "' and MRP = '" + Convert.ToString(objTr.MRP) + "' and Price_Date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            taxamt = taxamt / Math.Round(CDec(objTr.Item_Qty / COnverSion), 2) * ApplyQty

                            MfgDate = Date.Now.ToString("yyyy-MM-dd")
                            ExpiryDate = Date.Now.ToString("yyyy-MM-dd")
                            ItemType = connectSql.RunScalar(trans, "select ItemType  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + objTr.Batch_No + "'")
                            'SqlQuery = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                            '" '" + obj.ToLoc_Desc + "','" + Convert.ToString(ApplyQty) + "','" + Convert.ToString(ApplyQty * UnitCogs + taxamt) + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + objTr.MRP.ToString() + "','" + MfgDate.ToString() + "','" + objTr.Batch_No + "','" + ExpiryDate.ToString() + "', '" + ItemType.Trim() + "')"
                            'connectSql.RunSqlTransaction(trans, SqlQuery)
                        End If
                        If ShippedQty = 0 Then
                            Exit For
                        End If
                    Next count
                End If
            End If
        Next
        Return True
    End Function

    Private Shared Function FunItemLocationRevrsUpdate(ByVal obj As clsTransferMaster, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim SqlQuery, BatchNumber, flavourcodetem, packcodetem, empname, ItemType As String
        Dim ItemDs, ToItemDs As New DataSet()
        Dim ItemQty, Cogs, UnitCogs, ApplyQty, ShippedQty, ItemLocationQty, Amount, COnverSion As Decimal
        Dim MfgDate, ExpiryDate As String
        Dim transferdate As Date = obj.Transfer_Date
        BatchNumber = ""
        For Each objTr As clsTransferDetails In obj.Arr
            If objTr.Item_Qty > 0 Then
                COnverSion = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
                flavourcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                packcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                empname = connectSql.RunScalar(trans, "select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + obj.Salesmancode + "'")
                'If (strToLType = "Logical" OrElse strFromLType = "Logical") = True Then
                '    connectSql.RunSpTransaction(trans, "sp_TEMP_PROVISIONAL_SALES_insert_update_delete", New SqlParameter("@operation", "insert"), New SqlParameter("@transferno", obj.Transfer_No), New SqlParameter("@transferdate", transferdate), New SqlParameter("@vehiclecode", obj.Vehicle_Code), New SqlParameter("@loadoutlocation", obj.From_Location), New SqlParameter("@loadinlocation", "null"), New SqlParameter("Salesmancode", obj.Salesmancode), New SqlParameter("@empname", empname), New SqlParameter("@itemcode", objTr.Item_Code), New SqlParameter("@itemdesc", objTr.Item_Desc), New SqlParameter("@loadoutqty", objTr.Item_Qty), New SqlParameter("@loadinqty", "0"), New SqlParameter("@conversionfactor", clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)), New SqlParameter("@loadinno", "null"), New SqlParameter("@breakage", "0"), New SqlParameter("@leak", "0"), New SqlParameter("@mrp", Convert.ToString(objTr.MRP)), New SqlParameter("@unitcode", Convert.ToString(objTr.Uom)), New SqlParameter("@vehicleno", obj.Vehicle_Code), New SqlParameter("@packcode", packcodetem), New SqlParameter("@flavourcode", flavourcodetem))
                'End If
                ShippedQty = Math.Round(CDec(objTr.Item_Qty / COnverSion), 2)
                SqlQuery = "SELECT Item_Qty, Amount,Batch_No FROM TSPL_ITEM_LOCATION_DETAILS where Item_Qty>0 and  Item_Code='" + objTr.Item_Code + "' " & _
    " AND location_code='" + Convert.ToString(obj.From_Location) + "'   and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' order by Expiry_Date asc"
                ItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                If ItemDs.Tables(0).Rows.Count > 0 Then
                    For count As Integer = 0 To ItemDs.Tables(0).Rows.Count - 1
                        If Not CDec(ItemDs.Tables(0).Rows(count)("Item_Qty")) = 0 And Not CDec(ItemDs.Tables(0).Rows(count)("amount")) = 0 Then
                            ItemQty = CDec(ItemDs.Tables(0).Rows(count)("Item_Qty"))
                            Cogs = CDec(ItemDs.Tables(0).Rows(count)("Amount"))
                            UnitCogs = Math.Round(Cogs / ItemQty, 2)
                            BatchNumber = Convert.ToString(ItemDs.Tables(0).Rows(count)("Batch_No"))
                            If ShippedQty > ItemQty Then
                                ApplyQty = ItemQty
                                ShippedQty = ShippedQty - ItemQty
                            Else
                                ShippedQty = ShippedQty - ItemQty
                                ApplyQty = (ShippedQty + ItemQty)
                            End If
                            If ShippedQty >= 0 Then
                                ItemLocationQty = ItemQty - ShippedQty
                                Amount = Cogs - (UnitCogs * ItemLocationQty)
                                ItemLocationQty = 0
                                Amount = 0
                                Dim tim As String = objTr.Item_Code
                                SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                    "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                    " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' and batch_no = '" + BatchNumber + "'"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            Else
                                ItemLocationQty = ShippedQty * -1
                                Amount = UnitCogs * ItemLocationQty
                                ItemLocationQty = Math.Round(ItemLocationQty, 2)
                                Amount = Math.Round(Amount, 2)
                                Dim tim As String = objTr.Item_Code
                                SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                    "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + objTr.Item_Code + "' " & _
                                    " AND location_code='" + Convert.ToString(obj.From_Location) + "' and MRP='" + (CDec(objTr.MRP) * COnverSion).ToString() + "' and batch_no = '" + BatchNumber + "'"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            End If
                        End If
                        SqlQuery = "SELECT Item_Qty, Amount FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' " & _
" AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' "
                        ToItemDs.Clear()
                        ToItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                        If ToItemDs.Tables(0).Rows.Count > 0 Then
                            ItemQty = CDec(ToItemDs.Tables(0).Rows(0)(0).ToString())
                            Cogs = CDec(ToItemDs.Tables(0).Rows(0)(1).ToString())
                            If ItemQty = 0 Then
                                UnitCogs = 0
                            Else
                                UnitCogs = Math.Round(Cogs / ItemQty, 2)
                            End If
                            Dim TransferCogs As Decimal = ApplyQty * objTr.Item_Price
                            Dim taxamt As Decimal = connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_TRANSFER_DETAIL where Transfer_No =   '" + Convert.ToString(obj.Transfer_No) + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP) + "' and Price_Date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            taxamt = taxamt / Math.Round(CDec(objTr.Item_Qty / COnverSion), 2) * ApplyQty
                            Dim we As String = objTr.Item_Code
                            SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + (ItemQty + CDec(ApplyQty)).ToString() + "', " & _
         "Amount='" & (Cogs + TransferCogs + taxamt).ToString() & "' where Item_Code='" + objTr.Item_Code + "' " & _
         " AND location_code='" + Convert.ToString(obj.To_Location) + "' AND MRP='" + objTr.MRP.ToString() + "' "
                            connectSql.RunSqlTransaction(trans, SqlQuery)
                        Else
                            Dim taxamt As Decimal = connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_TRANSFER_DETAIL where Transfer_No =   '" + Convert.ToString(obj.Transfer_No) + "' and Item_Code = '" + objTr.Item_Code + "' and MRP = '" + Convert.ToString(objTr.MRP) + "' and Price_Date = '" + CDate(objTr.Price_Date).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(objTr.Batch_No) + "'")
                            taxamt = taxamt / Math.Round(CDec(objTr.Item_Qty / COnverSion), 2) * ApplyQty

                            MfgDate = Date.Now.ToString("yyyy-MM-dd")
                            ExpiryDate = Date.Now.ToString("yyyy-MM-dd")
                            ItemType = connectSql.RunScalar(trans, "select ItemType  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + objTr.Item_Code + "' and Location_Code = '" + Convert.ToString(obj.From_Location) + "' and Batch_No = '" + BatchNumber + "'")
                            SqlQuery = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + objTr.Item_Code + "','" + objTr.Item_Desc + "','" + Convert.ToString(obj.To_Location) + "'," & _
                            " '','" + Convert.ToString(ApplyQty) + "','" + Convert.ToString(ApplyQty * UnitCogs + taxamt) + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + objTr.MRP.ToString() + "','" + MfgDate.ToString() + "','" + BatchNumber + "','" + ExpiryDate.ToString() + "', '" + ItemType + "')"
                            connectSql.RunSqlTransaction(trans, SqlQuery)
                        End If
                        If ShippedQty = 0 Then
                            Exit For
                        End If
                    Next count
                End If
            End If
        Next
        Return True
    End Function

    Private Shared Function FunReturnLocation(ByVal loc As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        If trans Is Nothing Then
            Return connectSql.RunScalar("select Location_Type  from TSPL_LOCATION_MASTER where Location_Code = '" + loc + "'")
        Else
            Return connectSql.RunScalar(trans, "select Location_Type  from TSPL_LOCATION_MASTER where Location_Code = '" + loc + "'")

        End If
    End Function

    ''''' commented by priti on 3.08.12
    ''Public Shared Function FunInsertTEMP_PROVISIONAL(ByVal dt As DataTable) As Boolean
    ''    Dim flavourcodetem, packcodetem, empname As String
    ''    Dim Conversion As Decimal = 0
    ''    Dim ChkItemVerifyDs As New DataSet()
    ''    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    ''    Try
    ''        For Each dr As DataRow In dt.Rows
    ''            Dim LoadIn As String = dr("Transfer_No").ToString()
    ''            Dim LoadOut As String = dr("Load_Out_No").ToString()
    ''            If (clsCommon.myLen(LoadOut) <= 0) Then
    ''                Throw New Exception("LoadOutNo not found to Post")
    ''            End If
    ''            Dim obj As clsTransferMaster = clsTransferMaster.GetData(LoadOut, trans)
    ''            Dim transferdate As Date = obj.Transfer_Date
    ''            For Each objTr As clsTransferDetails In obj.Arr
    ''                If objTr.Item_Qty > 0 Then
    ''                    Conversion = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
    ''                    flavourcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
    ''                    packcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(objTr.Item_Code) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
    ''                    empname = connectSql.RunScalar(trans, "select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + obj.Salesmancode + "'")
    ''                    connectSql.RunSpTransaction(trans, "sp_TEMP_PROVISIONAL_SALES_insert_update_delete", New SqlParameter("@operation", "insert"), New SqlParameter("@transferno", obj.Transfer_No), New SqlParameter("@transferdate", transferdate), New SqlParameter("@vehiclecode", obj.Vehicle_Code), New SqlParameter("@loadoutlocation", obj.From_Location), New SqlParameter("@loadinlocation", "null"), New SqlParameter("Salesmancode", obj.Salesmancode), New SqlParameter("@empname", empname), New SqlParameter("@itemcode", objTr.Item_Code), New SqlParameter("@itemdesc", objTr.Item_Desc), New SqlParameter("@loadoutqty", objTr.Item_Qty), New SqlParameter("@loadinqty", "0"), New SqlParameter("@conversionfactor", clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)), New SqlParameter("@loadinno", "null"), New SqlParameter("@breakage", "0"), New SqlParameter("@leak", "0"), New SqlParameter("@mrp", Convert.ToString(objTr.MRP)), New SqlParameter("@unitcode", Convert.ToString(objTr.Uom)), New SqlParameter("@vehicleno", obj.Vehicle_Code), New SqlParameter("@packcode", packcodetem), New SqlParameter("@flavourcode", flavourcodetem))
    ''                End If
    ''            Next
    ''            If (clsCommon.myLen(LoadIn) <= 0) Then
    ''                Throw New Exception("LoadInNo not found to Post")
    ''            End If
    ''            obj = New clsTransferMaster()
    ''            obj = clsTransferMaster.GetData(LoadIn, trans)
    ''            For Each objTr As clsTransferDetails In obj.Arr
    ''                Conversion = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Uom, trans)
    ''                Dim loadintem As Decimal = connectSql.RunScalar(trans, "select loadinqty  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
    ''                Dim breakagetem As Decimal = connectSql.RunScalar(trans, "select  breakage  from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
    ''                Dim leaktem As Decimal = connectSql.RunScalar(trans, "select leak from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
    ''                Dim Amount As Decimal = connectSql.RunScalar(trans, "select isnull(Amount,0) from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
    ''                Dim Shortage As Decimal = connectSql.RunScalar(trans, "select Shortage from TEMP_PROVISIONAL_SALES where Transfer_No='" + obj.Load_Out_No + "'  and item_code = '" + Convert.ToString(objTr.Item_Code) + "' and mrp = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
    ''                loadintem = Math.Round(loadintem + objTr.LoadIn_Qty / Conversion, 2)
    ''                breakagetem = Math.Round(breakagetem + objTr.Burst / Conversion, 2)
    ''                leaktem = Math.Round(leaktem + objTr.Leak / Conversion, 2)
    ''                Shortage = Math.Round(Shortage + objTr.Shortage / Conversion, 2)
    ''                Amount = Math.Round(Amount + (CDec(objTr.LoadIn_Qty) + CDec(objTr.Burst) + CDec(objTr.Leak) + CDec(objTr.Shortage)) * (CDec(objTr.BasicPrice_WithTax) + CDec(objTr.TPT_Value) + CDec(objTr.Empty_Value)), 2)
    ''                Dim FlvrCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
    ''                Dim PackCode As String = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + objTr.Item_Code + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
    ''                Dim ItmChkQry As String = "select * from TEMP_PROVISIONAL_SALES where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' "
    ''                ChkItemVerifyDs.Clear()
    ''                ChkItemVerifyDs = connectSql.RunSQLReturnDS(trans, ItmChkQry)
    ''                If ChkItemVerifyDs.Tables(0).Rows.Count > 0 Then
    ''                    connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set Shortage=" + Shortage.ToString() + ", Amount=" + Amount.ToString() + ", LoadInQty = '" + Convert.ToString(loadintem) + "', Breakage ='" + Convert.ToString(breakagetem) + "', Leak = '" + Convert.ToString(leaktem) + "' where Transfer_No = '" + obj.Load_Out_No + "' and Item_Code = '" + Convert.ToString(objTr.Item_Code) + "' and MRP = '" + Convert.ToString(objTr.MRP * Conversion) + "'")
    ''                Else
    ''                    connectSql.RunSqlTransaction(trans, "insert TEMP_PROVISIONAL_SALES values ('" + obj.Load_Out_No + "','" + Format(obj.Transfer_Date, "yyyy-MM-dd") + "','" + obj.Vehicle_Code + "','" + obj.To_Location + "','null','" + obj.Salesmancode + "','" + obj.FromLoc_Desc + "','" + objTr.Item_Code + "','" + objTr.Item_Desc + "',0,0,0,'',0,0," + objTr.MRP.ToString() + ",'" + objTr.Uom + "','" + obj.Vehicle_Code + "','" + PackCode + "','" + FlvrCode + "'," + Amount.ToString() + ",0)")
    ''                End If
    ''            Next
    ''        Next
    ''        trans.Commit()
    ''    Catch ex As Exception
    ''        trans.Rollback()
    ''        common.clsCommon.MyMessageBoxShow(ex.Message)
    ''    End Try
    ''End Function

    '''' comments ends here

    Public Shared Function getBalanceWithUnapproveTranferInFC(ByVal strTransferNo As String, ByVal strICode As String, ByVal strCurrCode As String, ByVal dblMRP As Double, ByVal strUOM As String) As Double
        dblMRP = dblMRP * clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
        Dim qry As String = "select sum(Item_Qty * case when RI in (1,5) then 1 else case when RI in (2,3,4) then -1 else 0 end end) as BalanceQty from (" + Environment.NewLine
        qry += " select Transfer_No,Item_Code,Price_Date,Item_Qty,1 as RI from TSPL_TRANSFER_DETAIL where Transfer_No='" + strTransferNo + "' and Item_Code='" + strICode + "' and MRP=" + clsCommon.myCstr(dblMRP) + "" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor as Item_Qty ,2 as RI" + Environment.NewLine
        qry += " from TSPL_SHIPMENT_DETAILS " + Environment.NewLine
        qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No" + Environment.NewLine
        qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code" + Environment.NewLine
        qry += "where TSPL_SHIPMENT_MASTER.Is_Post='Y' and  TSPL_SHIPMENT_MASTER.Transfer_No='" + strTransferNo + "' and TSPL_SHIPMENT_DETAILS.Item_Code='" + strICode + "'  and  (Conversion_Factor*MRP_Amt)=" + clsCommon.myCstr(dblMRP) + "" + Environment.NewLine
        qry += "union all " + Environment.NewLine
        qry += "select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor   as Item_Qty ,3 as RI" + Environment.NewLine
        qry += " from TSPL_SHIPMENT_DETAILS" + Environment.NewLine
        qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No" + Environment.NewLine
        qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code" + Environment.NewLine
        qry += "where TSPL_SHIPMENT_MASTER.Is_Post='N' and  TSPL_SHIPMENT_MASTER.Transfer_No='" + strTransferNo + "' and TSPL_SHIPMENT_DETAILS.Item_Code='" + strICode + "'  and  (Conversion_Factor*MRP_Amt)=" + clsCommon.myCstr(dblMRP) + " and TSPL_SHIPMENT_MASTER.Shipment_No not in('" + strCurrCode + "') " + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,(ISNULL( TSPL_TRANSFER_DETAIL.Burst,0)+isnull(TSPL_TRANSFER_DETAIL.Leak,0)+isnull(TSPL_TRANSFER_DETAIL.Shortage,0)+TSPL_TRANSFER_DETAIL.LoadIn_Qty) /Conversion_Factor  as Item_Qty  ,4 as RI" + Environment.NewLine
        qry += " from TSPL_TRANSFER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
        qry += "where TSPL_TRANSFER_HEAD.Load_Out_No='" + strTransferNo + "' and Transfer_Type='LI' and TSPL_TRANSFER_DETAIL.Item_Code='" + strICode + "' and  (Conversion_Factor*MRP)=" + clsCommon.myCstr(dblMRP) + " "

        qry += " Union all " + Environment.NewLine
        qry += "  select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Price_Date,TSPL_SALE_RETURN_DETAIL.Return_Qty/Conversion_Factor  as Item_Qty  ,5 as RI "
        qry += "  from TSPL_SALE_RETURN_DETAIL "
        qry += "  left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No "
        qry += "  left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No "
        qry += "  left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No "
        qry += "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code "
        qry += "  where  TSPL_SHIPMENT_MASTER.Transfer_No='" + strTransferNo + "' and  TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer' and LEN(ISNULL(TSPL_SHIPMENT_MASTER.Transfer_No,''))>0  and TSPL_SALE_RETURN_DETAIL.Item_Code='" + strICode + "'  and  (MRP_Amt*Conversion_Factor)= " + clsCommon.myCstr(dblMRP) + " "
        qry += ") xxx "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function



End Class

Public Class clsTransferDetails
#Region "Variable"
    Public Line_No As Integer = 0
    Public Transfer_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Price_Date As Date
    Public Item_Qty As Double = 0
    Public MRP As Double = 0
    Public Item_Price As Double = 0
    Public Amount As Double = 0
    Public Disc_Perc As Double = 0
    Public Disc_Amount As Double = 0
    Public Net_Amount As Double = 0
    Public Pending_Qty As Double = 0
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public Tax1_Assessable_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public Tax2_Assessable_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public Tax3_Assessable_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public Tax4_Assessable_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public Tax5_Assessable_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public Tax6_Assessable_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public Tax7_Assessable_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public Tax8_Assessable_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public Tax9_Assessable_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Tax10_Assessable_Amt As Double = Nothing
    Public Total_Tax As Double = 0
    Public Total_Item_Amt As Double = 0
    Public Complete As String = Nothing
    Public Assessable_Amt As Double = 0
    Public LoadIn_Qty As Double = 0
    Public Uom As String = Nothing
    Public Breakage As Double = 0
    Public Basic_Price As Double = 0
    Public Batch_No As String = Nothing
    Public BasicPrice_WithTax As Double = 0
    Public Empty_Value As Double = 0
    Public TPT_Value As Double = 0
    Public Burst As Double = 0
    Public Leak As Double = 0
    Public Shortage As Double = 0
    Public Pending_Balance_In_Bottle As Double = 0
    Public Total_Item_Cost As Double = 0
    Public MRP_In_Bottle As Double = 0
    Public Total_QtyInCase As Double = 0
    Public Basic_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTransferDetails), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTransferDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Transfer_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Item_Qty", obj.Item_Qty)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Item_Price", obj.Item_Price)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Perc", obj.Disc_Perc)
                clsCommon.AddColumnsForChange(coll, "Disc_Amount", obj.Disc_Amount)
                clsCommon.AddColumnsForChange(coll, "Net_Amount", obj.Net_Amount)
                clsCommon.AddColumnsForChange(coll, "Pending_Qty", obj.Pending_Qty)

                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax1_Assessable_Amt", obj.Tax1_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax2_Assessable_Amt", obj.Tax2_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax3_Assessable_Amt", obj.Tax3_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax4_Assessable_Amt", obj.Tax4_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax5_Assessable_Amt", obj.Tax5_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax6_Assessable_Amt", obj.Tax6_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax7_Assessable_Amt", obj.Tax7_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax8_Assessable_Amt", obj.Tax8_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax9_Assessable_Amt", obj.Tax9_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax10_Assessable_Amt", obj.Tax10_Assessable_Amt)

                clsCommon.AddColumnsForChange(coll, "Total_Tax", obj.Total_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_Item_Amt", obj.Total_Item_Amt)
                clsCommon.AddColumnsForChange(coll, "Complete", obj.Complete)
                clsCommon.AddColumnsForChange(coll, "Assessable_Amt", obj.Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "LoadIn_Qty", obj.LoadIn_Qty)
                clsCommon.AddColumnsForChange(coll, "Uom", obj.Uom)
                clsCommon.AddColumnsForChange(coll, "Breakage", obj.Breakage)
                clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                clsCommon.AddColumnsForChange(coll, "BasicPrice_WithTax", obj.BasicPrice_WithTax)
                clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
                clsCommon.AddColumnsForChange(coll, "TPT_Value", obj.TPT_Value)
                clsCommon.AddColumnsForChange(coll, "Burst", obj.Burst)
                clsCommon.AddColumnsForChange(coll, "Leak", obj.Leak)
                clsCommon.AddColumnsForChange(coll, "Shortage", obj.Shortage)
                clsCommon.AddColumnsForChange(coll, "Pending_Balance_In_Bottle", obj.Pending_Balance_In_Bottle)
                clsCommon.AddColumnsForChange(coll, "Total_Item_Cost", obj.Total_Item_Cost)
                clsCommon.AddColumnsForChange(coll, "MRP_In_Bottle", obj.MRP_In_Bottle)
                clsCommon.AddColumnsForChange(coll, "Total_QtyInCase", obj.Total_QtyInCase)
                clsCommon.AddColumnsForChange(coll, "Basic_Amt", obj.Basic_Amt)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSFER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
