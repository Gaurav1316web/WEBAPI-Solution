'===============Created By Rohit
Imports common
Imports System.Data.SqlClient

Public Class FrmMCCMaterialSalePriceChart
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Private isInsideLoadData As Boolean = False
    Dim AllowPlandDeptMCCLocation As Boolean = False
#End Region

    Sub Reset()
        isInsideLoadData = False
        fndno.Value = ""
        chkMCCAll.IsChecked = True
        cbgMCC.CheckedAll()
        
        fndno.MyReadOnly = False
        txtdate.Text = clsCommon.GETSERVERDATE()
        MyDateTimePicker1.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        LoadGrid()
        'UcAttachment1.Form_ID = Me.Form_ID
        'UcAttachment1.BlankAllControls()
    End Sub
    Sub LoadGrid()
        gv.Columns.Add("Item Code")
        gv.Columns.Add("Item Name")
        gv.Columns.Add("Unit Code")
        gv.Columns.Add("Unit Desc")
        gv.Columns.Add("Price")
        gv.Columns.Add("Issaved")

        gv.Columns("Item Code").Width = 100
        gv.Columns("Item Name").Width = 300
        gv.Columns("Item Name").ReadOnly = True
        gv.Columns("Unit Code").Width = 100
        gv.Columns("Unit Desc").Width = 100
        gv.Columns("Unit Desc").ReadOnly = True
        gv.Columns("Price").Width = 100
        gv.Columns("Issaved").Width = 0
        gv.Columns("Issaved").IsVisible = False

        gv.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
        'gv.Width = 80
        ' gv.ReadOnly = False
        gv.AllowAddNewRow = True
        gv.AllowDeleteRow = False

    End Sub

    Sub LoadMCC()
        Dim qry As String = Nothing
        qry = "select MCC_Code as Code ,MCC_NAME as Name from TSPL_MCC_MASTER "
        If AllowPlandDeptMCCLocation Then
        qry += " union all " & _
        " select Location_Code AS Code, Location_Desc as Name  from TSPL_LOCATION_MASTER " & _
        " where type in ('Depot','Plant') and location_category<>'MCC' and  Is_Section='N' and Is_Sub_Location='N' " & _
        " and (GIT_Type='' or GIT_Type='N') and MCC_Type='N' order by Code "
         End If      

        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"
        chkMCCAll.IsChecked = True
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMCCMaterialSalePriceChart)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub FrmMCCMaterialSalePriceChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AllowPlandDeptMCCLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_Plant_Depot_MCC_typeLocation, clsFixedParameterCode.Allow_Plant_Depot_MCC_typeLocation, Nothing)) = "1", True, False))
        If AllowPlandDeptMCCLocation Then
            Try
                Dim qry As String = clsERPFuncationality.GetForeignKey_Name("TSPL_MCC_RATE_UPLOADER_MCC", "MCC_Code")
                If clsCommon.myLen(qry) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MCC_RATE_UPLOADER_MCC drop constraint " + qry)
                End If
            Catch ex As Exception
            End Try
        End If

        LoadMCC()


        Reset()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnrefresh, "Press Alt+R for Refresh the Window")
        ButtonToolTip.SetToolTip(btnshow, "Press Alt+S  for Show Data In Grid")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed

    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        LoadData()
    End Sub

    Sub LoadData()
        If clsCommon.myLen(fndno.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Code Of Price Chart Uploader", Me.Text)
            fndno.Focus()
            fndno.Select()
            ErrorControl.SetError(fndno, "Please Select Code Of Price Chart Uploader")
            Return
        Else
            ErrorControl.ResetError(fndno)
        End If

        'If clsCommon.myLen(txtdate.Text) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Fill Date", Me.Text)
        '    txtdate.Focus()
        '    txtdate.Select()
        '    ErrorControl.SetError(txtdate, "Please Fill Date")
        '    Return
        'Else
        '    ErrorControl.ResetError(txtdate)
        'End If

        'Dim xdate As Date = Nothing
        'xdate = Convert.ToDateTime(txtdate.Text).ToString("MM/dd/yyyy")

        Dim qry As String = "select count(*) from TSPL_MCC_RATE_UPLOADER_MASTER where code='" + clsCommon.myCstr(fndno.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Return
        End If

        chkMCCAll.IsChecked = True



        'qry = "select '' as 'Option',aa.fat," + values + " from (select ROW_NUMBER() over(PARTITION by fat order by fat) as sno,rate,snf from tspl_fat_snf_uploader_master where code='" + clsCommon.myCstr(fndno.Value) + "') as s pivot(max(rate) for snf in (" + values + ")) as t left outer join (select ROW_NUMBER() over(order by a.fat) as sno1,a.fat from (select distinct fat from tspl_fat_snf_uploader_master where code='" + clsCommon.myCstr(fndno.Value) + "')a)aa on aa.sno1=sno"
        qry = "select tspl_item_master.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_UNIT_MASTER. unit_code as [Unit Code]" _
        & " ,Unit_Desc as [Unit Desc],Price,1 as Issaved from tspl_item_master left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=tspl_item_master.item_code" _
        & " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code left join TSPL_MCC_RATE_UPLOADER_Detail on " _
        & " TSPL_MCC_RATE_UPLOADER_Detail.item_code=TSPL_ITEM_MASTER.item_code and TSPL_MCC_RATE_UPLOADER_Detail.rate_uom=TSPL_UNIT_MASTER.Unit_Code " _
        & " where code='" & fndno.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt

            gv.Columns("Item Code").Width = 100
            gv.Columns("Item Name").Width = 300
            gv.Columns("Item Name").ReadOnly = True
            gv.Columns("Unit Code").Width = 100
            gv.Columns("Unit Desc").Width = 100
            gv.Columns("Unit Desc").ReadOnly = True
            gv.Columns("Price").Width = 100
            gv.Columns("Issaved").Width = 0
            gv.Columns("Issaved").IsVisible = False

            gv.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            'gv.Width = 80
            ' gv.ReadOnly = False
            gv.AllowAddNewRow = True
            gv.AllowDeleteRow = False
            'gv.AllowAutoSizeColumns = True
            ' gv.Rows(0).Cells(0).Value = "FAT"
        End If


        '=======================MCC========================
        qry = "select * from TSPL_MCC_RATE_UPLOADER_MCC where code='" + fndno.Value + "'"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)

        Dim arr As New ArrayList()

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("mcc_code")))
            Next

            chkMCCSelect.IsChecked = True
            cbgMCC.CheckedValue = arr
        End If


        
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select tspl_item_master.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_UNIT_MASTER. unit_code as [Unit Code],TSPL_UNIT_MASTER. unit_desc as [Unit Desc],Price from tspl_item_master left join " _
        & " TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_MASTER .Unit_Code left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.item_code" _
        & " =TSPL_ITEM_MASTER.item_code and TSPL_MCC_RATE_UPLOADER_Detail.rate_uom=TSPL_UNIT_MASTER.Unit_Code "
        Dim whrcls As String = " and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI') and Item_Type not in ('A') "
        transportSql.ExporttoExcel(qry, whrcls, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        If clsCommon.myLen(txtdate.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Fill Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Return
        End If
        If clsCommon.myCDate(txtdate.Value) > clsCommon.myCDate(MyDateTimePicker1.Value) Then
            clsCommon.MyMessageBoxShow(Me, "Document Date will be Less then Effective Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Return
        End If

        
        '------------------------------------------------------------------
        '-------------------------------------------

        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim columnsname As String = transportSql.GetExcelColumnsName(gv1)

        Dim currentdate As Date = Nothing
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Dim code As String = ""
        Dim Item_Code As String = ""
        Dim Unit_Code As String = ""
        Dim Price As String = ""

        
        currentdate = Convert.ToDateTime(txtdate.Text)

        Dim qry As String = "select max(code) from TSPL_MCC_RATE_UPLOADER_MASTER"
        code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

        If clsCommon.myLen(code) > 0 Then
            code = clsCommon.myCstr(clsCommon.incval(code))
        Else
            code = "PCU000001"
        End If
        Try

            If clsCommon.myLen(columnsname) > 0 Then
                'Dim dt As New DataTable()
                'dt.Columns.Add("Price", GetType(String))

                If gv1.Rows.Count > 0 Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Code", code)
                    clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Effective_date", clsCommon.GetPrintDate(MyDateTimePicker1.Value, "dd/MMM/yyyy"))

                    clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                    clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)

                    For Each Grow As GridViewRowInfo In gv1.Rows
                        '---------------save data-----------------------------
                        If clsCommon.myCdbl(clsCommon.myCstr(Grow.Cells("Price").Value)) > 0 Then

                            Dim col2 As New Hashtable()
                            clsCommon.AddColumnsForChange(col2, "Code", code)
                            clsCommon.AddColumnsForChange(col2, "Item_Code", clsCommon.myCstr(Grow.Cells("Item Code").Value))
                            clsCommon.AddColumnsForChange(col2, "Rate_UOM", clsCommon.myCstr(Grow.Cells("Unit Code").Value))
                            clsCommon.AddColumnsForChange(col2, "Price", clsCommon.myCstr(Grow.Cells("Price").Value))

                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col2, "TSPL_MCC_RATE_UPLOADER_Detail", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    Next '-snf header value loop
                End If '--
            Else
                Throw New Exception("No Data Found For Transfer")
            End If
            trans.Commit()
            '===============save mcc data======================================
            If cbgMCC.CheckedValue.Count > 0 Then
                For Each strvalue As String In cbgMCC.CheckedValue
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "code", code)
                    clsCommon.AddColumnsForChange(coll, "mcc_code", strvalue, True)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            '===============================================================
            clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)

            'UcAttachment1.SaveData(clsCommon.myCstr(fndno.Value))
            'Reset()
            fndno.Value = code
            LoadData()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv1)
    End Sub

    Private Sub fndno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndno._MYValidating
        Dim qry As String = "select distinct Code,Date,Effective_Date as [Effective Date] from TSPL_MCC_RATE_UPLOADER_MASTER"
        fndno.Value = clsCommon.ShowSelectForm("CHTFND", qry, "Code", "", fndno.Value, "Code", isButtonClicked)

        If clsCommon.myLen(fndno.Value) > 0 Then
            txtdate.Value = clsDBFuncationality.getSingleValue("select distinct Date from TSPL_MCC_RATE_UPLOADER_MASTER where code='" + fndno.Value + "'")
            MyDateTimePicker1.Value = clsDBFuncationality.getSingleValue("select distinct Effective_Date from TSPL_MCC_RATE_UPLOADER_MASTER where code='" + fndno.Value + "'")
            LoadData()
        Else
            Reset()
        End If
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged, chkMCCSelect.ToggleStateChanged
        cbgMCC.Enabled = chkMCCSelect.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Sub Save()
        If clsCommon.myLen(txtdate.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Fill Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Return
        End If
        If clsCommon.myCDate(txtdate.Value) > clsCommon.myCDate(MyDateTimePicker1.Value) Then
            clsCommon.MyMessageBoxShow(Me, "Document Date will be Less then Effective Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Return
        End If


        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmMCCMaterialSalePriceChart, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        '------------------------------------------------------------------
        '-------------------------------------------
        Dim currentdate As Date = Nothing
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Dim code As String = ""

        currentdate = Convert.ToDateTime(txtdate.Text)

        If clsCommon.myLen(fndno.Value) <= 0 Then
            Dim qry As String = "select max(code) from TSPL_MCC_RATE_UPLOADER_MASTER"
            code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(code) > 0 Then
                code = clsCommon.myCstr(clsCommon.incval(code))
            Else
                code = "PCU000001"
            End If

        Else
            code = fndno.Value
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndno.Value, "TSPL_MCC_RATE_UPLOADER_master", "Code", "TSPL_MCC_RATE_UPLOADER_Detail", "Code", "TSPL_MCC_RATE_UPLOADER_MCC", "Code", trans)
        End If
        Try


            'Dim dt As New DataTable()
            'dt.Columns.Add("Price", GetType(String))

            If gv.Rows.Count > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", code)
                clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Effective_date", clsCommon.GetPrintDate(MyDateTimePicker1.Value, "dd/MMM/yyyy"))

                clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                If clsCommon.myLen(fndno.Value) <= 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MASTER", OMInsertOrUpdate.Update, "TSPL_MCC_RATE_UPLOADER_MASTER.code='" & fndno.Value & "'", trans)
                End If


                For Each Grow As GridViewRowInfo In gv.Rows

                    '---------------save data-----------------------------
                    If clsCommon.myCdbl(clsCommon.myCstr(Grow.Cells("Price").Value)) > 0 Then

                        Dim col2 As New Hashtable()
                        clsCommon.AddColumnsForChange(col2, "Code", code)
                        clsCommon.AddColumnsForChange(col2, "Item_Code", clsCommon.myCstr(Grow.Cells("Item Code").Value))
                        clsCommon.AddColumnsForChange(col2, "Rate_UOM", clsCommon.myCstr(Grow.Cells("Unit Code").Value))
                        clsCommon.AddColumnsForChange(col2, "Price", clsCommon.myCstr(Grow.Cells("Price").Value))
                        If clsCommon.myCdbl(Grow.Cells("Issaved").Value) <= 0 Then
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col2, "TSPL_MCC_RATE_UPLOADER_Detail", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col2, "TSPL_MCC_RATE_UPLOADER_Detail", OMInsertOrUpdate.Update, "TSPL_MCC_RATE_UPLOADER_Detail.code='" & code & "' and Item_code='" & clsCommon.myCstr(Grow.Cells("Item Code").Value) & "'", trans)
                        End If

                    End If
                Next '-snf header value loop
                'End If '--
            Else
                Throw New Exception("No Data Found For Transfer")
            End If
            trans.Commit()
            '===============save mcc data======================================
            If cbgMCC.CheckedValue.Count > 0 Then
                For Each strvalue As String In cbgMCC.CheckedValue
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "code", code)
                    clsCommon.AddColumnsForChange(coll, "mcc_code", strvalue, True)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            '===============================================================
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)

            'UcAttachment1.SaveData(clsCommon.myCstr(fndno.Value))
            'Reset()
            fndno.Value = code
            LoadData()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Save()
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                isInsideLoadData = True
                If e.Column Is gv.Columns("Item Code") Then
                    OpenICodeList(True)
                ElseIf e.Column Is gv.Columns("Unit Code") Then
                    OpenUOMList(True)
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim qry As String
        qry = "select * from (select TSPL_ITEM_MASTER.item_code as Item,TSPL_ITEM_MASTER.item_desc as [ItemDesc], TSPL_ITEM_MASTER.Unit_Code as Unit," _
        & " UOM_Description as [Unit Name]  from  TSPL_ITEM_MASTER  left join tspl_Item_Uom_Detail on tspl_Item_Uom_Detail.item_Code=" _
        & " TSPL_ITEM_MASTER.item_COde and stocking_unit='Y'   where  TSPL_ITEM_MASTER.Active=1 and (Item_Used_as='S' OR Item_Used_as='I' ) ) as s"
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
        '    qry += " OR Item_Used_as='I' "       'refer by ashok sir and done by stuti on 16/10/2016
        'End If
        '' KDIL > DATE : 19-01-2017
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
        '    qry += " OR Item_Used_as='I' "
        'End If

        'qry += ") ) as s"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("MCCS@I", qry)
        If Not dr Is Nothing Then
            If clsCommon.myLen(clsCommon.myCstr(dr("Item"))) > 0 Then
                gv.CurrentRow.Cells("Item Code").Value = clsCommon.myCstr(dr("Item"))
                gv.CurrentRow.Cells("Item Name").Value = clsCommon.myCstr(dr("ItemDesc"))
                gv.CurrentRow.Cells("Unit Code").Value = clsCommon.myCstr(dr("Unit"))
                gv.CurrentRow.Cells("Unit desc").Value = clsCommon.myCstr(dr("Unit Name"))
                gv.CurrentRow.Cells("IsSaved").Value = 0
            End If
        Else
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv.CurrentRow.Cells("Item Code").Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv.CurrentRow.Cells("Unit Code").Value = clsCommon.ShowSelectForm("Unit_Fndr", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells("Item Code").Value), "Code", isButtonClick)
            gv.CurrentRow.Cells("Unit desc").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_Unit_master where Unit_Code='" & gv.CurrentRow.Cells("Unit Code").Value & "'"))
        End If
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndno.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Price Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(fndno.Value, "Code", "TSPL_MCC_RATE_UPLOADER_master", "TSPL_MCC_RATE_UPLOADER_Detail")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
