Imports common
Imports System.Data.SqlClient

Public Class frmMCCWiseVehicleAndFreightChargesMapping
    Inherits Telerik.WinControls.UI.RadForm


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Public Const colTrankerNo As String = "colTrankerNo"
    Public Const colTrankerDesc As String = "colTrankerDesc"
    Public Const colFreightChargesCode As String = "colFreightChargesCode"
    Public Const colFreightChargesDesc As String = "colFreightChargesDesc"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Public strMCCCode As String = ""
#End Region

    Private Sub frmMCCWiseVehicleAndFreightChargesMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsCommon.myLen(strMCCCode) <= 0 Then
            Throw New Exception("Please provide MCC Code")
        End If
        gv.Enabled = True
        MyLabel1.Text = strMCCCode
        txtmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from TSPL_MCC_MASTER where MCC_Code='" + strMCCCode + "'"))
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        LoadBlankGrid()
        LoadData()

    End Sub

    Sub LoadData()
        Try
            Dim arr As List(Of clsMCCVehicleFreightChargesMapping) = clsMCCVehicleFreightChargesMapping.GetData(strMCCCode)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsMCCVehicleFreightChargesMapping In arr
                    gv.Rows.AddNew()
                    gv.Rows(gv.RowCount - 1).Cells(colFreightChargesCode).Value = obj.Freight_Code
                    gv.Rows(gv.RowCount - 1).Cells(colFreightChargesDesc).Value = obj.Freight_Name
                    gv.Rows(gv.RowCount - 1).Cells(colTrankerNo).Value = obj.Tanker_No
                    gv.Rows(gv.RowCount - 1).Cells(colTrankerDesc).Value = obj.Tanker_Name
                Next
            Else
                gv.Rows.AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGrid()
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()

            Dim repoTxtBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTxtBox.FormatString = ""
            repoTxtBox.HeaderText = "Tanker No"
            repoTxtBox.Name = colTrankerNo
            repoTxtBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTxtBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTxtBox.Width = 150
            repoTxtBox.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoTxtBox)

            repoTxtBox = New GridViewTextBoxColumn()
            repoTxtBox.FormatString = ""
            repoTxtBox.HeaderText = "Tanker Description"
            repoTxtBox.Name = colTrankerDesc
            repoTxtBox.Width = 300
            repoTxtBox.ReadOnly = True
            gv.MasterTemplate.Columns.Add(repoTxtBox)


            repoTxtBox = New GridViewTextBoxColumn()
            repoTxtBox.FormatString = ""
            repoTxtBox.HeaderText = "Freight Charges Code"
            repoTxtBox.Name = colFreightChargesCode
            repoTxtBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTxtBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTxtBox.Width = 150
            repoTxtBox.ReadOnly = False
            gv.MasterTemplate.Columns.Add(repoTxtBox)

            repoTxtBox = New GridViewTextBoxColumn()
            repoTxtBox.FormatString = ""
            repoTxtBox.HeaderText = "Freight Charges Description"
            repoTxtBox.Name = colFreightChargesDesc
            repoTxtBox.Width = 300
            repoTxtBox.ReadOnly = True
            gv.MasterTemplate.Columns.Add(repoTxtBox)

            gv.AllowAddNewRow = False
            gv.ShowGroupPanel = False
            gv.AllowColumnReorder = True
            gv.AllowRowReorder = False
            gv.EnableSorting = False
            gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv.MasterTemplate.ShowRowHeaderColumn = False
            gv.TableElement.TableHeaderHeight = 40
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    Dim qry As String
                    If e.Column Is gv.Columns(colTrankerNo) Then
                        qry = "select Tanker_No,Tanker_Name,Tanker_Transporter_Code,Description,Storage_Capacity,Year from TSPL_TANKER_MASTER "
                        gv.CurrentRow.Cells(colTrankerNo).Value = clsCommon.ShowSelectForm("MCCVTTF", qry, "Tanker_No", "", clsCommon.myCstr(gv.CurrentRow.Cells(colTrankerNo).Value), "", False)
                        gv.CurrentRow.Cells(colTrankerDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Tanker_Name) from TSPL_TANKER_MASTER where Tanker_No='" + clsCommon.myCstr(gv.CurrentRow.Cells(colTrankerNo).Value) + "'"))
                    ElseIf e.Column Is gv.Columns(colFreightChargesCode) Then
                        qry = "select Freight_Code,Freight_Description from TSPL_FREIGHT_CHARGES_MASTER "
                        gv.CurrentRow.Cells(colFreightChargesCode).Value = clsCommon.ShowSelectForm("MCCVTTF", qry, "Freight_Code", "", clsCommon.myCstr(gv.CurrentRow.Cells(colFreightChargesCode).Value), "", False)
                        gv.CurrentRow.Cells(colFreightChargesDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Freight_Description) from TSPL_FREIGHT_CHARGES_MASTER where Freight_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colFreightChargesCode).Value) + "'"))
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMCCWiseVehicleAndFreightChargesMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S Then ''AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D Then ''AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            btnclose.PerformClick()
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Function AllowToSave() As Boolean
        Return True
    End Function

    Sub SaveData()
        Dim arr As New List(Of clsMCCVehicleFreightChargesMapping)
        For ii As Integer = 0 To gv.RowCount - 1
            Dim obj As New clsMCCVehicleFreightChargesMapping
            obj.Tanker_No = clsCommon.myCstr(gv.Rows(ii).Cells(colTrankerNo).Value)
            obj.Freight_Code = clsCommon.myCstr(gv.Rows(ii).Cells(colFreightChargesCode).Value)
            If clsCommon.myLen(obj.Tanker_No) > 0 AndAlso clsCommon.myLen(obj.Freight_Code) > 0 Then
                arr.Add(obj)
            End If
        Next
        If arr Is Nothing OrElse arr.Count <= 0 Then
            Throw New Exception("No data found to save")
        End If
        clsMCCVehicleFreightChargesMapping.SaveData(strMCCCode, arr)
        clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
        btnclose.PerformClick()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv.UserDeletedRow
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.MyMessageBoxShow("Delete The Current Mapping." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                clsMCCVehicleFreightChargesMapping.DeleteData(strMCCCode, Nothing)
                clsCommon.MyMessageBoxShow(Me, "Data deleted successfully", Me.Text)
                btnclose.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Dim qry As String = "select count(*) from TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                qry = "select MCC_Code,Freight_Code,Tanker_No  from TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING  "
            Else
                qry = "select '' as MCC_Code,'' as Freight_Code'' as Tanker_No "
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "MCC_Code", "Freight_Code", "Tanker_No") Then
            Dim counter As Integer = 1
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim Coll As New Dictionary(Of String, List(Of clsMCCVehicleFreightChargesMapping))
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strMCC As String = clsCommon.myCstr(grow.Cells("MCC_Code").Value)
                    If clsCommon.myLen(strMCC) > 0 Then
                        strMCC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_MASTER where MCC_Code='" + strMCC + "'", trans))
                        If clsCommon.myLen(strMCC) <= 0 Then
                            Throw New Exception("Not a Valid MCC Code")
                        End If
                        If Not Coll.ContainsKey(strMCC) Then
                            Coll.Add(strMCC, New List(Of clsMCCVehicleFreightChargesMapping))
                        End If
                        Dim obj As New clsMCCVehicleFreightChargesMapping
                        obj.MCC_Code = strMCC
                        obj.Freight_Code = clsCommon.myCstr(grow.Cells("Freight_Code").Value)
                        obj.Freight_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Freight_Code  from TSPL_FREIGHT_CHARGES_MASTER where Freight_Code='" + obj.Freight_Code + "'", trans))
                        If clsCommon.myLen(obj.Freight_Code) <= 0 Then
                            Throw New Exception("Not a Valid Freight Code")
                        End If
                        obj.Tanker_No = clsCommon.myCstr(grow.Cells("Tanker_No").Value)
                        obj.Tanker_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_No from TSPL_TANKER_MASTER where Tanker_No='" + obj.Tanker_No + "'", trans))
                        If clsCommon.myLen(obj.Tanker_No) <= 0 Then
                            Throw New Exception("Not a Valid Tanker Code")
                        End If
                        Coll(strMCC).Add(obj)
                    End If
                    counter += 1
                Next
                For Each key As String In Coll.Keys
                    clsMCCVehicleFreightChargesMapping.SaveData(key, Coll(key), trans)
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Error at row no " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
End Class