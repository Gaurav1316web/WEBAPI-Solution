Imports common
Imports System.Data.SqlClient


Public Class clsAssetAgreementHead
#Region "Variables"
    Public docNo As String = Nothing
    Public docDate As String = Nothing
    Public empCode As String = Nothing
    Public empContactNo As String = Nothing
    Public locationCode As String = Nothing
    Public courierNo As String = Nothing
    Public courierComapnyName As String = Nothing
    Public courierDate As String = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String = Nothing
    Public remarks As String = Nothing
#End Region
    Public Shared Function saveData(ByVal obj As clsAssetAgreementHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim coll As New Hashtable()
            If obj.docDate = Nothing Or obj.docDate = "" Then
                Throw New Exception("Please select a valid Document Date")
            Else
                clsCommon.AddColumnsForChange(coll, "DOCUMENT_DATE", clsCommon.GetPrintDate(clsCommon.myCDate(obj.docDate), "dd/MMM/yyyy"))
            End If

            If obj.empCode = Nothing Or obj.empCode = "" Then
                Throw New Exception("Please Select a Valid Employee from finder")
            Else
                clsCommon.AddColumnsForChange(coll, "EMPLOYEE_CODE", obj.empCode)
            End If
            If obj.empContactNo = Nothing Or obj.empContactNo = "" Then
            Else
                clsCommon.AddColumnsForChange(coll, "EMP_CONTACT_NO", obj.empContactNo)
            End If
            If obj.locationCode = Nothing Or obj.locationCode = "" Then
                Throw New Exception("Please select a Valid Location it can't be blank")
            ElseIf clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where location_code='" & obj.locationCode & "'", trans) = 0 Then
                Throw New Exception("The location code You entered is Invalid.")
            Else
                clsCommon.AddColumnsForChange(coll, "LOC_CODE", obj.locationCode)
            End If
            If obj.courierNo = Nothing Or obj.courierNo = "" Then
                Throw New Exception("Please Enter Courier No.")
            Else
                clsCommon.AddColumnsForChange(coll, "COURIER_NO", obj.courierNo)
            End If
            If obj.courierComapnyName = Nothing Or obj.courierComapnyName = "" Then
                Throw New Exception("Please Enter the Name of courier Company")
            Else
                clsCommon.AddColumnsForChange(coll, "COURIER_COMP_NAME", obj.courierComapnyName)
            End If

            clsCommon.AddColumnsForChange(coll, "COURIER_DATE", clsCommon.GetPrintDate(clsCommon.myCDate(obj.courierDate), "dd/MMM/yyyy"))
            If clsCommon.myLen(obj.remarks) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.remarks)
            Else
                clsCommon.AddColumnsForChange(coll, "Remarks", "")
            End If

            If isNewEntry Then
                obj.docNo = clsERPFuncationality.GetNextCode(trans, obj.docDate, clsDocType.AssetAgreement, "", "")
                ''To be Uncomment
                'FrmAssetAgreement.txtDocNo.Value = obj.docNo
                'FrmAssetAgreement.txtDocNo.MyReadOnly = True
            ElseIf clsDBFuncationality.getSingleValue("select count(*) from tspl_asset_agreement_head where document_no='" & obj.docNo & "'", trans) = 0 Then
                Throw New Exception("Invalid Document No. for Updation")
                ''To be Uncomment
                'FrmAssetAgreement.txtDocNo.MyReadOnly = False
                'FrmAssetAgreement.txtDocNo.Focus()
            End If
            clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", obj.docNo)
            'If obj.docNo = Nothing AndAlso (Not isNewEntry) Then
            '    Throw New Exception("Document no can not be blank while update")
            '    FrmAssetAgreement.txtDocNo.MyReadOnly = False
            '    FrmAssetAgreement.txtDocNo.Focus()
            'ElseIf (Not isNewEntry) AndAlso clsDBFuncationality.getSingleValue("select count(*) from tspl_asset_agreement_head where docuement_no='" & obj.docNo & "'", trans) = 0 Then

            'ElseIf Not isNewEntry Then
            '    clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", obj.docNo)
            'ElseIf isNewEntry Then

            '    clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", obj.docNo)
            'Else
            '    
            'End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_AGREEMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_AGREEMENT_HEAD", OMInsertOrUpdate.Update, "DOCUMENT_NO='" & obj.docNo & "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal navType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetAgreementHead
        Dim obj As clsAssetAgreementHead = Nothing
        Try

            Dim qry As String = "select * from tspl_asset_agreement_head as a "
            Select Case navType
                Case NavigatorType.First
                    qry += " where  a.DOCUMENT_No = (select MIN(b.DOCUMENT_NO) from TSPL_ASSET_AGREEMENT_HEAD as b )"
                Case NavigatorType.Last
                    qry += " where  a.DOCUMENT_No = (select max(b.DOCUMENT_NO) from TSPL_ASSET_AGREEMENT_HEAD as b )"
                Case NavigatorType.Next
                    qry += " where  a.DOCUMENT_No = (select MIN(b.DOCUMENT_NO) from TSPL_ASSET_AGREEMENT_HEAD as b where b.DOCUMENT_NO>'" + strDocNo + "' )"
                Case NavigatorType.Previous
                    qry += " where  a.DOCUMENT_No = (select Max(b.DOCUMENT_NO) from TSPL_ASSET_AGREEMENT_HEAD as b where b.DOCUMENT_NO<'" + strDocNo + "' )"
                Case NavigatorType.Current
                    qry += " where a.DOCUMENT_NO='" + strDocNo + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsAssetAgreementHead()
                obj.docNo = clsCommon.myCstr(dt.Rows(0)("DOCUMENT_NO"))
                obj.docDate = clsCommon.myCstr(dt.Rows(0)("DOCUMENT_DATE"))
                obj.empCode = clsCommon.myCstr(dt.Rows(0)("EMPLOYEE_CODE"))
                obj.empContactNo = clsCommon.myCstr(dt.Rows(0)("EMP_CONTACT_NO"))
                obj.locationCode = clsCommon.myCstr(dt.Rows(0)("LOC_CODE"))
                obj.courierNo = clsCommon.myCstr(dt.Rows(0)("COURIER_NO"))
                obj.courierDate = clsCommon.myCstr(dt.Rows(0)("COURIER_DATE"))
                obj.courierComapnyName = clsCommon.myCstr(dt.Rows(0)("COURIER_COMP_NAME"))
                obj.remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal navType As NavigatorType) As clsAssetAgreementHead
        Return getData(strDocNo, navType, Nothing)
    End Function
    Public Shared Function delteData(ByVal strDocNo As String) As Boolean
        Return deleteData(strDocNo, Nothing)
    End Function
    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "delete from tspl_asset_agreement_head where document_no='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNewEntry(ByVal strDocNO As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select * from TSPL_ASSET_AGREEMENT_HEAD where DOCUMENT_NO ='" + strDocNO + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
