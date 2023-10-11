Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations
Imports System.Data.SqlClient
Public Class frmShareAllotment
    Private Sub frmShareAllotment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CrateTable()
            txtDate.Value = clsCommon.GETSERVERDATE()
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
        coll.Add("Remarks", "Varchar(200) not NULL")
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
            Dim qst As String = "select count(*) from TSPL_ACQUISITION_HEAD where TSPL_Share_Allotment='" + txtCode.Value + "'"
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

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

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
            Dim qry As String = "Select PK_ID AS [ID],Share_Code As [Code],Certificate_No As [Certificate No],Rate,Amount from TSPL_SHARE_MOVEMENT 
                                 Where RI='+1'
                                 Group By PK_ID,Share_Code,Certificate_No,Rate,Amount
                                 Having Count(PK_Id)<='" + txtNoOfShare.Text + "'"
            fndCertificate.arrValueMember = clsCommon.ShowMultipleSelectForm("@Certification", qry, "Certificate No", "Certificate No", fndCertificate.arrValueMember, fndCertificate.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtNoOfShare_TextChanged(sender As Object, e As EventArgs) Handles txtNoOfShare.TextChanged
        Try
            If clsCommon.myCdbl(txtNoOfShare.Value) > 0 AndAlso clsCommon.myCdbl(txtRate.Value) > 0 Then
                txtAmount.Value = clsCommon.myCdbl(txtRate.Value) * clsCommon.myCdbl(txtRate.Value)
            Else
                txtAmount.Value = 0
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

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

End Class