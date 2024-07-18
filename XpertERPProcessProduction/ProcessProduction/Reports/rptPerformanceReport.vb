Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptPerformanceReport
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'TxtMultiLocation.arrValueMember = Nothing
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = ""
        TxtRAL.arrValueMember = Nothing
        Gv1.DataSource = Nothing
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Performance_Report()
    End Sub

    Private Sub Load_Performance_Report()

        Dim qry As String = ""
        Dim dt As New DataTable()
        Try

            Dim whr As String = ""
            If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
                whr += " where XXY.Ref_No In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ")  "
            End If

            If rdobtnWeighment.Checked Then

                qry = "  SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, XXY.LOCATION, XXY.Ref_No,XXY.[Item] as [Item Description]  ,XXY.UOM,XXY.[Supplier's Name], XXY.[Quantity Approved],cast (XXY.Rate as decimal (18,2)) as Rate, cast (XXY.[Quantity Supplied] as decimal (18,2)) as [Quantity Supplied],cast (XXY.[Short/Excess Qty] as decimal (18,2)) as [Short/Excess Qty],XXY.RiskPurchase,cast (XXY.[% Supplied] as decimal (18,2)) as [% Supplied],XXY.Remarks ,XXY.LOCATION,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,FORMAT(XXY.FROM_DATE, 'dd/MM/yyyy')as FROM_DATE,FORMAT(XXY.TO_DATE, 'dd/MM/yyyy')as TO_DATE,TSPL_LOCATION_MASTER.Add4
                         from   (select final.HeadName, final.Ref_No,final.ITEM_DESC as [Item],final.UOM,final.Vendor_Name as [Supplier's Name],final.RAL_QTY as [Quantity Approved],final.Rate,final.GRNQTY as [Quantity Supplied],final.Pending_Qty as [Short/Excess Qty],final.RiskPurchase,final.[% Supplied],final.Remarks ,final.LOCATION,final.FROM_DATE,final.TO_DATE
                from (
                Select '' as HeadName, TSPL_GRN_HEAD.Ref_No ,TSPL_ITEM_MASTER.Short_Description As 'ITEM_DESC',TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_GRN_HEAD.Vendor_Name,
                cast(RM_RAL.RAL_QTY as numeric (18,0)) as 'RAL_QTY',
                max(TendorSeqNo) as TendorSeqNo,
                SUM(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) AS GRNQTY,
                (RM_RAL.RAL_QTY - sum(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) )as 'Pending_Qty', '' as RiskPurchase,
				(SUM(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight)*100)/ RM_RAL.RAL_QTY as [% Supplied],'' as Remarks,MAX(RM_RAL.Rate) AS RATE
                 ,MAX(RM_RAL.Location) AS Location, MAX(RM_RAL.FROM_DATE) AS FROM_DATE,MAX(RM_RAL.TO_DATE) AS TO_DATE 
                             

                from TSPL_PO_WEIGHTMENT_HEAD
                left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
                
                INNER JOIN 
                (SELECT TSPL_TENDER_DETAIL.Location AS 'LOCATION' ,TSPL_TENDER_HEADER.DocumentCode AS 'RAL',TSPL_TENDER_DETAIL.Vendor_Code AS 'VENDORCODE',TSPL_VENDOR_MASTER.Vendor_Name AS 'VENDORNAME',TSPL_TENDER_DETAIL.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_NAME',TSPL_TENDER_DETAIL.Unit_code  AS 'UOM',max(TSPL_TENDER_HEADER.TendorSeqNo) as TendorSeqNo,SUM(TSPL_TENDER_DETAIL.Qty) AS 'RAL_QTY', 0 AS 'GRNQTY',MAX(TSPL_TENDER_DETAIL.Rate) AS RATE,MAX(TSPL_TENDER_SCHEDULE.FROM_DATE) AS FROM_DATE,MAX(TSPL_TENDER_SCHEDULE.TO_DATE) AS TO_DATE
                FROM TSPL_TENDER_HEADER
                LEFT OUTER JOIN TSPL_TENDER_DETAIL ON TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.ITEM_CODE
                INNER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code
				left outer join (SELECT DocumentCode,Location_Code,Vendor_Code,Item_Code,MIN(FROM_DATE) AS FROM_DATE,MAX(TO_DATE) AS TO_DATE  FROM TSPL_TENDER_SCHEDULE  GROUP BY DocumentCode,Location_Code,Vendor_Code,Item_Code) as TSPL_TENDER_SCHEDULE ON TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode AND TSPL_TENDER_SCHEDULE.Location_Code=TSPL_TENDER_DETAIL.Location AND TSPL_TENDER_SCHEDULE.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code AND TSPL_TENDER_SCHEDULE.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                GROUP BY TSPL_TENDER_DETAIL.Location ,TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_TENDER_DETAIL.Unit_code) 
	            RM_RAL ON RM_RAL.RAL=TSPL_GRN_HEAD.Ref_No AND RM_RAL.LOCATION=TSPL_PO_WEIGHTMENT_HEAD.Location_Code AND RM_RAL.ITEM_CODE=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code AND RM_RAL.VENDORCODE=TSPL_GRN_HEAD.Vendor_Code  AND RM_RAL.UOM=TSPL_PO_WEIGHTMENT_DETAIL.UOM
                where TSPL_ITEM_MASTER.RAL=1 And TSPL_PO_WEIGHTMENT_HEAD.Location_Code= '" + clsCommon.myCstr(txtBillToLocation.Value) + "'   
                and TSPL_GRN_HEAD.IsCancel=0 and TSPL_GRN_HEAD.Status=1 and (VisualQCStatus <>2 or VisualQCStatusSecond<>2) 
                GROUP BY TSPL_PO_WEIGHTMENT_HEAD.Location_Code ,TSPL_GRN_HEAD.Ref_No,TSPL_PO_WEIGHTMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_PO_WEIGHTMENT_DETAIL.UOM ,TSPL_GRN_HEAD.Vendor_Code ,TSPL_GRN_HEAD.Vendor_Name ,TSPL_PO_WEIGHTMENT_DETAIL.UOM,RAL_QTY
                        ) final ) XXY
						LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=XXY.LOCATION  " + whr


            Else


                qry = "  SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, XXY.LOCATION, XXY.Ref_No,XXY.[Item] as [Item Description] ,XXY.UOM,XXY.[Supplier's Name], XXY.[Quantity Approved],cast (XXY.Rate as decimal (18,2)) as Rate, cast (XXY.[Quantity Supplied] as decimal (18,2)) as [Quantity Supplied],cast (XXY.[Short/Excess Qty] as decimal (18,2)) as [Short/Excess Qty],XXY.RiskPurchase,cast (XXY.[% Supplied] as decimal (18,2)) as [% Supplied],XXY.Remarks ,XXY.LOCATION,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,FORMAT(XXY.FROM_DATE, 'dd/MM/yyyy')as FROM_DATE,FORMAT(XXY.TO_DATE, 'dd/MM/yyyy')as TO_DATE,TSPL_LOCATION_MASTER.Add4
                         from   (select final.HeadName, final.Ref_No,final.ITEM_DESC as [Item],final.UOM,final.Vendor_Name as [Supplier's Name],final.RAL_QTY as [Quantity Approved],final.SRNQTY as [Quantity Supplied],final.Pending_Qty as [Short/Excess Qty],final.Rate,final.RiskPurchase,final.[% Supplied],final.Remarks,final.FROM_DATE,final.TO_DATE,final.LOCATION
                from (
                Select  '' as HeadName, TSPL_GRN_HEAD.Ref_No ,TSPL_ITEM_MASTER.Short_Description As 'ITEM_DESC',TSPL_SRN_DETAIL.Unit_code AS UOM,TSPL_GRN_HEAD.Vendor_Name,
                cast(RM_RAL.RAL_QTY as numeric (18,0)) as 'RAL_QTY',
                max(TendorSeqNo) as TendorSeqNo,
                SUM(SRN_Qty) AS SRNQTY,
                (RM_RAL.RAL_QTY - sum(TSPL_SRN_DETAIL.SRN_Qty )) as 'Pending_Qty',MAX(RM_RAL.Rate) AS RATE,'' as RiskPurchase,
				(sum(SRN_Qty)*100)/ RM_RAL.RAL_QTY as [% Supplied],'' as Remarks
                 , MAX(RM_RAL.FROM_DATE) AS FROM_DATE,MAX(RM_RAL.TO_DATE) AS TO_DATE,MAX(RM_RAL.Location) AS Location
                from TSPL_SRN_HEAD
				LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SRN_DETAIL.Item_Code
             
                INNER JOIN 
                (SELECT TSPL_TENDER_DETAIL.Location AS 'LOCATION' ,TSPL_TENDER_HEADER.DocumentCode AS 'RAL',TSPL_TENDER_DETAIL.Vendor_Code AS 'VENDORCODE',TSPL_VENDOR_MASTER.Vendor_Name AS 'VENDORNAME',TSPL_TENDER_DETAIL.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_NAME',TSPL_TENDER_DETAIL.Unit_code  AS 'UOM',max(TSPL_TENDER_HEADER.TendorSeqNo) as TendorSeqNo,SUM(TSPL_TENDER_DETAIL.Qty) AS 'RAL_QTY', 0 AS 'GRNQTY',MAX(TSPL_TENDER_DETAIL.Rate) AS RATE,MAX(TSPL_TENDER_SCHEDULE.FROM_DATE) AS FROM_DATE,MAX(TSPL_TENDER_SCHEDULE.TO_DATE) AS TO_DATE
                FROM TSPL_TENDER_HEADER
                LEFT OUTER JOIN TSPL_TENDER_DETAIL ON TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.ITEM_CODE
                INNER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code
				left outer join (SELECT DocumentCode,Location_Code,Vendor_Code,Item_Code,MIN(FROM_DATE) AS FROM_DATE,MAX(TO_DATE) AS TO_DATE  FROM TSPL_TENDER_SCHEDULE  GROUP BY DocumentCode,Location_Code,Vendor_Code,Item_Code) as TSPL_TENDER_SCHEDULE ON TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode AND TSPL_TENDER_SCHEDULE.Location_Code=TSPL_TENDER_DETAIL.Location AND TSPL_TENDER_SCHEDULE.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code AND TSPL_TENDER_SCHEDULE.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                GROUP BY TSPL_TENDER_DETAIL.Location ,TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_TENDER_DETAIL.Unit_code) 
	                RM_RAL ON RM_RAL.RAL=TSPL_GRN_HEAD.Ref_No AND RM_RAL.LOCATION=TSPL_SRN_HEAD.Bill_To_Location AND RM_RAL.ITEM_CODE=TSPL_SRN_DETAIL.Item_Code AND RM_RAL.VENDORCODE=TSPL_GRN_HEAD.Vendor_Code  AND RM_RAL.UOM=TSPL_SRN_DETAIL.Unit_code
                where  TSPL_SRN_HEAD.Bill_To_Location= '" + clsCommon.myCstr(txtBillToLocation.Value) + "' 
				and TSPL_GRN_HEAD.IsCancel=0 and TSPL_GRN_HEAD.Status=1  and (VisualQCStatus <>2 or VisualQCStatusSecond<>2)   
				GROUP BY TSPL_SRN_HEAD.Bill_To_Location ,TSPL_GRN_HEAD.Ref_No,TSPL_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_SRN_DETAIL.Unit_code ,TSPL_GRN_HEAD.Vendor_Code ,TSPL_GRN_HEAD.Vendor_Name ,TSPL_SRN_DETAIL.Unit_code,RAL_QTY
                        ) final) XXY 
						LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=XXY.LOCATION   " + whr



            End If

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Performance Report")
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("FROM_DATE").Width = 100
        Gv1.Columns("FROM_DATE").IsVisible = True
        Gv1.Columns("FROM_DATE").HeaderText = "From Date"

        Gv1.Columns("TO_DATE").Width = 100
        Gv1.Columns("TO_DATE").IsVisible = True
        Gv1.Columns("TO_DATE").HeaderText = "To Date"

        Gv1.Columns("Ref_No").Width = 150
        Gv1.Columns("Ref_No").IsVisible = True
        Gv1.Columns("Ref_No").HeaderText = "RAL No"

        Gv1.Columns("Item Description").Width = 100
        Gv1.Columns("Item Description").IsVisible = True
        Gv1.Columns("Item Description").HeaderText = "Item Description"

        Gv1.Columns("Supplier's Name").Width = 150
        Gv1.Columns("Supplier's Name").IsVisible = True
        Gv1.Columns("Supplier's Name").HeaderText = "Supplier's Name"

        Gv1.Columns("Quantity Approved").Width = 100
        Gv1.Columns("Quantity Approved").IsVisible = True
        Gv1.Columns("Quantity Approved").HeaderText = "Quantity Approved"

        Gv1.Columns("Rate").Width = 100
        Gv1.Columns("Rate").IsVisible = True
        Gv1.Columns("Rate").HeaderText = "Rate"

        Gv1.Columns("Quantity Supplied").Width = 100
        Gv1.Columns("Quantity Supplied").IsVisible = True
        Gv1.Columns("Quantity Supplied").HeaderText = "Quantity Supplied"

        Gv1.Columns("Short/Excess Qty").Width = 100
        Gv1.Columns("Short/Excess Qty").IsVisible = True
        Gv1.Columns("Short/Excess Qty").HeaderText = "Short/Excess Qty"

        Gv1.Columns("RiskPurchase").Width = 100
        Gv1.Columns("RiskPurchase").IsVisible = True
        Gv1.Columns("RiskPurchase").HeaderText = "RiskPurchase"

        Gv1.Columns("% Supplied").Width = 100
        Gv1.Columns("% Supplied").IsVisible = True
        Gv1.Columns("% Supplied").HeaderText = "% Supplied"

        Gv1.Columns("Remarks").Width = 100
        Gv1.Columns("Remarks").IsVisible = True
        Gv1.Columns("Remarks").HeaderText = "Remarks"

        Gv1.Columns("Add1").Width = 100
        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Add1").HeaderText = "Add1"

        Gv1.Columns("Location_Desc").Width = 100
        Gv1.Columns("Location_Desc").IsVisible = False
        Gv1.Columns("Location_Desc").HeaderText = "Location Description"

        Gv1.Columns("LOCATION").Width = 100
        Gv1.Columns("LOCATION").IsVisible = True
        Gv1.Columns("LOCATION").HeaderText = "LOCATION"


    End Sub
    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))

    End Sub

    Private Sub rptPerformanceReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click

        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                arrHeader.Add("Location:" + clsCommon.myCstr(txtBillToLocation.Value))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Performance Report", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click

        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If

                arrHeader.Add("Location:" + clsCommon.myCstr(lblBillToLocation.Text))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Performance Report", Gv1, arrHeader, "Performance Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim qry As String
        Try

            Dim whr As String = ""
            If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
                whr += " where XXY.Ref_No In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ")  "
            End If

            If rdobtnWeighment.Checked Then

                qry = "  SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, FORMAT(XXY.FROM_DATE, 'dd/MM/yyyy')as FROM_DATE,FORMAT(XXY.TO_DATE, 'dd/MM/yyyy')as TO_DATE, XXY.Ref_No,XXY.[Item] ,XXY.UOM,XXY.[Supplier's Name], XXY.[Quantity Approved],XXY.Rate, XXY.[Quantity Supplied],XXY.[Short/Excess Qty],XXY.RiskPurchase,XXY.[% Supplied],XXY.Remarks ,XXY.LOCATION,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4
                         from   (select final.HeadName, final.Ref_No,final.ITEM_DESC as [Item],final.UOM,final.Vendor_Name as [Supplier's Name],final.RAL_QTY as [Quantity Approved],final.Rate,final.GRNQTY as [Quantity Supplied],final.Pending_Qty as [Short/Excess Qty],final.RiskPurchase,final.[% Supplied],final.Remarks ,final.LOCATION,final.FROM_DATE,final.TO_DATE
                from (
                Select  '' as HeadName, TSPL_GRN_HEAD.Ref_No ,TSPL_ITEM_MASTER.Short_Description As 'ITEM_DESC',TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_GRN_HEAD.Vendor_Name,
                cast(RM_RAL.RAL_QTY as numeric (18,0)) as 'RAL_QTY',
                max(TendorSeqNo) as TendorSeqNo,
                SUM(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) AS GRNQTY,
                (RM_RAL.RAL_QTY - sum(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) )as 'Pending_Qty', '' as RiskPurchase,
				(SUM(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight)*100)/ RM_RAL.RAL_QTY as [% Supplied],'' as Remarks,MAX(RM_RAL.Rate) AS RATE
                 ,MAX(RM_RAL.Location) AS Location, MAX(RM_RAL.FROM_DATE) AS FROM_DATE,MAX(RM_RAL.TO_DATE) AS TO_DATE 
                             

                from TSPL_PO_WEIGHTMENT_HEAD
                left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
                
                INNER JOIN 
                (SELECT TSPL_TENDER_DETAIL.Location AS 'LOCATION' ,TSPL_TENDER_HEADER.DocumentCode AS 'RAL',TSPL_TENDER_DETAIL.Vendor_Code AS 'VENDORCODE',TSPL_VENDOR_MASTER.Vendor_Name AS 'VENDORNAME',TSPL_TENDER_DETAIL.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_NAME',TSPL_TENDER_DETAIL.Unit_code  AS 'UOM',max(TSPL_TENDER_HEADER.TendorSeqNo) as TendorSeqNo,SUM(TSPL_TENDER_DETAIL.Qty) AS 'RAL_QTY', 0 AS 'GRNQTY',MAX(TSPL_TENDER_DETAIL.Rate) AS RATE,MAX(TSPL_TENDER_SCHEDULE.FROM_DATE) AS FROM_DATE,MAX(TSPL_TENDER_SCHEDULE.TO_DATE) AS TO_DATE
                FROM TSPL_TENDER_HEADER
                LEFT OUTER JOIN TSPL_TENDER_DETAIL ON TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.ITEM_CODE
                INNER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code
				left outer join (SELECT DocumentCode,Location_Code,Vendor_Code,Item_Code,MIN(FROM_DATE) AS FROM_DATE,MAX(TO_DATE) AS TO_DATE  FROM TSPL_TENDER_SCHEDULE  GROUP BY DocumentCode,Location_Code,Vendor_Code,Item_Code) as TSPL_TENDER_SCHEDULE ON TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode AND TSPL_TENDER_SCHEDULE.Location_Code=TSPL_TENDER_DETAIL.Location AND TSPL_TENDER_SCHEDULE.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code AND TSPL_TENDER_SCHEDULE.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                GROUP BY TSPL_TENDER_DETAIL.Location ,TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_TENDER_DETAIL.Unit_code) 
	                RM_RAL ON RM_RAL.RAL=TSPL_GRN_HEAD.Ref_No AND RM_RAL.LOCATION=TSPL_PO_WEIGHTMENT_HEAD.Location_Code AND RM_RAL.ITEM_CODE=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code AND RM_RAL.VENDORCODE=TSPL_GRN_HEAD.Vendor_Code  AND RM_RAL.UOM=TSPL_PO_WEIGHTMENT_DETAIL.UOM
                where TSPL_ITEM_MASTER.RAL=1 And TSPL_PO_WEIGHTMENT_HEAD.Location_Code= '" + clsCommon.myCstr(txtBillToLocation.Value) + "'   
                and TSPL_GRN_HEAD.IsCancel=0 and TSPL_GRN_HEAD.Status=1 and (VisualQCStatus <>2 or VisualQCStatusSecond<>2) 
                GROUP BY TSPL_PO_WEIGHTMENT_HEAD.Location_Code ,TSPL_GRN_HEAD.Ref_No,TSPL_PO_WEIGHTMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_PO_WEIGHTMENT_DETAIL.UOM ,TSPL_GRN_HEAD.Vendor_Code ,TSPL_GRN_HEAD.Vendor_Name ,TSPL_PO_WEIGHTMENT_DETAIL.UOM,RAL_QTY
                        ) final ) XXY
						LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=XXY.LOCATION  " + whr

            Else

                qry = "  SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, FORMAT(XXY.FROM_DATE, 'dd/MM/yyyy')as FROM_DATE,FORMAT(XXY.TO_DATE, 'dd/MM/yyyy')as TO_DATE, XXY.Ref_No,XXY.[Item] ,XXY.UOM,XXY.[Supplier's Name], XXY.[Quantity Approved],XXY.Rate, XXY.[Quantity Supplied],XXY.[Short/Excess Qty],XXY.RiskPurchase,XXY.[% Supplied],XXY.Remarks ,XXY.LOCATION,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4
                         from   (select final.HeadName, final.Ref_No,final.ITEM_DESC as [Item],final.UOM,final.Vendor_Name as [Supplier's Name],final.RAL_QTY as [Quantity Approved],final.SRNQTY as [Quantity Supplied],final.Pending_Qty as [Short/Excess Qty],final.Rate,final.RiskPurchase,final.[% Supplied],final.Remarks,final.FROM_DATE,final.TO_DATE,final.LOCATION
                from (
                Select '' as HeadName, TSPL_GRN_HEAD.Ref_No ,TSPL_ITEM_MASTER.Short_Description As 'ITEM_DESC',TSPL_SRN_DETAIL.Unit_code AS UOM,TSPL_GRN_HEAD.Vendor_Name,
                cast(RM_RAL.RAL_QTY as numeric (18,0)) as 'RAL_QTY',
                max(TendorSeqNo) as TendorSeqNo,
                SUM(SRN_Qty) AS SRNQTY,
                (RM_RAL.RAL_QTY - sum(TSPL_SRN_DETAIL.SRN_Qty )) as 'Pending_Qty',MAX(RM_RAL.Rate) AS RATE,'' as RiskPurchase,
				(sum(SRN_Qty)*100)/ RM_RAL.RAL_QTY as [% Supplied],'' as Remarks
                 , MAX(RM_RAL.FROM_DATE) AS FROM_DATE,MAX(RM_RAL.TO_DATE) AS TO_DATE,MAX(RM_RAL.Location) AS Location
                from TSPL_SRN_HEAD
				LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SRN_DETAIL.Item_Code
             
                INNER JOIN 
                (SELECT TSPL_TENDER_DETAIL.Location AS 'LOCATION' ,TSPL_TENDER_HEADER.DocumentCode AS 'RAL',TSPL_TENDER_DETAIL.Vendor_Code AS 'VENDORCODE',TSPL_VENDOR_MASTER.Vendor_Name AS 'VENDORNAME',TSPL_TENDER_DETAIL.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_NAME',TSPL_TENDER_DETAIL.Unit_code  AS 'UOM',max(TSPL_TENDER_HEADER.TendorSeqNo) as TendorSeqNo,SUM(TSPL_TENDER_DETAIL.Qty) AS 'RAL_QTY', 0 AS 'GRNQTY',MAX(TSPL_TENDER_DETAIL.Rate) AS RATE,MAX(TSPL_TENDER_SCHEDULE.FROM_DATE) AS FROM_DATE,MAX(TSPL_TENDER_SCHEDULE.TO_DATE) AS TO_DATE
                FROM TSPL_TENDER_HEADER
                LEFT OUTER JOIN TSPL_TENDER_DETAIL ON TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.ITEM_CODE
                INNER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code
				left outer join (SELECT DocumentCode,Location_Code,Vendor_Code,Item_Code,MIN(FROM_DATE) AS FROM_DATE,MAX(TO_DATE) AS TO_DATE  FROM TSPL_TENDER_SCHEDULE  GROUP BY DocumentCode,Location_Code,Vendor_Code,Item_Code) as TSPL_TENDER_SCHEDULE ON TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode AND TSPL_TENDER_SCHEDULE.Location_Code=TSPL_TENDER_DETAIL.Location AND TSPL_TENDER_SCHEDULE.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code AND TSPL_TENDER_SCHEDULE.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                GROUP BY TSPL_TENDER_DETAIL.Location ,TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_TENDER_DETAIL.Unit_code) 
	                RM_RAL ON RM_RAL.RAL=TSPL_GRN_HEAD.Ref_No AND RM_RAL.LOCATION=TSPL_SRN_HEAD.Bill_To_Location AND RM_RAL.ITEM_CODE=TSPL_SRN_DETAIL.Item_Code AND RM_RAL.VENDORCODE=TSPL_GRN_HEAD.Vendor_Code  AND RM_RAL.UOM=TSPL_SRN_DETAIL.Unit_code
                where  TSPL_SRN_HEAD.Bill_To_Location= '" + clsCommon.myCstr(txtBillToLocation.Value) + "'  
                and TSPL_GRN_HEAD.IsCancel=0 and TSPL_GRN_HEAD.Status=1  and (VisualQCStatus <>2 or VisualQCStatusSecond<>2)
				GROUP BY TSPL_SRN_HEAD.Bill_To_Location ,TSPL_GRN_HEAD.Ref_No,TSPL_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_SRN_DETAIL.Unit_code ,TSPL_GRN_HEAD.Vendor_Code ,TSPL_GRN_HEAD.Vendor_Name ,TSPL_SRN_DETAIL.Unit_code,RAL_QTY
                        ) final) XXY 
						LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=XXY.LOCATION   " + whr


            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "rptPerformanceReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Dim qry As String = " SELECT DocumentCode,DocumentDate from TSPL_TENDER_HEADER order by DocumentCode "
        TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("ralMulSel", qry, "DocumentCode", "DocumentDate", TxtRAL.arrValueMember, TxtRAL.arrDispalyMember)
    End Sub
End Class