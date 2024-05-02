Imports System.Data.SqlClient
Imports System.IO
Imports common
'''' <summary>
'''' ''''''''''''''''''''''''TicketNo='BM00000001540''''''''''''''''''''''''''''''''''''''''
'=============================update by Preeti gupta Against ticket no[8662]
'''' </summary>
'''' <remarks></remarks>

Public Class frmMilkSRNMCC
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colHSNNo As String = "COLHSNNo"
    Const ColQty As String = "ColQty"
    Const ColACC_Qty As String = "ColACC_Qty"
    Const ColUOM As String = "ColUOM"
    Const ColPriceCode As String = "ColPriceCode"
    Const ColFatP As String = "ColFatP"
    Const COlCLR As String = "COlCLR"
    Const COlSNFP As String = "COlSNFP"
    Const ColFATKg As String = "ColFATKg"
    Const COlSNFKg As String = "COlSNFKg"
    Const COlRate As String = "COlRate"
    Const ColAmount As String = "ColAmount"

    Const colService_Charge As String = "colService_Charge"
    Const colCOMMISSION As String = "colCOMMISSION"
    Const colCOMMISSIONAmount As String = "colCOMMISSIONAmount"
    Const colPaymentCOMMISSION As String = "colPaymentCOMMISSION"
    Const colPaymentCOMMISSIONAmount As String = "colPaymentCOMMISSIONAmount"
    Const ColServiceTaxAmount As String = "ColServiceTaxAmount"
    Const ColNetAmount As String = "ColNetAmount"
    Const ColRoundOff As String = "ColRoundOff"

    Const ColCORRECTIONFACTOR As String = "ColCORRECTIONFACTOR"
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Public DtMilkReceipt As DataTable
    Dim objSr As New clsWeighingMachine
    Dim objSerial As New clsSerialPort
    Public Shared strDocumentNo As String = ""
    Dim IsRoundOffPaiseAmount As Boolean
    Dim MilkWeight_Setting As Double
    Dim isPickCLRInsteadOfSNF As Boolean = False
    Dim dclCorrectionFactor As Decimal = 0
    Dim settMaxReceiveSNFPer As Decimal = 0
    Dim settMaxFATPerLimit As Decimal = 0
    Dim settMaxSNFPerLimit As Decimal = 0
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkSRN)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If

        End If
        'btnPrint.Visible = MyBase.isModifyFlag
    End Sub

    Sub PrintDataNew()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Document number not found")
            End If
            Dim ArrSrnNo As New ArrayList() '' Added By abhishek kumar as on 13 july 2012 For get DocNo
            ArrSrnNo.Add(txtCode.Value)
            SRNPrintOut(Nothing, Nothing, False, ArrSrnNo, Nothing, Nothing)

            ''  commented panch raj on saying Amit Sir (print format must be same for all types of items)

            'Dim qry As String = "select Item_Type from TSPL_SRN_HEAD where SRN_No='" + txtDocNo.Value + "'"

            'If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "F") = CompairStringResult.Equal Then
            '    'PrintForFinishGoods()
            '    '' Added By abhishek kumar as on 13 july 2012 For Finished Goods.
            '    Obj.SRNPrintOut(Nothing, Nothing, False, ArrSrnNo, Nothing, Nothing)
            'Else
            '    'print()
            '    '' Added By abhishek kumar as on 13 july 2012 For RMOther Type
            '    Obj.SRNPrintOut(Nothing, Nothing, False, ArrSrnNo, Nothing, Nothing)

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub SRNPrintOut(ByVal FromDate As Date?, ByVal ToDate As Date?, ByVal IsDocTypeFinsihGoods As Boolean, ByVal ArrSrnNo As ArrayList, ByVal ArrVendor As ArrayList, ByVal ArrLocation As ArrayList)
        Dim qry As String

        Try
            '          qry = "SELECT TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_MILK_SRN_HEAD.VLC_DOC_CODE,TSPL_MILK_SRN_HEAD.Created_By ,TSPL_MILK_SRN_HEAD .Modified_By,   TSPL_MILK_SRN_HEAD.DOC_CODE, TSPL_MILK_SRN_HEAD.DOC_DATE, TSPL_VENDOR_MASTER.Vendor_Name, sh.DOC_DATE as Sample_Date, sh.DOC_CODE as Sample_No,TSPL_MILK_SRN_DETAIL.AMOUNT,TSPL_MILK_SRN_DETAIL.AMOUNT, TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_MILK_SRN_DETAIL .RATE as Landed_Cost_Rate, TSPL_MILK_SRN_DETAIL.Item_Code,itm.Item_Desc,itm.Unit_Code Unit_code,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_HEAD.Vsp_Code,TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3,  TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_MILK_SRN_DETAIL.Qty,FAT,SNF,pih.DOC_CODE as InvNo,pih.DOC_DATE as invdate," _
            '          & "  TSPL_MILK_SRN_HEAD.VEHICLE_CODE as vehicle_Code,vm.Description as vehicle_name,TSPL_MILK_SRN_HEAD.ROUTE_CODE,rm.Route_Name " _
            '          & "  FROM    TSPL_MILK_SRN_DETAIL INNER JOIN TSPL_MILK_SRN_HEAD ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " _
            '          & "  INNER JOIN TSPL_COMPANY_MASTER ON TSPL_MILK_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code " _
            '& " INNER JOIN TSPL_VENDOR_MASTER ON TSPL_MILK_SRN_HEAD.VSP_CODE = TSPL_VENDOR_MASTER.Vendor_Code inner join TSPL_MILK_SAMPLE_DETAIL sd on sd.MILK_SRN_CODE= TSPL_MILK_SRN_DETAIL.DOC_CODE  inner join TSPL_MILK_SAMPLE_Head sh on sh.doc_code= sd.DOC_CODE left join TSPL_ITEM_MASTER itm on itm.Item_Code=sd.Item_Code left join TSPL_MILK_PURCHASE_INVOICE_DETAIL pid on pid.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE left join TSPL_MILK_PURCHASE_INVOICE_HEAD pih on pih.DOC_CODE=pid.DOC_CODE left join TSPL_Primary_Vehicle_Master vm on vm.Vehicle_Code=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MCC_ROUTE_MASTER rm on rm.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE "

            qry = "select  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_MILK_SRN_HEAD.Created_By,TSPL_MILK_SRN_HEAD.Modified_By AS Modify_By, TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name  as comp_Name,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_add3,  concat('Tin No. : ' ,TSPL_LOCATION_MASTER.TIN_No) AS Loc_Tin_No, concat('Pin : ',TSPL_LOCATION_MASTER.Pin_Code) AS Loc_Pin_Code, concat('Ph. : ',CASE WHEN ISNULL(TSPL_LOCATION_MASTER.Phone1, '') = '(+__)__________' THEN '' ELSE TSPL_LOCATION_MASTER.Phone1 END + CASE WHEN ISNULL(TSPL_LOCATION_MASTER.Phone2, '') <> '(+__)__________' THEN ', ' + TSPL_LOCATION_MASTER.Phone2 ELSE '' END) AS Loc_Phn ,TSPL_MILK_SRN_HEAD.DOC_CODE  ,convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) as SRN_Date ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_ITEM_MASTER.Item_Desc " & _
                " ,TSPL_MILK_SRN_DETAIL.ACC_Qty as Net_Weight ,TSPL_MILK_SRN_DETAIL.fat_per ,TSPL_MILK_SRN_DETAIL.snf_Per,TSPL_MILK_SRN_DETAIL.UOM_Code  " & _
                 " ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ', '+TSPL_LOCATION_MASTER.City_Code else ' ' end + case when len(TSPL_LOCATION_MASTER.State )>0 then TSPL_LOCATION_MASTER.State else '' end  as Loc_address from " & _
                " TSPL_MILK_SRN_DETAIL INNER JOIN TSPL_MILK_SRN_HEAD ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_MILK_SRN_HEAD.MCC_CODE  " & _
                 " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code  " & _
                 " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  " & _
                 " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_MILK_SRN_DETAIL.Item_Code  " & _
                 " left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state " & _
                 " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code "


            'If FromDate.HasValue AndAlso ToDate.HasValue Then
            '    qry += " and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=Convert(date,'" + ToDate + "',103) "
            'End If

            'If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
            '    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
            'End If
            If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
                qry += " where TSPL_MILK_SRN_HEAD.Doc_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                'frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "MilkSRNReportThroughReport", "Milk SRN Report")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkSRN", "Milk SRN Report", clsCommon.myCDate(dt.Rows(0)("SRN_Date")))
                frmCRV = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    'Sub SaveData()
    '    Dim trans As SqlTransaction = Nothing
    '    Try
    '        If (AllowToSave()) Then
    '            trans = clsDBFuncationality.GetTransactin()
    '            Dim objHead As clsMilkReceiptMCC
    '            '' asign screen vaules in objHead
    '            objHead = New clsMilkReceiptMCC
    '            objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
    '            objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
    '            objHead.SHIFT = clsCommon.myCstr(Me.cboShift.Text)
    '            'objHead.COMM_PORT = clsCommon.myCstr(cboComPort.Text)
    '            objHead.MCC_CODE = clsCommon.myCstr(fndMccCode.Text)
    '            'objHead.MACHINE_NO = clsCommon.myCstr(txtSerialNo.Text)
    '            'objHead.TOTAL_WEIGHT = clsCommon.myCdbl(txtTotalWeight.Text)

    '            Dim objList As New List(Of clsMilkReceiptMCCDetail)

    '            Dim obj As clsMilkReceiptMCCDetail
    '            obj = New clsMilkReceiptMCCDetail
    '            obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
    '            obj.VLC_DOC_CODE = IIf(IsNothing(lblVLCCode.Tag), "", lblVLCCode.Tag) '' generate VLC_DOC_CODE by function
    '            obj.SAMPLE_NO = 0 '"" ''generate VLC_DOC_CODE by function
    '            obj.VLC_CODE = clsCommon.myCstr(fndVlcCode.Text)
    '            obj.ROUTE_CODE = clsCommon.myCstr(fndRouteCOde.Text)
    '            obj.VSP_CODE = clsCommon.myCstr(fndVSPCode.Text)
    '            obj.VEHICLE_CODE = clsCommon.myCstr(fndVehicleCode.Text)
    '            ' obj.NO_OF_CANS = clsCommon.myCstr(txtNoOfCans.Text)
    '            obj.MILK_WEIGHT = clsCommon.myCstr(txtsampleNo.Text)
    '            'obj.TYPE = clsCommon.myCstr(cboType.Text)
    '            'obj.MILK_TYPE = clsCommon.myCstr(cboMilkType.Text)
    '            obj.SAMPLE_NO_VALUES = "" '' generate sample no values

    '            obj.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
    '            obj.SHIFT = clsCommon.myCstr(Me.cboShift.Text)
    '            'obj.COMM_PORT = clsCommon.myCstr(cboComPort.Text)
    '            obj.MCC_CODE = clsCommon.myCstr(fndMccCode.Text)
    '            'obj.MACHINE_NO = clsCommon.myCstr(txtSerialNo.Text)
    '            objList.Add(obj)
    '            'End If
    '            'Next
    '            ' ''For Custom Fields
    '            ''Dim obj As New clsMilkReceiptMCC()
    '            'obj = New clsMilkReceiptMCC
    '            'obj.Form_ID = MyBase.Form_ID
    '            'obj.arrCustomFields = New List(Of clsCustomFieldValues)
    '            'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
    '            '    UcCustomFields1.GetData(obj.arrCustomFields)
    '            'End If
    '            'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
    '            '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
    '            'End If
    '            ' ''End of For Custom Fields
    '            If clsMilkReceiptMCC.SaveData(objHead, objList, trans) Then
    '                trans.Commit()
    '                UcAttachment1.SaveData(objHead.DOC_CODE)
    '                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
    '                LoadData(obj.DOC_CODE)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        trans.Rollback()
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    'Function SaveData()
    '    If AllowToSave() = False Then
    '        Return False
    '        Exit Function
    '    End If
    '    For Each grow As GridViewRowInfo In gv1.Rows
    '        If clsCommon.myCdbl(grow.Cells(COlRate).Value) <= 0 Then
    '            If clsCommon.myCdbl(grow.Cells(COlRate).Value) > 0 Then
    '                clsCommon.MyMessageBoxShow("This SRN can not be Recreate.")
    '                Return False
    '                Exit Function
    '            End If
    '            grow.Cells(ColFatP).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(ColFatP).Value) * 10) / 10
    '            grow.Cells(COlSNFP).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(COlSNFP).Value) * 10) / 10
    '            grow.Cells(COlRate).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(ColFatP).Value), clsCommon.myCdbl(grow.Cells(COlSNFP).Value), clsCommon.myCstr(fndMccCode.Text), clsCommon.myCstr(fndVlcCode.Text), IIf(cboShift.Text.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
    '            If clsCommon.myCdbl(grow.Cells(COlRate).Value) = 0 Then
    '                clsCommon.MyMessageBoxShow("Please fill rate first for selected vlc.")
    '                Return False
    '                Exit Function
    '            End If
    '            grow.Cells(ColAmount).Value = clsCommon.myCdbl(grow.Cells(COlRate).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value)
    '            gv1.CurrentRow.Cells(ColPriceCode).Value = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsCommon.myCdbl(gv1.CurrentRow.Cells(ColFatP).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(COlSNFP).Value), clsCommon.myCstr(fndMccCode.Text), clsCommon.myCstr(fndVlcCode.Text), IIf(cboShift.Text.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
    '        Else
    '            clsCommon.MyMessageBoxShow("This SRN can not be Recreate.")
    '            Return False
    '            Exit Function
    '        End If
    '    Next

    '    DtMilkReceipt = clsDBFuncationality.GetDataTable("select rd.*,rh.*,Case when Nature='C' then Actual_charges end as  commision_pers," _
    '           & " Case when Nature='E' then Actual_charges end as payment_commision_pers,Service_Charge_Type,coalesce(Rate_Head_Load,0) as Rate_Head_Load" _
    '           & " ,coalesce(Rate_Own_Asset,0) as Rate_Own_Asset,Service_Basis_Head_Load,Service_Basis_Own_Asset from TSPL_MILK_RECEIPT_HEAD rd Inner join TSPL_MILK_RECEIPT_DETAIL rh on rh.DOC_CODE=" _
    '           & " rd.DOC_CODE left join TSPL_VENDOR_MASTER on Vendor_Code=VSP_CODE")
    '    Dim DtVehicle As DataTable = clsDBFuncationality.GetDataTable("SELECT vm.* FROM  TSPL_Primary_Vehicle_Master vm ")
    '    Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING ")
    '    Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail ")


    '    Dim Milk_receipt_code As String = clsDBFuncationality.getSingleValue("select milk_receipt_code from tspl_milk_sample_Head where doc_code='" & fndMccCode.Tag & "'")
    '    Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try

    '        '        If (AllowToSave()) Then
    '        ' Trans = clsDBFuncationality.GetTransactin()
    '        Dim counter As Integer = 0
    '        Dim objHead As clsMilkSRNMCC
    '        '' asign screen vaules in objHead

    '        Dim objList As New List(Of clsMilkSRNMCCDetail)
    '        Dim obj1 As clsMilkSRNMCCDetail

    '        Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
    '        Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail

    '        Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
    '        Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail

    '        For Each grow As GridViewRowInfo In gv1.Rows

    '            objList = New List(Of clsMilkSRNMCCDetail)
    '            objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
    '            objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)

    '            Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & Milk_receipt_code & "' and vlc_DOC_Code='" & clsCommon.myCstr(fndVlcCode.Tag) & "'")
    '            'obj1.DOC_CODE = txtCode.Value

    '            objHead = New clsMilkSRNMCC
    '            objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
    '            objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
    '            objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
    '            objHead.MILK_SAMPLE_CODE = fndMccCode.Tag

    '            ' objHead.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
    '            objHead.MCC_CODE = IIf(clsCommon.myLen(clsCommon.myCstr(dr(0)("Irregular_MCC_CODE"))) > 0, clsCommon.myCstr(dr(0)("Irregular_MCC_CODE")), clsCommon.myCstr(dr(0)("MCC_CODE"))) 'clsCommon.myCstr(dr(0)("MCC_CODE"))
    '            objHead.SAMPLE_NO = clsCommon.myCdbl(txtsampleNo.Text)
    '            objHead.VLC_DOC_CODE = clsCommon.myCstr(fndVlcCode.Tag)
    '            objHead.VEHICLE_CODE = clsCommon.myCstr(dr(0)("VEHICLE_CODE"))
    '            objHead.VLC_CODE = clsCommon.myCstr(dr(0)("VLC_CODE"))
    '            objHead.ROUTE_CODE = clsCommon.myCstr(dr(0)("ROUTE_CODE"))
    '            objHead.VSP_CODE = clsCommon.myCstr(fndVSPCode.Text)
    '            If DtVehicle.Select("Vehicle_Code='" & dr(0)("VEHICLE_CODE") & "'").Length > 0 Then
    '                objHead.TransPorter = clsCommon.myCstr(DtVehicle.Select("Vehicle_Code='" & dr(0)("VEHICLE_CODE") & "'")(0)("Vendor_Code"))
    '            End If
    '            obj1 = New clsMilkSRNMCCDetail()
    '            obj1.Item_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
    '            obj1.MILK_Qty = clsCommon.myCdbl(grow.Cells(ColQty).Value)
    '            obj1.ACC_Qty = clsCommon.myCdbl(grow.Cells(ColACC_Qty).Value)
    '            obj1.FAT = clsCommon.myCdbl(grow.Cells(ColFatP).Value)
    '            obj1.SNF = clsCommon.myCdbl(grow.Cells(COlSNFP).Value)
    '            obj1.MCC_CODE = IIf(clsCommon.myLen(clsCommon.myCstr(dr(0)("Irregular_MCC_CODE"))) > 0, clsCommon.myCstr(dr(0)("Irregular_MCC_CODE")), clsCommon.myCstr(dr(0)("MCC_CODE")))
    '            obj1.Correction_Factor = 0.14
    '            obj1.RATE = clsCommon.myCdbl(grow.Cells(COlRate).Value)
    '            obj1.UOM = clsCommon.myCstr(grow.Cells(ColUOM).Value)
    '            obj1.Price_Code = clsCommon.myCstr(grow.Cells(ColPriceCode).Value)
    '            obj1.AMOUNT = clsCommon.myCdbl(grow.Cells(ColAmount).Value)
    '            obj1.Commission = clsCommon.myCdbl(dr(0)("commision_pers"))
    '            obj1.Head_Load_Rate = clsCommon.myCdbl(dr(0)("Rate_Head_Load"))
    '            obj1.Own_Asset_Rate = clsCommon.myCdbl(dr(0)("Rate_Own_Asset"))
    '            obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("payment_commision_pers"))
    '            If clsCommon.myCstr(dr(0)("Service_Charge_Type")) = "%(Percentage)" Then
    '                obj1.Commission_Amount = Math.Round(obj1.AMOUNT * obj1.Commission / 100, 2)
    '                obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission / 100, 2)
    '            ElseIf clsCommon.myCstr(dr(0)("Service_Charge_Type")) = "Rate/Kg" Then
    '                obj1.Commission_Amount = Math.Round(obj1.ACC_Qty * obj1.Commission, 2)
    '                obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
    '            ElseIf clsCommon.myCstr(dr(0)("Service_Charge_Type")) = "Rate/Ltr" And clsCommon.myCstr(dr(0)("UOM_Code")) = "LTR" Then
    '                obj1.Commission_Amount = Math.Round(obj1.MILK_Qty * obj1.Commission, 2)
    '                obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
    '            End If
    '            obj1.Service_Charge_Type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
    '            '==================Head Load==========================
    '            Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, Trans))
    '            Dim dclDistanceKM As Decimal = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("DistanceKM_Head_Load"))
    '            If dclDistanceKM = 0 Then
    '                dclDistanceKM = 1
    '            End If
    '            If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Head_Load")), "K") = CompairStringResult.Equal Then
    '                If obj1.ACC_Qty >= MinimumQtyForHeadLoad Then
    '                    obj1.Head_Load_Amount = Math.Round(obj1.ACC_Qty * obj1.Head_Load_Rate * dclDistanceKM, 2)
    '                End If
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Head_Load")), "L") = CompairStringResult.Equal Then
    '                If clsCommon.myCDecimal(dr(0)("ACC_WEIGHT_LTR")) >= MinimumQtyForHeadLoad Then
    '                    obj1.Head_Load_Amount = Math.Round(clsCommon.myCDecimal(dr(0)("ACC_WEIGHT_LTR")) * obj1.Head_Load_Rate * dclDistanceKM, 2)
    '                End If
    '            End If
    '            obj1.Head_Load_Type = clsCommon.myCstr(dr(0)("Service_Basis_Head_Load"))
    '            '============================================
    '            '==================Own Asset==========================
    '            If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
    '                obj1.Own_Asset_Amount = Math.Round(obj1.ACC_Qty * obj1.Own_Asset_Rate, 2)
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
    '                obj1.Own_Asset_Amount = Math.Round(obj1.MILK_Qty * obj1.Own_Asset_Rate, 2)
    '            End If
    '            obj1.Own_Asset_Type = clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset"))
    '            '============================================

    '            'If clsCommon.myCdbl(obj1.Commission) > 0 Then
    '            '    obj1.Commission_Amount = Math.Round(obj1.AMOUNT * obj1.Commission, 2)
    '            'End If
    '            'If clsCommon.myCdbl(obj1.Payment_Commission) > 0 Then
    '            '    obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission, 2)
    '            obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT + obj1.Emp_Amount, 2)
    '            'Else
    '            '    obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT, 2)
    '            'End If
    '            'obj1.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
    '            objList.Add(obj1)

    '            '============VSp Charge Detail=====================
    '            For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Select("Vsp_Code='" & objHead.VSP_CODE & "'")
    '                objVSP_Charge1 = New clsMilkSRNVSpChargeDetail()
    '                objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objHead.VSP_CODE)
    '                objVSP_Charge1.Vlc_Doc_Code = clsCommon.myCstr(fndVlcCode.Tag)
    '                objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
    '                objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
    '                objVSP_Charge1.Service_Type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
    '                If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
    '                    objVSP_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
    '                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
    '                    objVSP_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
    '                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dr(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
    '                    objVSP_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
    '                End If
    '                objVSPChargeList.Add(objVSP_Charge1)
    '            Next
    '            '===========================================


    '            '============Price Charge Detail=====================
    '            For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Select("Price_Code='" & obj1.Price_Code & "'")
    '                objPrice_Charge1 = New clsMilkSRNPriceChargeDetail()
    '                objPrice_Charge1.Price_Code = clsCommon.myCstr(obj1.Price_Code)
    '                objPrice_Charge1.Vlc_Doc_Code = clsCommon.myCstr(fndVlcCode.Tag)
    '                objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
    '                objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
    '                objPrice_Charge1.Service_type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
    '                If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
    '                    objPrice_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
    '                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
    '                    objPrice_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
    '                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dr(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
    '                    objPrice_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
    '                End If
    '                objPriceChargeList.Add(objPrice_Charge1)
    '            Next
    '            '===========================================

    '            If Not clsMilkSRNMCC.SaveDataFromSRNFrom(objHead, objList, objVSPChargeList, objPriceChargeList, Trans) Then
    '                Trans.Rollback()
    '                Return False
    '            Else
    '                clsMilkSRNMCC.UpdateSample(objHead.MILK_SAMPLE_CODE, objHead.SAMPLE_NO, objList(0).FAT, objList(0).SNF, objList(0).RATE, objList(0).AMOUNT, Trans, "", 0, 0)
    '                Trans.Commit()
    '                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
    '                'clsCommon.ProgressBarUpdate(counter & "/" & gv1.Rows.Count)
    '            End If
    '        Next
    '        Return True
    '        'If clsMilkSampleMCC.SaveData(objHead, objList, trans) Then
    '        ' Trans.Commit()
    '        'clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
    '        'End If
    '        '        End If
    '    Catch ex As Exception
    '        Trans.Rollback()
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '        Return False
    '    End Try
    'End Function

    Private Function AllowToSave() As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Select()
                Return False
            End If
            '=======================================================
            'If clsCommon.myLen(Me.cboShift.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter Shift", Me.Text)
            '    Return False
            'End If

            'If clsCommon.myLen(Me.fndMccCode.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter MCC", Me.Text)
            '    Return False
            'End If

            'If clsCommon.myLen(Me.fndVlcCode.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter VLC Code", Me.Text)
            '    Return False
            'End If

            'If clsCommon.myLen(Me.fndVSPCode.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter VSP Code", Me.Text)
            '    Return False
            'End If

            'If clsCommon.myCdbl(Me.txtsampleNo.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter Milk Weight", Me.Text)
            '    Return False
            'End If
            'Dim sQuery As String = "select count(*) from tspl_Milk_srn_Head where doc_code='" & txtCode.Value & "' and coalesce(is_Incentive_Created,'N')<>'N'"
            'Dim isinvoiceCreated As Integer = clsDBFuncationality.getSingleValue(sQuery)
            'If isinvoiceCreated > 0 Then
            '    clsCommon.MyMessageBoxShow("This SRN.[" & txtCode.Value & "] Invoice has been Created", "Save")
            '    Return False
            'End If
            'If clsCommon.myLen(Me.cboType.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter Type", Me.Text)
            '    Return False
            'End If

            'If clsCommon.myLen(Me.cboMilkType.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter Milk Type", Me.Text)
            '    Return False
            'End If

            'If UsLock1.Status = ERPTransactionStatus.Approved Then
            '    clsCommon.MyMessageBoxShow("This Document is Approved and can not take new entries..", Me.Text)
            '    Return False
            'End If

            'UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub AddNew()
        ' isNewEntry = True
        txtCode.Value = ""
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.cboShift.SelectedIndex = -1
        Me.fndMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        lblMccName.Text = clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & fndMccCode.Text & "'")
        Me.fndVlcCode.Text = Nothing
        Me.fndVSPCode.Text = Nothing
        Me.fndRouteCOde.Text = Nothing
        txtsampleNo.Text = Nothing
        'txtTotalWeight.Text = Nothing
        lblRouteDesc.Text = Nothing
        lblVehicleDesc.Text = Nothing
        lblVSPDesc.Text = Nothing
        lblVLCDesc.Text = Nothing
        txtCustomerName.Text = Nothing
        lblTransporter.Text = Nothing
        Me.fndVehicleCode.Text = Nothing
        Me.txtsampleNo.Text = 0
        'Me.txtNoOfCans.Text = 0
        'Me.cboMilkType.SelectedValue = 0
        'Me.cboType.SelectedIndex = -1
        lblVLCCode.Tag = Nothing
        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        UcAttachment1.BlankAllControls()
        ''End of For Custom Fields
        'objSerial.SetPortNameValues(cboComPort)
    End Sub

    Public Sub GetshiftType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
    End Sub

    Sub LoadDatainControls(ByVal vlccode As String)
        txtCode.Value = ""
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.cboShift.SelectedIndex = -1
        Me.fndMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        lblMccName.Text = clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & fndMccCode.Text & "'")
        Me.fndVlcCode.Text = Nothing
        Me.fndVSPCode.Text = Nothing
        Me.fndRouteCOde.Text = Nothing
        txtsampleNo.Text = Nothing
        txtCustomerName.Text = Nothing
        lblTransporter.Text = Nothing
        'txtTotalWeight.Text = Nothing
        lblRouteDesc.Text = Nothing
        lblVehicleDesc.Text = Nothing
        lblVSPDesc.Text = Nothing
        lblVLCDesc.Text = Nothing
        Me.fndVehicleCode.Text = Nothing
        Me.txtsampleNo.Text = 0
        ' Me.txtNoOfCans.Text = 0
        ' Me.cboMilkType.SelectedIndex = -1
        ' Me.cboType.SelectedIndex = -1

        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields

    End Sub

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintDataNew()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsMilkReceiptMCC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dclCorrectionFactor = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
        isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
        MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
        settMaxReceiveSNFPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Nothing))
        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"
        IsRoundOffPaiseAmount = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1
        ''End of For Custom Fields
        AddNew()
        ' LoadData(Me.txtCode.Value)
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowEditRow = False
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = True
        GetshiftType()
        ReStoreGridLayout()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, , NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), , NavigatorType.Current)
        End If
    End Sub


    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.E Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.RecreateConsumptionEntry
            pwd.strType = clsFixedParameterType.RecreateConsumptionEntry
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                Panel1.Visible = True
            End If
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            '    If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
            '        Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='MilkSetting' and TYPE='MCCMilkSRNRepost'")
            '        Dim pwd As New FrmPWD(Nothing)
            '        pwd.strCode = "MilkSetting"
            '        pwd.strType = "MCCMilkSRNRepost"
            '        pwd.ShowDialog()
            '        If pwd.isPasswordCorrect Then
            '            SaveData()
            '        End If
            '    End If
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            ' AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.K AndAlso MyBase.isModifyFlag Then
            If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then ''Should not run for UDL
                If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                    Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='MilkSetting' and TYPE='MCCMilkSRNRepost'")
                    Dim pwd As New FrmPWD(Nothing)
                    pwd.strCode = "MilkSetting"
                    pwd.strType = "MCCMilkSRNRepost"
                    pwd.ShowDialog()
                    If pwd.isPasswordCorrect Then
                        ImportSRNUpdate()
                    End If
                End If
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.B AndAlso MyBase.isModifyFlag Then
            If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then ''Should not run for UDL
                If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                    'Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='MilkSetting' and TYPE='MCCMilkSRNRepost'")
                    Dim pwd As New FrmPWD(Nothing)
                    pwd.strCode = clsFixedParameterCode.MilkSetting
                    pwd.strType = clsFixedParameterType.MCCMilkSRNRepost
                    pwd.ShowDialog()
                    If pwd.isPasswordCorrect Then
                        ImportSRNUpdate(True)
                    End If
                End If
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.X Then
            If Me.isPostFlag Then
                'Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='MilkSetting' and TYPE='MCCMilkSRNRepost'")
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.MilkSetting
                pwd.strType = clsFixedParameterType.MCCMilkSRNRepost
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    ImportCorrection()
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnPrint, "=========Setting Name======" + Environment.NewLine + _
                          "MilkProcuremntPickCLRInsteadOfSNF (For Milk Procuremnt Pick CLR Instead Of SNF)" + Environment.NewLine + _
                          "RoundOffPaiseAmount")

        End If
    End Sub
