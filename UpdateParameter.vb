Imports System.ComponentModel
Imports Autodesk.Revit.UI.Selection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.ApplicationServices
Imports Autodesk.Revit.UI
Imports Autodesk.Revit

Public Class UpdateParameter
    Private myApp As UIApplication
    Private myDoc As Document
    Dim selectedelement As Element

    Public Sub New(ByVal commandData As UI.ExternalCommandData)
        myApp = commandData.Application
        myDoc = myApp.ActiveUIDocument.Document
    End Sub

    Public Sub StartJob()
        'Select an instance with "Mark" parameter
        selectedelement = SelectInstance()

        Dim frm As frmProgressStatus = New frmProgressStatus()
        'Start Revit Call here
        AddHandler frm.Shown, AddressOf RevitCallWithProgessBar
        frm.ShowDialog()


    End Sub

    Private Sub RevitCallWithProgessBar(sender As Object, e As EventArgs)
        Dim frm As frmProgressStatus = sender

        For i As Integer = 1 To 100
            ChangeParameter(selectedelement, i)
            frm.UpdateStatus("Update parameter " & i.ToString & "...", i)
            If frm.ProcessCancelled Then
                Exit For
            End If
        Next
        frm.JobCompleted()
    End Sub

    Private Function SelectInstance() As Element
        Dim choices As Selection
        Dim ref As Reference
        Dim element As Element = Nothing

        'Prompt to select an element
        choices = myApp.ActiveUIDocument.Selection
        Do While element Is Nothing
            ref = choices.PickObject(ObjectType.Element, "Select an element")
            element = myDoc.GetElement(ref.ElementId)
            'Verify the selected instance
            For Each para As Parameter In element.Parameters
                If para.IsReadOnly = False And para.Definition.Name.ToUpper = "MARK" Then
                    Return element
                End If
            Next
            'If instance does not have editable 'Mark' parameter, select again
            MsgBox("Please select an instance with editable 'Mark' parameter ")
            element = Nothing
        Loop

    End Function

    Private Sub ChangeParameter(ByVal ele As Element, ByVal newvalue As String)
        For Each para As Parameter In ele.Parameters
            If para.IsReadOnly = False And para.Definition.Name.ToUpper = "MARK" Then
                Using trans As New Autodesk.Revit.DB.Transaction(myDoc, "SetParam")
                    Try
                        If trans.GetStatus <> DB.TransactionStatus.Started Then
                            trans.Start()
                        End If
                        para.Set(newvalue)
                        trans.Commit()
                        'Time-consuming call
                        System.Threading.Thread.Sleep(100)
                    Catch ex As Exception
                        trans.RollBack()
                    End Try
                End Using
            End If
        Next
    End Sub

End Class
