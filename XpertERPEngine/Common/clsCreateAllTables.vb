'=============BM00000007908================
Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports Telerik.WinControls.UI
Imports System.Drawing
Imports System

''check In Richa 22/06/2020



Public Class clsCreateAllTables
    Public Shared IsShowMenuOnRightClick As Boolean = False

    Public Shared Sub CreateAllTable()
        Try
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(OBJECT_ID) AS TotalTables FROM sys.tables"))
            If chk = 0 Then
                Dim strScript As String = XpertERPBlankTableScript.MainClass.GetQry()
                clsDBFuncationality.ExecuteNonQuery(strScript)
                clsCommonFunctionality.TableTotal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(1) from INFORMATION_SCHEMA.TABLES  where TABLE_TYPE='BASE TABLE'"))
            End If

            ExecuteBeforeCreateTable()
            XpertERPEngineFine.clsCreateAllTable.CreateAllTable()

            clsSyncHeadTables.AutoUpdateSyncTables(Nothing)
            clsSyncHeadTables.AutoUpdateBioSyncTables(Nothing)
            ExecuteAfterCreateTable()

            '' adding Standard Methods List 
            clsStandardMethods.AddStandardFunction()
            clsProgramIdFormNameMapping.setAllProgramFormName()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Shared Sub ExecuteBeforeCreateTable()
        Try
            Dim strForegintKey As String = clsERPFuncationality.GetConstraintWorking("TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF", "PI_No")
            If clsCommon.myLen(strForegintKey) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("alter table TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF drop constraint " + strForegintKey)
            End If

        Catch ex As Exception

        End Try


    End Sub
    Shared Sub ExecuteAfterCreateTable()
        Try
            clsCommon.ProgressBarUpdate("Running Alter Table scripts")
            Dim qry As String


            Try
                Dim intVal As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LinkFinancialYearStyleWithGSTDate, clsFixedParameterType.LinkFinancialYearStyleWithGSTDate, Nothing))
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_DOCPREFIX_MASTER set Short_Fiscal_Year='" + clsCommon.myCstr(intVal) + "' where Short_Fiscal_Year is null")


                qry = "select top 1 VendorCode from TSPL_VENDOR_PRICE_CHART_MAPPING  where isnull( Posted,0)=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    qry = "update TSPL_VENDOR_PRICE_CHART_MAPPING set Posted=1, Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If

                Dim strKey As String = clsERPFuncationality.GetConstraintWorking("TSPL_Dairy_Proforma_Invoice_HEAD", "Booking_No")
                If clsCommon.myLen(strKey) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("alter table TSPL_Dairy_Proforma_Invoice_HEAD drop constraint " + strKey)
                End If
            Catch ex As Exception

            End Try


            Try
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PURCHASE_ORDER_detail alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PURCHASE_ORDER_DETAIL_HIST_DATA alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PURCHASE_ORDER_DETAIL_Cancel_Data alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_GRN_DETAIL alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_GRN_DETAIL_HIST_DATA alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_GRN_DETAIL_Cancel_Data alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_MRN_DETAIL alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_MRN_DETAIL_HIST_DATA alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_MRN_DETAIL_Cancel_Data alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("DROP INDEX [For_NC_PI_Index1] ON [dbo].[TSPL_SRN_DETAIL]")
                clsCreateAllTables.ExecuteQeuryWithCatch("DROP INDEX [For_NC_PI_Index3] ON [dbo].[TSPL_SRN_DETAIL]")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_SRN_DETAIL alter column Item_Cost decimal(18,10) ")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_SRN_DETAIL_HIST_DATA alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_SRN_DETAIL_history alter column Item_Cost decimal(18,10) ")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_SRN_DETAIL_Cancel_Data alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PI_DETAIL alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PI_DETAIL_HIST_DATA alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PI_DETAIL_Cancel_Data alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PR_DETAIL alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PR_DETAIL_HIST_DATA alter column Item_Cost decimal(18,10)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_PR_DETAIL_Cancel_Data alter column Item_Cost decimal(18,10)")

                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER DROP CONSTRAINT PK__TSPL_ITE__813413F65EC63012")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Item_Basic_Net decimal(18,6) not null")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Item_Basic_Price decimal(18,6) not null")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Item_Selling_Price decimal(18,6) not null")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount1 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount2 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount3 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount4 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount5 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount6 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount7 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount8 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount9 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Price_Amount10 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Item_Rate decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Liquid_Rate decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Stock_Rate decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Abatement_Rate decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Markup_Percent decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Landing_Cost decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_MASTER alter column Purchase_Cost decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax1_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax2_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax3_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax4_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax5_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax6_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax7_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax8_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax9_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ALTER COLUMN tax10_amt decimal(18,6)")
                'clsCreateAllTables.'--alter table TSPL_ITEM_PRICE_MASTER alter column Item_Baisc_Price decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_MASTER ADD CONSTRAINT PK__TSPL_ITE__813413F65EC63012 PRIMARY KEY (Item_Code, UOM, Start_Date, Price_Code, Item_Basic_Net, Item_Basic_Price, Location_Code)")
                '-- Alter Price plan tax is 2 to 6 decimal point
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax1_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax2_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax3_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax4_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax5_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax6_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax7_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax8_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax9_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN tax10_amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("  ALTER TABLE TSPL_ITEM_PRICE_PLAN_DETAIL ALTER COLUMN Total_Tax_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column item_selling_price decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch(" alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount1 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount2 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount3 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount4 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount5 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount6 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount7 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount8 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount9 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Amount10 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate1 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate2 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate3 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate4 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate5 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate6 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate7 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate8 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate9 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Price_Rate10 decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column Item_Basic_Price decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX1_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX2_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX3_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX4_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX5_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX6_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX7_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX8_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX9_Base_Amt decimal(18,6)")
                clsCreateAllTables.ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX10_Base_Amt decimal(18,6)")

                '' adding Standard Methods List 
                clsStandardMethods.AddStandardFunction()
                clsProgramIdFormNameMapping.setAllProgramFormName()

                Dim strKey As String = clsERPFuncationality.GetConstraintWorking("TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL", "Reject_Type")
                If clsCommon.myLen(strKey) > 0 Then
                    qry = "alter table TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL drop " + strKey
                    clsCreateAllTables.ExecuteQeuryWithCatch(qry)
                End If



                strKey = clsERPFuncationality.GetConstraintWorking("TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL_SYNC", "Reject_Type")
                If clsCommon.myLen(strKey) > 0 Then
                    qry = "alter table TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL_SYNC drop " + strKey
                    clsCreateAllTables.ExecuteQeuryWithCatch(qry)
                End If

                strKey = clsERPFuncationality.GetDefaultConstraintWorking("TSPL_MILK_SAMPLE_DETAIL", "Dock_Collection_Milk_Type")
                If clsCommon.myLen(strKey) > 0 Then
                    qry = "alter table TSPL_MILK_SAMPLE_DETAIL drop " + strKey
                    ExecuteQeuryWithCatch(qry)
                End If
                qry = "alter table TSPL_MILK_SAMPLE_DETAIL drop column Dock_Collection_Milk_Type"
                ExecuteQeuryWithCatch(qry)


                strKey = clsERPFuncationality.GetDefaultConstraintWorking("TSPL_MILK_SAMPLE_DETAIL_SYNC", "Dock_Collection_Milk_Type")
                If clsCommon.myLen(strKey) > 0 Then
                    qry = "alter table TSPL_MILK_SAMPLE_DETAIL_SYNC drop " + strKey
                    ExecuteQeuryWithCatch(qry)
                End If
                qry = "alter table TSPL_MILK_SAMPLE_DETAIL_SYNC drop column Dock_Collection_Milk_Type"
                ExecuteQeuryWithCatch(qry)


                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    qry = "Select NUMERIC_SCALE  from INFORMATION_SCHEMA.columns where table_name='tspl_inventory_movement_new' and column_name='stock_qty'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran)) = 2 Then
                        qry = "drop index  [TSPL_INVENTORY_MOVEMENT_NEW].[TSPL_INVENTORY_MOVEMENT_NEW_Location_Code_Item_Code_Punching_Date]"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "drop index  [TSPL_INVENTORY_MOVEMENT_NEW].[TSPL_INVENTORY_MOVEMENT_NEW_Location_Code_Item_Code_main_location_Stock_Qty_NEW]"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "drop index  [TSPL_INVENTORY_MOVEMENT_NEW].[TSPL_INVENTORY_MOVEMENT_NEW_Location_Code_Item_Code_main_location_Qty]"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "drop index  [TSPL_INVENTORY_MOVEMENT_NEW].[for_NC_cansale_1]"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "alter table tspl_inventory_movement_new alter column Stock_Qty decimal(18,3)"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "CREATE NONCLUSTERED INDEX [TSPL_INVENTORY_MOVEMENT_NEW_Location_Code_Item_Code_Punching_Date] ON [dbo].[TSPL_INVENTORY_MOVEMENT_NEW](	[Location_Code] ASC,	[Item_Code] ASC,[Punching_Date] ASC)INCLUDE ( 	[InOut],[Avg_Cost],[Stock_Qty]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "CREATE NONCLUSTERED INDEX [TSPL_INVENTORY_MOVEMENT_NEW_Location_Code_Item_Code_main_location_Stock_Qty_NEW] ON [dbo].[TSPL_INVENTORY_MOVEMENT_NEW]([Location_Code] ASC,[Item_Code] ASC,[main_location] ASC,[Qty] ASC)INCLUDE ( 	[InOut],[Punching_Date],[Stock_UOM],[Stock_Qty]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "CREATE NONCLUSTERED INDEX [TSPL_INVENTORY_MOVEMENT_NEW_Location_Code_Item_Code_main_location_Qty] ON [dbo].[TSPL_INVENTORY_MOVEMENT_NEW]([Location_Code] ASC,[Item_Code] ASC,[main_location] ASC,[Qty] ASC)INCLUDE ( 	[InOut],[Stock_UOM],[Stock_Qty]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "CREATE NONCLUSTERED INDEX [for_NC_cansale_1] ON [dbo].[TSPL_INVENTORY_MOVEMENT_NEW]([Location_Code] ASC,[Item_Code] ASC,[Punching_Date] ASC)INCLUDE ( 	[InOut],[Avg_Cost],[Stock_Qty]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    End If
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    ''clsCommon.MyMessageBoxShow(ex.Message)
                End Try

                '-----Purchase moduel Tax Rate column change
                qry = "alter table tspl_Purchase_order_head alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_head alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_head alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_head alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_head alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_head alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_head alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_head alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_head alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_head alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_detail alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_detail alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_detail alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_detail alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table tspl_Purchase_order_detail alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_Purchase_order_detail alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_detail alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_detail alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_detail alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_Purchase_order_detail alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_head alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_head alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_head alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_head alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_head alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_head alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_head alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_head alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_head alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_head alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_detail alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_detail alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_detail alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_detail alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table tspl_GRN_detail alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_GRN_detail alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_detail alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_detail alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_detail alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_GRN_detail alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_head alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_head alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_head alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_head alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_head alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_head alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_head alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_head alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_head alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_head alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_detail alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_detail alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_detail alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_detail alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table tspl_MRN_detail alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_MRN_detail alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_detail alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_detail alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_detail alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_MRN_detail alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_COMPANY_MASTER alter column Vat_Reg_No varchar (50) null "
                ExecuteQeuryWithCatch(qry)

                qry = "drop INDEX [For_NC_PI_Index1]  ON [dbo].[TSPL_SRN_DETAIL]"
                ExecuteQeuryWithCatch(qry)

                qry = "drop INDEX [For_NC_PI_Index3] ON [dbo].[TSPL_SRN_DETAIL]"
                ExecuteQeuryWithCatch(qry)






                qry = "alter table tspl_SRN_head alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_head alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_head alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_head alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_head alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_head alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_head alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_head alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_head alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_head alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_detail alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_detail alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_detail alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_detail alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table tspl_SRN_detail alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_SRN_detail alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_detail alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_detail alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_detail alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_SRN_detail alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "CREATE NONCLUSTERED INDEX [For_NC_PI_Index1] ON [dbo].[TSPL_SRN_DETAIL](	[Status] ASC,[Row_Type] ASC)INCLUDE ( 	[SRN_No],	[Line_No],	[Item_Code],	[Item_Desc],	[SRN_Qty],	[MRN_Id],	[Unit_code],	[Location],	[Item_Cost],	[TAX1_Rate],	[TAX1_Amt],	[TAX2_Rate],	[TAX2_Amt],	[TAX3_Rate],	[TAX3_Amt],	[TAX4_Rate],	[TAX4_Amt],	[TAX5_Rate],	[TAX5_Amt],	[TAX6_Rate],	[TAX6_Amt],	[TAX7_Rate],	[TAX7_Amt],	[TAX8_Rate],	[TAX8_Amt],	[TAX9_Rate],	[TAX9_Amt],	[TAX10_Rate],	[TAX10_Amt],	[Amount],	[Disc_Per],	[MRP],	[Batch_No],	[MFG_Date],	[Expiry_Date],	[Rejected_Qty],	[Leak_Qty],	[Burst_Qty],	[Short_Qty],	[Is_Mannual_Amt],	[Free_Qty],	[PO_ID],	[AbatementRate],	[Bin_No],	[Disc_Type],	[GRN_ID],	[Category],	[Emergency],	[Capex_Code],	[Capex_SubCode]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)"
                ExecuteQeuryWithCatch(qry)

                qry = "CREATE NONCLUSTERED INDEX [For_NC_PI_Index3] ON [dbo].[TSPL_SRN_DETAIL](	[SRN_No] ASC,	[Status] ASC,	[Row_Type] ASC)INCLUDE ( 	[Line_No],	[Item_Code],	[Item_Desc],	[SRN_Qty],	[MRN_Id],	[Unit_code],	[Location],	[Item_Cost],	[TAX1_Rate],	[TAX1_Amt],	[TAX2_Rate],	[TAX2_Amt],	[TAX3_Rate],	[TAX3_Amt],	[TAX4_Rate],	[TAX4_Amt],	[TAX5_Rate],	[TAX5_Amt],	[TAX6_Rate],	[TAX6_Amt],	[TAX7_Rate],	[TAX7_Amt],	[TAX8_Rate],	[TAX8_Amt],	[TAX9_Rate],	[TAX9_Amt],	[TAX10_Rate],	[TAX10_Amt],	[Amount],	[Disc_Per],	[MRP],	[Batch_No],	[MFG_Date],	[Expiry_Date],	[Rejected_Qty],	[Leak_Qty],	[Burst_Qty],	[Short_Qty],	[Is_Mannual_Amt],	[Free_Qty],	[PO_ID],[AbatementRate],	[Bin_No],	[Disc_Type],	[GRN_ID],	[Category],	[Emergency],	[Capex_Code],	[Capex_SubCode]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_head alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_head alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_head alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_head alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_head alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_head alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_head alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_head alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_head alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_head alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_detail alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_detail alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_detail alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_detail alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table tspl_PI_detail alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PI_detail alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_detail alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_detail alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_detail alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PI_detail alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_head alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_head alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_head alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_head alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_head alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_head alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_head alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_head alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_head alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_head alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_detail alter column TAX1_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_detail alter column TAX2_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_detail alter column TAX3_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_detail alter column TAX4_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = " alter table tspl_PR_detail alter column TAX5_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table tspl_PR_detail alter column TAX6_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_detail alter column TAX7_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_detail alter column TAX8_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_detail alter column TAX9_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)


                qry = "alter table tspl_PR_detail alter column TAX10_Rate decimal(18,3)"
                ExecuteQeuryWithCatch(qry)



                '-----End  of Purchase moduel Tax Rate column change





                qry = "select 1 from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='TSPL_DOCPREFIX_MASTER' and column_name='PK_ID'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    qry = " ALTER TABLE TSPL_DOCPREFIX_MASTER ADD PK_ID int IDENTITY(1,1) not null"
                    ExecuteQeuryWithCatch(qry)
                    qry = "ALTER TABLE TSPL_DOCPREFIX_MASTER ADD PRIMARY KEY (PK_ID)"
                    ExecuteQeuryWithCatch(qry)
                End If

                qry = "alter table TSPL_TAX_RATES alter column TAX_RATE DECIMAL(9,4) "
                ExecuteQeuryWithCatch(qry)

                qry = "alter table TSPL_QC_CHECK_SRN_DETAIL alter column Param_QC_Status varchar(100) "
                ExecuteQeuryWithCatch(qry)

                qry = "alter table TSPL_QC_CHECK_SRN_DETAIL alter column Param_Status varchar(100) "
                ExecuteQeuryWithCatch(qry)

                qry = "alter table TSPL_BOOKING_DETAIL add CONSTRAINT uk_DOCUMENT_NO_LINE_NO UNIQUE (DOCUMENT_NO,LINE_NO)"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table TSPL_RECEIPT_DETAIL add CONSTRAINT uk_Receipt_No_Receipt_Line_No UNIQUE (Receipt_No,Receipt_Line_No)"
                ExecuteQeuryWithCatch(qry)

                qry = "ALTER TABLE TSPL_DOCPREFIX_MASTER DROP CONSTRAINT uk_TSPL_DOCPREFIX_MASTER"
                ExecuteQeuryWithCatch(qry)

                qry = "alter table TSPL_DOCPREFIX_MASTER add CONSTRAINT uk_TSPL_DOCPREFIX_MASTER UNIQUE (Doc_Type,Doc_Trans_Type,Location_Code,RouteNo,Fin_Year,Is_Change_Monthly,Is_Change_Daily,Curr_Month,Curr_Date)"
                ExecuteQeuryWithCatch(qry)

                ''richa agarwal 5 Oct,2020

                qry = " alter table TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE alter column tax1_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE alter column tax2_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  alter column tax3_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  alter column tax4_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  alter column tax5_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE  alter column tax1_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE alter column tax2_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE alter column tax3_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE alter column tax4_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE alter column tax5_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)


                qry = " alter table TSPL_SD_SALES_ORDER_HEAD alter column tax1_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_HEAD alter column tax2_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_HEAD alter column tax3_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_HEAD alter column tax4_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_HEAD  alter column tax5_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_DETAIL alter column tax1_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_DETAIL alter column tax2_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_DETAIL alter column tax3_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_DETAIL alter column tax4_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SALES_ORDER_DETAIL alter column tax5_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_HEAD alter column tax1_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_HEAD alter column tax2_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_HEAD alter column tax3_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_HEAD alter column tax4_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_HEAD alter column tax5_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_detail alter column tax1_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_detail alter column tax2_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_detail alter column tax3_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_detail alter column tax4_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_SHIPMENT_detail alter column tax5_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_head alter column tax1_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_head alter column tax2_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_head alter column tax3_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_head alter column tax4_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_head  alter column tax5_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_detail alter column tax1_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_detail  alter column tax2_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_detail alter column tax3_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_detail alter column tax4_rate Decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = " alter table TSPL_SD_sale_invoice_detail alter column tax5_rate decimal(18,4) null "
                ExecuteQeuryWithCatch(qry)

                qry = "alter table TSPL_ITEM_WISE_TAX_AUTHORITY alter column tax_rate decimal(18,4) null"
                ExecuteQeuryWithCatch(qry)

                'qry = "alter table TSPL_ITEM_PRICE_MASTER alter column tax1_rate decimal(18,4) null"
                'ExecuteQeuryWithCatch(qry)

                'qry = "alter table TSPL_ITEM_PRICE_MASTER alter column tax2_rate decimal(18,4) null"
                'ExecuteQeuryWithCatch(qry)

                'qry = "alter table TSPL_ITEM_PRICE_MASTER alter column tax3_rate decimal(18,4) null"
                'ExecuteQeuryWithCatch(qry)

                'qry = "alter table TSPL_ITEM_PRICE_MASTER alter column tax4_rate decimal(18,4) null"
                'ExecuteQeuryWithCatch(qry)

                'qry = "alter table TSPL_ITEM_PRICE_MASTER alter column tax5_rate decimal(18,4) null"
                'ExecuteQeuryWithCatch(qry)
                ''end of 5 oct,2020

                qry = "select 1 from TSPL_Exception"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('1','[ERROR] FATAL UNHANDLED EXCEPTION: System.IndexOutOfRangeException: Index was outside the bounds of the array.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('2','[0x00007] in <69eb3976d7534e8dae611f6b1ae97a34>:0 ','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('3','There is no non-obsolete alternative to ExecutionEngineException. If further execution of your application cannot be sustained, use the FailFast method.','0')"
                    ExecuteQeuryWithCatch(qry)


                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('4','An invalid Uniform Resource Identifier (URI) is used.','0')"
                    ExecuteQeuryWithCatch(qry)


                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('5','The time interval allotted to an operation has expired.','0')"
                    ExecuteQeuryWithCatch(qry)


                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('6','An array with the wrong number of dimensions is passed to a method.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('7','The operation is not supported on the current platform.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('8','A path or file name exceeds the maximum system-defined length.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('9','An arithmetic, casting, or conversion operation results in an overflow.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('10','An operation is performed on an object that has been disposed.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('11','A method or operation is not supported.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('12','A method or operation is not implemented.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('13','The specified key for accessing a member in a collection cannot be found.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('14','A method call is invalid in an objects current state.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('15','The denominator in an integer or Decimal division operation is zero.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('16','Part of a directory path is not valid.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('17','An index is outside the bounds of an array or collection.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('18','A value is not in an appropriate format to be converted from a string by a conversion method such as Parse.','0')"
                    ExecuteQeuryWithCatch(qry)


                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('19','A file does not exist.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('20','A drive is unavailable or does not exist.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('21','An argument that is passed to a method is null.','0')"
                    ExecuteQeuryWithCatch(qry)

                    qry = "Insert into TSPL_Exception(SNo,Exception_Msg,Is_Current)values ('22','A non-null argument that is passed to a method is invalid.','0')"
                    ExecuteQeuryWithCatch(qry)

                End If
            Catch ex As Exception
            End Try

            Try
                qry = "alter table TSPL_JWI_ESTIMATION_WEIGHMENT alter column FAT_PER decimal(18,3)"
                ExecuteQeuryWithCatch(qry)
                qry = "alter table TSPL_JWI_ESTIMATION_WEIGHMENT alter column SNF_PER decimal(18,3)"
                ExecuteQeuryWithCatch(qry)

                ''By Balwinder becuase this column used in replication of kwality.
                If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Code from tspl_company_master")), "KL") = CompairStringResult.Equal Then
                    qry = "alter table TSPL_MILK_Shift_End_HEAD drop column Reason"
                    ExecuteQeuryWithCatch(qry)

                    qry = "alter table TSPL_MILK_Shift_End_HEAD drop  DF__TSPL_MILK__Deduc__32BC038E"
                    ExecuteQeuryWithCatch(qry)

                    qry = "alter table TSPL_MILK_Shift_End_HEAD drop column Deduction_of_Transporter"
                    ExecuteQeuryWithCatch(qry)

                    Dim strKey As String = clsERPFuncationality.GetConstraintWorking("TSPL_JOURNAL_MASTER", "Hirerachy_Code")
                    If clsCommon.myLen(strKey) > 0 Then
                        qry = "alter table TSPL_JOURNAL_MASTER drop  " + strKey
                        ExecuteQeuryWithCatch(qry)
                    End If
                    qry = "alter table TSPL_JOURNAL_MASTER drop column Hirerachy_Code"
                    ExecuteQeuryWithCatch(qry)


                    strKey = clsERPFuncationality.GetConstraintWorking("TSPL_JOURNAL_MASTER", "Cost_Centre_Code")
                    If clsCommon.myLen(strKey) > 0 Then
                        qry = "alter table TSPL_JOURNAL_MASTER drop  " + strKey
                        ExecuteQeuryWithCatch(qry)
                    End If
                    qry = "alter table TSPL_JOURNAL_MASTER drop column Cost_Centre_Code"
                    ExecuteQeuryWithCatch(qry)



                    qry = "alter table TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL_SYNC drop constraint FK__TSPL_MILK__Docum__26438015"
                    ExecuteQeuryWithCatch(qry)

                        strKey = clsERPFuncationality.GetConstraint("TSPL_MILK_RECEIPT_DETAIL", "Against_Uploader_TR_No")
                        If clsCommon.myLen(strKey) > 0 Then
                            qry = "alter table TSPL_MILK_RECEIPT_DETAIL drop " + strKey
                            ExecuteQeuryWithCatch(qry)
                        End If


                    End If
            Catch ex As Exception
            End Try


            ''richa agarwal 8 Nov,2019 
            qry = " alter table  tspl_purchase_order_head_hist_new alter column workorder_vendor_phn varchar(20) null "
            ExecuteQeuryWithCatch(qry)
            ''------------------


            ''richa agarwal 3 Oct,2019 ERO/03/10/19-001041
            qry = " alter table TSPL_SCHEME_MASTER_VOLUME_SLAB alter column max_range decimal(18,2) "
            ExecuteQeuryWithCatch(qry)
            ''------------------

            ''richa agarwal 22 Oct,2019 
            qry = "alter table tspl_bank_book alter column SOURCE_NAME varchar(200) null "
            ExecuteQeuryWithCatch(qry)
            ''------------------


            qry = "alter table TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE drop column Range_Avg_Qty"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE drop column Incentive_Rate"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE drop column Incentive_Amount"

            qry = clsERPFuncationality.GetConstraint("TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE", "Incentive_TR_Code")
            If clsCommon.myLen(qry) > 0 Then
                qry = "alter table TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE drop " + qry
                ExecuteQeuryWithCatch(qry)
            End If
            qry = "alter table TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE drop FK__TSPL_CUST__Incen__146034BC"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE drop column Incentive_TR_Code"
            ExecuteQeuryWithCatch(qry)



            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_JWI_ESTIMATION_FAT_PRODUCTION alter column Item_Code varchar(50) null")
            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_JWI_ESTIMATION_FAT_PRODUCTION alter column BOM_CODE varchar(30) null")
            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_JWI_ESTIMATION_FAT_PRODUCTION alter column UOM varchar(12) null")

            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_JWI_ESTIMATION_SNF_PRODUCTION alter column Item_Code varchar(50) null")
            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_JWI_ESTIMATION_SNF_PRODUCTION alter column BOM_CODE varchar(30) null")
            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_JWI_ESTIMATION_SNF_PRODUCTION alter column UOM varchar(12) null")

            ''BHA/28/08/18-000489 by balwinder on 29/11/2018
            qry = "alter table TSPL_SMS_HEAD alter column SMS_Text nvarchar(4000)  "
            clsDBFuncationality.ExecuteNonQuery(qry)

            ''BHA/28/08/18-000489 by balwinder on 29/08/2018

            'qry = "alter table TSPL_GRN_DETAIL alter column Item_Cost decimal(18,3) null"
            'ExecuteQeuryWithCatch(qry)

            'qry = "alter table TSPL_MRN_DETAIL alter column Item_Cost decimal(18,3) null"
            'ExecuteQeuryWithCatch(qry)

            qry = "Alter table tspl_location_master Alter Column City_Code varchar(50) NULL"
            ExecuteQeuryWithCatch(qry)

            ''BHA/17/08/18-000448 by balwinder on 17/08/2018
            'qry = "update TSPL_PP_BOM_HEAD set revision_no=xx.BOM_CODE+'.'+xx.cnt from (" + Environment.NewLine + _
            '"select bom_code, Revision_No,(select max(Revision_No) from TSPL_PP_BOM_HEAD_HISTORY where TSPL_PP_BOM_HEAD_HISTORY.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE) as HisRevNo,cast((select count(*) from TSPL_PP_BOM_HEAD_HISTORY where TSPL_PP_BOM_HEAD_HISTORY.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE) as varchar(2)) as Cnt" + Environment.NewLine + _
            '"from TSPL_PP_BOM_HEAD where isnull( Revision_No,'')='' and exists(select 1  from TSPL_PP_BOM_HEAD_HISTORY where TSPL_PP_BOM_HEAD_HISTORY.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE)" + Environment.NewLine + _
            '")xx " + Environment.NewLine + _
            '"inner join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=xx.bom_code "
            'ExecuteQeuryWithCatch(qry)

            'qry = "update TSPL_PP_BATCH_ORDER_BOM_DETAIL set TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Revision_No= xx.Revision_No from (" + Environment.NewLine + _
            '"select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code,(select  top 1  Revision_No from (" + Environment.NewLine + _
            '"select PROD_QUANTITY, Prod_Item_Unit_Code,BOM_CODE,'AAA' as History_No,GETDATE() as Modified_Date,Revision_No   from TSPL_PP_BOM_HEAD where BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code" + Environment.NewLine + _
            '"union all" + Environment.NewLine + _
            '"select PROD_QUANTITY, Prod_Item_Unit_Code,BOM_CODE, History_No,Modified_Date,Revision_No  from TSPL_PP_BOM_HEAD_HISTORY where BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code" + Environment.NewLine + _
            '")xx where convert(date, Modified_Date,103)>CONVERT(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) order by Modified_Date ) as Revision_No " + Environment.NewLine + _
            '"from TSPL_PP_BATCH_ORDER_BOM_DETAIL " + Environment.NewLine + _
            '"left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code" + Environment.NewLine + _
            '")xx inner join TSPL_PP_BATCH_ORDER_BOM_DETAIL on xx.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and xx.Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code"
            'ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL alter column total_amount decimal(18,2)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_TRANSACTION_APPROVAL alter column Screen_Name varchar(100) not null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_PP_STAGE_PROCESS_HEAD alter column  Issue_code varchar (30) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  SNF_AMT float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  FIFO_Cost decimal  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  LIFO_Cost decimal  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  Avg_Cost decimal  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  Fat_Rate float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  SNF_Rate float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  Fat_Amt float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_pp_production_entry_detail alter column  SNF_Amt float null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  SNF_AMT float null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  FIFO_Cost decimal  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  LIFO_Cost decimal  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  Avg_Cost decimal  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  Fat_Rate float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  SNF_Rate float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  Fat_Amt float  null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL alter column  SNF_Amt float null"
            ExecuteQeuryWithCatch(qry)



            qry = "delete from tspl_fixed_parameter where type='Allow Journal Entry on Process Producion Issue'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "delete from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING where FP_Type='Allow Journal Entry on Process Producion Issue'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "delete from TSPL_FIXED_PARAMETER where Code = 'DateOfQRCodeImplementation' and Type = 'DateOfQRCodeImplementation'"
            clsDBFuncationality.ExecuteNonQuery(qry)


            qry = "alter table TSPL_MRN_DETAIL alter column Balance_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column Short_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column Excess_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column Leak_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column Burst_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column Accept_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column Reject_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table tspl_PI_DEtail alter column Leak_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table tspl_PI_DEtail alter column Burst_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table tspl_PI_DEtail alter column Short_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "alter table tspl_PI_DEtail alter column Reject_Qty decimal(18,3)"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Try
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Code from tspl_company_master")), "UDL") = CompairStringResult.Equal Then
                    qry = "select 1 from TSPL_MILK_REJECT_TYPE"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Sour','Sour','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Salt','Salt','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Sugar','Sugar','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Glucose','Glucose','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Maltose','Maltose','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Starch','Starch','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "Insert into TSPL_MILK_REJECT_TYPE(Code,Description,Applicable_Per,Item_Code,Created_By,Created_Date,Modify_By,Modify_Date)values ('Curd','Curd','0','RM0000001','Admin',getdate(),'Admin',getdate())"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                End If
            Catch ex As Exception
            End Try


            qry = "alter table TSPL_PURCHASE_ORDER_DETAIL alter column PurchaseOrder_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_DETAIL alter column GRN_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_DETAIL alter column MRN_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "drop index Index_tspl_srn_detail_free_burst_leak_srn_qty on TSPL_SRN_DETAIL"
            ExecuteQeuryWithCatch(qry)


            qry = "drop index _dta_index_TSPL_SRN_DETAIL_10_1709965168__K8_K3_K9_K1_K58_K4_74 on TSPL_SRN_DETAIL"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_SRN_DETAIL alter column SRN_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_SRN_DETAIL alter column Leak_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_SRN_DETAIL alter column Burst_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_SRN_DETAIL alter column Short_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_SRN_DETAIL alter column Rejected_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_PI_DETAIL alter column PI_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_PR_DETAIL alter column PR_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_SRN_DETAIL alter column GRN_Qty decimal(18,10) null"
            ExecuteQeuryWithCatch(qry)


            ' For Additional charge & Additional charge Hist Table 
            qry = "alter table tspl_Additional_Charges alter column description varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_ADDITIONAL_CHARGES_Hist_Data alter column Description varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' For AP INVOICE Entry Screen 
            qry = "alter table TSPL_VENDOR_INVOICE_DETAIL alter column AddChargeDesc varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_VENDOR_INVOICE_HEAD alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' Purchase Order
            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_Purchase_order_head alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' Purchase Order Hist

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' Purchase Invoice

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' MRN

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' MRN Hist

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' SRN

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' SRN Hist


            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' GRN

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            ' GRN HIST

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name2 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name3 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name4 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name5 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name6 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name7 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name8 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name9 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name10 varchar(500)"
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_PURCHASE_ORDER_HEAD_Hist alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_PI_Head alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_MRN_Head alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MRN_HEAD_HISTORY alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table tspl_SRN_Head alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SRN_HEAD_HISTORY alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GRN_HEAD_HISTORY alter column Add_Charge_Name1 varchar(500)"
            ExecuteQeuryWithCatch(qry)
            'Previous version missing script

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name1 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name2 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name3 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name4 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name5 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name6 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name7 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name8 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name9 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column Add_Charge_Name10 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            'HIST

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name1 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name2 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name3 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name4 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name5 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name6 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name7 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name8 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name9 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head_History alter column Add_Charge_Name10 varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            '................
            qry = "alter table TSPL_PURCHASE_ORDER_DETAIL alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PURCHASE_ORDER_DETAIL_Hist alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PURCHASE_ORDER_DETAIL_Hist_New alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_GRN_DETAIL alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_GRN_DETAIL_HISTORY alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MRN_DETAIL alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MRN_DETAIL_HISTORY alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SRN_DETAIL alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SRN_DETAIL_HISTORY alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PI_DETAIL alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PR_DETAIL alter column item_desc varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GATEPASS_MASTER_DAIRYSALE alter column Status default 0"
            ExecuteQeuryWithCatch(qry)
            qry = "Update TSPL_GATEPASS_MASTER_DAIRYSALE set Status=0 where Status is null"
            ExecuteQeuryWithCatch(qry)

            ExecuteQeuryWithCatch("Update TSPL_PURCHASE_ORDER_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null")
            ExecuteQeuryWithCatch("Update TSPL_PURCHASE_ORDER_DETAIL set Taxable_Amount=Amt_Less_Discount where Taxable_Amount is null")
            ExecuteQeuryWithCatch("Update TSPL_PURCHASE_ORDER_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null")

            ExecuteQeuryWithCatch("Update TSPL_GRN_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null")
            ExecuteQeuryWithCatch("Update TSPL_GRN_DETAIL set Taxable_Amount=Amt_Less_Discount where Taxable_Amount is null")
            ExecuteQeuryWithCatch("Update TSPL_GRN_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null")

            ExecuteQeuryWithCatch("Update TSPL_MRN_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null")
            ExecuteQeuryWithCatch("Update TSPL_MRN_DETAIL set Taxable_Amount=Amt_Less_Discount where Taxable_Amount is null")
            ExecuteQeuryWithCatch("Update TSPL_MRN_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null")

            ExecuteQeuryWithCatch("Update TSPL_SRN_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null")
            ExecuteQeuryWithCatch("Update TSPL_SRN_DETAIL set Taxable_Amount=Amt_Less_Discount where Taxable_Amount is null")
            ExecuteQeuryWithCatch("Update TSPL_SRN_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null")

            ExecuteQeuryWithCatch("Update TSPL_PI_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null")
            ExecuteQeuryWithCatch("Update TSPL_PI_DETAIL set Taxable_Amount=Amt_Less_Discount where Taxable_Amount is null")
            ExecuteQeuryWithCatch("Update TSPL_PI_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null")

            ExecuteQeuryWithCatch("Update TSPL_PR_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null")
            ExecuteQeuryWithCatch("Update TSPL_PR_DETAIL set Taxable_Amount=Amt_Less_Discount where Taxable_Amount is null")
            ExecuteQeuryWithCatch("Update TSPL_PR_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null")
            qry = "update TSPL_DOCUMENT_TYPE set Doc_Trans_Type='Bulk Milk Bill of Supply' where Doc_Trans_Type='Bulk Milk Tax Invoice'"
            ExecuteQeuryWithCatch(qry)
            qry = "update TSPL_DOCUMENT_TYPE set Doc_Trans_Type='Bulk Milk Bill of Supply Trade' where Doc_Trans_Type='Bulk Milk Tax Invoice Trade'"
            ExecuteQeuryWithCatch(qry)
            qry = "update TSPL_DOCPREFIX_MASTER set Doc_Trans_Type='Bulk Milk Bill of Supply' where Doc_Trans_Type='Bulk Milk Tax Invoice'"
            ExecuteQeuryWithCatch(qry)
            qry = "update TSPL_DOCPREFIX_MASTER set Doc_Trans_Type='Bulk Milk Bill of Supply Trade' where Doc_Trans_Type='Bulk Milk Tax Invoice Trade'"
            ExecuteQeuryWithCatch(qry)

            'qry = "Update TSPL_VENDOR_INVOICE_DETAIL set Taxable_Amount_Per=100 where Taxable_Amount_Per is null"
            'ExecuteQeuryWithCatch(qry)
            'qry = "Update TSPL_VENDOR_INVOICE_DETAIL set Taxable_Amount=Amount_Less_Discount where Taxable_Amount is null"
            'ExecuteQeuryWithCatch(qry)
            'qry = "Update TSPL_VENDOR_INVOICE_HEAD set Total_Taxable_Amount=Amount_Less_Discount where Total_Taxable_Amount is null"
            'ExecuteQeuryWithCatch(qry)

            qry = "Update TSPL_MILK_Shift_End_Head set Is_Flushing_Created=0 where Is_Flushing_Created is null"
            ExecuteQeuryWithCatch(qry)

            qry = "delete from TSPL_CREATE_BI_REPORT_FILTERS where code='PTYWSSALS'"
            ExecuteQeuryWithCatch(qry)
            qry = "delete from TSPL_CREATE_BI_REPORT where code='PTYWSSALS'"
            ExecuteQeuryWithCatch(qry)
            qry = "delete from TSPL_PROGRAM_MASTER where Program_Code='PTYWSSALS'"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_BOOKING_DETAIL alter column Vehicle_Code varchar(12) Not null"
            ExecuteQeuryWithCatch(qry)
            qry = "ALTER TABLE TSPL_BOOKING_DETAIL ADD CONSTRAINT FK_BOOKING_DETAIL_Vehicle_Code FOREIGN KEY (Vehicle_Code) REFERENCES TSPL_VEHICLE_MASTER(Vehicle_Id)"
            ExecuteQeuryWithCatch(qry)

            qry = "delete from TSPL_CREATE_BI_REPORT where Code='PenDocList'"
            ExecuteQeuryWithCatch(qry)
            qry = "delete from TSPL_PROGRAM_MASTER where Program_Code='PenDocList'"
            ExecuteQeuryWithCatch(qry)

            qry = " alter table  TSPL_INVOICE_MASTER_BULKSALE ADD CONSTRAINT DF_Constraint DEFAULT 0 FOR Posted "
            ExecuteQeuryWithCatch(qry)

            'sanjay
            qry = "delete from TSPL_CREATE_BI_REPORT where Code='TnkrStausrpt'"
            ExecuteQeuryWithCatch(qry)
            qry = "delete from TSPL_PROGRAM_MASTER where Program_Code='TnkrStausrpt'"
            ExecuteQeuryWithCatch(qry)
            'sanjay

            'sanjay
            qry = "delete from TSPL_PROGRAM_MASTER where Program_Code='SKU-SAL-RPT'"
            ExecuteQeuryWithCatch(qry)
            'sanjay

            'priti
            qry = "delete from TSPL_PROGRAM_MASTER where Program_code in ('MutCustDis','PR-BOOK')"
            ExecuteQeuryWithCatch(qry)
            'priti

            qry = "DROP TRIGGER [dbo].[TrgDeleteGLentry]"
            ExecuteQeuryWithCatch(qry)

            'priti
            qry = "delete from tspl_fixed_parameter where code='CreateBulkMilkSRNItemwise' "
            ExecuteQeuryWithCatch(qry)
            'priti

            'sanjay Ticket No- BHA/24/09/18-000564 Emp code as per employee type
            qry = "update TSPL_DOCPREFIX_MASTER set Doc_Trans_Type='All' where Doc_Type='Employee Master' and Doc_Trans_Type=''"
            ExecuteQeuryWithCatch(qry)
            'sanjay
            qry = "alter table TSPL_WEIGHT_CONVERSION alter column Contained_Qty decimal(18,4)"
            ExecuteQeuryWithCatch(qry)
            qry = " alter table TSPL_VENDOR_MASTER_Hist_Data alter column Hist_By varchar(12) null "
            ExecuteQeuryWithCatch(qry)
            'By Prabhakar- For Remove Screen Investment Declaration Old,Investment Type Old, IT Section Old 
            qry = " Delete from TSPL_PROGRAM_MASTER where Program_Code  in ('TDS_IT_Sec','TDS_INV_TYP','TDS_Inv_DEC') "
            ExecuteQeuryWithCatch(qry)
            ' Ticket No : TEC/12/02/19-000420 by prabhakar for remove screen , Ticket : BHA/15/02/19-000816 by prabhakar for [VSP Wise data report] delete from menu , Ticket No : TEC/19/03/19-000453 for HR Setting Screen Remove from List 
            qry = " Delete from TSPL_PROGRAM_MASTER where Program_Code  in ('ITEM-CAT','ITM-SUB-CAT','PROD-ENTRY','EXPIRY-ENT','PJC-Assembly','I-S-CNV','GP-TRANSFR','TRANSFR_CR','LST-DFCT-SN', 'Milk-RecShed','MILK-MCC','VSP-Data-Rpt','HR-Set','R-PAY-TERMS') "
            ExecuteQeuryWithCatch(qry)
            qry = " alter table TSPL_SALARY_CALCULATION add default 0 for ADJUSTMENT_PLUS "
            ExecuteQeuryWithCatch(qry)
            qry = " alter table TSPL_SALARY_CALCULATION add default 0 for ADJUSTMENT_MINUS "
            ExecuteQeuryWithCatch(qry)
            qry = " alter table TSPL_GENERATE_SALARY_ATTENDANCE add default 0 for EPS_TO_EPF "
            ExecuteQeuryWithCatch(qry)
            Try
                Dim chk As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(OBJECT_ID) AS TotalTables FROM sys.tables where name='TSPL_TABLECOPY'"))
                If chk = 0 Then
                    clsDBFuncationality.ExecuteNonQuery("select * into TSPL_TABLECOPY from (Select  1 as COL2,  'ORIGINAL COPY' as CopyType1  UNION Select  2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select  3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select  4 as COL2,  'QUADRUPLICATE COPY' as CopyType1)xxx")
                End If

            Catch ex As Exception

            End Try

            Try
                clsDBFuncationality.ExecuteNonQuery("Alter Table TSPL_PURCHASE_ORDER_detail Alter column Model VARCHAR(200) NULL")
                clsDBFuncationality.ExecuteNonQuery("Alter Table TSPL_PURCHASE_ORDER_detail Alter column Make VARCHAR(200) NULL")
                clsDBFuncationality.ExecuteNonQuery("Alter Table TSPL_PURCHASE_ORDER_detail Alter column Capacity VARCHAR(200) NULL")

                clsDBFuncationality.ExecuteNonQuery("Alter Table TSPL_PURCHASE_ORDER_detail_Hist_New Alter column Model VARCHAR(200) NULL")
                clsDBFuncationality.ExecuteNonQuery("Alter Table TSPL_PURCHASE_ORDER_detail_Hist_New Alter column Make VARCHAR(200) NULL")
                clsDBFuncationality.ExecuteNonQuery("Alter Table TSPL_PURCHASE_ORDER_detail_Hist_New Alter column Capacity VARCHAR(200) NULL")
            Catch ex As Exception
            End Try

            qry = "alter table TSPL_PRICE_GROUP_MAPPING_HEAD alter column created_date datetime NULL"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PRICE_GROUP_MAPPING_HEAD alter column modify_date datetime NULL"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PRICE_GROUP_MAPPING alter column created_date datetime NULL"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_PRICE_GROUP_MAPPING alter column modify_date datetime NULL"
            ExecuteQeuryWithCatch(qry)
            qry = " Alter Table TSPL_Customer_Invoice_Detail Alter Column Remarks varchar(500) null "
            ExecuteQeuryWithCatch(qry)
            qry = "Alter Table TSPL_VCGL_Head Alter Column Description varchar(500) null"
            ExecuteQeuryWithCatch(qry)

            'Ticket No-BHA/24/05/19-000896
            Dim chkConstraint As String = clsERPFuncationality.GetConstraint("TSPL_MP_MASTER", "MP_Farmer_Billing")
            If clsCommon.myLen(chkConstraint) <= 0 Then
                qry = "ALTER TABLE TSPL_MP_MASTER ADD DEFAULT 0 FOR MP_Farmer_Billing"
                ExecuteQeuryWithCatch(qry)
            End If

            qry = "ALTER TABLE TSPL_VENDOR_INVOICE_HEAD ALTER COLUMN Remarks varchar(200)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_EMPLOYEE_SALARY_PAYHEADS alter column RATE_AMOUNT numeric(18,2)"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_EMPLOYEE_SALARY_PAYHEADS alter column PAYPERIOD_AMOUNT numeric(18,2)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_ITEM_PRICE_MASTER alter column Abatement varchar(30)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_PAYMENT_PROCESS_DETAIL alter column Payee_Joint_Name varchar(100)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_REQUISITION_HEAD alter column Payee_Joint_Name varchar(100)"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SALSTRUCT_PAYHEADS alter column RATE_AMOUNT NUMERIC(10,3) NOT NULL"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SALARY_CALCULATION alter column RATE_AMOUNT NUMERIC(10,3) NOT NULL"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_GENERATE_SALARY_PAYHEADS alter column RATE_AMOUNT NUMERIC(10,3) NOT NULL"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_ARREAR_CALCULATION alter column RATE_AMOUNT NUMERIC(10,3) NOT NULL"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MILK_UNLOADING alter column Sub_location_Code varchar(12) NULL "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MILK_UNLOADING_History alter column Sub_location_Code varchar(12) NULL "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_From_1 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_From_2 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_From_3 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_From_4 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_From_5 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_To_1 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_To_2 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_To_3 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_To_4 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_MCC_MASTER alter column Day_Wise_Incentive_To_5 decimal(18,2) null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SD_SALE_INVOICE_HEAD alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SD_SALE_INVOICE_HEAD_HISTORY alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SD_SALE_RETURN_HEAD alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_Customer_Invoice_Head alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_Customer_Invoice_Head_History alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INVOICE_HEAD_Delete_Data alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INVOICE_HEAD_Cancel_Data alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            'qry = "alter table TSPL_SCRAPSALE_HEAD alter column QR_Code VARCHAR(MAX) null "
            'ExecuteQeuryWithCatch(qry)
            'qry = "alter table TSPL_SCRAPSALE_HEAD_history alter column QR_Code VARCHAR(MAX) null "
            'ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD_RETURN alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_TRANSFER_ORDER_HEAD alter column QR_Code VARCHAR(MAX) null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SCRAPSALE_HEAD_History drop column EInvoice_Type"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD_History drop column IRN_No"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD_History drop column QR_Code"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD_History drop column Ack_No"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD_History drop column Ack_Date"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD_History drop column BarCode_Img"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SCRAPSALE_HEAD drop column EInvoice_Type"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD drop column IRN_No"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD drop column QR_Code"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD drop column Ack_No"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD drop column Ack_Date"
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPSALE_HEAD drop column BarCode_Img"
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SD_SALE_INVOICE_HEAD alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SD_SALE_INVOICE_HEAD_HISTORY alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SD_SALE_RETURN_HEAD alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SD_SALE_RETURN_HEAD_Cancel_Data alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SCRAPINVOICE_HEAD alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPINVOICE_HEAD_CANCEL alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPINVOICE_HEAD_History alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_SCRAPSALE_HEAD_RETURN alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)


            qry = "alter table TSPL_TRANSFER_ORDER_HEAD alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_TRANSFER_ORDER_HEAD_Hist_Data alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)

            qry = "alter table TSPL_Customer_Invoice_Head alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_Customer_Invoice_Head_History alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INVOICE_HEAD_Delete_Data alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_CUSTOMER_INVOICE_HEAD_Cancel_Data alter column IRN_No VARCHAR(100) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_HEAD alter column tax1_rate Decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_HEAD alter column tax2_rate decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_HEAD alter column tax3_rate Decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_HEAD alter column tax4_rate decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_HEAD  alter column tax5_rate Decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_DETAIL alter column tax1_rate decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_DETAIL  alter column tax2_rate Decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_DETAIL alter column tax3_rate decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_DETAIL alter column tax4_rate Decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            qry = " alter table TSPL_SD_SALE_RETURN_DETAIL alter column tax5_rate decimal(18,4) null "
            ExecuteQeuryWithCatch(qry)

            ExecuteQeuryWithCatch("alter table TSPL_MF_RECEIPT_DETAIL alter column BOM_CODE Varchar(30)  NULL")
            ExecuteQeuryWithCatch("alter table TSPL_MF_RECEIPT_DETAIL alter column PRODUCTION_LINE_CODE VARCHAR(30)  NULL")
            ExecuteQeuryWithCatch("alter table TSPL_MF_RECEIPT_DETAIL alter column BATCH_QTY NUMERIC(18, 6)  NULL")
            ExecuteQeuryWithCatch("alter table TSPL_MF_RECEIPT_DETAIL alter column BALANCE_BATCH_QTY NUMERIC(18, 6)  NULL")

            ExecuteQeuryWithCatch("alter table TSPL_QC_Parameter_Detail alter column Param_Field_Value Varchar(500)  NULL")
            ExecuteQeuryWithCatch("alter table TSPL_QC_Parameter_Detail alter column Remarks Varchar(500)  NULL")
            ExecuteQeuryWithCatch("alter table TSPL_QC_Parameter_Detail_History alter column Param_Field_Value Varchar(500)  NULL")
            ExecuteQeuryWithCatch("alter table TSPL_QC_Parameter_Detail_History alter column Remarks Varchar(500)  NULL")

            qry = "alter table TSPL_SCRAPINVOICE_HEAD alter column Vehicle_code VARCHAR(30) NULL "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPINVOICE_HEAD_CANCEL alter column Vehicle_code VARCHAR(30) NULL "
            ExecuteQeuryWithCatch(qry)
            qry = "alter table TSPL_SCRAPINVOICE_HEAD_History alter column Vehicle_code VARCHAR(30) NULL "
            ExecuteQeuryWithCatch(qry)

            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX1_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX2_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX3_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX4_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX5_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX6_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX7_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX8_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX9_Rate decimal(18,3) null ")
            'ExecuteQeuryWithCatch("alter table TSPL_ITEM_PRICE_PLAN_DETAIL alter column TAX10_Rate decimal(18,3) null ")

            ExecuteQeuryWithCatch("alter table TSPL_OUTPUT_ENTRY alter column FatKG decimal(18,3) not null ")
            ExecuteQeuryWithCatch("alter table TSPL_OUTPUT_ENTRY alter column SNFKG decimal(18,3) not null ")

            ExecuteQeuryWithCatch("alter table TSPL_SECONDARY_CUSTOMER_MASTER alter column Add1 varchar(75) NOT NULL ")
            ExecuteQeuryWithCatch("alter table TSPL_SECONDARY_CUSTOMER_MASTER alter column Add2 varchar(75) NOT NULL ")
            ExecuteQeuryWithCatch("alter table TSPL_SECONDARY_CUSTOMER_MASTER alter column Add3 varchar(75) NOT NULL ")
            ExecuteQeuryWithCatch("ALTER TABLE TSPL_Milk_Quantity_Slab ALTER COLUMN min_range DECIMAL(18,2) ")
            ExecuteQeuryWithCatch("ALTER TABLE TSPL_Milk_Quantity_Slab ALTER COLUMN max_range DECIMAL(18,2) ")
            ExecuteQeuryWithCatch(" Alter table TSPL_MF_BATCH_PP_DETAIL  ALTER COLUMN PRODUCTION_LINE_CODE VARCHAR(30) NULL ")
            ExecuteQeuryWithCatch(" ALTER TABLE TSPL_MF_BATCH_ORDER_DETAIL ALTER COLUMN PRODUCTION_LINE_CODE VARCHAR(30) NULL ")

            ExecuteQeuryWithCatch(" alter table TSPL_SALSTRUCT_PAYHEADS alter column SUB_HEAD_TYPE varchar(30) ")
            ExecuteQeuryWithCatch(" alter table TSPL_SALARY_CALCULATION alter column SUB_HEAD_TYPE varchar(30) ")
            ExecuteQeuryWithCatch(" alter table TSPL_GENERATE_SALARY_PAYHEADS alter column SUB_HEAD_TYPE varchar(30) ")
            ExecuteQeuryWithCatch(" update TSPL_DOCPREFIX_MASTER set Doc_Trans_Type = 'MCC' where Doc_Type = 'MCC Master' and len (isnull(Doc_Trans_Type,'')) >=0 ")
            ExecuteQeuryWithCatch(" update TSPL_DOCPREFIX_MASTER set Doc_Trans_Type = 'Registered' where Doc_Type = 'VSP Master' and len (isnull(Doc_Trans_Type,'')) >=0 ")
            ExecuteQeuryWithCatch(" alter table tspl_milk_receipt_detail alter column vehicle_code varchar(30) ")
            ExecuteQeuryWithCatch(" alter table tspl_milk_srn_head alter column VEHICLE_CODE varchar(30) ")
            ExecuteQeuryWithCatch(" alter table TSPL_MILK_SRN_HEAD_SYNC alter column VEHICLE_CODE varchar(30) ")
            ExecuteQeuryWithCatch(" alter table TSPL_MILK_SRN_HEAD_Hist_Data alter column VEHICLE_CODE varchar(30) ")
            ExecuteQeuryWithCatch(" alter table TSPL_MILK_Shift_End_Route_DETAIL alter column VEHICLE_CODE varchar(30) ")
            ExecuteQeuryWithCatch(" alter table TSPL_MILK_Shift_End_Route_DETAIL_SYNC alter column VEHICLE_CODE varchar(30) ")
            qry = "DECLARE @ObjectName NVARCHAR(100)
                   SELECT @ObjectName = OBJECT_NAME([default_object_id]) FROM SYS.COLUMNS
                   WHERE [object_id] = OBJECT_ID('[TSPL_VENDOR_MASTER]') AND [name] = 'isOwnBMC';
                   EXEC('ALTER TABLE [TSPL_VENDOR_MASTER] DROP CONSTRAINT ' + @ObjectName)"
            ExecuteQeuryWithCatch(qry)

            qry = "ALTER TABLE TSPL_VENDOR_MASTER DROP COLUMN isOwnBMC"
            ExecuteQeuryWithCatch(qry)
            qry = "ALTER TABLE TSPL_VENDOR_MASTER DROP COLUMN MCCOwnBMC"
            ExecuteQeuryWithCatch(qry)
            ExecuteQeuryWithCatch(" alter table TSPL_PROFIT_AND_LOSS_PERFORMA alter column Formula varchar(150) ")
            ExecuteQeuryWithCatch(" alter table TSPL_DOCPREFIX_MASTER alter column Doc_Prfeix varchar(16) ")

            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Cast_Category_Code varchar(30) null")
            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Distict_Code varchar(30) null")
            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Block_Code varchar(30) null")
            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Revenue_Village_Code varchar(30) null")
            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Grampanchayat_Code varchar(30) null")
            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Panchayat_Samiti_Code varchar(30) null")
            'ExecuteQeuryWithCatch("alter table TSPL_CUSTOMER_MASTER alter column Vidhan_Sabha_Code varchar(30) null")
            'InsertStateOfIndia()
        Catch ex As Exception
        End Try
    End Sub

    Shared Sub ExecuteQeuryWithCatch(ByVal qry As String)
        Try
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow("Error in After Create All table ")
        End Try
    End Sub


    Public Shared Sub InvokeMethodSlow(AssemblyName As String, ClassName As String, MethodName As String, args As Object())
        Try
            Dim ass As Assembly = Assembly.LoadFrom(Application.StartupPath & "\" & AssemblyName)
            Dim FileAtt As String = IO.Path.GetFileNameWithoutExtension(AssemblyName)
            Dim factory As Object = ass.CreateInstance(FileAtt & "." & ClassName, True)
            Dim t As Type = factory.GetType
            Dim method As MethodInfo = t.GetMethod(MethodName)
            Dim obj As Object = method.Invoke(factory, args)
        Catch ex As Exception
        End Try


    End Sub

    Public Shared Sub InvokeMethodFromCurrentAssembly(ClassName As String, MethodName As String, args As Object())
        Try
            'clsCommon.MyMessageBoxShow("Hi")
            Dim ass As Assembly = Assembly.GetExecutingAssembly()
            Dim factory As Object = ass.CreateInstance(ClassName, True)
            Dim t As Type = factory.GetType
            Dim method As MethodInfo = t.GetMethod(MethodName)
            'Dim Mymethodbase As MethodBase = t.GetMethod(MethodName)
            'Dim Myarray As ParameterInfo() = Mymethodbase.GetParameters()
            'If Myarray.Length <> args.Length Then Exit Sub
            'Dim i As Integer = 0
            'If Myarray IsNot Nothing AndAlso Myarray.Length > 0 Then
            '    For Each Myparam As ParameterInfo In Myarray
            '        If TypeOf Myparam.ParameterType Is System.Int16 Then
            '            args(i) = clsCommon.myCdbl(args(i))
            '        End If

            '        i = i + 1
            '    Next
            'End If
            Dim obj As Object = method.Invoke(factory, args)
        Catch ex As Exception
        End Try


    End Sub


    Public Sub testMethod(a As Double, b As Double)
        '  clsCommon.MyMessageBoxShow("Hello!! This is Method Calling Example From Custom field Button")
        'Dim rString As List(Of String) = getControlsOnForm()
        'If rString IsNot Nothing AndAlso rString.Count > 0 Then
        '    clsCommon.MyMessageBoxShow("Control Name: " & rString(0) & ", Type:" & rString(1))
        'End If
        clsCommon.MyMessageBoxShow("Value Passed Are: " & a & " And " & b & " and Its Sum is : " & (a + b))
    End Sub

    Public Shared Function getControlsOnForm() As List(Of String)
        Dim dt As DataTable = Nothing
        Dim rString As New List(Of String)
        Dim ctr As New List(Of Control)
        Try
            'clsCommon.MyMessageBoxShow("Hi")
            Dim assName As String = Application.StartupPath & "\" & "ERP.EXE"
            Dim ClassName As String = "ERP.FrmMccDispatch"
            Dim FormName As String = clsCommon.myCstr(ClassName)
            Dim AsmName As String = clsCommon.myCstr(assName)
            Dim AsmToLoad As Assembly = Nothing
            Dim obj As Object = Nothing
            AsmToLoad = Assembly.LoadFile(AsmName)
            Dim classType As Type = AsmToLoad.[GetType](FormName)
            obj = AsmToLoad.CreateInstance(FormName, True)
            Dim frm As XpertERPEngine.FrmMainTranScreen = TryCast(obj, RadForm)
            findAndReturnContols(frm, ctr, Nothing)
            Dim qry As String = ""
            If ctr IsNot Nothing AndAlso ctr.Count > 0 Then
                For i As Integer = 0 To ctr.Count - 1
                    qry = qry & "select " & i + 1 & " as SLNO, '" & ctr(i).Name & "' as ControlName, '" & clsCommon.myCstr(ctr(i).GetType().Name) & "' as ControlType " & Environment.NewLine
                    If i < ctr.Count - 1 Then
                        qry = qry & " union all " & Environment.NewLine
                    End If
                Next
                Dim controlNum As Integer = clsCommon.myCdbl(clsCommon.ShowSelectForm("ControlList", qry, "SLNO", "", "", "", True)) - 1
                If controlNum >= 0 Then
                    Dim ControlName As String = ctr(controlNum).Name
                    Dim ControlType As String = ctr(controlNum).GetType().Name

                    rString.Add(ControlName)
                    rString.Add(ControlType)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return rString
    End Function

    Public Shared Sub getControlsOnForm(formId As String, ByRef gv As common.UserControls.MyRadGridView)
        Dim dt As DataTable = Nothing
        Dim rString As New List(Of String)
        Dim ctr As New List(Of Control)
        Try
            'clsCommon.MyMessageBoxShow("Hi")
            Dim formName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(MainFormName,'') as MainFormName from TSPL_PROGRAM_MASTER where program_code='" & formId & "'"))
            If clsCommon.myLen(formName) <= 0 Then
                Throw New Exception("Screen Not Mapped for Screen : " & formId)
            End If

            Dim asmnm As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(AsmName,'') as AsmName from TSPL_PROGRAM_MASTER where program_code='" & formId & "'"))
            If clsCommon.myLen(asmnm) <= 0 Then
                Throw New Exception("Assambly Not Mapped for Screen : " & formId)
            End If

            Dim assName As String = Application.StartupPath & "\" & asmnm
            Dim className As String = formName
            formName = "ERP." & formName

            Dim AsmName As String = clsCommon.myCstr(assName)
            Dim AsmToLoad As Assembly = Nothing
            Dim obj As Object = Nothing
            AsmToLoad = Assembly.LoadFile(AsmName)
            Dim classType As Type = AsmToLoad.[GetType](formName)
            obj = AsmToLoad.CreateInstance(formName, True)
            Dim frm As XpertERPEngine.FrmMainTranScreen = TryCast(obj, RadForm)
            findAndReturnContols(frm, ctr, Nothing)
            Dim qry As String = ""
            If ctr IsNot Nothing AndAlso ctr.Count > 0 Then
                For i As Integer = 0 To ctr.Count - 1
                    Dim desc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_SCREEN_CONTROL_MASTER where programCode='" & formId & "' and controlName='" & clsCommon.myCstr(ctr(i).Name) & "'"))
                    Dim TableName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TableName from TSPL_SCREEN_CONTROL_MASTER where programCode='" & formId & "' and controlName='" & clsCommon.myCstr(ctr(i).Name) & "'"))
                    Dim FieldName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select FieldName from TSPL_SCREEN_CONTROL_MASTER where programCode='" & formId & "' and controlName='" & clsCommon.myCstr(ctr(i).Name) & "'"))
                    Dim ProgramName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Program_Name from tspl_program_master where program_Code='" & formId & "'"))
                    qry = qry & "select " & i + 1 & " as SLNO, '" & formId & "' as ScreenCode, '" & className & "' as ScreenDesc,'" & ProgramName & "' as ProgramName, '" & ctr(i).Name & "' as ControlName, '" & clsCommon.myCstr(ctr(i).GetType().Name) & "' as ControlType, '" & IIf(clsCommon.myLen(desc) > 0, desc, "") & "' as Description,'" & TableName & "' as TableName,'" & FieldName & "' as FieldName " & Environment.NewLine
                    If i < ctr.Count - 1 Then
                        qry = qry & " union all " & Environment.NewLine
                    End If
                Next
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv.DataSource = dt
                    gv.Columns(0).ReadOnly = True
                    gv.Columns(1).ReadOnly = True
                    gv.Columns(2).ReadOnly = True
                    gv.Columns(3).ReadOnly = True
                    gv.Columns(2).IsVisible = False
                    gv.Columns(4).ReadOnly = True
                    gv.Columns(5).ReadOnly = True
                    gv.Columns("Description").ReadOnly = False
                    gv.Columns(6).ReadOnly = False
                    gv.Columns(7).ReadOnly = False
                    gv.BestFitColumns()
                    gv.EnableFiltering = True
                    gv.AllowAddNewRow = False

                Else
                    Throw New Exception("No Control Found")
                End If
                'Dim controlNum As Integer = clsCommon.myCdbl(clsCommon.ShowSelectForm("ControlList", qry, "SLNO", "", "", "", True)) - 1
                'If controlNum >= 0 Then
                '    Dim ControlName As String = ctr(controlNum).Name
                '    Dim ControlType As String = ctr(controlNum).GetType().Name

                '    rString.Add(ControlName)
                '    rString.Add(ControlType)
                'End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Shared Sub findAndReturnContols(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByRef ctr As List(Of Control), Optional ByVal contrl As Control = Nothing)

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    findAndReturnContols(formname, ctr, ctrl)
                End If
                If Not (TypeOf ctrl Is RadGroupBox OrElse TypeOf ctrl Is SplitContainer OrElse TypeOf ctrl Is RadPanel OrElse TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is common.UserControls.MyRadGridView) AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                    ctr.Add(ctrl)
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    findAndReturnContols(formname, ctr, ctrl)
                End If
                If Not (TypeOf ctrl Is RadGroupBox OrElse TypeOf ctrl Is SplitContainer OrElse TypeOf ctrl Is RadPanel OrElse TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is common.UserControls.MyRadGridView) AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                    ctr.Add(ctrl)
                End If
            Next
        End If
    End Sub

    Public Shared Sub FindAnyCntrolByFieldName(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByRef ctr As Control, ctrName As String, Optional ByVal contrl As Control = Nothing)

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    FindAnyCntrolByFieldName(formname, ctr, ctrName, ctrl)
                End If
                If TypeOf ctrl Is MyNumBox Then
                    If clsCommon.CompairString(TryCast(ctrl, MyNumBox).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyTextBox Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyTextBox).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.UserControls.txtFinder Then
                    If clsCommon.CompairString(TryCast(ctrl, common.UserControls.txtFinder).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.UserControls.txtNavigator Then
                    If clsCommon.CompairString(TryCast(ctrl, common.UserControls.txtNavigator).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyDateTimePicker Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyDateTimePicker).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyComboBox Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyComboBox).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyLabel Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyLabel).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    FindAnyCntrolByFieldName(formname, ctr, ctrName, ctrl)
                End If
                If TypeOf ctrl Is MyNumBox Then
                    If clsCommon.CompairString(TryCast(ctrl, MyNumBox).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyTextBox Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyTextBox).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.UserControls.txtFinder Then
                    If clsCommon.CompairString(TryCast(ctrl, common.UserControls.txtFinder).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.UserControls.txtNavigator Then
                    If clsCommon.CompairString(TryCast(ctrl, common.UserControls.txtNavigator).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyDateTimePicker Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyDateTimePicker).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyComboBox Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyComboBox).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyLabel Then
                    If clsCommon.CompairString(TryCast(ctrl, common.Controls.MyLabel).FieldName, ctrName) = CompairStringResult.Equal Then
                        ctr = ctrl
                    End If
                End If
            Next
        End If
    End Sub

    'Public Shared Sub FindAnyCntrolByFieldName(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByRef ctr As Control, ctrName As String, Optional ByVal contrl As Control = Nothing)

    '    If IsNothing(contrl) Then
    '        For Each ctrl As Control In formname.Controls
    '            If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
    '                FindAnyCntrolByFieldName(formname, ctr, ctrName, ctrl)
    '            End If
    '            If TypeOf ctrl Is MyNumBox Then
    '                If clsCommon.CompairString(TryCast(ctr, MyNumBox).FieldName, ctrName) = CompairStringResult.Equal Then
    '                    ctr = ctrl
    '                End If
    '            End If
    '        Next
    '    Else
    '        For Each ctrl As Control In contrl.Controls
    '            If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
    '                FindAnyCntrolByFieldName(formname, ctr, ctrName, ctrl)
    '            End If
    '            If TypeOf ctrl Is MyNumBox Then
    '                If clsCommon.CompairString(TryCast(ctr, MyNumBox).FieldName, ctrName) = CompairStringResult.Equal Then
    '                    ctr = ctrl
    '                End If
    '            End If
    '        Next
    '    End If
    'End Sub

    Public Shared Function FindControlAtPoint(container As Control, pos As Point) As Control
        Dim child As Control
        For Each c As Control In container.Controls
            If c.Visible AndAlso c.Bounds.Contains(pos) Then
                child = FindControlAtPoint(c, New Point(pos.X - c.Left, pos.Y - c.Top))
                If child Is Nothing Then
                    Return c
                Else
                    Return child
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function FindControlAtCursor(form As FrmMainTranScreen) As Control
        Dim pos As Point = Cursor.Position
        If form.Bounds.Contains(pos) Then
            Return FindControlAtPoint(form, form.PointToClient(Cursor.Position))
        End If
        Return Nothing
    End Function
    'Public Shared Sub InsertStateOfIndia()
    '    Try

    '        Dim qry As String = "  select * from ( " & _
    '                        "  select 'AP' as   STATE_CODE , 'Andhra Pradesh' as  STATE_NAME , '37' as  GST_STATE_Code  " & _
    '                        "  union all " & _
    '                        "  select 'AR' as   STATE_CODE , 'Arunachal Pradesh' as  STATE_NAME , '12' as  GST_STATE_Code  " & _
    '                        "  union all    " & _
    '                        "  select 'AS' as   STATE_CODE , 'Assam' as  STATE_NAME , '18' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'BR' as   STATE_CODE , 'Bihar' as  STATE_NAME , '10' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'CG' as   STATE_CODE , 'Chhattisgarh' as  STATE_NAME , '22' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'GA' as   STATE_CODE , 'Goa' as  STATE_NAME , '30' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'GJ' as   STATE_CODE , 'Gujarat' as  STATE_NAME , '24' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'HR' as   STATE_CODE , 'Haryana' as  STATE_NAME , '06' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'HP' as   STATE_CODE , 'Himachal Pradesh' as  STATE_NAME , '02' as  GST_STATE_Code   " & _
    '                        "  union all    " & _
    '                        "  select 'JK' as   STATE_CODE , 'Jammu and Kashmir' as  STATE_NAME , '01' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'JH' as   STATE_CODE , 'Jharkhand' as  STATE_NAME , '20' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'KA' as   STATE_CODE , 'Karnataka' as  STATE_NAME , '29' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'KL' as   STATE_CODE , 'Kerala' as  STATE_NAME , '32' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'MP' as   STATE_CODE , 'Madhya Pradesh' as  STATE_NAME , '23' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'MH' as   STATE_CODE , 'Maharashtra' as  STATE_NAME , '27' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'MN' as   STATE_CODE , 'Manipur' as  STATE_NAME , '14' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'ML' as   STATE_CODE , 'Meghalaya' as  STATE_NAME , '17' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'MZ' as   STATE_CODE , 'Mizoram' as  STATE_NAME , '15' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'NL' as   STATE_CODE , 'Nagaland' as  STATE_NAME , '13' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'OR' as   STATE_CODE , 'Orissa' as  STATE_NAME , '21' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'PB' as   STATE_CODE , 'Punjab' as  STATE_NAME , '03' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'RJ' as   STATE_CODE , 'Rajasthan' as  STATE_NAME , '08' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'SK' as   STATE_CODE , 'Sikkim' as  STATE_NAME , '11' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'TR' as   STATE_CODE , 'Tripura' as  STATE_NAME , '16' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'UK' as   STATE_CODE , 'Uttarakhand' as  STATE_NAME , '05' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'UP' as   STATE_CODE , 'Uttar Pradesh' as  STATE_NAME , '09' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'WB' as   STATE_CODE , 'West Bengal' as  STATE_NAME , '19' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'TN' as   STATE_CODE , 'Tamil Nadu' as  STATE_NAME , '33' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'TS' as   STATE_CODE , 'TELANGANA' as  STATE_NAME , '36' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'AN' as   STATE_CODE , 'Andaman and Nicobar Islands' as  STATE_NAME , '35' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'CH' as   STATE_CODE , 'Chandigarh' as  STATE_NAME , '04' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'DH' as   STATE_CODE , 'Dadra and Nagar Haveli' as  STATE_NAME , '26' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'DD' as   STATE_CODE , 'Daman and Diu' as  STATE_NAME , '25' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'DL' as   STATE_CODE , 'Delhi' as  STATE_NAME , '07' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'LD' as   STATE_CODE , 'Lakshadweep' as  STATE_NAME , '31' as  GST_STATE_Code   " & _
    '                        "  union all   " & _
    '                        "  select 'PY' as   STATE_CODE , 'Pondicherry' as  STATE_NAME , '34' as  GST_STATE_Code   " & _
    '                        "  ) Final order by Final.GST_STATE_Code asc  "

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        Dim strStateCode As String = Nothing
    '        Dim strStateName As String = Nothing
    '        Dim strGstStateCode As String = Nothing
    '        Dim strCountryCode As String = "INDIA"
    '        Dim chkCountry As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_COUNTRY_MASTER where Country_code = '" + strCountryCode + "'"))
    '        If chkCountry = False Then
    '            clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_COUNTRY_MASTER (COUNTRY_CODE,COUNTRY_NAME,Created_By,Created_Date,Modified_By,Modified_Date)values ('INDIA','INDIA','Admin','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "','Admin','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "') ")
    '        End If

    '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '            For Each row As DataRow In dt.Rows
    '                strStateCode = row.Item("STATE_CODE")
    '                strStateName = row.Item("STATE_NAME")
    '                strGstStateCode = row.Item("GST_STATE_Code")

    '                Dim isStateCodeExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from tspl_state_Master where state_Code ='" + strStateCode + "' "))
    '                If isStateCodeExist = False Then
    '                    clsDBFuncationality.ExecuteNonQuery("insert into tspl_state_Master (STATE_CODE,STATE_NAME,COUNTRY_CODE,Created_By,Created_Date,Modified_By,Modified_Date,GST_STATE_Code) values ('" + strStateCode + "','" + strStateName + "','" + strCountryCode + "','Admin', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', 'Admin','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "','" + strGstStateCode + "')")
    '                Else
    '                    clsDBFuncationality.ExecuteNonQuery("update tspl_state_Master set STATE_NAME = '" + strStateName + "' , GST_STATE_Code = '" + strGstStateCode + "' where STATE_CODE = '" + strStateCode + "'")
    '                End If
    '            Next

    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub
End Class
