Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmCorrectionforWrongEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Is_RGP_After_PO As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Visible = MyBase.isPrintFlag
        'btncancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsGRNHead()
            obj = clsGRNHead.GetData(strCode, NavTyep)
            txtDocNo.Value = obj.GRN_No
            txtRefNo.Text = obj.Ref_No
            txtBillToLocation.Value = obj.Bill_To_Location
            lblBillToLocation.Text = obj.BillToLocationName
            txtVendorNo.Value = obj.Vendor_Code
            lblVendorName.Text = obj.Vendor_Name
            txtItemCode.Text = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_GRN_DETAIL where GRN_No = '" + obj.GRN_No + "'  ")
            txtItemName.Text = clsDBFuncationality.getSingleValue("select Item_Code from TSPL_GRN_DETAIL where GRN_No = '" + obj.GRN_No + "'  ")
            txtItemType.Text = obj.Item_Type
            txtReqNo.Text = clsDBFuncationality.getSingleValue("select Against_PO from TSPL_GRN_HEAD where GRN_No = '" + obj.GRN_No + "'")
            txtDate.Value = obj.GRN_Date
            txtVehicleNo.Text = obj.VehicleNo
            txtGRNo.Text = obj.GRNo
            txtLRNo.Text = obj.LR_No
            txtChallanNo.Text = obj.Invoiceno
            If obj.InvoiceDate IsNot Nothing AndAlso clsCommon.myLen(obj.InvoiceDate) > 0 AndAlso IsDate(obj.InvoiceDate) Then
                txtInvoiceDate.Value = obj.InvoiceDate
            End If
            txtGENo.Text = obj.GENo
            If obj.GEDate.HasValue Then
                txtGEDate.Value = obj.GEDate
            End If
            TxtWeighment.Text = clsDBFuncationality.getSingleValue(" Select Weighment_Code from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No where Against_GRN_No = '" + obj.GRN_No + "'  ")
            WeighmetDate.Value = clsDBFuncationality.getSingleValue(" Select Weighment_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No  where Against_GRN_No = '" + obj.GRN_No + "'  ")
            txtMRN.Text = clsDBFuncationality.getSingleValue(" Select MRN_No from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "'  ")
            MRNDate.Value = clsDBFuncationality.getSingleValue(" Select MRN_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "'  ")
            txtSRN.Text = clsDBFuncationality.getSingleValue("Select SRN_No from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "' ")
            SRNDate.Value = clsDBFuncationality.getSingleValue("Select SRN_Date from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_GRN= TSPL_GRN_HEAD.GRN_No where Against_GRN = '" + obj.GRN_No + "' ")
            txtPINo.Text = clsDBFuncationality.getSingleValue(" Select PI_No from TSPL_PI_HEAD where Against_GRN='" + obj.GRN_No + "'")
            txtPINo.Text = obj.PINo
            If obj.PINo IsNot Nothing AndAlso obj.PINo.Count > 0 Then
                txtInvoiceDate.Enabled = False
                txtChallanNo.Enabled = False
            Else
                txtInvoiceDate.Enabled = True
                txtChallanNo.Enabled = True
            End If
            'TxtWeighment.Text = obj.WeighmentNo
            obj.WeighmentNo = TxtWeighment.Text
            If clsCommon.myLen(obj.WeighmentNo) <= 0 Then
                MRNDate.ReadOnly = False
            Else
                MRNDate.ReadOnly = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub



    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating

        Dim qry As String = "select TSPL_GRN_HEAD.GRN_No as Code,FORMAT(CAST(GRN_Date AS DATETIME),'dd/MM/yyyy hh:mm tt') AS Date,TSPL_GRN_HEAD.Vendor_Code as [Vendor Code], TSPL_GRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],GRN_Total_Amt as Amount,case when TSPL_GRN_HEAD.IsCancel=1 then 'Cancel' when TSPL_GRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_GRN_HEAD.Against_PO as [Against PO Code] "
        If Is_RGP_After_PO Then
            qry += ",TSPL_GRN_HEAD.Against_RGP_No as [Against RGP Code] "
        End If
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as [Tendor No]
                 ,TSPL_GRN_HEAD.VehicleNo as [Vehicle No]
                 ,case when VisualQCRequired.Is_Visual_QC=0 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else 'Pending' end as [Visual QC Status]
                 ,case when VisualQCRequired.Is_Visual_QC=0 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else 'Pending' end as [Visual QC Second Status]
                 from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_GRN_HEAD.Vendor_Code 
                 left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO 
                left outer join (
                SELECT TSPL_GRN_DETAIL.GRN_No as GRN_No,MAX(TSPL_ITEM_MASTER.Visual_QC) AS Is_Visual_QC FROM TSPL_GRN_DETAIL
                LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_GRN_DETAIL.ITEM_CODE
                GROUP BY TSPL_GRN_DETAIL.GRN_No) as VisualQCRequired on TSPL_GRN_HEAD.grn_no=VisualQCRequired.GRN_No"
        Dim whrClas As String = "  2=2  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("GRNFND", qry, "Code", whrClas, txtDocNo.Value, "GRN_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator

        Try
            'Dim qst As String = "select count(*) from TSPL_GRN_HEAD where GRN_No='" + txtDocNo.Value + "' and  GRN_Total_Amt>0"
            Dim qst As String = "select count(*) from TSPL_GRN_HEAD where GRN_No='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        UpdateD()
    End Sub

    Sub UpdateD()
        Try
            Dim obj As New clsGRNHead()
            Dim vehicleNo As String = txtVehicleNo.Text
            Dim trans As SqlTransaction = Nothing
            obj.GRN_No = txtDocNo.Value

            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, PurchaseOrder_Date,103) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" + txtReqNo.Text + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                Throw New Exception("Date cannot be less than from PO Date")
            End If

            obj.GRN_Date = clsCommon.myCDate(txtDate.Value, "yyyy/MMM/dd hh:mm:ss.ttt")
            obj.VehicleNo = txtVehicleNo.Text
            obj.LR_No = txtLRNo.Text
            obj.Item_Code = txtItemName.Text
            obj.Item_Desc = txtItemCode.Text
            obj.Invoiceno = txtChallanNo.Text
            obj.InvoiceDate = clsCommon.myCDate(txtInvoiceDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            If obj.InvoiceDate > clsCommon.GETSERVERDATE Then
                Throw New Exception("InvoiceDate should be less than Server Date")
            Else
                obj.InvoiceDate = clsCommon.myCDate(txtInvoiceDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            End If
            obj.GRNo = txtGRNo.Text
            obj.GENo = txtGENo.Text
            obj.GEDate = clsCommon.myCDate(txtGEDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            If obj.GEDate <= obj.GRN_Date Then
                obj.GEDate = clsCommon.myCDate(txtGEDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            Else
                Throw New Exception("GateEntryDate should be less than or Equalto GRN Date")
            End If
            obj.WeighmentNo = TxtWeighment.Text
            obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            obj.MRNNo = txtMRN.Text
            'obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
            obj.PINo = txtPINo.Text
            obj.SRNDate = SRNDate.Value

            If clsCommon.myLen(obj.WeighmentNo) > 0 Then
                If obj.WeighmentDate >= obj.GRN_Date Then
                    obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    Throw New Exception("WeighmentDate should be greater than or Equalto GRN Date")
                End If

                If obj.WeighmentDate < obj.SRNDate Then
                    obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    Throw New Exception("WeighmentDate should be lesser than SRNDate")
                End If


                If clsCommon.myLen(obj.SRNNo) <= 0 Then
                    If obj.WeighmentDate <= clsCommon.GETSERVERDATE() Then
                        obj.WeighmentDate = clsCommon.myCDate(WeighmetDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                    Else
                        Throw New Exception("WeighmentDate cannot be greater than serverdate")
                    End If
                End If
            End If

            If clsCommon.myLen(obj.WeighmentNo) <= 0 Then
                MRNDate.ReadOnly = False
                If obj.MRNDate >= obj.GRN_Date Then
                    obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    Throw New Exception("MRNDate cannot be lesser than GRNdate")
                End If
                If obj.MRNDate < obj.SRNDate Then
                    obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                Else
                    Throw New Exception("MRNDate cannot be greater than SRNdate")
                End If

                If clsCommon.myLen(obj.SRNNo) <= 0 Then
                    If obj.MRNDate <= clsCommon.GETSERVERDATE() Then
                        obj.MRNDate = clsCommon.myCDate(MRNDate.Value, "dd/MM/yyyy hh:mm:ss.ttt")
                    Else
                        Throw New Exception("SRNDate cannot be greater than serverdate")
                    End If
                End If
            End If
            If clsGRNHead.UpdateData(obj, trans) Then
                common.clsCommon.MyMessageBoxShow("Data Updated Successfully")
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                Throw New Exception("No Updation")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        txtBillToLocation.Value = ""
        txtChallanNo.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDocNo.Value = ""
        txtGEDate.Value = clsCommon.GETSERVERDATE()
        txtGENo.Text = ""
        txtGRNo.Text = ""
        txtInvoiceDate.Value = clsCommon.GETSERVERDATE()
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtItemType.Text = ""
        txtLRNo.Text = ""
        txtRefNo.Text = ""
        txtVehicleNo.Text = ""
        txtVendorNo.Value = ""
        lblBillToLocation.Text = ""
        lblVendorName.Text = ""
        txtPINo.Text = ""
        txtMRN.Text = ""
        TxtWeighment.Text = ""
        MRNDate.Value = clsCommon.GETSERVERDATE()
        WeighmetDate.Value = clsCommon.GETSERVERDATE()
        txtReqNo.Text = ""
        txtSRN.Text = ""
        SRNDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub frmCorrectionforWrongEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If

    End Sub
End Class