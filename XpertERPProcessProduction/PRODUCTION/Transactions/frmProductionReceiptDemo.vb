'-==================created by Monika 28/08/2014==============
Imports common
Imports System.Data.SqlClient

Public Class FrmProductionReceiptDemo
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isNewEntry As Boolean = True

    Const colGVLineno As String = "Lineno"
    Const colGVItemcode As String = "itemCode"
    Const colGVIname As String = "Iname"
    Const colGVUnit As String = "Unit"
    Const colGVQty As String = "Qty"
    Const colGVSerialno As String = "SerialNo"
    Const colGVRemarks As String = "Remarks"
    Const colGVBOM As String = "BOMCode"
    Const ColGVIssueNo As String = "GVIssueNo"
    Const ColGVItemType As String = "GVItemType"
    Const ColSelect As String = "Select"

    Const colLineno As String = "LineNo"
    Const colprod_line_no As String = "ProdLineNo"
    Const colmainIcode As String = "MainIcode"
    Const colMainIname As String = "MainIname"
    Const colMainSerialNo As String = "MainSerialNo"
    Const colitemcode As String = "Item Code"
    Const colItemdesc As String = "Item Desc"
    Const colUnit As String = "Unit"
    Const colSerialNo As String = "SerialNo"
    Const colRecvqty As String = "Rec Qty"
    Const colRemarks As String = "Remarks"
    Const colBOM As String = "BOMCODE"
    Const colIssueno As String = "IssueNo"
    Const ColTagNo As String = "ColTagNo"

    Const colRawLineno As String = "LineNo"
    Const colRawprod_line_no As String = "ProdLineNo"
    Const colRawmainIcode As String = "MainIcode"
    Const colRawMainIname As String = "MainIname"
    Const colRawMainSerialNo As String = "MainSerialNo"
    Const colRawitemcode As String = "Item Code"
    Const colRawItemdesc As String = "Item Desc"
    Const colRawUnit As String = "Unit"
    Const colRawSerialNo As String = "SerialNo"
    Const colRawRecvqty As String = "Rec Qty"
    Const colRawRemarks As String = "Remarks"
    Const colRawBOM As String = "BOMCOde"
    Const colRawIssueno As String = "IssueNo"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Index As Integer = -1
    Dim PIndex As Integer = -1
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionReceiptDemo)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FunReset()
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        txtComment.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtReceivedBy.Value = ""
        lblEmpName.Text = ""
        txtRecptNo.Value = ""
        dtpRecptDate.Text = clsCommon.GETSERVERDATE(Nothing)

        'gv.Rows.Clear()
        'gv_main.Rows.Clear()
        'gv_raw.Rows.Clear()
        LoadBlankGrid()
        LoadPrinciGrid()
        LoadRawGrid()
        Index = -1
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnsave.Text = "Save"
        isNewEntry = True
    End Sub

    Private Sub LoadPrinciGrid()
        gv_main.Rows.Clear()
        gv_main.Columns.Clear()

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Width = 50
        repolineno.HeaderText = "S.No."
        repolineno.Name = colLineno
        repolineno.ReadOnly = True
        repolineno.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repolineno)

        Dim repobomicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomicode.Width = 130
        repobomicode.HeaderText = "BOM Code"
        repobomicode.Name = colBOM
        repobomicode.ReadOnly = True
        repobomicode.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repobomicode)

        Dim repoissuecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoissuecode.Width = 110
        repoissuecode.HeaderText = "Issue Code"
        repoissuecode.Name = colIssueno
        repoissuecode.IsVisible = False
        repoissuecode.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repoissuecode)

        Dim repomainicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomainicode.Width = 110
        repomainicode.HeaderText = "Main Item Code"
        repomainicode.Name = colmainIcode
        repomainicode.ReadOnly = True
        repomainicode.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repomainicode)

        Dim repomaininame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame.Width = 190
        repomaininame.HeaderText = "Main Description"
        repomaininame.Name = colMainIname
        repomaininame.ReadOnly = True
        repomaininame.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repomaininame)

        Dim repomainserialno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomainserialno.Width = 100
        repomainserialno.HeaderText = "Main Serial No."
        repomainserialno.Name = colMainSerialNo
        repomainserialno.ReadOnly = True
        repomainserialno.IsVisible = False
        repomainserialno.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repomainserialno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colitemcode
        repoicode.FormatString = ""
        repoicode.ReadOnly = True
        gv_main.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.Width = 190
        repoiname.HeaderText = "Description"
        repoiname.Name = colItemdesc
        repoiname.ReadOnly = True
        repoiname.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repoiname)

        Dim repounit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repounit.Width = 70
        repounit.HeaderText = "Unit"
        repounit.Name = colUnit
        repounit.ReadOnly = True
        repounit.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(repounit)

        Dim reposerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerial.Width = 110
        reposerial.HeaderText = "Serial No"
        reposerial.Name = colSerialNo
        reposerial.FormatString = ""
        reposerial.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        reposerial.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_main.MasterTemplate.Columns.Add(reposerial)

        Dim reporcvqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporcvqty.Width = 100
        reporcvqty.HeaderText = "Receive Qty"
        reporcvqty.Name = colRecvqty
        reporcvqty.FormatString = "{0:f}"
        reporcvqty.DecimalPlaces = 2
        gv_main.MasterTemplate.Columns.Add(reporcvqty)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.Width = 200
        reporem.HeaderText = "Remarks"
        reporem.Name = colRemarks
        reporem.MaxLength = 200
        reporem.FormatString = ""
        gv_main.MasterTemplate.Columns.Add(reporem)

        gv_main.AllowDeleteRow = True
        gv_main.AllowAddNewRow = False
        gv_main.ShowGroupPanel = False
        gv_main.AllowColumnReorder = False
        gv_main.AllowRowReorder = False
        gv_main.EnableSorting = False
        gv_main.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_main.MasterTemplate.ShowRowHeaderColumn = False
        gv_main.Rows.AddNew()

    End Sub

    Private Sub LoadRawGrid()
        gv_raw.Rows.Clear()
        gv_raw.Columns.Clear()

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Width = 50
        repolineno.HeaderText = "S.No."
        repolineno.Name = colRawLineno
        repolineno.ReadOnly = True
        repolineno.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repolineno)

        Dim repobomicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomicode.Width = 130
        repobomicode.HeaderText = "BOM Code"
        repobomicode.Name = colRawBOM
        repobomicode.ReadOnly = True
        repobomicode.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repobomicode)

        Dim repoissuecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoissuecode.Width = 110
        repoissuecode.HeaderText = "Issue Code"
        repoissuecode.Name = colRawIssueno
        repoissuecode.IsVisible = False
        repoissuecode.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repoissuecode)

        Dim repomainicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomainicode.Width = 110
        repomainicode.HeaderText = "Main Item Code"
        repomainicode.Name = colRawmainIcode
        repomainicode.ReadOnly = True
        repomainicode.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repomainicode)

        Dim repomaininame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomaininame.Width = 190
        repomaininame.HeaderText = "Main Description"
        repomaininame.Name = colRawMainIname
        repomaininame.ReadOnly = True
        repomaininame.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repomaininame)

        Dim repomainserialno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomainserialno.Width = 100
        repomainserialno.HeaderText = "Main Serial No."
        repomainserialno.Name = colRawMainSerialNo
        repomainserialno.IsVisible = False
        repomainserialno.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repomainserialno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colRawitemcode
        repoicode.FormatString = ""
        repoicode.ReadOnly = True
        gv_raw.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.Width = 190
        repoiname.HeaderText = "Description"
        repoiname.Name = colRawItemdesc
        repoiname.ReadOnly = True
        repoiname.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repoiname)

        Dim repounit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repounit.Width = 70
        repounit.HeaderText = "Unit"
        repounit.Name = colRawUnit
        repounit.ReadOnly = True
        repounit.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(repounit)

        Dim reposerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerial.Width = 110
        reposerial.HeaderText = "Serial No"
        reposerial.Name = colRawSerialNo
        reposerial.FormatString = ""
        reposerial.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        reposerial.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_raw.MasterTemplate.Columns.Add(reposerial)


        Dim reporcvqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporcvqty.Width = 100
        reporcvqty.HeaderText = "Receive Qty"
        reporcvqty.Name = colRawRecvqty
        reporcvqty.FormatString = "{0:f}"
        reporcvqty.DecimalPlaces = 2
        gv_raw.MasterTemplate.Columns.Add(reporcvqty)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.Width = 200
        reporem.HeaderText = "Remarks"
        reporem.Name = colRawRemarks
        reporem.MaxLength = 200
        reporem.FormatString = ""
        gv_raw.MasterTemplate.Columns.Add(reporem)

        gv_raw.AllowDeleteRow = True
        gv_raw.AllowAddNewRow = False
        gv_raw.ShowGroupPanel = False
        gv_raw.AllowColumnReorder = False
        gv_raw.AllowRowReorder = False
        gv_raw.EnableSorting = False
        gv_raw.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_raw.MasterTemplate.ShowRowHeaderColumn = False
        gv_raw.Rows.AddNew()
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Width = 50
        repolineno.HeaderText = "S.No."
        repolineno.Name = colGVLineno
        repolineno.ReadOnly = True
        repolineno.FormatString = ""
        gv.MasterTemplate.Columns.Add(repolineno)

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = "Select"
        repoSelect.Name = ColSelect
        repoSelect.Width = 75
        repoSelect.IsVisible = False
        repoSelect.ReadOnly = True
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoSelect)

        Dim repobomicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomicode.Width = 130
        repobomicode.HeaderText = "BOM Code"
        repobomicode.Name = colGVBOM
        repobomicode.ReadOnly = True
        repobomicode.FormatString = ""
        gv.MasterTemplate.Columns.Add(repobomicode)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colGVItemcode
        repoicode.FormatString = ""
        repoicode.ReadOnly = True
        'repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.Width = 220
        repoiname.HeaderText = "Description"
        repoiname.Name = colGVIname
        repoiname.ReadOnly = True
        repoiname.FormatString = ""
        gv.MasterTemplate.Columns.Add(repoiname)

        Dim repounit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repounit.Width = 70
        repounit.HeaderText = "Unit"
        repounit.Name = colGVUnit
        repounit.ReadOnly = True
        repounit.FormatString = ""
        gv.MasterTemplate.Columns.Add(repounit)

        Dim repoIssueNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueNo.Width = 110
        repoIssueNo.HeaderText = "Issue No"
        repoIssueNo.Name = ColGVIssueNo
        repoIssueNo.ReadOnly = True
        repoIssueNo.FormatString = ""
        gv.MasterTemplate.Columns.Add(repoIssueNo)

        Dim repoItemType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemType.Width = 80
        repoItemType.HeaderText = "Item Type"
        repoItemType.Name = ColGVItemType
        repoItemType.ReadOnly = True
        repoItemType.IsVisible = False
        repoItemType.FormatString = ""
        gv.MasterTemplate.Columns.Add(repoItemType)

        Dim reposerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerial.Width = 110
        reposerial.HeaderText = "Serial No"
        reposerial.Name = colGVSerialno
        reposerial.FormatString = ""
        reposerial.ReadOnly = True
        'reposerial.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'reposerial.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(reposerial)


        Dim reporcvqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporcvqty.Width = 100
        reporcvqty.HeaderText = "Build Qty"
        reporcvqty.Name = colGVQty
        reporcvqty.FormatString = "{0:f}"
        reporcvqty.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(reporcvqty)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.Width = 260
        reporem.HeaderText = "Remarks"
        reporem.Name = colGVRemarks
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

    Private Sub FrmProductionReceiptDemo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmProductionReceiptDemo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadPrinciGrid()
        LoadRawGrid()
        LoadBlankGrid()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save data")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete data")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P for post data")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")

        lblReceivedBy.Visible = True
        btngo.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If



    End Sub

    Private Function AllowToSave() As Boolean
        Try
            Dim MainItemType As String = String.Empty

            If clsCommon.myLen(txtRecptNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select issue detail.", Me.Text)
                txtRecptNo.Focus()
                txtRecptNo.Select()
                Return False
            End If

            If clsCommon.myLen(gv.Rows(0).Cells(colGVItemcode).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Fill atleast one row in main item grid.", Me.Text)
                Return False
            End If
            MainItemType = clsCommon.myCstr(gv.Rows(0).Cells(ColGVItemType).Value)
            If clsCommon.CompairString(MainItemType, "S") <> CompairStringResult.Equal Then
                If clsCommon.myLen(gv_main.Rows(0).Cells(colitemcode).Value) <= 0 AndAlso clsCommon.myLen(gv_raw.Rows(0).Cells(colRawitemcode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill atleast one row in principle item grid and raw item grid.", Me.Text)
                    Return False
                End If
            Else
                If clsCommon.myLen(gv_raw.Rows(0).Cells(colRawitemcode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill atleast one row in raw item grid.", Me.Text)
                    Return False
                End If
            End If
            
            '' 12-Oct-2015 Child Items BM00000008095
            Dim DBSrNo As String = String.Empty

            Dim arr As List(Of clsfrmProductionReceiptDemo_Detail) = New List(Of clsfrmProductionReceiptDemo_Detail)
            Dim obj As New clsfrmProductionReceiptDemo_Detail()
            If Index >= 0 Then
                For Each grow As GridViewRowInfo In gv_raw.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colRawBOM).Value)) > 0 Then
                        obj = New clsfrmProductionReceiptDemo_Detail()
                        obj.main_bomcode = clsCommon.myCstr(grow.Cells(colRawBOM).Value)
                        obj.item_code = clsCommon.myCstr(grow.Cells(colRawitemcode).Value)
                        obj.iname = clsCommon.myCstr(grow.Cells(colRawItemdesc).Value)
                        obj.main_icode = clsCommon.myCstr(grow.Cells(colRawmainIcode).Value)
                        obj.main_iname = clsCommon.myCstr(grow.Cells(colRawMainIname).Value)
                        obj.main_issueno = clsCommon.myCstr(grow.Cells(colRawIssueno).Value)
                        obj.main_serial_no = clsCommon.myCstr(grow.Cells(colRawMainSerialNo).Value)
                        obj.unit = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                        obj.serial_no = clsCommon.myCstr(grow.Cells(colRawSerialNo).Value)
                        obj.sno = clsCommon.myCstr(grow.Cells(colRawLineno).Value)
                        obj.rec_qty = clsCommon.myCstr(grow.Cells(colRawRecvqty).Value)
                        obj.remarks = clsCommon.myCstr(grow.Cells(colRawRemarks).Value)
                        arr.Add(obj)
                    End If
                Next
                gv.Rows(Index).Tag = arr
            End If
            '' Princi Items
            Dim arrP As List(Of clsfrmProductionReceiptDemo_Detail) = New List(Of clsfrmProductionReceiptDemo_Detail)
            Dim objP As New clsfrmProductionReceiptDemo_Detail()
            If Index >= 0 Then
                For Each grow As GridViewRowInfo In gv_main.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colBOM).Value)) > 0 Then
                        objP = New clsfrmProductionReceiptDemo_Detail()
                        objP.main_bomcode = clsCommon.myCstr(grow.Cells(colBOM).Value)
                        objP.item_code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                        objP.iname = clsCommon.myCstr(grow.Cells(colItemdesc).Value)
                        objP.main_icode = clsCommon.myCstr(grow.Cells(colmainIcode).Value)
                        objP.main_iname = clsCommon.myCstr(grow.Cells(colMainIname).Value)
                        objP.main_issueno = clsCommon.myCstr(grow.Cells(colIssueno).Value)
                        objP.main_serial_no = clsCommon.myCstr(grow.Cells(colMainSerialNo).Value)
                        objP.unit = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objP.serial_no = clsCommon.myCstr(grow.Cells(colSerialNo).Value)
                        objP.sno = clsCommon.myCstr(grow.Cells(colLineno).Value)
                        objP.rec_qty = clsCommon.myCstr(grow.Cells(colRecvqty).Value)
                        objP.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objP.is_principle = "1"
                        arrP.Add(objP)
                    End If
                Next
                gv.Rows(Index).Cells(colLineno).Tag = arrP
            End If
            'Dim arr As List(Of clsfrmProductionReceiptDemo_Detail) = New List(Of clsfrmProductionReceiptDemo_Detail)

            '' For Child Items
            Dim LST As List(Of String) = New List(Of String)()
            For ii As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv.Rows(ii).Cells(colGVBOM).Value)) > 0 Then
                    arr = TryCast(gv.Rows(ii).Tag, List(Of clsfrmProductionReceiptDemo_Detail))
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For Each obj1 As clsfrmProductionReceiptDemo_Detail In arr
                            If clsCommon.myLen(obj1.serial_no) > 0 Then
                                If Not (LST.Contains(obj1.serial_no)) Then
                                    LST.Add(obj1.serial_no)
                                Else
                                    common.clsCommon.MyMessageBoxShow("Please check ! " + obj1.serial_no + " is repeating with " + obj1.item_code + " item (" & obj1.main_serial_no & ") in other item.")
                                    Return False
                                End If
                            End If
                        Next
                    End If
                End If
            Next

            '' For Princi Items
            Dim LSTP As List(Of String) = New List(Of String)()
            For i As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv.Rows(i).Cells(colGVBOM).Value)) > 0 Then
                    arrP = TryCast(gv.Rows(i).Cells(colLineno).Tag, List(Of clsfrmProductionReceiptDemo_Detail))
                    If arrP IsNot Nothing AndAlso arrP.Count > 0 Then
                        For Each obj1 As clsfrmProductionReceiptDemo_Detail In arrP
                            If clsCommon.myLen(obj1.serial_no) > 0 Then
                                If Not (LSTP.Contains(obj1.serial_no)) Then
                                    LSTP.Add(obj1.serial_no)
                                Else
                                    common.clsCommon.MyMessageBoxShow(Me, "Please check ! " + obj1.serial_no + " is repeating with " + obj1.item_code + " item (" & obj1.main_serial_no & ") in principle item.")
                                    Return False
                                End If
                            End If
                        Next
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim obj As New clsfrmProductionReceiptDemo()

            obj.arrMain = New List(Of clsfrmProductionRecieptDetail)
            obj.arrItem = New List(Of clsfrmProductionReceiptDemo_Detail)

            obj.docno = clsCommon.myCstr(txtCode.Value)
            obj.docdate = clsCommon.myCDate(dtpDate.Text)
            obj.description = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.comment = clsCommon.myCstr(txtComment.Text).Replace("'", "`")
            obj.recvno = clsCommon.myCstr(txtRecptNo.Value)
            obj.loc_code = clsCommon.myCstr(txtLocation.Value)
            obj.is_post = "0"
            If isPost Then
                obj.is_post = "1"
            End If

            Dim objtr1 As New clsfrmProductionRecieptDetail()
            For Each grow As GridViewRowInfo In gv.Rows
                objtr1 = New clsfrmProductionRecieptDetail()

                objtr1.sno = CInt(grow.Cells(colGVLineno).Value)
                objtr1.bom_code = clsCommon.myCstr(grow.Cells(colGVBOM).Value)
                objtr1.icode = clsCommon.myCstr(grow.Cells(colGVItemcode).Value)
                objtr1.qty = clsCommon.myCdbl(grow.Cells(colGVQty).Value)
                objtr1.unit = clsCommon.myCstr(grow.Cells(colGVUnit).Value)
                objtr1.serialno = clsCommon.myCstr(grow.Cells(colGVSerialno).Value)
                objtr1.remarks = clsCommon.myCstr(grow.Cells(colGVRemarks).Value)
                '' 05-Oct-2015
                If CBool(grow.Cells(ColSelect).Value) = True Then
                    objtr1.IsSelect = "1"
                Else
                    objtr1.IsSelect = "0"
                End If
                objtr1.arrSrItem = TryCast(grow.Tag, List(Of clsfrmProductionReceiptDemo_Detail))
                objtr1.arrPrinciItem = TryCast(grow.Cells(colLineno).Tag, List(Of clsfrmProductionReceiptDemo_Detail))
                If clsCommon.myLen(objtr1.icode) > 0 Then
                    obj.arrMain.Add(objtr1)
                End If
            Next

            'Dim objtr As New clsfrmProductionReceiptDemo_Detail()
            'For Each grow As GridViewRowInfo In gv_main.Rows
            '    objtr = New clsfrmProductionReceiptDemo_Detail()

            '    objtr.sno = CInt(grow.Cells(colLineno).Value)
            '    objtr.main_bomcode = clsCommon.myCstr(grow.Cells(colBOM).Value)
            '    objtr.main_issueno = clsCommon.myCstr(grow.Cells(colIssueno).Value)
            '    objtr.main_icode = clsCommon.myCstr(grow.Cells(colmainIcode).Value)
            '    objtr.main_serial_no = clsCommon.myCstr(grow.Cells(colMainSerialNo).Value)
            '    objtr.item_code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
            '    objtr.unit = clsCommon.myCstr(grow.Cells(colUnit).Value)
            '    objtr.rec_qty = clsCommon.myCdbl(grow.Cells(colRecvqty).Value)
            '    objtr.serial_no = clsCommon.myCstr(grow.Cells(colSerialNo).Value)
            '    objtr.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
            '    objtr.is_principle = "1"

            '    If clsCommon.myLen(objtr.item_code) > 0 Then
            '        obj.arrItem.Add(objtr)

            '    End If

            'Next

            'For Each grow As GridViewRowInfo In gv_raw.Rows
            '    objtr = New clsfrmProductionReceiptDemo_Detail()

            '    objtr.sno = CInt(grow.Cells(colRawLineno).Value)
            '    objtr.main_bomcode = clsCommon.myCstr(grow.Cells(colRawBOM).Value)
            '    objtr.main_issueno = clsCommon.myCstr(grow.Cells(colRawIssueno).Value)
            '    objtr.main_icode = clsCommon.myCstr(grow.Cells(colRawmainIcode).Value)
            '    objtr.main_serial_no = clsCommon.myCstr(grow.Cells(colRawMainSerialNo).Value)
            '    objtr.item_code = clsCommon.myCstr(grow.Cells(colRawitemcode).Value)
            '    objtr.unit = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
            '    objtr.rec_qty = clsCommon.myCdbl(grow.Cells(colRawRecvqty).Value)
            '    objtr.serial_no = clsCommon.myCstr(grow.Cells(colRawSerialNo).Value)
            '    objtr.remarks = clsCommon.myCstr(grow.Cells(colRawRemarks).Value)
            '    objtr.is_principle = "0"

            '    If clsCommon.myLen(objtr.item_code) > 0 Then
            '        obj.arrItem.Add(objtr)
            '    End If
            'Next

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmProductionReceiptDemo.SaveData(isNewEntry, isPost, obj, trans) Then
                If Not isPost Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                End If
                txtCode.Value = obj.docno

                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select document for deletion")
            End If

            If Not clsCommon.MyMessageBoxShow(Me, "Are you sure,want to delete document no. " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmProductionReceiptDemo.DeleteData(txtCode.Value, trans) Then
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select document for posting")
            End If

            If Not clsCommon.MyMessageBoxShow(Me, "Are you sure,want to post document no. " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            isNewEntry = False
            SaveData(True)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmProductionReceiptDemo.PostData(txtCode.Value, trans) Then
                clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(clsCommon.myCstr(txtCode.Value), NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*)  from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where doc_no='" + clsCommon.myCstr(txtCode.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsfrmProductionReceiptDemo.GetFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        'Try
        '    Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        '    Dim WhrCls As String = " Location_Type='Physical'  "
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        '    End If
        '    txtLocation.Value = clsCommon.ShowSelectForm("LocFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        '    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub

    Private Sub txtReceivedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReceivedBy._MYValidating
        'Try
        '    Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtReceivedBy.Value, isButtonClicked)
        '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
        '        txtReceivedBy.Value = obj.EMP_CODE
        '        lblEmpName.Text = obj.Emp_Name
        '    Else
        '        txtReceivedBy.Value = ""
        '        lblEmpName.Text = ""
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub

    Private Sub txtIssueNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRecptNo._MYValidating
        Try
            Dim qry As String = "SELECT T1.RECEIPT_CODE AS Code,T1.DESCRIPTION,T1.RECEIPT_DATE, "
            qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_RECEIPT AS T1"
            txtRecptNo.Value = clsCommon.ShowSelectForm("RCPTFND", qry, "Code", " POSTED='1' and RECEIPT_CODE not in (select Against_Receipt_No from TSPL_MF_PRINCIPLE_RECEIPT_HEAD)", txtRecptNo.Value, "Code", isButtonClicked)

            gv.Rows.AddNew()
            gv_main.Rows.Clear()
            gv_raw.Rows.Clear()
            If clsCommon.myLen(txtRecptNo.Value) > 0 Then
                dtpRecptDate.Text = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select receipt_date from TSPL_MF_RECEIPT where receipt_CODE='" + clsCommon.myCstr(txtRecptNo.Value) + "'"))
                txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from TSPL_MF_RECEIPT where receipt_CODE='" + clsCommon.myCstr(txtRecptNo.Value) + "'"))
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + clsCommon.myCstr(txtLocation.Value) + "'"))
                txtReceivedBy.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select received_by from TSPL_MF_RECEIPT where receipt_CODE='" + clsCommon.myCstr(txtRecptNo.Value) + "'"))
                lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" + clsCommon.myCstr(txtReceivedBy.Value) + "'"))
                AutoFillMain()
                btngo.PerformClick()
            Else
                gv.Rows.Clear()
                gv.Rows.AddNew()
                dtpRecptDate.Text = clsCommon.GETSERVERDATE(Nothing)
                txtLocation.Value = ""
                lblLocation.Text = ""
                txtReceivedBy.Value = ""
                lblReceivedBy.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim MainItemType As String = String.Empty
            Dim RIndex As Integer = 0

            Dim obj As New clsfrmProductionReceiptDemo()
            isNewEntry = False
            obj = clsfrmProductionReceiptDemo.GetData(strCode, NavType)
            isInsideLoadData = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                FunReset()
                txtCode.Value = obj.docno
                dtpDate.Text = obj.docdate
                txtDesc.Text = obj.description
                txtComment.Text = obj.comment
                txtRecptNo.Value = obj.recvno
                dtpRecptDate.Text = obj.recdate
                txtLocation.Value = obj.loc_code
                lblLocation.Text = obj.loc_desc
                txtReceivedBy.Value = obj.recv_by
                lblEmpName.Text = obj.recv_name


                'gv.Rows.Clear()
                'gv.Rows.AddNew()
                If obj.arrMain IsNot Nothing AndAlso obj.arrMain.Count > 0 Then
                    For Each objtr As clsfrmProductionRecieptDetail In obj.arrMain

                        gv.Rows(gv.Rows.Count - 1).Cells(colGVLineno).Value = objtr.sno
                        gv.Rows(gv.Rows.Count - 1).Cells(colGVItemcode).Value = objtr.icode
                        gv.Rows(gv.Rows.Count - 1).Cells(ColGVItemType).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Item_Type,'') As [Item Type] FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & objtr.icode & "'"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colGVBOM).Value = objtr.bom_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colGVIname).Value = objtr.iname
                        gv.Rows(gv.Rows.Count - 1).Cells(colGVUnit).Value = objtr.unit
                        gv.Rows(gv.Rows.Count - 1).Cells(colGVQty).Value = objtr.qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colGVSerialno).Value = objtr.serialno
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(ColGVIssueNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISSUE_CODE FROM TSPL_MF_ISSUE_DETAIL WHERE BOM_CODE ='" + objtr.bom_code + "'"))
                        MainItemType = clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(ColGVItemType).Value)
                        ''''''''''''''''''''''''''''''''''''''''''
                        gv.Rows(gv.Rows.Count - 1).Tag = objtr.arrSrItem
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Tag = objtr.arrPrinciItem
                        'Dim counter As Integer = 0
                        'If objtr.arrSrItem IsNot Nothing AndAlso objtr.arrSrItem.Count > 0 Then
                        '    gv_raw.Rows.Clear()
                        '    For Each obj1 As clsfrmProductionReceiptDemo_Detail In objtr.arrSrItem
                        '        If counter > gv_raw.RowCount - 1 Then
                        '            gv_raw.Rows.AddNew()
                        '        End If
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawLineno).Value = obj1.sno
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawitemcode).Value = obj1.item_code
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawItemdesc).Value = obj1.iname
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawBOM).Value = obj1.main_bomcode
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawmainIcode).Value = obj1.main_icode
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawMainIname).Value = obj1.main_iname
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawIssueno).Value = obj1.main_issueno
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawMainSerialNo).Value = obj1.main_serial_no
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawUnit).Value = obj1.unit
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawRecvqty).Value = obj1.rec_qty
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawRemarks).Value = obj1.remarks
                        '        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawSerialNo).Value = obj1.serial_no
                        '        counter += 1
                        '    Next
                        'End If

                        ' ''''''''''''''''''''''

                        ' '' Princi Items
                        'Dim Pcounter As Integer = 0
                        'If objtr.arrPrinciItem IsNot Nothing AndAlso objtr.arrPrinciItem.Count > 0 Then
                        '    gv_main.Rows.Clear()
                        '    For Each obj1 As clsfrmProductionReceiptDemo_Detail In objtr.arrPrinciItem
                        '        If Pcounter > gv_main.RowCount - 1 Then
                        '            gv_main.Rows.AddNew()
                        '        End If
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colLineno).Value = obj1.sno
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colitemcode).Value = obj1.item_code
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colItemdesc).Value = obj1.iname
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colBOM).Value = obj1.main_bomcode
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colmainIcode).Value = obj1.main_icode
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colMainIname).Value = obj1.main_iname
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colIssueno).Value = obj1.main_issueno
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colMainSerialNo).Value = obj1.main_serial_no
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colUnit).Value = obj1.unit
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colRecvqty).Value = obj1.rec_qty
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colRemarks).Value = obj1.remarks
                        '        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colSerialNo).Value = obj1.serial_no

                        '        Pcounter += 1
                        '    Next
                        'End If
                        ''
                        gv.Rows.AddNew()
                        RIndex = gv.Rows(gv.Rows.Count - 1).Index
                    Next
                End If

                'If clsCommon.CompairString(MainItemType, "S") <> CompairStringResult.Equal Then
                '    gv_main.Rows.Clear()
                '    gv_raw.Rows.Clear()
                '    gv_main.Rows.AddNew()
                '    gv_raw.Rows.AddNew()

                '    If obj.arrItem IsNot Nothing AndAlso obj.arrItem.Count > 0 Then
                '        For Each objtr As clsfrmProductionReceiptDemo_Detail In obj.arrItem
                '            If clsCommon.CompairString(objtr.is_principle, "1") = CompairStringResult.Equal Then
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colLineno).Value = objtr.sno
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colBOM).Value = objtr.main_bomcode
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colIssueno).Value = objtr.main_issueno
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colmainIcode).Value = objtr.main_icode
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colMainIname).Value = objtr.main_iname
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colMainSerialNo).Value = objtr.main_serial_no
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colitemcode).Value = objtr.item_code
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colItemdesc).Value = objtr.iname
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colUnit).Value = objtr.unit
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colSerialNo).Value = objtr.serial_no
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colRecvqty).Value = objtr.rec_qty
                '                gv_main.Rows(gv_main.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks

                '                gv_main.Rows.AddNew()
                '            Else
                '                ''
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawLineno).Value = objtr.sno
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawBOM).Value = objtr.main_bomcode
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawIssueno).Value = objtr.main_issueno
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawmainIcode).Value = objtr.main_icode
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawMainIname).Value = objtr.main_iname
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawMainSerialNo).Value = objtr.main_serial_no
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawitemcode).Value = objtr.item_code
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawItemdesc).Value = objtr.iname
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawUnit).Value = objtr.unit
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawSerialNo).Value = objtr.serial_no
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawRecvqty).Value = objtr.rec_qty
                '                gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawRemarks).Value = objtr.remarks

                '                gv_raw.Rows.AddNew()
                '                ''

                '            End If
                '        Next
                '    End If
                'End If
                If clsCommon.CompairString(MainItemType, "S") = CompairStringResult.Equal Then
                    SplitContainer4.Panel1.Visible = False
                    SplitContainer3.SplitterDistance = 187
                    SplitContainer4.SplitterDistance = 27
                Else
                    SplitContainer4.Panel1.Visible = True
                    SplitContainer3.SplitterDistance = 128
                    SplitContainer4.SplitterDistance = 124
                End If

                UsLock1.Status = ERPTransactionStatus.Pending
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True
                txtCode.MyReadOnly = True

                If obj.is_post = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                End If

                'gv.CurrentRow = gv.Rows(0)
                gv.CurrentRow = gv.Rows(RIndex - 1)
                'If clsCommon.CompairString(MainItemType, "S") <> CompairStringResult.Equal Then
                '    gv_main.CurrentRow = gv_main.Rows(0)
                'End If

                'gv_raw.CurrentRow = gv_raw.Rows(0)
                LblPSno.Text = "S.No. (" & RIndex & ")"
                LblSNo.Text = "S.No. (" & RIndex & ")"
            Else
                FunReset()
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isInsideLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "Grid Events"

    Private Sub gv_main_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv_main.CellFormatting
        
    End Sub
    Private Sub gv_main_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_main.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv_main.Columns(colSerialNo) Then
                        isCellValueChanged = True
                        OpenMainSerialCode(False)
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenMainSerialCode(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_SERIAL_ITEM.Auto_Sr_No as Code,TSPL_SERIAL_ITEM.Item_Code,TSPL_ITEM_MASTER.Item_Desc as Description from TSPL_SERIAL_ITEM left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code "
        Dim code As String = clsCommon.ShowSelectForm("RAWFND", qry, "Code", " TSPL_SERIAL_ITEM.Item_Code='" + clsCommon.myCstr(gv_main.CurrentRow.Cells(colitemcode).Value) + "' and TSPL_SERIAL_ITEM.Document_Code='" + clsCommon.myCstr(gv_main.CurrentRow.Cells(colIssueno).Value) + "' and TSPL_SERIAL_ITEM.Document_Type='PROD_IS' and TSPL_SERIAL_ITEM.Location_Code='" + clsCommon.myCstr(txtLocation.Value) + "' and TSPL_SERIAL_ITEM.Auto_Sr_No not in (select Serial_No from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where Doc_No<>'" + clsCommon.myCstr(txtCode.Value) + "')", clsCommon.myCstr(gv_main.CurrentRow.Cells(colSerialNo).Value), "Code", isButtonClicked)
        'Dim code As String = clsCommon.ShowSelectForm("RAWFND", qry, "Code", " TSPL_SERIAL_ITEM.Item_Code='" + clsCommon.myCstr(gv_main.CurrentRow.Cells(colitemcode).Value) + "' and TSPL_SERIAL_ITEM.Document_Code='" + clsCommon.myCstr(gv_main.CurrentRow.Cells(colIssueno).Value) + "' and TSPL_SERIAL_ITEM.Document_Type='PROD_IS' and TSPL_SERIAL_ITEM.Location_Code='" + clsCommon.myCstr(txtLocation.Value) + "' ", clsCommon.myCstr(gv_main.CurrentRow.Cells(colSerialNo).Value), "Code", isButtonClicked)

        If clsCommon.myLen(code) > 0 Then
            gv_main.CurrentRow.Cells(colSerialNo).Value = code
        End If

    End Sub

    Private Sub gv_main_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_main.CurrentColumnChanged
        If gv_main.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_main.CurrentRow.Index
            gv_main.CurrentRow.Cells(colLineno).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gv_main.Rows.Count - 1 Then
                gv_main.Rows.AddNew()
                gv_main.CurrentRow = gv_main.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_main_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv_main.UserDeletingRow

    End Sub
    '=====================================================================
    Private Sub gv_raw_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_raw.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv_raw.Columns(colRawSerialNo) Then
                        isCellValueChanged = True
                        OpenRawSerialNo(False)
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenRawSerialNo(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_SERIAL_ITEM.Auto_Sr_No as Code,TSPL_SERIAL_ITEM.Item_Code,TSPL_ITEM_MASTER.Item_Desc as Description from TSPL_SERIAL_ITEM left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code "
        Dim code As String = clsCommon.ShowSelectForm("RAWFND", qry, "Code", " TSPL_SERIAL_ITEM.Item_Code='" + clsCommon.myCstr(gv_raw.CurrentRow.Cells(colRawitemcode).Value) + "' and TSPL_SERIAL_ITEM.Document_Code='" + clsCommon.myCstr(gv_raw.CurrentRow.Cells(colRawIssueno).Value) + "' and TSPL_SERIAL_ITEM.Document_Type='PROD_IS' and TSPL_SERIAL_ITEM.Location_Code='" + clsCommon.myCstr(txtLocation.Value) + "' and TSPL_SERIAL_ITEM.Auto_Sr_No not in (select Serial_No from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where Doc_No<>'" + clsCommon.myCstr(txtCode.Value) + "')", clsCommon.myCstr(gv_raw.CurrentRow.Cells(colRawSerialNo).Value), "Code", isButtonClicked)
        'Dim code As String = clsCommon.ShowSelectForm("RAWFND", qry, "Code", " TSPL_SERIAL_ITEM.Item_Code='" + clsCommon.myCstr(gv_raw.CurrentRow.Cells(colRawitemcode).Value) + "' and TSPL_SERIAL_ITEM.Document_Code='" + clsCommon.myCstr(gv_raw.CurrentRow.Cells(colRawIssueno).Value) + "' and TSPL_SERIAL_ITEM.Document_Type='PROD_IS' and TSPL_SERIAL_ITEM.Location_Code='" + clsCommon.myCstr(txtLocation.Value) + "' and TSPL_SERIAL_ITEM.Auto_Sr_No not in (select Serial_No from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where Doc_No='" + clsCommon.myCstr(txtCode.Value) + "')", clsCommon.myCstr(gv_raw.CurrentRow.Cells(colRawSerialNo).Value), "Code", isButtonClicked)

        If clsCommon.myLen(code) > 0 Then
            gv_raw.CurrentRow.Cells(colRawSerialNo).Value = code
        End If

    End Sub

    Private Sub gv_raw_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_raw.CurrentColumnChanged
        If gv_raw.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_raw.CurrentRow.Index
            gv_raw.CurrentRow.Cells(colRawLineno).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gv_raw.Rows.Count - 1 Then
                gv_raw.Rows.AddNew()
                gv_raw.CurrentRow = gv_raw.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_raw_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv_raw.UserDeletingRow

    End Sub
#End Region

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        Try
            gv_main.Rows.Clear()
            gv_raw.Rows.Clear()
            Dim allbomcode As String = Nothing

            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(colGVIname).Value) > 0 Then
                    allbomcode = allbomcode + "','" + clsCommon.myCstr(grow.Cells(colGVBOM).Value)
                End If
            Next

            If clsCommon.myLen(allbomcode) > 0 AndAlso allbomcode.Substring(0, 3) = "','" Then
                allbomcode = allbomcode.Substring(2, allbomcode.Length - 2)
            End If
            gv_main.Rows.Clear()
            gv_raw.Rows.Clear()
            'gv_main.Rows.AddNew()
            'gv_raw.Rows.AddNew()
            'ShowDetail(allbomcode) 05-Oct
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub ShowDetail(ByVal allbomcode As String, ByVal SerialNo As String, Optional ByVal ItemCode As String = Nothing, Optional ByVal IssueNo As String = Nothing)

        Dim DBExists As Double = 0
        Dim qry As String = String.Empty

        DBExists = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(IsSelect,0) AS IsSelect From TSPL_MF_PRINCIPLE_RECEIPT_DETAIL Where bom_code=" & allbomcode & "' And Serial_No ='" & SerialNo & "'"))

        If DBExists = 0 Then
            qry = "select TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as Code,tspl_item_master.ITEM_TYPE,tspl_item_master.Is_Serial_Item, " & _
            " tspl_item_master.item_desc as Description,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as Unit,TSPL_MF_BOM_DETAIL.CONSM_QUANTITY AS BOMQty,aa.Qty,tspl_mf_bom_head.BOM_CODE as [BOM Code], " & _
            " TSPL_MF_BOM_HEAD.PROD_ITEM_CODE as [Main Item],aa.issue_code,TSPL_MF_BOM_DETAIL.Is_Principle from TSPL_MF_BOM_DETAIL  " & _
            " left outer join TSPL_MF_BOM_HEAD on tspl_mf_bom_head.bom_code=TSPL_MF_BOM_DETAIL.bom_code " & _
            " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE " & _
            " left outer join (select tspl_mf_issue_detail.ISSUE_CODE,tspl_mf_issue_detail.BOM_CODE,tspl_mf_issue_detail.ITEM_CODE, " & _
            " SUM(tspl_mf_issue_detail.issue_qty) as qty from tspl_mf_issue_detail " & _
            " inner join tspl_mf_issue on tspl_mf_issue_detail.ISSUE_CODE=tspl_mf_issue.ISSUE_CODE where tspl_mf_issue.POSTED=1  " & _
            " and tspl_mf_issue_detail.bom_code in (" + allbomcode + "') group by tspl_mf_issue_detail.ISSUE_CODE,tspl_mf_issue_detail.ITEM_CODE, " & _
            " tspl_mf_issue_detail.BOM_CODE) aa on aa.bom_code=tspl_mf_bom_detail.bom_code and aa.item_code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE"
            qry += " where TSPL_MF_BOM_DETAIL.bom_code in (" + allbomcode + "') and tspl_mf_bom_head.posted='1'"
        Else
            qry = "Select TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code AS Code,TSPL_ITEM_MASTER.Item_Type AS Item_Type,TSPL_ITEM_MASTER.Is_Serial_Item ," & _
                    " TSPL_ITEM_MASTER.Item_Desc AS Description,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Unit_Code AS Unit,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Rec_Qty AS Qty  , " & _
                    " TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.bom_code AS [BOM Code], " & _
                    " TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code AS [Main Item], " & _
                    " TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.issue_code AS [Issue Code] " & _
                    " From TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL " & _
                    " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code  = TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code " & _
                    " WHERE TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.issue_code IN (" & IssueNo & ") AND TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code IN (" & ItemCode & ")" & _
                    " AND TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Serial_No IN (" & SerialNo & ")"
        End If

        Dim RCount As Integer = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' Dim qty As Decimal = clsCommon.myCdbl(dr("qty"))
                Dim qty As Decimal = clsCommon.myCdbl(dr("BOMQty"))

                While (qty > 0)
                    If dr.Item("Is_Serial_Item") = 0 Then
                        Continue For
                    End If
                    RCount += 1
                    If clsCommon.CompairString(clsCommon.myCstr(dr("is_principle")), "1") = CompairStringResult.Equal Then
                        If RCount = 1 Then
                            gv_main.Rows.AddNew()
                            gv_main.Rows.Clear()
                        End If
                        gv_main.Rows.AddNew()
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colLineno).Value = CInt(gv_main.Rows.Count)
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colmainIcode).Value = clsCommon.myCstr(dr("main item"))
                        'gv_main.CurrentRow.Cells(colMainSerialNo).Value = clsCommon.myCstr(dr("main item"))
                        gv_main.CurrentRow.Cells(colMainSerialNo).Value = SerialNo
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colMainIname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(dr("main item")) + "'"))
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("Code"))
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colItemdesc).Value = clsCommon.myCstr(dr("description"))
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colBOM).Value = clsCommon.myCstr(dr("bom code"))
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("unit"))
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colRecvqty).Value = 1
                        gv_main.Rows(gv_main.Rows.Count - 1).Cells(colIssueno).Value = clsCommon.myCstr(dr("issue_code"))

                    Else
                        ' No need for recursive items ,child items should be dispaly as it is
                        'If clsCommon.CompairString(dr.Item("Item_Type"), "S") = CompairStringResult.Equal Or clsCommon.CompairString(dr.Item("Item_Type"), "F") = CompairStringResult.Equal Then
                        '    Dim dtBOM As DataTable = clsManufacturingOrder.GetItemBOM(dr.Item("Code"), Nothing)
                        '    If dtBOM.Rows.Count > 0 Then
                        '        If clsCommon.myLen(dtBOM.Rows(0).Item("BOM_Code")) > 0 Then
                        '            ShowDetail("'" & dtBOM.Rows(0).Item("BOM_Code"), "", "")
                        '        End If
                        '    End If
                        'End If
                        If dr.Item("Is_Serial_Item") = 0 Then
                            Continue For
                        End If
                        gv_raw.Rows.AddNew()
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawLineno).Value = CInt(gv_raw.Rows.Count)
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawmainIcode).Value = clsCommon.myCstr(dr("main item"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawMainIname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(dr("main item")) + "'"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawitemcode).Value = clsCommon.myCstr(dr("Code"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawItemdesc).Value = clsCommon.myCstr(dr("description"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawBOM).Value = clsCommon.myCstr(dr("bom code"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawUnit).Value = clsCommon.myCstr(dr("unit"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawRecvqty).Value = 1
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawIssueno).Value = clsCommon.myCstr(dr("issue_code"))
                        gv_raw.Rows(gv_raw.Rows.Count - 1).Cells(colRawMainSerialNo).Value = clsCommon.myCstr(SerialNo)

                    End If

                    qty -= 1
                End While
            Next
            gv_main.Rows.AddNew()
            gv_raw.Rows.AddNew()
            gv_main.CurrentRow = gv_main.Rows(0)
            gv_raw.CurrentRow = gv_raw.Rows(0)
        End If
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick

        isInsideLoadData = True
        If gv.CurrentRow IsNot Nothing Then
            Dim strBOM As String = clsCommon.myCstr(gv.CurrentRow.Cells(colGVBOM).Value)
            Dim strSrNo As String = clsCommon.myCstr(gv.CurrentRow.Cells(colGVSerialno).Value)
            Dim strItemCode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colGVItemcode).Value)
            Dim strIssueNo As String = clsCommon.myCstr(gv.CurrentRow.Cells(ColGVIssueNo).Value)
            Dim IsBlankFilled As Boolean = False
            If clsCommon.myLen(strBOM) > 0 Then
                '' For Child Items
                Dim arr As List(Of clsfrmProductionReceiptDemo_Detail) = New List(Of clsfrmProductionReceiptDemo_Detail)
                Dim arrP As List(Of clsfrmProductionReceiptDemo_Detail) = New List(Of clsfrmProductionReceiptDemo_Detail)
                Dim obj As New clsfrmProductionReceiptDemo_Detail()
                Dim objP As New clsfrmProductionReceiptDemo_Detail()
                If Index >= 0 Then
                    For Each grow As GridViewRowInfo In gv_raw.Rows
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colRawBOM).Value)) > 0 Then
                            obj = New clsfrmProductionReceiptDemo_Detail()
                            obj.main_bomcode = clsCommon.myCstr(grow.Cells(colRawBOM).Value)
                            obj.item_code = clsCommon.myCstr(grow.Cells(colRawitemcode).Value)
                            obj.iname = clsCommon.myCstr(grow.Cells(colRawItemdesc).Value)
                            obj.main_icode = clsCommon.myCstr(grow.Cells(colRawmainIcode).Value)
                            obj.main_iname = clsCommon.myCstr(grow.Cells(colRawMainIname).Value)
                            obj.main_issueno = clsCommon.myCstr(grow.Cells(colRawIssueno).Value)
                            obj.main_serial_no = clsCommon.myCstr(grow.Cells(colRawMainSerialNo).Value)
                            obj.unit = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                            obj.serial_no = clsCommon.myCstr(grow.Cells(colRawSerialNo).Value)
                            obj.sno = clsCommon.myCstr(grow.Cells(colRawLineno).Value)
                            obj.rec_qty = clsCommon.myCstr(grow.Cells(colRawRecvqty).Value)
                            obj.remarks = clsCommon.myCstr(grow.Cells(colRawRemarks).Value)
                            arr.Add(obj)
                        End If
                    Next
                    gv.Rows(Index).Tag = arr


                    For Each grow As GridViewRowInfo In gv_main.Rows
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colBOM).Value)) > 0 Then
                            objP = New clsfrmProductionReceiptDemo_Detail()
                            objP.sno = clsCommon.myCstr(grow.Cells(colLineno).Value)
                            objP.main_bomcode = clsCommon.myCstr(grow.Cells(colBOM).Value)
                            objP.item_code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                            objP.iname = clsCommon.myCstr(grow.Cells(colItemdesc).Value)
                            objP.main_icode = clsCommon.myCstr(grow.Cells(colmainIcode).Value)
                            objP.main_iname = clsCommon.myCstr(grow.Cells(colMainIname).Value)
                            objP.main_issueno = clsCommon.myCstr(grow.Cells(colIssueno).Value)
                            objP.main_serial_no = clsCommon.myCstr(grow.Cells(colMainSerialNo).Value)
                            objP.unit = clsCommon.myCstr(grow.Cells(colUnit).Value)
                            objP.serial_no = clsCommon.myCstr(grow.Cells(colSerialNo).Value)
                            objP.rec_qty = clsCommon.myCstr(grow.Cells(colRecvqty).Value)
                            objP.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                            objP.is_principle = "1"
                            arrP.Add(objP)
                        End If
                    Next
                    gv.Rows(Index).Cells(colLineno).Tag = arrP
                End If


                gv_raw.DataSource = Nothing
                gv_raw.Rows.Clear()

                'gv.CurrentRow.Tag = arr
                arr = Nothing
                arr = TryCast(gv.CurrentRow.Tag, List(Of clsfrmProductionReceiptDemo_Detail))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        Dim counter As Integer = 0
                        For Each obj1 As clsfrmProductionReceiptDemo_Detail In arr
                            If counter > gv_raw.RowCount - 1 Then
                                gv_raw.Rows.AddNew()
                            End If
                            gv_raw.Rows(counter).Cells(colRawLineno).Value = obj1.sno
                            gv_raw.Rows(counter).Cells(colRawitemcode).Value = obj1.item_code
                            gv_raw.Rows(counter).Cells(colRawItemdesc).Value = obj1.iname
                            gv_raw.Rows(counter).Cells(colRawBOM).Value = obj1.main_bomcode
                            gv_raw.Rows(counter).Cells(colRawmainIcode).Value = obj1.main_icode
                            gv_raw.Rows(counter).Cells(colRawMainIname).Value = obj1.main_iname
                            gv_raw.Rows(counter).Cells(colRawIssueno).Value = obj1.main_issueno
                            gv_raw.Rows(counter).Cells(colRawMainSerialNo).Value = obj1.main_serial_no
                            gv_raw.Rows(counter).Cells(colRawUnit).Value = obj1.unit
                            gv_raw.Rows(counter).Cells(colRawRecvqty).Value = obj1.rec_qty
                            gv_raw.Rows(counter).Cells(colRawRemarks).Value = obj1.remarks
                            gv_raw.Rows(counter).Cells(colRawSerialNo).Value = obj1.serial_no
                            counter += 1
                        Next
                    End If
                    LblSNo.Text = "S.No. (" & gv.CurrentRow.Cells(colGVLineno).Value & ")"
                Else
                    IsBlankFilled = True
                    ShowDetail("'" + strBOM, strSrNo, strItemCode, strIssueNo)
                    LblSNo.Text = "S.No. (" & gv.CurrentRow.Cells(colGVLineno).Value & ")"
                End If
                '' End Child Items

               
               
                'gv_main.DataSource = Nothing
                'gv_main.Rows.Clear()

                'gv.CurrentRow.Tag = arr
                If Not IsBlankFilled Then
                    arrP = Nothing

                    arrP = TryCast(gv.CurrentRow.Cells(colLineno).Tag, List(Of clsfrmProductionReceiptDemo_Detail))
                    If arrP IsNot Nothing AndAlso arrP.Count > 0 Then
                        If arrP IsNot Nothing AndAlso arrP.Count > 0 Then
                            Dim counter As Integer = 0
                            For Each obj1 As clsfrmProductionReceiptDemo_Detail In arrP
                                If counter > gv_main.RowCount - 1 Then
                                    gv_main.Rows.AddNew()
                                End If
                                gv_main.Rows(counter).Cells(colLineno).Value = obj1.sno
                                gv_main.Rows(counter).Cells(colitemcode).Value = obj1.item_code
                                gv_main.Rows(counter).Cells(colItemdesc).Value = obj1.iname
                                gv_main.Rows(counter).Cells(colBOM).Value = obj1.main_bomcode
                                gv_main.Rows(counter).Cells(colmainIcode).Value = obj1.main_icode
                                gv_main.Rows(counter).Cells(colMainIname).Value = obj1.main_iname
                                gv_main.Rows(counter).Cells(colIssueno).Value = obj1.main_issueno
                                gv_main.Rows(counter).Cells(colMainSerialNo).Value = obj1.main_serial_no
                                gv_main.Rows(counter).Cells(colUnit).Value = obj1.unit
                                gv_main.Rows(counter).Cells(colRecvqty).Value = obj1.rec_qty
                                gv_main.Rows(counter).Cells(colRemarks).Value = obj1.remarks
                                gv_main.Rows(counter).Cells(colSerialNo).Value = obj1.serial_no
                                counter += 1
                            Next
                        End If
                        LblPSno.Text = "S.No. (" & gv.CurrentRow.Cells(colGVLineno).Value & ")"
                    Else
                        LblPSno.Text = "S.No. (" & gv.CurrentRow.Cells(colGVLineno).Value & ")"
                    End If
                End If

                
                '' End Princi Items
            End If
            Index = gv.CurrentRow.Index
        End If
        isInsideLoadData = False
    End Sub

 
    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv.Columns(colGVItemcode) Then
                    isCellValueChanged = True
                    'OpenMainIcode(False)
                    isCellValueChanged = False
                End If

                If e.Column Is gv.Columns(colGVSerialno) Then
                    isCellValueChanged = True
                    'OpenMainSerial(False)
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub AutoFillMain()
        Try
            Dim MainItemType As String = String.Empty

            gv.Rows.Clear()
            gv.Rows.AddNew()

            Dim arrSr As New List(Of String)()
            arrSr = New List(Of String)
            Dim qry As String = " select TSPL_MF_RECEIPT_DETAIL.ITEM_CODE as Code,tspl_item_master.item_desc as [Description],TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY as [Qty],TSPL_MF_RECEIPT_DETAIL.UNIT_CODE as [Unit],TSPL_MF_RECEIPT_DETAIL.BOM_CODE as [BOM Code],ISNULL(tspl_item_master.Item_Type,'') AS [Item Type] from TSPL_MF_RECEIPT_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_RECEIPT_DETAIL.ITEM_CODE " & _
            " where TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE='" + clsCommon.myCstr(txtRecptNo.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim XR As Integer = 0

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim qty As Decimal = clsCommon.myCdbl(dr("qty"))
                    While (qty > 0)
                        'gv.Rows.AddNew()
                        gv.Rows(XR).Cells(colGVLineno).Value = CInt(XR + 1)
                        gv.Rows(XR).Cells(colGVItemcode).Value = clsCommon.myCstr(dr("Code"))
                        gv.Rows(XR).Cells(colGVBOM).Value = clsCommon.myCstr(dr("bom code"))
                        gv.Rows(XR).Cells(colGVIname).Value = clsCommon.myCstr(dr("description"))
                        gv.Rows(XR).Cells(colGVUnit).Value = clsCommon.myCstr(dr("unit"))
                        gv.Rows(XR).Cells(ColGVIssueNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISSUE_CODE FROM TSPL_MF_ISSUE_DETAIL WHERE BOM_CODE ='" + clsCommon.myCstr(dr("bom code")) + "'"))
                        gv.Rows(XR).Cells(ColGVItemType).Value = clsCommon.myCstr(dr("Item Type"))
                        gv.Rows(XR).Cells(colGVQty).Value = 1

                        Dim whtcls As String = ""
                        If arrSr IsNot Nothing AndAlso arrSr.Count > 0 Then
                            whtcls = " and auto_sr_no not in (" + clsCommon.GetMulcallString(arrSr) + ")"
                        End If
                        gv.Rows(XR).Cells(colGVSerialno).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select auto_sr_no from tspl_serial_item where document_type='production' and document_code='" + txtRecptNo.Value + "' and item_code='" + clsCommon.myCstr(dr("code")) + "' " + whtcls + " "))

                        If Not arrSr.Contains(clsCommon.myCstr(gv.Rows(XR).Cells(colGVSerialno).Value)) Then
                            arrSr.Add(clsCommon.myCstr(gv.Rows(XR).Cells(colGVSerialno).Value))
                        End If
                        qty -= 1
                        XR += 1

                        MainItemType = clsCommon.myCstr(dr("Item Type"))

                        gv.Rows.AddNew()
                    End While
                Next
                'gv.BestFitColumns()
                gv.CurrentRow = gv.Rows(0)
                If clsCommon.CompairString(MainItemType, "S") = CompairStringResult.Equal Then

                    SplitContainer4.Panel1.Visible = False
                    SplitContainer3.SplitterDistance = 187
                    SplitContainer4.SplitterDistance = 27
                Else
                    SplitContainer4.Panel1.Visible = True
                    SplitContainer3.SplitterDistance = 128
                    SplitContainer4.SplitterDistance = 124
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenMainIcode(ByVal isButtonCLicked As Boolean)
        Dim qry As String = "select TSPL_MF_RECEIPT_DETAIL.ITEM_CODE as Code,tspl_item_master.item_desc as [Description],TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY as [Qty],TSPL_MF_RECEIPT_DETAIL.UNIT_CODE as [Unit],TSPL_MF_RECEIPT_DETAIL.BOM_CODE as [BOM Code] from TSPL_MF_RECEIPT_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_RECEIPT_DETAIL.ITEM_CODE"
        Dim icode As String = ""

        icode = clsCommon.ShowSelectForm("FINDER", qry, "Code", " TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE='" + clsCommon.myCstr(txtRecptNo.Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colGVItemcode).Value), "Code", isButtonCLicked)
        Dim XR As Integer = gv.CurrentRow.Index

        If clsCommon.myLen(icode) > 0 Then
            Dim qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select RECEIPT_QTY from TSPL_MF_RECEIPT_DETAIL where RECEIPT_CODE='" + txtRecptNo.Value + "' and item_code='" + icode + "'"))
            While (qty > 0)
                If gv.Rows.Count - 1 < XR Then
                    gv.Rows.AddNew()
                End If
                gv.Rows(XR).Cells(colGVLineno).Value = CInt(XR + 1)
                gv.Rows(XR).Cells(colGVItemcode).Value = icode
                gv.Rows(XR).Cells(colGVBOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select bom_code from TSPL_MF_RECEIPT_DETAIL where RECEIPT_CODE='" + txtRecptNo.Value + "' and item_code='" + icode + "'"))
                gv.Rows(XR).Cells(colGVIname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))
                gv.Rows(XR).Cells(colGVUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UNIT_CODE from TSPL_MF_RECEIPT_DETAIL where RECEIPT_CODE='" + txtRecptNo.Value + "' and item_code='" + icode + "'"))
                gv.Rows(XR).Cells(colGVQty).Value = 1

                qty -= 1
                XR += 1

            End While
        Else
            gv.CurrentRow.Cells(colGVBOM).Value = Nothing
            gv.CurrentRow.Cells(colGVItemcode).Value = Nothing
            gv.CurrentRow.Cells(colGVIname).Value = Nothing
            gv.CurrentRow.Cells(colGVUnit).Value = Nothing
            gv.CurrentRow.Cells(colGVQty).Value = Nothing
            gv.CurrentRow.Cells(colGVRemarks).Value = Nothing
            gv.CurrentRow.Cells(colGVSerialno).Value = Nothing
        End If
    End Sub

    Sub OpenMainSerial(ByVal isButtonCLicked As Boolean)
        Dim qry As String = "select TSPL_SERIAL_ITEM.Auto_Sr_No as [SerialNo],TSPL_SERIAL_ITEM.Item_Code as [Code],TSPL_ITEM_MASTER.Item_Desc as [Description] from TSPL_SERIAL_ITEM left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code "
        Dim icode As String = ""
        Dim whrcls As String = " TSPL_SERIAL_ITEM.Document_Code='" + txtRecptNo.Value + "' and TSPL_SERIAL_ITEM.Document_Type='Production' and TSPL_SERIAL_ITEM.Item_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colGVItemcode).Value) + "' and TSPL_SERIAL_ITEM.Location_Code='" + clsCommon.myCstr(txtLocation.Value) + "' and TSPL_SERIAL_ITEM.Auto_Sr_No not in (select Serial_No from TSPL_MF_PRINCIPLE_RECEIPT_DETAIL where Main_Item_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colGVItemcode).Value) + "' and Location_Code='" + clsCommon.myCstr(txtLocation.Value) + "' and Doc_No<>'" + txtCode.Value + "')"

        icode = clsCommon.ShowSelectForm("FINDER", qry, "SerialNo", whrcls, clsCommon.myCstr(gv.CurrentRow.Cells(colGVItemcode).Value), "SerialNo", isButtonCLicked)

        If clsCommon.myLen(icode) > 0 Then
            gv.CurrentRow.Cells(colGVSerialno).Value = icode
        Else
            gv.CurrentRow.Cells(colGVSerialno).Value = Nothing
        End If

    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colGVLineno).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub


    Private Sub gv_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv.ValueChanging
        Dim GVBOM As String = String.Empty
        Dim GVSerialNo As String = String.Empty
        Dim GVItemCode As String = String.Empty
        Dim GVIssueNo As String = String.Empty
        Dim MainBOM As String = String.Empty
        Dim MainSerialNo As String = String.Empty
        Dim MainItemCode As String = String.Empty
        Dim MainIssueNo As String = String.Empty

        'If Not isCellValueChanged AndAlso gv.CurrentColumn Is gv.Columns(ColSelect) Then
        '    If e.NewValue = True Then
        '        isCellValueChanged = True
        '        ShowDetail("'" + gv.CurrentRow.Cells(colGVBOM).Value, clsCommon.myCstr(gv.CurrentRow.Cells(colGVSerialno).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colGVItemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(ColGVIssueNo).Value))
        '        isCellValueChanged = False
        '    Else

        '    End If
        'End If
        '' Loop 
        'For Each grow As GridViewRowInfo In gv.Rows

        '    '' 05-Oct-2015
        '    If CBool(grow.Cells(ColSelect).Value) = True Then
        '        GVBOM = clsCommon.myCstr(grow.Cells(colGVBOM).Value)
        '        GVItemCode = clsCommon.myCstr(grow.Cells(colGVItemcode).Value)
        '        GVSerialNo = clsCommon.myCstr(grow.Cells(colGVSerialno).Value)
        '        GVIssueNo = clsCommon.myCstr(grow.Cells(ColGVIssueNo).Value)
        '        If clsCommon.myLen(GVBOM) > 0 Then
        '            MainBOM = MainBOM + "," + "" + MainBOM + ""
        '        End If
        '        If clsCommon.myLen(GVItemCode) > 0 Then
        '            MainItemCode = MainItemCode + "," + "" + MainItemCode + ""
        '        End If
        '        If clsCommon.myLen(GVIssueNo) > 0 Then
        '            MainIssueNo = MainIssueNo + "," + "" + MainIssueNo + ""
        '        End If
        '        If clsCommon.myLen(GVSerialNo) > 0 Then
        '            MainSerialNo = MainSerialNo + "," + "" + MainSerialNo + ""
        '        End If
        '    End If
        'Next
        'If MainBOM.Length > 0 Then
        '    If MainBOM.Substring(0, 1) = "," Then
        '        MainBOM = MainBOM.Substring(1, MainBOM.Length - 1)
        '    End If
        'End If
        'If MainItemCode.Length > 0 Then
        '    If MainItemCode.Substring(0, 1) = "," Then
        '        MainItemCode = MainItemCode.Substring(1, MainItemCode.Length - 1)
        '    End If
        'End If
        'If MainIssueNo.Length > 0 Then
        '    If MainIssueNo.Substring(0, 1) = "," Then
        '        MainIssueNo = MainIssueNo.Substring(1, MainIssueNo.Length - 1)
        '    End If
        'End If
        'If MainSerialNo.Length > 0 Then
        '    If MainSerialNo.Substring(0, 1) = "," Then
        '        MainSerialNo = MainSerialNo.Substring(1, MainSerialNo.Length - 1)
        '    End If
        'End If
    End Sub

    Private Sub BtnTag_Click(sender As Object, e As EventArgs)
        ''
        Dim arr As List(Of clsfrmProductionReceiptDemo_Detail) = New List(Of clsfrmProductionReceiptDemo_Detail)
        Dim obj As New clsfrmProductionReceiptDemo_Detail()
        If Index >= 0 Then
            For Each grow As GridViewRowInfo In gv_raw.Rows

                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colRawBOM).Value)) > 0 Then
                    obj.main_bomcode = clsCommon.myCstr(grow.Cells(colRawBOM).Value)
                    obj.item_code = clsCommon.myCstr(grow.Cells(colRawitemcode).Value)
                    obj.iname = clsCommon.myCstr(grow.Cells(colRawItemdesc).Value)
                    obj.main_icode = clsCommon.myCstr(grow.Cells(colRawmainIcode).Value)
                    obj.main_iname = clsCommon.myCstr(grow.Cells(colRawMainIname).Value)
                    obj.main_issueno = clsCommon.myCstr(grow.Cells(colRawIssueno).Value)
                    obj.main_serial_no = clsCommon.myCstr(grow.Cells(colRawMainSerialNo).Value)
                    obj.unit = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                    obj.serial_no = clsCommon.myCstr(grow.Cells(colRawSerialNo).Value)
                    obj.sno = clsCommon.myCstr(grow.Cells(colRawLineno).Value)
                    obj.rec_qty = clsCommon.myCstr(grow.Cells(colRawRecvqty).Value)
                    obj.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    arr.Add(obj)
                End If
            Next
            gv.Rows(Index).Tag = arr
        End If
        gv_raw.DataSource = Nothing
        gv_raw.Rows.Clear()

        'gv.CurrentRow.Tag = arr
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            arr = TryCast(gv.Rows(Index).Tag, List(Of clsfrmProductionReceiptDemo_Detail))
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim counter As Integer = 0
                For Each obj1 As clsfrmProductionReceiptDemo_Detail In arr
                    If counter > gv_raw.RowCount - 1 Then
                        gv_raw.Rows.AddNew()
                    End If
                    gv_raw.Rows(counter).Cells(colRawLineno).Value = obj1.sno
                    gv_raw.Rows(counter).Cells(colRawitemcode).Value = obj1.item_code
                    gv_raw.Rows(counter).Cells(colRawItemdesc).Value = obj1.iname
                    gv_raw.Rows(counter).Cells(colRawBOM).Value = obj1.main_bomcode
                    gv_raw.Rows(counter).Cells(colRawmainIcode).Value = obj1.main_icode
                    gv_raw.Rows(counter).Cells(colRawItemdesc).Value = obj1.main_iname
                    gv_raw.Rows(counter).Cells(colRawIssueno).Value = obj1.main_issueno
                    gv_raw.Rows(counter).Cells(colRawMainSerialNo).Value = obj1.main_serial_no
                    gv_raw.Rows(counter).Cells(colRawUnit).Value = obj1.unit
                    gv_raw.Rows(counter).Cells(colRawRecvqty).Value = obj1.rec_qty
                    gv_raw.Rows(counter).Cells(colRawRemarks).Value = obj1.remarks
                    counter += 1
                Next
            End If

        End If
        ''
    End Sub

  
End Class
