'' Updation By Richa Agarwal Against Ticket No. BM00000003838 and BM00000003776 on 09/09/2014
'' updation by richa agarwal BM00000003849,BM00000003879,BM00000003948,BM00000004067
''richa agarwal against ticket no BM00000004766 25/11/2014,BM00000005060,BM00000005224,BM00000005222,BM00000005223
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

Public Class FrmGateEntrySale
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Dim Gateentrynoforsalereturn As String = Nothing
    Dim Tankerfromtankersalemasteringateentry As Boolean = False
    Public strDocCode As String = ""
    Dim ApplyTSPriceAtBulkSale As Boolean = False

    Private Sub FrmGateEntrySale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    'richa Against Ticket No.BM00000003776 on 08/09/2014
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim LocationName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER where Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and Location_Code ='" & obj.Default_LocCode & "'"))
                If clsCommon.myLen(LocationName) > 0 Then
                    fndLocation.Value = obj.Default_LocCode
                    lblLocation.Text = obj.Default_LocName

                Else
                    fndLocation.Value = ""
                    lblLocation.Text = ""
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    ''------------------------------------------------------------
    Private Sub FrmGateEntrySale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Tankerfromtankersalemasteringateentry = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Tankerfromtankersalemasteringateentry, clsFixedParameterCode.Tankerfromtankersalemasteringateentry, Nothing)) = 1, True, False))
        ApplyTSPriceAtBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        MyLabel6.Visible = False
        fndOrderNo.Visible = False
        lblItemCode.Visible = False
        lblItemName.Visible = False
        If ApplyTSPriceAtBulkSale = True Then
            MyLabel6.Visible = True
            fndOrderNo.Visible = True
            lblItemCode.Visible = True
            lblItemName.Visible = True
        End If
        If Tankerfromtankersalemasteringateentry Then
            fndtankersale.Visible = True
            TxtTankerName.Visible = False
        Else
            fndtankersale.Visible = False
            TxtTankerName.Visible = True
        End If
        AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
    End Sub
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmGateEntrySale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsGateEntrySale = clsGateEntrySale.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndGateEntryNo.Value = obj.Document_No
            txtDate.Value = obj.Document_Date
            'TxtTankerName.Text = obj.Tanker_No
            'fndTanker.Value = obj.Tanker_No
            'fndTransporter.Value = obj.Tanker_Transporter_Code
            fndLocation.Value = obj.Location_Code
            If clsCommon.CompairString(obj.IsSaleReturn, "Y") = CompairStringResult.Equal Then
                chkSaleReturn.Checked = True
                FndSaleReturnTanker.Value = obj.Tanker_No
                FndDispatchNo.Value = obj.Dispatch_No
            Else
                chkSaleReturn.Checked = False
                If Tankerfromtankersalemasteringateentry Then
                    fndtankersale.Value = obj.Tanker_No
                Else
                    TxtTankerName.Text = obj.Tanker_No
                End If
                FndDispatchNo.Value = ""
            End If
            Gateentrynoforsalereturn = obj.SaleReturnAgaintGEN
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
            ' lblTanker.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(tanker_name,'') from TSPL_TANKER_MASTER where Tanker_No='" & fndTanker.Value & "'"))
            'lblTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(vendor_name,'') from tspl_vendor_master where form_type='TTM' and Vendor_Code='" & fndTransporter.Value & "' "))
            If ApplyTSPriceAtBulkSale = True Then
                fndOrderNo.Value = obj.BulkSONo
            End If
            FndCustomer.Value = obj.Customer_Code
            LblCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Customer_Name,'') from TSPL_CUSTOMER_MASTER where Cust_Code='" & FndCustomer.Value & "'"))
            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            fndLocation.Enabled = False
            btnsave.Text = "Update"
            fndGateEntryNo.MyReadOnly = True
        Else
            AddNew()
        End If
    End Sub
    Sub SaveData()

        Try
            If AllowToSave() Then
                Dim obj As New clsGateEntrySale()
                obj.Document_No = fndGateEntryNo.Value
                obj.Document_Date = txtDate.Value
                'obj.Tanker_No = fndTanker.Value
                If chkSaleReturn.Checked Then
                    obj.Tanker_No = FndSaleReturnTanker.Value
                    obj.Dispatch_No = FndDispatchNo.Value
                    obj.IsSaleReturn = "Y"
                    obj.SaleReturnAgaintGEN = Gateentrynoforsalereturn
                Else
                    If Tankerfromtankersalemasteringateentry Then
                        obj.Tanker_No = fndtankersale.Value
                    Else
                        obj.Tanker_No = TxtTankerName.Text.Trim()
                    End If

                    obj.SaleReturnAgaintGEN = ""
                End If

                'obj.Tanker_No = TxtTankerName.Text.Trim()
                obj.Tanker_Transporter_Code = fndTransporter.Value
                obj.Location_Code = fndLocation.Value
                obj.Customer_Code = clsCommon.myCstr(FndCustomer.Value)
                ' obj.Transporter_name = clsCommon.myCstr(lblTransporter.Text)
                obj.Tanker_name = clsCommon.myCstr(lblTanker.Text)
                obj.BulkSONo = clsCommon.myCstr(fndOrderNo.Value)
                If (clsGateEntrySale.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            txtDate.Select()
            Return False
        End If

        If clsCommon.myLen(clsCommon.myCstr(FndCustomer.Value)) <= 0 Then
            FndCustomer.Focus()
            Throw New Exception("Please select Customer")
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) <= 0 Then
            fndLocation.Focus()
            Throw New Exception("Please select Location")
        End If
        If Not chkSaleReturn.Checked Then
            If Tankerfromtankersalemasteringateentry Then
                If clsCommon.myLen(clsCommon.myCstr(fndtankersale.Value)) <= 0 Then
                    fndtankersale.Focus()
                    Throw New Exception("Please select Tanker")
                End If
            Else
                If clsCommon.myLen(clsCommon.myCstr(TxtTankerName.Text)) <= 0 Then
                    TxtTankerName.Focus()
                    Throw New Exception("Please select Tanker")
                End If
            End If

        End If
        ''richa agarwal 17/12/2014
        If chkSaleReturn.Checked Then
            If clsCommon.myLen(clsCommon.myCstr(FndSaleReturnTanker.Value)) <= 0 Then
                FndSaleReturnTanker.Focus()
                Throw New Exception("Please select Tanker")
            End If
            If clsCommon.myLen(clsCommon.myCstr(FndDispatchNo.Value)) <= 0 Then
                FndDispatchNo.Focus()
                Throw New Exception("Please select Dispatch No")
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(FndSaleReturnTanker.Value)) > 0 AndAlso Not chkSaleReturn.Checked Then
            chkSaleReturn.Focus()
            Throw New Exception("Please select sale return")
        End If
        If clsCommon.myLen(clsCommon.myCstr(FndDispatchNo.Value)) > 0 AndAlso Not chkSaleReturn.Checked Then
            chkSaleReturn.Focus()
            Throw New Exception("Please select sale return")
        End If
        ''----------------------------
        'If clsCommon.myLen(clsCommon.myCstr(fndTransporter.Value)) <= 0 Then
        '    fndTransporter.Focus()
        '    Throw New Exception("Please select Transporter No")
        'End If
        'If clsCommon.myLen(clsCommon.myCstr(fndTanker.Value)) <= 0 Then
        '    fndTanker.Focus()
        '    Throw New Exception("Please select Tanker")
        'End If
        'If clsCommon.myLen(fndTanker.Value) > 0 Then
        '    Dim index As Integer = 0
        '    index = clsCommon.myCstr(fndTanker.Value).IndexOf(" ")

        '    If index > 0 AndAlso index < clsCommon.myLen(fndTanker.Value) Then
        '        fndTanker.Focus()
        '        Throw New Exception("No White Space Allowed In Tanker No.")
        '    End If
        'End If
        If clsCommon.myLen(TxtTankerName.Text) > 0 Then
            Dim index As Integer = 0
            index = clsCommon.myCstr(TxtTankerName.Text).IndexOf(" ")

            If index > 0 AndAlso index < clsCommon.myLen(TxtTankerName.Text) Then
                TxtTankerName.Focus()
                Throw New Exception("No White Space Allowed In Tanker No.")
            End If
        End If
        If Not isFlag Then
            'Dim dt As DataTable
            'dt = clsDBFuncationality.GetDataTable("Select isnull(XXX.Document_No,'') as GateinEntryNo,isnull(TSPL_GATEOUT_SALE.GateEntryNo,'') as GateoutentryNoOut from (Select Document_No, ROW_NUMBER() OVER (Order By Document_Date DESC) as RowNo from TSPL_GATEENTRY_SALE WHERE Customer_Code='" & FndCustomer.Value & "' AND Location_Code='" & fndLocation.Value & "' AND Tanker_No='" & fndTanker.Value & "') XXX LEFT OUTER JOIN TSPL_GATEOUT_SALE ON TSPL_GATEOUT_SALE.GateEntryNo=XXX.Document_No WHERE RowNo=1")
            'If dt.Rows.Count > 0 Then
            '    If clsCommon.myLen(dt.Rows(0)("GateinEntryNo")) > 0 And clsCommon.myLen(dt.Rows(0)("GateoutentryNoOut")) <= 0 Then
            '        Throw New Exception("This tanker no already in process without tanker out for " & dt.Rows(0)("GateinEntryNo") & " system should not accept it again")
            '        ' Throw New Exception("Please create Tanker Out for " & dt.Rows(0)("GateinEntryNo") & " first ")
            '    End If
            'End If
            Dim dt As DataTable
            ' dt = clsDBFuncationality.GetDataTable("Select isnull(XXX.Document_No,'') as GateinEntryNo,isnull(TSPL_GATEOUT_SALE.GateEntryNo,'') as GateoutentryNoOut from (Select Document_No, ROW_NUMBER() OVER (Order By Document_Date DESC) as RowNo from TSPL_GATEENTRY_SALE WHERE Customer_Code='" & FndCustomer.Value & "' AND Location_Code='" & fndLocation.Value & "' AND Tanker_No='" & TxtTankerName.Text & "') XXX LEFT OUTER JOIN TSPL_GATEOUT_SALE ON TSPL_GATEOUT_SALE.GateEntryNo=XXX.Document_No WHERE RowNo=1")
            If Tankerfromtankersalemasteringateentry Then
                dt = clsDBFuncationality.GetDataTable("Select isnull(XXX.Document_No,'') as GateinEntryNo,isnull(TSPL_GATEOUT_SALE.GateEntryNo,'') as GateoutentryNoOut from (Select Document_No, ROW_NUMBER() OVER (Order By Document_Date DESC) as RowNo from TSPL_GATEENTRY_SALE WHERE Customer_Code='" & FndCustomer.Value & "' AND Location_Code='" & fndLocation.Value & "' AND Tanker_No='" & fndtankersale.Value & "' and ISNULL(TSPL_GATEENTRY_SALE.Dispatch_No ,'')='' and ISNULL(TSPL_GATEENTRY_SALE.SaleReturnAgaintGEN,'')='' and (TSPL_GATEENTRY_SALE.IsSaleReturn ='Y' or TSPL_GATEENTRY_SALE.IsSaleReturn ='N')) XXX LEFT OUTER JOIN TSPL_GATEOUT_SALE ON TSPL_GATEOUT_SALE.GateEntryNo=XXX.Document_No WHERE RowNo=1")
            Else
                dt = clsDBFuncationality.GetDataTable("Select isnull(XXX.Document_No,'') as GateinEntryNo,isnull(TSPL_GATEOUT_SALE.GateEntryNo,'') as GateoutentryNoOut from (Select Document_No, ROW_NUMBER() OVER (Order By Document_Date DESC) as RowNo from TSPL_GATEENTRY_SALE WHERE Customer_Code='" & FndCustomer.Value & "' AND Location_Code='" & fndLocation.Value & "' AND Tanker_No='" & TxtTankerName.Text & "' and ISNULL(TSPL_GATEENTRY_SALE.Dispatch_No ,'')='' and ISNULL(TSPL_GATEENTRY_SALE.SaleReturnAgaintGEN,'')='' and (TSPL_GATEENTRY_SALE.IsSaleReturn ='Y' or TSPL_GATEENTRY_SALE.IsSaleReturn ='N')) XXX LEFT OUTER JOIN TSPL_GATEOUT_SALE ON TSPL_GATEOUT_SALE.GateEntryNo=XXX.Document_No WHERE RowNo=1")
            End If

            If dt.Rows.Count > 0 Then
                If clsCommon.CompairString(fndGateEntryNo.Value, clsCommon.myCstr(dt.Rows(0)("GateinEntryNo"))) <> CompairStringResult.Equal Then
                    If clsCommon.myLen(dt.Rows(0)("GateinEntryNo")) > 0 And clsCommon.myLen(dt.Rows(0)("GateoutentryNoOut")) <= 0 Then
                        Throw New Exception("This tanker no already in process without tanker out for " & dt.Rows(0)("GateinEntryNo") & " system should not accept it again")
                    End If
                End If
            End If
        End If
        Return True
    End Function
    Private Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Try

            If (myMessages.deleteConfirm()) Then
                arr.Add(fndGateEntryNo.Value)
                If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.FrmGateEntrySale, Nothing) Then
                    If (clsGateEntrySale.DeleteData(fndGateEntryNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                        AddNew()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub AddNew()
        isNewEntry = True
        txtDate.Value = clsCommon.GETSERVERDATE
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
        End If
        fndGateEntryNo.Value = ""
        fndLocation.Value = ""
        fndLocation.Enabled = True
        ' fndTanker.Value = ""
        'fndTransporter.Value = ""
        TxtTankerName.Text = ""
        fndtankersale.Value = ""
        lblLocation.Text = ""
        'lblTransporter.Text = ""
        'lblTanker.Text = ""
        FndCustomer.Value = ""
        LblCustomer.Text = ""
        ''richa agarwal 17/12/2014
        FndSaleReturnTanker.Value = ""
        chkSaleReturn.Checked = False
        If Tankerfromtankersalemasteringateentry Then
            fndtankersale.Visible = True
        Else
            TxtTankerName.Visible = True
        End If
        FndSaleReturnTanker.Visible = False
        FndDispatchNo.Enabled = False
        FndCustomer.Enabled = True
        fndLocation.Enabled = True
        Gateentrynoforsalereturn = ""
        ''-------------------------
        UsLock1.Status = ERPTransactionStatus.Pending
        fndOrderNo.Value = ""
        lblItemCode.Text = ""
        lblItemName.Text = ""
        LOCATIONRIGTHS()
        fndGateEntryNo.MyReadOnly = False
        btnsave.Text = "Save"
        btnPost.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub
    Sub LoadPODetail(ByVal PONO As String)
        Dim Qry = "SELECT Document_Date,Customer_Code,Location_Code,D.Item_Code,D.Item_Name FROM TSPL_SALES_ORDER_MASTER_BULKSALE M JOIN TSPL_SALES_ORDER_DETAIL_BULKSALE D ON M.Document_No=D.Document_No WHERE M.Document_No='" + PONO + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            FndCustomer.Value = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            fndLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            lblItemCode.Text = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            lblItemName.Text = clsCommon.myCstr(dt.Rows(0)("Item_Name"))
        Else
            FndCustomer.Value = ""
            fndLocation.Value = ""
            lblItemCode.Text = ""
            lblItemName.Text = ""
        End If

    End Sub
#End Region

    Private Sub fndTanker__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTanker._MYValidating
        Try

            If clsCommon.myLen(fndTransporter.Value) > 0 Then
                If clsCommon.myLen(fndTanker.Value) > 0 Then
                    ''richa agarwal against ticket no BM00000004766 25/11/2014
                    If fndTanker.Value.Contains("'").ToString() Then
                        fndTanker.Value = fndTanker.Value.Replace("'", "").ToString()
                    End If

                    Dim tanker As String = clsDBFuncationality.getSingleValue(" select TSPL_TANKER_MASTER.Tanker_No as [TankerNo],TSPL_TANKER_MASTER.tanker_name as [Tanker Description],TSPL_TANKER_MASTER.tanker_transporter_code as [Transporter No],TSPL_VENDOR_MASTER.Vendor_Name as [Tanker Transporter Name],(TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2+' '+TSPL_VENDOR_MASTER.Add3) as Address,TSPL_VENDOR_MASTER.City_Code_Desc as City,TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Country,(TSPL_VENDOR_MASTER.Phone1+' '+TSPL_VENDOR_MASTER.Phone2) as [Phone No],TSPL_VENDOR_MASTER.Email,TSPL_TANKER_MASTER.Storage_Capacity as [Storage Cap.(mt)],TSPL_TANKER_MASTER.StorageCapacityDesc as [Storage Capacity Description],TSPL_TANKER_MASTER.Year as [Year of Manufacturing],TSPL_TANKER_MASTER.Inner_SS as [Inner SS],TSPL_TANKER_MASTER.Outer_SS as [Outer SS],TSPL_TANKER_MASTER.Shift_Charges as [Charges per Shift],TSPL_TANKER_MASTER.Avg_KM_Ltr as [Avg. KM per Ltr.],TSPL_TANKER_MASTER.Diesel_Rate as [Rate of Diesel],TSPL_TANKER_MASTER.Price_KM as [Rate per KM],TSPL_TANKER_MASTER.Price_Ltr as [Rate per Ltr/KG],TSPL_TANKER_MASTER.Ltr_KG as [Ltr/KG],TSPL_TANKER_MASTER.Rental_Day as [Rental per Day],TSPL_TANKER_MASTER.Rental_Week as [Rental per Week],TSPL_TANKER_MASTER.Rental_Month as [Rental per Month] from TSPL_VENDOR_MASTER right outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code where  Tanker_Transporter_Code='" + fndTransporter.Value + "' and Tanker_No='" + fndTanker.Value + "'")
                    'Dim qry As String = "select TSPL_TANKER_MASTER.Tanker_No as [TankerNo],TSPL_TANKER_MASTER.tanker_name as [Tanker Description],TSPL_TANKER_MASTER.tanker_transporter_code as [Transporter No],TSPL_VENDOR_MASTER.Vendor_Name as [Tanker Transporter Name],(TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2+' '+TSPL_VENDOR_MASTER.Add3) as Address,TSPL_VENDOR_MASTER.City_Code_Desc as City,TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Country,(TSPL_VENDOR_MASTER.Phone1+' '+TSPL_VENDOR_MASTER.Phone2) as [Phone No],TSPL_VENDOR_MASTER.Email,TSPL_TANKER_MASTER.Storage_Capacity as [Storage Cap.(mt)],TSPL_TANKER_MASTER.StorageCapacityDesc as [Storage Capacity Description],TSPL_TANKER_MASTER.Year as [Year of Manufacturing],TSPL_TANKER_MASTER.Inner_SS as [Inner SS],TSPL_TANKER_MASTER.Outer_SS as [Outer SS],TSPL_TANKER_MASTER.Shift_Charges as [Charges per Shift],TSPL_TANKER_MASTER.Avg_KM_Ltr as [Avg. KM per Ltr.],TSPL_TANKER_MASTER.Diesel_Rate as [Rate of Diesel],TSPL_TANKER_MASTER.Price_KM as [Rate per KM],TSPL_TANKER_MASTER.Price_Ltr as [Rate per Ltr/KG],TSPL_TANKER_MASTER.Ltr_KG as [Ltr/KG],TSPL_TANKER_MASTER.Rental_Day as [Rental per Day],TSPL_TANKER_MASTER.Rental_Week as [Rental per Week],TSPL_TANKER_MASTER.Rental_Month as [Rental per Month] from TSPL_VENDOR_MASTER right outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code"

                    If clsCommon.myLen(tanker) > 0 Then
                        fndTanker.Value = tanker
                        If clsCommon.myLen(fndTanker.Value) > 0 Then
                            lblTanker.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tanker_name from TSPL_TANKER_MASTER where Tanker_No='" & fndTanker.Value & "'"))
                        End If
                    Else

                        lblTanker.Text = ""
                    End If
                Else
                    fndTanker.Value = clsfrmTankerMaster.GetFinder(" Tanker_Transporter_Code='" + fndTransporter.Value + "'", fndTanker.Value, isButtonClicked)
                    If clsCommon.myLen(fndTanker.Value) > 0 Then
                        lblTanker.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tanker_name from TSPL_TANKER_MASTER where Tanker_No='" & fndTanker.Value & "'"))
                    End If
                End If

            Else
                If clsCommon.myLen(fndTanker.Value) > 0 Then
                    If clsCommon.myLen(fndTransporter.Value) > 0 Then


                        Dim tanker As String = clsDBFuncationality.getSingleValue(" select TSPL_TANKER_MASTER.Tanker_No as [TankerNo],TSPL_TANKER_MASTER.tanker_name as [Tanker Description],TSPL_TANKER_MASTER.tanker_transporter_code as [Transporter No],TSPL_VENDOR_MASTER.Vendor_Name as [Tanker Transporter Name],(TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2+' '+TSPL_VENDOR_MASTER.Add3) as Address,TSPL_VENDOR_MASTER.City_Code_Desc as City,TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Country,(TSPL_VENDOR_MASTER.Phone1+' '+TSPL_VENDOR_MASTER.Phone2) as [Phone No],TSPL_VENDOR_MASTER.Email,TSPL_TANKER_MASTER.Storage_Capacity as [Storage Cap.(mt)],TSPL_TANKER_MASTER.StorageCapacityDesc as [Storage Capacity Description],TSPL_TANKER_MASTER.Year as [Year of Manufacturing],TSPL_TANKER_MASTER.Inner_SS as [Inner SS],TSPL_TANKER_MASTER.Outer_SS as [Outer SS],TSPL_TANKER_MASTER.Shift_Charges as [Charges per Shift],TSPL_TANKER_MASTER.Avg_KM_Ltr as [Avg. KM per Ltr.],TSPL_TANKER_MASTER.Diesel_Rate as [Rate of Diesel],TSPL_TANKER_MASTER.Price_KM as [Rate per KM],TSPL_TANKER_MASTER.Price_Ltr as [Rate per Ltr/KG],TSPL_TANKER_MASTER.Ltr_KG as [Ltr/KG],TSPL_TANKER_MASTER.Rental_Day as [Rental per Day],TSPL_TANKER_MASTER.Rental_Week as [Rental per Week],TSPL_TANKER_MASTER.Rental_Month as [Rental per Month] from TSPL_VENDOR_MASTER right outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code where  Tanker_Transporter_Code='" + fndTransporter.Value + "' and Tanker_No='" + fndTanker.Value + "'")
                        If clsCommon.myLen(tanker) > 0 Then
                            fndTanker.Value = tanker
                            If clsCommon.myLen(fndTanker.Value) > 0 Then
                                lblTanker.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tanker_name from TSPL_TANKER_MASTER where Tanker_No='" & fndTanker.Value & "'"))
                            End If
                        Else
                            lblTanker.Text = ""
                        End If
                    Else
                        Throw New Exception("Please Select Transporter No first")
                    End If
                Else
                    fndTanker.Value = clsfrmTankerMaster.GetFinder("", fndTanker.Value, isButtonClicked)
                    If clsCommon.myLen(fndTanker.Value) > 0 Then
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Tanker_Name,Tanker_Transporter_Code,Description as Transporter_name from TSPL_TANKER_MASTER where Tanker_No ='" + fndTanker.Value + "'")
                        If dt.Rows.Count > 0 Then
                            lblTanker.Text = clsCommon.myCstr(dt.Rows(0)("Tanker_Name"))
                            fndTransporter.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_Transporter_Code"))
                            'lblTransporter.Text = clsCommon.myCstr(dt.Rows(0)("Transporter_name"))
                            lblTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where form_type='TTM' and Vendor_Code='" & fndTransporter.Value & "'"))
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
            'fndTransporter.Select()
        End Try

        'fndTanker.Value = clsfrmTankerMaster.GetFinder(" Tanker_Transporter_Code='" + fndTransporter.Value + "'", fndTanker.Value, isButtonClicked)

        'If clsCommon.myLen(fndTanker.Value) > 0 Then
        '    lblTanker.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tanker_name from TSPL_TANKER_MASTER where Tanker_No='" & fndTanker.Value & "'"))
        'Else
        '    lblTanker.Text = ""
        'End If
    End Sub

    Private Sub fndTransporter__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTransporter._MYValidating
        fndTransporter.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Form_Type='TTM' ", fndTransporter.Value, isButtonClicked)

        If clsCommon.myLen(fndTransporter.Value) > 0 Then
            lblTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where form_type='TTM' and Vendor_Code='" & fndTransporter.Value & "'"))
            ''richa 23/09/2014
            If clsCommon.myLen(fndTanker.Value) > 0 Then
                Dim tanker As String = clsDBFuncationality.getSingleValue(" select TSPL_TANKER_MASTER.Tanker_No as [TankerNo],TSPL_TANKER_MASTER.tanker_name as [Tanker Description],TSPL_TANKER_MASTER.tanker_transporter_code as [Transporter No],TSPL_VENDOR_MASTER.Vendor_Name as [Tanker Transporter Name],(TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2+' '+TSPL_VENDOR_MASTER.Add3) as Address,TSPL_VENDOR_MASTER.City_Code_Desc as City,TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Country,(TSPL_VENDOR_MASTER.Phone1+' '+TSPL_VENDOR_MASTER.Phone2) as [Phone No],TSPL_VENDOR_MASTER.Email,TSPL_TANKER_MASTER.Storage_Capacity as [Storage Cap.(mt)],TSPL_TANKER_MASTER.StorageCapacityDesc as [Storage Capacity Description],TSPL_TANKER_MASTER.Year as [Year of Manufacturing],TSPL_TANKER_MASTER.Inner_SS as [Inner SS],TSPL_TANKER_MASTER.Outer_SS as [Outer SS],TSPL_TANKER_MASTER.Shift_Charges as [Charges per Shift],TSPL_TANKER_MASTER.Avg_KM_Ltr as [Avg. KM per Ltr.],TSPL_TANKER_MASTER.Diesel_Rate as [Rate of Diesel],TSPL_TANKER_MASTER.Price_KM as [Rate per KM],TSPL_TANKER_MASTER.Price_Ltr as [Rate per Ltr/KG],TSPL_TANKER_MASTER.Ltr_KG as [Ltr/KG],TSPL_TANKER_MASTER.Rental_Day as [Rental per Day],TSPL_TANKER_MASTER.Rental_Week as [Rental per Week],TSPL_TANKER_MASTER.Rental_Month as [Rental per Month] from TSPL_VENDOR_MASTER right outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code where  Tanker_Transporter_Code='" + fndTransporter.Value + "' and Tanker_No='" + fndTanker.Value + "'")
                If clsCommon.myLen(tanker) > 0 Then
                    fndTanker.Value = tanker
                    If clsCommon.myLen(fndTanker.Value) > 0 Then
                        lblTanker.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tanker_name from TSPL_TANKER_MASTER where Tanker_No='" & fndTanker.Value & "'"))
                    End If
                Else

                    fndTanker.Value = ""
                    lblTanker.Text = ""
                End If
            End If
            '=======================================
        Else
            lblTransporter.Text = ""
        End If
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and tspl_location_master.location_code in (" + arrLoc + ")", fndLocation.Value, isButtonClicked)

        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
        Else
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub fndGateEntryNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateEntryNo._MYNavigator
        Dim qry As String = Nothing
        Try
            qry = "select count(*) from TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndGateEntryNo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndGateEntryNo.MyReadOnly = False
            End If

            LoadData(fndGateEntryNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        ' Dim qry As String = "select distinct TSPL_GATEENTRY_SALE.Document_No as DocumentNo,Convert(varchar,TSPL_GATEENTRY_SALE.Document_date,103) as [Doc Date], Location_Code as Location,Tanker_No as Tanker,Tanker_Transporter_Code as Transporter from TSPL_GATEENTRY_SALE"
        ' Dim qry As String = "Select TSPL_GATEENTRY_SALE.Document_No as DocumentNo,Convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as Date,TSPL_GATEENTRY_SALE.Tanker_No as [Tanker No],TSPL_TANKER_MASTER.Tanker_Name AS [Tanker Name],TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_GATEENTRY_SALE.Tanker_Transporter_Code as [Transporter No],TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Description] from TSPL_GATEENTRY_SALE Left Outer join TSPL_TANKER_MASTER on TSPL_GATEENTRY_SALE.Tanker_No =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_VENDOR_MASTER on TSPL_GATEENTRY_SALE.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code and TSPL_VENDOR_MASTER.Form_Type='TTM'"
        ''richa ERO/08/05/19-000595 add Sale order no
        Dim qry As String = "Select TSPL_GATEENTRY_SALE.Document_No as DocumentNo,Convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as Date,TSPL_GATEENTRY_SALE.Tanker_No as [Tanker No],TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_GATEENTRY_SALE.IsSaleReturn as [Sale Return],ISNULL(SaleReturnAgaintGEN,'') as [SR Against Gate Entry No],isnull(TSPL_GATEENTRY_SALE.Dispatch_No,'') as [Dispatch No],isnull(TSPL_GATEENTRY_SALE.Bulk_SO_No,'') as [Sale Order No] from TSPL_GATEENTRY_SALE Left Outer join TSPL_TANKER_MASTER on TSPL_GATEENTRY_SALE.Tanker_No =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
        Dim whrClas As String = "TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") and TSPL_GATEENTRY_SALE.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Reset()
        LoadData(clsCommon.ShowSelectForm("CRTMNTID", qry, "DocumentNo", whrClas, fndGateEntryNo.Value, "DocumentNo", isButtonClicked), NavigatorType.Current)
        qry = Nothing
        whrClas = Nothing
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Try

            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (clsGateEntrySale.PostData(MyBase.Form_ID, arrLoc, fndGateEntryNo.Value)) Then
                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(fndGateEntryNo.Value, NavigatorType.Current)
                End If
            End If
            isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Sub FndCustomer__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndCustomer._MYValidating
        FndCustomer.Value = clsCustomerMaster.getFinder("isnull(Status,'N')='N'", FndCustomer.Value, isButtonClicked)
        If clsCommon.myLen(FndCustomer.Value) > 0 Then
            LblCustomer.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + FndCustomer.Value + "' ")
        Else
            LblCustomer.Text = ""
        End If
    End Sub


    Private Sub TxtTankerName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTankerName.Leave
        If clsCommon.myLen(TxtTankerName.Text) > 0 Then
            Dim index As Integer = 0
            index = clsCommon.myCstr(TxtTankerName.Text).IndexOf(" ")

            If index > 0 AndAlso index < clsCommon.myLen(TxtTankerName.Text) Then
                TxtTankerName.Focus()
                clsCommon.MyMessageBoxShow("No White Space Allowed In Tanker No.")
            End If
        End If
        If TxtTankerName.Text.Contains("'").ToString() Then
            TxtTankerName.Text = TxtTankerName.Text.Replace("'", "").ToString()
        End If
        Dim dt As DataTable = Nothing
        dt = clsDBFuncationality.GetDataTable("Select isnull(XXX.Document_No,'') as GateinEntryNo,isnull(TSPL_GATEOUT_SALE.GateEntryNo,'') as GateoutentryNoOut from (Select Document_No, ROW_NUMBER() OVER (Order By Document_Date DESC) as RowNo from TSPL_GATEENTRY_SALE WHERE Customer_Code='" & FndCustomer.Value & "' AND Location_Code='" & fndLocation.Value & "' AND Tanker_No='" & TxtTankerName.Text.Trim() & "') XXX LEFT OUTER JOIN TSPL_GATEOUT_SALE ON TSPL_GATEOUT_SALE.GateEntryNo=XXX.Document_No WHERE RowNo=1")
        If dt.Rows.Count > 0 Then
            If clsCommon.CompairString(fndGateEntryNo.Value, clsCommon.myCstr(dt.Rows(0)("GateinEntryNo"))) <> CompairStringResult.Equal Then
                If clsCommon.myLen(dt.Rows(0)("GateinEntryNo")) > 0 And clsCommon.myLen(dt.Rows(0)("GateoutentryNoOut")) <= 0 Then
                    'TxtTankerName.Focus()
                    clsCommon.MyMessageBoxShow("This tanker no already in process without tanker out for " & dt.Rows(0)("GateinEntryNo") & " system should not accept it again")
                End If
            End If
        End If
        dt = Nothing
    End Sub
    ''richa agarwal 17/12/2014
    Private Sub chkSaleReturn_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSaleReturn.ToggleStateChanged
        If chkSaleReturn.Checked Then
            FndSaleReturnTanker.Visible = True
            If Tankerfromtankersalemasteringateentry Then
                fndtankersale.Visible = False
            Else
                TxtTankerName.Visible = False
            End If

            FndDispatchNo.Enabled = False
            FndCustomer.Enabled = False
            fndLocation.Enabled = False
        Else
            FndSaleReturnTanker.Visible = False
            If Tankerfromtankersalemasteringateentry Then
                fndtankersale.Visible = True
            Else
                TxtTankerName.Visible = True
            End If
            FndDispatchNo.Enabled = False
            FndCustomer.Enabled = True
            fndLocation.Enabled = True
            fndLocation.Value = ""
            TxtTankerName.Text = ""
            lblLocation.Text = ""
            FndCustomer.Value = ""
            LblCustomer.Text = ""
            fndtankersale.Value = ""
            FndSaleReturnTanker.Value = ""
            FndDispatchNo.Value = ""
            Gateentrynoforsalereturn = ""
        End If
    End Sub

    Private Sub FndSaleReturnTanker__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndSaleReturnTanker._MYValidating
        Dim qry As String = String.Empty
        'qry = " select final.code as [TankerNo],final.Document_No as [DispatchNo],final.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],final.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name]  from ("
        'qry += " Select TSPL_Dispatch_BulkSale.Tanker_Code as Code,TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_BulkSale.Customer_Code ,TSPL_Dispatch_BulkSale.Location_Code from TSPL_INVOICE_MASTER_BULKSALE "
        'qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No inner join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No inner join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code inner join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code inner join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo=TSPL_Quality_Check_BulkSale.GateEntry_Document_No where   TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='Against Dispatch' and TSPL_INVOICE_MASTER_BULKSALE.Posted=1  and not exists (Select 1 from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Against_Sale_No )"
        'qry += " union all"
        'qry += " select TSPL_Dispatch_BulkSale.Tanker_Code as Code,TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_BulkSale.Customer_Code ,TSPL_Dispatch_BulkSale.Location_Code from TSPL_Dispatch_BulkSale left outer join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code where TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code is null ) final "
        'qry += " Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Customer_Code"
        'qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=final.Location_Code"
        qry += "select distinct final.code as [TankerNo],final.Document_No as [DispatchNo],final.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],final.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],final.GateEntry_Document_No   from (" & _
        " Select TSPL_Dispatch_BulkSale.Tanker_Code as Code,TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_BulkSale.Customer_Code ," & _
         " TSPL_Dispatch_BulkSale.Location_Code,TSPL_Quality_Check_BulkSale.GateEntry_Document_No from TSPL_INVOICE_MASTER_BULKSALE " & _
        " left outer join TSPL_Customer_Invoice_Head on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No " & _
         " inner join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " & _
        " inner join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code " & _
         " inner join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code " & _
         " inner join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo=TSPL_Quality_Check_BulkSale.GateEntry_Document_No " & _
        " where TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='Against Dispatch' and TSPL_INVOICE_MASTER_BULKSALE.Posted=1  and not exists (Select 1 from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Against_Sale_No )" & _
         " union all" & _
         " select TSPL_Dispatch_BulkSale.Tanker_Code as Code,TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_BulkSale.Customer_Code ," & _
         " TSPL_Dispatch_BulkSale.Location_Code,'' AS GateEntry_Document_No from TSPL_Dispatch_BulkSale INNER join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code" & _
         " inner join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code" & _
         " inner join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo=TSPL_Quality_Check_BulkSale.GateEntry_Document_No" & _
         " where TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code is null ) final " & _
       " left outer  join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Customer_Code " & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=final.Location_Code " & _
        " where final.GateEntry_Document_No not in  (Select TSPL_GATEENTRY_SALE.Document_No  from TSPL_GATEENTRY_SALE left outer join TSPL_GATEENTRY_SALE salereturnGEN on TSPL_GATEENTRY_SALE.Document_No=salereturnGEN.SaleReturnAgaintGEN where salereturnGEN.Document_No <> '" & fndGateEntryNo.Value & "' and TSPL_GATEENTRY_SALE.IsSaleReturn='Y' )"
        '" where 1=1 and Not exists (select 1 from TSPL_GATEENTRY_SALE WHERE TSPL_GATEENTRY_SALE.Document_No=final.GateEntry_Document_No AND TSPL_GATEENTRY_SALE.Customer_Code=final.Customer_Code AND TSPL_GATEENTRY_SALE.Location_Code=final.Location_Code and IsSaleReturn='Y' and TSPL_GATEENTRY_SALE.Document_No <> '" & fndGateEntryNo.Value & "') "
        '" where final.GateEntry_Document_No not in (Select Document_No from TSPL_GATEENTRY_SALE where TSPL_GATEENTRY_SALE.Document_No <> '" & fndGateEntryNo.Value & "' )"
        Dim dr As DataRow = Nothing
        dr = clsCommon.ShowSelectFormForRow("TnkFndSR", qry)
        If Not dr Is Nothing Then
            FndSaleReturnTanker.Value = clsCommon.myCstr(dr("TankerNo"))
            FndCustomer.Value = clsCommon.myCstr(dr("Customer Code"))
            LblCustomer.Text = clsCommon.myCstr(dr("Customer Name"))
            fndLocation.Value = clsCommon.myCstr(dr("Location Code"))
            lblLocation.Text = clsCommon.myCstr(dr("Location Name"))
            FndDispatchNo.Value = clsCommon.myCstr(dr("DispatchNo"))
            Gateentrynoforsalereturn = clsCommon.myCstr(dr("GateEntry_Document_No"))
        Else
            FndSaleReturnTanker.Value = ""
            FndCustomer.Value = ""
            LblCustomer.Text = ""
            fndLocation.Value = ""
            lblLocation.Text = ""
            FndDispatchNo.Value = ""
            Gateentrynoforsalereturn = ""
        End If
        qry = Nothing
        dr = Nothing
    End Sub


    Private Sub fndtankersale__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndtankersale._MYValidating
        fndtankersale.Value = clsTankerMasterSaleHead.getFinder("", fndtankersale.Value, isButtonClicked)
        If clsCommon.myLen(fndtankersale.Value) > 0 Then
            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable("Select isnull(XXX.Document_No,'') as GateinEntryNo,isnull(TSPL_GATEOUT_SALE.GateEntryNo,'') as GateoutentryNoOut from (Select Document_No, ROW_NUMBER() OVER (Order By Document_Date DESC) as RowNo from TSPL_GATEENTRY_SALE WHERE Customer_Code='" & FndCustomer.Value & "' AND Location_Code='" & fndLocation.Value & "' AND Tanker_No='" & fndtankersale.Value & "') XXX LEFT OUTER JOIN TSPL_GATEOUT_SALE ON TSPL_GATEOUT_SALE.GateEntryNo=XXX.Document_No WHERE RowNo=1")
            If dt.Rows.Count > 0 Then
                If clsCommon.CompairString(fndGateEntryNo.Value, clsCommon.myCstr(dt.Rows(0)("GateinEntryNo"))) <> CompairStringResult.Equal Then
                    If clsCommon.myLen(dt.Rows(0)("GateinEntryNo")) > 0 And clsCommon.myLen(dt.Rows(0)("GateoutentryNoOut")) <= 0 Then
                        clsCommon.MyMessageBoxShow("This tanker no already in process without tanker out for " & dt.Rows(0)("GateinEntryNo") & " system should not accept it again")
                    End If
                End If
            End If
            dt = Nothing
        End If
    End Sub
    ' Ticket No : ERO/06/03/19-000504 by prabhakar 
    Private Sub fndOrderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndOrderNo._MYValidating
        Dim qry As String = "Select TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_SALES_ORDER_MASTER_BULKSALE.Document_Date,103) as [Dispatch Date],TSPL_SALES_ORDER_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_SALES_ORDER_MASTER_BULKSALE.Customer_Name as [Customer Name],TSPL_SALES_ORDER_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_SALES_ORDER_MASTER_BULKSALE.Location_Name [Location Name],TSPL_SALES_ORDER_MASTER_BULKSALE.PO_NO as [PO NO],Convert(varchar,TSPL_SALES_ORDER_MASTER_BULKSALE.PO_Date,103) as [PO Date],TSPL_SALES_ORDER_MASTER_BULKSALE.Price_Code as [Price Code],TSPL_SALES_ORDER_MASTER_BULKSALE.TERMS_Code as [Payment Terms],case when TSPL_SALES_ORDER_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_SALES_ORDER_MASTER_BULKSALE "
        'fndOrderNo.Value = clsCommon.ShowSelectForm("SalesOrderBulkSale", qry, "Code", "Posted=1", fndOrderNo.Value, "", isButtonClicked)
        fndOrderNo.Value = clsCommon.ShowSelectForm("SalesOrderBulkSale", qry, "Code", "TSPL_SALES_ORDER_MASTER_BULKSALE.Posted=1 and TSPL_SALES_ORDER_MASTER_BULKSALE.close_yn='N' and TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No not in (select TSPL_GATEENTRY_SALE.Bulk_SO_NO from TSPL_GATEENTRY_SALE where TSPL_GATEENTRY_SALE.Bulk_SO_NO is not null and TSPL_GATEENTRY_SALE.Bulk_SO_NO <> '' )", fndOrderNo.Value, "", isButtonClicked)
        LoadPODetail(fndOrderNo.Value)
    End Sub

End Class
