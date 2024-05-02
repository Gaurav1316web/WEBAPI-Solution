' ----------------- Created By Anubhooti On 02-Sep-2015 Against BM00000006776-------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions

Imports common
Imports System.IO
Imports XpertERPEngine

Public Class FrmServiceEnquiry
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
#Region "Main Items"
    Const ColSNo As String = "SNo"
    Const ColItemCode As String = "Main Item"
    Const ColItemDesp As String = "ItemDesp"
    Const ColChildItemCode As String = "Item Code"
    Const ColItemSerialNo As String = "Serial No."
    Const ColMainItemSerialNo As String = "Main Sr No"
    Const ColItemType As String = "Item Type"
    Const ColIssueCode As String = "Issue Code"
    Const ColIssueName As String = "Issue Desp"
    Const ColItemPartNo As String = "ItemPartNo"
    Const ColIsSerial As String = "Is Serial"
    Const ColWarrStatus As String = "Warranty Status"
    Const ColChargeStatus As String = "Charge Status"
    Const ColRevisionNo As String = "Revision No"
#End Region
#Region "Child Items"
    Const ColCSNo As String = "SNo"
    Const ColCItemCode As String = "Child Item"
    Const ColCItemDesp As String = "ItemDesp"
    Const ColCItemSerialNo As String = "Serial No."
    Const ColCItemType As String = "Item Type"
    Const ColCIssueCode As String = "Issue Code"
    Const ColCIssueName As String = "Issue Desp"
    Const ColCItemPartNo As String = "ItemPartNo"
    Const ColCIsSerial As String = "Is Serial"
    Const ColCMainItemCode As String = "Main Item"
    Const ColCWarrStatus As String = "Warranty Status"
    Const ColCChargeStatus As String = "Charge Status"
    Const ColCRevisionNo As String = "Revision No"
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmServiceEnquiry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnsave.Focus()

            If clsCommon.myLen(TxtCustGrp.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill customer group")
                TxtCustGrp.Focus()
                errorControl.SetError(TxtCustGrp, "Customer Group must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtCustGrp, "")
            End If

            If clsCommon.myLen(TxtDealer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill dealer")
                TxtDealer.Focus()
                errorControl.SetError(TxtDealer, "Dealer must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtDealer, "")
            End If

            If clsCommon.myLen(TxtVehicleName.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill vehicle name")
                TxtVehicleName.Focus()
                errorControl.SetError(TxtVehicleName, "Vehicle name must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtVehicleName, "")
            End If

            If clsCommon.myLen(TxtItemPartNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill item part no.")
                TxtItemPartNo.Focus()
                errorControl.SetError(TxtItemPartNo, "Item part no. must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtItemPartNo, "")
            End If

            If clsCommon.myLen(TxtIssueNotice.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill issue notice")
                TxtIssueNotice.Focus()
                errorControl.SetError(TxtIssueNotice, "Issue notice must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtIssueNotice, "")
            End If
            '' Main Items
            Dim GridRowQual As Integer = 0
            Dim HighQual As Integer = 0
            Dim QLineNo As Integer = 1
            Dim RowNo As Integer = 0

            For Each grow As GridViewRowInfo In gvMainItem.Rows
                QLineNo += 1
                If clsCommon.myLen(grow.Cells(ColItemCode).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(ColIssueCode).Value) <= 0 Then
                        Me.RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Please fill issue code at line no. " + clsCommon.myCstr(QLineNo) + "")
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColItemSerialNo).Value)) > 0 Then
                        RowNo = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select COUNT(*) AS RowNo From TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM " & _
                                                                                " WHERE (TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Serial_No='" & clsCommon.myCstr(grow.Cells(ColItemSerialNo).Value) & "' AND TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Main_Item_Code ='" & clsCommon.myCstr(grow.Cells(ColItemCode).Value) & "') AND TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Service_Enquiry_Code <>'" & txtcode.Value & "'"))
                        If RowNo >= 1 Then
                            Throw New Exception("Serial No. - " & clsCommon.myCstr(grow.Cells(ColItemSerialNo).Value) & " is in process")
                        End If
                    End If

                    GridRowQual = GridRowQual + 1
                End If

            Next
            If GridRowQual <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Enter at least one issue in main item tab")
            End If

            '' Child Items
            Dim GridRowChk As Integer = 0
            If gvChildItem.Rows.Count > 1 Then
                For Each grow As GridViewRowInfo In gvChildItem.Rows
                    If clsCommon.myLen(grow.Cells(ColCIssueCode).Value) > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColCItemSerialNo).Value)) > 0 Then
                            RowNo = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select COUNT(*) AS RowNo From TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM " & _
                                                                                    " WHERE (TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Serial_No='" & clsCommon.myCstr(grow.Cells(ColItemSerialNo).Value) & "' AND TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Child_Item_Code ='" & clsCommon.myCstr(grow.Cells(ColItemCode).Value) & "') AND TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Service_Enquiry_Code <>'" & txtcode.Value & "'"))
                            If RowNo >= 1 Then
                                Throw New Exception("Serial No. - " & clsCommon.myCstr(grow.Cells(ColCItemSerialNo).Value) & " is in process")
                            End If
                        End If

                        GridRowChk = GridRowChk + 1
                    End If
                Next
            End If

            If GridRowChk <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage4
                Throw New Exception("Enter at least one issue in child item tab")
            End If


            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub FunReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        TxtCustGrp.Value = ""
        LblCustGrp.Text = ""
        TxtDealer.Value = ""
        LblDealer.Text = ""
        TxtVehicleName.Value = ""
        LblVehicleName.Text = ""
        TxtChEngNo.Text = ""
        TxtItemPartNo.Value = ""
        LblItemPartNo.Text = ""
        TxtRemarks.Text = ""
        TxtIssueNotice.Value = ""
        LblIssueNotice.Text = ""
        TxtCallNo.Value = ""
        LblCallNo.Text = ""
        TxtCustGrp.Enabled = True
        TxtDealer.Enabled = True
        TxtItemPartNo.Enabled = True
        TxtIssueNotice.Enabled = True
        TxtVehicleName.Enabled = True
        LblAllocatedTo.Text = ""
        PnlGreen.BackColor = Color.LightSalmon
        LblAllTop.Text = "Not Allocated"
        Me.RadPageView1.SelectedPage = RadPageViewPage3

        dtpDate.Value = clsCommon.GETSERVERDATE()
        dtpDateofSale.Value = clsCommon.GETSERVERDATE()

        '' Blank Grid
        gvMainItem.DataSource = Nothing
        gvMainItem.Rows.Clear()
        gvMainItem.Columns.Clear()
        gvChildItem.DataSource = Nothing
        gvChildItem.Rows.Clear()
        gvChildItem.Columns.Clear()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If

        FunDelete()
    End Sub
    Sub FunDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsServiceEnquiry.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    FunReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub LoadDropDowns()
        Dim repoWarrStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoWarrStatus.FormatString = ""
        repoWarrStatus.HeaderText = "Warranty Status"
        repoWarrStatus.Name = ColWarrStatus
        repoWarrStatus.Width = 100
        repoWarrStatus.ReadOnly = True
        repoWarrStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoWarrStatus.DataSource = ClsServiceEnquiry.GetWarranty()
        repoWarrStatus.ValueMember = "Code"
        repoWarrStatus.DisplayMember = "Name"
        gvMainItem.MasterTemplate.Columns.Add(repoWarrStatus) '5
    End Sub
    Sub LoadFromBOM(ByVal VehicleItemCode As String, ByVal StrCode As String)
        Dim Qry As String = String.Empty
        Dim ExpDate As String = String.Empty

        If clsCommon.myLen(StrCode) > 0 Then
            Qry = " Select ROW_NUMBER() OVER (ORDER BY TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Service_Enquiry_Code)  AS [S.No],TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Item_Code As [Item Code],TSPL_ITEM_MASTER.Is_Serial_Item AS [Is Serial],TSPL_ITEM_MASTER.Item_Type As [Item Type] ,TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Main_Item_Code AS [Main Item],TSPL_ITEM_MASTER.Item_Desc AS [Item Description],ISNULL(TSPL_ITEM_MASTER.Part_No,'') AS [Part No.], ISNULL(TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Serial_No,'') as [Serial No.],ISNULL(TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Issued_Code,'') as [Issue Code],TSPL_SW_FAULT_MASTER.Fault_Master_Name AS [Issue Desp],TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Main_Serial_No AS [Main Sr No],TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.BOM_Revision_No As [Revision No],TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Warranty_Status As [Warranty Status1],TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Charge_Status As [Charge Status1] From TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Main_Item_Code " & _
                  " LEFT OUTER JOIN TSPL_SW_FAULT_MASTER ON TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Issued_Code = TSPL_SW_FAULT_MASTER.Fault_Master_Code " & _
                  " WHERE  TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM.Service_Enquiry_Code='" & StrCode & "' "
        Else
            ' Qry = "SELECT ROW_NUMBER() OVER (ORDER BY TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE)  AS [S.No],TSPL_ITEM_MASTER.Is_Serial_Item AS [Is Serial],TSPL_ITEM_MASTER.Item_Type As [Item Type], TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS [Main Item], ITEM_DESCRIPTION AS [Item Description],ISNULL(TSPL_ITEM_MASTER.Part_No,'') AS [Part No.],'' AS [Serial No.],'' As [Issue Code],'' AS [Issue Desp],ISNULL(TSPL_MF_BOM_HEAD.REVISION_NO,'') AS [Revision No] FROM TSPL_MF_BOM_DETAIL LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.BOM_CODE =TSPL_MF_BOM_DETAIL.BOM_CODE LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE WHERE TSPL_MF_BOM_HEAD.PROD_ITEM_CODE = '" + VehicleItemCode + " '"
            Qry = " Select  ROW_NUMBER() Over (order by TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Serial_No)  AS [S.No], TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code  AS [Main Item],TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code AS [Item Code],TSPL_ITEM_MASTER.Item_Desc As [Item Description],TSPL_ITEM_MASTER.Item_Type As [Item Type] " & _
                  " ,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Serial_No As [Serial No.],TSPL_ITEM_MASTER.Is_Serial_Item AS [Is Serial],ISNULL(TSPL_ITEM_MASTER.Part_No,'') AS [Part No.],TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Serial_No AS [Main Sr No],'' As [Issue Code],'' AS [Issue Desp],TSPL_MF_BOM_HEAD.REVISION_NO AS [Revision No] " & _
                  " From TSPL_MF_PRINCIPLE_RECEIPT_DETAIL " & _
                  " LEFT OUTER JOIN TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL ON TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code= TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code  = TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code " & _
                  " LEFT OUTER JOIN TSPL_MF_BOM_HEAD  ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE 	=  TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code  " & _
                  " WHERE TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code  ='" & VehicleItemCode & "' AND TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Serial_No='" & LblSerialNo.Text & "' AND TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Serial_No ='" & LblSerialNo.Text & "' "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        'dt.Columns.Add("Warranty Status", GetType(ComboBox))

        'dt.AcceptChanges()

        gvMainItem.DataSource = Nothing
        gvMainItem.Rows.Clear()
        gvMainItem.Columns.Clear()

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvMainItem.DataSource = dt

            '    If clsCommon.myLen(StrCode) <= 0 Then
            '' Warranty Status
            Dim repocombo As New GridViewComboBoxColumn()
            repocombo.FormatString = ""
            repocombo.HeaderText = "Warranty Status"
            repocombo.Name = ColWarrStatus
            repocombo.DataSource = ClsServiceEnquiry.GetWarranty()
            repocombo.ValueMember = "Code"
            repocombo.DisplayMember = "Name"
            repocombo.Width = 100
            gvMainItem.MasterTemplate.Columns.Add(repocombo)

            '' Charge Status
            Dim repochargeS As New GridViewComboBoxColumn()
            repochargeS.FormatString = ""
            repochargeS.HeaderText = "Charge Status"
            repochargeS.Name = ColChargeStatus
            repochargeS.DataSource = ClsServiceEnquiry.GetCharge()
            repochargeS.ValueMember = "Code"
            repochargeS.DisplayMember = "Name"
            repochargeS.Width = 80
            gvMainItem.MasterTemplate.Columns.Add(repochargeS)
            'End If


            gvMainItem.Columns("Item Code").Width = 120
            gvMainItem.Columns("Main Item").Width = 65
            gvMainItem.Columns("Main Item").ReadOnly = True
            gvMainItem.Columns("Item Description").ReadOnly = True
            gvMainItem.Columns("Item Description").Width = 200
            gvMainItem.Columns("Part No.").ReadOnly = True
            gvMainItem.Columns("Part No.").Width = 70
            gvMainItem.Columns("S.No").ReadOnly = True
            gvMainItem.Columns("S.No").Width = 50
            gvMainItem.Columns("Issue Desp").ReadOnly = True
            gvMainItem.Columns("Issue Desp").Width = 150
            gvMainItem.Columns("Issue Code").Width = 70
            gvMainItem.Columns("Serial No.").Width = 175
            gvMainItem.Columns("Serial No.").ReadOnly = True
            gvMainItem.Columns("Item Type").ReadOnly = True
            gvMainItem.Columns("Item Type").IsVisible = False
            gvMainItem.Columns("Is Serial").ReadOnly = True
            gvMainItem.Columns("Is Serial").IsVisible = False
            gvMainItem.Columns("Revision No").ReadOnly = True
            gvMainItem.Columns("Revision No").IsVisible = False
            gvMainItem.Columns("Main Sr No").Width = 100
            gvMainItem.Columns("Main Sr No").ReadOnly = True
            gvMainItem.Columns("Warranty Status").Width = 100
            gvMainItem.Columns("Charge Status").Width = 80

            Dim TableName As String = String.Empty
            Dim AppliedDate As String = String.Empty
            Dim RowCount As Integer = 0
            If clsCommon.myLen(StrCode) <= 0 Then
                For Each grow As GridViewRowInfo In gvMainItem.Rows
                    AppliedDate = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Warranty_Applied_From,'') AS Warranty_Applied_From FROM TSPL_ITEM_MASTER WHERE ITEM_CODE ='" & clsCommon.myCstr(grow.Cells(ColItemCode).Value) & "'"))
                    If clsCommon.myLen(AppliedDate) > 0 Then
                        If clsCommon.CompairString(AppliedDate, "M") = CompairStringResult.Equal Then
                            ExpDate = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select CAST( ExpDate AS DATE) AS ExpDate From  ( SELECT CONVERT(DATE, ISNULL(DATEADD(day,ISNULL(TSPL_WARRANTY_MASTER.Warranty_Days,0),TSPL_MF_RECEIPT.RECEIPT_DATE),''),103) AS ExpDate FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_MF_RECEIPT_DETAIL ON TSPL_MF_RECEIPT_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_MF_RECEIPT ON TSPL_MF_RECEIPT.RECEIPT_CODE =TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  = TSPL_MF_RECEIPT_DETAIL.Item_Code  LEFT OUTER JOIN TSPL_WARRANTY_MASTER ON TSPL_WARRANTY_MASTER.Code = TSPL_ITEM_MASTER.WARRANTY_CODE  WHERE TSPL_ITEM_MASTER.Item_Code ='" & clsCommon.myCstr(grow.Cells(ColItemCode).Value) & "' AND LEN(TSPL_ITEM_MASTER.WARRANTY_CODE) > 0  AND TSPL_MF_BOM_HEAD.REVISION_NO ='" & clsCommon.myCstr(grow.Cells(ColRevisionNo).Value) & "' ) XX WHERE CONVERT(VARCHAR, ExpDate ,103) >  '" & dtpDate.Value & "'"))
                        Else
                            ExpDate = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select CAST( ExpDate AS DATE) AS ExpDate From ( SELECT ISNULL(DATEADD(day,TSPL_WARRANTY_MASTER.Warranty_Days,TSPL_SD_SALE_INVOICE_HEAD.Document_Date),'') AS ExpDate FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN TSPL_WARRANTY_MASTER ON TSPL_WARRANTY_MASTER.Code = TSPL_ITEM_MASTER.WARRANTY_CODE  WHERE TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='ALL' AND TSPL_ITEM_MASTER.Item_Code ='" & clsCommon.myCstr(grow.Cells(ColItemCode).Value) & "' AND LEN(TSPL_ITEM_MASTER.WARRANTY_CODE) > 0 AND TSPL_MF_BOM_HEAD.REVISION_NO ='" & clsCommon.myCstr(grow.Cells(ColRevisionNo).Value) & "' ) XX WHERE CONVERT(VARCHAR, ExpDate ,103) > '" & dtpDate.Value & "'"))
                        End If
                    End If

                    If clsCommon.myLen(ExpDate) > 0 Then
                        gvMainItem.Rows(RowCount).Cells(ColWarrStatus).Value = "UW"
                        gvMainItem.Rows(RowCount).Cells(ColChargeStatus).Value = "FOC"
                    Else
                        gvMainItem.Rows(RowCount).Cells(ColWarrStatus).Value = "NA"
                        gvMainItem.Rows(RowCount).Cells(ColChargeStatus).Value = "C"
                    End If
                    RowCount += 1
                Next
            Else
                For Each grow As GridViewRowInfo In gvMainItem.Rows
                    grow.Cells(ColWarrStatus).Value = grow.Cells("Warranty Status1").Value
                    grow.Cells(ColChargeStatus).Value = grow.Cells("Charge Status1").Value
                Next
                gvMainItem.Columns("Warranty Status1").IsVisible = False
                gvMainItem.Columns("Charge Status1").IsVisible = False
                gvMainItem.Columns("Warranty Status1").VisibleInColumnChooser = False
                gvMainItem.Columns("Charge Status1").VisibleInColumnChooser = False
            End If
            ' gvMainItem.AllowDeleteRow = False
            gvMainItem.AllowAddNewRow = False
            gvMainItem.ShowGroupPanel = False
            gvMainItem.AllowColumnReorder = False
            gvMainItem.AllowRowReorder = False
            gvMainItem.EnableSorting = False
            gvMainItem.EnableGrouping = False

            ' gvMainItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvMainItem.MasterTemplate.ShowRowHeaderColumn = False
            gvMainItem.TableElement.TableHeaderHeight = 40
            'gvMainItem.BestFitColumns()

            '' Load Child Items 
            Dim MainItemCode As String = String.Empty
            Dim MainItemTotal As String = String.Empty

            If clsCommon.myLen(StrCode) <= 0 Then
                For Each dr As DataRow In dt.Rows
                    If (clsCommon.CompairString(clsCommon.myCstr(dr("Item Type")), "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr("Item Type")), "S") = CompairStringResult.Equal) AndAlso clsCommon.myLen(clsCommon.myCstr(dr("Item Type"))) > 0 Then
                        'MainItemCode = clsCommon.myCstr(dr("Main Item"))
                        MainItemCode = clsCommon.myCstr(dr("Item Code"))
                        If clsCommon.myLen(MainItemCode) > 0 Then
                            MainItemTotal = MainItemTotal + "," + "'" + MainItemCode + "'"
                        End If
                    End If
                Next
                If MainItemTotal.Length > 0 Then
                    If MainItemTotal.Substring(0, 1) = "," Then
                        MainItemTotal = MainItemTotal.Substring(1, MainItemTotal.Length - 1)
                    End If
                End If
            End If

            If clsCommon.myLen(MainItemTotal) > 0 OrElse clsCommon.myLen(StrCode) > 0 Then
                LoadChildItems(MainItemTotal, StrCode)
            Else
                gvChildItem.DataSource = Nothing
                gvChildItem.Rows.Clear()
                gvChildItem.Columns.Clear()
            End If
        Else
            Me.gvMainItem.Rows.Clear()
            Me.gvMainItem.Rows.AddNew()
        End If
    End Sub
    Sub LoadChildItems(ByVal MainItemCodes As String, ByVal StrCode As String)
        Dim Qry As String = String.Empty

        If clsCommon.myLen(StrCode) > 0 Then
            Qry = " Select ROW_NUMBER() OVER (ORDER BY TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Service_Enquiry_Code)  AS [S.No],TSPL_ITEM_MASTER.Is_Serial_Item AS [Is Serial] ,TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Main_Item_Code AS [Main Item],TSPL_ITEM_MASTER.Item_Type As [Item Type],TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Child_Item_Code AS [Child Item],TSPL_ITEM_MASTER.Item_Desc AS [Item Description],ISNULL(TSPL_ITEM_MASTER.Part_No,'') AS [Part No.], ISNULL(TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Serial_No,'') as [Serial No.],ISNULL(TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Issued_Code,'') as [Issue Code],TSPL_SW_FAULT_MASTER.Fault_Master_Name AS [Issue Desp],TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.BOM_Revision_No As [Revision No],TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Warranty_Status AS [Warranty Status1],TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Charge_Status As [Charge Status1] From TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Child_Item_Code " & _
                  " LEFT OUTER JOIN TSPL_SW_FAULT_MASTER ON TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Issued_Code = TSPL_SW_FAULT_MASTER.Fault_Master_Code " & _
                  " WHERE TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM.Service_Enquiry_Code='" & StrCode & "'"
        Else
            Qry = "SELECT ROW_NUMBER() OVER (ORDER BY TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE)  AS [S.No],TSPL_ITEM_MASTER.Is_Serial_Item AS [Is Serial],TSPL_MF_BOM_HEAD.PROD_ITEM_CODE As [Main Item],TSPL_ITEM_MASTER.Item_Type As [Item Type], TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS [Child Item], ITEM_DESCRIPTION AS [Item Description],ISNULL(TSPL_ITEM_MASTER.Part_No,'') AS [Part No.],'' AS [Serial No.],'' As [Issue Code],'' AS [Issue Desp],ISNULL(TSPL_MF_BOM_HEAD.REVISION_NO,'') AS [Revision No] FROM TSPL_MF_BOM_DETAIL LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.BOM_CODE =TSPL_MF_BOM_DETAIL.BOM_CODE LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE WHERE TSPL_MF_BOM_HEAD.PROD_ITEM_CODE IN (" + MainItemCodes + ")"
        End If


        Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)

        gvChildItem.DataSource = Nothing
        gvChildItem.Rows.Clear()
        gvChildItem.Columns.Clear()

        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
            gvChildItem.DataSource = DT

            '' Child Warranty Status
            Dim repoCcombo As New GridViewComboBoxColumn()
            repoCcombo.FormatString = ""
            repoCcombo.HeaderText = "Warranty Status"
            repoCcombo.Name = ColCWarrStatus
            repoCcombo.DataSource = ClsServiceEnquiry.GetWarranty()
            repoCcombo.ValueMember = "Code"
            repoCcombo.DisplayMember = "Name"
            repoCcombo.Width = 100
            gvChildItem.MasterTemplate.Columns.Add(repoCcombo)

            '' Child Charge Status
            Dim repoCchargeS As New GridViewComboBoxColumn()
            repoCchargeS.FormatString = ""
            repoCchargeS.HeaderText = "Charge Status"
            repoCchargeS.Name = ColCChargeStatus
            repoCchargeS.DataSource = ClsServiceEnquiry.GetCharge()
            repoCchargeS.ValueMember = "Code"
            repoCchargeS.DisplayMember = "Name"
            repoCchargeS.Width = 80
            gvChildItem.MasterTemplate.Columns.Add(repoCchargeS)

            gvChildItem.Columns("Main Item").Width = 70
            gvChildItem.Columns("Main Item").ReadOnly = True
            gvChildItem.Columns("Child Item").Width = 100
            gvChildItem.Columns("Child Item").ReadOnly = True
            gvChildItem.Columns("Item Description").ReadOnly = True
            gvChildItem.Columns("Item Description").Width = 160
            gvChildItem.Columns("Part No.").ReadOnly = True
            gvChildItem.Columns("Part No.").Width = 75
            gvChildItem.Columns("S.No").ReadOnly = True
            gvChildItem.Columns("S.No").Width = 50
            gvChildItem.Columns("Issue Desp").ReadOnly = True
            gvChildItem.Columns("Issue Desp").Width = 160
            gvChildItem.Columns("Issue Code").Width = 70
            gvChildItem.Columns("Serial No.").Width = 80
            gvChildItem.Columns("Item Type").ReadOnly = True
            gvChildItem.Columns("Item Type").IsVisible = False
            gvChildItem.Columns("Is Serial").ReadOnly = True
            gvChildItem.Columns("Is Serial").IsVisible = False
            gvChildItem.Columns("Revision No").ReadOnly = True
            gvChildItem.Columns("Revision No").IsVisible = False
            gvChildItem.Columns("Warranty Status").Width = 100
            gvChildItem.Columns("Charge Status").Width = 80

            Dim ExpDate As String = String.Empty
            Dim RowCount As Integer = 0
            If clsCommon.myLen(StrCode) <= 0 Then
                For Each grow As GridViewRowInfo In gvChildItem.Rows

                    ExpDate = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select CAST( ExpDate AS DATE) AS ExpDate From ( SELECT ISNULL(DATEADD(day,TSPL_WARRANTY_MASTER.Warranty_Days,TSPL_SD_SALE_INVOICE_HEAD.Document_Date),'') AS ExpDate FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN TSPL_WARRANTY_MASTER ON TSPL_WARRANTY_MASTER.Code = TSPL_ITEM_MASTER.WARRANTY_CODE  WHERE TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='ALL' AND TSPL_ITEM_MASTER.Item_Code ='" & clsCommon.myCstr(grow.Cells(ColItemCode).Value) & "' AND LEN(TSPL_ITEM_MASTER.WARRANTY_CODE) > 0 AND TSPL_MF_BOM_HEAD.REVISION_NO ='" & clsCommon.myCstr(grow.Cells(ColRevisionNo).Value) & "') XX WHERE CONVERT(VARCHAR, ExpDate ,103) > '" & dtpDate.Value & "'"))
                    If clsCommon.myLen(ExpDate) > 0 Then
                        gvChildItem.Rows(RowCount).Cells(ColCWarrStatus).Value = "UW"
                        gvChildItem.Rows(RowCount).Cells(ColCChargeStatus).Value = "FOC"
                    Else
                        gvChildItem.Rows(RowCount).Cells(ColCWarrStatus).Value = "NA"
                        gvChildItem.Rows(RowCount).Cells(ColCChargeStatus).Value = "C"
                    End If
                    RowCount += 1
                Next
            Else
                For Each grow As GridViewRowInfo In gvChildItem.Rows
                    grow.Cells(ColWarrStatus).Value = grow.Cells("Warranty Status1").Value
                    grow.Cells(ColChargeStatus).Value = grow.Cells("Charge Status1").Value
                Next
                gvChildItem.Columns("Warranty Status1").IsVisible = False
                gvChildItem.Columns("Charge Status1").IsVisible = False
                gvChildItem.Columns("Warranty Status1").VisibleInColumnChooser = False
                gvChildItem.Columns("Charge Status1").VisibleInColumnChooser = False
            End If

            gvChildItem.AllowAddNewRow = False
            gvChildItem.ShowGroupPanel = False
            gvChildItem.AllowColumnReorder = False
            gvChildItem.AllowRowReorder = False
            gvChildItem.EnableSorting = False
            gvChildItem.MasterTemplate.ShowRowHeaderColumn = False
            gvChildItem.TableElement.TableHeaderHeight = 40
        Else
            Me.gvChildItem.Rows.Clear()
            Me.gvChildItem.Rows.AddNew()
        End If
    End Sub

    Public Sub Save()
        Try

            If AllowToSave() Then

                Dim arr As New List(Of ClsServiceEnquiry)
                Dim obj As New ClsServiceEnquiry()
                obj.Service_Enquiry_Code = clsCommon.myCstr(txtcode.Value)
                obj.Cust_Group_Code = clsCommon.myCstr(TxtCustGrp.Value)
                obj.Service_Enquiry_Date = dtpDate.Value
                obj.Dealer_Code = clsCommon.myCstr(TxtDealer.Value)
                obj.DateOfSale = dtpDateofSale.Value
                obj.EngineNo = clsCommon.myCstr(TxtChEngNo.Text)
                obj.Item_Part_No = clsCommon.myCstr(TxtItemPartNo.Value)
                obj.Remarks = clsCommon.myCstr(TxtRemarks.Text)
                obj.Issued_Notice = clsCommon.myCstr(TxtIssueNotice.Value)
                obj.Vehicle_Code = clsCommon.myCstr(TxtVehicleName.Value)
                '' Main Item 
                obj.ObjList = New List(Of ClsServiceEnquiryMainItem)
                For Each grow As GridViewRowInfo In gvMainItem.Rows
                    If clsCommon.myLen(grow.Cells(ColItemCode).Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr As New ClsServiceEnquiryMainItem()
                    objTr.Service_Enquiry_Code = clsCommon.myCstr(Me.txtcode.Value)
                    objTr.Main_Item_Code = clsCommon.myCstr(grow.Cells(ColItemCode).Value)
                    objTr.Child_Item_Code = clsCommon.myCstr(grow.Cells(ColChildItemCode).Value)
                    objTr.Main_Serial_No = clsCommon.myCstr(grow.Cells(ColMainItemSerialNo).Value)
                    objTr.Serial_No = clsCommon.myCstr(grow.Cells(ColItemSerialNo).Value)
                    objTr.Issued_Code = clsCommon.myCstr(grow.Cells(ColIssueCode).Value)
                    objTr.BOM_Revision_No = clsCommon.myCstr(grow.Cells(ColRevisionNo).Value)
                    objTr.Warranty_Status = clsCommon.myCstr(grow.Cells(ColWarrStatus).Value)
                    objTr.Charge_Status = clsCommon.myCstr(grow.Cells(ColChargeStatus).Value)
                    obj.ObjList.Add(objTr)
                Next

                '' Child Item 
                obj.ObjChildL = New List(Of ClsServiceEnquiryChildItem)
                For Each grow As GridViewRowInfo In gvChildItem.Rows
                    If clsCommon.myLen(grow.Cells(ColCItemCode).Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr1 As New ClsServiceEnquiryChildItem()
                    objTr1.Service_Enquiry_Code = clsCommon.myCstr(Me.txtcode.Value)
                    objTr1.Main_Item_Code = clsCommon.myCstr(grow.Cells(ColCMainItemCode).Value)
                    objTr1.Serial_No = clsCommon.myCstr(grow.Cells(ColCItemSerialNo).Value)
                    objTr1.Issued_Code = clsCommon.myCstr(grow.Cells(ColCIssueCode).Value)
                    objTr1.Child_Item_Code = clsCommon.myCstr(grow.Cells(ColCItemCode).Value)
                    objTr1.BOM_Revision_No = clsCommon.myCstr(grow.Cells(ColCRevisionNo).Value)
                    objTr1.Warranty_Status = clsCommon.myCstr(grow.Cells(ColCWarrStatus).Value)
                    objTr1.Charge_Status = clsCommon.myCstr(grow.Cells(ColCChargeStatus).Value)
                    obj.ObjChildL.Add(objTr1)
                Next
                arr.Add(obj)
                If (ClsServiceEnquiry.SaveData(arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Service_Enquiry_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            txtcode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False

            Dim obj As New ClsServiceEnquiry()
            obj = ClsServiceEnquiry.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Service_Enquiry_Code) > 0) Then
                FunReset()
                isNewEntry = False
                btnsave.Text = "Update"
                btndelete.Enabled = True

                txtcode.Value = obj.Service_Enquiry_Code
                TxtCustGrp.Value = obj.Cust_Group_Code
                If clsCommon.myLen(obj.Cust_Group_Code) > 0 Then
                    LblCustGrp.Text = clsDBFuncationality.getSingleValue("Select isnull(Cust_Group_Desc,'') As Cust_Group_Desc From TSPL_CUSTOMER_GROUP_MASTER Where Cust_Group_Code ='" + obj.Cust_Group_Code + "'")
                Else
                    LblCustGrp.Text = ""
                End If
                dtpDate.Value = obj.Service_Enquiry_Date
                TxtDealer.Value = obj.Dealer_Code

                If clsCommon.myLen(TxtDealer.Value) > 0 Then
                    LblDealer.Text = clsDBFuncationality.getSingleValue("Select isnull(Customer_Name,'') As Customer_Name From TSPL_CUSTOMER_MASTER Where Cust_Code ='" + TxtDealer.Value + "'")
                Else
                    LblDealer.Text = ""
                End If
                TxtVehicleName.Value = obj.Vehicle_Code
                If clsCommon.myLen(TxtVehicleName.Value) > 0 Then
                    LblVehicleName.Text = clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') As Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" + TxtVehicleName.Value + "'")
                Else
                    LblVehicleName.Text = ""
                End If
                dtpDateofSale.Value = obj.DateOfSale
                TxtChEngNo.Text = obj.EngineNo
                TxtRemarks.Text = obj.Remarks
                TxtItemPartNo.Value = obj.Item_Part_No
                TxtIssueNotice.Value = obj.Issued_Notice
                If clsCommon.myLen(TxtItemPartNo.Value) > 0 Then
                    LblItemPartNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE Item_Code='" + TxtItemPartNo.Value + "'"))
                Else
                    LblItemPartNo.Text = ""
                End If
                If clsCommon.myLen(TxtIssueNotice.Value) > 0 Then
                    LblIssueNotice.Text = clsDBFuncationality.getSingleValue("Select isnull(Fault_Master_Name,'') As Fault_Master_Name From TSPL_SW_FAULT_MASTER Where Fault_Master_Code='" + TxtIssueNotice.Value + "'")
                Else
                    LblIssueNotice.Text = ""
                End If
                If clsCommon.myLen(LblAllocatedTo.Text) > 0 Then
                    PnlGreen.BackColor = Color.LightGreen
                    LblAllTop.Text = "Allocated"
                Else
                    PnlGreen.BackColor = Color.LightSalmon
                    LblAllTop.Text = "Not Allocated"
                End If
                Dim ServiceAll As String = String.Empty
                Dim ServiceCall As String = String.Empty
                Dim AllName As String = String.Empty
                Dim CallName As String = String.Empty

                ServiceAll = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Engineer_Code FROM TSPL_SW_SERVICE_ALLOCATION WHERE Service_Enquiry_Code ='" + txtcode.Value + "'"))
                ServiceCall = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Assigned_To FROM TSPL_SW_SERVICE_CALL WHERE Service_Call_Code  ='" + txtcode.Value + "'"))
                AllName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + clsCommon.myCstr(ServiceAll) + "'"))
                CallName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + clsCommon.myCstr(ServiceCall) + "'"))

                If clsCommon.myLen(ServiceAll) > 0 Then
                    LblAllocatedTo.Text = ServiceAll + " (" + AllName + ") "
                    PnlGreen.BackColor = Color.LightGreen
                    LblAllTop.Text = "Allocated"
                ElseIf clsCommon.myLen(TxtCallNo.Value) > 0 Then
                    LblAllocatedTo.Text = ServiceCall + " (" + CallName + ") "
                    PnlGreen.BackColor = Color.LightGreen
                    LblAllTop.Text = "Allocated"
                Else
                    LblAllocatedTo.Text = ""
                    PnlGreen.BackColor = Color.LightSalmon
                    LblAllTop.Text = "Not Allocated"
                End If
                '' Main Item
                LoadFromBOM("", obj.Service_Enquiry_Code)

                txtcode.MyReadOnly = True
            Else
                isNewEntry = True
                FunReset()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub OpenItemSerialNo(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColItemSerialNo).Value)) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColIsSerial).Value), "1") = CompairStringResult.Equal Then
                Dim qry As String = " Select Auto_Sr_No AS Code,Document_Code AS [Document No],CONVERT(VARCHAR,Document_Date,103) AS [Document Date],Document_Type AS [Document Type] ,In_Out_Type AS [In Out Type] From TSPL_SERIAL_ITEM "
                gvMainItem.CurrentRow.Cells(ColItemSerialNo).Value = clsCommon.ShowSelectForm("SWMSerItem", qry, "Code", " Document_Type = 'ISSTRAN' AND Item_Code='" & clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColItemCode).Value) & "' ", clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColItemSerialNo).Value), "Code", isButtonClick)
            Else
                gvMainItem.CurrentRow.Cells(ColItemSerialNo).Value = ""
                clsCommon.MyMessageBoxShow("This item " & clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColItemCode).Value) & " is not a serial item.")
            End If
        End If
    End Sub
    Sub OpenItemIssueNo(ByVal isButtonClick As Boolean)
        Dim qry As String = " SELECT Fault_Master_Code AS Code ,Fault_Master_Name AS Name,Fault_Category_Code AS [Fault Category Code] FROM TSPL_SW_FAULT_MASTER "
        gvMainItem.CurrentRow.Cells(ColIssueCode).Value = clsCommon.ShowSelectForm("SWMIssIt", qry, "Code", "", clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColIssueCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvMainItem.CurrentRow.Cells(ColIssueCode).Value) > 0 Then
            gvMainItem.CurrentRow.Cells(ColIssueName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Fault_Master_Name AS Name FROM TSPL_SW_FAULT_MASTER WHERE Fault_Master_Code ='" + clsCommon.myCstr(gvMainItem.CurrentRow.Cells(ColIssueCode).Value) + "'"))
        Else
            gvMainItem.CurrentRow.Cells(ColIssueName).Value = ""
        End If
    End Sub
    Sub OpenCItemSerialNo(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCItemSerialNo).Value)) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCIsSerial).Value), "1") = CompairStringResult.Equal Then
                Dim qry As String = " Select Auto_Sr_No AS Code,Document_Code AS [Document No],CONVERT(VARCHAR,Document_Date,103) AS [Document Date],Document_Type AS [Document Type] ,In_Out_Type AS [In Out Type] From TSPL_SERIAL_ITEM "
                gvChildItem.CurrentRow.Cells(ColCItemSerialNo).Value = clsCommon.ShowSelectForm("SWCSerItem", qry, "Code", " Document_Type = 'ISSTRAN' AND Item_Code='" & clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCItemCode).Value) & "' ", clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCItemSerialNo).Value), "Code", isButtonClick)
            Else
                gvChildItem.CurrentRow.Cells(ColCItemSerialNo).Value = ""
                clsCommon.MyMessageBoxShow("This item " & clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCItemCode).Value) & " is not a serial item.")
            End If
        End If
    End Sub
    Sub OpenCItemIssueNo(ByVal isButtonClick As Boolean)
        Dim qry As String = " SELECT Fault_Master_Code AS Code ,Fault_Master_Name AS Name,Fault_Category_Code AS [Fault Category Code] FROM TSPL_SW_FAULT_MASTER "
        gvChildItem.CurrentRow.Cells(ColCIssueCode).Value = clsCommon.ShowSelectForm("SWCIssIt", qry, "Code", "", clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCIssueCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvChildItem.CurrentRow.Cells(ColCIssueCode).Value) > 0 Then
            gvChildItem.CurrentRow.Cells(ColCIssueName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Fault_Master_Name AS Name FROM TSPL_SW_FAULT_MASTER WHERE Fault_Master_Code ='" + clsCommon.myCstr(gvChildItem.CurrentRow.Cells(ColCIssueCode).Value) + "'"))
        Else
            gvChildItem.CurrentRow.Cells(ColCIssueName).Value = ""
        End If
    End Sub
#End Region
    Private Sub TxtCustGrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCustGrp._MYValidating
        Try
            TxtCustGrp.Value = clsCustomerGroupMaster.getFinder("", TxtCustGrp.Value, isButtonClicked)
            If clsCommon.myLen(TxtCustGrp.Value) > 0 Then
                LblCustGrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Cust_Group_Desc FROM TSPL_CUSTOMER_GROUP_MASTER WHERE Cust_Group_Code='" & TxtCustGrp.Value & "'"))
            Else
                LblCustGrp.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDealer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDealer._MYValidating
        Try
            If clsCommon.myLen(TxtCustGrp.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Customer Group First..")
                TxtCustGrp.Focus()
                Exit Sub
            End If

            LblDealer.Text = ""
            TxtVehicleName.Value = ""
            LblVehicleName.Text = ""
            TxtItemPartNo.Value = ""
            LblItemPartNo.Text = ""
            gvMainItem.DataSource = Nothing
            gvChildItem.DataSource = Nothing
            TxtDealer.Value = clsCustomerMaster.getFinder(" Cust_Group_Code ='" & TxtCustGrp.Value & "' ", TxtDealer.Value, isButtonClicked)
            If clsCommon.myLen(TxtDealer.Value) > 0 Then
                LblDealer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Customer_Name FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & TxtDealer.Value & "'"))
            Else
                LblDealer.Text = ""
                TxtVehicleName.Value = ""
                LblVehicleName.Text = ""
                TxtItemPartNo.Value = ""
                LblItemPartNo.Text = ""
                Me.gvMainItem.Rows.Clear()
                Me.gvMainItem.Rows.AddNew()
                Me.gvChildItem.Rows.Clear()
                Me.gvChildItem.Rows.AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtIssueNotice__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtIssueNotice._MYValidating
        Try
            TxtIssueNotice.Value = ClsFaultMaster.GetFinder("", TxtIssueNotice.Value, isButtonClicked)
            If clsCommon.myLen(TxtIssueNotice.Value) > 0 Then
                LblIssueNotice.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Fault_Master_Name FROM TSPL_SW_FAULT_MASTER WHERE Fault_Master_Code='" & TxtIssueNotice.Value & "'"))
            Else
                LblIssueNotice.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtItemPartNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtItemPartNo._MYValidating
        Try
            If clsCommon.myLen(TxtVehicleName.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Vehicle First..")
                TxtVehicleName.Focus()
                Exit Sub
            End If

            Dim qry As String = ""
            qry = " Select CONSM_ITEM_CODE AS Code,ITEM_DESCRIPTION As [Item Description] From TSPL_MF_BOM_DETAIL LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.BOM_CODE =TSPL_MF_BOM_DETAIL.BOM_CODE "

            TxtItemPartNo.Value = clsCommon.ShowSelectForm("SWSerEnqIC", qry, "Code", "  TSPL_MF_BOM_HEAD.PROD_ITEM_CODE ='" & TxtVehicleName.Value & "' ", TxtItemPartNo.Value, "Code", isButtonClicked)

            If clsCommon.myLen(TxtItemPartNo.Value) > 0 Then
                LblItemPartNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE Item_Code='" + TxtItemPartNo.Value + "'"))
            Else
                LblItemPartNo.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVehicleName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtVehicleName._MYValidating
        Try
            If clsCommon.myLen(TxtDealer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select dealer first", Me.Text)
                TxtDealer.Focus()
                TxtDealer.Select()
                Return
            End If

            LblVehicleName.Text = ""
            TxtItemPartNo.Value = ""
            LblItemPartNo.Text = ""
            gvMainItem.DataSource = Nothing
            gvChildItem.DataSource = Nothing
            'TxtVehicleName.Value = clsItemMaster.getFinder("", TxtVehicleName.Value, isButtonClicked)
            Dim Qry As String = String.Empty
            Qry = " SELECT DISTINCT TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AS [Code],TSPL_ITEM_MASTER.Item_Desc As [Item Desp],TSPL_ITEM_MASTER.Short_Description AS [Short Description],TSPL_ITEM_MASTER.Item_Type AS [Item Type],ISNULL(TSPL_ITEM_MASTER.item_category,'') AS [Item Category],ISNULL(TSPL_MF_BOM_HEAD.REVISION_NO,'') AS [Revision No] FROM TSPL_SD_SALE_INVOICE_HEAD " & _
                  " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                  " LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "

            TxtVehicleName.Value = clsCommon.ShowSelectForm("SEVehNam", Qry, "Code", "  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='ALL' AND TSPL_MF_BOM_HEAD.POSTED=1 AND TSPL_SD_SALE_INVOICE_HEAD.Status=1 AND TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & TxtDealer.Value & " ' ", TxtVehicleName.Value, "Code", isButtonClicked)

            If clsCommon.myLen(TxtVehicleName.Value) > 0 Then
                LblVehicleName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE Item_Code='" & TxtVehicleName.Value & "'"))
                LoadFromBOM(TxtVehicleName.Value, "")
            Else
                LblVehicleName.Text = ""
                TxtItemPartNo.Value = ""
                LblItemPartNo.Text = ""
                Me.gvMainItem.Rows.Clear()
                Me.gvMainItem.Columns.Clear()
                Me.gvMainItem.Rows.AddNew()

                Me.gvChildItem.Rows.Clear()
                Me.gvChildItem.Columns.Clear()
                Me.gvChildItem.Rows.AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "SELECT count(*) FROM TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            'qry = "SELECT Service_Enquiry_Code As [Code],Service_Enquiry_Date As [Date],Cust_Group_Code AS [Cust Group Code],Dealer_Code AS [Dealer Code],Vehicle_Code As [Vehicle Code],Date_Of_Sale As [Date Of Sale],EngineNo As [Engine No],Item_Part_No As [Item Part No],Remarks,Issued_Notice As [Issued Notice],Warranty_Status As [Warranty Status],Charge_Status As [Charge Status] FROM TSPL_SW_SERVICE_ENQUIRY"
            'txtcode.Value = clsCommon.ShowSelectForm("SWSerChar", qry, "Code", "", txtcode.Value, "TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code", isButtonClicked)
            txtcode.Value = ClsServiceEnquiry.GetFinder("", txtcode.Value, isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsServiceEnquiry
                objOT = ClsServiceEnquiry.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmServiceEnquiry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            FunReset()
        End If
    End Sub

    Private Sub FrmServiceEnquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        isNewEntry = True
        FunReset()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub gvMainItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvMainItem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvMainItem.Columns(ColItemSerialNo) Then
                    OpenItemSerialNo(False)
                ElseIf e.Column Is gvMainItem.Columns(ColIssueCode) Then
                    OpenItemIssueNo(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvChildItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvChildItem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvChildItem.Columns(ColCItemSerialNo) Then
                    OpenCItemSerialNo(False)
                ElseIf e.Column Is gvChildItem.Columns(ColCIssueCode) Then
                    OpenCItemIssueNo(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtCallNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCallNo._MYValidating
        Try
            TxtCallNo.Value = ClsServiceCall.GetFinder("", TxtCallNo.Value, isButtonClicked)
            If clsCommon.myLen(TxtCallNo.Value) > 0 Then
                LblCallNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Subject FROM TSPL_SW_SERVICE_CALL WHERE Service_Call_Code='" & TxtCallNo.Value & "'"))
                LoadServiceCallData(TxtCallNo.Value)
                LoadFromBOM(TxtVehicleName.Value, "")
            Else
                LblCallNo.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadServiceCallData(ByVal CallNo As String)
        Dim DT As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM TSPL_SW_SERVICE_CALL WHERE Service_Call_Code='" & CallNo & "'")
        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            TxtCustGrp.Value = clsCommon.myCstr(DT.Rows(0)("Cust_Group_Code"))
            TxtDealer.Value = clsCommon.myCstr(DT.Rows(0)("Dealer_Code"))
            TxtVehicleName.Value = clsCommon.myCstr(DT.Rows(0)("Vehicle_Code"))
            TxtItemPartNo.Value = clsCommon.myCstr(DT.Rows(0)("Item_Part_No"))
            TxtIssueNotice.Value = clsCommon.myCstr(DT.Rows(0)("Issued_Notice"))
            TxtRemarks.Text = clsCommon.myCstr(DT.Rows(0)("Subject"))
            LblSerialNo.Text = clsCommon.myCstr(DT.Rows(0)("Vehicle_Sr_No"))
            LblAllocatedTo.Text = clsCommon.myCstr(DT.Rows(0)("Assigned_To")) + " (" + clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + clsCommon.myCstr(DT.Rows(0)("Assigned_To")) + "'")) + ") "
            If clsCommon.myLen(TxtCustGrp.Value) > 0 Then
                LblCustGrp.Text = clsDBFuncationality.getSingleValue("Select isnull(Cust_Group_Desc,'') As Cust_Group_Desc From TSPL_CUSTOMER_GROUP_MASTER Where Cust_Group_Code ='" + TxtCustGrp.Value + "'")
            Else
                LblCustGrp.Text = ""
            End If

            If clsCommon.myLen(TxtDealer.Value) > 0 Then
                LblDealer.Text = clsDBFuncationality.getSingleValue("Select isnull(Customer_Name,'') As Customer_Name From TSPL_CUSTOMER_MASTER Where Cust_Code ='" + TxtDealer.Value + "'")
            Else
                LblDealer.Text = ""
            End If

            If clsCommon.myLen(TxtVehicleName.Value) > 0 Then
                LblVehicleName.Text = clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') As Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" + TxtVehicleName.Value + "'")
            Else
                LblVehicleName.Text = ""
            End If

            If clsCommon.myLen(TxtItemPartNo.Value) > 0 Then
                LblItemPartNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE Item_Code='" + TxtItemPartNo.Value + "'"))
            Else
                LblItemPartNo.Text = ""
            End If

            If clsCommon.myLen(TxtIssueNotice.Value) > 0 Then
                LblIssueNotice.Text = clsDBFuncationality.getSingleValue("Select isnull(Fault_Master_Name,'') As Fault_Master_Name From TSPL_SW_FAULT_MASTER Where Fault_Master_Code='" + TxtIssueNotice.Value + "'")
            Else
                LblIssueNotice.Text = ""
            End If
            TxtCustGrp.Enabled = False
            TxtDealer.Enabled = False
            TxtVehicleName.Enabled = False
            TxtItemPartNo.Enabled = False
            TxtIssueNotice.Enabled = False
            TxtRemarks.ReadOnly = True
        Else
            TxtCustGrp.Enabled = True
            TxtDealer.Enabled = True
            TxtVehicleName.Enabled = True
            TxtItemPartNo.Enabled = True
            TxtIssueNotice.Enabled = True
            TxtRemarks.ReadOnly = False
        End If
    End Sub
End Class
