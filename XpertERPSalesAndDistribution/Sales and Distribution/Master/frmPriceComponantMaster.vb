'---Created By --[Pankaj Kumar Chaudhary]--against Ticket No--[BM00000002206]-Save, Update, Delete, Import, Export, SHortcut key, custom Controls
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine

Public Class FrmPriceComponantMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim qry As String
    Dim dt As DataTable
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PriceComponentMasters)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            Importmenu.Enabled = True
            Exportmenu.Enabled = True
        Else
            Importmenu.Enabled = False
            Exportmenu.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmPriceComponantMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData(txtComponentCode.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Public Sub SetLength()
        txtComponentCode.MyMaxLength = 12
        txtDesc.MaxLength = 100
    End Sub

    Private Sub FrmPriceComponantMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetLength()
        LoadSerialNo()
        btnDelete.Enabled = False
        gbGLAccount.Enabled = False
        txtComponentCode.MyReadOnly = False
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
    End Sub

    Private Sub LoadSerialNo()
        Dim dt1 As New DataTable
        dt1.Columns.Add("Code", GetType(Integer))
        dt1.Columns.Add("Desc", GetType(String))
        For ii As Integer = 0 To 10
            If ii = 0 Then
                dt1.Rows.Add(ii, "Select")
            Else
                dt1.Rows.Add(ii, ii.ToString())
            End If
        Next
        ddlSerialNumber.DataSource = dt1
        ddlSerialNumber.ValueMember = "Code"
        ddlSerialNumber.DisplayMember = "Code"
    End Sub

    Private Sub Reset()
        txtComponentCode.Value = ""
        txtDesc.Text = ""
        chkGLAccountApplicable.Checked = False
        txtGLAccountcc.Value = ""
        lblglaccdescription.Text = ""
        chktpt.Checked = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtComponentCode.MyReadOnly = False
        ddlSerialNumber.SelectedIndex = -1
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.PriceComponentMasters, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsPriceComponent)
                Dim obj As New clsPriceComponent()
                obj.Price_Comp_code = clsCommon.myCstr(txtComponentCode.Value)
                obj.Price_Comp_Desc = clsCommon.myCstr(txtDesc.Text)
                obj.GL_Account_App = IIf(chkGLAccountApplicable.Checked, "T", "F")
                obj.Price_Comp_Account_Code = clsCommon.myCstr(txtGLAccountcc.Value)
                obj.TPT_Type = IIf(chktpt.Checked, "Y", "N")
                obj.Serial_Number = clsCommon.myCdbl(ddlSerialNumber.SelectedValue)
                Arr.Add(obj)
                If (clsPriceComponent.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Price_Comp_code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtComponentCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Component Code not be left blank.")
                txtComponentCode.Focus()
                Return False
            ElseIf clsCommon.myLen(txtDesc.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Description can not be left blank.")
                txtDesc.Focus()
                Return False
            ElseIf chkGLAccountApplicable.Checked Then
                If clsCommon.myLen(txtGLAccountcc.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Select GL Account.")
                    txtGLAccountcc.Focus()
                    Return False
                End If
            ElseIf clsCommon.myCdbl(ddlSerialNumber.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow("Select serial no.")
                ddlSerialNumber.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(txtComponentCode.Value)
    End Sub

    Private Sub DeleteData(ByVal strPriceCompCode As String)
        Try
            If clsCommon.myLen(strPriceCompCode) > 0 Then
                If clsPriceComponent.DeleteData(strPriceCompCode) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow("No Customer found to delete.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    '---------------Enable/Disables GL Account finder on checking -GL Account Applicable----------------
    Private Sub chkGLAccountApplicable_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGLAccountApplicable.ToggleStateChanged
        gbGLAccount.Enabled = chkGLAccountApplicable.Checked
    End Sub

    '-----Opens finder for GL Account--------------
    Private Sub fndGLAcc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGLAccountcc._MYValidating
        Dim qry As String = "select Account_Code as [Account Code], Description as [Description] from TSPL_GL_ACCOUNTS "
        txtGLAccountcc.Value = clsCommon.ShowSelectForm("PriCompntMastrGL", qry, "Account Code", "", txtGLAccountcc.Value, "", isButtonClicked)
        lblAccountDesc.Text = (New BAL.BALPriceComponant).FillGLAccDesc(txtGLAccountcc.Value)
    End Sub

    '------------------Opens finder for Price Component-----------------
    Private Sub fndPc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtComponentCode._MYValidating
        qry = "select count(*) from TSPL_PRICE_COMPONENT_MASTER where Price_Comp_code ='" + txtComponentCode.Value + "' "
        Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If Count = 0 Then
            txtComponentCode.MyReadOnly = False
        Else
            txtComponentCode.MyReadOnly = True
        End If

        If txtComponentCode.MyReadOnly OrElse isButtonClicked Then
            'qry = "select Price_Comp_code as Code, Price_Comp_Desc as Description, Case When GL_Account_App='T' Then 'Yes' Else 'No' End as [GL A/C Applicable], Case When TPT_Type='Y' Then 'Yes' Else 'No' End as [TPT Type] from TSPL_PRICE_COMPONENT_MASTER"
            'txtComponentCode.Value = clsCommon.ShowSelectForm("PRCMapngFND", qry, "Code", "", txtComponentCode.Value, "", isButtonClicked)
            txtComponentCode.Value = clsPriceComponent.getFinder("", txtComponentCode.Value, isButtonClicked)
            LoadData(txtComponentCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtComponentCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtComponentCode._MYNavigator
        Try
            qry = "select count(*) from TSPL_PRICE_COMPONENT_MASTER where Price_Comp_code='" + txtComponentCode.Value + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                txtComponentCode.MyReadOnly = False
            Else
                txtComponentCode.MyReadOnly = True
            End If
            LoadData(txtComponentCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strPriceCompCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            Dim obj As New clsPriceComponent
            obj = clsPriceComponent.GetData(strPriceCompCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Price_Comp_code) > 0 Then
                txtComponentCode.Value = obj.Price_Comp_code
                txtDesc.Text = obj.Price_Comp_Desc
                ddlSerialNumber.SelectedValue = obj.Serial_Number
                chkGLAccountApplicable.Checked = IIf(clsCommon.CompairString(obj.GL_Account_App, "T") = CompairStringResult.Equal, True, False)
                If clsCommon.CompairString(obj.GL_Account_App, "T") = CompairStringResult.Equal Then
                    txtGLAccountcc.Value = obj.Price_Comp_Account_Code
                    lblAccountDesc.Text = obj.GL_Account_Desc
                End If
                chktpt.Checked = IIf(clsCommon.CompairString(obj.TPT_Type, "Y") = CompairStringResult.Equal, True, False)
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            Else
                btnSave.Text = "Save"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub exitmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitmenu.Click
        Me.Close()
    End Sub

    Private Sub Importmenu_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Importmenu.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Price Component Code", "Price Component Description", "Serial Number", "Price Component Account Code", "Gl Account App", "TPT Type") Then
            Try
                Dim LineNo As String
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsPriceComponent)
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(dgrv.Index + 2)
                    Dim obj As New clsPriceComponent()
                    obj.Price_Comp_code = clsCommon.myCstr(dgrv.Cells("Price Component Code").Value)
                    If clsCommon.myLen(obj.Price_Comp_code) > 0 Then
                        If clsCommon.myLen(obj.Price_Comp_code) > 12 Then
                            Throw New Exception("Line " + LineNo + " : Length of Price Component Code Should be less than 12.")
                        End If

                        obj.Price_Comp_Desc = clsCommon.myCstr(dgrv.Cells("Price Component Description").Value)
                        If clsCommon.myLen(obj.Price_Comp_Desc) > 100 Then
                            Throw New Exception("Line " + LineNo + " : Length of Price Component Description Should be less than 100.")
                        End If

                        obj.Serial_Number = clsCommon.myCdbl(dgrv.Cells("Serial Number").Value)
                        If obj.Serial_Number <= 0 Or obj.Serial_Number > 10 Then
                            Throw New Exception("Line " + LineNo + " : Enter Serial Number between 1 to 10.")
                        End If

                        obj.GL_Account_App = clsCommon.myCstr(dgrv.Cells("Gl Account App").Value)
                        If Not (clsCommon.CompairString(obj.GL_Account_App, "T") = CompairStringResult.Equal Or clsCommon.CompairString(obj.GL_Account_App, "F") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Enter 'Gl Account App' as 'T' OR 'F'.")
                        End If

                        If clsCommon.CompairString(obj.GL_Account_App, "T") = CompairStringResult.Equal Then
                            obj.Price_Comp_Account_Code = clsCommon.myCstr(dgrv.Cells("Price Component Account Code").Value)
                            obj.Price_Comp_Account_Code = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE Account_Code='" + obj.Price_Comp_Account_Code + "'")
                            If clsCommon.myLen(obj.Price_Comp_Account_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Enter a valid 'Price Component Account Code'.")
                            End If
                        Else
                            obj.Price_Comp_Account_Code = ""
                        End If

                        obj.TPT_Type = clsCommon.myCstr(dgrv.Cells("TPT Type").Value)
                        If Not (clsCommon.CompairString(obj.TPT_Type, "Y") = CompairStringResult.Equal Or clsCommon.CompairString(obj.TPT_Type, "N") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Enter 'TPT Type' as 'Y' OR 'N'.")
                        End If
                        Arr.Add(obj)
                    End If
                Next
                If (clsPriceComponent.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub Exportmenu_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Exportmenu.Click
        Dim query As String = "Select price_comp_code as 'Price Component Code',Price_comp_desc as 'Price Component Description',Serial_Number as 'Serial Number',Price_Comp_account_code as 'Price Component Account Code',Gl_account_App as 'Gl Account App' ,tpt_type as 'TPT Type'   from tspl_price_component_master"
        transportSql.ExporttoExcel(query, Me)
    End Sub

End Class
