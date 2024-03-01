Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmCheckSlipEntry
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strQuery As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDate"
    Const ColCustomer As String = "ColCustomer"
    Const ColSalesmanname As String = "ColSalesmanname"
    Const ColRouteDesc As String = "ColRouteDesc"
    Const ColLocation As String = "ColLocation"


    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCheckSlipEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        RadSplitButton1.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub txtVehicle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicle._MYValidating
        strQuery = "select Vehicle_Id as Code,Description from TSPL_VEHICLE_MASTER"
        txtVehicle.Value = clsCommon.ShowSelectForm("Vehicle", strQuery, "Code", "", txtVehicle.Value, "Code", isButtonClicked)
        lblVehicle.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")
    End Sub
    Private Sub txtLocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLocCode._MYValidating
        strQuery = "select Segment_code as Code,Description  from TSPL_GL_SEGMENT_CODE"
        txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "Seg_No='7'", txtLocCode.Value, "Code", isButtonClicked)
        lblLocation.Text = clsDBFuncationality.getSingleValue("select  Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code ='" & txtLocCode.Value & "'")
    End Sub


    Private Sub txtCustomer__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        strQuery = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("Customer", strQuery, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
        lblCustomer.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtCustomer.Value & "'")
    End Sub

    Private Sub txtSalesman__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtSalesman._MYValidating
        strQuery = "select distinct Salesman_Code as Code,Emp_Name as Name from TSPL_SALES_ORDER_HEAD left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALES_ORDER_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE"
        txtSalesman.Value = clsCommon.ShowSelectForm("Salesman", strQuery, "Code", "", txtSalesman.Value, "Code", isButtonClicked)
        lblSales.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & txtSalesman.Value & "'")
    End Sub
    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False

        Dim gvSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvSelect.FormatString = ""
        gvSelect.Name = ColApply
        gvSelect.HeaderText = "Select"
        gvSelect.Width = 50
        gvSelect.ReadOnly = False
        gvSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvSelect)


        Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        docNo.HeaderText = "Document no"
        docNo.Name = ColDocNo
        docNo.Width = 100
        docNo.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(docNo)

        Dim Docdate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Docdate.FormatString = ""
        Docdate.HeaderText = "Doc Date"
        Docdate.Name = ColDocDate
        Docdate.Width = 70
        Docdate.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Docdate)

        Dim Cust As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Cust.FormatString = ""
        Cust.HeaderText = "Customer"
        Cust.Name = ColCustomer
        Cust.Width = 100
        Cust.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Cust)

        Dim Salesman As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Salesman.FormatString = ""
        Salesman.HeaderText = "Sales man"
        Salesman.Width = 200
        Salesman.Name = ColSalesmanname
        Salesman.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Salesman)


        Dim RouteDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RouteDesc.FormatString = ""
        RouteDesc.HeaderText = "Route"
        RouteDesc.Name = ColRouteDesc
        RouteDesc.Width = 100
        RouteDesc.ReadOnly = True
        RouteDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(RouteDesc)

        Dim Loc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Loc.FormatString = ""
        Loc.HeaderText = "Location"
        Loc.Name = ColLocation
        Loc.Width = 100
        Loc.ReadOnly = True
        Loc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(Loc)


        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_SALES_ORDER_HEAD where CheckSlipNo is not null or CheckSlipNo <> ''"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = " select distinct CheckSlipNo,CheckSlip_Date from TSPL_SALES_ORDER_HEAD "

        LoadData(clsCommon.ShowSelectForm("CheckSlip", qry, "CheckSlipNo", " (CheckSlipNo Is Not null Or CheckSlipNo <> '') ", txtCode.Value, "CheckSlipNo", isButtonClicked), NavigatorType.Current)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
    End Sub

    Private Sub funFillGrid()
        Try

            LoadBlankGrid()
            'Dim Whrcls As String
            'If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            '    Whrcls = ""
            'Else
            '    Whrcls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
            'End If

            Dim strLoc As String
            'Dim strRoute As String
            Dim strCust As String
            Dim strSalesman As String
            Dim strVehicle As String

            If clsCommon.myLen(txtLocCode.Value) > 0 Then
                strLoc = " and  Location in (select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" + txtLocCode.Value + "' )"
            Else
                strLoc = ""
            End If
            If clsCommon.myLen(txtCustomer.Value) > 0 Then
                strCust = " and  Cust_Code ='" + txtCustomer.Value + "' )"
            Else
                strCust = ""
            End If
            If clsCommon.myLen(txtSalesman.Value) > 0 Then
                strSalesman = " and  Salesman_Code ='" + txtSalesman.Value + "' )"
            Else
                strSalesman = ""
            End If
            If clsCommon.myLen(txtVehicle.Value) > 0 Then
                strVehicle = " and  Vehicle_Code='" + txtVehicle.Value + "' )"
            Else
                strVehicle = ""
            End If

            strQuery = "select 1 as Status, Order_No,Order_Date,Emp_Name,Cust_Name,Route_Desc,Location from TSPL_SALES_ORDER_HEAD " & _
            "left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALES_ORDER_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "where (CheckSlipNo is  null or CheckSlipNo = '' ) and convert(date,Order_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  " & _
            " " & strLoc & "  " & strCust & " " & strSalesman & " " & strVehicle & ""


            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    If clsCommon.myCstr(dr("Status")) = "1" Then
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = True
                    Else
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = False
                    End If
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocNo).Value = clsCommon.myCstr(dr("Order_No"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocDate).Value = clsCommon.myCstr(dr("Order_Date"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColCustomer).Value = clsCommon.myCstr(dr("Cust_Name"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColSalesmanname).Value = clsCommon.myCstr(dr("Emp_Name"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColLocation).Value = clsCommon.myCstr(dr("Location"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColRouteDesc).Value = clsCommon.myCstr(dr("Route_Desc"))
                    txtDate.Enabled = False

                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "GatePass Entry", MessageBoxButtons.OK)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()

            Dim obj As New clsSalesOrder()
            obj = clsSalesOrder.GetDataCheckSlip(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CheckSlipNo) > 0) Then
                isNewEntry = False
                'If obj.Post = "Y" Then
                '    btnSave.Enabled = False
                '    btnPost.Enabled = False
                'Else
                '    btnSave.Enabled = True
                '    btnPost.Enabled = True
                'End If
                txtCode.Value = obj.CheckSlipNo
                txtVehicle.Value = obj.Vehicle_Code
                lblVehicle.Text = obj.Vehicle_No
                txtLocCode.Value = obj.Location
                lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
                txtCustomer.Value = obj.Cust_Code
                lblCustomer.Text = obj.Cust_Name
                txtVehicle.Value = obj.Vehicle_Code
                lblVehicle.Text = obj.Vehicle_No
                txtSalesman.Value = obj.Salesman_Code
                lblSales.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & txtSalesman.Value & "'")
                txtComments.Text = obj.CSComments
                txtRemarks.Text = obj.CSRemarks
                isInsideLoadData = True
                txtDate.Value = obj.CheckSlip_Date

                If obj.Arr1 IsNot Nothing AndAlso obj.Arr1.Count > 0 Then
                    For Each objTr As clsSalesOrder In obj.Arr1
                        Gv1.Rows.AddNew()

                        If clsCommon.myCstr(objTr.Status) = "1" Then
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = True
                        Else
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = False
                        End If
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocNo).Value = objTr.Order_No
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocDate).Value = objTr.Order_Date
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColSalesmanname).Value = objTr.Salesman_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColCustomer).Value = objTr.Cust_Name
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColLocation).Value = objTr.Location
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColRouteDesc).Value = objTr.Route_Desc

                    Next
                    isInsideLoadData = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub FrmCheckSlipEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtRemarks.MaxLength = 500
        txtComments.MaxLength = 500
        btnPost.Enabled = True
        btnPost.Visible = True
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        funFillGrid()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            Dim isSaved As Boolean
            'If (AllowToSave()) Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

            If clsCommon.myLen(txtCode.Value) = 0 Then
                txtCode.Value = clsERPFuncationality.GetNextCode(trans, txtDate.Value, clsDocType.CheckSlip, "", "")
            End If

            For Each grow As GridViewRowInfo In Gv1.Rows
                If clsCommon.myCBool(grow.Cells(ColApply).Value) Then
                    Dim objTr As New clsGPDetail()
                    strQuery = "Update TSPL_SALES_ORDER_HEAD set CheckSlipNo='" & txtCode.Value & "',CheckSlip_Date='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "',CSRemarks='" & txtComments.Text & "',CSComments='" & txtRemarks.Text & "' where Order_No='" & clsCommon.myCstr(grow.Cells(ColDocNo).Value) & "'"
                    clsDBFuncationality.ExecuteNonQuery(strQuery, trans)
                    isSaved = True
                End If
            Next

            If isSaved = True Then
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Addnew()
        txtDate.Enabled = True
    End Sub
    Private Sub Addnew()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        btnSave.Enabled = True
        btnPost.Enabled = True
        LoadBlankGrid()
        isNewEntry = True
        isInsideLoadData = False
        txtLocCode.Value = ""
        lblLocation.Text = ""
        txtSalesman.Value = ""
        lblSales.Text = ""
        txtVehicle.Value = ""
        lblVehicle.Text = ""
        txtCustomer.Value = ""
        lblVehicle.Text = ""
    End Sub
End Class
