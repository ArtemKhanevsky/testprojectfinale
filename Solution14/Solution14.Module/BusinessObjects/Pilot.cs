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
    [XafDisplayName("Пилот")]

    public class Pilot : BaseObject
    {

        public Pilot(Session session)
            : base(session)
        {

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();


        }
        protected override void OnSaving()
        {
            if (testttt.f.CheckPilotPlaneAccordance(this, planes)) { base.OnSaving(); }
            else { testttt.f.Error("Один из самолетов прикреплен к другому аэропорту."); }
            if (testttt.f.CheckAirportAccordance(Airport, planes)) { base.OnSaving(); }
            else { testttt.f.Error("Один из пилотов прикреплен к другому аэропорту."); }

        }
        //rule is referenced 






        private string name;
        [XafDisplayName("Имя")]
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



        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, "0")]
        [XafDisplayName("Количество самолетов")]
        public int count
        {
            get { return planes.Count; }
        }
       
        private Airport airport;


        [Association]
        [XafDisplayName("Аэропорт")]
      

        public Airport Airport
        {

            get { return airport; }
            set { SetPropertyValue("", ref airport, value); }
        }







    }
}