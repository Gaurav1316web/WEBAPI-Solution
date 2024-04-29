Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine


Public Class frmBullCurlingEntry
    Inherits FrmMainTranScreen

    #Region "Variables"
    Dim isNewEntry As Boolean = True

    Const ColBullCode As String = "ColCode"
    Const ColSpeciesCode As String = "ColSpeciesCode"
    Const ColCategoryCode As String = "ColCategoryCode"
    Const ColSubCategoryCode As String = "ColSubCategoryCode"
    Const ColSSCenterCode As String = "ColSSCenterCode"
    Const ColBreed As String = "ColBreed"
    Const ColShedCode As String = "ColShedCode"
    Const ColPenCode As String = "ColPenCode"
    Const ColStatusCode As String = "ColStatusCode"
    Const ColSubStatusCode As String = "ColSubStatusCode"
    Const ColBullImported As String = "ColBullImported"
    Const ColExoticBlood As String = "ColExoticBlood"
    Const ColBullBookValue As String = "ColBullBookValue"
    Const ColRegistrationDate As String = "ColRegistrationDate"
    Const ColBullID As String = "ColBullID"
    Const ColPrevBullID As String = "ColPrevBullID"
    Const ColDateOfBirth As String = "ColDateOfBirth"
    Const ColSSBullID As String = "ColSSBullID"
    Const ColBullaliasName As String = "ColBullName"
    Const ColSSCenter As String = "ColSSCenter"
    Const ColAmount As String = "ColAmount"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = True
    Dim ErrorControl As New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmDairyGatePass
        MyBase.SetUserMgmt(clsUserMgtCode.frmDairyGatePass)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
    End Sub
    Private Sub frmBullCurlingEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Document_No", "VARCHAR(30) NOT NULL PRIMARY KEY")
        'coll.Add("Document_Date", "Datetime NOT NULL")
        'coll.Add("Remarks", "VARCHAR(200) NULL")
        'coll.Add("Status", "integer NULL")
        'coll.Add("Created_By", "varchar(20) NOT NULL")
        'coll.Add("Created_Date", "Datetime NOT NULL")
        'coll.Add("Modified_By", "varchar(20) NOT NULL")
        'coll.Add("Modified_Date", "Datetime NOT NULL")
        'coll.Add("Posted_By", "varchar(20)  NULL ")
        'coll.Add("Posted_Date", "Datetime  NULL")
        ''clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULL_CURLING", coll)
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULL_CURLING", coll, Nothing, False, False, "", "Document_No", "")

        'coll = New Dictionary(Of String, String)()
        'coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION")
        'coll.Add("Document_No", "VARCHAR(30) NULL REFERENCES TSPL_BULL_CURLING(Document_No) ")
        'coll.Add("Bull_ID", "VARCHAR(30) NULL REFERENCES TSPL_BULL_MASTER(Bull_Code) ")
        'coll.Add("Amount", "Decimal (18,2) Null")
        '' clsCommonFunctionality.CreateOrAlterTable("TSPL_BULL_CURLING_Detail", coll)
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULL_CURLING_Detail", coll, Nothing, False, False, "TSPL_BULL_CURLING", "Document_No", "")

        AddNew()
        'dtpDate.Value = clsCommon.GETSERVERDATE()
        'loadBlankGrid()
    End Sub

    Sub loadBlankGrid()
        Try
            Dim qry As String = String.Empty

            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim gridcolCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            Dim gridcolName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            Dim gridcoltype As GridViewTextBoxColumn = New GridViewTextBoxColumn()

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull ID"
            gridcolName.Name = ColBullID
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = False
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Species Code"
            gridcolName.Name = ColSpeciesCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Category Code"
            gridcolName.Name = ColCategoryCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Sub Category Code"
            gridcolName.Name = ColSubCategoryCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "SS Center"
            gridcolName.Name = ColSSCenterCode
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Breed"
            gridcolName.Name = ColBreed
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Shed Code"
            gridcolName.Name = ColShedCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Pen Code"
            gridcolName.Name = ColPenCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Status Code"
            gridcolName.Name = ColStatusCode
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Sub Status Code"
            gridcolName.Name = ColSubStatusCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull Imported"
            gridcolName.Name = ColBullImported
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Exotic Blood"
            gridcolName.Name = ColExoticBlood
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull Book Value"
            gridcolName.Name = ColBullBookValue
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Registration Date"
            gridcolName.Name = ColRegistrationDate
            gridcolName.Width = 90
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Prev Bull ID"
            gridcolName.Name = ColPrevBullID
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Date Of Birth"
            gridcolName.Name = ColDateOfBirth
            gridcolName.Width = 90
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "SS Bull ID"
            gridcolName.Name = ColSSBullID
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull Name"
            gridcolName.Name = ColBullaliasName
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "SS Center"
            gridcolName.Name = ColSSCenter
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Amount"
            gridcolName.Name = ColAmount
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = False
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = True
            gv1.AllowRowReorder = False
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = False
            gv1.EnableSorting = False
            gv1.EnableGrouping = False
            gv1.AllowColumnChooser = True
            gv1.AllowColumnReorder = True
            gv1.Rows.AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        fndCode.Value = Nothing
        txtremarks.Text = ""
        dtpDate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        loadBlankGrid()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btnsave.Text = "Save"
        btndelete.Enabled = True
        btnPost.Enabled = True
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_CURLING where Document_No='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(fndCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try

            Dim qry As String = ""
            qry = "select Document_No as Code,Remarks as [Remarks],Document_Date as Date from TSPL_BULL_CURLING"
            fndCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", "", fndCode.Value, " Code asc", isButtonClicked, Nothing)
            LoadData(fndCode.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Function SaveData() As Boolean

        Dim obj As New ClsBullCurlingEntry()
        obj.Code = fndCode.Value
        obj.Remarks = txtremarks.Text
        obj.Doc_Date = dtpDate.Value

        obj.Arr = New List(Of ClsBullCurlingEntryDeatil)

        For Each row As GridViewRowInfo In gv1.Rows
            Dim objTr As New ClsBullCurlingEntryDeatil()

            objTr.BullID = clsCommon.myCstr(row.Cells(ColBullID).Value)
            objTr.Amount = clsCommon.myCdbl(row.Cells(ColAmount).Value)

            If (clsCommon.myLen(objTr.BullID) > 0) Then
                obj.Arr.Add(objTr)
            End If
        Next

        Dim Sqlqry As String = "select count(1) from TSPL_BULL_CURLING where Document_No ='" + fndCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            isNewEntry = True
        Else
            isNewEntry = False
        End If

        If (ClsBullCurlingEntry.SaveData(obj, isNewEntry)) Then
            clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
            LoadData(obj.Code, NavigatorType.Current)
        End If

        Return True
    End Function

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (ClsBullCurlingEntry.DeleteData(fndCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If clsCommon.myLen(fndCode.Value) > 0 Then
            PostData(fndCode.Value)
        Else
        End If
    End Sub

    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + fndCode.Value + "]" + Environment.NewLine + "Are You Sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                ClsBullCurlingEntry.PostData(clsCommon.myCstr(fndCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(fndCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If isCellValueChangedOpen Then
                    isCellValueChangedOpen = False

                    If e.Column Is gv1.Columns(ColBullID) Then
                        OpenBullList(False)
                    End If
                    isCellValueChangedOpen = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenBullList(ByVal isButtonClick As Boolean)
        Try
            Dim whrls As String = Nothing
            Dim check As List(Of String) = New List(Of String)
            If gv1.Rows.Count > 1 Then
                For Each row In gv1.Rows
                    check.Add(clsCommon.myCstr(row.Cells("ColBullID").Value))
                Next
            End If
            If check IsNot Nothing AndAlso check.Count > 0 Then
                whrls = "  Bull_Code not in (" + clsCommon.GetMulcallString(check) + ")"
            End If

            Dim qry As String = " select Bull_Code,Species_Code,Category_Code,Sub_Category_Code,SS_Centre_Code,Breed_Code,Shed_Code,Pen_Code,Status_Code,Sub_Status_Code,
                                  Exotic_Blood_Per,Bull_Book_Value,CONVERT(varchar, Registration_Date, 103) as Registration_Date,Prev_Bull_Id,CONVERT(varchar, Date_Of_Birth, 103) as Date_Of_Birth,SS_Bull_Id,Bull_Alia_Name from TSPL_BULL_MASTER "
            gv1.CurrentRow.Cells(ColBullID).Value = clsCommon.ShowSelectForm("ItemFnder@PriceMstr", qry, "Bull_Code", whrls, clsCommon.myCstr(gv1.CurrentRow.Cells(ColBullID).Value), "", isButtonClick)
            Dim whrcls As String = " where Bull_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColBullID).Value) + "'"
            qry = " select Bull_Code,Species_Code,Category_Code,Sub_Category_Code,SS_Centre_Code,Breed_Code,Shed_Code,Pen_Code,Status_Code,Sub_Status_Code,
                    Exotic_Blood_Per,Bull_Book_Value,CONVERT(varchar, Registration_Date, 103) as Registration_Date,Prev_Bull_Id,CONVERT(varchar, Date_Of_Birth, 103) as Date_Of_Birth,SS_Bull_Id,Bull_Alia_Name from TSPL_BULL_MASTER " + whrcls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(ColBullID).Value = clsCommon.myCstr(dt.Rows(0)("Bull_Code"))
                gv1.CurrentRow.Cells(ColSpeciesCode).Value = clsCommon.myCstr(dt.Rows(0)("Species_Code"))
                gv1.CurrentRow.Cells(ColCategoryCode).Value = clsCommon.myCstr(dt.Rows(0)("Category_Code"))
                gv1.CurrentRow.Cells(ColSubCategoryCode).Value = clsCommon.myCstr(dt.Rows(0)("Sub_Category_Code"))
                gv1.CurrentRow.Cells(ColSSCenterCode).Value = clsCommon.myCstr(dt.Rows(0)("SS_Centre_Code"))
                gv1.CurrentRow.Cells(ColBreed).Value = clsCommon.myCstr(dt.Rows(0)("Breed_Code"))
                gv1.CurrentRow.Cells(ColShedCode).Value = clsCommon.myCstr(dt.Rows(0)("Shed_Code"))
                gv1.CurrentRow.Cells(ColPenCode).Value = clsCommon.myCstr(dt.Rows(0)("Pen_Code"))
                gv1.CurrentRow.Cells(ColStatusCode).Value = clsCommon.myCstr(dt.Rows(0)("Status_Code"))
                gv1.CurrentRow.Cells(ColSubStatusCode).Value = clsCommon.myCstr(dt.Rows(0)("Sub_Status_Code"))
                'gv1.CurrentRow.Cells(ColBullImported).Value = clsCommon.myCstr(dt.Rows(0)("Bull_Imported"))
                gv1.CurrentRow.Cells(ColExoticBlood).Value = clsCommon.myCstr(dt.Rows(0)("Exotic_Blood_Per"))
                gv1.CurrentRow.Cells(ColBullBookValue).Value = clsCommon.myCstr(dt.Rows(0)("Bull_Book_Value"))
                gv1.CurrentRow.Cells(ColRegistrationDate).Value = clsCommon.myCstr(dt.Rows(0)("Registration_Date"))
                gv1.CurrentRow.Cells(ColPrevBullID).Value = clsCommon.myCstr(dt.Rows(0)("Prev_Bull_Id"))
                gv1.CurrentRow.Cells(ColDateOfBirth).Value = clsCommon.myCstr(dt.Rows(0)("Date_Of_Birth"))
                gv1.CurrentRow.Cells(ColSSBullID).Value = clsCommon.myCstr(dt.Rows(0)("SS_Bull_Id"))
                gv1.CurrentRow.Cells(ColBullaliasName).Value = clsCommon.myCstr(dt.Rows(0)("Bull_Alia_Name"))
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            'loadBlankGrid()
            gv1.DataSource = Nothing
            gv1.Refresh()
            isInsideLoadData = True
            fndCode.MyReadOnly = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim obj As ClsBullCurlingEntry = ClsBullCurlingEntry.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                fndCode.Value = obj.Code
                txtremarks.Text = obj.Remarks
                dtpDate.Value = obj.Doc_Date
                fndCode.MyReadOnly = True
                If obj.Arr IsNot Nothing Then
                    For Each objrow As ClsBullCurlingEntryDeatil In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBullID).Value = objrow.BullID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSpeciesCode).Value = objrow.Species_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCategoryCode).Value = objrow.Category_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSubCategoryCode).Value = objrow.Sub_Category_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSSCenterCode).Value = objrow.SS_Centre_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBreed).Value = objrow.Breed_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColShedCode).Value = objrow.Shed_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPenCode).Value = objrow.Pen_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStatusCode).Value = objrow.Status_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSubStatusCode).Value = objrow.Sub_Status_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBullImported).Value = objrow.Bull_Imported
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColExoticBlood).Value = objrow.Exotic_Blood_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBullBookValue).Value = objrow.Bull_Book_Value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRegistrationDate).Value = objrow.Registration_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPrevBullID).Value = objrow.Prev_Bull_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDateOfBirth).Value = objrow.Date_Of_Birth
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSSBullID).Value = objrow.SS_Bull_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBullaliasName).Value = objrow.Bull_Alia_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = objrow.Amount

                        gv1.Rows.AddNew()
                    Next
                End If
                If clsCommon.myCdbl(ERPTransactionStatus.Approved) = clsCommon.myCdbl(obj.Status) Then
                    UsLock1.Status = obj.Status
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                ElseIf ERPTransactionStatus.Pending = obj.Status Then
                    UsLock1.Status = obj.Status
                    btnsave.Enabled = True
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                End If
                isInsideLoadData = False

                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim Amount As New GridViewSummaryItem(ColAmount, "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(Amount)
            Else
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Public Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        fndCode.MyReadOnly = False
        fndCode.Value = Nothing
        dtpDate.Value = clsCommon.GETSERVERDATE()
        txtremarks.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = True
        loadBlankGrid()
    End Sub

    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFocus()
        'If gv1.CurrentCell IsNot Nothing Then
        '    Dim setnxtRow As Boolean = False
        '    If gv1.CurrentCell.ColumnInfo.Name = ColBullID Then
        '        gv1.CurrentColumn = gv1.Columns(ColAmount)
        '    ElseIf (gv1.CurrentCell.ColumnInfo.Name = ColAmount) Then
        '        setnxtRow = True
        '        gv1.CurrentColumn = gv1.Columns(ColBullID)
        '    End If
        '    If setnxtRow Then
        '        gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
        '        gv1.CurrentColumn = gv1.Columns(ColBullID)
        '    End If
        'End If
        If gv1.CurrentCell IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            Dim setnxtRow As Boolean = False
            If gv1.CurrentCell.ColumnInfo.Name = ColBullID Then
                gv1.CurrentColumn = gv1.Columns(ColAmount)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = ColAmount Then
                setnxtRow = True
                gv1.CurrentColumn = gv1.Columns(ColBullID)
            End If

            If setnxtRow Then
                Dim nextRowIndex As Integer = gv1.CurrentRow.Index + 1
                If nextRowIndex < gv1.Rows.Count Then
                    gv1.CurrentRow = gv1.Rows(nextRowIndex)
                    gv1.CurrentColumn = gv1.Columns(ColBullID)
                End If
            End If
        End If

    End Sub
End Class