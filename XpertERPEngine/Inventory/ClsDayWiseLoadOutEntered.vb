Imports System
Imports System.Data.SqlClient
Imports common

Public Class ClsDayWiseLoadOutEntered
#Region "veriables"
    Public LoadOutDate As String
    Public NoOfLoadOutMake As Double = 0
    Public Balance As Double = 0
    Public location As String
    Public type As String
    Public remarks As String
    Public NoOfLoadOutmade As Double = 0
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String
    Public NoofTotalCount As Double = 0
    Public NoOfToBePosted As Double = 0

#End Region

    Public Function SaveData(ByVal obj As ClsDayWiseLoadOutEntered, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, " NoOf_LoadOuttobeMake ", obj.NoOfLoadOutMake)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.location)
            clsCommon.AddColumnsForChange(coll, "Type", obj.type)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.remarks)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "LoadOutDate", clsCommon.GetPrintDate(obj.LoadOutDate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DayWiseEnteredLoadOut ", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DayWiseEnteredLoadOut ", OMInsertOrUpdate.Update, "Convert(Date,LoadOutDate,103)=Convert(Date,'" + obj.LoadOutDate + "',103) and Location_Code ='" & obj.location & "' and type='" & obj.type & "'", trans)

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
    Public Shared Function GetData(ByVal LoadOutDate As String, ByVal location As String, ByVal type As String) As ClsDayWiseLoadOutEntered
        Dim obj As ClsDayWiseLoadOutEntered = Nothing
        Dim qry As String = "SELECT LoadOutDate ,NoOf_LoadOuttobeMake,location_code,TYPE,remarks  from TSPL_DayWiseEnteredLoadOut  where  2=2"
        qry += " and  TSPL_DayWiseEnteredLoadOut .LoadOutDate = '" + clsCommon.GetPrintDate(clsCommon.myCDate(LoadOutDate), "dd/MMM/yyyy") + "' and location_code='" & location & "' and type='" & type & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDayWiseLoadOutEntered()
            obj.LoadOutDate = clsCommon.GetPrintDate(dt.Rows(0)("LoadOutDate"), "dd/MMM/yyyy")
            obj.NoOfLoadOutMake = clsCommon.myCdbl(dt.Rows(0)("NoOf_LoadOuttobeMake"))
            obj.location = dt.Rows(0)("location_code")
            obj.type = dt.Rows(0)("Type")
            obj.remarks = dt.Rows(0)("remarks")
            obj.NoOfLoadOutmade = getloadoutmade(obj.type, obj.location, obj.LoadOutDate)
            obj.NoofTotalCount = GetTotalCount(obj.type, obj.location, obj.LoadOutDate)
            obj.Balance = clsCommon.myCdbl(obj.NoOfLoadOutMake) - clsCommon.myCdbl(obj.NoofTotalCount)
            obj.NoOfToBePosted = clsCommon.myCdbl(obj.NoofTotalCount - obj.NoOfLoadOutmade)

        End If

        Return obj
    End Function

    Public Shared Function getloadoutmade(ByVal drptype As String, ByVal location As String, ByVal Loadoutdate As String) As Double
        If drptype = "Load Out" Then
            Dim NoOfLoadOUt As Double
            Dim qry As String = "select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where   To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and Convert(Date,Transfer_Date,103) =Convert(date,'" & Loadoutdate & "',103)and TSPL_TRANSFER_HEAD .From_Location ='" & location & "' and Transfer_Type ='LO' and Post ='Y'"
            NoOfLoadOUt = clsDBFuncationality.getSingleValue(qry)
            Return NoOfLoadOUt

        ElseIf drptype = "Load IN" Then
            Dim NoOfLoadIn As Double
            Dim LoadInqry As String = "select Count(Transfer_No) from TSPL_TRANSFER_HEAD where To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Physical')and Convert(Date,Transfer_Date,103) =Convert(date,'" & Loadoutdate & "',103)and TSPL_TRANSFER_HEAD .To_Location ='" & location & "' and Transfer_Type ='LI' and Post ='Y' "
            NoOfLoadIn = clsDBFuncationality.getSingleValue(LoadInqry)
            Return NoOfLoadIn

        ElseIf drptype = "Settlement" Then
            Dim NoOfsettlement As Double
            Dim settlementqry As String = "select COUNT(Payment_No ) from TSPL_PAYMENT_HEADER where Convert(Date,Payment_Date,103) =Convert(date,'" & Loadoutdate & "',103) and Payment_Code ='SETTLEMENT'and Location_Code ='" & location & "'and Posted ='P'"
            NoOfsettlement = clsDBFuncationality.getSingleValue(settlementqry)
            Return NoOfsettlement
        ElseIf drptype = "Empty Settlement" Then
            Dim NoOfemptySettlement As Double
            Dim NoOfEmptysettlment As String = "select COUNT(Adjustment_No ) from TSPL_ADJUSTMENT_HEADER where Convert(Date,Adjustment_Date,103) =Convert(date,'" & Loadoutdate & "',103) and Loc_Code ='" & location & "' and Reference_Document ='Load Out/Transfer' and ItemType ='E'and  Trans_Type ='In'and Posted ='Y'"
            NoOfemptySettlement = clsDBFuncationality.getSingleValue(NoOfEmptysettlment)
            Return NoOfemptySettlement

        ElseIf drptype = "Sale Invoice" Then
            Dim NoOfsaleInvoice As Double
            Dim SaleInvoice As String = "select Count(Sale_Invoice_No)   from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date =Convert(date,'" & Loadoutdate & "',103) and Location ='" & location & "'and  is_post='Y' "
            NoOfsaleInvoice = clsDBFuncationality.getSingleValue(SaleInvoice)
            Return NoOfsaleInvoice
        End If
        Return 0
    End Function

    Public Shared Function GetTotalCount(ByVal drptype As String, ByVal location As String, ByVal Loadoutdate As String) As Double
        If drptype = "Load Out" Then
            Dim NoOfTotalCount As Double
            Dim NoofTotalCountqry As String = "select Count(Transfer_No)   from TSPL_TRANSFER_HEAD where   To_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and Convert(Date,Transfer_Date,103) =Convert(date,'" & Loadoutdate & "',103)and TSPL_TRANSFER_HEAD .From_Location ='" & location & "' and Transfer_Type ='LO'"
            NoOfTotalCount = clsDBFuncationality.getSingleValue(NoofTotalCountqry)
            Return NoOfTotalCount

        ElseIf drptype = "Load IN" Then
            Dim NoOftotalCount As Double
            Dim LoadInTotoal As String = "select Count(Transfer_No)  from TSPL_TRANSFER_HEAD where From_Location in (select Location_Code  from TSPL_LOCATION_MASTER where Location_Type ='Logical')and Convert(Date,Transfer_Date,103) =Convert(date,'" & Loadoutdate & "',103)and TSPL_TRANSFER_HEAD .To_Location ='" & location & "' and Transfer_Type ='LI' "
            NoOftotalCount = clsDBFuncationality.getSingleValue(LoadInTotoal)
            Return NoOftotalCount

        ElseIf drptype = "Settlement" Then
            Dim NoOfTotalSetlement As Double
            Dim settlementqryTotal As String = "select COUNT(Payment_No ) from TSPL_PAYMENT_HEADER where Convert(Date,Payment_Date,103) =Convert(date,'" & Loadoutdate & "',103) and Payment_Code ='SETTLEMENT'and Location_Code ='" & location & "'"
            NoOfTotalSetlement = clsDBFuncationality.getSingleValue(settlementqryTotal)
            Return NoOfTotalSetlement
        ElseIf drptype = "Empty Settlement" Then
            Dim NoOfEmptySettlmentotal As Double
            Dim NoOfEmptysettlmentTotal As String = "select COUNT(Adjustment_No ) from TSPL_ADJUSTMENT_HEADER where Convert(Date,Adjustment_Date,103) =Convert(date,'" & Loadoutdate & "',103) and Loc_Code ='" & location & "' and Reference_Document ='Load Out/Transfer' and ItemType ='E'and  Trans_Type ='In'"
            NoOfEmptySettlmentotal = clsDBFuncationality.getSingleValue(NoOfEmptysettlmentTotal)
            Return NoOfEmptySettlmentotal
        ElseIf drptype = "Sale Invoice" Then
            Dim NoOftotalcount As Double
            Dim SaleInvoiceTotal As String = "select Count(Sale_Invoice_No)   from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date =Convert(date,'" & Loadoutdate & "',103) and Location ='" & location & "'   "
            NoOftotalcount = clsDBFuncationality.getSingleValue(SaleInvoiceTotal)
            Return NoOftotalcount
        End If
        Return 0
    End Function
End Class
