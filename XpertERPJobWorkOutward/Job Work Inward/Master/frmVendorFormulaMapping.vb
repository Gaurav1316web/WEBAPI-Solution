Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
'' work to be done agaist ticket no.BHA/28/08/18-000494

Public Class frmVendorFormulaMapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
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
        Reset()

    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
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
            Dim strqry As String = "select code as Code,Structure_Code as [Structure Code],Formula,0 as Posted from TSPL_JW_FORMULA "
            gvPriceCode.DataSource = clsDBFuncationality.GetDataTable(strqry)

            gvPriceCode.Columns("Code").HeaderText = "Formula Code"
            gvPriceCode.Columns("Code").Width = 100
            gvPriceCode.Columns("Code").ReadOnly = True

            gvPriceCode.Columns("Structure Code").HeaderText = "Item Structure Code"
            gvPriceCode.Columns("Structure Code").Width = 200
            gvPriceCode.Columns("Structure Code").ReadOnly = True

            gvPriceCode.Columns("Formula").HeaderText = "Formula"
            gvPriceCode.Columns("Formula").Width = 100
            gvPriceCode.Columns("Formula").ReadOnly = True
            gvPriceCode.Columns("Formula").IsVisible = True

            gvPriceCode.Columns("Posted").HeaderText = "Posted"
            gvPriceCode.Columns("Posted").Width = 100
            gvPriceCode.Columns("Posted").ReadOnly = True
            gvPriceCode.Columns("Posted").IsVisible = False

            gvPriceCode.AllowAddNewRow = False
            gvPriceCode.ShowGroupPanel = False
            gvPriceCode.AllowColumnReorder = False
            gvPriceCode.AllowRowReorder = False
            gvPriceCode.EnableSorting = False
            gvPriceCode.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvPriceCode.MasterTemplate.ShowRowHeaderColumn = False

            'TagMappedVendor()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            gvPriceCode.DataSource = Nothing
            Dim dt As DataTable = Nothing
            Dim strqry As String = "select distinct TSPL_VENDOR_FORMULA_MAPPING.DocCode,TSPL_VENDOR_FORMULA_MAPPING.DocDate,TSPL_JW_FORMULA.Code,TSPL_JW_FORMULA.Structure_Code,TSPL_JW_FORMULA.Formula,TSPL_VENDOR_FORMULA_MAPPING.Posted from TSPL_VENDOR_FORMULA_MAPPING"
            strqry += " left outer join TSPL_JW_FORMULA on TSPL_JW_FORMULA.Code=TSPL_VENDOR_FORMULA_MAPPING.FormulaCode where 2=2 "
            strqry += " and TSPL_VENDOR_FORMULA_MAPPING.DocCode='" & strCode & "'"
            gvPriceCode.DataSource = clsDBFuncationality.GetDataTable(strqry)
            dt = clsDBFuncationality.GetDataTable(strqry)

            For ii As Integer = 0 To gvPriceCode.Columns.Count - 1
                gvPriceCode.Columns(ii).ReadOnly = True
                gvPriceCode.Columns(ii).IsVisible = False
            Next
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtCode.Value = dt.Rows(0)("DocCode").ToString()
                txtDate.Value = dt.Rows(0)("DocDate").ToString()
                UsLock1.Status = dt.Rows(0)("Posted").ToString()
                isNewEntry = False
                Dim posted As String = dt.Rows(0)("Posted").ToString()
                If clsCommon.CompairString(posted, "1") = CompairStringResult.Equal Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnsave.Text = "Save"
                Else
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                    btnsave.Text = "Update"
                End If

                gvPriceCode.Columns("Code").HeaderText = "Formula Code"
                gvPriceCode.Columns("Code").Width = 100
                gvPriceCode.Columns("Code").ReadOnly = True
                gvPriceCode.Columns("Code").IsVisible = True

                gvPriceCode.Columns("Structure_Code").HeaderText = "Item Structure Code"
                gvPriceCode.Columns("Structure_Code").Width = 200
                gvPriceCode.Columns("Structure_Code").ReadOnly = True
                gvPriceCode.Columns("Structure_Code").IsVisible = True

                gvPriceCode.Columns("Formula").HeaderText = "Formula"
                gvPriceCode.Columns("Formula").Width = 100
                gvPriceCode.Columns("Formula").ReadOnly = True
                gvPriceCode.Columns("Formula").IsVisible = True

                gvPriceCode.Columns("Posted").HeaderText = "Posted"
                gvPriceCode.Columns("Posted").Width = 100
                gvPriceCode.Columns("Posted").ReadOnly = True
                gvPriceCode.Columns("Posted").IsVisible = False

                gvPriceCode.AllowAddNewRow = False
                gvPriceCode.ShowGroupPanel = False
                gvPriceCode.AllowColumnReorder = False
                gvPriceCode.AllowRowReorder = False
                gvPriceCode.EnableSorting = False
                gvPriceCode.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gvPriceCode.MasterTemplate.ShowRowHeaderColumn = False

                TagMappedVendor()
            Else
                LoadPriceData()
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub TagMappedVendor()
        Dim qry As String
        Dim arr As ArrayList
        Dim dt As DataTable
        Try
            For ii As Integer = 0 To gvPriceCode.RowCount - 1
                qry = "select VendorCode from TSPL_VENDOR_FORMULA_MAPPING_detail where DocCode='" + txtCode.Value + "'"
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
                        clsCommon.MyMessageBoxShow("Vendor Added successfully")
                        LoadPriceData()
                        If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
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
            clsCommon.MyMessageBoxShow("Data saved successfully")
            LoadPriceData()
            obj = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmJWVendorFormula, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return False
            End If
        End If




        Return True
    End Function
    Sub Reset()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        isNewEntry = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadPriceData()
    End Sub
    Sub SaveData()
        Dim arr As New List(Of clsVendorFormulaMapping)
        If isNewEntry Then
            txtCode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy"), clsDocType.JWVendorFormula, "", "")
        End If
        For ii As Integer = 0 To gvPriceCode.RowCount - 1
            If clsCommon.myCdbl(gvPriceCode.Rows(ii).Cells("Posted").Value) = 0 Then
                Dim obj As New clsVendorFormulaMapping
                obj.Formulacode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("code").Value)
                obj.DocDate = txtDate.Value
                obj.DocCode = txtCode.Value
                obj.arrVendor = TryCast(gvPriceCode.Rows(ii).Tag, ArrayList)
                If obj.arrVendor IsNot Nothing AndAlso obj.arrVendor.Count > 0 Then
                    arr.Add(obj)
                End If
            End If
        Next
        If arr.Count <= 0 Then
            Throw New Exception("No Data found to Save")
        Else
            Dim objCall As New clsVendorFormulaMapping '
            objCall.SaveData(arr, isNewEntry)

            clsCommon.MyMessageBoxShow("Data saved successfully")
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No Data found to Delete")
            End If
            If myMessages.deleteConfirm() Then
                Dim arr As New List(Of clsVendorFormulaMapping)
                For ii As Integer = 0 To gvPriceCode.RowCount - 1
                    If clsCommon.myCdbl(gvPriceCode.Rows(ii).Cells("Posted").Value) = 0 Then
                        Dim obj As New clsVendorFormulaMapping
                        obj.Formulacode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Formula Code").Value)
                        arr.Add(obj)
                    End If
                Next
                If arr.Count <= 0 Then
                    Throw New Exception("No Data found to Delete")
                Else
                    Dim objCall As New clsVendorFormulaMapping()
                    objCall.DeleteData(arr)
                    objCall = Nothing
                    myMessages.delete()
                    LoadPriceData()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If myMessages.postConfirm() Then
                Dim arr As New List(Of clsVendorFormulaMapping)
                For ii As Integer = 0 To gvPriceCode.RowCount - 1
                    If clsCommon.myCdbl(gvPriceCode.Rows(ii).Cells("Posted").Value) = 0 Then
                        Dim obj As New clsVendorFormulaMapping
                        obj.DocCode = txtCode.Value
                        obj.arrVendor = TryCast(gvPriceCode.Rows(ii).Tag, ArrayList)
                        If obj.arrVendor IsNot Nothing AndAlso obj.arrVendor.Count > 0 Then
                            arr.Add(obj)
                        End If
                    End If
                Next
                If arr.Count <= 0 Then
                    Throw New Exception("No Data found to Post")
                Else
                    Dim objCall As New clsVendorFormulaMapping()
                    objCall.PostData(arr)
                    objCall = Nothing
                    myMessages.post()
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    
    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_VENDOR_FORMULA_MAPPING where DocCode='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                LoadData(clsVendorFormulaMapping.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        Reset()
    End Sub
End Class
