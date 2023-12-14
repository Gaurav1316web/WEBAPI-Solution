Imports common
Imports System.Data.SqlClient
Public Class clsIndentHead
#Region "Variable"
    Public Indent_No As String = Nothing
    Public Indent_Date As Date = Nothing
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
    Public Post As Integer = 0
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
    Public Total_Basic_Amt As Double = 0

    Public Arr As List(Of clsIndentDetail) = Nothing

    Public Location As String = Nothing
    Public Cust_Account As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsIndentHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsIndentHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(obj.Indent_No) > 0 Then
                qry = "select Post from TSPL_INDENT_HEAD where Indent_No='" + obj.Indent_No + "'"
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Already Posted Trasaction")
                End If
            End If



            qry = "delete from TSPL_INDENT_DETAIL where Indent_No='" + obj.Indent_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Indent_No = clsERPFuncationality.GetNextCode(trans, obj.Indent_Date, clsDocType.Indent, "", "")
            End If
            If clsCommon.myLen(obj.Indent_No) <= 0 Then
                Throw New Exception("Indent No not found")
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Indent_Date", clsCommon.GetPrintDate(obj.EntryDateTime, "dd/MMM/yyyy"))
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
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Indent_No", obj.Indent_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INDENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INDENT_HEAD", OMInsertOrUpdate.Update, "TSPL_INDENT_HEAD.Indent_No='" + obj.Indent_No + "'", trans)
            End If
            clsIndentDetail.SaveData(obj.Indent_No, obj.Arr, trans)

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Indent_No), "TSPL_INDENT_HEAD", "Indent_No", "TSPL_INDENT_DETAIL", "Indent_No", trans)
            'End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal Trans As SqlTransaction) As clsIndentHead
        Dim obj As clsIndentHead = Nothing
        Dim qry As String = "select * from TSPL_INDENT_HEAD where Indent_No = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsIndentHead
            If dt.Rows(0)("Indent_Date") IsNot DBNull.Value Then
                obj.Indent_Date = clsCommon.myCDate(dt.Rows(0)("Indent_Date"))
            End If
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Indent_No = clsCommon.myCstr(dt.Rows(0)("Indent_No"))
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

            obj.is_Auto_Created_Trans = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Auto_Created_Trans")) = 1, True, False)
            qry = "select * from TSPL_INDENT_DETAIL where Indent_No='" + strCode + "' order by Line_No"
            dt = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsIndentDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsIndentDetail = New clsIndentDetail()
                    objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objtr.Indent_No = clsCommon.myCstr(dr("Indent_No"))
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

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Indent No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsIndentHead = clsIndentHead.GetData(strDocNo, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Indent_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Post = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_INDENT_HEAD set Post=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Indent_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsIndentDetail
#Region "Variable"
    Public Line_No As Integer = 0
    Public Indent_No As String = Nothing
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

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsIndentDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsIndentDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Indent_No", strDocNo)
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

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INDENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceQty(ByVal strIndentNo As String, ByVal strICode As String, ByVal dblMRP As Double, ByVal strTransferNo As String, ByVal isBalanceQtyWithTolerance As Boolean) As Double
        Dim dblRetQty As Double = 0
        Dim qry As String = "select Indent_No,Item_Code,MRP,SUM(Item_Qty*RI) as BalanceQty from ("
        qry += " select TSPL_INDENT_DETAIL.Indent_No,TSPL_INDENT_DETAIL.Item_Code,TSPL_INDENT_DETAIL.MRP,"
        If isBalanceQtyWithTolerance Then
            Dim dblTolerence As Double = 100 + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IndentTolerence, clsFixedParameterCode.IndentTolerence, Nothing))
            qry += " (TSPL_INDENT_DETAIL.Item_Qty* " + clsCommon.myCstr(dblTolerence) + "/100 ) as Item_Qty"
        Else
            qry += " TSPL_INDENT_DETAIL.Item_Qty"
        End If

        qry += " ,1 as RI from TSPL_INDENT_DETAIL"
        qry += " where Indent_No='" + strIndentNo + "' and  TSPL_INDENT_DETAIL.Item_Code='" + strICode + "' and TSPL_INDENT_DETAIL.MRP='" + clsCommon.myCstr(dblMRP) + "'"
        qry += " union all"
        qry += " select TSPL_TRANSFER_HEAD.Against_Indent_No as Indent_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.MRP,TSPL_TRANSFER_DETAIL.Item_Qty,-1 as RI"
        qry += " from TSPL_TRANSFER_DETAIL"
        qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No"
        qry += " where TSPL_TRANSFER_HEAD.Against_Indent_No='" + strIndentNo + "' and  TSPL_TRANSFER_DETAIL.Item_Code='" + strICode + "' and TSPL_TRANSFER_DETAIL.MRP='" + clsCommon.myCstr(dblMRP) + "' and TSPL_TRANSFER_HEAD.Transfer_No not in ('" + strTransferNo + "')"
        qry += " )xxx group by Indent_No,Item_Code,MRP  having SUM(Item_Qty*RI)>0"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dblRetQty = clsCommon.myCdbl(dt.Rows(0)("BalanceQty"))
        End If
        Return dblRetQty
    End Function
End Class

