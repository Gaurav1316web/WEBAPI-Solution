Imports common
Imports System.Data.SqlClient

'Public Class clsTransferHead
'#Region "Variables"
'    Public Transfer_No As String = Nothing
'    Public Transfer_Date As DateTime
'    Public Posting_Date As DateTime
'    Public Transfer_Type As String = Nothing
'    Public Load_Out_No As String = Nothing
'    Public From_Location As String = Nothing
'    Public To_Location As String = Nothing
'    Public Price_Date As DateTime
'    Public Tax_Group As String = Nothing
'    Public Reference As String = Nothing
'    Public description As String = Nothing
'    Public Route_No As String = Nothing
'    Public Salesmancode As String = Nothing
'    Public Price_Code As String = Nothing
'    Public Vehicle_Code As String = Nothing
'    Public Vehicle_No As String = Nothing
'    Public Mode_Of_Transport As String = Nothing
'    Public Km_Reading As String = Nothing
'    Public TAX1 As String = Nothing
'    Public TAX1_Rate As Double = 0
'    Public TAX1_Amt As Double = 0
'    Public Tax1_Assessable_Amt As Double = 0
'    Public TAX2 As String = Nothing
'    Public TAX2_Rate As Double = 0
'    Public TAX2_Amt As Double = 0
'    Public Tax2_Assessable_Amt As Double = 0
'    Public TAX3 As String = Nothing
'    Public TAX3_Rate As Double = 0
'    Public TAX3_Amt As Double = 0
'    Public Tax3_Assessable_Amt As Double = 0
'    Public TAX4 As String = Nothing
'    Public TAX4_Rate As Double = 0
'    Public TAX4_Amt As Double = 0
'    Public Tax4_Assessable_Amt As Double = 0
'    Public TAX5 As String = Nothing
'    Public TAX5_Rate As Double = 0
'    Public TAX5_Amt As Double = 0
'    Public Tax5_Assessable_Amt As Double = 0
'    Public TAX6 As String = Nothing
'    Public TAX6_Rate As Double = 0
'    Public TAX6_Amt As Double = 0
'    Public Tax6_Assessable_Amt As Double = 0
'    Public TAX7 As String = Nothing
'    Public TAX7_Rate As Double = 0
'    Public TAX7_Amt As Double = 0
'    Public Tax7_Assessable_Amt As Double = 0
'    Public TAX8 As String = Nothing
'    Public TAX8_Rate As Double = 0
'    Public TAX8_Amt As Double = 0
'    Public Tax8_Assessable_Amt As Double = 0
'    Public TAX9 As String = Nothing
'    Public TAX9_Rate As Double = 0
'    Public TAX9_Amt As Double = 0
'    Public Tax9_Assessable_Amt As Double = 0
'    Public TAX10 As String = Nothing
'    Public TAX10_Rate As Double = 0
'    Public TAX10_Amt As Double = 0
'    Public Tax10_Assessable_Amt As Double = 0
'    Public Item_Amount As Double = 0
'    Public Total_Tax_Amount As Double = 0
'    Public Total_Item_Amount As Double = 0
'    Public Post As String = Nothing
'    Public Created_By As String = Nothing
'    Public Created_Date As DateTime
'    Public Modify_By As String = Nothing
'    Public Modify_Date As DateTime
'    Public Level1_User_code As String = Nothing
'    Public Level2_User_code As String = Nothing
'    Public Level3_User_code As String = Nothing
'    Public Level4_User_code As String = Nothing
'    Public Level5_User_code As String = Nothing
'    Public Comp_Code As String = Nothing
'    Public Load_Out_Date As DateTime
'    Public Is_Shipped As String = Nothing
'    Public Trip_No As String = Nothing
'    Public Item_Type As String = Nothing
'    Public Date_Time_Removal As DateTime
'    Public Is_Complete As String = Nothing
'    Public HOS As String = Nothing
'    Public TDM As String = Nothing
'    Public ADC As String = Nothing
'    Public CE As String = Nothing
'    Public EntryDateTime As DateTime
'    Public FromLoc_Desc As String = Nothing
'    Public ToLoc_Desc As String = Nothing
'    Public Route_Desc As String = Nothing
'    Public Price_Desc As String = Nothing
'    Public Vehicle_Desc As String = Nothing
'    Public Printed As String = Nothing
'    Public Total_Transfer_Amount As Double = 0
'    Public Total_Transfer_QtyInCase As Double = 0
'    Public Quick_Settlement As String = Nothing
'    Public Sale_Invoice_Completed As Double = 0
'    Public Is_AgainstFormF As Double = 0
'    Public Location_Type As String = Nothing
'    Public Route_Type_Id As String = Nothing
'    Public Tax_Group_Type As String = Nothing
'#End Region

