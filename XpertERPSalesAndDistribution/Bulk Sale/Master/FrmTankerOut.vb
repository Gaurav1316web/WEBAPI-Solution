'' Created By Richa Agarwal Against Ticket No.BM00000003852 on 10/09/2014,BM00000005110
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
Public Class FrmTankerOut
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    ' Dim userCode, companyCode As String
    Dim arrLoc As String = Nothing
    Dim Qry As String
    Public Const colSlNo As String = "SlNo"
    Public Const colSealNo As String = "SealNo"
    Dim AllowSeal As Boolean = False

    Private Sub FrmTankerOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'fndLocation.Value = obj.Default_LocCode
                'lblLocation.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FrmTankerOut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Post Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        AllowSeal = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowSealNumberForTunkerOut, clsFixedParameterCode.ShowSealNumberForTunkerOut, Nothing)) = "1", True, False)
        RadGroupBox3.Visible = AllowSeal

    End Sub
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmTankerOut)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsTankerOut = ClsTankerOut.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndGateOutNo.Value = obj.Document_No
            txtDate.Value = obj.Document_Date
            lblGateEntryNo.Text = obj.GateEntryNo
            fndTanker.Value = obj.Tanker_No
            ' lblTanker.Text = obj.Tanker_Name
            lblLocationCode.Text = obj.Location_Code
            lblLocation.Text = obj.Location_Name
            lblCustomerCode.Text = obj.Customer_Code
            LblCustomer.Text = obj.Customer_Name
            If clsCommon.CompairString(obj.IsGateOut, "1") = CompairStringResult.Equal Then
                chkGateOut.Checked = True
            Else
                chkGateOut.Checked = False
            End If
            If AllowSeal Then
                gvManualSeal.Rows(0).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No1)
                gvManualSeal.Rows(1).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No2)
                gvManualSeal.Rows(2).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No3)
                gvManualSeal.Rows(3).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No4)
                gvManualSeal.Rows(4).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No5)
            End If
           
            btnsave.Text = "Update"
            fndGateOutNo.MyReadOnly = True
        Else
            AddNew()
        End If
    End Sub
    Sub SaveData()

        Try
            If AllowToSave() Then
                Dim obj As New ClsTankerOut()
                obj.Document_No = fndGateOutNo.Value
                obj.Document_Date = txtDate.Value
                obj.GateEntryNo = clsCommon.myCstr(lblGateEntryNo.Text)
                obj.Tanker_No = clsCommon.myCstr(fndTanker.Value)
                obj.Location_Code = clsCommon.myCstr(lblLocationCode.Text)
                obj.Customer_Code = clsCommon.myCstr(lblCustomerCode.Text)
                If chkGateOut.Checked = True Then
                    obj.IsGateOut = 1
                Else
                    obj.IsGateOut = 0
                End If
                If AllowSeal Then
                    obj.Seal_No1 = clsCommon.myCstr(gvManualSeal.Rows(0).Cells(colSealNo).Value)
                    obj.Seal_No2 = clsCommon.myCstr(gvManualSeal.Rows(1).Cells(colSealNo).Value)
                    obj.Seal_No3 = clsCommon.myCstr(gvManualSeal.Rows(2).Cells(colSealNo).Value)
                    obj.Seal_No4 = clsCommon.myCstr(gvManualSeal.Rows(3).Cells(colSealNo).Value)
                    obj.Seal_No5 = clsCommon.myCstr(gvManualSeal.Rows(4).Cells(colSealNo).Value)
                End If
                

                If (ClsTankerOut.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean

        Try
            ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

        If clsCommon.myLen(clsCommon.myCstr(lblGateEntryNo.Text)) <= 0 Then
            FndCustomer.Focus()
            Throw New Exception("Please select Gate Entry No")
        End If
        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date,Document_Date,103) from TSPL_GATEENTRY_SALE where Document_No ='" + lblGateEntryNo.Text + "'")) > clsCommon.myCDate(txtDate.Value) Then
            txtDate.Focus()
            Throw New Exception("Gate Out Date cannot be less than from Gate Entry No Date")
        End If
        If chkGateOut.Checked = False Then
            chkGateOut.Focus()
            Throw New Exception("Please checked Gate Out")
        End If
        If AllowSeal Then
            If isManualSealEmpty() Then
                Throw New Exception("Please enter minimum 3 Seal No")
            End If
            For k As Integer = 0 To gvManualSeal.Rows.Count - 1
                If clsCommon.myLen(gvManualSeal.Rows(k).Cells(colSealNo).Value) > 30 Then
                    Throw New Exception("Seal No lenngth not allow greter then 32 at Row no " & (k + 1))
                End If
            Next

            For i As Integer = 0 To gvManualSeal.Rows.Count - 2
                For j As Integer = i + 1 To gvManualSeal.Rows.Count - 1
                    If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealNo).Value) > 0 AndAlso clsCommon.myLen(gvManualSeal.Rows(j).Cells(colSealNo).Value) > 0 Then
                        If clsCommon.CompairString(gvManualSeal.Rows(i).Cells(colSealNo).Value, gvManualSeal.Rows(j).Cells(colSealNo).Value) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Seal No found at Row no " & (j + 1))
                        End If
                    End If
                Next
            Next


        End If
        

        Return True
    End Function


    Sub AddNew()
        isNewEntry = True
        txtDate.Value = clsCommon.GETSERVERDATE
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
        End If
        fndGateOutNo.Value = ""
        fndLocation.Value = ""
        fndTanker.Value = ""
        lblLocation.Text = ""
        ' lblTanker.Text = ""
        FndCustomer.Value = ""
        LblCustomer.Text = ""
        fndGateEntryIn.Value = ""
        lblGateEntryNo.Text = ""
        lblLocationCode.Text = ""
        lblCustomerCode.Text = ""
        chkGateOut.Checked = False
        LOCATIONRIGTHS()
        fndGateOutNo.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        loadBlankGridManualSeal()
    End Sub
