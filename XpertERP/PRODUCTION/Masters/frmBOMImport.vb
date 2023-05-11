'--18/09/2013--form Added By- Panch Raj-------- Ticket no :BM00000000478---------
Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmBOMImport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim OpenFileDialog1 As New OpenFileDialog
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Save()
    End Sub

    Private Sub Save()
        If AllowToSave() Then

            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            If Me.cboBOMStatus.Text = "BOM Head" Then
                ImportBOMHead(gv)
            ElseIf Me.cboBOMStatus.Text = "BOM Components" Then
                ImportBOMComponents(gv)
            End If
        End If
    End Sub
    Function ImportBOMHead(ByVal gv As RadGridView) As Boolean
        If transportSql.importExcel(gv, "BOM Code", "Description", "BOM Date", "Revision No", "Start Date", "End Date", "Status", "Default", "Main Item Code", "Build Quantity", "Unit Code", "Min Batch Size") Then
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                Dim isNewEntry As Boolean = False
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsBillOfMaterial()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("BOM Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("BOM Code can not be blank or incorrect.")
                    End If
                    obj.BOM_CODE = strCode
                    Dim strq As String = "SELECT BOM_CODE FROM TSPL_MF_BOM_HEAD WHERE BOM_CODE='" & strCode & "'"
                    Dim dt As DataTable
                    dt = clsDBFuncationality.GetDataTable(strq)
                    If dt.Rows.Count > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If

                    Dim description As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If description.Length > 200 Or (String.IsNullOrEmpty(description)) Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.DESCRIPTION = description

                    Dim bom_date As String = clsCommon.myCDate(grow.Cells("BOM Date").Value)
                    If bom_date.Length = 0 Then
                        Throw New Exception("BOM Date can not be blank or incorrect.")
                    End If
                    obj.BOM_DATE = bom_date
                    Dim Bom_Field As String

                    Bom_Field = clsCommon.myCstr(grow.Cells("Revision No").Value)
                    If Bom_Field.Length > 50 Then
                        Throw New Exception("REVISION NO can not be blank or incorrect.")
                    End If
                    obj.REVISION_NO = Bom_Field

                    Dim start_date As Date
                    start_date = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    If clsCommon.myLen(start_date.ToString) = 0 Then
                        Throw New Exception("Satrt Date can not be blank or incorrect.")
                    End If
                    obj.START_DATE = start_date

                    Dim end_date As Date? = Nothing
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("End Date").Value)) = 0 Then
                        end_date = Nothing
                    Else
                        end_date = clsCommon.myCDate(grow.Cells("End Date").Value)
                    End If

                    obj.END_DATE = end_date


                    Bom_Field = clsCommon.myCstr(grow.Cells("Status").Value)
                    If Bom_Field.Length > 30 Then
                        Throw New Exception("Status can not be blank or incorrect.")
                    End If
                    obj.STATUS = Bom_Field

                    Bom_Field = clsCommon.myCstr(grow.Cells("Default").Value)
                    If Bom_Field <> "Yes" And Bom_Field <> "No" Then
                        Throw New Exception("IS_DEFAULT Must be Yes or No.")
                    End If
                    If Bom_Field = "Yes" Then
                        obj.IS_DEFAULT = 1
                    ElseIf Bom_Field = "No" Then
                        obj.IS_DEFAULT = 0
                    End If



                    Bom_Field = clsCommon.myCstr(grow.Cells("Main Item Code").Value)
                    If Bom_Field.Length > 50 Or Bom_Field.Length = 0 Then
                        Throw New Exception("Main Item Code can not be blank or incorrect.")
                    End If
                    obj.PROD_ITEM_CODE = Bom_Field

                    Dim PROD_QUANTITY As Double = 0
                    PROD_QUANTITY = clsCommon.myCdbl(grow.Cells("Build QUANTITY").Value)
                    If PROD_QUANTITY <= 0 Then
                        Throw New Exception("PROD_QUANTITY(Build Qty) can not be blank or incorrect.")
                    End If
                    obj.PROD_QUANTITY = PROD_QUANTITY

                    Bom_Field = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If Bom_Field.Length > 12 Then
                        Throw New Exception("Unit Code can not be blank or incorrect.")
                    End If
                    obj.PROD_ITEM_UNIT_CODE = Bom_Field



                    Dim MIN_BATCH_SIZE As Double = 0
                    MIN_BATCH_SIZE = clsCommon.myCdbl(grow.Cells("Min Batch Size").Value)
                    If MIN_BATCH_SIZE <= 0 Then
                        Throw New Exception("Min Batch Size can not be blank or incorrect.")
                    End If
                    obj.MIN_BATCH_SIZE = MIN_BATCH_SIZE

                    obj.SaveData(obj, Nothing, isNewEntry, obj.BOM_CODE)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Function

    Function ImportBOMComponents(ByVal gv As RadGridView) As Boolean

        If transportSql.importExcel(gv, "BOM Code", "Line No", "Production Category", "Item Code", "Item Desc", "Quantity", "Unit Code", "Scrap Percent", "Wastage Percent", "Remarks", "Revision No") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()

                Dim obj As New clsBOMDetail
                Dim obj1 As New clsBillOfMaterial
                Dim ObjList As New List(Of clsBillOfMaterial)


                For Each grow As GridViewRowInfo In gv.Rows
                    obj1 = New clsBillOfMaterial

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("BOM Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("BOM Code can not be blank or incorrect.")
                    End If
                    obj1.BOM_CODE = strCode

                    Dim Line_No As Integer = clsCommon.myCdbl(grow.Cells("Line No").Value)
                    If Line_No = 0 Then
                        Throw New Exception("Line No can not be blank or incorrect.")
                    End If
                    obj1.Line_No = Line_No
                    Dim BOM_Field As String = ""
                    BOM_Field = clsCommon.myCstr(grow.Cells("Production Category").Value)
                    If BOM_Field.Length > 30 Then
                        Throw New Exception("Production Category can not be blank or incorrect.")
                    End If
                    obj1.CONSM_ITEM_CATEGORY_CODE = BOM_Field

                    BOM_Field = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If BOM_Field.Length > 50 Or BOM_Field.Length = 0 Then
                        Throw New Exception("Item Code can not be blank or incorrect.")
                    End If
                    obj1.CONSM_ITEM_CODE = BOM_Field

                    BOM_Field = clsCommon.myCstr(grow.Cells("Item Desc").Value)

                    obj1.ITEM_DESCRIPTION = BOM_Field

                    Dim CONSM_QUANTITY As Double = 0
                    CONSM_QUANTITY = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                    If CONSM_QUANTITY = 0 Then
                        Throw New Exception("Quantity can not be blank or incorrect.")
                    End If
                    obj1.CONSM_QUANTITY = CONSM_QUANTITY

                    BOM_Field = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If BOM_Field.Length = 0 Or BOM_Field.Length > 12 Then
                        Throw New Exception("Unit Code can not be blank or incorrect.")
                    End If
                    obj1.CONSM_ITEM_UNIT_CODE = BOM_Field

                    Dim SCRAP_PERCENT As Double = 0
                    SCRAP_PERCENT = clsCommon.myCdbl(grow.Cells("Scrap Percent").Value)
                    obj1.SCRAP_PERCENT = SCRAP_PERCENT

                    Dim WASTAGE_PERCENT As Double = 0
                    WASTAGE_PERCENT = clsCommon.myCdbl(grow.Cells("Wastage Percent").Value)
                    obj1.WASTAGE_PERCENT = WASTAGE_PERCENT

                    BOM_Field = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    obj1.REMARKS = BOM_Field

                    BOM_Field = clsCommon.myCstr(grow.Cells("Revision No").Value)
                    If BOM_Field.Length > 50 Then
                        Throw New Exception("REVISION NO can not be blank or incorrect.")
                    End If
                    obj1.REVISION_NO = BOM_Field


                    ObjList.Add(obj1)

                Next
                clsBOMDetail.SaveDataImport(ObjList, trans)

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Function


    Function AllowToSave() As Boolean
        If clsCommon.myLen(cboBOMStatus.Text) <= 0 Then
            myMessages.blankValue("BOM Transaction Type !")
            cboBOMStatus.Focus()
            Return False

            'ElseIf clsCommon.myLen(OpenFileDialog1.FileName) = 0 Then
            '    myMessages.blankValue("File to Import !")
            '    Return False
        End If

        Return True
    End Function





    Private Sub frmBOMImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnImport, "Press Alt+S for Import ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnExport, "Press Alt+D  for Export ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBOMImport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnImport.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnExport.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True

        Me.cboBOMStatus.SelectedIndex = -1
        txtDescription.Text = ""
        btnImport.Text = "Import"
        btnImport.Enabled = True
        btnExport.Enabled = True
        Me.txtDocPath.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub


    Private Sub frmBOMImport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnImport.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnExport.Enabled Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            OpenFileDialog1.ShowDialog()
            txtDocPath.Text = OpenFileDialog1.SafeFileName
            Me.txtDocPath.ReadOnly = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If AllowToSave() Then
            Dim str As String = ""
            If Me.cboBOMStatus.Text = "BOM Head" Then
                str = "SELECT BOM_CODE AS  [BOM Code],DESCRIPTION AS [Description],BOM_DATE as [BOM Date],REVISION_NO AS [Revision No],START_DATE as [Start Date],END_DATE as [End Date],STATUS as [Status],(CASE WHEN IS_DEFAULT=1 THEN 'Yes' else 'No' END) AS [Default] ,PROD_ITEM_CODE as [Main Item Code],PROD_QUANTITY as [Build Quantity],PROD_ITEM_UNIT_CODE as [Unit Code],MIN_BATCH_SIZE as [Min Batch Size] FROM TSPL_MF_BOM_HEAD"
            ElseIf Me.cboBOMStatus.Text = "BOM Components" Then
                str = "SELECT BOM_CODE AS [BOM Code],LINE_NO as [Line No],CONSM_ITEM_CATEGORY_CODE as [Production Category],CONSM_ITEM_CODE as [Item Code],ITEM_DESCRIPTION as [Item Desc],CONSM_QUANTITY as [Quantity],CONSM_ITEM_UNIT_CODE as [Unit Code]," _
                      & " SCRAP_PERCENT as [Scrap Percent],WASTAGE_PERCENT as [Wastage Percent],REMARKS as [Remarks],REVISION_NO as [Revision No] FROM TSPL_MF_BOM_DETAIL"
            End If
            transportSql.ExporttoExcel(str, Me)
        End If
    End Sub
End Class
