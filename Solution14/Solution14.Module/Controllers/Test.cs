using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace Solution14.Module.Controllers
{

    
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Test : ViewController
    {
        public Test()
        {
            SimpleAction SimpleAction1 = new SimpleAction(this, "SimpleAction1", "Tools"
               );
            SimpleAction1.ImageName = "Action_SimpleAction";
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void SimpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Password=;User ID=Admin;Data Source=Solution14.mdb;Mode=Share Deny None;";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);
            Session session = new Session();
            session.ExecuteNonQuery("delete from Solution14.Planeplanes_Pilotpilots");
            session.ExecuteNonQuery("delete from Solution14.Pilot");
            session.ExecuteNonQuery("delete from Solution14.Plane");
            session.ExecuteNonQuery("delete from Solution14.Airport");


             
            for (int i = 0; i < 9; i++)
            {
                BusinessObjects.Airport a = new BusinessObjects.Airport(session);
                a.Name = "Аэропорт" + (i+1).ToString();
                for (int k = 0; k < 10; k++)
                {
                    BusinessObjects.Pilot pi1 = new BusinessObjects.Pilot(session);
                    pi1.Name = "Пилот" + (30* i+3*k).ToString();
                    BusinessObjects.Plane pl1 = new BusinessObjects.Plane(session);
                    pl1.Name = "Самолет" + (30 * i+3*k).ToString();
                    pi1.Airport = a;
                    pl1.Airport = a;
                    
                    BusinessObjects.Pilot pi2 = new BusinessObjects.Pilot(session);
                    pi2.Name = "Пилот" + (30 * i + 1+3*k).ToString();
                    BusinessObjects.Plane pl2 = new BusinessObjects.Plane(session);
                    pl2.Name = "Самолет" + (30 * i + 1+3*k).ToString();
                    pi2.Airport = a;
                    pl2.Airport = a;

                    BusinessObjects.Pilot pi3 = new BusinessObjects.Pilot(session);
                    pi3.Name = "Пилот" + (30 * i + 2+3*k).ToString();
                    BusinessObjects.Plane pl3 = new BusinessObjects.Plane(session);
                    pl3.Name = "Самолет" + (30 * i + 2+3*k).ToString();
                    pi3.Airport = a;
                    pl3.Airport = a;

                    pi1.planes.Add(pl1);
                    pi1.planes.Add(pl2);
                    pi1.planes.Add(pl3);
                    pi2.planes.Add(pl1);
                    pi2.planes.Add(pl2);
                    pi2.planes.Add(pl3);
                    pi3.planes.Add(pl1);
                    pi3.planes.Add(pl2);
                    pi3.planes.Add(pl3);

                    pi1.Save();
                    pl1.Save();
                    pi2.Save();
                    pl2.Save();
                    pi3.Save();
                    pl3.Save();
                }
                a.Save();
            }
        }
    }
}
