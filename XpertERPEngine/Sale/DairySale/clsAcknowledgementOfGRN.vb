''created by richa against ticket no VIJ/09/12/19-000110
Imports common
Imports System.Data.SqlClient
Public Class clsAcknowledgementOfGRN

#Region "Variable"
    Public ACKNOWLEDGEMENT_No As String = String.Empty
    Public ACKNOWLEDGEMENT_Date As DateTime?
    Public Customer_Code As String = String.Empty
    Public Location_Code As String = String.Empty
    Public Remarks As String = String.Empty
    Public Customer_Name As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Posted As Integer = 0
    Public arrAcknowledgementOfGRN As List(Of clsAcknowledgementOfGRN_Dtail) = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "Select TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_No As Code ,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_Date as Date from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD "
        Return clsCommon.ShowSelectForm("ACKNOWLEDGEMENT_OF_GRN", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function
    Public Shared Function SaveData(ByVal obj As clsAcknowledgementOfGRN, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsAcknowledgementOfGRN, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim ApplyTSPriceAtBulkSale As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, trans)) = 1, True, False))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmAcknowledgeMentOfGRN, obj.Location_Code, obj.ACKNOWLEDGEMENT_Date, trans)
            qry = "delete from TSPL_ACKNOWLEDGEMENT_OF_GRN_DETAIL where ACKNOWLEDGEMENT_No='" & obj.ACKNOWLEDGEMENT_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.ACKNOWLEDGEMENT_No = clsERPFuncationality.GetNextCode(trans, obj.ACKNOWLEDGEMENT_Date, clsDocType.AcknowledgementGRN, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "ACKNOWLEDGEMENT_Date", clsCommon.GetPrintDate(obj.ACKNOWLEDGEMENT_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "ACKNOWLEDGEMENT_Date", clsCommon.GetPrintDate(obj.ACKNOWLEDGEMENT_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
               
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "ACKNOWLEDGEMENT_No", obj.ACKNOWLEDGEMENT_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_No='" + obj.ACKNOWLEDGEMENT_No + "'", trans)
            End If
            clsAcknowledgementOfGRN_Dtail.saveData(obj.arrAcknowledgementOfGRN, obj.ACKNOWLEDGEMENT_No, obj.ACKNOWLEDGEMENT_Date, trans)
          
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAcknowledgementOfGRN
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal Trans As SqlTransaction) As clsAcknowledgementOfGRN
        Dim obj As clsAcknowledgementOfGRN = Nothing
        Dim qry As String = "select TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Posted,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_No,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.ACKNOWLEDGEMENT_Date,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Remarks,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Customer_Code,TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Location_Code,tspl_location_master.Location_desc,TSPL_Customer_Master.Customer_Name from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD left outer join tspl_location_master on tspl_location_master.Location_Code= TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Location_Code left outer join TSPL_Customer_Master on TSPL_Customer_Master.Cust_Code= TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.Customer_Code  where TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case NavType
            Case NavigatorType.Current
                qry += "  ACKNOWLEDGEMENT_No='" + strDocumentNo + "'"
            Case NavigatorType.Next
                qry += " ACKNOWLEDGEMENT_No in (select isnull(min(t.ACKNOWLEDGEMENT_No),'') from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD  as t where t.ACKNOWLEDGEMENT_No  >'" + strDocumentNo + "')"
            Case NavigatorType.First
                qry += " ACKNOWLEDGEMENT_No in (select isnull(min(t.ACKNOWLEDGEMENT_No),'') from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD  as t  )"
            Case NavigatorType.Last
                qry += " ACKNOWLEDGEMENT_No in (select isnull(max(t.ACKNOWLEDGEMENT_No),'') from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD  as t )"
            Case NavigatorType.Previous
                qry += " ACKNOWLEDGEMENT_No in (select isnull(max(t.ACKNOWLEDGEMENT_No),'') from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD  as t where  t.ACKNOWLEDGEMENT_No  <'" + strDocumentNo + "')"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsAcknowledgementOfGRN
            obj.ACKNOWLEDGEMENT_No = clsCommon.myCstr(dt.Rows(0)("ACKNOWLEDGEMENT_No"))
            obj.ACKNOWLEDGEMENT_Date = clsCommon.myCDate(dt.Rows(0)("ACKNOWLEDGEMENT_date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.arrAcknowledgementOfGRN = clsAcknowledgementOfGRN_Dtail.getData(obj.ACKNOWLEDGEMENT_No, Trans)
        End If
        Return obj

    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select ACKNOWLEDGEMENT_Date,Location_Code from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD where ACKNOWLEDGEMENT_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmAcknowledgeMentOfGRN, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("ACKNOWLEDGEMENT_Date")), trans)

            End If
            Dim qry As String = ""

            qry = "delete from TSPL_ACKNOWLEDGEMENT_OF_GRN_DETAIL where ACKNOWLEDGEMENT_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD where ACKNOWLEDGEMENT_No='" + strDocNo + "'"
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
                Throw New Exception("Acknowledgement No not found to Post")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select ACKNOWLEDGEMENT_Date,Location_Code from TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD where ACKNOWLEDGEMENT_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmAcknowledgeMentOfGRN, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("ACKNOWLEDGEMENT_Date")), trans)

            End If
            Dim qry = "Update TSPL_ACKNOWLEDGEMENT_OF_GRN_HEAD set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "'  " & _
            " where ACKNOWLEDGEMENT_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsAcknowledgementOfGRN_Dtail
    Public Document_No As String = String.Empty
    Public ACKNOWLEDGEMENT_No As String = String.Empty
    Public Sale_Invoice_No As String = String.Empty
    Public Sale_Invoice_Date As String = String.Empty
    Public Remarks As String = String.Empty
    Public SNo As Integer = 0
    Public TR_CODE As String = String.Empty
   
    Public Shared Function saveData(ByVal arrObj As List(Of clsAcknowledgementOfGRN_Dtail), ByVal strQCNo As String, ByVal strDocDate As Date, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsAcknowledgementOfGRN_Dtail In arrObj
                    coll = New Hashtable()
                    obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, strDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "ACKNOWLEDGEMENT_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Sale_Invoice_No", obj.Sale_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", clsCommon.GetPrintDate(obj.Sale_Invoice_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACKNOWLEDGEMENT_OF_GRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of clsAcknowledgementOfGRN_Dtail)
        Try
            Dim arrObj As List(Of clsAcknowledgementOfGRN_Dtail) = Nothing
            Dim obj As clsAcknowledgementOfGRN_Dtail = Nothing
            Dim qry As String = "Select * from TSPL_ACKNOWLEDGEMENT_OF_GRN_DETAIL where ACKNOWLEDGEMENT_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsAcknowledgementOfGRN_Dtail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsAcknowledgementOfGRN_Dtail()
                    obj.ACKNOWLEDGEMENT_No = clsCommon.myCstr(dt.Rows(i)("ACKNOWLEDGEMENT_No"))
                    obj.Sale_Invoice_No = clsCommon.myCstr(dt.Rows(i)("Sale_Invoice_No"))
                    obj.Sale_Invoice_Date = clsCommon.myCDate(dt.Rows(i)("Sale_Invoice_Date"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