'End Class

'Public Class clsTransferDetails
'#Region "Variables"
'    Public Line_No As Double = 0
'    Public Transfer_No As String = Nothing
'    Public Item_Code As String = Nothing
'    Public Item_Desc As String = Nothing
'    Public Price_Date As DateTime
'    Public Item_Qty As Double = 0
'    Public MRP As Double = 0
'    Public Item_Price As Double = 0
'    Public Amount As Double = 0
'    Public Disc_Perc As Double = 0
'    Public Disc_Amount As Double = 0
'    Public Net_Amount As Double = 0
'    Public Pending_Qty As Double = 0
'    Public TAX1 As String = Nothing
'    Public TAX1_Rate As Double = 0
'    Public TAX1_Amt As Double = 0
'    Public Tax1_Assessable_Amt As Double = 0
'    Public TAX2 As String = Nothing
'    Public TAX2_Rate As Double = 0
'    Public TAX2_Amt As Double = 0
'    Public Tax2_Assessable_Amt As Double = 0
'    Public TAX3 As String = Nothing
'    Public TAX3_Rate As Double = 0
'    Public TAX3_Amt As Double = 0
'    Public Tax3_Assessable_Amt As Double = 0
'    Public TAX4 As String = Nothing
'    Public TAX4_Rate As Double = 0
'    Public TAX4_Amt As Double = 0
'    Public Tax4_Assessable_Amt As Double = 0
'    Public TAX5 As String = Nothing
'    Public TAX5_Rate As Double = 0
'    Public TAX5_Amt As Double = 0
'    Public Tax5_Assessable_Amt As Double = 0
'    Public TAX6 As String = Nothing
'    Public TAX6_Rate As Double = 0
'    Public TAX6_Amt As Double = 0
'    Public Tax6_Assessable_Amt As Double = 0
'    Public TAX7 As String = Nothing
'    Public TAX7_Rate As Double = 0
'    Public TAX7_Amt As Double = 0
'    Public Tax7_Assessable_Amt As Double = 0
'    Public TAX8 As String = Nothing
'    Public TAX8_Rate As Double = 0
'    Public TAX8_Amt As Double = 0
'    Public Tax8_Assessable_Amt As Double = 0
'    Public TAX9 As String = Nothing
'    Public TAX9_Rate As Double = 0
'    Public TAX9_Amt As Double = 0
'    Public Tax9_Assessable_Amt As Double = 0
'    Public TAX10 As String = Nothing
'    Public TAX10_Rate As Double = 0
'    Public TAX10_Amt As Double = 0
'    Public Tax10_Assessable_Amt As Double = 0
'    Public Total_Tax As Double = 0
'    Public Total_Item_Amt As Double = 0
'    Public Complete As String = Nothing
'    Public Assessable_Amt As Double = 0
'    Public LoadIn_Qty As Double = 0
'    Public Uom As String = Nothing
'    Public Breakage As Double = 0
'    Public Basic_Price As Double = 0
'    Public Batch_No As String = Nothing
'    Public BasicPrice_WithTax As Double = 0
'    Public Empty_Value As Double = 0
'    Public TPT_Value As Double = 0
'    Public Burst As Double = 0
'    Public Leak As Double = 0
'    Public Shortage As Double = 0
'    Public Pending_Balance_In_Bottle As Double = 0
'    Public Total_Item_Cost As Double = 0
'    Public MRP_In_Bottle As Double = 0
'    Public Total_QtyInCase As Double = 0
'#End Region

'End Class

Public Class clsTransfer_LoadIn
    Public Shared Function PostData(ByVal strDocno As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocno) <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If

            Dim qry As String = "select * from TSPL_TRANSFER_HEAD where Transfer_Type='LI' AND Transfer_No='" + strDocno + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If

            If clsCommon.CompairString("Y", clsCommon.myCstr(dt.Rows(0)("Post"))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document")
            End If


            ''
            ''Add The Code For Posting
            ''

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class

Public Class clsTransfer_LoadOut
    Public Shared Function PostData(ByVal strDocno As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocno) <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If

            Dim qry As String = "select * from TSPL_BANK_TRANSFER where Transfer_Type='LO' AND Transfer_No='" + strDocno + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            If clsCommon.CompairString("Y", clsCommon.myCstr(dt.Rows(0)("Post"))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document")
            End If

            ''
            ''Add The Code For Posting
            ''

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
