'--18/12/2014--form Add By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Object
'Imports Microsoft.SqlServer.ReplicationObject
Public Class frmReplicationSetting
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    'Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    Const colSourceSeqNo As String = "colSourceSeqNo"
    Const colSourceTableName As String = "colSourceTableName"

    Const colTargetSeqNo As String = "colTargetSeqNo"
    Const colTargetTableName As String = "colTargetTableName"
    Const colTargetPK As String = "colTargetPK"
    Const colTargetPK2 As String = "colTargetPK2"
    Const colTargetDetail As String = "colTargetDetail"
    Const colType As String = "colType"
    Const col_COND As String = "col_COND"


#End Region

   
    Sub LoadGridColumns(ByVal gv As RadGridView)
        'gvSourceTables.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        
            Dim TargetSeq As New GridViewTextBoxColumn
            TargetSeq.FormatString = ""
            TargetSeq.HeaderText = "Sequence No"
            TargetSeq.Name = colTargetSeqNo
            TargetSeq.Width = 100
            TargetSeq.ReadOnly = False
            TargetSeq.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetSeq)

            Dim TargetTableName As New GridViewTextBoxColumn
            TargetTableName.FormatString = ""
            TargetTableName.HeaderText = "Table Name"
            TargetTableName.Name = colTargetTableName
            TargetTableName.Width = 200
            TargetTableName.ReadOnly = True
            TargetTableName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetTableName)

            Dim TargetPK As New GridViewTextBoxColumn
            TargetPK.FormatString = ""
            TargetPK.HeaderText = "Primary Key"
            TargetPK.Name = colTargetPK
            TargetPK.Width = 100
            TargetPK.ReadOnly = True
            TargetPK.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetPK)

            Dim TargetPK2 As New GridViewTextBoxColumn
            TargetPK2.FormatString = ""
            TargetPK2.HeaderText = "Primary Key"
            TargetPK2.Name = colTargetPK2
            TargetPK2.Width = 100
            TargetPK2.ReadOnly = True
            TargetPK2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetPK2)


            Dim TargetDetail As New GridViewCheckBoxColumn
            TargetDetail.FormatString = ""
            TargetDetail.HeaderText = "Is Detail"
            TargetDetail.Name = colTargetDetail
            TargetDetail.Width = 80
            TargetDetail.ReadOnly = False
            TargetDetail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetDetail)
            gvTargetTables.EnableCustomFiltering = False
            gvTargetTables.EnableFiltering = True

            Dim _Type As New GridViewTextBoxColumn
            _Type.FormatString = ""
            _Type.HeaderText = "Type"
            _Type.Name = colType
            _Type.Width = 80
            _Type.ReadOnly = False
            _Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(_Type)

            Dim _COND As New GridViewTextBoxColumn
            _COND.FormatString = ""
            _COND.HeaderText = "Condition"
            _COND.Name = col_COND
            _COND.Width = 80
            _COND.ReadOnly = False
            _COND.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(_COND)

            gvTargetTables.EnableCustomFiltering = False
            gvTargetTables.EnableFiltering = True
        

    End Sub
    
    'Private Sub btnSaveDist_Click(sender As Object, e As EventArgs) Handles btnSaveDist.Click
    '    ' Set the server and database names
    '    Dim distributionDbName As String = "distribution"
    '    Dim publisherName As String = publisherInstance
    '    Dim publicationDbName As String = "AdventureWorks2012"

    '    Dim distributionDb As DistributionDatabase
    '    Dim distributor As ReplicationServer
    '    Dim publisher As DistributionPublisher
    '    Dim publicationDb As ReplicationDatabase

    '    ' Create a connection to the server using Windows Authentication.
    '    Dim conn As ServerConnection = New ServerConnection(publisherName)

    '    Try
    '        ' Connect to the server acting as the Distributor 
    '        ' and local Publisher.
    '        conn.Connect()

    '        ' Define the distribution database at the Distributor,
    '        ' but do not create it now.
    '        distributionDb = New DistributionDatabase(distributionDbName, conn)
    '        distributionDb.MaxDistributionRetention = 96
    '        distributionDb.HistoryRetention = 120

    '        ' Set the Distributor properties and install the Distributor.
    '        ' This also creates the specified distribution database.
    '        distributor = New ReplicationServer(conn)
    '        distributor.InstallDistributor((CType(Nothing, String)), distributionDb)

    '        ' Set the Publisher properties and install the Publisher.
    '        publisher = New DistributionPublisher(publisherName, conn)
    '        publisher.DistributionDatabase = distributionDb.Name
    '        publisher.WorkingDirectory = "\\" + publisherName + "\repldata"
    '        publisher.PublisherSecurity.WindowsAuthentication = True
    '        publisher.Create()

    '        ' Enable AdventureWorks2012 as a publication database.
    '        publicationDb = New ReplicationDatabase(publicationDbName, conn)

    '        publicationDb.EnabledTransPublishing = True
    '        publicationDb.EnabledMergePublishing = True

    '    Catch ex As Exception
    '        ' Implement appropriate error handling here.
    '        Throw New ApplicationException("An error occured when installing distribution and publishing.", ex)

    '    Finally
    '        conn.Disconnect()

    '    End Try
    'End Sub

    Private Sub btnSavePub_Click(sender As Object, e As EventArgs) Handles btnSavePub.Click

    End Sub

    Private Sub frmReplicationSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim objdistr As DistributionDatabase

    End Sub
    Function CheckDistributor() As Boolean
        '   Dim qry As String

    End Function
    Function CheckPublication() As Boolean

    End Function
    Function CheckSubscription() As Boolean

    End Function
End Class
