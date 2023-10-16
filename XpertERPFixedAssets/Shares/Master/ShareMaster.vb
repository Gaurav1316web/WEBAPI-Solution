Imports XpertERPEngine
Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Public Class ShareMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isNewEntry As Boolean = True
#End Region

    Private Sub ShareMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim coll = New Dictionary(Of String, String)()
        'coll.Add("Code", "VARCHAR(30) NOT NULL PRIMARY KEY ")
        'coll.Add("Name", "Varchar(50) NOT NULL ")
        'coll.Add("IDate", "Datetime NOT NULL")
        'coll.Add("Range_From", "int not Null")
        'coll.Add("Range_To", "int not Null")
        'coll.Add("Qty", "int not Null")
        'coll.Add("Rate", "Decimal (18,2) Null")
        'coll.Add("Amount", "Decimal (18,2) Null")
        'coll.Add("Status", "integer null")
        'coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        'coll.Add("Created_Date", "Datetime NOT NULL")
        'coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        'coll.Add("Modified_Date", "Datetime NOT NULL")
        'coll.Add("Post_By", "varchar(12) NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        'coll.Add("Post_Date", "Datetime NULL")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_SHARE_MASTER", coll)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION")
        'coll.Add("Source_Code", "VARCHAR(30) NOT NULL")
        'coll.Add("Source_Date", "Datetime NOT NULL")
        'coll.Add("Source_Type", "Varchar(5) NOT NULL") ''SH-MA    ,SH-AL
        'coll.Add("Share_Code", "varchar(30) NOT NULL REFERENCES TSPL_SHARE_MASTER(Code)")
        'coll.Add("Certificate_No", "Varchar(10) NOT NULL")
        'coll.Add("RI", "int not Null") ''SH-MA +1  ,SH-AL -1
        'coll.Add("Status", "integer null")
        'coll.Add("Rate", "Decimal (18,2) Null")
        'coll.Add("Amount", "Decimal (18,2) Null")
        'coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        'coll.Add("Created_Date", "Datetime NOT NULL")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_SHARE_MOVEMENT", coll)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("Code", "VARCHAR(30) NOT NULL PRIMARY KEY ")
        'coll.Add("IDate", "Datetime NOT NULL")
        'coll.Add("Remarks", "Varchar(200) not NULL")
        'coll.Add("Share_Code", "varchar(30) NOT NULL REFERENCES TSPL_SHARE_MASTER(Code)")
        'coll.Add("DCS_Code", "varchar(12) NOT NULL REFERENCES TSPL_VENDOR_MASTER(Vendor_Code)")
        'coll.Add("Name", "Varchar(50) NOT NULL ")
        'coll.Add("Qty", "int not Null")
        'coll.Add("Rate", "Decimal (18,2) Null")
        'coll.Add("Amount", "Decimal (18,2) Null")
        'coll.Add("Status", "integer null")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_SHARE_ALLOTMENT", coll)

        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Value = clsCommon.GETSERVERDATE()
        Addnew()

    End Sub

    Public Sub Addnew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCode.Focus()
        txtCerificateFrom.Text = ""
        TxtCertificateTo.Text = ""
        TxtShare.Text = ""
        TxtRate.Text = ""
        TxtAmount.Text = ""
        txtName.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = True
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_SHARE_MASTER where Code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Name,IDate from TSPL_SHARE_MASTER "
            txtCode.Value = clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "TSPL_SHARE_MASTER.Code ", isButtonClicked, "")
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_SHARE_MASTER where Code='" + txtCode.Value + "'"

            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Addnew()
            txtCode.MyReadOnly = True
            Dim obj As New ClsShareMaster()
            obj = ClsShareMaster.GetData(strCode, NavType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDate.Value = obj.IDate
                txtName.Text = obj.Name
                txtCerificateFrom.Text = obj.CertificateFrom
                TxtCertificateTo.Text = obj.CertificateTo
                TxtShare.Text = obj.Shares
                TxtRate.Text = obj.Rate
                TxtAmount.Text = obj.Amount

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
                Addnew()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            'If (AllowToSave()) Then
            Dim obj As New ClsShareMaster()
            obj.Code = txtCode.Value
            obj.Name = txtName.Text
            obj.IDate = txtDate.Value
            obj.CertificateFrom = txtCerificateFrom.Text
            obj.CertificateTo = TxtCertificateTo.Text
            obj.Shares = TxtShare.Text
            obj.Rate = TxtRate.Text
            obj.Amount = TxtAmount.Text
            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.")
                LoadData(obj.Code, NavigatorType.Current)
            End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtShare_TextChanged(sender As Object, e As EventArgs) Handles TxtShare.TextChanged
        CalculateCT()
        CalculateAmount()
    End Sub

    Private Sub txtCerificateFrom_TextChanged(sender As Object, e As EventArgs) Handles txtCerificateFrom.TextChanged
        CalculateCT()
    End Sub

    Private Sub TxtRate_TextChanged(sender As Object, e As EventArgs) Handles TxtRate.TextChanged
        CalculateAmount()
    End Sub

    Private Sub CalculateCT()
        ' Check if CFTextBox and SHTextBox have valid numeric values
        Dim cfValue As Double
        Dim shValue As Double

        If Double.TryParse(txtCerificateFrom.Text, cfValue) AndAlso Double.TryParse(TxtShare.Text, shValue) Then
            ' Perform the calculation and update CTTextBox
            Dim ctValue As Double = (cfValue + shValue) - 1
            TxtCertificateTo.Text = ctValue.ToString()
        Else
            ' Handle invalid input (e.g., non-numeric input)
            TxtCertificateTo.Text = 0
        End If
    End Sub

    Private Sub CalculateAmount()
        Dim rate As Double
        Dim share As Double

        If Double.TryParse(TxtRate.Text, rate) AndAlso Double.TryParse(TxtShare.Text, share) Then
            ' Perform the calculation and update CTTextBox
            Dim ctValue As Double = (rate * share)
            TxtAmount.Text = ctValue.ToString()
        Else
            ' Handle invalid input (e.g., non-numeric input)
            TxtAmount.Text = 0
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (ClsShareMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            PostData(txtCode.Value)
        Else
        End If
    End Sub


    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                ClsShareMaster.PostData(clsCommon.myCstr(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class