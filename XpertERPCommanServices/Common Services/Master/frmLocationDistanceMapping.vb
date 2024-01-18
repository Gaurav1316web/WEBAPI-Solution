Imports common
Imports System.Data.SqlClient

Public Class frmLocationDistanceMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isFromApprovalForm As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colCustCode As String = "colCustCode"
    Const colCustdesc As String = "colCustdesc"
    Const ColDistance As String = "ColDistance"

#End Region

    Private Sub FrmLocationDistanceMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        btndelete.Enabled = True
        btnsave.Enabled = True
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D to Delete")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")


        If isFromApprovalForm Then
            Me.Text = "Approval Customer Item Details"
        End If
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsLocationDistanceMapping()
                obj.Location_Code = fndCustomer.Value
                obj.comp_code = objCommonVar.CurrentCompanyCode
                Dim Arr As New List(Of clsLocationDistanceMapping)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsLocationDistanceMapping()
                    objTr.Customer_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                    objTr.Distance = clsCommon.myCstr(grow.Cells(ColDistance).Value)
                    objTr.TransType = IIf(rdbSale.IsChecked, "S", "T")
                    If (clsCommon.myLen(objTr.Customer_Code) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next

                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at least one Customer Detail", Me.Text)
                    Return
                End If
                If (obj.SaveData(fndCustomer.Value, txtdesc.Text, IIf(rdbSale.IsChecked, "S", "T"), objCommonVar.CurrentCompanyCode, Arr)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer No", Me.Text)
                fndCustomer.Focus()
                Return False
            End If

            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strCustCode As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colCustCode).Value)
                Dim strCustName As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colCustdesc).Value)
                Dim dblDistance As Double = clsCommon.myCdbl(dgvitem.Rows(ii).Cells(ColDistance).Value)
                If dblDistance = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Enter Distance For Customer '" + strCustCode + "'")
                    Return False
                End If
                For jj As Integer = 0 To dgvitem.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strCustCode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colCustCode).Value)) = CompairStringResult.Equal) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Already selected Customer " + strCustCode.Trim() + "( " + strCustName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If
                Next
                If Not arrICode.Contains(strCustCode) Then
                    arrICode.Add(strCustCode)
                End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub LoadData(ByVal LocationCode As String)
        Try
            Dim strTransType = IIf(rdbSale.IsChecked, "S", "T")
            btnsave.Enabled = True
            btndelete.Enabled = True
            isInsideLoadData = True
            txtdesc.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where location_code='" + LocationCode + "'")
            LoadBlankGrid()
            Dim Arr As List(Of clsLocationDistanceMapping) = clsLocationDistanceMapping.GetData(LocationCode, strTransType)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsLocationDistanceMapping In Arr
                    dgvitem.Rows.AddNew()
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colCustCode).Value = objTr.Customer_Code
                    If rdbSale.IsChecked Then
                        dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colCustdesc).Value = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER Where Cust_Code='" + objTr.Customer_Code + "'")
                    Else
                        dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colCustdesc).Value = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where location_code='" + objTr.Customer_Code + "'")
                    End If
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(ColDistance).Value = objTr.Distance
                    If clsCommon.CompairString(objTr.TransType, "T") = CompairStringResult.Equal Then
                        rdbTransfer.IsChecked = True
                    Else
                        rdbSale.IsChecked = True
                    End If
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Public Sub funreset()
        fndCustomer.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
        rdbSale.IsChecked = True
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        delete()
    End Sub

    Public Sub delete()
        Try
            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Location found to delete.", Me.Text)
                fndCustomer.Focus()
            Else
                If clsLocationDistanceMapping.DeleteData(fndCustomer.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.", Me.Text)
                    LoadData(fndCustomer.Value)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndvendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER"
        fndCustomer.Value = clsCommon.ShowSelectForm("Route1Location", qry, "Code", "ISNULL(GIT_Type,'N')<>'Y'", fndCustomer.Value, "Code", isButtonClicked)
        txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndCustomer.Value + "'"))
        LoadData(fndCustomer.Value)
    End Sub

    Private Sub dgvitem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is dgvitem.Columns(colCustCode) Then
                        OpenCustCodeList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Sub OpenCustCodeList(ByVal isButtonClick As Boolean)
        Dim Qry As String = ""
        Dim WhrCls As String = ""
        If rdbSale.IsChecked Then
            Qry = "select Cust_Code as [Code],Customer_Name as [Name],Cust_Group_Code as [Group Code],(case when isnull(Status,'N')<>'Y' then 'Active' when isnull(Status,'N') ='Y' then 'In-Active' end) as [Status] from TSPL_CUSTOMER_MASTER "
            WhrCls = " isnull(Status,'N') <>'Y' AND isnull(OnHold,'N')<>'Y' "

            dgvitem.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("Route1CustFinder", Qry, "Code", WhrCls, dgvitem.CurrentRow.Cells(colCustCode).Value, "Code", isButtonClick)

            If clsCommon.myLen(dgvitem.CurrentRow.Cells(colCustCode).Value) > 0 Then
                dgvitem.CurrentRow.Cells(colCustdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & dgvitem.CurrentRow.Cells(colCustCode).Value & "'"))
            Else
                dgvitem.CurrentRow.Cells(colCustdesc).Value = ""
                dgvitem.CurrentRow.Cells(ColDistance).Value = 0
            End If

        Else
            Qry = "select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER"
            WhrCls = "ISNULL(GIT_Type,'N')<>'Y'"

            dgvitem.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("Route1CustFinder", Qry, "Code", WhrCls, dgvitem.CurrentRow.Cells(colCustCode).Value, "Code", isButtonClick)

            If clsCommon.myLen(dgvitem.CurrentRow.Cells(colCustCode).Value) > 0 Then
                dgvitem.CurrentRow.Cells(colCustdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + dgvitem.CurrentRow.Cells(colCustCode).Value + "'"))
            Else
                dgvitem.CurrentRow.Cells(colCustdesc).Value = ""
                dgvitem.CurrentRow.Cells(ColDistance).Value = 0
            End If

        End If


    End Sub

    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()

        Dim item_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        If rdbSale.IsChecked Then
            item_code.HeaderText = "Customer"
        Else
            item_code.HeaderText = "Location"
        End If
        item_code.Name = colCustCode
        item_code.Width = 90
        item_code.ReadOnly = False
        item_code.TextImageRelation = TextImageRelation.TextBeforeImage
        item_code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        item_code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_code)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        If rdbSale.IsChecked Then
            item_desc.HeaderText = "Customer Desc"
        Else
            item_desc.HeaderText = "Location Desc"
        End If

        item_desc.Name = colCustdesc
        item_desc.Width = 200
        item_desc.ReadOnly = True
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_desc)

        Dim itemrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemrate.FormatString = ""
        itemrate.HeaderText = "Distance"
        itemrate.Name = ColDistance
        itemrate.Width = 70
        itemrate.ReadOnly = False
        itemrate.ShowUpDownButtons = False
        itemrate.Step = 0
        dgvitem.MasterTemplate.Columns.Add(itemrate)

        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = Not isFromApprovalForm
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.frmLocationDistanceMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        ' btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Sub BlankAllControls()
        fndCustomer.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub Export_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select TransType,Location_Code as [Location Code],Customer_Code as [Customer Code],Distance   " &
        "from TSPL_LOCATION_DISTANCE_MAPPING  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub Import_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "TransType", "Location Code", "Customer Code", "Distance") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strTransType As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.CompairString(strTransType, "S") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(strTransType, "T") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("TransType Type has some incorrect values.You must Enter S or T ")
                    End If
                    Dim Locacode As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If Locacode.Length > 12 Then
                        Throw New Exception("Check the length of 'Location Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim CountLocCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code='" + Locacode + "'", trans)
                    If CountLocCode = 0 OrElse Locacode Is Nothing Then
                        Throw New Exception("This  '" + Locacode + "'  Item does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim Custcode As String = ""
                    If clsCommon.CompairString(strTransType, "S") = CompairStringResult.Equal Then
                        Custcode = clsCommon.myCstr(grow.Cells(2).Value)
                        If Custcode.Length > 12 Then
                            Throw New Exception("Check the length of 'Cust Code'.")
                            trans.Rollback()
                            Exit Sub
                        End If
                        Dim CountCustCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
                        If CountCustCode = 0 OrElse Custcode Is Nothing Then
                            Throw New Exception("This  '" + Custcode + "'  Customer does not exist")
                            trans.Rollback()
                            Exit Sub
                        End If
                    Else
                        Custcode = clsCommon.myCstr(grow.Cells(2).Value)
                        If Custcode.Length > 12 Then
                            Throw New Exception("Check the length of 'TO Location Code'.")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim CountToLocCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code='" + Custcode + "'", trans)
                        If CountToLocCode = 0 OrElse Custcode Is Nothing Then
                            Throw New Exception("This  '" + Custcode + "'  Location does not exist")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If





                    Dim Distance As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If Not Distance.Length < 18 And IsNumeric(Distance) Then
                        Throw New Exception("Check the value of 'Distance'.")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim sql1 As String = "select count(*) from TSPL_LOCATION_DISTANCE_MAPPING where Customer_Code='" + Custcode + "' and Location_Code='" + Locacode + "' and transtype='" & strTransType & "' "
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    If i > 0 Then
                        If clsCommon.CompairString(strTransType, "S") = CompairStringResult.Equal Then
                            Throw New Exception("Already selected Location " + Locacode + "  and Customer " + Custcode.Trim() + " ")
                        Else
                            Throw New Exception("Already selected Location " + Locacode + "  and To Location " + Custcode.Trim() + " ")
                        End If
                        trans.Rollback()
                        Exit Sub
                    End If

                    If (i = 0) Then
                        Dim qry As String = "insert into TSPL_LOCATION_DISTANCE_MAPPING( Customer_Code ,Location_Code  ,Distance ,Comp_code,Transtype ) values('" + Convert.ToString(Custcode) + "','" + Convert.ToString(Locacode) + "','" + Convert.ToString(Distance) + "','" + Convert.ToString(objCommonVar.CurrentCompanyCode) + "' ,'" + Convert.ToString(strTransType) + "' )"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        Dim qry As String = "update TSPL_LOCATION_DISTANCE_MAPPING set Distance= '" + Convert.ToString(Distance) + "'  ,comp_code='" + Convert.ToString(objCommonVar.CurrentCompanyCode) + "',transtype='" + Convert.ToString(strTransType) + "'  where Customer_Code= '" + Convert.ToString(Custcode) + "' and Location_Code='" + Convert.ToString(Locacode) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmLocationDistanceMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            delete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclear.Enabled Then
            Me.Close()
        End If
    End Sub


    Private Sub dgvitem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvitem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub


    Private Sub dgvitem_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles dgvitem.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                'If (e.Column Is dgvitem.Columns(colApprovalRate)) Then
                '    dgvitem.CurrentRow.Cells(colApprovalRate).ReadOnly = Not isFromApprovalForm

                'End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbSale_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSale.ToggleStateChanged
        fndCustomer.Value = ""
        LoadBlankGrid()
    End Sub

    Private Sub rdbTransfer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbTransfer.ToggleStateChanged
        fndCustomer.Value = ""
        LoadBlankGrid()
    End Sub


End Class

