Imports common
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class FrmCompany
    Public isOperationSucced As Boolean = False
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Try
            If AllowToSave() Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", txtCompanyCode.Text)
                    clsCommon.AddColumnsForChange(coll, "Comp_Name", txtCompanyName.Text)
                    clsCommon.AddColumnsForChange(coll, "Created_By", "Admin")
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", "Admin")
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code1", txtCompanyCode.Text)
                    clsCommon.AddColumnsForChange(coll, "IntegrateWithTally", 0)
                    clsCommon.AddColumnsForChange(coll, "ApplyMultiCurrency", 0)
                    clsCommon.AddColumnsForChange(coll, "BaseCurrencyCode", fndBaseCurrency.Value)
                    clsCommon.AddColumnsForChange(coll, "Is_Main_Company", 1)
                    clsCommon.AddColumnsForChange(coll, "DataBase_Name", objCommonVar.CurrDatabase)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COMPANY_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    objCommonVar.CurrentCompanyCode = txtCompanyCode.Text

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "User_Code", "Admin")
                    clsCommon.AddColumnsForChange(coll, "User_Name", "Administrator")
                    clsCommon.AddColumnsForChange(coll, "Password", txtPassword.Text)
                    clsCommon.AddColumnsForChange(coll, "User_Type", "Admin") '
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", "Admin")
                    clsCommon.AddColumnsForChange(coll, "Emp_Name", "Admin")
                    clsCommon.AddColumnsForChange(coll, "Created_By", "Admin")
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", "Admin")
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", txtCompanyCode.Text)
                    clsCommon.AddColumnsForChange(coll, "Level", 0)
                    clsCommon.AddColumnsForChange(coll, "ApprovalLevel", 0)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    objCommonVar.CurrentUserCode = "Admin"

                    Dim ObjList As New List(Of clsModuleScreenHead)
                    Dim obj As clsModuleScreenHead = Nothing
                    If rbtnGeneral.IsChecked Then
                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleBI
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleCommonServices
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleExportSale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleFavourite
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleFixedAsset
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleGL
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleHR
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleHumanResource
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleMaterial
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleMISReports
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModulePayable
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModulePurchase
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleQualityControl
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleReceivable
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModulesalePurchaseSecurity
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleSalesNew
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleProductionSTD
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleSystemAdmin
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleTDS
                        ObjList.Add(obj)
                    ElseIf rbtnDairy.IsChecked Then
                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleBulkSale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleBI
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleCommonServices
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleCSASale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleProductionDairy
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleExportSale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleFavourite
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleFixedAsset
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleFreshSale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleGL
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleHR
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleHumanResource
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleMaterial
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleMerchantTradeSale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleProductionDairy
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleMCCMilkProcurement
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleMISReports
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModulePayable
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleProductSale
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleProjectManagement
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModulePurchase
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleReceivable
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModulesalePurchaseSecurity
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleProductionSTD
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleSystemAdmin
                        ObjList.Add(obj)

                        obj = New clsModuleScreenHead()
                        obj.IsAvailable = True
                        obj.Module_Name = clsUserMgtCode.ModuleTDS
                        ObjList.Add(obj)
                    End If
                    clsModuleScreenDetails.SaveData(ObjList, trans)
                    trans.Commit()

                    InsertDefaultGLSourceCode()
                    InsertDefaultInvenotrySourceCode()

                    isOperationSucced = True
                    Me.Close()
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
                objCommonVar.CurrentCompanyCode = txtCompanyCode.Text
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCompanyCode.Text) <= 0 Then
            txtCompanyCode.Focus()
            Throw New Exception("Please Enter your comapny sort name")
        End If
        If clsCommon.myLen(txtCompanyName.Text) <= 0 Then
            txtCompanyName.Focus()
            Throw New Exception("Please Enter your comapny name")
        End If
        If clsCommon.myLen(txtPassword.Text) <= 0 Then
            txtPassword.Focus()
            Throw New Exception("Please Enter password")
        End If
        If clsCommon.myLen(txtPasswordConfirm.Text) <= 0 Then
            txtPasswordConfirm.Focus()
            Throw New Exception("Please Enter Confirm password")
        End If
        If Not clsCommon.CompairString(txtPassword.Text, txtPasswordConfirm.Text) = CompairStringResult.Equal Then
            Throw New Exception("Password and confirm password should be same")
        End If
        If clsCommon.myLen(fndBaseCurrency.Value) <= 0 Then
            fndBaseCurrency.Focus()
            Throw New Exception("Please Enter base currency")
        End If
        clsCommon.MyMessageBoxShow(Me, "Your login ID is " + MyLabel6.Text, Me.Text, MessageBoxButtons.OK)
        Return True
    End Function

    Private Sub fndBaseCurrency__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBaseCurrency._MYValidating
        fndBaseCurrency.Value = clsCurrencyMaster.getFinder("", fndBaseCurrency.Value, isButtonClicked)
    End Sub

    Private Sub FrmCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_CURRENCY_MASTER")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                InsertDefaultCurrenry()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub InsertDefaultCurrenry()
        Dim obj As clsCurrencyMaster = New clsCurrencyMaster()
        Try
            obj = New clsCurrencyMaster()
            obj.Code = "INR"
            obj.Name = "INR"
            obj.Description = "Indian Ruppes"
            obj.CURRENCY_SIGN = "Rs"
            obj.SaveData(obj, True)
        Catch ex As Exception

        End Try


        Try
            obj = New clsCurrencyMaster()
            obj.Code = "USD"
            obj.Name = "USD"
            obj.Description = "US Dollar"
            obj.CURRENCY_SIGN = "$"
            obj.SaveData(obj, True)
        Catch ex As Exception

        End Try


        Try
            obj = New clsCurrencyMaster()
            obj.Code = "EURO"
            obj.Name = "EURO"
            obj.Description = "EURO"
            obj.CURRENCY_SIGN = "€"
            obj.SaveData(obj, True)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub InsertDefaultGLSourceCode()
        Dim obj As clsGLSourceCode = New clsGLSourceCode()
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AP-AD"
            obj.SourceLedger = "AP"
            obj.SourceType = "AD"
            obj.SourceDescription = "AP ADJUSTEMENT"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AP-CN"
            obj.SourceLedger = "AP"
            obj.SourceType = "CN"
            obj.SourceDescription = "Vendors Credit Notes"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AP-DN"
            obj.SourceLedger = "AP"
            obj.SourceType = "DN"
            obj.SourceDescription = "DEBIT NOTE"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AP-IN"
            obj.SourceLedger = "AP"
            obj.SourceType = "IN"
            obj.SourceDescription = "Vendor Invoices"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AP-MI"
            obj.SourceLedger = "AP"
            obj.SourceType = "MI"
            obj.SourceDescription = "MISC. Payments"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AP-PY"
            obj.SourceLedger = "AP"
            obj.SourceType = "PY"
            obj.SourceDescription = "Payment Entry"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-AD"
            obj.SourceLedger = "AR"
            obj.SourceType = "AD"
            obj.SourceDescription = "AR ADJUSTMENT"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-CR"
            obj.SourceLedger = "AR"
            obj.SourceType = "CR"
            obj.SourceDescription = "Product Sale Return"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-DC"
            obj.SourceLedger = "AR"
            obj.SourceType = "DC"
            obj.SourceDescription = "Receipt Entry"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-DN"
            obj.SourceLedger = "AR"
            obj.SourceType = "DN"
            obj.SourceDescription = "VCGL entry"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-IN"
            obj.SourceLedger = "AR"
            obj.SourceType = "IN"
            obj.SourceDescription = "AR INVOICE"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-MI"
            obj.SourceLedger = "AR"
            obj.SourceType = "MI"
            obj.SourceDescription = "ARMI"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-OA"
            obj.SourceLedger = "AR"
            obj.SourceType = "OA"
            obj.SourceDescription = "Onaccount Receipt"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-PI"
            obj.SourceLedger = "AR"
            obj.SourceType = "PI"
            obj.SourceDescription = "Receivable"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-PY"
            obj.SourceLedger = "AR"
            obj.SourceType = "PY"
            obj.SourceDescription = "RECEIPT ENTRY"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-RF"
            obj.SourceLedger = "AR"
            obj.SourceType = "RF"
            obj.SourceDescription = "Receipt Refund"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "AR-SI"
            obj.SourceLedger = "AR"
            obj.SourceType = "SI"
            obj.SourceDescription = "SALE RETURN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "BK-TF"
            obj.SourceLedger = "BK"
            obj.SourceType = "TF"
            obj.SourceDescription = "Contra Vouchers"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "BM-PI"
            obj.SourceLedger = "BM"
            obj.SourceType = "PI"
            obj.SourceDescription = "BULK MILK PURCHASE INVOICE"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "BM-SR"
            obj.SourceLedger = "BM"
            obj.SourceType = "SR"
            obj.SourceDescription = "BULK MILK SRN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "bm-tr"
            obj.SourceLedger = "bm"
            obj.SourceType = "tr"
            obj.SourceDescription = ""
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "CS-RC"
            obj.SourceLedger = "CS"
            obj.SourceType = "RC"
            obj.SourceDescription = "CSA TRANSFER RETURN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "CS-TR"
            obj.SourceLedger = "CS"
            obj.SourceType = "TR"
            obj.SourceDescription = "CSA Transfer"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "DF-FS"
            obj.SourceLedger = "DF"
            obj.SourceType = "FS"
            obj.SourceDescription = "DISPATCH FRESH SALE"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "DI-CH"
            obj.SourceLedger = "DI"
            obj.SourceType = "CH"
            obj.SourceDescription = "MCC Dispatch"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "DS-BS"
            obj.SourceLedger = "DS"
            obj.SourceType = "BS"
            obj.SourceDescription = "DISPATCH BULK SALE"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "ds-bt"
            obj.SourceLedger = "ds"
            obj.SourceType = "bt"
            obj.SourceDescription = ""
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "DS-FS"
            obj.SourceLedger = "DS"
            obj.SourceType = "FS"
            obj.SourceDescription = "FRESH DISPATCH"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "GL-JE"
            obj.SourceLedger = "GL"
            obj.SourceType = "JE"
            obj.SourceDescription = "GENERAL ENTRY"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "IC-AD"
            obj.SourceLedger = "IC"
            obj.SourceType = "AD"
            obj.SourceDescription = "ADJUSTMENT"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "MI-SR"
            obj.SourceLedger = "MI"
            obj.SourceType = "SR"
            obj.SourceDescription = "Milk SRN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "MM-TF"
            obj.SourceLedger = "MM"
            obj.SourceType = "TF"
            obj.SourceDescription = "Stock Transfer"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "MT-IN"
            obj.SourceLedger = "MT"
            obj.SourceType = "IN"
            obj.SourceDescription = "MILK IN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "PI-CM"
            obj.SourceLedger = "PI"
            obj.SourceType = "CM"
            obj.SourceDescription = "PICM"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "PO-RC"
            obj.SourceLedger = "PO"
            obj.SourceType = "RC"
            obj.SourceDescription = "SRN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "PO-RT"
            obj.SourceLedger = "PO"
            obj.SourceType = "RT"
            obj.SourceDescription = "PURCHASE RETURN"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "PS-SH"
            obj.SourceLedger = "PS"
            obj.SourceType = "SH"
            obj.SourceDescription = "Inventory Source code"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "PU-IS"
            obj.SourceLedger = "PU"
            obj.SourceType = "IS"
            obj.SourceDescription = "Material Issue"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "PU-RE"
            obj.SourceLedger = "PU"
            obj.SourceType = "RE"
            obj.SourceDescription = "Material Return"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "RG-JW"
            obj.SourceLedger = "RG"
            obj.SourceType = "JW"
            obj.SourceDescription = "RG JW"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "RM-JW"
            obj.SourceLedger = "RM"
            obj.SourceType = "JW"
            obj.SourceDescription = "RM JOB WORK"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "RV-TA"
            obj.SourceLedger = "RV"
            obj.SourceType = "TA"
            obj.SourceDescription = "Bank/Cash Reverse"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "SD-SH	SD	SH	DISPATCH"
            obj.SourceLedger = ""
            obj.SourceType = ""
            obj.SourceDescription = ""
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "SN-RT"
            obj.SourceLedger = "SN"
            obj.SourceType = "RT"
            obj.SourceDescription = "SRN Return"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "sr-rg	sr"
            obj.SourceLedger = "sr"
            obj.SourceType = "rg"
            obj.SourceDescription = "Source for Store Received Note"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "sr-rt"
            obj.SourceLedger = "sr"
            obj.SourceType = "rt"
            obj.SourceDescription = ""
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
        Try
            obj = New clsGLSourceCode()
            obj.SourceCode = "VC-GL"
            obj.SourceLedger = "VC"
            obj.SourceType = "GL"
            obj.SourceDescription = "VCGL"
            obj.SaveData(obj, True)
        Catch ex As Exception
        End Try
         

    End Sub

    Public Sub InsertDefaultInvenotrySourceCode()
        Dim obj As clsInventorySourceCode = New clsInventorySourceCode()
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "BulkSRN"
            obj.Name = "Contractor Milk"
            obj.InOutType = "In"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try

        Try
            obj = New clsInventorySourceCode()
            obj.Code = "BulkSRNRet"
            obj.Name = "Bulk SRN Return"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try

        Try
            obj = New clsInventorySourceCode()
            obj.Code = "BulkSRNTrade"
            obj.Name = "BulkSRNTrade"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "DispatchBS"
            obj.Name = "Bulk Sale Dispatch"
            obj.InOutType = "Out"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "DispatchBSTrade"
            obj.Name = "Bulk Sale Dispatch Trade"
            obj.InOutType = "Out"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "DispChallan"
            obj.Name = "MCC Tanker Dispatch"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "FS-SH"
            obj.Name = "Fresh Dispatch"
            obj.InOutType = "Out"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "FS-SR"
            obj.Name = "Fresh Sale Return"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "IC-AD"
            obj.Name = "Stock Adjustment"
            obj.InOutType = "All"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "ISSTRAN"
            obj.Name = "Issue/Return"
            obj.InOutType = "All"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MCC-AISSUE"
            obj.Name = "MCC Asset Issue"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MCC-IISSUE"
            obj.Name = "MCC Item Issue"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MCC-MSALE"
            obj.Name = "MCC Sale"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MCC-MSR"
            obj.Name = "MCC SALE RETURN"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MCC-MSRN"
            obj.Name = "MCC Milk SRN"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MilkTransferIn"
            obj.Name = "MCC Milk In"
            obj.InOutType = "In"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "MT_SALE_IN"
            obj.Name = "MERCHANT SALE"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "NRGP"
            obj.Name = "NRGP"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "PP_ISSUE"
            obj.Name = "Production Issue"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "PP_STDN"
            obj.Name = "Standardization"
            obj.InOutType = "All"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "PRD_STG_PROC"
            obj.Name = "Stage Process"
            obj.InOutType = "All"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "PROD_ENTRY"
            obj.Name = "Production Entry"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "PS-SH"
            obj.Name = "Product Dispatch"
            obj.InOutType = "Out"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "PS-SR"
            obj.Name = "Product Sale Return"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "Purchase Return"
            obj.Name = "Purchase Return"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "RGP"
            obj.Name = "RGP/NRGP"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SALE RETURN"
            obj.Name = "SALE RETURN"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SALERETURNBS"
            obj.Name = "Sales Return - Bulk Sale"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "ScrapIn"
            obj.Name = "Misc Sale"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SD-CSATRANS"
            obj.Name = "CSA Stock Transfer"
            obj.InOutType = "Out"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SD-CSATRANS-RETURN"
            obj.Name = "CSA Transfer Return"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SD-SH"
            obj.Name = "Dispatch"
            obj.InOutType = "Out"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SRN"
            obj.Name = "General Stock IN"
            obj.InOutType = "In"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "SRN-RET"
            obj.Name = "SRN Return"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "Transfer"
            obj.Name = "Branch Transfer"
            obj.InOutType = "All"
            obj.Type = "MIS"
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
        Try
            obj = New clsInventorySourceCode()
            obj.Code = "VSPTRAN"
            obj.Name = "VSP Asset Issue"
            obj.InOutType = "Out"
            obj.Type = ""
            clsInventorySourceCode.SaveData(obj, True)
        Catch ex As Exception

        End Try
         


         
    End Sub
End Class
