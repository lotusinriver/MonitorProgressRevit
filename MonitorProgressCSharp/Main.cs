using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace MonitorProgressCSharp
{
    [Transaction(TransactionMode.Manual)]
    class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UpdateParameter updateParam = new UpdateParameter(commandData);
                updateParam.StartJob();
            }
            catch (Exception ex)
            {
                message = ex.Message + "\n" + ex.StackTrace;
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
