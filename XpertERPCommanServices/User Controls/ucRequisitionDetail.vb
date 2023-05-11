Imports common

Public Class ucRequisitionDetail
    Private _AppCode As String = ""
    Private _ReqCode As String = ""
    Private _Date As String = ""
    Private _AppName As String = ""
    Private _DateofBirth As String = ""
    Private _TelephoneNo As String = ""
    Private _Email As String = ""

    Public Property AppCode() As String
        Get
            Return _AppCode
        End Get
        Set(ByVal value As String)
            _AppCode = value
        End Set
    End Property
    Public Property ReqCode() As String
        Get
            Return LblReqNo.Text
        End Get
        Set(ByVal value As String)
            LblReqNo.Text = value
        End Set
    End Property
    Public Property AppDate() As String
        Get
            Return LblAppDate.Text
        End Get
        Set(ByVal value As String)
            LblAppDate.Text = value
        End Set
    End Property
    Public Property AppName() As String
        Get
            Return LblAppName.Text
        End Get
        Set(ByVal value As String)
            LblAppName.Text = value
        End Set
    End Property
    Public Property DateofBirth() As String
        Get
            Return LblDOB.Text
        End Get
        Set(ByVal value As String)
            LblDOB.Text = value
        End Set
    End Property
    Public Property TelephoneNo() As String
        Get
            Return LblTelephoneNo.Text
        End Get
        Set(ByVal value As String)
            LblTelephoneNo.Text = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return LblEmail.Text
        End Get
        Set(ByVal value As String)
            LblEmail.Text = value
        End Set
    End Property

    Public Sub RefreshData()
        RadGroupBox1.Text = ""
        LblAppCode.Text = _AppCode
        LblReqNo.Text = ""
        LblAppName.Text = ""
        LblEmail.Text = ""
        LblTelephoneNo.Text = ""
        LblAppDate.Text = ""
        LblDOB.Text = ""
        Dim qry As String

        qry = "SELECT Requisition_Code,Applicant_Date,Applicant_Date_Of_Birth,First_Name + '' + Middle_Name + '' + Last_Name As Name ,TELEPHONE_NO,Email  FROM TSPL_HR_APPLICANT_ENTRY Where APPLICANT_CODE='" + _AppCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            LblReqNo.Text = clsCommon.myCstr(dt.Rows(0)("Requisition_Code"))
            LblAppName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            LblEmail.Text = clsCommon.myCstr(dt.Rows(0)("Email"))
            LblTelephoneNo.Text = clsCommon.myCstr(dt.Rows(0)("TELEPHONE_NO"))
            LblAppDate.Text = clsCommon.myCDate(dt.Rows(0)("Applicant_Date"))
            LblDOB.Text = clsCommon.myCDate(dt.Rows(0)("Applicant_Date_Of_Birth"))
        End If
    End Sub
End Class
