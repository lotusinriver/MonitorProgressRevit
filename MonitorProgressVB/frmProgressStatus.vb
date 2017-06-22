Imports System.Windows.Forms

Public Class frmProgressStatus

    Private _isAutoClose As Boolean
    Private _ProcessCancelled As Boolean
    Event CancelProcess(ByVal sender As Object, ByVal e As EventArgs)

    Public ReadOnly Property ProcessCancelled() As Boolean
        Get
            Return _ProcessCancelled
        End Get
    End Property

 
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub UpdateStatus(ByVal message As String, ByVal percentage As Integer)
        Me.pbarSync.Value = percentage
        lblStatus.Text = message
        Application.DoEvents()
    End Sub

    Public Sub JobCompleted()
        btnOK.Visible = True
        btnCancel.Visible = False
        If _ProcessCancelled Then
            lblStatus.Text = "Job is cancelled at your request"
        Else
            lblStatus.Text = "Job is completed"
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        _ProcessCancelled = True
        btnCancel.Visible = False
    End Sub

End Class