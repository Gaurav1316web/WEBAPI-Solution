'-Created By--[Pankaj Kumar Chaudhary]--Against Ticket No-[BM00000002119]
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class FrmSecondaryCustomerSale
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim IsFormLoad As Boolean = False

    Private Sub FrmSecondaryCustomerSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsFormLoad = True
        SetUserMgmtNew()
        LoadYears()
        LoadMonths()
        Reset()
        IsFormLoad = False
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Delete the record")

    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmSecondaryCustomerSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '--preeti gupta--ticket no[BM00000003180]
        btnsave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If btnsave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCustomer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer")
            txtCustomer.Focus()
            Return False
        End If
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim Arr As New List(Of clsSecondaryCustomerSale)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim obj As New clsSecondaryCustomerSale()
                    obj.Cust_Code = clsCommon.myCstr(txtCustomer.Value)
                    obj.Year = clsCommon.myCstr(ddlYear.SelectedValue)
                    obj.Month = clsCommon.myCstr(ddlMonth.SelectedValue)
                    obj.Pack = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myCdbl(grow.Cells("Sale").Value) > 0 Then
                        obj.Sale = clsCommon.myCdbl(grow.Cells("Sale").Value)
                    End If
                    Arr.Add(obj)
                Next
                If (clsSecondaryCustomerSale.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(clsCommon.myCstr(txtCustomer.Value), clsCommon.myCstr(ddlYear.SelectedValue), clsCommon.myCstr(ddlMonth.SelectedValue))
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        qry = "Select TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code as [Code], TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name as [Name]," & _
            " TSPL_SECONDARY_CUSTOMER_MASTER.Distributor+' - '+TSPL_CUSTOMER_MASTER.Customer_Name as [Distributor] from TSPL_SECONDARY_CUSTOMER_MASTER" & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SECONDARY_CUSTOMER_MASTER.Distributor"
        txtCustomer.Value = clsCommon.ShowSelectForm("cust@secSale", qry, "Code", "TSPL_SECONDARY_CUSTOMER_MASTER.Status ='Active'", txtCustomer.Value, "Code", isButtonClicked)
        lblCustomerName.Text = clsSecondaryCustomer.getCustomerName(txtCustomer.Value, Nothing)
        lblDistributor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Code+' - '+ Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code=(Select Distributor from TSPL_SECONDARY_CUSTOMER_MASTER WHERE Cust_Code='" + txtCustomer.Value + "')"))
        LoadData(clsCommon.myCstr(txtCustomer.Value), clsCommon.myCstr(ddlYear.SelectedValue), clsCommon.myCstr(ddlMonth.SelectedValue))
    End Sub

    Sub LoadData(ByVal strCustCode As String, ByVal Year As String, ByVal Month As String)
        Try
            If clsCommon.myLen(strCustCode) > 0 And Year > 0 And Month > 0 Then
                dt = clsSecondaryCustomerSale.GetData(strCustCode, Year, Month)
                gv1.DataSource = dt
                If gv1.Rows.Count > 0 Then
                    FormatGrid()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        gv1.Columns("Code").HeaderText = "Pack Type"
        gv1.Columns("Code").Width = 100
        gv1.Columns("Code").ReadOnly = True

        gv1.Columns("Description").HeaderText = "Pack Type Description"
        gv1.Columns("Description").Width = 250
        gv1.Columns("Description").ReadOnly = True

        gv1.Columns("Sale").HeaderText = "Sale"
        gv1.Columns("Sale").Width = 150
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        txtCustomer.Value = ""
        lblCustomerName.Text = ""
        lblDistributor.Text = ""
        ddlYear.SelectedValue = CurrentDate.Year
        ddlMonth.SelectedValue = CurrentDate.Month
        gv1.DataSource = Nothing
    End Sub

    Private Sub LoadYears()
        Dim dtyear As New DataTable
        dtyear.Columns.Add("Year", GetType(Integer))
        For year As Integer = 2011 To 2030
            dtyear.Rows.Add(year)
        Next
        ddlYear.DataSource = dtyear
        ddlYear.DisplayMember = "Year"
        ddlYear.ValueMember = "Year"
    End Sub

    Private Sub LoadMonths()
        Dim dtmonth As New DataTable
        dtmonth.Columns.Add("Month", GetType(Integer))
        dtmonth.Columns.Add("MonthName", GetType(String))
        dtmonth.Rows.Add(1, "Janaury")
        dtmonth.Rows.Add(2, "February")
        dtmonth.Rows.Add(3, "March")
        dtmonth.Rows.Add(4, "April")
        dtmonth.Rows.Add(5, "May")
        dtmonth.Rows.Add(6, "June")
        dtmonth.Rows.Add(7, "July")
        dtmonth.Rows.Add(8, "August")
        dtmonth.Rows.Add(9, "September")
        dtmonth.Rows.Add(10, "October")
        dtmonth.Rows.Add(11, "November")
        dtmonth.Rows.Add(12, "December")
        ddlMonth.DataSource = dtmonth
        ddlMonth.DisplayMember = "MonthName"
        ddlMonth.ValueMember = "Month"
    End Sub

    Private Sub frmItemReorderLevel1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData(txtCustomer.Value, clsCommon.myCstr(ddlYear.SelectedValue), clsCommon.myCstr(ddlMonth.SelectedValue))
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(txtCustomer.Value, clsCommon.myCstr(ddlYear.SelectedValue), clsCommon.myCstr(ddlMonth.SelectedValue))
    End Sub

    Private Sub DeleteData(ByVal strCustCode As String, ByVal Year As String, ByVal Month As String)
        Try
            If clsSecondaryCustomerSale.DeleteData(strCustCode, Year, Month) Then
                clsCommon.MyMessageBoxShow("Data deleted successfully.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ddlYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlYear.SelectedIndexChanged
        If Not IsFormLoad Then
            LoadData(clsCommon.myCstr(txtCustomer.Value), clsCommon.myCstr(ddlYear.SelectedValue), clsCommon.myCstr(ddlMonth.SelectedValue))
        End If
    End Sub

    Private Sub ddlMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlMonth.SelectedIndexChanged
        If Not IsFormLoad Then
            LoadData(clsCommon.myCstr(txtCustomer.Value), clsCommon.myCstr(ddlYear.SelectedValue), clsCommon.myCstr(ddlMonth.SelectedValue))
        End If
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Try
            qry = "Select TSPL_SECONDARY_CUSTOMER_SALE.Cust_Code as [Customer Code], TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name as [Customer Name]," & _
                " TSPL_SECONDARY_CUSTOMER_SALE.Year, TSPL_SECONDARY_CUSTOMER_SALE.Month, TSPL_SECONDARY_CUSTOMER_SALE.Pack as [Pack Type]," & _
                " TSPL_INV_CLASS_DETAILS.Inv_Class_Desc as [Pack Type Description], TSPL_SECONDARY_CUSTOMER_SALE.Sale from TSPL_SECONDARY_CUSTOMER_SALE" & _
                " LEFT OUTER JOIN TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_SECONDARY_CUSTOMER_SALE.Cust_Code=TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code" & _
                " LEFT OUTER JOIN TSPL_INV_CLASS_DETAILS ON TSPL_INV_CLASS_DETAILS.Inv_Class_Code=TSPL_SECONDARY_CUSTOMER_SALE.Pack" & _
                " Order By TSPL_SECONDARY_CUSTOMER_SALE.Cust_Code, TSPL_SECONDARY_CUSTOMER_SALE.Pack"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                qry = "Select '' As [Customer Code], '' as [Customer Name], 0 As Year, 0 as Month, '' as [Pack Type], '' as [Pack Type Description], 0 as Sale"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Try
            ImportItems()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ImportItems()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Year", "Month", "Pack Type", "Pack Type Description", "Sale") Then
            clsCommon.ProgressBarShow()
            Try
                Dim Cust_Code As String
                Dim Pack As String
                Dim Arr As New List(Of clsSecondaryCustomerSale)
                Dim LineNo As String
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    Dim obj As New clsSecondaryCustomerSale()
                    Cust_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    If clsCommon.myLen(Cust_Code) > 0 Then
                        obj.Cust_Code = clsDBFuncationality.getSingleValue("Select * from TSPL_SECONDARY_CUSTOMER_MASTER Where Cust_Code='" + Cust_Code + "'")
                        If Not clsCommon.CompairString(obj.Cust_Code, Cust_Code) = CompairStringResult.Equal Then
                            Throw New Exception("Line " + LineNo + " : Customer does not exist as Secondary Customer.")
                        End If
                    End If

                    obj.Year = clsCommon.myCdbl(grow.Cells("Year").Value)
                    If Not (clsCommon.myCdbl(obj.Year) >= 2011 And clsCommon.myCdbl(obj.Year) <= 2030) Then
                        Throw New Exception("Line " + LineNo + " : Enter year between 2011 to 2030.")
                    End If

                    obj.Month = clsCommon.myCdbl(grow.Cells("Month").Value)
                    If Not (clsCommon.myCdbl(obj.Month) >= 1 And clsCommon.myCdbl(obj.Month) <= 12) Then
                        Throw New Exception("Line " + LineNo + " : Enter month between 1 to 12.")
                    End If

                    Pack = clsCommon.myCstr(grow.Cells("Pack Type").Value)
                    If clsCommon.myLen(Pack) > 0 Then
                        obj.Pack = clsDBFuncationality.getSingleValue("Select Inv_Class_Code from TSPL_INV_CLASS_DETAILS Where Inv_Class_Code='" + Pack + "' AND Inv_Class_Name='Size'")
                        If Not clsCommon.CompairString(obj.Pack, Pack) = CompairStringResult.Equal Then
                            Throw New Exception("Line " + LineNo + " : Pack type does not exist.")
                        End If
                    Else
                        Throw New Exception("Line " + LineNo + " : Enter pack type.")
                    End If
                    obj.Sale = clsCommon.myCdbl(grow.Cells("Sale").Value)
                    Arr.Add(obj)
                Next
                If (clsSecondaryCustomerSale.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        Me.Close()
    End Sub
End Class
