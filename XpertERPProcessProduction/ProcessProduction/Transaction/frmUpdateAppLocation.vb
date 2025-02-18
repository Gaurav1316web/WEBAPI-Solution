Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmUpdateAppLocation
    Inherits FrmMainTranScreen
    Const ColCode As String = "ColCode"
    Private isInsideLoadData As Boolean = False

    Const colLocationName As String = "colLocationName"
    Const colDataBaseName As String = "colDataBaseName"
    Const colCustomerCode As String = "colCustomerCode"
    Const colCustomerName As String = "colCustomerName"
    Const colCustomerAccountNo As String = "colCustomerAccountNo"
    Const colSchedulerApplySMS As String = "colSchedulerApplySMS"
    Const colSchedulerApplyEMail As String = "colSchedulerApplyEMail"
    Const colApplyPDAccount As String = "colApplyPDAccount"
    Const colApplyECollect As String = "colApplyECollect"

    Private Sub frmUpdateAppLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formLoad()
    End Sub
    Sub formLoad()
        Dim dt As DataTable = Nothing
        Dim qry As String = "select *  from TSPL_MASTER.dbo.TSPL_APP_LOCATION "
        dt = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                LoadBlankGridColmns()
                'gv1.DataSource = dt
                gv1.BestFitColumns()
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCode).Value = dr("Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = dr("Location_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDataBaseName).Value = dr("DataBase_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerCode).Value = dr("Customer_Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerName).Value = dr("Customer_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerAccountNo).Value = dr("Customer_Account_No")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchedulerApplyEMail).Value = dr("Scheduler_Apply_SMS")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchedulerApplySMS).Value = dr("Scheduler_Apply_EMail")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyPDAccount).Value = dr("Apply_PD_Account")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyECollect).Value = dr("Apply_ECollect")
                Next
                'View()
                ' SetGridFormation()
                'ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                'RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        End If
    End Sub

    Sub LoadBlankGridColmns()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        'S.NO column'
        Dim gridColCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColCode.FormatString = ""
        gridColCode.HeaderText = "Code"
        gridColCode.Name = ColCode
        gridColCode.Width = 105
        gridColCode.ReadOnly = True
        gridColCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridColCode)

        Dim gridColLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColLocationName.FormatString = ""
        gridColLocationName.HeaderText = "Location Name"
        gridColLocationName.Name = colLocationName
        gridColLocationName.Width = 105
        gridColLocationName.ReadOnly = False
        gridColLocationName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridColLocationName)


        Dim gridcolCustomerCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolCustomerCode.FormatString = ""
        gridcolCustomerCode.HeaderText = "Customer Code"
        gridcolCustomerCode.Name = colCustomerCode
        gridcolCustomerCode.Width = 105
        gridcolCustomerCode.ReadOnly = False
        gridcolCustomerCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolCustomerCode)


        Dim gridcolDataBaseName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolDataBaseName.FormatString = ""
        gridcolDataBaseName.HeaderText = "DataBase Name"
        gridcolDataBaseName.Name = colDataBaseName
        gridcolDataBaseName.Width = 105
        gridcolDataBaseName.ReadOnly = False
        gridcolDataBaseName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolDataBaseName)

        Dim gridcolCustomerName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolCustomerName.FormatString = ""
        gridcolCustomerName.HeaderText = "Customer Name"
        gridcolCustomerName.Name = colCustomerName
        gridcolCustomerName.Width = 105
        gridcolCustomerName.ReadOnly = False
        gridcolCustomerName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolCustomerName)

        Dim gridcolCustomerAccountNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolCustomerAccountNo.FormatString = ""
        gridcolCustomerAccountNo.HeaderText = "Customer Account No"
        gridcolCustomerAccountNo.Name = colCustomerAccountNo
        gridcolCustomerAccountNo.Width = 105
        gridcolCustomerAccountNo.ReadOnly = False
        gridcolCustomerAccountNo.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolCustomerAccountNo)

        Dim gridcolSchedulerApplySMS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolSchedulerApplySMS.FormatString = ""
        gridcolSchedulerApplySMS.HeaderText = "Scheduler Apply SMS"
        gridcolSchedulerApplySMS.Name = colSchedulerApplySMS
        gridcolSchedulerApplySMS.Width = 105
        gridcolSchedulerApplySMS.ReadOnly = False
        gridcolSchedulerApplySMS.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolSchedulerApplySMS)


        Dim gridcolSchedulerApplyEMail As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolSchedulerApplyEMail.FormatString = ""
        gridcolSchedulerApplyEMail.HeaderText = "Scheduler Apply Email"
        gridcolSchedulerApplyEMail.Name = colSchedulerApplyEMail
        gridcolSchedulerApplyEMail.Width = 105
        gridcolSchedulerApplyEMail.ReadOnly = False
        gridcolSchedulerApplyEMail.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolSchedulerApplyEMail)


        Dim gridcolApplyPDAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolApplyPDAccount.FormatString = ""
        gridcolApplyPDAccount.HeaderText = "Apply PD Account"
        gridcolApplyPDAccount.Name = colApplyPDAccount
        gridcolApplyPDAccount.Width = 105
        gridcolApplyPDAccount.ReadOnly = False
        gridcolApplyPDAccount.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolApplyPDAccount)


        Dim gridcolApplyECollect As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolApplyECollect.FormatString = ""
        gridcolApplyECollect.HeaderText = "Apply E Collect"
        gridcolApplyECollect.Name = colApplyECollect
        gridcolApplyECollect.Width = 105
        gridcolApplyECollect.ReadOnly = False
        gridcolApplyECollect.IsVisible = True
        gv1.MasterTemplate.Columns.Add(gridcolApplyECollect)

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        'gv1.Rows.AddNew()
        'ReStoreGridLayout()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub


    ' Helper function to check if a row is modified (implement your logic here)


    Sub SaveData()
        Try
            Dim obj As ClsUpdateAppLocation
            obj = New ClsUpdateAppLocation()
            obj.Arr = New List(Of ClsUpdateAppLocation)
            'Dim objTr As ClsUpdateAppLocation
            '0--
            'If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            'For Each grow As GridViewRowInfo In gv1.Rows
            '    Dim objTr As New ClsUpdateAppLocation()
            '    objTr.Code = clsCommon.myCstr(grow.Cells(ColCode).Value)
            '    objTr.Location_Name = clsCommon.myCstr(grow.Cells(colLocationName).Value)
            '    objTr.DataBase_Name = clsCommon.myCstr(grow.Cells(colDataBaseName).Value)
            '    objTr.Customer_Code = clsCommon.myCstr(grow.Cells(colCustomerCode).Value)
            '    objTr.Customer_Name = clsCommon.myCstr(grow.Cells(colCustomerName).Value)
            '    objTr.Customer_Account_No = clsCommon.myCstr(grow.Cells(colCustomerAccountNo).Value)
            '    objTr.Scheduler_Apply_SMS = clsCommon.myCdbl(grow.Cells(colSchedulerApplySMS).Value)
            '    objTr.Scheduler_Apply_EMail = clsCommon.myCdbl(grow.Cells(colSchedulerApplyEMail).Value)
            '    objTr.Apply_PD_Account = clsCommon.myCdbl(grow.Cells(colApplyPDAccount).Value)
            '    objTr.Apply_ECollect = clsCommon.myCdbl(grow.Cells(colApplyECollect).Value)

            '    If (clsCommon.myLen(objTr.Code) > 0) Then
            '        Arr.Add(objTr)
            '    End If
            'Next

            'End If
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New ClsUpdateAppLocation()
                objTr.Code = clsCommon.myCstr(grow.Cells(ColCode).Value)
                objTr.Location_Name = clsCommon.myCstr(grow.Cells(colLocationName).Value)
                objTr.DataBase_Name = clsCommon.myCstr(grow.Cells(colDataBaseName).Value)
                objTr.Customer_Code = clsCommon.myCstr(grow.Cells(colCustomerCode).Value)
                objTr.Customer_Name = clsCommon.myCstr(grow.Cells(colCustomerName).Value)
                objTr.Customer_Account_No = clsCommon.myCstr(grow.Cells(colCustomerAccountNo).Value)
                objTr.Scheduler_Apply_SMS = clsCommon.myCdbl(grow.Cells(colSchedulerApplySMS).Value)
                objTr.Scheduler_Apply_EMail = clsCommon.myCdbl(grow.Cells(colSchedulerApplyEMail).Value)
                objTr.Apply_PD_Account = clsCommon.myCdbl(grow.Cells(colApplyPDAccount).Value)
                objTr.Apply_ECollect = clsCommon.myCdbl(grow.Cells(colApplyECollect).Value)

                If (clsCommon.myLen(objTr.Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next

            'If (obj.SaveData(Arr)) Then
            '    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            '    btnsave.Text = "Update"
            '    'LoadData(obj.vendor_code, NavigatorType.Current)
            'End If

            If (obj.SaveData(obj.Arr)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                btnsave.Text = "Update"
                LoadData(obj.Code, NavigatorType.Current)
            End If
            ' End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal Code As String, ByVal NavType As NavigatorType)
        Try
            btnsave.Enabled = True
            btnsave.Text = "Save"

            isInsideLoadData = True

            'btnsave.Text = "Update"

            'funreset()
            'LoadBlankGrid()

            Dim Arr As List(Of ClsUpdateAppLocation) = ClsUpdateAppLocation.GetData(Code)
            'fndvendor.Value = vendorcode
            '.Text = Desc
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As ClsUpdateAppLocation In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCode).Value = objTr.Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.Location_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDataBaseName).Value = objTr.DataBase_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerCode).Value = objTr.Customer_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerName).Value = objTr.Customer_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerAccountNo).Value = objTr.Customer_Account_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchedulerApplySMS).Value = objTr.Scheduler_Apply_SMS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchedulerApplyEMail).Value = objTr.Scheduler_Apply_EMail
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyPDAccount).Value = objTr.Apply_PD_Account
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyECollect).Value = objTr.Apply_ECollect
                    btnsave.Text = "Update"
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class