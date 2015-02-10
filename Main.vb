 
Imports Autodesk.Revit
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB



<Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)> _
Public Class CmdMonitorProgess
  Implements IExternalCommand

  'Please hookup this command to any existing Revit add-in
  'It will prompt user to select an instance from a model and change element's "MARK" parameter  
  Public Function Execute( _
      ByVal commandData As Autodesk.Revit.UI.ExternalCommandData, _
      ByRef message As String, _
      ByVal elements As Autodesk.Revit.DB.ElementSet) _
    As Autodesk.Revit.UI.Result _
    Implements Autodesk.Revit.UI.IExternalCommand.Execute
    Try
      Dim monitor As UpdateParameter = New UpdateParameter(commandData)
      monitor.StartJob()

      Return Result.Succeeded

    Catch ex As Exception
      Return Result.Failed
    End Try
  End Function
End Class

