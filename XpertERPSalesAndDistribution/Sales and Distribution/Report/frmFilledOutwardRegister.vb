Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Public Class FrmFilledOutwardRegister
    Inherits FrmMainTranScreen
    Dim userCode, companyCode, strSql As String
    Dim intFromLoadNo, intToLoadNo As Integer
    Dim CustCatgDesc As String

   
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FilloutwardRegisterReport1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub



    Private Sub FrmFilledOutwardRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        FilledLoadOut()




    End Sub

    Private Sub fndCustCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        Try
            If TxtfndFromLoadOut.Value <> "" Then
                If TxtfndToLoadOut.Value = "" Then
                    TxtfndToLoadOut.Value = TxtfndFromLoadOut.Value
                    intToLoadNo = intFromLoadNo

                End If
             
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Guntur") = CompairStringResult.Equal Then


                strSql = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS shipmennt_no, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as  Shipment_Date, " & _
                "TSPL_SALE_INVOICE_HEAD.Route_No, TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_HEAD.Route_Desc," & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Code,(TSPL_SALE_INVOICE_DETAIL.Shipped_Qty/Conversion_Factor) as Shipped_Qty, " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, '" & fromDate.Value & "' AS fdate, '" & ToDate.Value & "' AS Tdate,'" & TxtfndCustCategory.Value & "' as Cust_Category_Code, " & _
                "'" & CustCatgDesc & "' as CUST_CATEGORY_DESC,'" & TxtfndFromLoadOut.Value & "' as FromLoadOut,'" & TxtfndToLoadOut.Value & "' as ToLoadOut FROM " & _
                "TSPL_SALE_INVOICE_HEAD INNER JOIN TSPL_SALE_INVOICE_DETAIL ON " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                "INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                "TSPL_CUSTOMER_TYPE_MASTER ON " & _
                "TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code inner join " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >= convert(date,'" & fromDate.Value & "',103) and " & _
                "Sale_Invoice_Date <= convert(date,'" & ToDate.Value & "',103) and Is_Post='Y' "
                If chkLocSelect.IsChecked = True AndAlso chkgv1.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                    Return
                ElseIf chkLocSelect.IsChecked AndAlso chkgv1.CheckedValue.Count > 0 Then
                    strSql += " and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  in (" + clsCommon.GetMulcallString(chkgv1.CheckedValue) + ")"


                    'If TxtfndFromLoadOut.Value <> "" Then
                    '    strSql += " and TSPL_SHIPMENT_MASTER.Shipment_Id >= '" & intFromLoadNo & "' and TSPL_SHIPMENT_MASTER.Shipment_ID <= '" & intToLoadNo & "' "
                End If

                'If TxtfndCustCategory.Value <> "" Then
                '    strSql += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code='" & TxtfndCustCategory.Value & "'"
                'End If


            Else

                strSql = " select * from (select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS shipmennt_no, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Shipment_Date , TSPL_SALE_INVOICE_HEAD.Route_No, TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_SALE_INVOICE_HEAD.Route_Desc,TSPL_SALE_INVOICE_DETAIL.Item_Code,(TSPL_SALE_INVOICE_DETAIL.Shipped_Qty/Conversion_Factor) as Shipped_Qty, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, '" & fromDate.Value & "' AS fdate, '" & ToDate.Value & "' AS Tdate,'" & TxtfndCustCategory.Value & "' as Cust_Category_Code,'" & CustCatgDesc & "' as CUST_CATEGORY_DESC,'" & TxtfndFromLoadOut.Value & "' as FromLoadOut,'" & TxtfndToLoadOut.Value & "' as ToLoadOut  ,TSPL_SALE_INVOICE_HEAD.Is_Post "

                strSql += "   FROM TSPL_SALE_INVOICE_HEAD "
                strSql += "   INNER JOIN TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No "
                strSql += "   INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code "
                strSql += "   INNER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Type_Code = TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code "
                strSql += "   inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code "
                'If TxtfndCustCategory.Value <> "" Then
                '    strSql += " where TSPL_CUSTOMER_MASTER.Cust_Type_Code='" & TxtfndCustCategory.Value & "'"
                'End If

                strSql += "       union all "

                strSql += "   select TSPL_TRANSFER_HEAD.Transfer_No  AS shipmennt_no, TSPL_TRANSFER_HEAD.Transfer_Date , TSPL_TRANSFER_HEAD.Route_No , TSPL_TRANSFER_HEAD.Vehicle_No ,TSPL_TRANSFER_HEAD.Route_Desc ,TSPL_TRANSFER_DETAIL .Item_Code ,(TSPL_TRANSFER_DETAIL .Item_Qty /Conversion_Factor) as Shipped_Qty, TSPL_TRANSFER_HEAD.Transfer_No,  '" & fromDate.Value & "' AS fdate, '" & ToDate.Value & "' AS Tdate,'" & TxtfndCustCategory.Value & "' as Cust_Category_Code,'" & CustCatgDesc & "' as CUST_CATEGORY_DESC,'" & TxtfndFromLoadOut.Value & "' as FromLoadOut,'" & TxtfndToLoadOut.Value & "' as ToLoadOut,TSPL_TRANSFER_HEAD.Post  FROM TSPL_TRANSFER_HEAD "
                strSql += "   INNER JOIN TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No  = TSPL_TRANSFER_DETAIL .Transfer_No"
                strSql += "   inner join TSPL_ITEM_UOM_DETAIL on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom =TSPL_ITEM_UOM_DETAIL.UOM_Code   where Tax_Group='BEDT'"
                'If TxtfndCustCategory.Value <> "" Then
                '    strSql += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code='" & TxtfndCustCategory.Value & "'"
                'End If
                strSql += "   ) xxx  "



                strSql += "   where xxx.Shipment_Date >= convert(date,'" & fromDate.Value & "',103) and Shipment_Date <= convert(date,'" & ToDate.Value & "',103) and Is_Post='Y'"
                If chkLocSelect.IsChecked = True AndAlso chkgv1.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                    Return
                ElseIf chkLocSelect.IsChecked AndAlso chkgv1.CheckedValue.Count > 0 Then
                    strSql += " and xxx.shipmennt_no in (" + clsCommon.GetMulcallString(chkgv1.CheckedValue) + ")"


                    'If TxtfndFromLoadOut.Value <> "" Then
                    '    strSql += " and TSPL_SHIPMENT_MASTER.Shipment_Id >= '" & intFromLoadNo & "' and TSPL_SHIPMENT_MASTER.Shipment_ID <= '" & intToLoadNo & "' "
                End If

            End If



            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptFilledoutwardRegister", "Filled Outward Register")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        RESET()
    End Sub
    Sub RESET()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        TxtfndCustCategory.Value = ""
        TxtfndFromLoadOut.Value = ""
        TxtfndToLoadOut.Value = ""
        lblCustDesc.Text = ""
        FilledLoadOut()
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmFilledOutwardRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            RESET()
        End If
    End Sub

    '' Added By abhishek as on 28/12/2012 For New Finder
    'Private Sub TxtfndFromLoadOut__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtfndFromLoadOut._MYValidating
    '    Dim Qry As String = "select  Shipment_No as [LoadOutNo],Shipment_ID as [Id],Shipment_Date as [Loadout Date],Cust_Name as [Customer Name],Route_No as [Route No] from TSPL_SHIPMENT_MASTER "
    '    TxtfndFromLoadOut.Value = clsCommon.ShowSelectForm("Load Out No", Qry, "LoadOutNo", "", TxtfndFromLoadOut.Value, "ID", isButtonClicked)
    '    intFromLoadNo = Convert.ToInt32(clsDBFuncationality.getSingleValue("select Shipment_ID from TSPL_SHIPMENT_MASTER where Shipment_No ='" + TxtfndFromLoadOut.Value + "'"))
    'End Sub

    'Private Sub TxtfndToLoadOut__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtfndToLoadOut._MYValidating
    '    Dim Qry As String = "select  Shipment_No as [LoadOutNo],Shipment_ID as [Id],Shipment_Date as [Loadout Date],Cust_Name as [Customer Name],Route_No as [Route No] from TSPL_SHIPMENT_MASTER"
    '    TxtfndToLoadOut.Value = clsCommon.ShowSelectForm("Load Out No", Qry, "LoadOutNo", "", TxtfndToLoadOut.Value, "ID", isButtonClicked)
    '    intToLoadNo = Convert.ToInt32(clsDBFuncationality.getSingleValue("select  Shipment_ID from TSPL_SHIPMENT_MASTER where Shipment_No ='" + TxtfndToLoadOut.Value + "'"))

    'End Sub
    Sub FilledLoadOut()
        chkLocAll.IsChecked = True
        Dim qry As String = "    select  Sale_Invoice_No  as [LoadOutNo],Sale_Invoice_ID  as [Id],Sale_Invoice_Date  as [Loadout Date],Cust_Name as [Customer Name],Route_No as [Route No] from TSPL_SALE_INVOICE_HEAD  "
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
            qry += " union "
            qry += "    select  Transfer_No   as [LoadOutNo],''  as [Id],Transfer_Date   as [Loadout Date],'' as [Customer Name],Route_No as [Route No] from TSPL_TRANSFER_HEAD   where Tax_Group='BEDT' "
        End If
        chkgv1.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkgv1.ValueMember = "LoadOutNo"
        chkgv1.DisplayMember = "Loadout Date"
       End Sub


    Private Sub TxtfndCustCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtfndCustCategory._MYValidating
        Dim Qry As String = "select DISTINCT TSPL_CUSTOMER_MASTER.Cust_Type_Code as [Code],TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc as [Description] from TSPL_CUSTOMER_MASTER inner join TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_MASTER.Cust_Type_Code=TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code"
        TxtfndCustCategory.Value = clsCommon.ShowSelectForm("Customer Category", Qry, "Code", "", TxtfndCustCategory.Value, "Code", isButtonClicked)
        Dim CustCatgDesc As String = clsDBFuncationality.getSingleValue("select DISTINCT TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc as [Description] from TSPL_CUSTOMER_MASTER inner join TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_MASTER.Cust_Type_Code=TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code where TSPL_CUSTOMER_MASTER.Cust_Type_Code ='" + TxtfndCustCategory.Value + "'")
        lblCustDesc.Text = CustCatgDesc
    End Sub
    '' Code Ends here----

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        chkgv1.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged

    End Sub
End Class
