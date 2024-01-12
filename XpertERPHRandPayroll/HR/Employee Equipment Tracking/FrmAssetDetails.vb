'created by Pulkit @BM00000001155
'created on 15/11/2013 
'table created: TSPL_Asset_Details
'class used ClsAssetDetails
Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmAssetDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public Code As String
    Private Sub FrmAssetDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  SetUserMgmtNew()
        LoadData(Code, NavigatorType.Current)

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        dtpDOP.Value = System.DateTime.Now.ToString()
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
    End Sub
    'Public Sub SetUserMgmtNew()
    '    ' Try
    '    ''MyBase.SetUserMgmt(clsUserMgtCode.LeaveMain)
    '    '    If Not (MyBase.isReadFlag) Then
    '    '        common.clsCommon.MyMessageBoxShow("Permission Denied")
    '    '        Me.Close()
    '    '        Exit Function
    '    '    End If
    '    '    btnSave.Visible = MyBase.isModifyFlag
    '    '    btnDelete.Visible = MyBase.isDeleteFlag
    '    'Catch ex As Exception
    '    '    clsCommon.MyMessageBoxShow(ex.Message)
    '    'End Try
    'End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As ClsAssetDetails = ClsAssetDetails.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtAssetCode.Value = obj.Asset_Code
                txtAssetDesc.Text = obj.Asset_Description
                txtAssetSpecs.Text = obj.Asset_Specification
                txtAssetType.Value = obj.Asset_Type_Code
                txtAssetSubCategory.Value = obj.Asset_Sub_Category
                txtCompany.Text = obj.Company
                txtSerialNo.Text = obj.Serial_No
                dtpDOP.Value = obj.Date_Of_Purchase
                txtAssetCode.MyReadOnly = True
                UcAttachment1.LoadData(obj.Asset_Code)
            Else
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsAssetDetails()
                obj.Asset_Code = txtAssetCode.Value
                obj.Asset_Description = txtAssetDesc.Text
                obj.Asset_Specification = txtAssetSpecs.Text
                obj.Asset_Type_Code = txtAssetType.Value
                obj.Asset_Sub_Category = txtAssetSubCategory.Value
                obj.Company = txtCompany.Text
                obj.Serial_No = txtSerialNo.Text
                obj.Date_Of_Purchase = dtpDOP.Value

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Asset_Code) from TSPL_Asset_Details where Asset_Code='" + obj.Asset_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsAssetDetails.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Asset_Code)
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Asset_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtAssetDesc.Text)) <= 0 Then
                txtAssetDesc.Focus()
                txtAssetDesc.MendatroryField = True
                Throw New Exception("Please Fill Asset Description")
            ElseIf clsCommon.myLen(clsCommon.myCstr(txtAssetSpecs.Text)) <= 0 Then
                txtAssetSpecs.Focus()
                txtAssetSpecs.MendatroryField = True
                Throw New Exception("Please Fill Asset Specification")
            ElseIf clsCommon.myLen(clsCommon.myCstr(txtAssetType.Value)) <= 0 Then
                txtAssetType.Focus()
                txtAssetType.MendatroryField = True
                Throw New Exception("Please Fill Asset Type")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub txtAssetCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAssetCode._MYNavigator
        LoadData(txtAssetCode.Value, NavType)
    End Sub
    Private Sub txtAssetCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAssetCode._MYValidating
        If txtAssetCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Asset_Code as AssetCode,Asset_Description as AssetDescription,Asset_Specification as AssetSpecification,Asset_Type_Code as AssetType,Serial_No as SerialNo,Date_Of_Purchase as DateOfPurchase from  TSPL_Asset_Details"
            Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            txtAssetCode.Value = clsCommon.ShowSelectForm("ASSET_Dtl", qry, "AssetCode", whrClas, txtAssetCode.Value, "", isButtonClicked)
            LoadData(txtAssetCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub txtAssetType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAssetType._MYValidating
        Try
            If txtAssetType.MyReadOnly OrElse isButtonClicked Then
                Dim qry As String = "select Asset_Type_Code as AssetType,Asset_Type_Description as AssetTypeDescription From TSPL_Asset_Type_Master"
                Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
                txtAssetType.Value = clsCommon.ShowSelectForm("ASSETMASTER", qry, "AssetType", whrClas, txtAssetType.Value, "", isButtonClicked)
                If txtAssetType.Value <> "" Then
                    AssetTypeLoadData(txtAssetType.Value, NavigatorType.Current)
                Else
                    Reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function AssetTypeLoadData(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        Try
            Dim obj As ClsAssetType = ClsAssetType.GetData(strCode, NavType)
            If obj IsNot Nothing Then
                txtAssetType.Value = obj.Asset_Type_Code
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtAssetCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow("Do you want to delete Code '" + txtAssetCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from TSPL_Asset_Details where Asset_Code='" + txtAssetCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), " Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current  Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        Try
            txtAssetCode.Value = ""
            txtAssetDesc.Text = ""
            txtAssetSpecs.Text = ""
            txtAssetType.Value = ""
            txtSerialNo.Text = ""
            dtpDOP.Value = System.DateTime.Now.ToString()
            UcAttachment1.BlankAllControls()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmAssetDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub txtAssetSubCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAssetSubCategory._MYValidating
        Try
            If clsCommon.myLen(txtAssetType.Value) <= 0 Then
                Throw New Exception("Please Select Asset Category First")
            End If
            Dim qry As String = "select Code, Description, Category from TSPL_Asset_SubCategory_Master "
            txtAssetSubCategory.Value = clsCommon.ShowSelectForm("TSPL_Asset_SubCategory_Master", qry, "Code", " CompCode='" + objCommonVar.CurrentCompanyCode + "' and Category='" + txtAssetType.Value + "' ", txtAssetSubCategory.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Added By Anand
    'Date-07/March/2014
    Private Sub btnAddAttachment_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddAttachment()
    End Sub
    Private Sub AddAttachment()
        Try
            If clsCommon.myLen(txtAssetCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please first save the Asset Detail and then add attachments", Me.Text)
                Exit Sub
            End If
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'OpenFileDialog1.FileName = filePath
                Dim filePath As String = OpenFileDialog1.FileName.ToString()
                Dim filename As String = Path.GetFileName(filePath)
                Dim ext As String = Path.GetExtension(filename)
                Dim contenttype As String = String.Empty
                Dim strDocNo As String = filename
                Dim strAppPath As String = Application.StartupPath
                Dim strDestination As String = strAppPath & "\Tecxpert\" & strDocNo
                If System.IO.File.Exists(strDestination) Then
                    System.IO.File.Delete(strDestination)
                End If
                My.Computer.FileSystem.CopyFile(filePath, strDestination)
                OpenFileDialog1.FileName = strDestination
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim fs As System.IO.Stream = OpenFileDialog1.OpenFile()
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(fs.Length)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "FILENAME", filename)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_Details", OMInsertOrUpdate.Update, "Asset_Code = '" + txtAssetCode.Value + "'", trans)

                    clsDBFuncationality.UpdateImage("ATTACHMENT", bytes, "TSPL_Asset_Details", "Asset_Code = '" + txtAssetCode.Value + "'", trans)
                    br.Close() ' done by stuti reagrding memory leakage
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "File Successfully Uploaded", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        ' Return Nothing
    End Sub
    'Added by Anand
    'Date-07/March/2014
    Private Sub btnViewAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtAssetCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Asset Code Value  not found to view attachments", Me.Text)
            Exit Sub
        End If
        Dim frm As New FrmAssetDetailAttachments()
        frm.strCode = txtAssetCode.Value
        frm.Show()
    End Sub
End Class
