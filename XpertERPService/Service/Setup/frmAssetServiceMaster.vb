'' richa agarwal against ticket no. BM00000007792 as 04/sep/2015
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text.RegularExpressions
Imports common
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices.Marshal

Imports System.IO
Imports System.Configuration
Imports System

Imports System.Collections.Generic
Imports System.ComponentModel

Imports Excel = Microsoft.Office.Interop.Excel
Imports XpertERPEngine

Public Class FrmAssetServiceMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As New clsassetservicemaster()
#End Region

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        Visiblility()
    End Sub
    Sub LoadCombobox2()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("code") = ""
        dr("name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MAKE"
        dr("name") = "Make"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MODEL"
        dr("name") = "Model No."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "SIZE"
        dr("name") = "Size"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "TYPE"
        dr("name") = "type"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "OTHER"
        dr("name") = "Other"
        dt.Rows.Add(dr)

        comlevel2.DataSource = dt
        comlevel2.DisplayMember = "name"
        comlevel2.ValueMember = "code"

    End Sub
    Sub Loadcombobox3()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("code") = ""
        dr("name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MAKE"
        dr("name") = "Make"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MODEL"
        dr("name") = "Model No."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "SIZE"
        dr("name") = "Size"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "TYPE"
        dr("name") = "type"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "OTHER"
        dr("name") = "Other"
        dt.Rows.Add(dr)

        comlevel3.DataSource = dt
        comlevel3.DisplayMember = "name"
        comlevel3.ValueMember = "code"

    End Sub
    Sub LoadCombobox4()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("code") = ""
        dr("name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MAKE"
        dr("name") = "Make"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MODEL"
        dr("name") = "Model No."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "SIZE"
        dr("name") = "Size"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "TYPE"
        dr("name") = "type"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "OTHER"
        dr("name") = "Other"
        dt.Rows.Add(dr)


        comlevel4.DataSource = dt
        comlevel4.DisplayMember = "name"
        comlevel4.ValueMember = "code"

    End Sub
    Sub LoadCombobox5()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("code") = ""
        dr("name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MAKE"
        dr("name") = "Make"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MODEL"
        dr("name") = "Model No."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "SIZE"
        dr("name") = "Size"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "TYPE"
        dr("name") = "type"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "OTHER"
        dr("name") = "Other"
        dt.Rows.Add(dr)

        comlevel5.DataSource = dt
        comlevel5.DisplayMember = "name"
        comlevel5.ValueMember = "code"
    End Sub
    Sub LoadCombobox()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("code") = ""
        dr("name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MAKE"
        dr("name") = "Make"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "MODEL"
        dr("name") = "Model No."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "SIZE"
        dr("name") = "Size"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "TYPE"
        dr("name") = "type"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "OTHER"
        dr("name") = "Other"
        dt.Rows.Add(dr)

        comlevel1.DataSource = dt
        comlevel1.DisplayMember = "name"
        comlevel1.ValueMember = "code"

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAssetServiceMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub Reset()
        txtitemcode.Value = ""
        txtsno.Text = ""
        lblassetdesc.Text = ""
        txtcatcode.Text = ""
        lblCategorydesc.Text = ""
        txttagno.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        txtlev1.Text = ""
        txtlev2.Text = ""
        txtlev3.Text = ""
        txtlev4.Text = ""
        txtlev5.Text = ""
        txtdesc1.Text = ""
        txtdesc2.Text = ""
        txtdesc3.Text = ""
        txtdesc4.Text = ""
        txtdesc5.Text = ""
        comlevel1.Text = ""
        comlevel2.Text = ""
        comlevel3.Text = ""
        comlevel4.Text = ""
        comlevel5.Text = ""
    End Sub
    Public Sub Visiblility()
        lblcatstr.Visible = False
        lblCategorydesc.Visible = False
        txtcatcode.Visible = False
        lbllevel1.Visible = False
        lbllevel2.Visible = False
        lbllevel3.Visible = False
        lbllevel4.Visible = False
        lbllevel5.Visible = False
        txtlev1.Visible = False
        txtlev2.Visible = False
        txtlev3.Visible = False
        txtlev4.Visible = False
        txtlev5.Visible = False
        txtdesc1.Visible = False
        txtdesc2.Visible = False
        txtdesc3.Visible = False
        txtdesc4.Visible = False
        txtdesc5.Visible = False
        comlevel1.Visible = False
        comlevel2.Visible = False
        comlevel3.Visible = False
        comlevel4.Visible = False
        comlevel5.Visible = False
    End Sub
    Private Sub s_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
        Visiblility()
        LoadCombobox()
        LoadCombobox2()
        Loadcombobox3()
        LoadCombobox4()
        LoadCombobox5()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtassetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtitemcode._MYValidating
        Reset()
        Visiblility()
        Try
            If isButtonClicked Then
                Dim qry As String = "select distinct TSPL_SRN_DETAIL.Item_Code as [Asset Code],TSPL_ITEM_MASTER.item_desc as Description,TSPL_SERIAL_ITEM.Auto_Sr_No as [Asset Serial No],TSPL_ITEM_MASTER.structure_desc as [Structure Desc],TSPL_ITEM_MASTER.unit_code as Unit,TSPL_ITEM_MASTER.Rate,TSPL_ITEM_MASTER.SubItemType,TSPL_VISI_MASTER.tag_no as [Asset Tag No],TSPL_SRN_DETAIL.Item_Code+'#$'+TSPL_SERIAL_ITEM.Auto_Sr_No as ItemCode from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SRN_DETAIL.Item_Code right outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code=TSPL_SRN_DETAIL.Item_Code left outer join TSPL_VISI_MASTER on TSPL_VISI_MASTER.asset_no=TSPL_SRN_DETAIL.item_code and TSPL_SERIAL_ITEM.Auto_Sr_No=TSPL_VISI_MASTER.Visi_Id"
                Dim whrcls As String = " TSPL_SRN_HEAD.Status='1' and TSPL_SRN_HEAD.Item_Type='A'"
                'txtassetcode.Value = clsCommon.ShowSelectForm("ASTIDFND", qry, "Item_Code", whrcls, txtassetcode.Value, "", isButtonClicked)
                'txtsno.Text = clsCommon.ShowSelectForm("SNOIDFND", qry, "Auto_Sr_No", "", txtsno.Text, "", isButtonClicked)
                'Dim strsplit As String = clsCommon.ShowSelectForm("ASTIDFND", qry, "itemcode", whrcls, strsplit, "", isButtonClicked)
                Dim strsplit As String = clsCommon.ShowSelectForm("ASTIDFND", qry, "itemcode", whrcls, "", "", isButtonClicked)
                Dim xsplit() As String
                xsplit = strsplit.Split("#$")

                txtitemcode.Value = xsplit(0).Replace("#", "").Replace("$", "")
                txtsno.Text = xsplit(1).Replace("#", "").Replace("$", "")

                lblassetdesc.Text = clsDBFuncationality.getSingleValue("Select item_desc from TSPL_ITEM_MASTER where item_code='" + txtitemcode.Value + "'")
            Else
                lblassetdesc.Text = clsDBFuncationality.getSingleValue("Select item_desc from TSPL_ITEM_MASTER where item_code='" + txtitemcode.Value + "'")
            End If

            Dim structcode As String = clsDBFuncationality.getSingleValue("Select Item_Category_Struct_Code from TSPL_ITEM_MASTER where item_code='" + txtitemcode.Value + "'")

            Dim obj As clsassetservicemaster = clsassetservicemaster.GetData(txtitemcode.Value, structcode)

            lbllevel1.Text = obj.lbl1
            lbllevel2.Text = obj.lbl2
            lbllevel3.Text = obj.lbl3
            lbllevel4.Text = obj.lbl4
            lbllevel5.Text = obj.lbl5

            txtlev1.Text = obj.lev1code
            txtlev2.Text = obj.lev2code
            txtlev3.Text = obj.lev3code
            txtlev4.Text = obj.lev4code
            txtlev5.Text = obj.lev5code

            txtdesc1.Text = obj.lev1desc
            txtdesc2.Text = obj.lev2desc
            txtdesc3.Text = obj.lev3desc
            txtdesc4.Text = obj.lev4desc
            txtdesc5.Text = obj.lev5desc

            GetComboBoxValue()
            Visiblility()
            Isvisible()

            '********check for data*******************************************
            Dim qry1 As String = "select count(*) from TSPL_VISI_MASTER where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
            If check > 0 Then
                qry1 = "select tag_no from TSPL_VISI_MASTER where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
                txttagno.Text = clsDBFuncationality.getSingleValue(qry1)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub GetComboBoxValue()
        Try

            If lbllevel1.Text.ToUpper().Contains("MAKE") Then
                comlevel1.SelectedValue = "MAKE"
            ElseIf lbllevel1.Text.ToUpper().Contains("MODEL") Then
                comlevel1.SelectedValue = "MODEL"
            ElseIf lbllevel1.Text.ToUpper().Contains("SIZE") Then
                comlevel1.SelectedValue = "SIZE"
            ElseIf lbllevel1.Text.ToUpper().Contains("TYPE") Then
                comlevel1.SelectedValue = "TYPE"
            ElseIf lbllevel1.Text.ToUpper().Contains("OTHER") Then
                comlevel1.SelectedValue = "OTHER"
            End If

            If lbllevel2.Text.ToUpper().Contains("MAKE") Then
                comlevel2.SelectedValue = "MAKE"
            ElseIf lbllevel2.Text.ToUpper().Contains("MODEL") Then
                comlevel2.SelectedValue = "MODEL"
            ElseIf lbllevel2.Text.ToUpper().Contains("SIZE") Then
                comlevel2.SelectedValue = "SIZE"
            ElseIf lbllevel2.Text.ToUpper().Contains("TYPE") Then
                comlevel2.SelectedValue = "TYPE"
            ElseIf lbllevel2.Text.ToUpper().Contains("OTHER") Then
                comlevel2.SelectedValue = "OTHER"
            End If

            If lbllevel3.Text.ToUpper().Contains("MAKE") Then
                comlevel3.SelectedValue = "MAKE"
            ElseIf lbllevel3.Text.ToUpper().Contains("MODEL") Then
                comlevel3.SelectedValue = "MODEL"
            ElseIf lbllevel3.Text.ToUpper().Contains("SIZE") Then
                comlevel3.SelectedValue = "SIZE"
            ElseIf lbllevel3.Text.ToUpper().Contains("TYPE") Then
                comlevel3.SelectedValue = "TYPE"
            ElseIf lbllevel3.Text.ToUpper().Contains("OTHER") Then
                comlevel3.SelectedValue = "OTHER"
            End If

            If lbllevel4.Text.ToUpper().Contains("MAKE") Then
                comlevel4.SelectedValue = "MAKE"
            ElseIf lbllevel4.Text.ToUpper().Contains("MODEL") Then
                comlevel4.SelectedValue = "MODEL"
            ElseIf lbllevel4.Text.ToUpper().Contains("SIZE") Then
                comlevel4.SelectedValue = "SIZE"
            ElseIf lbllevel4.Text.ToUpper().Contains("TYPE") Then
                comlevel4.SelectedValue = "TYPE"
            ElseIf lbllevel4.Text.ToUpper().Contains("OTHER") Then
                comlevel4.SelectedValue = "OTHER"
            End If

            If lbllevel5.Text.ToUpper().Contains("MAKE") Then
                comlevel5.SelectedValue = "MAKE"
            ElseIf lbllevel5.Text.ToUpper().Contains("MODEL") Then
                comlevel5.SelectedValue = "MODEL"
            ElseIf lbllevel5.Text.ToUpper().Contains("SIZE") Then
                comlevel5.SelectedValue = "SIZE"
            ElseIf lbllevel5.Text.ToUpper().Contains("TYPE") Then
                comlevel5.SelectedValue = "TYPE"
            ElseIf lbllevel5.Text.ToUpper().Contains("OTHER") Then
                comlevel5.SelectedValue = "OTHER"
            End If
            '--------------combobox1

            Dim check As Integer = 0
            If clsCommon.myLen(txtdesc1.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visimake='" + txtdesc1.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel1.SelectedValue = "MAKE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visi_size='" + txtdesc1.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel1.SelectedValue = "SIZE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and model_no='" + txtdesc1.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel1.SelectedValue = "MODEL"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_type='" + txtdesc1.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel1.SelectedValue = "TYPE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_other='" + txtdesc1.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel1.SelectedValue = "OTHER"
                End If
            End If
            '----------------------------------------------------------------------
            '---------------combobox2--------------------------------------
            If clsCommon.myLen(txtdesc2.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visimake='" + txtdesc2.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel2.SelectedValue = "MAKE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visi_size='" + txtdesc2.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel2.SelectedValue = "SIZE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and model_no='" + txtdesc2.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel2.SelectedValue = "MODEL"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_type='" + txtdesc2.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel2.SelectedValue = "TYPE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_other='" + txtdesc2.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel2.SelectedValue = "OTHER"
                End If
            End If
            '--------------------combobox3----------------------------------
            If clsCommon.myLen(txtdesc3.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visimake='" + txtdesc3.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel3.SelectedValue = "MAKE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visi_size='" + txtdesc3.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel3.SelectedValue = "SIZE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and model_no='" + txtdesc3.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel3.SelectedValue = "MODEL"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_type='" + txtdesc3.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel3.SelectedValue = "TYPE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_other='" + txtdesc3.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel3.SelectedValue = "OTHER"
                End If
            End If
            '---------------------------combobox4---------------------------------------
            If clsCommon.myLen(txtdesc4.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visimake='" + txtdesc4.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel4.SelectedValue = "MAKE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visi_size='" + txtdesc4.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel4.SelectedValue = "SIZE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and model_no='" + txtdesc4.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel4.SelectedValue = "MODEL"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_type='" + txtdesc4.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel4.SelectedValue = "TYPE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_other='" + txtdesc4.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel4.SelectedValue = "OTHER"
                End If
            End If
            '--------------------------combobox5------------------------------
            If clsCommon.myLen(txtdesc5.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visimake='" + txtdesc5.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel5.SelectedValue = "MAKE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and visi_size='" + txtdesc5.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel5.SelectedValue = "SIZE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and model_no='" + txtdesc5.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel5.SelectedValue = "MODEL"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_type='" + txtdesc5.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel5.SelectedValue = "TYPE"
                End If

                qry = "select count(*) from tspl_visi_master where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and asset_other='" + txtdesc5.Text + "'"
                check = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    comlevel5.SelectedValue = "OTHER"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub Isvisible()
        If clsCommon.myLen(txtlev1.Text) > 0 Then
            txtlev1.Visible = True
            txtdesc1.Visible = True
            lbllevel1.Visible = True
            comlevel1.Visible = True
        End If

        If clsCommon.myLen(txtlev2.Text) > 0 Then
            txtlev2.Visible = True
            txtdesc2.Visible = True
            lbllevel2.Visible = True
            comlevel2.Visible = True
        End If

        If clsCommon.myLen(txtlev3.Text) > 0 Then
            txtlev3.Visible = True
            txtdesc3.Visible = True
            lbllevel3.Visible = True
            comlevel3.Visible = True
        End If

        If clsCommon.myLen(txtlev4.Text) > 0 Then
            txtlev4.Visible = True
            txtdesc4.Visible = True
            lbllevel4.Visible = True
            comlevel4.Visible = True
        End If

        If clsCommon.myLen(txtlev5.Text) > 0 Then
            txtlev5.Visible = True
            txtdesc5.Visible = True
            lbllevel5.Visible = True
            comlevel5.Visible = True
        End If
    End Sub
    Public Function AllowToSave() As Boolean
        If (clsCommon.myLen(txtitemcode.Value) = 0) Then
            clsCommon.MyMessageBoxShow("Please Select Asset Code", Me.Text)
            txtitemcode.Focus()
            txtitemcode.Select()
            Return False
        End If
        If (clsCommon.myLen(txttagno.Text) = 0) Then
            clsCommon.MyMessageBoxShow("Please Insert Tag No. For Asset", Me.Text)
            txttagno.Focus()
            txttagno.Select()
            Return False
        End If

        '-----------------------------------------------------------------
        If txtlev1.Visible = True AndAlso comlevel1.SelectedValue = "" Then
            clsCommon.MyMessageBoxShow("Please Select Category Types Of " + lbllevel1.Text + "", Me.Text)
            Return False
        End If

        If txtlev2.Visible = True AndAlso comlevel2.SelectedValue = "" Then
            clsCommon.MyMessageBoxShow("Please Select Category Types Of " + lbllevel2.Text + "", Me.Text)
            Return False
        End If

        If txtlev3.Visible = True AndAlso comlevel3.SelectedValue = "" Then
            clsCommon.MyMessageBoxShow("Please Select Category Types Of " + lbllevel3.Text + "", Me.Text)
            Return False
        End If

        If txtlev4.Visible = True AndAlso comlevel4.SelectedValue = "" Then
            clsCommon.MyMessageBoxShow("Please Select Category Types Of " + lbllevel4.Text + "", Me.Text)
            Return False
        End If

        If txtlev5.Visible = True AndAlso comlevel5.SelectedValue = "" Then
            clsCommon.MyMessageBoxShow("Please Select Category Types Of " + lbllevel5.Text + "", Me.Text)
            Return False
        End If

        If comlevel1.SelectedValue <> "" AndAlso (comlevel1.SelectedValue = comlevel2.SelectedValue Or comlevel1.SelectedValue = comlevel3.SelectedValue Or comlevel1.SelectedValue = comlevel4.SelectedValue Or comlevel1.SelectedValue = comlevel5.SelectedValue) Then
            clsCommon.MyMessageBoxShow("No Two Types Having Same Value", Me.Text)
            comlevel1.SelectedValue = ""
            Return False
        End If

        If comlevel2.SelectedValue <> "" AndAlso (comlevel1.SelectedValue = comlevel2.SelectedValue Or comlevel2.SelectedValue = comlevel3.SelectedValue Or comlevel2.SelectedValue = comlevel4.SelectedValue Or comlevel2.SelectedValue = comlevel5.SelectedValue) Then
            clsCommon.MyMessageBoxShow("No Two Types Having Same Value", Me.Text)
            comlevel2.SelectedValue = ""
            Return False
        End If

        If comlevel3.SelectedValue <> "" AndAlso (comlevel3.SelectedValue = comlevel2.SelectedValue Or comlevel1.SelectedValue = comlevel3.SelectedValue Or comlevel3.SelectedValue = comlevel4.SelectedValue Or comlevel3.SelectedValue = comlevel5.SelectedValue) Then
            clsCommon.MyMessageBoxShow("No Two Types Having Same Value", Me.Text)
            comlevel3.SelectedValue = ""
            Return False
        End If

        If comlevel4.SelectedValue <> "" AndAlso (comlevel4.SelectedValue = comlevel2.SelectedValue Or comlevel4.SelectedValue = comlevel3.SelectedValue Or comlevel1.SelectedValue = comlevel4.SelectedValue Or comlevel4.SelectedValue = comlevel5.SelectedValue) Then
            clsCommon.MyMessageBoxShow("No Two Types Having Same Value", Me.Text)
            comlevel4.SelectedValue = ""
            Return False
        End If

        If comlevel5.SelectedValue <> "" AndAlso (comlevel5.SelectedValue = comlevel2.SelectedValue Or comlevel5.SelectedValue = comlevel3.SelectedValue Or comlevel5.SelectedValue = comlevel4.SelectedValue Or comlevel1.SelectedValue = comlevel5.SelectedValue) Then
            clsCommon.MyMessageBoxShow("No Two Types Having Same Value", Me.Text)
            comlevel5.SelectedValue = ""
            Return False
        End If
        '-----------------------------------------------------------------------------
        Return True
    End Function
    Public Sub SaveData()
        obj = New clsassetservicemaster()
        obj.lev1code = txtlev1.Text
        obj.lev2code = txtlev2.Text
        obj.lev3code = txtlev3.Text
        obj.lev4code = txtlev4.Text
        obj.lev5code = txtlev5.Text
        obj.lev1desc = txtdesc1.Text
        obj.lev2desc = txtdesc2.Text
        obj.lev3desc = txtdesc3.Text
        obj.lev4desc = txtdesc4.Text
        obj.lev5desc = txtdesc5.Text
        obj.serialno = txtsno.Text
        obj.tagno = txttagno.Text
        obj.asstcode = txtitemcode.Value

        obj.comlevel1 = comlevel1.SelectedValue
        obj.comlevel2 = comlevel2.SelectedValue
        obj.comlevel3 = comlevel3.SelectedValue
        obj.comlevel4 = comlevel4.SelectedValue
        obj.comlevel5 = comlevel5.SelectedValue

        If btnsave.Text <> "Save" Then
            Dim qry As String = "select count(*) from TSPL_VISI_MASTER where visi_id='" + obj.serialno + "' and asset_no='" + obj.asstcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                Reset()
                Visiblility()
                Return
            End If
        End If

        If AllowToSave() Then
            Dim isSaved As Boolean = clsassetservicemaster.SaveData(obj, IIf(btnsave.Text = "Save", True, False), Nothing)
            If isSaved Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                common.clsCommon.MyMessageBoxShow("Data Could Not Saved", Me.Text)
            End If
        End If
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim qry As String = "select count(*) from TSPL_VISI_MASTER where visi_id='" + txtsno.Text + "' and asset_no='" + txtitemcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check = 0 Then
            clsCommon.MyMessageBoxShow("No Data Found For Deletion", Me.Text)
            Reset()
            Visiblility()
            Return
        End If

        Try
            If Not (common.clsCommon.MyMessageBoxShow("Delete the Asset Service Master of Asset Code " + txtitemcode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            clsassetservicemaster.DeleteDate(txtsno.Text, txtitemcode.Value)
            clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
            Reset()
            Visiblility()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub rdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdexit.Click
        Me.Close()
    End Sub

    Private Sub rdexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdexport.Click
        Dim str As String
        str = "select tspl_visi_master.visi_id as 'Visi Id' ,tspl_visi_master.Asset_No as [Asset Code],TSPL_ITEM_MASTER.item_desc as [Asset Desc],tspl_visi_master.serial_no as [Asset Serial No],tspl_visi_master.tag_no as [Asset Tag No] from tspl_visi_master left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=tspl_visi_master.asset_no "
        transportSql.ExporttoExcel(str, " and tspl_visi_master.comp_code='" + objCommonVar.CurrentCompanyCode + "' and tspl_visi_master.tag_no<> ''", Me)
    End Sub

    Private Sub rdimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Visi Id", "Asset Code", "Asset Desc", "Asset Serial No", "Asset Tag No") Then
            ' Dim trans As SqlTransaction
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    'Dim obj As New clsassetservicemaster()


                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("VISI Code can not be blank or incorrect.")
                    End If
                    'obj.serialno = strCode

                    Dim strcode1 As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strcode1.Length > 30 Or (String.IsNullOrEmpty(strcode1)) Then
                        Throw New Exception("Asset Code can not be blank or incorrect.")
                    End If
                    'obj.asstcode = strcode1

                    Dim strDec1 As String = clsCommon.myCstr(grow.Cells(2).Value)
                    'obj.asstdesc = strDec1

                    Dim strcode2 As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If strcode2.Length > 30 Or (String.IsNullOrEmpty(strcode2)) Then
                        Throw New Exception("Asset Serial No can not be blank or incorrect")
                    End If
                    'obj.serialno = strcode2

                    Dim strDec2 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    'obj.tagno = strDec2

                    Dim structcode As String = clsDBFuncationality.getSingleValue("Select Item_Category_Struct_Code from TSPL_ITEM_MASTER where item_code='" + strcode1 + "'")
                    ' Dim obj As New clsassetservicemaster()
                    Dim obj As clsassetservicemaster = clsassetservicemaster.GetData(strcode1, structcode)

                    lbllevel1.Text = obj.lbl1
                    lbllevel2.Text = obj.lbl2
                    lbllevel3.Text = obj.lbl3
                    lbllevel4.Text = obj.lbl4
                    lbllevel5.Text = obj.lbl5

                    txtdesc1.Text = obj.lev1desc
                    txtdesc2.Text = obj.lev2desc
                    txtdesc3.Text = obj.lev3desc
                    txtdesc4.Text = obj.lev4desc
                    txtdesc5.Text = obj.lev5desc

                    txtlev1.Text = obj.lev1code
                    txtlev2.Text = obj.lev2code
                    txtlev3.Text = obj.lev3code
                    txtlev4.Text = obj.lev4code
                    txtlev5.Text = obj.lev5code

                    txtsno.Text = strcode2
                    txtitemcode.Value = strcode1
                    GetComboBoxValue()

                    obj.comlevel1 = comlevel1.SelectedValue
                    obj.comlevel2 = comlevel2.SelectedValue
                    obj.comlevel3 = comlevel3.SelectedValue
                    obj.comlevel4 = comlevel4.SelectedValue
                    obj.comlevel5 = comlevel5.SelectedValue


                    txtdesc1.Text = ""
                    txtdesc2.Text = ""
                    txtdesc3.Text = ""
                    txtdesc4.Text = ""
                    txtdesc5.Text = ""

                    txtlev1.Text = ""
                    txtlev2.Text = ""
                    txtlev3.Text = ""
                    txtlev4.Text = ""
                    txtlev5.Text = ""

                    lbllevel1.Text = ""
                    lbllevel2.Text = ""
                    lbllevel3.Text = ""
                    lbllevel4.Text = ""
                    lbllevel5.Text = ""

                    txtsno.Text = ""
                    txtitemcode.Value = ""

                    obj.serialno = strCode
                    obj.asstcode = strcode1
                    obj.asstdesc = strDec1
                    obj.serialno = strcode2
                    obj.tagno = strDec2
                    Visiblility()

                    clsassetservicemaster.SaveData(obj, True, Nothing)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub comlevel1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comlevel1.TextChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub comlevel2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comlevel2.TextChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub comlevel3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comlevel3.TextChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub comlevel4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comlevel4.TextChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub comlevel5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comlevel5.TextChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub
End Class