Public Class clsAssetAgreementDetails
#Region "Variables"
    Public docNo As String = Nothing
    Public outletNo As String = Nothing
    Public agreementNo As String = Nothing
    Public agreementDate As String = Nothing
    Public receivedYN As String = Nothing
    Public receivedDate As String = Nothing
    Public AssetId As String = Nothing
    Public AgreementFrom As String = Nothing
    Public AgreementTo As String = Nothing

#End Region
    Public Shared Function saveData(ByVal strDocNo As String, ByVal arr As List(Of clsAssetAgreementDetails), Optional ByVal isNewEntry As Boolean = True, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Try
            If Not isNewEntry Then
                Dim qry1 As String = "delete from tspl_asset_agreement_details where document_no='" & strDocNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            End If
            If (arr IsNot Nothing AndAlso arr.Count > 0) Then
                For Each obj As clsAssetAgreementDetails In arr
                    Dim coll As New Hashtable()
                    If obj.outletNo = Nothing Or obj.outletNo = "" Then
                        Throw New Exception("Please select a valid Outlet NO")
                    ElseIf clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where cust_code='" & obj.outletNo & "'", trans) = 0 Then
                        Throw New Exception("Outlet No is Not Valid")
                    Else
                        clsCommon.AddColumnsForChange(coll, "OUTLET_NO", obj.outletNo)
                    End If

                    If obj.agreementNo = Nothing Or obj.agreementNo = "" Then
                        Throw New Exception("Please Enter Valid Agreement No")
                    Else
                        clsCommon.AddColumnsForChange(coll, "AGREEMENT_NO", obj.agreementNo)
                    End If
                    If obj.agreementDate = Nothing Or obj.agreementDate = "" Then
                        Throw New Exception("Invalid Agreement Date")
                    Else
                        clsCommon.AddColumnsForChange(coll, "AGREEMENT_DATE", clsCommon.GetPrintDate(clsCommon.myCDate(obj.agreementDate), "dd/MMM/yyyy"))
                    End If
                    If obj.AgreementFrom = Nothing Or obj.AgreementFrom = "" Then
                        Throw New Exception("Invalid Agreement From Date")
                    Else
                        clsCommon.AddColumnsForChange(coll, "AGREEMENT_FROM_DATE", clsCommon.GetPrintDate(clsCommon.myCDate(obj.AgreementFrom), "dd/MMM/yyyy"))
                    End If
                    If obj.AgreementTo = Nothing Or obj.AgreementTo = "" Then
                        Throw New Exception("Invalid Agreement Till Date")
                    Else
                        clsCommon.AddColumnsForChange(coll, "AGREEMENT_TO_DATE", clsCommon.GetPrintDate(clsCommon.myCDate(obj.AgreementTo), "dd/MMM/yyyy"))
                    End If
                    If obj.receivedYN = Nothing Or obj.receivedYN = "" Then
                    ElseIf obj.receivedYN = "NO" Or obj.receivedYN = "N" Then
                        clsCommon.AddColumnsForChange(coll, "RECEIVED_YN", "N")
                    Else
                        If obj.receivedDate = Nothing Or obj.receivedDate = "" Then
                            Throw New Exception("Please Select valid Received Date ")
                        Else
                            clsCommon.AddColumnsForChange(coll, "RECEIVED_YN", "Y")
                            clsCommon.AddColumnsForChange(coll, "RECEIVED_DATE", clsCommon.GetPrintDate(clsCommon.myCDate(obj.receivedDate), "dd/MMM/yyyy"))
                        End If

                    End If

                    If String.IsNullOrEmpty(obj.AssetId) Then
                        Throw New Exception("Asset ID can Not be Blank")
                    End If
                    If clsCommon.myLen(obj.AssetId) > 50 Then
                        Throw New Exception("Length of Asset Id Can not be more then 12")
                    End If
                    clsCommon.AddColumnsForChange(coll, "ASSET_ID", obj.AssetId)
                    clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", strDocNo)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_AGREEMENT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                    clsDBFuncationality.ExecuteNonQuery("update  tspl_rgp_detail set agreement_no=' " & obj.agreementNo & "'  where rgp_no in (select TSPL_RGP_HEAD.RGP_No   from TSPL_RGP_HEAD where  TSPL_RGP_HEAD.Vendor_Code='" & obj.outletNo & "')and serial_no='" & obj.AssetId & "'", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsAssetAgreementDetails)
        Dim arr As New List(Of clsAssetAgreementDetails)
        Dim obj As clsAssetAgreementDetails = Nothing
        Try
            Dim qry As String = "select * from tspl_asset_agreement_details where DOCUMENT_NO='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For j As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsAssetAgreementDetails()
                    obj.docNo = clsCommon.myCstr(dt.Rows(j)("DOCUMENT_NO"))
                    obj.outletNo = clsCommon.myCstr(dt.Rows(j)("OUTLET_NO"))
                    obj.agreementNo = clsCommon.myCstr(dt.Rows(j)("AGREEMENT_NO"))
                    obj.agreementDate = clsCommon.myCstr(dt.Rows(j)("AGREEMENT_DATE"))
                    obj.AgreementFrom = clsCommon.myCstr(dt.Rows(j)("AGREEMENT_FROM_DATE"))
                    obj.AgreementTo = clsCommon.myCstr(dt.Rows(j)("AGREEMENT_TO_DATE"))
                    obj.receivedYN = IIf(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(j)("RECEIVED_YN")), "Y") = CompairStringResult.Equal, "YES", "NO")
                    If clsCommon.CompairString(obj.receivedYN, "YES") = CompairStringResult.Equal Then
                        obj.receivedDate = clsCommon.myCDate(dt.Rows(j)("RECEIVED_DATE"))
                    End If
                    obj.AssetId = clsCommon.myCstr(dt.Rows(j)("ASSET_ID"))
                    'obj.receivedDate = IIf(clsCommon.CompairString(obj.receivedYN, "NO") = CompairStringResult.Equal, "", clsCommon.myCDate(dt.Rows(j)("RECEIVED_DATE")))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function
    Public Shared Function getData(ByVal strDocNo As String) As List(Of clsAssetAgreementDetails)
        Return getData(strDocNo, Nothing)
    End Function
    Public Shared Function delteData(ByVal strDocNo As String) As Boolean
        Return deleteData(strDocNo, Nothing)
    End Function
    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "delete from tspl_asset_agreement_details where document_no='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNewEntry(ByVal obj As clsAssetAgreementDetails, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select * from TSPL_ASSET_AGREEMENT_details where DOCUMENT_NO ='" + obj.docNo + "' and outlet_No='" + obj.outletNo + "' and agreement_No='" + obj.agreementNo + "' "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
