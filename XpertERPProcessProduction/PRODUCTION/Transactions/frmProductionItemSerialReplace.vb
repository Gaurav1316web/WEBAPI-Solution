'==========BM00000003734=====created by Monika-===================01/09/2014
Imports common
Imports System.Data.SqlClient

Public Class frmProductionItemSerialReplace
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanges As Boolean = False

    '============main========================
    Const colLineNo As String = "Sno"
    Const colIcode As String = "ItemCode"
    Const colIname As String = "Iname"
    Const colBOMCode As String = "BOMCode"
    Const colUnit As String = "UnitCode"
    Const colSerialNo As String = "SerialNo"
    Const colRemarks As String = "Remarks"

    '=============princi-======================
    Const colPrinciLineNo As String = "Sno"
    Const colPrinciMainIcode As String = "MainIcode"
    Const colPrinciMainIname As String = "MainIname"
    Const colPrinciMainSerialno As String = "MainSerialNo"
    Const colPrinciIssueCode As String = "IssueCode"
    Const colPrinciBOMCode As String = "BOMCode"
    Const colPrinciIcode As String = "ItemCode"
    Const colPrinciIname As String = "Iname"
    Const colPrinciUnit As String = "UnitCode"
    Const colPrinciSerialNo As String = "SerialNo"
    Const colPrinciNewSerialNo As String = "NewSerialNo"
    Const colPrinciRemarks As String = "Remarks"

    '=================raw========================
    Const colRawLineNo As String = "Sno"
    Const colRawMainIcode As String = "MainIcode"
    Const colRawMainIname As String = "MainIname"
    Const colRawMainSerialno As String = "MainSerialNo"
    Const colRawIssueCode As String = "IssueCode"
    Const colRawBOMCode As String = "BOMCode"
    Const colRawIcode As String = "ItemCode"
    Const colRawIname As String = "Iname"
    Const colRawUnit As String = "UnitCode"
    Const colRawSerialNo As String = "SerialNo"
    Const colRawNewSerialNo As String = "NewSerialNo"
    Const colRawRemarks As String = "Remarks"