#End Region


    Private Function GetFATSNFQTY() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SRNFATSNF"
        dr("Name") = IIf(isPickCLRInsteadOfSNF, "SRN FAT CLR", IIf(objCommonVar.DisplayTypeInMilkReceipt, "SRN TYPE FAT SNF", "SRN FAT SNF"))
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SRNQTY"
        dr("Name") = "SRN QTY"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SRNVLC"
        dr("Name") = "SRN VLC"
        dt.Rows.Add(dr)

        Return dt
    End Function


    Private Sub ImportCorrection()
        Dim gvImport As New RadGridView()
        Me.Controls.Add(gvImport)
        Try
            Dim frm As New XpertERPEngine.FrmFreeComboBox()
            frm.ComboSource = GetFATSNFQTY()
            frm.ComboValueMember = "Code"
            frm.ComboDisplayMember = "Name"
            frm.ShowDialog()
            If clsCommon.myLen(frm.strRetValue) > 0 Then
                Try
                    Dim CorrTypeSRNQty As Boolean = False
                    Dim CorrTypeSRNFATSNF As Boolean = False
                    Dim CorrTypeSRNVLC As Boolean = False

                    If clsCommon.CompairString(frm.strRetValue, "SRNFATSNF") = CompairStringResult.Equal Then
                        If objCommonVar.DisplayTypeInMilkReceipt Then
                            transportSql.importExcel(gvImport, "SRN No", "TYPE", "FAT", "SNF")
                        ElseIf isPickCLRInsteadOfSNF Then
                            transportSql.importExcel(gvImport, "SRN No", "FAT", "CLR")
                        Else
                            transportSql.importExcel(gvImport, "SRN No", "FAT", "SNF")
                        End If
                        CorrTypeSRNFATSNF = True

                    ElseIf clsCommon.CompairString(frm.strRetValue, "SRNQTY") = CompairStringResult.Equal Then
                        transportSql.importExcel(gvImport, "SRN No", "Qty")
                        CorrTypeSRNQty = True

                    ElseIf clsCommon.CompairString(frm.strRetValue, "SRNVLC") = CompairStringResult.Equal Then
                        transportSql.importExcel(gvImport, "SRN No", "VLC")
                        CorrTypeSRNVLC = True
                    Else
                        Throw New Exception("Wrong Format")
                    End If

                    clsCommon.ProgressBarShow()

                    Dim counter_Index As Integer = 0
                    For Each growImport As GridViewRowInfo In gvImport.Rows
                        Try
                            Dim dclQty As Decimal = 0
                            Dim strType As String = ""
                            Dim dclFAT As Decimal = 0
                            Dim dclSNF As Decimal = 0
                            Dim dclCLR As Decimal = 0
                            Dim strVLCUploaderCode As String = ""
                            If CorrTypeSRNFATSNF Then
                                If objCommonVar.DisplayTypeInMilkReceipt Then
                                    strType = clsCommon.myCstr(growImport.Cells("TYPE").Value)
                                End If
                                dclFAT = clsCommon.myCDecimal(growImport.Cells("FAT").Value)
                                If isPickCLRInsteadOfSNF Then
                                    dclCLR = clsCommon.myCDecimal(growImport.Cells("CLR").Value)
                                    dclSNF = Math.Round(clsEkoPro.getSnfOnCalculation(dclFAT, dclCLR, dclCorrectionFactor), 2, MidpointRounding.ToEven)
                                Else
                                    dclSNF = clsCommon.myCDecimal(growImport.Cells("SNF").Value)
                                End If
                            ElseIf CorrTypeSRNQty Then
                                dclQty = clsCommon.myCDecimal(growImport.Cells("Qty").Value)
                            ElseIf CorrTypeSRNVLC Then
                                strVLCUploaderCode = clsCommon.myCstr(growImport.Cells("VLC").Value)
                            End If
                            clsMilkSRNMCC.Correction(clsCommon.myCstr(growImport.Cells("SRN No").Value), CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, dclQty, strType, dclFAT, dclSNF, strVLCUploaderCode)
                        Catch ex As Exception
                            clsCommon.ProgressBarHide()
                            clsCommon.MyMessageBoxShow("Error at Row No : " + (clsCommon.myCstr(counter_Index + 1)) + Environment.NewLine + ex.Message, Me.Text)
                        End Try
