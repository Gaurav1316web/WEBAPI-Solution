Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports common

'Start date =18/5/2011
'End date =20/5/2011
'Last modify date = 30/5/2011
'Database =TSPLERP
' Tables=Tspl_visi_master
''--Updation by --[Pankaj kumar Chaudhary]--Against Ticket No --[]
'--14/12/2012-4:15PM---Updation by --[Pankaj kumar]--Made Visi Make Uneditabe-----fwd by--Ranjana mam
'--preeti gupta-ticket no.[BM00000003133]
Public Class frmvisimaster
    Inherits FrmMainTranScreen
    Dim arrDBName As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Dim dr As DataTable
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim IsNewEntry As Boolean = True
    'This Cunstructer is used to send usercode and compcode data in table.
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Public Sub SetLength()
        fndid.MyMaxLength = 12
        txtAssetNo.MaxLength = 50
        txtcustname.MaxLength = 100
        txtVisiSize.MaxLength = 50
        rdtxtmake.MaxLength = 50
        txtModelNo.MaxLength = 12
        txtSerialNo.MaxLength = 50
        txtTagNo.MaxLength = 50
    End Sub
    Private Sub frmvisimaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetLength()
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")
        rdtxtmake.ReadOnly = True

        SetDataBaseGrid()
        SetUserMgmtNew()
        text_changed()
        fndid_Leave()
        fndid_Leave1()
        cust_txtchanged()
        custid_leave()
        fndid.Value = ""
        rdtxtmake.Enabled = True
        rdbtnsave.Enabled = True
        rdbtndelete.Enabled = False
        rdbtnclose.Enabled = True
        fndid.BackColor = Color.White
        fndid.TabIndex = 0
        rdtxtmake.TabIndex = 2
        fndcustomerid.Enabled = False
        txtAssetNo.Enabled = False
        txtcustname.Enabled = False
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.visiMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub


    'This function is used to retrive data from table in all controls.
    Public Sub FunFill()
        Try
            Dim obj As New clsVisiMaster
            obj = clsVisiMaster.GetData(fndid.Value, NavigatorType.Current)
            If obj IsNot Nothing Then
                fndid.Value = obj.Visi_Id
                rdtxtmake.Text = obj.VisiMake
                txtAssetNo.Text = obj.Asset_No
                txtModelNo.Text = obj.Model_No
                txtVisiSize.Text = obj.Visi_Size
                fndcustomerid.Value = obj.Customer_Id
                txtSerialNo.Text = obj.Serial_No
                txtTagNo.Text = obj.Tag_No
                txtcustname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Id + "'"))
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'This functiion is used to reset all controls.
    Public Sub funreset()
        IsNewEntry = True
        fndid.Enabled = True
        fndid.MyReadOnly = False
        fndid.Value = ""
        fndcustomerid.Value = ""
        txtcustname.Text = ""
        rdbtnsave.Text = "Save"
        rdtxtmake.Enabled = True
        rdtxtmake.Text = ""
        rdbtnsave.Enabled = True
        rdbtndelete.Enabled = False
        rdbtnclose.Enabled = True
        txtcustname.Enabled = False
        txtAssetNo.Text = ""
        txtVisiSize.Text = ""
        txtModelNo.Text = ""
        txtSerialNo.Text = ""
        txtTagNo.Text = ""
    End Sub

    Sub text_changed()
        Dim str As String = "Select visi_id from TSPL_VISI_MASTER where visi_id ='" + fndid.Value + "'"
        dr = clsDBFuncationality.GetDataTable(str)
        Dim value As String = String.Empty
        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            value = dr.Rows(0)(0).ToString()
        End If
        If (value <> "") Then
            funfill()
            rdbtnsave.Text = "Update"
            rdtxtmake.Enabled = False
            rdbtndelete.Enabled = True
            rdbtnsave.Enabled = True
            IsNewEntry = False
        Else
            rdbtnsave.Text = "Save"
            rdtxtmake.Enabled = True
            rdbtndelete.Enabled = False
            rdbtnsave.Enabled = True
            IsNewEntry = True
        End If

    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        If fndid.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Visi Id does not exist")
            Return False
        ElseIf (rdtxtmake.Text = "") Then
            myMessages.blankValue(Me, "Visi Make", Me.Text)
            Return False
        ElseIf clsCommon.myLen(txtAssetNo.Text) > 0 Then    ' CHecks Unique Asset No
            Dim VisiId As String = clsDBFuncationality.getSingleValue("Select Visi_Id from TSPL_VISI_MASTER where Asset_No='" + txtAssetNo.Text + "' AND Visi_Id <>'" + fndid.Value + "'")
            If clsCommon.myLen(VisiId) > 0 Then
                common.clsCommon.MyMessageBoxShow("Sorry! This Asset No is alrady Exists Against Visi '" + VisiId + "'")
                txtAssetNo.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub SaveData()
        Try
            If AllowToSave() Then
                GetReplecateCompaniesDataBase()    '--This Function Fills Database Array
                Dim Arr As New List(Of clsVisiMaster)
                Dim obj As New clsVisiMaster()
                obj.Visi_Id = clsCommon.myCstr(fndid.Value)
                obj.VisiMake = clsCommon.myCstr(rdtxtmake.Text)
                obj.Visi_Size = clsCommon.myCstr(txtVisiSize.Text)
                obj.Asset_No = clsCommon.myCstr(txtAssetNo.Text)
                obj.Model_No = clsCommon.myCstr(txtModelNo.Text)
                obj.Customer_Id = clsCommon.myCstr(fndcustomerid.Value)
                obj.Serial_No = clsCommon.myCstr(txtSerialNo.Text)
                obj.Tag_No = clsCommon.myCstr(txtTagNo.Text)
                Arr.Add(obj)
                If (clsVisiMaster.SaveData(Arr, arrDBName, IsNewEntry)) Then
                    myMessages.insert()
                    text_changed() ' Function Is Used For Retreiving data
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If myMessages.deleteConfirm() Then
                If clsVisiMaster.DeleteData(fndid.Value) Then
                    myMessages.delete()
                    rdbtnsave.Text = "Save"
                    rdbtndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Sub fndid_Leave()
        If fndid.Value <> "" Then
            rdtxtmake.Enabled = True
            rdbtnsave.Enabled = True
        End If
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        funreset()
    End Sub

    
    Private Sub fndid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        Try
            Dim query As String = "Select visi_id as 'Visi Id',visimake as 'Visi Make', Asset_No as 'Asset No', Visi_Size as 'Visi Size', Model_No as 'Model No',Serial_No as 'Serial No',Tag_No as 'Tag No' from tspl_visi_master "
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Visi Id", "Visi Make", "Asset No", "Visi Size", "Model No", "Serial No", "Tag No") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    Dim coll As New Hashtable()
                    Dim LineNo As String = clsCommon.myCstr(dgrv.Index + 2)

                    '--------------------Visi Id-----------------------
                    Dim strVisiId As String = clsCommon.myCstr(dgrv.Cells("Visi Id").Value)
                    If clsCommon.myLen(strVisiId) > 0 Then
                        strVisiId = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Segment_code from TSPL_GL_SEGMENT_CODE Where Seg_No=6 AND Segment_code='" + strVisiId + "'", trans))
                        If clsCommon.myLen(strVisiId) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Visi_Id", strVisiId)
                        Else
                            Throw New Exception("The Visi Id '" + clsCommon.myCstr(dgrv.Cells("Visi Id").Value) + "' At Line No '" + LineNo + "' Does Not Exist in Sagement Master")
                        End If
                    Else
                        Throw New Exception("Enter Visi Id At Line No '" + LineNo + "'")
                    End If

                    '-----------------Visi Make-------------------------
                    Dim strvisimake As String = clsCommon.myCstr(dgrv.Cells("Visi Make").Value)
                    If clsCommon.myLen(strvisimake) > 0 AndAlso clsCommon.myLen(strvisimake) <= 50 Then
                        clsCommon.AddColumnsForChange(coll, "VisiMake", strvisimake)
                    Else
                        Throw New Exception("Please Enter Visi Make of Length [1 To 50] At Line No '" + LineNo + "'")
                    End If

                    '-----------------Asset No-------------------------
                    Dim strAssetNo As String = clsCommon.myCstr(clsCommon.myCstr(dgrv.Cells("Asset No").Value))
                    If clsCommon.myLen(strAssetNo) > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(clsCommon.myCstr(dgrv.Cells("Asset No").Value))) > 50 Then
                            common.clsCommon.MyMessageBoxShow("Sorry! The Asset No '" + strAssetNo + "' at line No '" + LineNo + "' can be of maximum Length 50 ")
                            Exit Sub
                        End If
                        Dim VisiId As String = clsDBFuncationality.getSingleValue("Select Visi_Id from TSPL_VISI_MASTER where Asset_No='" + strAssetNo + "' AND Visi_Id <>'" + strVisiId + "'", trans)
                        If clsCommon.myLen(VisiId) > 0 Then
                            common.clsCommon.MyMessageBoxShow("Sorry! The Asset No '" + strAssetNo + "' at line No '" + LineNo + "' is alrady Exists Against Visi '" + VisiId + "' ")
                            Exit Sub
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Asset_No", strAssetNo)

                    '-----------------Visi Size-------------------------
                    Dim strVisiSize As String = clsCommon.myCstr(clsCommon.myCstr(dgrv.Cells("Visi Size").Value))
                    If clsCommon.myLen(strVisiSize) > 50 Then
                        common.clsCommon.MyMessageBoxShow("Sorry! The Visi Size '" + strVisiSize + "' at line No '" + LineNo + "' can be of maximum Length 50 ")
                        Exit Sub
                    End If
                    clsCommon.AddColumnsForChange(coll, "Visi_Size", strVisiSize)

                    '-----------------Model No-------------------------
                    Dim strModelNo As String = clsCommon.myCstr(clsCommon.myCstr(dgrv.Cells("Model No").Value))
                    If clsCommon.myLen(strModelNo) > 12 Then
                        common.clsCommon.MyMessageBoxShow("Sorry! The Model No '" + strModelNo + "' at line No '" + LineNo + "' can be of maximum Length 12 ")
                        Exit Sub
                    End If
                    clsCommon.AddColumnsForChange(coll, "Model_No", strModelNo)

                    '-----------------Serial No-------------------------
                    Dim strSerial As String = clsCommon.myCstr(clsCommon.myCstr(dgrv.Cells("Serial No").Value))
                    If clsCommon.myLen(strSerial) > 50 Then
                        common.clsCommon.MyMessageBoxShow("Sorry! The Serial No Size '" + strVisiSize + "' at line No '" + LineNo + "' can be of maximum Length 50 ")
                        Exit Sub
                    End If
                    clsCommon.AddColumnsForChange(coll, "Serial_No", strSerial)

                    '-----------------tag No-------------------------
                    Dim strTag As String = clsCommon.myCstr(clsCommon.myCstr(dgrv.Cells("Tag No").Value))
                    If clsCommon.myLen(strTag) > 50 Then
                        common.clsCommon.MyMessageBoxShow("Sorry! The Tag No Size '" + strTag + "' at line No '" + LineNo + "' can be of maximum Length 50 ")
                        Exit Sub
                    End If
                    clsCommon.AddColumnsForChange(coll, "Tag_No", strTag)


                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*)from tspl_Visi_master where Visi_Id='" + strVisiId + "'", trans))
                    If (i = 0) Then
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VISI_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_Id='" + strVisiId + "'", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Sub fndid_Leave1()
        If fndid.Value = "" Then

        Else
            Try
                Dim str As String = "select visi_id,visimake from tspl_visi_master where visi_id='" + fndid.Value + "'"
                Dim dr As DataTable = Nothing
                Dim strvalue As String = String.Empty

                dr = clsDBFuncationality.GetDataTable(str)

                If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)(0).ToString()
                End If

                If strvalue <> "" Then

                Else
                    str = ""
                    common.clsCommon.MyMessageBoxShow("This Visi Id does not exist")
                    fndid.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    Sub cust_txtchanged()
        Dim query As String = "select cust_code as [Customer Id] from tspl_customer_master where cust_code='" + fndcustomerid.Value + "' "
        Dim cust_id As String = connectSql.RunScalar(query)
        If cust_id <> "" Then
            Dim query1 As String = "select customer_name from tspl_customer_master where cust_code='" + fndcustomerid.Value + "'"
            Dim custname As String = connectSql.RunScalar(query1)
            txtcustname.Text = custname
        Else
            txtcustname.Text = ""
        End If
    End Sub

    Sub custid_leave()
        If fndcustomerid.Value = "" Then
        Else
            Dim query As String = "select cust_code as [Customer Id] from tspl_customer_master where cust_code='" + fndcustomerid.Value + "' "
            Dim custid As String = connectSql.RunScalar(query)
            If custid <> "" Then
            Else : custid = ""
                txtcustname.Text = ""
                common.clsCommon.MyMessageBoxShow("This Customer Id does not exist")
                fndcustomerid.Value = ""
            End If
        End If
    End Sub

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        arrDBName.Clear()
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub



    Private Sub frmvisimaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub fndid__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndid._MYValidating

        Dim str As String = "select count(*) from TSPL_VISI_MASTER  where visi_id ='" + fndid.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndid.MyReadOnly = False
        Else
            fndid.MyReadOnly = True
        End If
        Dim qry As String = "select segment_code as [VisiId],Description as [Description]from TSPL_GL_SEGMENT_CODE "
        fndid.Value = clsCommon.ShowSelectForm("POProjectIFND", qry, "VisiId", "Seg_No='6' ", fndid.Value, "", isButtonClicked)
        rdtxtmake.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE WHere segment_code='" + fndid.Value + "' "))
        If fndid.Value IsNot Nothing Then
            rdbtndelete.Enabled = True
        Else
            rdbtndelete.Enabled = False
        End If
        text_changed()
    End Sub

    Private Sub fndid__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndid._MYNavigator

        Dim qst As String = "select segment_code as [Visi Id],Description as [Description] from TSPL_GL_SEGMENT_CODE    where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                qst += " and TSPL_GL_SEGMENT_CODE .segment_code in ('" + fndid.Value + "')"

                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and segment_code in (select min(segment_code) from TSPL_GL_SEGMENT_CODE where segment_code>'" + fndid.Value + "' and Seg_No='6'  ) "
            Case NavigatorType.First
                qst += "and segment_code in (select MIN(segment_code) from TSPL_GL_SEGMENT_CODE where Seg_No='6' )"
            Case NavigatorType.Last
                qst += "and segment_code in (select Max(segment_code) from TSPL_GL_SEGMENT_CODE where Seg_No='6' )"
            Case NavigatorType.Previous
                qst += "and segment_code in (select max(segment_code) from TSPL_GL_SEGMENT_CODE where segment_code<'" + fndid.Value + "'  and Seg_No='6' )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndid.Value = clsCommon.myCstr(dt.Rows(0)("Visi Id"))
        End If
        rdtxtmake.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where segment_code='" + fndid.Value + "'"))


        If fndid.Value IsNot Nothing Then
            rdbtndelete.Enabled = True
        Else
            rdbtndelete.Enabled = False

        End If
        text_changed()
        'fndid_Leave()
        'fndid_Leave1()

    End Sub

    Private Sub fndcustomerid__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustomerid._MYValidating
        'If isButtonClicked Then
        Dim qry As String = "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account"
        fndcustomerid.Value = clsCommon.ShowSelectForm("POProjctfND", qry, "Code", "", fndcustomerid.Value, "", isButtonClicked)
        '   rdtxtaccountset.Text = clsDBFuncationality.getSingleValue("Select acct_set_desc from tspl_vendor_account_set where acct_set_code='" + fndaccountset.Value + "'")
        'Else
        'Dim qry As String = "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name],m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account "
        'fndcustomerid.Value = clsCommon.ShowSelectForm("POProject3", qry, "Account Set", " (Substring(a.Receivable_Control_acct,6,3) in (" + UserAvailableLocationCodeQuery() + ") or a.Receivable_Control_acct in (" + UserAvailableAccountQuery() + ")) ", fndcustomerid.Value, "", isButtonClicked)
        '  If objCommonVar.CurrentUserCode = "ADMIN" AndAlso isButtonClicked Then
        'End If
        cust_txtchanged()
        custid_leave()


    End Sub
End Class