#End Region

    Private Sub FunReset()
        isNewEntry = True
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""

        gv.Rows.Clear()
        gv.Rows.AddNew()

        gv_princi.Rows.Clear()
        gv_princi.Rows.AddNew()

        gv_other.Rows.Clear()
        gv_other.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        txtCode.MyReadOnly = False
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionItemSerialReplace)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Enabled = MyBase.isModifyFlag
        btndelete.Enabled = MyBase.isDeleteFlag
        btnpost.Enabled = MyBase.isPostFlag
    End Sub

    Private Sub frmProductionItemSerialReplace_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
            btnpost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub frmProductionItemSerialReplace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        LoadBlankGrid()
        LoadPrinciGrid()
        LoadOtherGrid()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repobomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomcode.Name = colBOMCode
        repobomcode.IsVisible = False
        gv.MasterTemplate.Columns.Add(repobomcode)

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Width = 80
        repolineno.HeaderText = "S.No."
        repolineno.Name = colLineNo
        repolineno.ReadOnly = True
        repolineno.FormatString = ""
        gv.MasterTemplate.Columns.Add(repolineno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colIcode
        repoicode.FormatString = ""
        repoicode.ReadOnly = False
        repoicode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.Width = 350
        repoiname.HeaderText = "Description"
        repoiname.Name = colIname
        repoiname.ReadOnly = True
        repoiname.FormatString = ""
        gv.MasterTemplate.Columns.Add(repoiname)

        Dim repounit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repounit.Width = 80
        repounit.HeaderText = "Unit"
        repounit.Name = colUnit
        repounit.ReadOnly = True
        repounit.FormatString = ""
        gv.MasterTemplate.Columns.Add(repounit)

        Dim reposerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerial.Width = 110
        reposerial.HeaderText = "Serial No"
        reposerial.Name = colSerialNo
        reposerial.FormatString = ""
        reposerial.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposerial)


        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.Width = 200
        reporem.HeaderText = "Remarks"
        reporem.Name = colRemarks
        reporem.MaxLength = 200
        reporem.FormatString = ""
        gv.MasterTemplate.Columns.Add(reporem)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.Rows.AddNew()
    End Sub

    Private Sub LoadPrinciGrid()
        gv_princi.Rows.Clear()
        gv_princi.Columns.Clear()

        Dim repoissueno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoissueno.IsVisible = False
        repoissueno.Name = colPrinciIssueCode
        gv_princi.MasterTemplate.Columns.Add(repoissueno)

        Dim repobomno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomno.IsVisible = False
        repobomno.Name = colPrinciBOMCode
        gv_princi.MasterTemplate.Columns.Add(repobomno)

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Width = 80
        repolineno.HeaderText = "S.No."
        repolineno.Name = colPrinciLineNo
        repolineno.ReadOnly = True
        repolineno.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(repolineno)

        Dim repomaininame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame.Width = 110
        repomaininame.HeaderText = "Prod Item Code"
        repomaininame.Name = colPrinciMainIcode
        repomaininame.ReadOnly = True
        repomaininame.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(repomaininame)

        Dim repomaininame1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame1.Width = 200
        repomaininame1.HeaderText = "Description"
        repomaininame1.Name = colPrinciMainIname
        repomaininame1.ReadOnly = True
        repomaininame1.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(repomaininame1)

        Dim repomaininame11 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame11.Width = 100
        repomaininame11.HeaderText = "Prod Serial No"
        repomaininame11.Name = colPrinciMainSerialno
        repomaininame11.ReadOnly = True
        repomaininame11.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(repomaininame11)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colPrinciIcode
        repoicode.FormatString = ""
        repoicode.ReadOnly = True
        gv_princi.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.Width = 350
        repoiname.HeaderText = "Description"
        repoiname.Name = colPrinciIname
        repoiname.ReadOnly = True
        repoiname.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(repoiname)

        Dim repounit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repounit.Width = 80
        repounit.HeaderText = "Unit"
        repounit.Name = colPrinciUnit
        repounit.ReadOnly = True
        repounit.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(repounit)

        Dim reposerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerial.Width = 110
        reposerial.HeaderText = "Serial No"
        reposerial.Name = colPrinciSerialNo
        reposerial.FormatString = ""
        reposerial.ReadOnly = True
        gv_princi.MasterTemplate.Columns.Add(reposerial)

        Dim reponewserial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponewserial.Width = 110
        reponewserial.HeaderText = "New Serial No"
        reponewserial.Name = colPrinciNewSerialNo
        reponewserial.FormatString = ""
        reponewserial.ReadOnly = False
        reponewserial.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        reponewserial.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_princi.MasterTemplate.Columns.Add(reponewserial)
        

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.Width = 200
        reporem.HeaderText = "Remarks"
        reporem.Name = colPrinciRemarks
        reporem.MaxLength = 200
        reporem.FormatString = ""
        gv_princi.MasterTemplate.Columns.Add(reporem)

        gv_princi.AllowDeleteRow = True
        gv_princi.AllowAddNewRow = False
        gv_princi.ShowGroupPanel = False
        gv_princi.AllowColumnReorder = False
        gv_princi.AllowRowReorder = False
        gv_princi.EnableSorting = False
        gv_princi.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_princi.MasterTemplate.ShowRowHeaderColumn = False
        gv_princi.Rows.AddNew()
    End Sub

    Private Sub LoadOtherGrid()
        gv_other.Rows.Clear()
        gv_other.Columns.Clear()

        Dim repoissueno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoissueno.IsVisible = False
        repoissueno.Name = colRawIssueCode
        gv_other.MasterTemplate.Columns.Add(repoissueno)

        Dim repobomno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomno.IsVisible = False
        repobomno.Name = colRawBOMCode
        gv_other.MasterTemplate.Columns.Add(repobomno)

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Width = 80
        repolineno.HeaderText = "S.No."
        repolineno.Name = colRawLineNo
        repolineno.ReadOnly = True
        repolineno.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(repolineno)

        Dim repomaininame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame.Width = 110
        repomaininame.HeaderText = "Prod Item Code"
        repomaininame.Name = colRawMainIcode
        repomaininame.ReadOnly = True
        repomaininame.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(repomaininame)

        Dim repomaininame1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame1.Width = 200
        repomaininame1.HeaderText = "Description"
        repomaininame1.Name = colRawMainIname
        repomaininame1.ReadOnly = True
        repomaininame1.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(repomaininame1)

        Dim repomaininame11 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame11.Width = 100
        repomaininame11.HeaderText = "Prod Serial No"
        repomaininame11.Name = colRawMainSerialno
        repomaininame11.ReadOnly = True
        repomaininame11.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(repomaininame11)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colRawIcode
        repoicode.FormatString = ""
        repoicode.ReadOnly = True
        gv_other.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.Width = 350
        repoiname.HeaderText = "Description"
        repoiname.Name = colRawIname
        repoiname.ReadOnly = True
        repoiname.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(repoiname)

        Dim repounit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repounit.Width = 80
        repounit.HeaderText = "Unit"
        repounit.Name = colRawUnit
        repounit.ReadOnly = True
        repounit.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(repounit)

        Dim reposerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerial.Width = 110
        reposerial.HeaderText = "Serial No"
        reposerial.Name = colRawSerialNo
        reposerial.FormatString = ""
        reposerial.ReadOnly = True
        gv_other.MasterTemplate.Columns.Add(reposerial)

        Dim reponewserial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponewserial.Width = 110
        reponewserial.HeaderText = "New Serial No"
        reponewserial.Name = colRawNewSerialNo
        reponewserial.FormatString = ""
        reponewserial.ReadOnly = False
        reponewserial.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        reponewserial.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_other.MasterTemplate.Columns.Add(reponewserial)


        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.Width = 200
        reporem.HeaderText = "Remarks"
        reporem.Name = colRawRemarks
        reporem.MaxLength = 200
        reporem.FormatString = ""
        gv_other.MasterTemplate.Columns.Add(reporem)

        gv_other.AllowDeleteRow = True
        gv_other.AllowAddNewRow = False
        gv_other.ShowGroupPanel = False
        gv_other.AllowColumnReorder = False
        gv_other.AllowRowReorder = False
        gv_other.EnableSorting = False
        gv_other.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_other.MasterTemplate.ShowRowHeaderColumn = False
        gv_other.Rows.AddNew()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.Value = clsCommon.ShowSelectForm("LOCFND", qry, "Code", " Location_Type='Physical'", txtLocation.Value, "Code", isButtonClicked)

        gv.Rows.Clear()
        gv.Rows.AddNew()
        gv_other.Rows.Clear()
        gv_other.Rows.AddNew()
        gv_princi.Rows.Clear()
        gv_princi.Rows.AddNew()

        If clsCommon.myLen(txtLocation.Value) Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where location_code='" + txtLocation.Value + "'"))
            AutoFillProdItem()
            If gv.Rows.Count > 0 Then
                AutoFillRawGrid()
            End If
        Else
            txtLocation.Value = ""
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub AutoFillProdItem()
        Try
            
            Dim qry As String = "select distinct TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.bom_code,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_SERIAL_ITEM.Auto_Sr_No from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code and TSPL_SERIAL_ITEM.Document_Type='production' and TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Location_Code=TSPL_SERIAL_ITEM.Location_Code where TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Location_Code='" + txtLocation.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv.Rows(gv.Rows.Count - 1).Cells(colLineNo).Value = CInt(gv.Rows.Count)
                    gv.Rows(gv.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(dr("bom_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colIcode).Value = clsCommon.myCstr(dr("Main_Item_Code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colIname).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colSerialNo).Value = clsCommon.myCstr(dr("Auto_Sr_No"))

                    gv.Rows.AddNew()
                Next

                gv.CurrentRow = gv.Rows(0)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub AutoFillRawGrid()
        Try
            Dim qry As String = "select distinct TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.bom_code,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.issue_code,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Is_principle,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SERIAL_ITEM.Auto_Sr_No,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code,t1.Item_Desc as descrptn,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Unit_Code,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Serial_No from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code and TSPL_SERIAL_ITEM.Document_Type='production' and TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Location_Code=TSPL_SERIAL_ITEM.Location_Code left outer join TSPL_ITEM_MASTER as t1 on t1.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code where TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Location_Code='" + txtLocation.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Is_principle")), "1") = CompairStringResult.Equal Then
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciLineNo).Value = CInt(gv_princi.Rows.Count)
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciMainIcode).Value = clsCommon.myCstr(dr("Main_Item_Code"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciMainIname).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciMainSerialno).Value = clsCommon.myCstr(dr("Auto_Sr_No"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciIcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciIname).Value = clsCommon.myCstr(dr("descrptn"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciSerialNo).Value = clsCommon.myCstr(dr("Serial_No"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciIssueCode).Value = clsCommon.myCstr(dr("issue_code"))
                        gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciBOMCode).Value = clsCommon.myCstr(dr("bom_code"))

                        gv_princi.Rows.AddNew()
                    Else
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawLineNo).Value = CInt(gv_princi.Rows.Count)
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawMainIcode).Value = clsCommon.myCstr(dr("Main_Item_Code"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawMainIname).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawMainSerialno).Value = clsCommon.myCstr(dr("Auto_Sr_No"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawIcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawIname).Value = clsCommon.myCstr(dr("descrptn"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawSerialNo).Value = clsCommon.myCstr(dr("Serial_No"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawIssueCode).Value = clsCommon.myCstr(dr("issue_code"))
                        gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawBOMCode).Value = clsCommon.myCstr(dr("bom_code"))

                        gv_other.Rows.AddNew()
                    End If
                Next

                gv_other.CurrentRow = gv_other.Rows(0)
                gv_princi.CurrentRow = gv_princi.Rows(0)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD where doc_no='" + txtCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsProductionItemSerialReplace.GetFinder("", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As New clsProductionItemSerialReplace()
            obj = clsProductionItemSerialReplace.GetData(strCode, NavType)

            gv.Rows.Clear()
            gv_princi.Rows.Clear()
            gv_other.Rows.Clear()

            gv.Rows.AddNew()
            gv_princi.Rows.AddNew()
            gv_other.Rows.AddNew()

            isNewEntry = True
            isInsideLoadData = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                isNewEntry = False
                txtCode.Value = obj.docno
                dtpDate.Text = obj.docdate
                txtDesc.Text = obj.Descrptn
                txtLocation.Value = obj.loc_code
                lblLocation.Text = obj.loc_desc

                UsLock1.Status = ERPTransactionStatus.Pending
                If obj.is_post = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsProductionItemSerialReplace_Prod In obj.Arr
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineNo).Value = objtr.sno
                        gv.Rows(gv.Rows.Count - 1).Cells(colIcode).Value = objtr.icode
                        gv.Rows(gv.Rows.Count - 1).Cells(colIname).Value = objtr.iname
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = objtr.unit
                        gv.Rows(gv.Rows.Count - 1).Cells(colSerialNo).Value = objtr.serialno
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMCode).Value = objtr.bomcode

                        gv.Rows.AddNew()
                    Next
                End If

                If obj.Arr_Other IsNot Nothing AndAlso obj.Arr_Other.Count > 0 Then
                    For Each objtr As clsProductionItemSerialReplace_Detail In obj.Arr_Other
                        If clsCommon.CompairString(clsCommon.myCstr(objtr.is_principle), "1") = CompairStringResult.Equal Then
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciLineNo).Value = objtr.sno
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciMainIcode).Value = objtr.main_icode
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciMainIname).Value = objtr.main_iname
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciMainSerialno).Value = objtr.main_Serial_No
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciIcode).Value = objtr.icode
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciIname).Value = objtr.iname
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciUnit).Value = objtr.unit
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciSerialNo).Value = objtr.serialno
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciNewSerialNo).Value = objtr.New_serialno
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciRemarks).Value = objtr.remarks
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciBOMCode).Value = objtr.Bomcode
                            gv_princi.Rows(gv_princi.Rows.Count - 1).Cells(colPrinciIssueCode).Value = objtr.Issuecode

                            gv_princi.Rows.AddNew()
                        Else
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawLineNo).Value = objtr.sno
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawMainIcode).Value = objtr.main_icode
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawMainIname).Value = objtr.main_iname
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawMainSerialno).Value = objtr.main_Serial_No
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawIcode).Value = objtr.icode
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawIname).Value = objtr.iname
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawUnit).Value = objtr.unit
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawSerialNo).Value = objtr.serialno
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawNewSerialNo).Value = objtr.New_serialno
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawRemarks).Value = objtr.remarks
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawBOMCode).Value = objtr.Bomcode
                            gv_other.Rows(gv_other.Rows.Count - 1).Cells(colRawIssueCode).Value = objtr.Issuecode

                            gv_other.Rows.AddNew()
                        End If
                    Next
                End If

                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True

                If obj.is_post = "1" Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                End If

                gv.CurrentRow = gv.Rows(0)
                gv_other.CurrentRow = gv_other.Rows(0)
                gv_princi.CurrentRow = gv_princi.Rows(0)
            Else
                FunReset()
            End If

            isInsideLoadData = False
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                txtLocation.Select()
                Throw New Exception("Select Location First")
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim obj As New clsProductionItemSerialReplace()

            obj.docno = clsCommon.myCstr(txtCode.Value)
            obj.docdate = clsCommon.myCDate(dtpDate.Text)
            obj.Descrptn = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.loc_code = clsCommon.myCstr(txtLocation.Value)
            obj.is_post = "0"

            If isPost Then
                obj.is_post = "1"
            End If

            obj.Arr = New List(Of clsProductionItemSerialReplace_Prod)
            obj.Arr_Other = New List(Of clsProductionItemSerialReplace_Detail)

            '================Prod Item====================================
            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsProductionItemSerialReplace_Prod()

                objtr.sno = CInt(grow.Cells(colLineNo).Value)
                objtr.icode = clsCommon.myCstr(grow.Cells(colIcode).Value)
                objtr.unit = clsCommon.myCstr(grow.Cells(colUnit).Value)
                objtr.serialno = clsCommon.myCstr(grow.Cells(colSerialNo).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                objtr.bomcode = clsCommon.myCstr(grow.Cells(colBOMCode).Value)

                If clsCommon.myLen(objtr.icode) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            '================Principle Item====================================
            For Each grow As GridViewRowInfo In gv_princi.Rows
                Dim objtr As New clsProductionItemSerialReplace_Detail()

                objtr.sno = CInt(grow.Cells(colPrinciLineNo).Value)
                objtr.main_icode = clsCommon.myCstr(grow.Cells(colPrinciMainIcode).Value)
                objtr.main_Serial_No = clsCommon.myCstr(grow.Cells(colPrinciMainSerialno).Value)
                objtr.icode = clsCommon.myCstr(grow.Cells(colPrinciIcode).Value)
                objtr.unit = clsCommon.myCstr(grow.Cells(colPrinciUnit).Value)
                objtr.serialno = clsCommon.myCstr(grow.Cells(colPrinciSerialNo).Value)
                objtr.New_serialno = clsCommon.myCstr(grow.Cells(colPrinciNewSerialNo).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colPrinciRemarks).Value).Replace("'", "`")
                objtr.is_principle = "1"
                objtr.Bomcode = clsCommon.myCstr(grow.Cells(colPrinciBOMCode).Value)
                objtr.Issuecode = clsCommon.myCstr(grow.Cells(colPrinciIssueCode).Value)

                If clsCommon.myLen(objtr.icode) > 0 Then
                    obj.Arr_Other.Add(objtr)
                End If
            Next

            '================Other Than Principle Item====================================
            For Each grow As GridViewRowInfo In gv_other.Rows
                Dim objtr As New clsProductionItemSerialReplace_Detail()

                objtr.sno = CInt(grow.Cells(colRawLineNo).Value)
                objtr.main_icode = clsCommon.myCstr(grow.Cells(colRawMainIcode).Value)
                objtr.main_Serial_No = clsCommon.myCstr(grow.Cells(colRawMainSerialno).Value)
                objtr.icode = clsCommon.myCstr(grow.Cells(colRawIcode).Value)
                objtr.unit = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                objtr.serialno = clsCommon.myCstr(grow.Cells(colRawSerialNo).Value)
                objtr.New_serialno = clsCommon.myCstr(grow.Cells(colRawNewSerialNo).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colRawRemarks).Value).Replace("'", "`")
                objtr.is_principle = "0"
                objtr.Bomcode = clsCommon.myCstr(grow.Cells(colRawBOMCode).Value)
                objtr.Issuecode = clsCommon.myCstr(grow.Cells(colRawIssueCode).Value)

                If clsCommon.myLen(objtr.icode) > 0 Then
                    obj.Arr_Other.Add(objtr)
                End If
            Next
            '=================================================

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProductionItemSerialReplace.SaveData(obj, isNewEntry, isPost, trans) Then
                If Not isPost Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                End If

                txtCode.Value = obj.docno

                LoadData(txtCode.Value, NavigatorType.Current)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select document no. first")
            End If

            If Not clsCommon.MyMessageBoxShow("Are you sure,want to delete document no. " + clsCommon.myCstr(txtCode.Value) + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProductionItemSerialReplace.DeleteData(txtCode.Value, trans) Then
                clsCommon.MyMessageBoxShow("Data Delete Successfully", Me.Text)
                FunReset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select document no. first")
            End If

            If Not clsCommon.MyMessageBoxShow("Are you sure,want to post document no. " + clsCommon.myCstr(txtCode.Value) + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            isNewEntry = False
            SaveData(True)

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProductionItemSerialReplace.PostData(txtCode.Value, trans) Then
                clsCommon.MyMessageBoxShow("Data Posted Successfully", Me.Text)
                btnpost.Enabled = False
                btndelete.Enabled = False
                btnsave.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv_other_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_other.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanges Then
                    If e.Column Is gv_other.Columns(colRawNewSerialNo) Then
                        isCellValueChanges = True
                        OpenOtherSerialNo(False)
                        isCellValueChanges = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub OpenOtherSerialNo(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_SERIAL_ITEM.Auto_Sr_No as [SerialCode],TSPL_MF_ISSUE_DETAIL.ITEM_CODE as [Item Code] from TSPL_MF_ISSUE_DETAIL "
        qry += "left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Document_Code=TSPL_MF_ISSUE_DETAIL.ISSUE_CODE and TSPL_SERIAL_ITEM.Document_Type='PROD_IS' and TSPL_SERIAL_ITEM.In_Out_Type='O' "
        qry += "and TSPL_SERIAL_ITEM.Auto_Sr_No<> '" + clsCommon.myCstr(gv_other.CurrentRow.Cells(colRawSerialNo).Value) + "' "
        Dim whrCls As String = " TSPL_MF_ISSUE_DETAIL.BOM_CODE='" + clsCommon.myCstr(gv_other.CurrentRow.Cells(colRawBOMCode).Value) + "' and TSPL_MF_ISSUE_DETAIL.ITEM_CODE='" + clsCommon.myCstr(gv_other.CurrentRow.Cells(colRawIcode).Value) + "' and TSPL_SERIAL_ITEM.Auto_Sr_No not in (select distinct a.New_Serial_No from (select New_Serial_No from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no<>'" + txtCode.Value + "' union all select Old_Serial_No as New_Serial_No from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no<>'" + txtCode.Value + "')a)" 'and TSPL_MF_ISSUE_DETAIL.ISSUE_CODE='" + clsCommon.myCstr(gv_other.CurrentRow.Cells(colRawIssueCode).Value) + "'
        Dim icode As String = ""
        icode = clsCommon.ShowSelectForm("SRNFND", qry, "SerialCode", whrCls, clsCommon.myCstr(gv_other.CurrentRow.Cells(colRawNewSerialNo).Value), "SerialCode", isButtonClicked)

        If clsCommon.myLen(icode) > 0 Then
            gv_other.CurrentRow.Cells(colRawNewSerialNo).Value = icode
        Else
            gv_other.CurrentRow.Cells(colRawNewSerialNo).Value = Nothing
        End If

    End Sub

    Private Sub OpenPrinciSerialNo(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_SERIAL_ITEM.Auto_Sr_No as [SerialCode],TSPL_MF_ISSUE_DETAIL.ITEM_CODE as [Item Code] from TSPL_MF_ISSUE_DETAIL "
        qry += "left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Document_Code=TSPL_MF_ISSUE_DETAIL.ISSUE_CODE and TSPL_SERIAL_ITEM.Document_Type='PROD_IS' and TSPL_SERIAL_ITEM.In_Out_Type='O' "
        qry += "and TSPL_SERIAL_ITEM.Auto_Sr_No<> '" + clsCommon.myCstr(gv_princi.CurrentRow.Cells(colPrinciSerialNo).Value) + "' "
        Dim whrCls As String = " TSPL_MF_ISSUE_DETAIL.BOM_CODE='" + clsCommon.myCstr(gv_princi.CurrentRow.Cells(colPrinciBOMCode).Value) + "' and TSPL_MF_ISSUE_DETAIL.ITEM_CODE='" + clsCommon.myCstr(gv_princi.CurrentRow.Cells(colPrinciIcode).Value) + "' and TSPL_SERIAL_ITEM.Auto_Sr_No not in (select distinct a.New_Serial_No from (select New_Serial_No from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no<>'" + txtCode.Value + "' union all select Old_Serial_No as New_Serial_No from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no<>'" + txtCode.Value + "')a)" 'and TSPL_MF_ISSUE_DETAIL.ISSUE_CODE='" + clsCommon.myCstr(gv_princi.CurrentRow.Cells(colPrinciIssueCode).Value) + "'
        Dim icode As String = ""
        icode = clsCommon.ShowSelectForm("SRNFND", qry, "SerialCode", whrCls, clsCommon.myCstr(gv_princi.CurrentRow.Cells(colPrinciNewSerialNo).Value), "SerialCode", isButtonClicked)

        If clsCommon.myLen(icode) > 0 Then
            gv_princi.CurrentRow.Cells(colPrinciNewSerialNo).Value = icode
        Else
            gv_princi.CurrentRow.Cells(colPrinciNewSerialNo).Value = Nothing
        End If
    End Sub

    Private Sub gv_princi_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_princi.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanges Then
                    If e.Column Is gv_princi.Columns(colPrinciNewSerialNo) Then
                        isCellValueChanges = True
                        OpenPrinciSerialNo(False)
                        isCellValueChanges = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class