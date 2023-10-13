Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations
Imports System.Data.SqlClient
Public Class frmShareAllotment

#Region "Variables"
    Dim isNewEntry As Boolean = False
#End Region
    Private Sub frmShareAllotment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CrateTable()
            txtDate.Value = clsCommon.GETSERVERDATE()
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CrateTable()
        Dim coll = New Dictionary(Of String, String)()
        coll.Add("Code", "VARCHAR(30) NOT NULL PRIMARY KEY ")
        coll.Add("Name", "Varchar(50) NOT NULL ")
        coll.Add("IDate", "Datetime NOT NULL")
        coll.Add("Range_From", "int not Null")
        coll.Add("Range_To", "int not Null")
        coll.Add("Qty", "int not Null")
        coll.Add("Rate", "Decimal (18,2) Null")
        coll.Add("Amount", "Decimal (18,2) Null")
        coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Post_By", "varchar(12) NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Post_Date", "Datetime NULL")
        coll.Add("Status", "integer Null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_SHARE_MASTER", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION")
        coll.Add("Source_Code", "VARCHAR(30) NOT NULL")
        coll.Add("Source_Date", "Datetime NOT NULL")
        coll.Add("Source_Type", "Varchar(5) NOT NULL") ''SH-MA    ,SH-AL
        coll.Add("Share_Code", "varchar(30) NOT NULL REFERENCES TSPL_SHARE_MASTER(Code)")
        coll.Add("Certificate_No", "Varchar(10) NOT NULL")
        coll.Add("RI", "int not Null") ''SH-MA +1  ,SH-AL -1
        coll.Add("Rate", "Decimal (18,2) Null")
        coll.Add("Amount", "Decimal (18,2) Null")
        coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Status", "integer Null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_SHARE_MOVEMENT", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "VARCHAR(30) NOT NULL PRIMARY KEY ")
        coll.Add("IDate", "Datetime NOT NULL")
        coll.Add("Remarks", "Varchar(200) NULL")
        coll.Add("Share_Code", "varchar(30) NOT NULL REFERENCES TSPL_SHARE_MASTER(Code)")
        coll.Add("DCS_Code", "varchar(12) NOT NULL REFERENCES TSPL_VENDOR_MASTER(Vendor_Code)")
        coll.Add("Name", "Varchar(50) NOT NULL ")
        coll.Add("Qty", "int not Null")
        coll.Add("Rate", "Decimal (18,2) Null")
        coll.Add("Amount", "Decimal (18,2) Null")
        coll.Add("Status", "integer Null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_SHARE_ALLOTMENT", coll)
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "Select Code, IDate As Date,Share_Code As [Share Code], Dcs_Code As [DCS Code],Name,Status, Remarks from TSPL_Share_Allotment"
            LoadData(clsCommon.ShowSelectForm("@ShareAllot", qry, "Code", Nothing, txtCode.Value, Nothing, isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_Share_Allotment where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            AddNew()
            txtCode.MyReadOnly = True
            Dim obj As New clsShareAllotment()
            obj = clsShareAllotment.GetData(strCode, NavType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtCode.Value = obj.Code
                txtDate.Value = obj.IDate
                fndDCSCode.Value = obj.DCS_Code
                fndShare.Value = obj.Share_Code
                Dim arrCertificate As New ArrayList
                For i As Integer = 0 To obj.Certificate_No.Count - 1
                    arrCertificate.Add(obj.Certificate_No(i))
                Next
                fndCertificate.arrValueMember = arrCertificate
                lblName.Text = obj.Name
                txtNoOfShare.Text = obj.Qty
                txtRate.Text = obj.Rate
                txtAmount.Text = obj.Amount
                txtRemarks.Text = obj.Remarks
                If clsCommon.myCdbl(ERPTransactionStatus.Approved) = clsCommon.myCdbl(obj.Status) Then
                    UsLock1.Status = obj.Status
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                ElseIf ERPTransactionStatus.Pending = obj.Status Then
                    UsLock1.Status = obj.Status
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
            Else
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDCSCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDCSCode._MYValidating
        Try
            Dim qry As String = " Select TSPL_VENDOR_MASTER.Vendor_Code As [DCS Code], TSPL_VENDOR_MASTER.Vendor_Name As [DCS Name],TSPL_VENDOR_MASTER.RegistrationNo As [DCS Registration],(TSPL_VENDOR_MASTER.Add1+','+TSPL_VENDOR_MASTER.Add2+','+TSPL_VENDOR_MASTER.Add3) As [Address], TSPL_VENDOR_MASTER.City_Code As [City],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [DCS Uploader Code] from TSPL_VENDOR_MASTER
                                  Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code "
            fndDCSCode.Value = clsCommon.ShowSelectForm("ShareDCS", qry, "DCS Code", "", fndDCSCode.Value, "", isButtonClicked)
            If clsCommon.myLen(clsCommon.myCstr(fndDCSCode.Value)) > 0 Then
                qry += " Where TSPL_VENDOR_MASTER.Vendor_Code='" + clsCommon.myCstr(fndDCSCode.Value) + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    lblRegistration.Text = clsCommon.myCstr(dt.Rows(0)("DCS Registration"))
                    lblName.Text = clsCommon.myCstr(dt.Rows(0)("DCS Name"))
                    lblUploaderCode.Text = clsCommon.myCstr(dt.Rows(0)("DCS Uploader Code"))
                End If
            Else
                lblRegistration.Text = Nothing
                lblName.Text = Nothing
                lblUploaderCode.Text = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndShare__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndShare._MYValidating
        Try
            Dim qry As String = "Select Code,Name,IDate As [Date],Range_From As [Range From],Range_To As [Range To],Qty,Rate,Amount,Status from TSPL_SHARE_MASTER"
            fndShare.Value = clsCommon.ShowSelectForm("@Share", qry, "Code", "", fndShare.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCertificate__My_Click(sender As Object, e As EventArgs) Handles fndCertificate._My_Click
        Try
            'Dim qry As String = "select Certificate_No As [Certificate No],max(case when Source_Type='SH-MA' then Rate else 0 end) as Rate    from (
            '                     Select PK_ID,Share_Code,Certificate_No,Rate,Amount,RI,Source_Type from TSPL_SHARE_MOVEMENT 
            '                     Where Share_Code='" + fndShare.Value + "')xxx Group By Share_Code,Certificate_No
            '                     Having sum(RI)>0"
            If clsCommon.myLen(txtNoOfShare.Value) > 0 Then
                Dim qry As String = clsShareAllotment.ReturnQry(fndShare.Value, clsCommon.myCstr(txtNoOfShare.Value))
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                fndCertificate.arrValueMember = clsCommon.ShowMultipleSelectForm("@Certification", qry, "Certificate No", "Certificate No", fndCertificate.arrValueMember, fndCertificate.arrDispalyMember)
                If fndCertificate.arrValueMember.Count > 0 Then
                    Dim rtQry As String = "Select Rate From TSPL_SHARE_MOVEMENT where Certificate_No IN (" & clsCommon.GetMulcallStringWithComma(fndCertificate.arrValueMember) & ") "
                    txtRate.Value = clsDBFuncationality.getSingleValue(rtQry)
                End If
            Else
                clsCommon.MyMessageBoxShow("Fill No Of Share", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim obj As New clsShareAllotment()
            obj.Code = txtCode.Value
            obj.IDate = txtDate.Value
            obj.DCS_Code = fndDCSCode.Value
            obj.Share_Code = fndShare.Value
            obj.Name = lblName.Text
            obj.Qty = txtNoOfShare.Text
            obj.Rate = txtRate.Text
            obj.Amount = txtAmount.Text
            obj.Remarks = txtRemarks.Text
            If fndCertificate.arrValueMember IsNot Nothing Then
                For i As Integer = 0 To fndCertificate.arrValueMember.Count - 1
                    obj.Certificate_No.Add(fndCertificate.arrValueMember(i))
                Next
            End If
            If clsCommon.myLen(obj.Code) > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
            End If
            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow("Data save successfully.", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndDCSCode.Value = Nothing
        fndShare.Value = Nothing
        lblRegistration.Text = Nothing
        lblName.Text = Nothing
        lblUploaderCode.Text = Nothing
        txtNoOfShare.Text = 0
        txtRate.Text = 0
        txtAmount.Text = 0
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = True
        txtRemarks.Text = Nothing
        fndCertificate.arrValueMember = Nothing
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Try
                Dim Reason As String = ""
                If (myMessages.deleteConfirm()) Then
                    If (clsShareAllotment.DeleteData(txtCode.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ", Me.Text)
                        AddNew()
                    End If
                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        Try
            If clsCommon.myCdbl(txtNoOfShare.Value) > 0 AndAlso clsCommon.myCdbl(txtRate.Value) > 0 Then
                txtAmount.Value = clsCommon.myCdbl(txtNoOfShare.Value) * clsCommon.myCdbl(txtRate.Value)
            Else
                txtAmount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                PostData(txtCode.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsShareAllotment.PostData(clsCommon.myCstr(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtNoOfShare_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNoOfShare.Validating
        Try
            If clsCommon.myCdbl(txtNoOfShare.Value) > 0 Then
                Dim Qry As String = clsShareAllotment.ReturnQry(fndShare.Value, clsCommon.myCstr(txtNoOfShare.Value))
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    Dim arrCertificate As New ArrayList
                    For i As Integer = 0 To dt.Rows.Count - 1
                        arrCertificate.Add(dt.Rows(i)("Certificate No"))
                    Next
                    If clsCommon.myCdbl(arrCertificate.Count) < (txtNoOfShare.Value) Then
                        clsCommon.MyMessageBoxShow("Availabe Only " + clsCommon.myCstr(arrCertificate.Count) + " Share", Me.Text)
                        txtNoOfShare.Value = clsCommon.myCdbl(arrCertificate.Count)
                    End If
                    fndCertificate.arrValueMember = arrCertificate
                    If fndCertificate.arrValueMember.Count > 0 Then
                            Dim rtQry As String = "Select Rate From TSPL_SHARE_MOVEMENT where Certificate_No IN (" & clsCommon.GetMulcallStringWithComma(fndCertificate.arrValueMember) & ") "
                            txtRate.Value = clsDBFuncationality.getSingleValue(rtQry)
                        End If
                    Else
                        clsCommon.MyMessageBoxShow("Share not available", Me.Text)
                End If
            End If


            If clsCommon.myCdbl(txtNoOfShare.Value) > 0 AndAlso clsCommon.myCdbl(txtRate.Value) > 0 Then
                txtAmount.Value = clsCommon.myCdbl(txtNoOfShare.Value) * clsCommon.myCdbl(txtRate.Value)
            Else
                txtAmount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class