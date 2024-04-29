'--preeti gupta-ticket no.[BM00000003133]
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common
Public Class TDMwiseTarget

    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colFlavour As String = "Flavour"
    Const coldesc As String = "Flavour Desc"
    Const colQTY As String = "QTY"
   

   

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsTDMWiseTarget()
                obj.Employee_Code = fndEmployee.Value
                obj.Employee_desc = txtdesc.Text
                obj.TargetQty = txtTargetQty.Value
                If chkFlavour.Checked Then
                    obj.FlavourWise = "Y"
                Else
                    obj.FlavourWise = "N"
                End If

                Dim Arr As New List(Of clsTDMWiseTarget)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsTDMWiseTarget()
                    objTr.Flavour = clsCommon.myCstr(grow.Cells(colFlavour).Value)
                    objTr.Flavour_desc = clsCommon.myCstr(grow.Cells(coldesc).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQTY).Value)
                   
                    If (clsCommon.myLen(objTr.Flavour) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next

                If chkFlavour.Checked = True Then
                    If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                        Return
                    End If
                End If
                'Dim objHist As New clsTDMWiseTargetHistory()
                'objHist.SaveDataHistory(fndvendor.Value)

                If (obj.SaveData(fndEmployee.Value, txtdesc.Text, txtTargetQty.Value, obj.FlavourWise, txtFromDate.Value, Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    'LoadData(obj.vendor_code, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(fndEmployee.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Employee ", Me.Text)
                fndEmployee.Focus()
                Return False
            End If

            If txtTargetQty.Text = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Please fill TargetQty", Me.Text)
                Return False
            End If


            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strFlavour As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colFlavour).Value)
                Dim strdesc As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(coldesc).Value)
                Dim strIQTY As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colQTY).Value)

                For jj As Integer = 0 To dgvitem.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strFlavour, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colFlavour).Value)) = CompairStringResult.Equal) Then
                        If (clsCommon.CompairString(strIQTY, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colQTY).Value)) = CompairStringResult.Equal) Then
                            common.clsCommon.MyMessageBoxShow("Already selected Item " + strFlavour.Trim() + "( " + strdesc + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                            Return False
                        End If
                    End If
                Next

                If Not arrICode.Contains(strFlavour) Then
                    arrICode.Add(strFlavour)
                End If
            Next
            Dim Total As Double = Nothing
            For jj As Integer = 0 To dgvitem.Rows.Count - 1
                If Not (dgvitem.Rows(jj).Cells(colQTY).Value = Nothing) Then
                    Total += clsCommon.myCdbl(dgvitem.Rows(jj).Cells(colQTY).Value)
                End If
            Next
            If chkFlavour.Checked = True Then
                If Not Total = txtTargetQty.Value Then
                    common.clsCommon.MyMessageBoxShow(Me, "Target Qty should be equal to Flavours Qty", Me.Text)
                    Return False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub LoadData(ByVal vendorcode As String, ByVal Desc As String)
        Try

            chkFlavour.Checked = False

            LoadBlankGrid()
            btnsave.Enabled = True


            isInsideLoadData = True

            'btnsave.Text = "Update"

            'funreset()


            Dim Arr As List(Of clsTDMWiseTarget) = clsTDMWiseTarget.GetData(vendorcode, txtFromDate.Value)
            'fndvendor.Value = vendorcode
            txtdesc.Text = Desc

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsTDMWiseTarget In Arr
                    dgvitem.Rows.AddNew()
                    If clsCommon.myLen(objTr.Flavour) > 0 Then

                        chkFlavour.Checked = True
                        dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colFlavour).Value = objTr.Flavour
                    End If
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coldesc).Value = objTr.Flavour_desc
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colQTY).Value = objTr.Qty


                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

 

    Private Sub btnsave_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Public Sub funreset()
        fndEmployee.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
        txtTargetQty.Value = Nothing
        chkFlavour.Checked = False
        txtFromDate.Value = clsCommon.GETSERVERDATE()

        'btndelete.Enabled = False
    End Sub
    Sub BlankAllControls()
        txtTargetQty.Value = Nothing
        chkFlavour.Checked = False

        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"

    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click, RadButton1.Click
        Me.Close()
    End Sub

    Private Sub TDMwiseTarget_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclear.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub dgvitem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvitem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If

    End Sub
    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()

        Dim Flavour As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Flavour.FormatString = ""
        Flavour.HeaderText = "Flavour"
        Flavour.Name = colFlavour
        Flavour.Width = 90
        Flavour.ReadOnly = False
        Flavour.TextImageRelation = TextImageRelation.TextBeforeImage
        Flavour.HeaderImage = Global.XpertERPSalesAndDistribution.My.Resources.Resources.search4
        Flavour.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(Flavour)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        item_desc.HeaderText = "Description"
        item_desc.Name = coldesc
        item_desc.Width = 200
        item_desc.ReadOnly = True
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_desc)

        Dim QTY As GridViewDecimalColumn = New GridViewDecimalColumn()
        QTY.FormatString = ""
        QTY.HeaderText = "QTY"
        QTY.Name = colQTY
        QTY.Width = 70
        QTY.ReadOnly = False
        QTY.TextImageRelation = TextImageRelation.TextBeforeImage
        QTY.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(QTY)



        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = True
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TDMTARGET)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        ' btnPost.Visible = MyBase.isPostFlag

    End Sub

    Private Sub TDMwiseTarget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadBlankGrid()
        funreset()
        btnsave.Enabled = True
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim Qry As String = "select distinct  Class_Code as Code,Class_Desc as Description from TSPL_ITEM_DETAILS   "
        dgvitem.CurrentRow.Cells(colFlavour).Value = clsCommon.ShowSelectForm("EMPITMFND", Qry, "Code", "Class_Name ='flavour'", dgvitem.CurrentRow.Cells(colFlavour).Value, "Code", isButtonClick)
        dgvitem.CurrentRow.Cells(coldesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Class_Desc from TSPL_ITEM_DETAILS where Class_Code='" + clsCommon.myCstr(dgvitem.CurrentRow.Cells(colFlavour).Value) + "'"))


    End Sub

    Private Sub dgvitem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try

            If isInsideLoadData = False Then

                If e.Column Is dgvitem.Columns(colFlavour) Then
                    OpenICodeList(False)
                End If
            End If
            'txtTargetQty.Value = Nothing
            'For jj As Integer = 0 To dgvitem.Rows.Count - 1
            '    If Not (dgvitem.Rows(jj).Cells(colQTY).Value = Nothing) Then
            '        txtTargetQty.Value += clsCommon.myCdbl(dgvitem.Rows(jj).Cells(colQTY).Value)
            '    End If
            'Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Private Sub fndEmployee__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndEmployee._MYValidating
        Dim qry As String = "select EMP_CODE as [EmployeeCode] ,Emp_Name as [Employee Name]  from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = " Emp_Status='Active'"
        'fndEmployee.Value = clsCommon.ShowSelectForm("EMPMasFND", qry, "EmployeeCode", whrcls, fndEmployee.Value, "", isButtonClicked)
        fndEmployee.Value = clsCommon.ShowSelectForm("EMPB", qry, "EmployeeCode", whrcls, fndEmployee.Value, "", isButtonClicked)
        txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndEmployee.Value + "'"))
        txtTargetQty.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TargetQty from TSPL_TDMWISE_TARGET_DETAIL WHERE Employee_Code='" + fndEmployee.Value + "'"))

        LoadData(fndEmployee.Value, txtdesc.Text)
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub



    Private Sub fndID_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
