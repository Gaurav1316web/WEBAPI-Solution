Imports common
Imports System.Data.SqlClient

Public Class frmVendorPriceChartMappingUDL
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim isLoadData As Boolean = False
    Dim isValueChanged As Boolean = True
    Dim isInsideLoadData As Boolean = False
#End Region

    Private Sub FrmParameterRangeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post the price chart")
        txtDate.Value = clsCommon.GETSERVERDATE.AddMonths(-1)
        LoadPriceData()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub FrmParameterRangeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub
     
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        LoadPriceData()
    End Sub

    Sub LoadPriceData()
        Try
            gvPriceCode.DataSource = Nothing
            Dim strqry As String = "SELECT TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],TSPL_Bulk_Price_MASTER.Price_Date as [Price Date],tspl_bulk_price_detail_item_wise.Standard_Rate as [Standard Rate],  " &
            " TSPL_BULK_PRICE_DETAIL.Milk_Grade_code as [Milk Grade code],TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE],(select max(Tspl_Vendor_Price_Chart_mapping.Posted) from Tspl_Vendor_Price_Chart_mapping where isnull( Tspl_Vendor_Price_Chart_mapping.PriceCode,'')=isnull(TSPL_Bulk_Price_MASTER.Price_Code,'') and isnull(Tspl_Vendor_Price_Chart_mapping.Milk_Grade_Code,'')=isnull(TSPL_BULK_PRICE_DETAIL.Milk_Grade_code,'')) as Posted,'Add More' as AddMore FROM " &
            " TSPL_Bulk_Price_MASTER  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " &
            " left outer join tspl_bulk_price_detail_item_wise on TSPL_Bulk_Price_MASTER.Price_Code=tspl_bulk_price_detail_item_wise.Price_Code " &
            " left outer join TSPL_MILK_GRADE_MASTER on TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE  where TSPL_Bulk_Price_MASTER.Posted=1  and TSPL_Bulk_Price_MASTER.milk_type_code <> ''" +
            " and TSPL_Bulk_Price_MASTER.Price_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_Bulk_Price_MASTER.Price_Date"
            gvPriceCode.DataSource = clsDBFuncationality.GetDataTable(strqry)

            gvPriceCode.Columns("Price Code").HeaderText = "Price Code"
            gvPriceCode.Columns("Price Code").Width = 100
            gvPriceCode.Columns("Price Code").ReadOnly = True

            gvPriceCode.Columns("Price Date").HeaderText = "Price date"
            gvPriceCode.Columns("Price Date").Width = 200
            gvPriceCode.Columns("Price Date").ReadOnly = True

            gvPriceCode.Columns("Standard Rate").HeaderText = "Standard Rate"
            gvPriceCode.Columns("Standard Rate").Width = 100
            gvPriceCode.Columns("Standard Rate").ReadOnly = True
            gvPriceCode.Columns("Standard Rate").IsVisible = True

            gvPriceCode.Columns("Milk Grade code").HeaderText = "Grade Code"
            gvPriceCode.Columns("Milk Grade code").Width = 100
            gvPriceCode.Columns("Milk Grade code").ReadOnly = True
            gvPriceCode.Columns("Milk Grade code").IsVisible = True

            gvPriceCode.Columns("GRADE TYPE").HeaderText = "Grade Type"
            gvPriceCode.Columns("GRADE TYPE").Width = 100
            gvPriceCode.Columns("GRADE TYPE").ReadOnly = True
            gvPriceCode.Columns("GRADE TYPE").IsVisible = True

            gvPriceCode.Columns("Posted").HeaderText = "Posted"
            gvPriceCode.Columns("Posted").Width = 100
            gvPriceCode.Columns("Posted").ReadOnly = True
            gvPriceCode.Columns("Posted").IsVisible = False

            gvPriceCode.Columns("AddMore").IsVisible = True
            gvPriceCode.Columns("AddMore").Width = 100
            gvPriceCode.Columns("AddMore").ReadOnly = True
            gvPriceCode.Columns("AddMore").HeaderText = "Add More"

            gvPriceCode.AllowAddNewRow = False
            gvPriceCode.ShowGroupPanel = False
            gvPriceCode.AllowColumnReorder = False
            gvPriceCode.AllowRowReorder = False
            gvPriceCode.EnableSorting = False
            gvPriceCode.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvPriceCode.MasterTemplate.ShowRowHeaderColumn = False

            TagMappedVendor()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub TagMappedVendor()
        Dim qry As String
        Dim arr As ArrayList
        Dim dt As DataTable
        Try
            For ii As Integer = 0 To gvPriceCode.RowCount - 1
                qry = "select VendorCode from Tspl_Vendor_Price_Chart_mapping where PriceCode='" + clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value) + "' and Milk_Grade_Code='" + clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value) + "'"
                arr = Nothing
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    arr = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        arr.Add(dr("VendorCode"))
                    Next
                End If
                gvPriceCode.Rows(ii).Tag = arr
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            arr = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Sub gvPriceCode_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvPriceCode.CellDoubleClick
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        If e.Column Is gvPriceCode.Columns("AddMore") AndAlso clsCommon.myCdbl(gvPriceCode.CurrentRow.Cells("Posted").Value) > 0 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.MCC_DLTDATA_PWD
            frm.strCode = clsFixedParameterCode.MCCDLTPWD
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                Dim arr As ArrayList = TryCast(gvPriceCode.CurrentRow.Tag, ArrayList)
                Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER where TSPL_VENDOR_MASTER.Status='N' and Vendor_Type_CHA in ('M','J')"
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    qry += " and Vendor_Code not in (" + clsCommon.GetMulcallString(arr) + ")"
                End If
                arr = clsCommon.ShowMultipleSelectForm(False, "VPCMUDLVENM1", qry, "Code", "Name", arr, Nothing)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    If clsCommon.MyMessageBoxShow(clsCommon.myCstr(arr.Count) + " Vendors will added as posted." + Environment.NewLine + "Do you want to continue...", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        ADDMore(arr)
                        clsCommon.MyMessageBoxShow(Me, "Vendor Added successfully", Me.Text)
                        LoadPriceData()
                        If (common.clsCommon.MyMessageBoxShow(Me, "Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                            printData()
                        End If
                    End If
                End If
            End If
        Else
            Dim arr As ArrayList = TryCast(gvPriceCode.CurrentRow.Tag, ArrayList)
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER where TSPL_VENDOR_MASTER.Status='N' and Vendor_Type_CHA in ('M','J')"
            arr = clsCommon.ShowMultipleSelectForm(False, "VPCMUDLVENM", qry, "Code", "Name", arr, Nothing)
            If clsCommon.myCdbl(gvPriceCode.CurrentRow.Cells("Posted").Value) = 0 Then
                gvPriceCode.CurrentRow.Tag = arr
            End If
        End If
    End Sub

    Sub ADDMore(ByVal arrVendor As ArrayList)
        Try
             
            Dim obj As New clsVendorPriceChartMappingUDLHead
            obj.Pricecode = clsCommon.myCstr(gvPriceCode.CurrentRow.Cells("Price Code").Value)
            obj.Milk_Grade_Code = clsCommon.myCstr(gvPriceCode.CurrentRow.Cells("Milk Grade code").Value)
            obj.arrVendor = arrVendor
            obj.AddMore(obj)
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadPriceData()
            obj = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVendorPriceChartMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return False
            End If
        End If
        Return True
    End Function

    Sub SaveData()
        Dim arr As New List(Of clsVendorPriceChartMappingUDLHead)
        For ii As Integer = 0 To gvPriceCode.RowCount - 1
            If clsCommon.myCdbl(gvPriceCode.Rows(ii).Cells("Posted").Value) = 0 Then
                Dim obj As New clsVendorPriceChartMappingUDLHead
                obj.Pricecode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)
                obj.Milk_Grade_Code = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value)
                obj.arrVendor = TryCast(gvPriceCode.Rows(ii).Tag, ArrayList)
                If obj.arrVendor IsNot Nothing AndAlso obj.arrVendor.Count > 0 Then
                    arr.Add(obj)
                End If
            End If
        Next
        If arr.Count <= 0 Then
            Throw New Exception("No Data found to Save")
        Else
            Dim objCall As New clsVendorPriceChartMappingUDLHead()
            objCall.SaveData(arr)
            objCall = Nothing
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadPriceData()
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If myMessages.deleteConfirm() Then
                Dim arr As New List(Of clsVendorPriceChartMappingUDLHead)
                For ii As Integer = 0 To gvPriceCode.RowCount - 1
                    If clsCommon.myCdbl(gvPriceCode.Rows(ii).Cells("Posted").Value) = 0 Then
                        Dim obj As New clsVendorPriceChartMappingUDLHead
                        obj.Pricecode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)
                        obj.Milk_Grade_Code = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value)
                        arr.Add(obj)
                    End If
                Next
                If arr.Count <= 0 Then
                    Throw New Exception("No Data found to Delete")
                Else
                    Dim objCall As New clsVendorPriceChartMappingUDLHead()
                    objCall.DeleteData(arr)
                    objCall = Nothing
                    myMessages.delete()
                    LoadPriceData()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If myMessages.postConfirm() Then
                Dim arr As New List(Of clsVendorPriceChartMappingUDLHead)
                For ii As Integer = 0 To gvPriceCode.RowCount - 1
                    If clsCommon.myCdbl(gvPriceCode.Rows(ii).Cells("Posted").Value) = 0 Then
                        Dim obj As New clsVendorPriceChartMappingUDLHead
                        obj.Pricecode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)
                        obj.Milk_Grade_Code = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value)
                        obj.arrVendor = TryCast(gvPriceCode.Rows(ii).Tag, ArrayList)
                        If obj.arrVendor IsNot Nothing AndAlso obj.arrVendor.Count > 0 Then
                            arr.Add(obj)
                        End If
                    End If
                Next
                If arr.Count <= 0 Then
                    Throw New Exception("No Data found to Post")
                Else
                    Dim objCall As New clsVendorPriceChartMappingUDLHead()
                    objCall.PostData(arr)
                    objCall = Nothing
                    myMessages.post()
                    LoadPriceData()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvPriceCode_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvPriceCode.RowFormatting
        Try
            If clsCommon.myCdbl(e.RowElement.RowInfo.Cells("Posted").Value) > 0 Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.LightGreen
            Else
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printData()
    End Sub
    Sub printData()
        Try
            Dim fff As String = gvPriceCode.CurrentRow.Index
            Dim strGradeCode As String = ""
            Dim priceCode As String = gvPriceCode.CurrentRow().Cells(0).Value.ToString
            Dim gradeCode As String = gvPriceCode.CurrentRow().Cells(2).Value.ToString
            If clsCommon.myLen(gradeCode) > 0 Then
                strGradeCode = " and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code = '" + gradeCode + "' and tspl_Vendor_price_chart_mapping.Milk_Grade_code='" + gradeCode + "' "
            Else
                strGradeCode = " and isnull(TSPL_BULK_PRICE_DETAIL.Milk_Grade_code,'') = '" + gradeCode + "' and  isnull(tspl_Vendor_price_chart_mapping.Milk_Grade_code,'')='' "
            End If
            Dim arr As ArrayList = TryCast(gvPriceCode.CurrentRow.Tag, ArrayList)
            Dim vendorCodeList As String = clsCommon.GetMulcallString(arr)

            If clsCommon.myLen(priceCode) > 0 Then
                Dim qry As String = " SELECT distinct TSPL_COMPANY_MASTER.Comp_Name as Company_Name , TSPL_COMPANY_MASTER.Add1 as Address1,TSPL_COMPANY_MASTER.Add2 as Address2,  TSPL_COMPANY_MASTER.Add3 as Address3,TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],format( TSPL_Bulk_Price_MASTER.Price_Date,'dd/MM/yyyy hh:mm:ss') as [Price Date],  TSPL_BULK_PRICE_DETAIL.Milk_Grade_code as [Milk Grade code],TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE], " & _
                                   " (select max(Tspl_Vendor_Price_Chart_mapping.Posted) from Tspl_Vendor_Price_Chart_mapping where isnull( Tspl_Vendor_Price_Chart_mapping.PriceCode,'')=isnull(TSPL_Bulk_Price_MASTER.Price_Code,'') and isnull(Tspl_Vendor_Price_Chart_mapping.Milk_Grade_Code,'')=isnull(TSPL_BULK_PRICE_DETAIL.Milk_Grade_code,'')) as Posted, tspl_Vendor_price_chart_mapping.VendorCode,TSPL_VENDOR_MASTER.Vendor_Name " & _
                                   " FROM  TSPL_Bulk_Price_MASTER  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code  left outer join TSPL_MILK_GRADE_MASTER on TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE " & _
                                   " left outer join tspl_Vendor_price_chart_mapping on TSPL_Bulk_Price_MASTER.Price_Code =tspl_Vendor_price_chart_mapping.PriceCode " & _
                                   " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =tspl_Vendor_price_chart_mapping.VendorCode " & _
                                   " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_Bulk_Price_MASTER.Comp_Code " & _
                                   " where TSPL_Bulk_Price_MASTER.Posted=1  and TSPL_Bulk_Price_MASTER.milk_type_code <> '' and TSPL_Bulk_Price_MASTER.Price_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_Bulk_Price_MASTER.Price_Code='" + priceCode + "' and Tspl_Vendor_Price_Chart_mapping.VendorCode in (" + vendorCodeList + ") " + strGradeCode + " and TSPL_VENDOR_MASTER.Status='N' and Vendor_Type_CHA in ('M','J')  "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptVendorPriceChartMapping", "Vendor Price Chart Mapping")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
