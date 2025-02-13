Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmUpdateAppLocation
    Inherits FrmMainTranScreen
    Const ColCode As String = "ColCode"
    Private isInsideLoadData As Boolean = False

    Const colLocationName As String = "Location Name"
    Const colDataBaseName As String = "DataBase Name"
    Const colCustomerCode As String = "Customer Code"
    Const colCustomerName As String = "Customer Name"
    Const colCustomerAccountNo As String = "Customer Account No"
    Const colSchedulerApplySMS As String = "Scheduler Apply SMS"
    Const colSchedulerApplyEMail As String = "Scheduler Apply EMail"
    Const colApplyPDAccount As String = "Apply PD Account"
    Const colApplyECollect As String = "Apply ECollect"

    Private Sub frmUpdateAppLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formLoad()
    End Sub
    Sub formLoad()
        Dim dt As DataTable = Nothing
        Dim qry As String = "select Code, *  from TSPL_MASTER.dbo.TSPL_APP_LOCATION "
        dt = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                'View()
                ' SetGridFormation()
                'ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                'RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        End If
    End Sub

    Sub LoadBlankGridColmns()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        'S.NO column'
        Dim gridColCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColCode.FormatString = ""
        gridColCode.HeaderText = "Code"
        gridColCode.Name = ColCode
        gridColCode.Width = 105
        gridColCode.ReadOnly = False
        gridColCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(gridColCode)


        'Dim gridColRelation As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        'gridColRelation.FormatString = ""
        'gridColRelation.HeaderText = "Relation"
        'gridColRelation.Name = ColRelation
        'gridColRelation.FormatString = ""
        ''gridColRelation.DecimalPlaces = 0
        'gridColRelation.Width = 52
        'gridColRelation.ReadOnly = False

        'gv1.MasterTemplate.Columns.Add(gridColRelation)

        ''Code column'
        'Dim gridColTaqNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridColTaqNo.FormatString = ""
        'gridColTaqNo.HeaderText = "Taq No"
        'gridColTaqNo.Name = ColTaqNo
        'gridColTaqNo.Width = 105
        'gridColTaqNo.ReadOnly = False

        'gv1.MasterTemplate.Columns.Add(gridColTaqNo)

        ''Item Name column'
        'Dim gridColFirstStandardLocationYield As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridColFirstStandardLocationYield.FormatString = ""
        'gridColFirstStandardLocationYield.HeaderText = "First Standard Location Yield"
        'gridColFirstStandardLocationYield.Name = ColFirstStandardLocationYield
        'gridColFirstStandardLocationYield.Width = 105
        'gridColFirstStandardLocationYield.ReadOnly = False
        'gv1.MasterTemplate.Columns.Add(gridColFirstStandardLocationYield)

        ''Unit column'
        'Dim gridColFatPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        'gridColFatPercent.FormatString = ""
        'gridColFatPercent.HeaderText = "Fat Percent"
        'gridColFatPercent.Name = ColFatPercent
        'gridColFatPercent.Width = 105
        'gridColFatPercent.ReadOnly = False
        'gv1.MasterTemplate.Columns.Add(gridColFatPercent)


        ''Quantity column'
        'Dim gridColSnfPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        'gridColSnfPercent.FormatString = ""
        'gridColSnfPercent.HeaderText = "Snf Percent"
        'gridColSnfPercent.Name = ColSnfPercent
        'gridColSnfPercent.Width = 105
        'gridColSnfPercent.Minimum = 0
        'gridColSnfPercent.ReadOnly = False
        'gridColSnfPercent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(gridColSnfPercent)

        ''Rate column'
        'Dim gridColProtien As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridColProtien.FormatString = ""
        'gridColProtien.HeaderText = "Protien"
        'gridColProtien.Name = ColProtien
        'gridColProtien.ReadOnly = False
        'gridColProtien.Width = 105
        'gv1.MasterTemplate.Columns.Add(gridColProtien)

        ''Amount column'
        'Dim gridColLactose As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridColLactose.FormatString = ""
        'gridColLactose.HeaderText = "Lactose"
        'gridColLactose.Name = ColLactose
        'gridColLactose.Width = 105
        'gridColLactose.ReadOnly = False
        'gv1.MasterTemplate.Columns.Add(gridColLactose)


        'Dim gridColDateOfPedigreeInformationUpdate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'gridColDateOfPedigreeInformationUpdate.FormatString = "{0:dd/MM/yyyy}"
        'gridColDateOfPedigreeInformationUpdate.HeaderText = "Date Of Pedigree Information Update"
        'gridColDateOfPedigreeInformationUpdate.Name = ColDateOfPedigreeInformationUpdate
        'gridColDateOfPedigreeInformationUpdate.ReadOnly = False
        'gridColDateOfPedigreeInformationUpdate.Width = 90
        'gv1.MasterTemplate.Columns.Add(gridColDateOfPedigreeInformationUpdate)

        ''Tax after amount column'

        'Dim gridColSCC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridColSCC.FormatString = ""
        'gridColSCC.HeaderText = "SCC"
        'gridColSCC.Name = ColSCC
        'gridColSCC.ReadOnly = False
        'gridColSCC.Width = 105
        'gv1.MasterTemplate.Columns.Add(gridColSCC)

        ''Amount after tax column'

        'Dim gridColMUN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridColMUN.FormatString = ""
        'gridColMUN.HeaderText = "MUN"
        'gridColMUN.Name = ColMUN
        'gridColMUN.ReadOnly = False
        'gridColMUN.Width = 135
        'gv1.MasterTemplate.Columns.Add(gridColMUN)
        'gv1.Rows.AddNew()
        'gv1.AllowAddNewRow = False

        ''Discount percent
        'Dim gridBestStandardLocationYield As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'gridBestStandardLocationYield.FormatString = ""
        'gridBestStandardLocationYield.HeaderText = "Best Standard Location Yield"
        'gridBestStandardLocationYield.Name = ColBestStandardLocationYield
        'gridBestStandardLocationYield.ReadOnly = False
        'gridBestStandardLocationYield.Width = 90
        'gv1.MasterTemplate.Columns.Add(gridBestStandardLocationYield)

        ''Discount Amount column'

        'Dim gridLactNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        'gridLactNo.FormatString = ""
        'gridLactNo.HeaderText = "LactNo"
        'gridLactNo.Name = ColLactNo
        'gridLactNo.ReadOnly = False
        'gridLactNo.Width = 125
        'gv1.MasterTemplate.Columns.Add(gridLactNo)

        'Dim gridFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'gridFATKG.FormatString = ""
        'gridFATKG.Name = ColFATINKG
        'gridFATKG.HeaderText = "FAT IN KG"
        'gridFATKG.ReadOnly = False
        'gridFATKG.Width = 135
        'gv1.MasterTemplate.Columns.Add(gridFATKG)

        'Dim gridSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'gridSNFKG.FormatString = ""
        'gridSNFKG.Name = ColSNFINKG
        'gridSNFKG.HeaderText = "SNF IN KG"
        'gridSNFKG.ReadOnly = False
        'gridSNFKG.Width = 135
        'gv1.MasterTemplate.Columns.Add(gridSNFKG)


        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        'gv1.Rows.AddNew()
        'ReStoreGridLayout()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub


    ' Helper function to check if a row is modified (implement your logic here)


    Sub SaveData()
        Try

            'If (AllowToSave()) Then
            Dim obj As New ClsUpdateAppLocation()

            'If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
            'obj.ITEM_CODE = fndvendor.Value
            Dim Arr As New List(Of ClsUpdateAppLocation)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New ClsUpdateAppLocation()
                objTr.Code = clsCommon.myCstr(grow.Cells(ColCode).Value)
                objTr.Location_Name = clsCommon.myCstr(grow.Cells(colLocationName).Value)
                objTr.DataBase_Name = clsCommon.myCstr(grow.Cells(colDataBaseName).Value)
                objTr.Customer_Code = clsCommon.myCstr(grow.Cells(colCustomerCode).Value)
                objTr.Customer_Name = clsCommon.myCdbl(grow.Cells(colCustomerName).Value)
                objTr.Customer_Account_No = clsCommon.myCdbl(grow.Cells(colCustomerAccountNo).Value)
                objTr.Scheduler_Apply_SMS = clsCommon.myCstr(grow.Cells(colSchedulerApplySMS).Value)
                objTr.Scheduler_Apply_EMail = clsCommon.myCstr(grow.Cells(colSchedulerApplyEMail).Value)
                objTr.Apply_PD_Account = clsCommon.myCstr(grow.Cells(colApplyPDAccount).Value)
                objTr.Apply_ECollect = clsCommon.myCstr(grow.Cells(colApplyECollect).Value)

                If (clsCommon.myLen(objTr.Code) > 0) Then
                    Arr.Add(objTr)
                End If
            Next


            If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                Return
            End If

            'Dim objHist As New ClsAlternateitemDetailHistory()
            'objHist.SaveDataHistory(fndvendor.Value)
            If (obj.SaveData(obj.Code, Arr)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                btnsave.Text = "Update"
                'LoadData(obj.vendor_code, NavigatorType.Current)
            End If

            ' End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal vendorcode As String, ByVal Desc As String)
        Try
            btnsave.Enabled = True
            btnsave.Text = "Save"

            isInsideLoadData = True

            'btnsave.Text = "Update"

            'funreset()
            'LoadBlankGrid()

            Dim Arr As List(Of ClsUpdateAppLocation) = ClsUpdateAppLocation.GetData(vendorcode)
            'fndvendor.Value = vendorcode
            '.Text = Desc
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As ClsUpdateAppLocation In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCode).Value = objTr.Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.Location_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDataBaseName).Value = objTr.DataBase_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerCode).Value = objTr.Customer_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerName).Value = objTr.Customer_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerAccountNo).Value = objTr.Customer_Account_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchedulerApplySMS).Value = objTr.Scheduler_Apply_SMS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchedulerApplyEMail).Value = objTr.Scheduler_Apply_EMail
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyPDAccount).Value = objTr.Apply_PD_Account
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyECollect).Value = objTr.Apply_ECollect
                    btnsave.Text = "Update"
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
End Class