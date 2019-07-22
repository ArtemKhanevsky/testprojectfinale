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
using testttt;


namespace Solution14.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Аэропорт")]

    public class Airport : XPObject
    {


        public Airport(Session session)
            : base(session)
        {

        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
        protected override void OnSaving()
        {
            if (testttt.f.CheckAirportAccordance(this, pilots)) { base.OnSaving(); }
            else { testttt.f.Error("Один из пилотов прикреплен к другому аэропорту."); }

            if (testttt.f.CheckAirportAccordance(this, planes)) { base.OnSaving(); }
            else { testttt.f.Error("Один из самолетов прикреплен к другому аэропорту."); }

        }
        



        private string name;

        [XafDisplayName("Название")]
        [RuleUniqueValue]
        [RuleRequiredField]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("", ref name, value); }
        }


        [XafDisplayName("Список самолетов")]
        [Association]
        public XPCollection<Plane> planes
        {
            get { return GetCollection<Plane>("planes"); }
        }

        [XafDisplayName("Количество самолетов")]
        public int count
        {
            get { return planes.Count; }
        }

        


        [XafDisplayName("Список пилотов")]
        [Association]
        public XPCollection<Pilot> pilots
        {
            get { return GetCollection<Pilot>("pilots"); }
        }

        [XafDisplayName("Количество пилотов")]
        public int count2
        {
            get { return pilots.Count; }
        }
    }
}