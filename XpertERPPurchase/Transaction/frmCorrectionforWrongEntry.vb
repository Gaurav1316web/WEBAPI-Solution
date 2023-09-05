Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

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

        'LoadData(clsCommon.ShowSelectForm("GRNFND", qry, "Code", whrClas, txtDocNo.Value, "GRN_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub



End Class