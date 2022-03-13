using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingChangeWall
{
    public class WallUtils
    {
        public static List<Element> GetWallTypes(ExternalCommandData commandData, List<Element> element)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            
            List<Element> elementList = element[0].GetValidTypes().Select(selectedObject => doc.GetElement(selectedObject)).ToList();
            return elementList;
        }
    }
}