#End Region

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub fndGateEntryIn__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryIn._MYValidating
        'fndGateEntryIn.Value = clsGateEntrySale.getFinder(" Posted=1 and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") and  Document_No not in (Select TSPL_GATEOUT_SALE.GateEntryNo from TSPL_GATEOUT_SALE where Document_No<> '" + fndGateOutNo.Value + "' and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )  ", fndGateEntryIn.Value, isButtonClicked)
        'Qry = "SELECT Tanker_No AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryIn.Value + "' "
        'fndTanker.Value = clsDBFuncationality.getSingleValue(Qry)
        'lblTanker.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + fndTanker.Value + "' ")
        'Qry = "SELECT Location_Code AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryIn.Value + "' "
        'fndLocation.Value = clsDBFuncationality.getSingleValue(Qry)
        'lblLocation.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + fndLocation.Value + "' ")
        'Qry = "SELECT ISNULL(Customer_Code,'') AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryIn.Value + "' "
        'FndCustomer.Value = clsDBFuncationality.getSingleValue(Qry)
        'LblCustomer.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" + FndCustomer.Value + "' ")

    End Sub

    Private Sub fndGateOutNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateOutNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_GATEOUT_SALE where Document_No='" + fndGateOutNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If check > 0 Then
                fndGateOutNo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndGateOutNo.MyReadOnly = False
            End If
            LoadData(fndGateOutNo.Value, NavType)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndGateOutNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateOutNo._MYValidating
        ' Dim qry As String = "Select TSPL_GATEOUT_SALE.Document_No as Code,Convert(varchar,TSPL_GATEOUT_SALE.Document_Date,103) as Date,TSPL_GATEOUT_SALE.GateEntryNo as [Gate Entry No],TSPL_GATEOUT_SALE.Tanker_No as [Tanker No],TSPL_TANKER_MASTER.Tanker_Name AS [Tanker Name],TSPL_GATEOUT_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEOUT_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],case when TSPL_GATEOUT_SALE.IsGateOut=0 then CAST(0 as BIT) else CAST(1 as BIT) end as [Gate Out] from TSPL_GATEOUT_SALE Left Outer join TSPL_TANKER_MASTER on TSPL_GATEOUT_SALE.Tanker_No =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEOUT_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEOUT_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code "
        Dim qry As String = "Select TSPL_GATEOUT_SALE.Document_No as Code,Convert(varchar,TSPL_GATEOUT_SALE.Document_Date,103) as Date,TSPL_GATEOUT_SALE.GateEntryNo as [Gate Entry No],TSPL_GATEOUT_SALE.Tanker_No as [Tanker No],TSPL_GATEOUT_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEOUT_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],case when TSPL_GATEOUT_SALE.IsGateOut=0 then CAST(0 as BIT) else CAST(1 as BIT) end as [Gate Out] from TSPL_GATEOUT_SALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEOUT_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEOUT_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code "
        fndGateOutNo.Value = clsCommon.ShowSelectForm("TankerOut", qry, "Code", " TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") and TSPL_GATEOUT_SALE.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'", fndGateOutNo.Value, "", isButtonClicked, "TSPL_GATEOUT_SALE.Document_Date")
        LoadData(fndGateOutNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub fndTanker__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTanker._MYValidating
        'fndGateEntryIn.Value = clsGateEntrySale.getFinder(" Posted=1 and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") and  Document_No not in (Select TSPL_GATEOUT_SALE.GateEntryNo from TSPL_GATEOUT_SALE where Document_No<> '" + fndGateOutNo.Value + "' and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )  ", fndGateEntryIn.Value, isButtonClicked)
        'fndGateEntryIn.Value = ClsTankerOut.getTankerFinder(" Posted=1 and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") and  Document_No not in (Select TSPL_GATEOUT_SALE.GateEntryNo from TSPL_GATEOUT_SALE where Document_No<> '" + fndGateOutNo.Value + "' and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )  ", fndTanker.Value, isButtonClicked)
        lblGateEntryNo.Text = ClsTankerOut.getTankerFinder(" TSPL_Dispatch_BulkSale.Posted=1 and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") and  TSPL_Quality_Check_BulkSale.GateEntry_Document_No not in (Select TSPL_GATEOUT_SALE.GateEntryNo from TSPL_GATEOUT_SALE where TSPL_GATEOUT_SALE.Document_No<> '' and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )", fndTanker.Value, isButtonClicked)
        Qry = "SELECT Tanker_No AS Code FROM TSPL_GATEENTRY_SALE where Document_No='" + lblGateEntryNo.Text + "' "
        fndTanker.Value = clsDBFuncationality.getSingleValue(Qry)
        Qry = "SELECT Location_Code AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + lblGateEntryNo.Text + "' "
        lblLocationCode.Text = clsDBFuncationality.getSingleValue(Qry)
        lblLocation.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
        Qry = "SELECT ISNULL(Customer_Code,'') AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + lblGateEntryNo.Text + "' "
        lblCustomerCode.Text = clsDBFuncationality.getSingleValue(Qry)
        LblCustomer.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" + lblCustomerCode.Text + "' ")
        chkGateOut.Checked = True
    End Sub

    Sub loadBlankGridManualSeal()
        gvManualSeal.Rows.Clear()
        gvManualSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvManualSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        repoCode.HeaderImage = XpertERPSalesAndDistribution.My.Resources.Resources.search4
        repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = colSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = False
        gvManualSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 5
            gvManualSeal.Rows.AddNew()
            gvManualSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvManualSeal.Rows(i - 1).Cells(colSealNo).Value = ""
        Next
        gvManualSeal.AllowAddNewRow = False
        gvManualSeal.AllowDeleteRow = False
        gvManualSeal.ShowGroupPanel = False
        gvManualSeal.AllowColumnReorder = False
        gvManualSeal.AllowRowReorder = False
        gvManualSeal.EnableSorting = False
        gvManualSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvManualSeal.MasterTemplate.ShowColumnHeaders = True
        gvManualSeal.EnableAlternatingRowColor = True
        gvManualSeal.EnableFiltering = False
        gvManualSeal.ShowFilteringRow = False
        gvManualSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Function isManualSealEmpty() As Boolean
        Dim isEmpty As Boolean = True
        Dim count As Integer = 0
        If gvManualSeal IsNot Nothing AndAlso gvManualSeal.Rows.Count > 0 Then

            Try
                For i As Integer = 0 To gvManualSeal.Rows.Count - 1
                    If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealNo).Value) > 0 Then
                        count = count + 1
                    End If
                Next
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
            
            If count >= 3 Then
                isEmpty = False
            Else
                isEmpty = True
            End If

        End If
        Return isEmpty
    End Function

End Class
