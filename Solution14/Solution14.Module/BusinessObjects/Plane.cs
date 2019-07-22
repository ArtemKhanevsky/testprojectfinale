using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraEditors;
using testttt;

namespace Solution14.Module.BusinessObjects
{
    [DefaultClassOptions]

    [XafDisplayName("Самолет")]
    public class Plane : XPObject
    {
        public Plane(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


        protected override void OnSaving()
        {
           if (testttt.f.CheckPilotPlaneAccordance(this, pilots)) { base.OnSaving(); }
            else { testttt.f.Error("Один из пилотов прикреплен к другому аэропорту"); }
            if (testttt.f.CheckAirportAccordance(Airport, pilots)) { base.OnSaving(); }
            else { testttt.f.Error("Один из пилотов прикреплен к другому аэропорту."); }

        }


        private string name;
        [XafDisplayName("Серийный номер")]
        [RuleUniqueValue]
        [RuleRequiredField]
        [Appearance("Green Planes", Criteria = "(Contains(Name,'a' ))OR(Contains(Name,'A' ))",
            FontColor = "Green", TargetItems = "Name", Context = "ListView")]
        [Size(200)]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("", ref name, value); }
        }

        /* 
        [Appearance("", Criteria = "1=1", TargetItems = "CheckAirport", Visibility = ViewItemVisibility.Hide)]
        [RuleFromBoolProperty("if plane              connected", DefaultContexts.Save,
"Несоответствие пилота и аэропорта")]
        public bool CheckAirport
        {
            get
            {
                if (pilots[0].Airport == this.Airport) { return true; }
                else { return false; }
            }


        }
        */


        private Airport airport;
        [XafDisplayName("Аэропорт")]
        [RuleRequiredField]
        [Association]
        public Airport Airport
        {
            get { return airport; }
            set { SetPropertyValue("", ref airport, value); }
        }


        [XafDisplayName("Список пилотов")]
        [Association]
        public XPCollection<Pilot> pilots
        {
            get { return GetCollection<Pilot>("pilots"); }
        }



        [XafDisplayName("Количество пилотов")]
        public int count
        {
            get { return pilots.Count; }
        }
    }
}