#Region "Commented Code Remove after CLR Handle"
                        'Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        'Try

                        '    



                        '    Dim counter As Integer = 0
                        '    Dim Net_amt As Double = 0
                        '    Dim objHead As clsMilkSRNMCC = clsMilkSRNMCC.GetData(clsCommon.myCstr(growImport.Cells("SRN No").Value), NavigatorType.Current, Trans)
                        '    Dim strMilkReceiptCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MILK_RECEIPT_CODE from tspl_milk_sample_Head where doc_code='" & objHead.MILK_SAMPLE_CODE & "'", Trans))
                        '    If clsCommon.myLen(strMilkReceiptCode) <= 0 Then
                        '        Throw New Exception("Milk Receipt No Not found")
                        '    End If
                        '    If objHead.Failed_Sample_Status Then
                        '        Throw New Exception("SRN No -" + objHead.DOC_CODE + ".Approve failed sample so can't apply any correction on it")
                        '    End If
                        '    Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
                        '    Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail
                        '    Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
                        '    Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail

                        '    objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
                        '    objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)

                        '    Dim str As String = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & objHead.MCC_CODE & "' "
                        '    Dim Unit_Code As String = clsDBFuncationality.getSingleValue(str, Trans)
                        '    If Unit_Code = "" Then
                        '        clsCommon.MyMessageBoxShow("Fill UOM of Current Mcc")
                        '        Exit Sub
                        '    End If
                        '    str = "select UOM_Code from TSPL_Item_UOM_DETAIL where Item_CODE='" & clsMilkSRNMCC.ObjList(0).Item_CODE & "' and UOM_code='" & Unit_Code & "' "
                        '    Dim Item_Unit_Code As String = clsDBFuncationality.getSingleValue(str, Trans)
                        '    If Item_Unit_Code = "" Then
                        '        clsCommon.MyMessageBoxShow("Fill " & Unit_Code & " UOM of Current Item")
                        '        Exit Sub
                        '    End If
                        '    Dim conv_fac As Decimal = clsWeightConversionInfo.GetConversion_factor(clsMilkSRNMCC.ObjList(0).Item_CODE, Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), Trans)

                        '    Dim qry As String
                        '    Dim strMilkType As String = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                        '    If objCommonVar.DisplayTypeInMilkReceipt Then
                        '        qry = "select Type from tspl_milk_sample_detail where doc_code='" & objHead.MILK_SAMPLE_CODE & "' and sample_no='" & clsCommon.myCstr(objHead.SAMPLE_NO) & "' "
                        '        strMilkType = clsDBFuncationality.getSingleValue(qry, Trans)
                        '        If clsCommon.myLen(strMilkType) <= 0 Then
                        '            Throw New Exception("Type Not found for milk Sample Doc No [" + objHead.MILK_SAMPLE_CODE + "] and Sample No [" + clsCommon.myCstr(objHead.SAMPLE_NO) + "]")
                        '        End If
                        '    End If

                        '    If clsCommon.CompairString(frm.strRetValue, "SRNQTY") = CompairStringResult.Equal Then
                        '        Dim dblQty As Decimal = clsCommon.myCdbl(growImport.Cells("Qty").Value)
                        '        Dim dblLTRQty As Decimal = 0
                        '        Dim Unit_CodeApply As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Milk_Receive_UOM from TSPL_VLC_MASTER_HEAD where VLC_Code='" + objHead.VLC_CODE + "'", Trans))
                        '        If clsCommon.myLen(Unit_CodeApply) > 0 Then
                        '            Unit_Code = Unit_CodeApply
                        '            conv_fac = clsWeightConversionInfo.GetConversion_factor(Unit_CodeApply, IIf(clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal, "LTR", "KG"), Trans)
                        '        End If
                        '        Dim strCustomUOM As String = clsItemMaster.GetCustomConversionUOM(clsMilkSRNMCC.ObjList(0).Item_CODE, Trans)
                        '        If clsCommon.myLen(strCustomUOM) > 0 Then
                        '            conv_fac = 1 + (clsMilkSRNMCC.ObjList(0).CLR / 1000)
                        '        End If

                        '        If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                        '            clsMilkSRNMCC.ObjList(0).ACC_Qty = dblQty
                        '            clsMilkSRNMCC.ObjList(0).MILK_Qty = dblQty
                        '            dblLTRQty = dblQty / conv_fac
                        '        Else
                        '            dblLTRQty = dblQty
                        '            clsMilkSRNMCC.ObjList(0).MILK_Qty = dblQty
                        '            clsMilkSRNMCC.ObjList(0).ACC_Qty = dblQty * conv_fac
                        '        End If
                        '        If clsCommon.myCdbl(MilkWeight_Setting) <= 0 Then
                        '            Throw New Exception("Please set Fix paratment vale of type-" + clsFixedParameterType.Milk_Can_Weight_Ratio + " and code-" + clsFixedParameterCode.MilkSetting)
                        '        End If
                        '        clsMilkSRNMCC.ObjList(0).UOM = Unit_Code
                        '        Dim NoOfCans As Integer = Math.Ceiling(clsMilkSRNMCC.ObjList(0).ACC_Qty / MilkWeight_Setting)
                        '        'Dim x As Decimal = clsERPFuncationality.myFloor(((clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT) / 100), objCommonVar.MilkSRNFATSNFDecimalPlaces)

                        '        qry = "update TSPL_MILK_RECEIPT_DETAIL set NO_OF_CANS='" + clsCommon.myCstr(NoOfCans) + "', ACC_WEIGHT='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).ACC_Qty) + "',ACC_WEIGHT_LTR='" + clsCommon.myCstr(dblLTRQty) + "',MILK_WEIGHT='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).MILK_Qty) + "',UOM_Code='" + clsMilkSRNMCC.ObjList(0).UOM + "'  where DOC_CODE='" + strMilkReceiptCode + "' and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                        '        clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        '        qry = " update TSPL_MILK_SAMPLE_DETAIL set Qty='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).MILK_Qty) + "',ACC_Qty='" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).ACC_Qty) +
                        '            "',FAT_KG=" + clsCommon.myCstr(clsERPFuncationality.myFloor(((clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT) / 100), objCommonVar.MilkSRNFATSNFDecimalPlaces)) +
                        '            ",SNF_KG=" + clsCommon.myCstr(clsERPFuncationality.myFloor(((clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT) / 100), objCommonVar.MilkSRNFATSNFDecimalPlaces)) +
                        '            ",UOM_Code='" + clsMilkSRNMCC.ObjList(0).UOM + "' " +
                        '            " where doc_code= (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where MILK_RECEIPT_CODE='" + strMilkReceiptCode + "')and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                        '        clsDBFuncationality.ExecuteNonQuery(qry, Trans)

                        '    ElseIf clsCommon.CompairString(frm.strRetValue, "SRNFATSNF") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).FAT = Math.Truncate(clsCommon.myCdbl(growImport.Cells("FAT").Value) * 10) / 10
                        '        If isPickCLRInsteadOfSNF Then ''BHA/07/06/18-000043 by balwinder on 06/06/2018
                        '            clsMilkSRNMCC.ObjList(0).CLR = Math.Truncate(clsCommon.myCdbl(growImport.Cells("CLR").Value) * 10) / 10
                        '            clsMilkSRNMCC.ObjList(0).SNF = Math.Round(clsEkoPro.getSnfOnCalculation(clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).CLR, dclCorrectionFactor), 2, MidpointRounding.ToEven)
                        '        Else
                        '            If objCommonVar.MilkProcurementSNF2DecimalPlaces Then
                        '                clsMilkSRNMCC.ObjList(0).SNF = Math.Round(clsCommon.myCdbl(growImport.Cells("SNF").Value), 2, MidpointRounding.AwayFromZero)
                        '            Else
                        '                clsMilkSRNMCC.ObjList(0).SNF = Math.Truncate(clsCommon.myCdbl(growImport.Cells("SNF").Value) * 10) / 10
                        '            End If
                        '        End If
                        '        If objCommonVar.DisplayTypeInMilkReceipt Then
                        '            ''Do not change exception "Milk Type [" by balwinder used in form.
                        '            If clsCommon.CompairString(clsCommon.myCstr(growImport.Cells("TYPE").Value), "M") = CompairStringResult.Equal Then
                        '                If objCommonVar.AddValidationofMilkTypeinsample Then
                        '                    If clsMilkSRNMCC.ObjList(0).FAT < objCommonVar.FatMinMix OrElse clsMilkSRNMCC.ObjList(0).FAT > objCommonVar.FatMaxMix Then
                        '                        Throw New Exception("Milk Type [" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "] " + Environment.NewLine + " FAT [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinMix) + " - " + clsCommon.myCstr(objCommonVar.FatMaxMix) + "]")
                        '                    ElseIf clsMilkSRNMCC.ObjList(0).SNF < objCommonVar.SNFMinMix OrElse clsMilkSRNMCC.ObjList(0).SNF > objCommonVar.SNFMaxMix Then
                        '                        Throw New Exception("Milk Type [" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinMix) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxMix) + "]")
                        '                    End If
                        '                End If
                        '                clsMilkSRNMCC.ObjList(0).Item_CODE = objCommonVar.DefaultMilkItemCode
                        '            ElseIf clsCommon.CompairString(clsCommon.myCstr(growImport.Cells("TYPE").Value), "C") = CompairStringResult.Equal Then
                        '                If objCommonVar.AddValidationofMilkTypeinsample Then
                        '                    If clsMilkSRNMCC.ObjList(0).FAT < objCommonVar.FatMinCow OrElse clsMilkSRNMCC.ObjList(0).FAT > objCommonVar.FatMaxCow Then
                        '                        Throw New Exception("Milk Type [" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "] " + Environment.NewLine + "FAT [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinCow) + " - " + clsCommon.myCstr(objCommonVar.FatMaxCow) + "]")
                        '                    ElseIf clsMilkSRNMCC.ObjList(0).SNF < objCommonVar.SNFMinCow OrElse clsMilkSRNMCC.ObjList(0).SNF > objCommonVar.SNFMaxCow Then
                        '                        Throw New Exception("Milk Type [" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinCow) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxCow) + "]")
                        '                    End If
                        '                End If
                        '                clsMilkSRNMCC.ObjList(0).Item_CODE = objCommonVar.DefaultMilkItemCodeCow
                        '            ElseIf clsCommon.CompairString(clsCommon.myCstr(growImport.Cells("TYPE").Value), "B") = CompairStringResult.Equal Then
                        '                If objCommonVar.AddValidationofMilkTypeinsample Then
                        '                    If clsMilkSRNMCC.ObjList(0).FAT < objCommonVar.FatMinBuff OrElse clsMilkSRNMCC.ObjList(0).FAT > objCommonVar.FatMaxBuff Then
                        '                        Throw New Exception("Milk Type [" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "] " + Environment.NewLine + "FAT [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).FAT) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.FatMinBuff) + " - " + clsCommon.myCstr(objCommonVar.FatMaxBuff) + "]")
                        '                    ElseIf clsMilkSRNMCC.ObjList(0).SNF < objCommonVar.SNFMinBuff OrElse clsMilkSRNMCC.ObjList(0).SNF > objCommonVar.SNFMaxBuff Then
                        '                        Throw New Exception("Milk Type [" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "] " + Environment.NewLine + "SNF [" + clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).SNF) + "]" + Environment.NewLine + "Valid Range [" + clsCommon.myCstr(objCommonVar.SNFMinBuff) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxBuff) + "]")
                        '                    End If
                        '                End If
                        '                clsMilkSRNMCC.ObjList(0).Item_CODE = objCommonVar.DefaultMilkItemCodeBuffalo
                        '            Else
                        '                Throw New Exception("Milk Type should be M/B/C")
                        '            End If
                        '            strMilkType = clsCommon.myCstr(growImport.Cells("TYPE").Value)
                        '            qry = "update TSPL_MILK_RECEIPT_DETAIL set Item_Code='" + clsMilkSRNMCC.ObjList(0).Item_CODE + "',type='" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "' where DOC_CODE='" + strMilkReceiptCode + "' and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                        '            clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        '            qry = " update TSPL_MILK_SAMPLE_DETAIL set item_code='" + clsMilkSRNMCC.ObjList(0).Item_CODE + "',type='" + clsCommon.myCstr(growImport.Cells("TYPE").Value) + "'  where doc_code= (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where MILK_RECEIPT_CODE='" + strMilkReceiptCode + "')and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                        '            clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        '        End If


                        '        If settMaxReceiveSNFPer > 0 And clsMilkSRNMCC.ObjList(0).SNF > settMaxReceiveSNFPer Then
                        '            Throw New Exception("SNF % Can't be more than -" + clsCommon.myCstr(settMaxReceiveSNFPer))
                        '        End If
                        '        If settMaxFATPerLimit > 0 And clsMilkSRNMCC.ObjList(0).FAT > settMaxFATPerLimit Then
                        '            Throw New Exception("FAT % Can't be more than -" + clsCommon.myCstr(settMaxFATPerLimit))
                        '        End If
                        '        If settMaxSNFPerLimit > 0 And clsMilkSRNMCC.ObjList(0).SNF > settMaxSNFPerLimit Then
                        '            Throw New Exception("SNF % Can't be more than -" + clsCommon.myCstr(settMaxSNFPerLimit))
                        '        End If

                        '    ElseIf clsCommon.CompairString(frm.strRetValue, "SRNVLC") = CompairStringResult.Equal Then
                        '        qry = "select VLC_Code,VSP_Code,Route_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader ='" + clsCommon.myCstr(growImport.Cells("VLC").Value) + "' and MCC='" + objHead.MCC_CODE + "'"
                        '        Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                        '        If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                        '            Throw New Exception("Not a Valid VLC Uploader Code:" + clsCommon.myCstr(growImport.Cells("VLC").Value))
                        '        End If
                        '        objHead.VLC_CODE = clsCommon.myCstr(dtTemp.Rows(0)("VLC_Code"))
                        '        objHead.VSP_CODE = clsCommon.myCstr(dtTemp.Rows(0)("VSP_Code"))
                        '        objHead.ROUTE_CODE = clsCommon.myCstr(dtTemp.Rows(0)("Route_Code"))
                        '        If clsCommon.myLen(objHead.VLC_CODE) <= 0 Then
                        '            Throw New Exception("Not a Valid VLC Uploader Code:" + clsCommon.myCstr(growImport.Cells("VLC").Value))
                        '        End If

                        '        qry = "update TSPL_MILK_SAMPLE_DETAIL set VSP_CODE='" + objHead.VSP_CODE + "'  where doc_code= '" + objHead.MILK_SAMPLE_CODE + "' and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                        '        clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        '        qry = " update TSPL_MILK_RECEIPT_DETAIL set  VLC_CODE='" + objHead.VLC_CODE + "',VSP_CODE='" + objHead.VSP_CODE + "',ROUTE_CODE='" + objHead.ROUTE_CODE + "' where DOC_CODE=(select MILK_RECEIPT_CODE from TSPL_MILK_SAMPLE_HEAD where DOC_CODE='" + objHead.MILK_SAMPLE_CODE + "') and SAMPLE_NO='" + clsCommon.myCstr(objHead.SAMPLE_NO) + "'"
                        '        clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        '        qry = "update TSPL_MILK_SRN_HEAD set VLC_CODE='" + objHead.VLC_CODE + "',VSP_CODE='" + objHead.VSP_CODE + "',ROUTE_CODE='" + objHead.ROUTE_CODE + "'  where DOC_CODE='" + objHead.DOC_CODE + "'"
                        '        clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        '    End If

                        '    qry = "select rd.*,rh.*,Case when Nature='C' then Actual_charges end as  commision_pers," _
                        '    & " Case when Nature='E' then Actual_charges end as payment_commision_pers,Service_Charge_Type,coalesce(Rate_Head_Load,0) as Rate_Head_Load" _
                        '    & " ,coalesce(Rate_Own_Asset,0) as Rate_Own_Asset,Service_Basis_Head_Load,Service_Basis_Own_Asset,TSPL_VENDOR_MASTER.EMP_Type " _
                        '    & " ,TSPL_VENDOR_MASTER.EMP_Fixed_Amount " _
                        '    & " ,TSPL_VENDOR_MASTER.Actual_charges_Slab " _
                        '    & " ,TSPL_VENDOR_MASTER.Actual_charges " _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges_Slab2" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges2" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges_Slab3" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges3" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges_Slab4" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges4" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges_Slab5" _
                        '    & ",TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix,TSPL_VENDOR_MASTER.DistanceKM_Head_Load from TSPL_MILK_RECEIPT_HEAD rd Inner join TSPL_MILK_RECEIPT_DETAIL rh on rh.DOC_CODE=" _
                        '    & " rd.DOC_CODE left join TSPL_VENDOR_MASTER on Vendor_Code=VSP_CODE where rd.DOC_CODE='" & strMilkReceiptCode & "' and rh.vlc_DOC_Code='" & objHead.VLC_DOC_CODE & "'"
                        '    DtMilkReceipt = clsDBFuncationality.GetDataTable(qry, Trans)



                        '    objHead.VEHICLE_CODE = clsCommon.myCstr(DtMilkReceipt.Rows(0)("VEHICLE_CODE"))
                        '    objHead.VLC_CODE = clsCommon.myCstr(DtMilkReceipt.Rows(0)("VLC_CODE"))
                        '    objHead.ROUTE_CODE = clsCommon.myCstr(DtMilkReceipt.Rows(0)("ROUTE_CODE"))

                        '    Dim DtVehicle As DataTable = clsDBFuncationality.GetDataTable("SELECT vm.* FROM TSPL_Primary_Vehicle_Master vm where Vehicle_Code='" & clsCommon.myCstr(DtMilkReceipt.Rows(0)("VEHICLE_CODE")) & "'", Trans)
                        '    objHead.TransPorter = clsCommon.myCstr(DtVehicle.Rows(0)("Vendor_Code"))

                        '    If isPickCLRInsteadOfSNF Then
                        '        Dim strPriceCode As String = ""
                        '        clsMilkSRNMCC.ObjList(0).RATE = clsEkoPro.getRateFromUploaderShiftWiseCLR(clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).CLR, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType, strPriceCode)
                        '        clsMilkSRNMCC.ObjList(0).Price_Code = strPriceCode
                        '    Else
                        '        clsMilkSRNMCC.ObjList(0).RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsMilkSRNMCC.ObjList(0).MILK_Qty, clsMilkSRNMCC.ObjList(0).Price_Code, clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType)
                        '        'clsMilkSRNMCC.ObjList(0).RATE = clsEkoPro.getRateFromUploaderShiftWise(clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType)
                        '        'clsMilkSRNMCC.ObjList(0).Price_Code = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, objHead.MCC_CODE, objHead.VLC_CODE, IIf(objHead.SHIFT.Contains("M"), "M", "E"), objHead.DOC_DATE, Trans, strMilkType)
                        '    End If
                        '    clsMilkSRNMCC.ObjList(0).AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).RATE * clsMilkSRNMCC.ObjList(0).MILK_Qty, 2, MidpointRounding.AwayFromZero)
                        '    clsMilkSRNMCC.ObjList(0).Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("commision_pers"))
                        '    clsMilkSRNMCC.ObjList(0).Head_Load_Rate = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Rate_Head_Load"))
                        '    clsMilkSRNMCC.ObjList(0).Own_Asset_Rate = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Rate_Own_Asset"))
                        '    clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("payment_commision_pers"))
                        '    If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                        '        If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * clsMilkSRNMCC.ObjList(0).Payment_Commission / 100, 2)
                        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                        '        Else
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = 0
                        '            'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                        '        End If
                        '        If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount += clsCommon.myCdbl(DtMilkReceipt.Rows(0)("EMP_Fixed_Amount"))
                        '        End If
                        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                        '        If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                        '            If clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges5"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges4"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges3"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).AMOUNT >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges2"))
                        '            Else
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                        '            End If
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * clsMilkSRNMCC.ObjList(0).Payment_Commission / 100, 2)
                        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                        '            If clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges5"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges4"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges3"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).ACC_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges2"))
                        '            Else
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                        '            End If
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                        '            If clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab5")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges5"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab4")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges4"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab3")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges3"))
                        '            ElseIf clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso clsMilkSRNMCC.ObjList(0).MILK_Qty >= clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges_Slab2")) Then
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges2"))
                        '            Else
                        '                clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Actual_charges"))
                        '            End If
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Payment_Commission, 2)
                        '        Else
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount = 0
                        '            'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                        '        End If
                        '        If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                        '            clsMilkSRNMCC.ObjList(0).Emp_Amount += clsCommon.myCdbl(DtMilkReceipt.Rows(0)("EMP_Fixed_Amount"))
                        '        End If
                        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("EMP_Type")), "FPSP") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).Payment_Commission = clsCommon.myCdbl(DtMilkReceipt(0)("Actual_charges"))
                        '        Dim objSPR As clsStandardPrice = clsStandardPrice.GetStandartPrice(clsMilkSRNMCC.ObjList(0).Price_Code, Trans)
                        '        If objSPR IsNot Nothing Then
                        '            If (objSPR.Std_Percent_FAT <> 0 AndAlso objSPR.Std_Percent_SNF <> 0) Then
                        '                If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                        '                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round((Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                        '                ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                        '                    Dim qty As Decimal = clsMilkSRNMCC.ObjList(0).ACC_Qty
                        '                    If conv_fac <> 0 Then
                        '                        qty = clsMilkSRNMCC.ObjList(0).ACC_Qty / conv_fac
                        '                    End If
                        '                    clsMilkSRNMCC.ObjList(0).Emp_Amount = Math.Round((Math.Round(qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 3) * clsMilkSRNMCC.ObjList(0).Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                        '                Else
                        '                    clsMilkSRNMCC.ObjList(0).Emp_Amount = 0
                        '                    'Throw New Exception("EMP Service Basis is Not valid of VSP " + clsMilkSRNMCC.ObjList(0).VlC_Code)
                        '                End If
                        '            End If
                        '        End If
                        '    Else
                        '        Throw New Exception("EMP Type is Not a valid ")
                        '    End If

                        '    If clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).TIP_Amount = Math.Round(clsCommon.myCdbl(DtMilkReceipt(0)("TIP_Cow")) * (clsMilkSRNMCC.ObjList(0).FAT + clsMilkSRNMCC.ObjList(0).SNF) * clsMilkSRNMCC.ObjList(0).ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                        '    ElseIf clsCommon.CompairString(strMilkType, "B") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).TIP_Amount = Math.Round(clsCommon.myCdbl(DtMilkReceipt(0)("TIP_Buffalo")) * clsMilkSRNMCC.ObjList(0).FAT * clsMilkSRNMCC.ObjList(0).ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                        '    Else
                        '        clsMilkSRNMCC.ObjList(0).TIP_Amount = Math.Round(clsCommon.myCdbl(DtMilkReceipt(0)("TIP_Mix")) * clsMilkSRNMCC.ObjList(0).FAT * clsMilkSRNMCC.ObjList(0).ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                        '    End If

                        '    clsMilkSRNMCC.ObjList(0).Service_Charge_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type"))
                        '    '==================Head Load==========================
                        '    Dim dclDistanceKM As Decimal = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("DistanceKM_Head_Load"))
                        '    If dclDistanceKM = 0 Then
                        '        dclDistanceKM = 1
                        '    End If
                        '    If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Head_Load")), "K") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).Head_Load_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Head_Load_Rate * dclDistanceKM, 2)
                        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Head_Load")), "L") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).Head_Load_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Head_Load_Rate * dclDistanceKM, 2)
                        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Head_Load")), "W") = CompairStringResult.Equal Then
                        '        qry = "select Ratio,SNF_Ratio,FAT_Pers,SNF_Pers from TSPL_MILK_PRICE_MASTER where Price_Code=(select top 1 Price_Code from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsMilkSRNMCC.ObjList(0).Price_Code + "')"
                        '        Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                        '        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        '            clsMilkSRNMCC.ObjList(0).FAT_KG = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 3)
                        '            clsMilkSRNMCC.ObjList(0).SNF_KG = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 3)
                        '            Dim dblFATRate As Decimal = clsMilkSRNMCC.ObjList(0).Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("FAT_Pers"))
                        '            Dim dblSNFRate As Decimal = clsMilkSRNMCC.ObjList(0).Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Pers"))
                        '            clsMilkSRNMCC.ObjList(0).Head_Load_Amount = Math.Round(((clsMilkSRNMCC.ObjList(0).FAT_KG * dblFATRate) + (clsMilkSRNMCC.ObjList(0).SNF_KG * dblSNFRate)) * dclDistanceKM, 2)
                        '        End If
                        '    End If
                        '    clsMilkSRNMCC.ObjList(0).Head_Load_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Head_Load"))
                        '    '============================================
                        '    '==================Own Asset==========================
                        '    If clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).Own_Asset_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).Own_Asset_Rate, 2)
                        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                        '        clsMilkSRNMCC.ObjList(0).Own_Asset_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsMilkSRNMCC.ObjList(0).Own_Asset_Rate, 2)
                        '    End If
                        '    clsMilkSRNMCC.ObjList(0).Own_Asset_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Basis_Own_Asset"))
                        '    '============================================

                        '    clsMilkSRNMCC.ObjList(0).Service_Charge_Amount = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * clsCommon.myCdbl(DtMilkReceipt.Rows(0)("Service_Charge_Per_Unit")), 2)
                        '    clsMilkSRNMCC.ObjList(0).NET_AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT + clsMilkSRNMCC.ObjList(0).Emp_Amount + clsMilkSRNMCC.ObjList(0).TIP_Amount - clsMilkSRNMCC.ObjList(0).Service_Charge_Amount, 2)
                        '    If IsRoundOffPaiseAmount Then
                        '        clsMilkSRNMCC.ObjList(0).Round_Off = (clsMilkSRNMCC.ObjList(0).NET_AMOUNT Mod 1)
                        '        clsMilkSRNMCC.ObjList(0).NET_AMOUNT = clsMilkSRNMCC.ObjList(0).NET_AMOUNT - (clsMilkSRNMCC.ObjList(0).NET_AMOUNT Mod 1)
                        '    End If

                        '    '============VSp Charge Detail=====================
                        '    Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING where Vsp_Code='" & objHead.VSP_CODE & "'", Trans)
                        '    For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Rows
                        '        objVSP_Charge1 = New clsMilkSRNVSpChargeDetail()
                        '        objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objHead.VSP_CODE)
                        '        objVSP_Charge1.Vlc_Doc_Code = clsCommon.myCstr(objHead.VLC_DOC_CODE)
                        '        objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
                        '        objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
                        '        objVSP_Charge1.Service_Type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type"))
                        '        If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
                        '            objVSP_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
                        '        ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
                        '            objVSP_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
                        '        ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(DtMilkReceipt.Rows(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                        '            objVSP_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
                        '        End If
                        '        objVSPChargeList.Add(objVSP_Charge1)
                        '    Next
                        '    '===========================================


                        '    '============Price Charge Detail=====================
                        '    Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail where Price_Code='" & clsMilkSRNMCC.ObjList(0).Price_Code & "'", Trans)


                        '    For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Rows
                        '        objPrice_Charge1 = New clsMilkSRNPriceChargeDetail()
                        '        objPrice_Charge1.Price_Code = clsCommon.myCstr(clsMilkSRNMCC.ObjList(0).Price_Code)
                        '        objPrice_Charge1.Vlc_Doc_Code = objHead.VLC_DOC_CODE
                        '        objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
                        '        objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
                        '        objPrice_Charge1.Service_type = clsCommon.myCstr(DtMilkReceipt.Rows(0)("Service_Charge_Type"))
                        '        If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
                        '            objPrice_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
                        '        ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
                        '            objPrice_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
                        '        ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(DtMilkReceipt.Rows(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                        '            objPrice_Charge1.AMOUNT = Math.Round(clsMilkSRNMCC.ObjList(0).MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
                        '        End If
                        '        objPriceChargeList.Add(objPrice_Charge1)
                        '    Next
                        '    '===========================================
                        '    clsMilkSRNMCC.ObjList(0).Std_Qty = clsInventoryMovementNew.GetStdQty(Trans, Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).FAT / 100, 2), Math.Round(clsMilkSRNMCC.ObjList(0).ACC_Qty * clsMilkSRNMCC.ObjList(0).SNF / 100, 2), objHead.DOC_DATE)

                        '    clsMilkSRNMCC.UpdateDataFromSRNFrom(objHead, clsMilkSRNMCC.ObjList, objVSPChargeList, objPriceChargeList, Trans)
                        '    clsMilkSRNMCC.updateJournalEntryWithTran("MI-SR", objHead.DOC_CODE, Trans)
                        '    clsMilkSRNMCC.UpdateSample(objHead.MILK_SAMPLE_CODE, objHead.SAMPLE_NO, clsMilkSRNMCC.ObjList(0).FAT, clsMilkSRNMCC.ObjList(0).SNF, clsMilkSRNMCC.ObjList(0).RATE, clsMilkSRNMCC.ObjList(0).AMOUNT, Trans, clsMilkSRNMCC.ObjList(0).Price_Code)

                        '    Trans.Commit()


                        '    objVSPChargeList = Nothing
                        '    objPriceChargeList = Nothing

                        '    objHead = Nothing

                        '    DtMilkReceipt = Nothing
                        '    DtVehicle = Nothing
                        '    DtVSPChargeDetail = Nothing
                        '    DtPriceChargeDetail = Nothing
                        '    strMilkReceiptCode = String.Empty
                        'Catch ex As Exception
                        '    Trans.Rollback()
                        '    clsCommon.ProgressBarHide()
                        '    clsCommon.MyMessageBoxShow("Error at Row No : " + (clsCommon.myCstr(counter_Index + 1)) + Environment.NewLine + ex.Message, Me.Text)
                        'End Try
#End Region
                        counter_Index += 1
                        clsCommon.ProgressBarUpdate("Total Receords Imported : " & counter_Index & " / " & gvImport.Rows.Count)
                    Next
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Task Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    clsCommon.ProgressBarHide()
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvImport)
        End Try
    End Sub

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            AddNew()
            LoadBlankGrid()
            btnPrint.Enabled = False
            Dim obj As clsMilkSRNMCC = clsMilkSRNMCC.GetData(strDoc, navType)
            txtCode.Value = obj.DOC_CODE
            'btnPrint.Text = "Update"
            fndMccCode.Text = obj.MCC_CODE
            lblMccName.Text = clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
            fndMccCode.Tag = obj.MILK_SAMPLE_CODE
            dtpDocDate.Value = obj.DOC_DATE
            cboShift.SelectedValue = obj.SHIFT
            fndVlcCode.Text = obj.VLC_CODE
            fndVSPCode.Text = obj.VSP_CODE
            fndRouteCOde.Text = obj.ROUTE_CODE
            fndVlcCode.Tag = obj.VLC_DOC_CODE
            txtsampleNo.Text = obj.SAMPLE_NO
            fndVehicleCode.Text = obj.VEHICLE_CODE
            txtCustomerName.Text = obj.TransPorter
            lblTransporter.Text = obj.TransPorter_name
            UsLock1.Status = obj.POSTED
            cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
            lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'")
            lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Text & "'")
            lblVLCDesc.Text = clsDBFuncationality.getSingleValue("select vlc_name from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndVlcCode.Text & "'")
            lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_Primary_VEHICLE_MASTER where vehicle_code='" & fndVehicleCode.Text & "'")

            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnPrint.Enabled = True
            End If

            If (clsMilkSRNMCC.ObjList IsNot Nothing AndAlso clsMilkSRNMCC.ObjList.Count > 0) Then
                For Each obj1 As clsMilkSRNMCCDetail In clsMilkSRNMCC.ObjList
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = obj1.Item_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = obj1.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj1.Item_CODE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColQty).Value = obj1.MILK_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColACC_Qty).Value = obj1.ACC_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColUOM).Value = obj1.UOM
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColFatP).Value = obj1.FAT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COlCLR).Value = obj1.CLR
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COlSNFP).Value = obj1.SNF

                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATKg).Value = IIf(obj1.FAT_KG > 0, obj1.FAT_KG, Math.Round(clsCommon.myCdbl(obj1.FAT * obj1.ACC_Qty) / 100, 2))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COlSNFKg).Value = IIf(obj1.SNF_KG > 0, obj1.SNF_KG, Math.Round(clsCommon.myCdbl(obj1.SNF * obj1.ACC_Qty) / 100, 2))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COlRate).Value = obj1.RATE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = obj1.AMOUNT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColNetAmount).Value = obj1.NET_AMOUNT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundOff).Value = obj1.Round_Off
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColServiceTaxAmount).Value = obj1.Service_Charge_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSIONAmount).Value = obj1.Commission_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSIONAmount).Value = obj1.Emp_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCORRECTIONFACTOR).Value = obj1.Correction_Factor
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColPriceCode).Value = obj1.Price_Code
                Next
            Else
                gv1.Rows.AddNew()
            End If
            UcAttachment1.LoadData(obj.DOC_CODE)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Item Code"
        repoCode.Name = colItemCode
        repoCode.Width = 70
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Item Desc"
        repoEmpCode.Name = colItemDesc
        repoEmpCode.Width = 200
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSNNo
        repoHSNCode.Width = 150
        repoHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCode)

        Dim repoProjectCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "Qty"
        repoProjectCode.Name = ColQty
        repoProjectCode.Width = 70
        repoProjectCode.ReadOnly = True
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectCode)

        Dim repoProjectDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectDesc.FormatString = ""
        repoProjectDesc.HeaderText = "UOM"
        repoProjectDesc.Name = ColUOM
        repoProjectDesc.Width = 70
        repoProjectDesc.ReadOnly = True
        repoProjectDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectDesc)

        Dim repoACCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCCode.FormatString = ""
        repoACCCode.HeaderText = "Actual Qty(KG)"
        repoACCCode.Name = ColACC_Qty
        repoACCCode.Width = 100
        repoACCCode.ReadOnly = True
        repoACCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoACCCode)


        Dim repoCustCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "FAT(%)"
        repoCustCode.Name = ColFatP
        repoCustCode.Width = 70
        repoCustCode.ReadOnly = True
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoCustCode)

        Dim repoCustDesc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDesc.FormatString = ""
        repoCustDesc.HeaderText = "CLR"
        repoCustDesc.Name = COlCLR
        repoCustDesc.Width = 70
        repoCustDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDesc)

        repoCustDesc = New GridViewDecimalColumn()
        repoCustDesc.FormatString = ""
        repoCustDesc.HeaderText = "SNF(%)"
        repoCustDesc.Name = COlSNFP
        repoCustDesc.Width = 70
        repoCustDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDesc)

        


        Dim repoCorrection_factor As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCorrection_factor.FormatString = ""
        repoCorrection_factor.HeaderText = "Correction Factor"
        repoCorrection_factor.Name = ColCORRECTIONFACTOR
        repoCorrection_factor.Width = 0
        repoCorrection_factor.IsVisible = False
        repoCorrection_factor.ReadOnly = True
        repoCorrection_factor.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoCorrection_factor)

        Dim repoFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT(KG)"
        repoFATKG.Name = ColFATKg
        repoFATKG.Width = 70
        repoFATKG.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF(KG)"
        repoSNFKG.Name = COlSNFKg
        repoSNFKG.Width = 70
        repoSNFKG.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSNFKG)



        Dim repoUnitCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitCost.FormatString = ""
        repoUnitCost.HeaderText = "Rate"
        repoUnitCost.Name = COlRate
        repoUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitCost.ReadOnly = True
        repoUnitCost.WrapText = True
        repoUnitCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoUnitCost)

        Dim repoTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCost.FormatString = ""
        repoTotalCost.HeaderText = "Total Amount"
        repoTotalCost.Name = ColAmount
        repoTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCost.ReadOnly = True
        repoTotalCost.WrapText = True
        repoTotalCost.Width = 130
        gv1.MasterTemplate.Columns.Add(repoTotalCost)

        Dim repoService_Charge As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoService_Charge.FormatString = ""
        repoService_Charge.HeaderText = "Service Charge Type"
        repoService_Charge.Name = colService_Charge
        repoService_Charge.Width = 120
        repoService_Charge.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoService_Charge)

        Dim repoTotalBilling As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBilling.FormatString = ""
        repoTotalBilling.HeaderText = "Service Charge(%)"
        repoTotalBilling.Name = colCOMMISSION
        repoTotalBilling.IsVisible = True
        repoTotalBilling.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBilling.ReadOnly = True
        repoTotalBilling.WrapText = True
        repoTotalBilling.IsVisible = False
        repoTotalBilling.Width = 0
        gv1.MasterTemplate.Columns.Add(repoTotalBilling)

        Dim repoCommissionAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommissionAmount.FormatString = ""
        repoCommissionAmount.HeaderText = "Service Charge Amount"
        repoCommissionAmount.Name = colCOMMISSIONAmount
        repoCommissionAmount.IsVisible = True
        repoCommissionAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommissionAmount.ReadOnly = True
        repoCommissionAmount.WrapText = True
        repoCommissionAmount.Width = 0
        repoCommissionAmount.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCommissionAmount)

        repoCommissionAmount = New GridViewDecimalColumn()
        repoCommissionAmount.FormatString = ""
        repoCommissionAmount.HeaderText = "Service Charge Amount"
        repoCommissionAmount.Name = ColServiceTaxAmount
        repoCommissionAmount.IsVisible = True
        repoCommissionAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommissionAmount.ReadOnly = True
        repoCommissionAmount.WrapText = True
        repoCommissionAmount.Width = 0
        repoCommissionAmount.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCommissionAmount)

        Dim repoPaymentComm As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentComm.FormatString = ""
        repoPaymentComm.HeaderText = "EMP(%)"
        repoPaymentComm.Name = colPaymentCOMMISSION
        repoPaymentComm.IsVisible = True
        repoPaymentComm.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPaymentComm.ReadOnly = True
        repoPaymentComm.WrapText = True
        repoPaymentComm.Width = 0
        repoPaymentComm.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPaymentComm)

        Dim repoPaymentCommAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentCommAmount.FormatString = ""
        repoPaymentCommAmount.HeaderText = "EMP"
        repoPaymentCommAmount.Name = colPaymentCOMMISSIONAmount
        repoPaymentCommAmount.IsVisible = True
        repoPaymentCommAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPaymentCommAmount.ReadOnly = True
        repoPaymentCommAmount.WrapText = True
        repoPaymentCommAmount.Width = 100
        gv1.MasterTemplate.Columns.Add(repoPaymentCommAmount)

        Dim repoNetSaveTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetSaveTotalCost.FormatString = ""
        repoNetSaveTotalCost.HeaderText = "Net Amount"
        repoNetSaveTotalCost.Name = ColNetAmount
        repoNetSaveTotalCost.IsVisible = True
        repoNetSaveTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetSaveTotalCost.ReadOnly = True
        repoNetSaveTotalCost.WrapText = True
        repoNetSaveTotalCost.IsVisible = True
        repoNetSaveTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNetSaveTotalCost)

        repoNetSaveTotalCost = New GridViewDecimalColumn()
        repoNetSaveTotalCost.FormatString = ""
        repoNetSaveTotalCost.HeaderText = "Round Off"
        repoNetSaveTotalCost.Name = ColRoundOff
        repoNetSaveTotalCost.IsVisible = True
        repoNetSaveTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetSaveTotalCost.ReadOnly = True
        repoNetSaveTotalCost.WrapText = True
        repoNetSaveTotalCost.IsVisible = True
        repoNetSaveTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNetSaveTotalCost)

        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = ColPriceCode
        repoPriceCode.Width = 70
        repoPriceCode.ReadOnly = True
        repoPriceCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoPriceCode)


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
        ReStoreGridLayout()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, , NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim Squery As String = "select TSPL_MILK_SRN_Head.DOC_COde as [Code],convert(varchar,Doc_date,103) as [Date],milk_sample_code as [Milk Sample Code],vlc_Name as [VLC]," _
            & " Vendor_Name as [VSP], Route_Name as [Route],Vehical_Name as [Vehicle],DOC_DATE from tspl_Milk_srn_head left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=VSP_CODE " _
            & " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=tspl_Milk_srn_head .VLC_CODE Left outer join TSPL_Primary_Vehicle_Master on " _
            & " TSPL_Primary_Vehicle_Master.Vehicle_Code=tspl_Milk_srn_head.VEHICLE_CODE Left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=tspl_Milk_srn_head.Route_CODE"


            txtCode.Value = clsCommon.ShowSelectForm("MSRNFND", Squery, "Code", " Posted=1", txtCode.Value, "Code", isButtonClicked, "DOC_DATE")
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
            End If
            Squery = String.Empty
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveLayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "MilkSRNGrid"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        'End If
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("MilkSRNGrid", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkSRNGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        Dim strPoInvcNo As String = connectSql.RunScalar("select voucher_no from TSPL_JOURNAL_MASTER where Source_Doc_No='" + txtCode.Value + "'")
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strPoInvcNo)
    End Sub

    Private Sub ImportSRNUpdate(Optional ByVal UpdateOnlyGl As Boolean = False)
        Dim gv As New RadGridView()
        'Dim IsNewEntry As Boolean
        Dim counter As Integer = 0
        Dim totqty As Double = 0
        Dim totsnf As Double = 0
        Dim totfat As Double = 0
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "SRN No", "FAT", "SNF") Then
            Try
                Dim counter_Index As Integer = 0
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    LoadData(clsCommon.myCstr(grow.Cells("SRN No").Value), , NavigatorType.Current)
                    gv1.Rows(0).Cells(ColFatP).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells("FAT").Value) * 10) / 10
                    gv1.Rows(0).Cells(COlSNFP).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells("SNF").Value) * 10) / 10
                    If UpdateOnlyGl = False Then
                        'SaveDataSRN()
                    Else
                        clsMilkSRNMCC.updateJournalEntry("MI-SR", clsCommon.myCstr(grow.Cells("SRN No").Value), clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(ColNetAmount).Value))
                    End If

                    counter_Index += 1
                    clsCommon.ProgressBarUpdate("Total Receords Imported : " & counter_Index & " / " & gv.Rows.Count)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    'Function SaveDataSRN()
    '    If AllowToSave() = False Then
    '        Return False
    '        Exit Function
    '    End If
    '    For Each grow As GridViewRowInfo In gv1.Rows

    '        'If clsCommon.myCdbl(grow.Cells(COlRate).Value) <= 0 Then
    '        grow.Cells(ColFatP).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(ColFatP).Value) * 10) / 10
    '        grow.Cells(COlSNFP).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(COlSNFP).Value) * 10) / 10
    '        grow.Cells(COlRate).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(ColFatP).Value), clsCommon.myCdbl(grow.Cells(COlSNFP).Value), clsCommon.myCstr(fndMccCode.Text), clsCommon.myCstr(fndVlcCode.Text), IIf(cboShift.Text.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
    '        If clsCommon.myCdbl(grow.Cells(COlRate).Value) = 0 Then
    '            Return False
    '            Exit Function
    '        End If
    '        grow.Cells(ColAmount).Value = clsCommon.myCdbl(grow.Cells(COlRate).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value)
    '        gv1.CurrentRow.Cells(ColPriceCode).Value = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsCommon.myCdbl(gv1.CurrentRow.Cells(ColFatP).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(COlSNFP).Value), clsCommon.myCstr(fndMccCode.Text), clsCommon.myCstr(fndVlcCode.Text), IIf(cboShift.Text.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))

    '    Next


    '    Dim DtVehicle As DataTable = clsDBFuncationality.GetDataTable("SELECT vm.* FROM  TSPL_Primary_Vehicle_Master vm ")
    '    Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING ")
    '    Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail ")


    '    Dim Milk_receipt_code As String = clsDBFuncationality.getSingleValue("select milk_receipt_code from tspl_milk_sample_Head where doc_code='" & fndMccCode.Tag & "'")
    '    Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try

    '        '        If (AllowToSave()) Then
    '        ' Trans = clsDBFuncationality.GetTransactin()
    '        Dim counter As Integer = 0
    '        Dim Net_amt As Double = 0
    '        Dim objHead As clsMilkSRNMCC
    '        '' asign screen vaules in objHead

    '        Dim objList As New List(Of clsMilkSRNMCCDetail)
    '        Dim obj1 As clsMilkSRNMCCDetail

    '        Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
    '        Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail

    '        Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
    '        Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail

    '        For Each grow As GridViewRowInfo In gv1.Rows

    '            objList = New List(Of clsMilkSRNMCCDetail)
    '            objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
    '            objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)
    '            DtMilkReceipt = clsDBFuncationality.GetDataTable("select rd.*,rh.*,Case when Nature='C' then Actual_charges end as  commision_pers," _
    '            & " Case when Nature='E' then Actual_charges end as payment_commision_pers,Service_Charge_Type,coalesce(Rate_Head_Load,0) as Rate_Head_Load" _
    '            & " ,coalesce(Rate_Own_Asset,0) as Rate_Own_Asset,Service_Basis_Head_Load,Service_Basis_Own_Asset,TSPL_VENDOR_MASTER.EMP_Type " _
    '            & " ,TSPL_VENDOR_MASTER.EMP_Fixed_Amount " _
    '            & " ,TSPL_VENDOR_MASTER.Actual_charges_Slab " _
    '            & " ,TSPL_VENDOR_MASTER.Actual_charges " _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab2" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges2" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab3" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges3" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab4" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges4" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab5" _
    '            & ",TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,TSPL_VENDOR_MASTER.DistanceKM_Head_Load from TSPL_MILK_RECEIPT_HEAD rd Inner join TSPL_MILK_RECEIPT_DETAIL rh on rh.DOC_CODE=" _
    '            & " rd.DOC_CODE left join TSPL_VENDOR_MASTER on Vendor_Code=VSP_CODE where rd.DOC_CODE='" & Milk_receipt_code & "' and rh.vlc_DOC_Code='" & clsCommon.myCstr(fndVlcCode.Tag) & "'", Trans)
    '            Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & Milk_receipt_code & "' and vlc_DOC_Code='" & clsCommon.myCstr(fndVlcCode.Tag) & "'")
    '            'obj1.DOC_CODE = txtCode.Value

    '            objHead = New clsMilkSRNMCC
    '            objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
    '            objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
    '            objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
    '            objHead.MILK_SAMPLE_CODE = fndMccCode.Tag

    '            ' objHead.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
    '            objHead.MCC_CODE = IIf(clsCommon.myLen(clsCommon.myCstr(dr(0)("Irregular_MCC_CODE"))) > 0, clsCommon.myCstr(dr(0)("Irregular_MCC_CODE")), clsCommon.myCstr(dr(0)("MCC_CODE"))) 'clsCommon.myCstr(dr(0)("MCC_CODE"))
    '            objHead.SAMPLE_NO = clsCommon.myCdbl(txtsampleNo.Text)
    '            objHead.VLC_DOC_CODE = clsCommon.myCstr(fndVlcCode.Tag)
    '            objHead.VEHICLE_CODE = clsCommon.myCstr(dr(0)("VEHICLE_CODE"))
    '            objHead.VLC_CODE = clsCommon.myCstr(dr(0)("VLC_CODE"))
    '            objHead.ROUTE_CODE = clsCommon.myCstr(dr(0)("ROUTE_CODE"))
    '            objHead.VSP_CODE = clsCommon.myCstr(fndVSPCode.Text)
    '            If DtVehicle.Select("Vehicle_Code='" & dr(0)("VEHICLE_CODE") & "'").Length > 0 Then
    '                objHead.TransPorter = clsCommon.myCstr(DtVehicle.Select("Vehicle_Code='" & dr(0)("VEHICLE_CODE") & "'")(0)("Vendor_Code"))
    '            End If
    '            obj1 = New clsMilkSRNMCCDetail()
    '            obj1.Item_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
    '            obj1.MILK_Qty = clsCommon.myCdbl(grow.Cells(ColQty).Value)
    '            obj1.ACC_Qty = clsCommon.myCdbl(grow.Cells(ColACC_Qty).Value)
    '            obj1.FAT = clsCommon.myCdbl(grow.Cells(ColFatP).Value)
    '            obj1.SNF = clsCommon.myCdbl(grow.Cells(COlSNFP).Value)
    '            obj1.MCC_CODE = IIf(clsCommon.myLen(clsCommon.myCstr(dr(0)("Irregular_MCC_CODE"))) > 0, clsCommon.myCstr(dr(0)("Irregular_MCC_CODE")), clsCommon.myCstr(dr(0)("MCC_CODE")))
    '            obj1.Correction_Factor = 0.14
    '            obj1.RATE = clsCommon.myCdbl(grow.Cells(COlRate).Value)
    '            obj1.UOM = clsCommon.myCstr(grow.Cells(ColUOM).Value)
    '            obj1.Price_Code = clsCommon.myCstr(grow.Cells(ColPriceCode).Value)
    '            obj1.AMOUNT = clsCommon.myCdbl(grow.Cells(ColAmount).Value)
    '            obj1.Commission = clsCommon.myCdbl(dr(0)("commision_pers"))
    '            obj1.Head_Load_Rate = clsCommon.myCdbl(dr(0)("Rate_Head_Load"))
    '            obj1.Own_Asset_Rate = clsCommon.myCdbl(dr(0)("Rate_Own_Asset"))
    '            obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("payment_commision_pers"))
    '            If clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
    '                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
    '                If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
    '                    obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission / 100, 2)
    '                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
    '                    obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
    '                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
    '                    obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
    '                Else
    '                    obj1.Emp_Amount = 0
    '                    'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
    '                End If
    '                If clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
    '                    obj1.Emp_Amount += clsCommon.myCdbl(dr(0)("EMP_Fixed_Amount"))
    '                End If
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
    '                If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
    '                    If clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges5"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges4"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges3"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges2"))
    '                    Else
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
    '                    End If
    '                    obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission / 100, 2)
    '                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
    '                    If clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges5"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges4"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges3"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges2"))
    '                    Else
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
    '                    End If
    '                    obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
    '                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
    '                    If clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges5"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges4"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges3"))
    '                    ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) Then
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges2"))
    '                    Else
    '                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
    '                    End If
    '                    obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
    '                Else
    '                    obj1.Emp_Amount = 0
    '                    'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
    '                End If
    '                If clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
    '                    obj1.Emp_Amount += clsCommon.myCdbl(dr(0)("EMP_Fixed_Amount"))
    '                End If
    '            Else
    '                Throw New Exception("EMP Type is Not a valid ")
    '            End If
    '            obj1.Service_Charge_Type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
    '            '==================Head Load==========================
    '            Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, Trans))
    '            Dim dclDistanceKM As Decimal = clsCommon.myCdbl(DtMilkReceipt.Rows(0)("DistanceKM_Head_Load"))
    '            If dclDistanceKM = 0 Then
    '                dclDistanceKM = 1
    '            End If
    '            If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Head_Load")), "K") = CompairStringResult.Equal Then
    '                If obj1.ACC_Qty >= MinimumQtyForHeadLoad Then
    '                    obj1.Head_Load_Amount = Math.Round(obj1.ACC_Qty * obj1.Head_Load_Rate * dclDistanceKM, 2)
    '                End If
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Head_Load")), "L") = CompairStringResult.Equal Then
    '                If clsCommon.myCDecimal(dr(0)("ACC_WEIGHT_LTR")) >= MinimumQtyForHeadLoad Then
    '                    obj1.Head_Load_Amount = Math.Round(clsCommon.myCDecimal(dr(0)("ACC_WEIGHT_LTR")) * obj1.Head_Load_Rate * dclDistanceKM, 2)
    '                End If
    '            End If
    '            obj1.Head_Load_Type = clsCommon.myCstr(dr(0)("Service_Basis_Head_Load"))
    '            '============================================
    '            '==================Own Asset==========================
    '            If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
    '                obj1.Own_Asset_Amount = Math.Round(obj1.ACC_Qty * obj1.Own_Asset_Rate, 2)
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
    '                obj1.Own_Asset_Amount = Math.Round(obj1.MILK_Qty * obj1.Own_Asset_Rate, 2)
    '            End If
    '            obj1.Own_Asset_Type = clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset"))
    '            '============================================

    '            'If clsCommon.myCdbl(obj1.Commission) > 0 Then
    '            '    obj1.Commission_Amount = Math.Round(obj1.AMOUNT * obj1.Commission, 2)
    '            'End If
    '            'If clsCommon.myCdbl(obj1.Payment_Commission) > 0 Then
    '            '    obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission, 2)
    '            obj1.Service_Charge_Amount = Math.Round(obj1.MILK_Qty * clsCommon.myCdbl(dr(0)("Service_Charge_Per_Unit")), 2)
    '            obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT + obj1.Emp_Amount - obj1.Service_Charge_Amount, 2)
    '            'Else
    '            '    obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT, 2)
    '            'End If
    '            'obj1.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
    '            objList.Add(obj1)

    '            '============VSp Charge Detail=====================
    '            For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Select("Vsp_Code='" & objHead.VSP_CODE & "'")
    '                objVSP_Charge1 = New clsMilkSRNVSpChargeDetail()
    '                objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objHead.VSP_CODE)
    '                objVSP_Charge1.Vlc_Doc_Code = clsCommon.myCstr(fndVlcCode.Tag)
    '                objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
    '                objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
    '                objVSP_Charge1.Service_Type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
    '                If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
    '                    objVSP_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
    '                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
    '                    objVSP_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
    '                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dr(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
    '                    objVSP_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
    '                End If
    '                objVSPChargeList.Add(objVSP_Charge1)
    '            Next
    '            '===========================================


    '            '============Price Charge Detail=====================
    '            For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Select("Price_Code='" & obj1.Price_Code & "'")
    '                objPrice_Charge1 = New clsMilkSRNPriceChargeDetail()
    '                objPrice_Charge1.Price_Code = clsCommon.myCstr(obj1.Price_Code)
    '                objPrice_Charge1.Vlc_Doc_Code = clsCommon.myCstr(fndVlcCode.Tag)
    '                objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
    '                objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
    '                objPrice_Charge1.Service_type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
    '                If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
    '                    objPrice_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
    '                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
    '                    objPrice_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
    '                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dr(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
    '                    objPrice_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
    '                End If
    '                objPriceChargeList.Add(objPrice_Charge1)
    '            Next
    '            '===========================================

    '            If Not clsMilkSRNMCC.UpdateDataFromSRNFrom(objHead, objList, objVSPChargeList, objPriceChargeList, Trans) Then
    '                Trans.Rollback()
    '                Return False
    '            Else
    '                clsMilkSRNMCC.updateJournalEntry("MI-SR", objHead.DOC_CODE, obj1.NET_AMOUNT, Trans)
    '                clsMilkSRNMCC.UpdateSample(objHead.MILK_SAMPLE_CODE, objHead.SAMPLE_NO, objList(0).FAT, objList(0).SNF, objList(0).RATE, objList(0).AMOUNT, Trans, "", 0, 0)
    '                Trans.Commit()
    '                'clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
    '                'clsCommon.ProgressBarUpdate(counter & "/" & gv1.Rows.Count)
    '            End If
    '            objList = Nothing
    '            objVSPChargeList = Nothing
    '            objPriceChargeList = Nothing

    '            dr = Nothing
    '            'obj1.DOC_CODE = txtCode.Value

    '            objHead = Nothing
    '        Next
    '        Return True
    '        'If clsMilkSampleMCC.SaveData(objHead, objList, trans) Then
    '        ' Trans.Commit()
    '        'clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
    '        'End If
    '        '        End If
    '        DtMilkReceipt = Nothing
    '        DtVehicle = Nothing
    '        DtVSPChargeDetail = Nothing
    '        DtPriceChargeDetail = Nothing


    '        Milk_receipt_code = String.Empty

    '    Catch ex As Exception
    '        Trans.Rollback()
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '        Return False
    '    End Try
    'End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsMilkShiftEndMCC.RecreateConsumptionEntry(txtShiftEnd.arrValueMember, trans)
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Task completed successfully", Me.Text)
            txtShiftEnd.arrValueMember = Nothing
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtShiftEnd__My_Click(sender As Object, e As EventArgs) Handles txtShiftEnd._My_Click
        Dim qry As String
        qry = clsMilkShiftEndMCC.getShiftEndQuery(True)
        txtShiftEnd.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "shftendMulsr", qry, "DOC_CODE", "", txtShiftEnd.arrValueMember, Nothing)
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If objCommonVar.PricePlan = 4 Then
                Dim qry As String = "select DOC_CODE,Price_Code,Qty,FAT_PER,SNF_PER from TSPL_MILK_SRN_DETAIL where fat_Ratio is null"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                For Each dr As DataRow In dt.Rows
                    Dim dblFATRatio As Decimal = 0
                    Dim dtFATSNFUploader As DataTable = clsDBFuncationality.GetDataTable("select top 1 Price_Code,Planning_Code  from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsCommon.myCstr(dr("Price_Code")) + "'")
                    If dtFATSNFUploader IsNot Nothing AndAlso dtFATSNFUploader.Rows.Count > 0 Then
                        Dim Emp_Amount As Decimal = clsEkoPro.GetRateCalculated(clsCommon.myCstr(dtFATSNFUploader.Rows(0)("Planning_Code")), clsCommon.myCstr(dtFATSNFUploader.Rows(0)("Price_Code")), clsCommon.myCdbl(dr("Qty")), clsCommon.myCdbl(dr("FAT_PER")), clsCommon.myCdbl(dr("SNF_PER")), Nothing, 0, dblFATRatio)
                        qry = "Update TSPL_MILK_SRN_DETAIL set fat_Ratio=" + clsCommon.myCstr(dblFATRatio) + " where DOC_CODE='" + clsCommon.myCstr(dr("DOC_CODE")) + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(txtCode.Value), "DOC_CODE", "TSPL_MILK_SRN_HEAD", "TSPL_MILK_SRN_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        ShowJE(MyBase.Form_ID, txtCode.Value)
    End Sub

    'Private Function getShiftEndQuery() As String
    '    Return "select TSPL_MILK_Shift_End_HEAD.DOC_CODE,TSPL_MILK_Shift_End_HEAD.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_Shift_End_HEAD.DOC_DATE,TSPL_MILK_Shift_End_HEAD.MCC_DATE,TSPL_MILK_Shift_End_HEAD.SHIFT,  MSE_MCC_OUT.Adjustment_No as MCCOutAdjustmentNo,MSE_MCC_OUT_JV.Voucher_No as MCCOutAdjustmentVoucherNo,MSE_PLT_IN.Adjustment_No as PlantInAdjustmentNo,MSE_PLT_IN_JV.Voucher_No as PlantInAdjustmentVoucherNo,MSE_PLT_CONSUME.Adjustment_No as ConsumptionAdjustmentNo,MSE_PLT_CONSUME_JV.Voucher_No as ConsumptionAdjustmentVoucherNo" + Environment.NewLine + _
    '    " from TSPL_MILK_Shift_End_HEAD" + Environment.NewLine + _
    '    " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_Shift_End_HEAD.MCC_CODE " + Environment.NewLine + _
    '    " left outer join TSPL_ADJUSTMENT_HEADER as MSE_MCC_OUT on MSE_MCC_OUT.Document_No=TSPL_MILK_Shift_End_HEAD.DOC_CODE and MSE_MCC_OUT.Reference_Document='MSE-MCC-OUT'" + Environment.NewLine + _
    '    " left outer join TSPL_JOURNAL_MASTER as MSE_MCC_OUT_JV on MSE_MCC_OUT_JV.Source_Doc_No=MSE_MCC_OUT.Adjustment_No " + Environment.NewLine + _
    '    " left outer join TSPL_ADJUSTMENT_HEADER as MSE_PLT_IN on MSE_PLT_IN.Document_No=TSPL_MILK_Shift_End_HEAD.DOC_CODE and MSE_PLT_IN.Reference_Document='MSE-PLT-IN'" + Environment.NewLine + _
    '    " left outer join TSPL_JOURNAL_MASTER as MSE_PLT_IN_JV on MSE_PLT_IN_JV.Source_Doc_No=MSE_PLT_IN.Adjustment_No" + Environment.NewLine + _
    '    " left outer join TSPL_ADJUSTMENT_HEADER as MSE_PLT_CONSUME on MSE_PLT_CONSUME.Document_No=TSPL_MILK_Shift_End_HEAD.DOC_CODE and MSE_PLT_CONSUME.Reference_Document='MSE-PLT-CONSUME'" + Environment.NewLine + _
    '    " left outer join TSPL_JOURNAL_MASTER as MSE_PLT_CONSUME_JV on MSE_PLT_CONSUME_JV.Source_Doc_No=MSE_PLT_CONSUME.Adjustment_No" + Environment.NewLine + _
    '    " where len( isnull( MSE_MCC_OUT.Adjustment_No,''))>0 "
    'End Function
    ' Ticket No : TEC/29/10/18-000352 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub
End Class
