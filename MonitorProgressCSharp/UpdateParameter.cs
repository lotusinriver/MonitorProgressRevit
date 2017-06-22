using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using MonitorProgressCSharp.Views;

namespace MonitorProgressCSharp
{
    internal class UpdateParameter
    {
        private UIApplication App { get; set; }
        private Document Doc { get; set; }
        private Element Element { get; set; }
        private Parameter Parameter { get; set; }

        public UpdateParameter(ExternalCommandData commandData)
        {
            App = commandData.Application;
            Doc = App.ActiveUIDocument.Document;
        }

        public void StartJob()
        {
            SelectInstance();

            Views.ProgressStatusUI progressBarUI = new Views.ProgressStatusUI();
            progressBarUI.ContentRendered += ProgressBarUI_ContentRendered;
            progressBarUI.ShowDialog();
        }

        private void ProgressBarUI_ContentRendered(object sender, EventArgs e)
        {
            Views.ProgressStatusUI progressBarUI = sender as Views.ProgressStatusUI;

            if (progressBarUI == null)
                throw new Exception("Error trying to create progress bar window");

            for (int i = 1; i <= 100; i++)
            {
                ChangeParameter(i.ToString());
                progressBarUI.UpdateStatus(string.Format("Update parameter {0}", i.ToString()), i);
                if (progressBarUI.ProcessCancelled)
                    break;
            }

            progressBarUI.JobCompleted();
        }

        private void ChangeParameter(string newValue)
        {
            using (Transaction t = new Transaction(Doc, "Set Parameter"))
            {
                t.Start();

                Parameter.Set(newValue);
                System.Threading.Thread.Sleep(50);

                t.Commit();
            }
        }

        private void SelectInstance()
        {
            Selection sel = App.ActiveUIDocument.Selection;
            Reference reference = sel.PickObject(ObjectType.Element, "Select an element");
            Element = Doc.GetElement(reference);

            Parameter = Element.get_Parameter(BuiltInParameter.DOOR_NUMBER);

            if (Parameter.IsReadOnly)
                throw new Exception("Please select an instance with editable 'Mark' parameter");
        }
    }
}