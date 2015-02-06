 
Imports Autodesk.Revit
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB



<Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)> _
<Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)> _
<Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)> _
Public Class cmdMonitorProgess
    Implements IExternalCommand

    'Please hookup this command to any existing Revit add-in
    'It will prompt user to select an instance from a model and change element's "MARK" parameter  
    Public Function Execute(ByVal commandData As Autodesk.Revit.UI.ExternalCommandData, ByRef message As String, ByVal elements As Autodesk.Revit.DB.ElementSet) As Autodesk.Revit.UI.Result Implements Autodesk.Revit.UI.IExternalCommand.Execute
        Try
            Dim monitor As UpdateParameter = New UpdateParameter(commandData)
            monitor.StartJob()

            Return Result.Succeeded

        Catch ex As Exception
            Return Result.Failed
        End Try
    End Function
End Class

