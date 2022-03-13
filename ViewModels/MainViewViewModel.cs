using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingChangeWall
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }

        public List<Element> PickedObjects { get; }

        public List<Element> WallTypes { get; } 

        public Element SelectedWallTypes { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SaveCommand = new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickObjects(commandData);
            WallTypes = WallUtils.GetWallTypes(commandData, PickedObjects);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (PickedObjects.Count == 0 || SelectedWallTypes == null)
                return;

            using (var ts = new Transaction(doc, "Set system type"))
            {
                ts.Start();
                foreach (var pickedObject in PickedObjects)
                {
                    if(pickedObject is Wall)
                    {
                        var oWall = pickedObject as Wall;
                        WallType wallType = SelectedWallTypes as WallType;
                        oWall.WallType = wallType;
                    }
                }
                ts.Commit();
            }
            RaiseCloseRequest();
        }
        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
