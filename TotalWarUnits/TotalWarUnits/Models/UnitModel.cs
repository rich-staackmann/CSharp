using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToExcel;
using System.IO;

namespace TotalWarUnits.Models
{
    public class UnitModel
    {
        public String Weapon { get; set; }
        public String Availability { get; set; }
        public String Name { get; set; }
        public int? Soldiers { get; set; }
        public int? Cost { get; set; }
        public int? Upkeep { get; set; }
        public int? Attack { get; set; }
        public int? Charge { get; set; }
        public int? Anti_Cav { get; set; }
        public int? Defence { get; set; }
        public int? Armor { get; set; }
        public int? Morale { get; set; }
        public int? Speed { get; set; }
        public String RecruitedFrom { get; set; }
        public String Abilities { get; set; }
        public int Order { get; set; }
        public String Notes { get; set; }
        public int Base_Charge { get; set; }
        public int Base_Defence { get; set; }
        public int Base_Morale { get; set; }

        //i added LinqToExcel with Nuget and use that to query a spreadsheet with all the data
        public static IQueryable<UnitModel> GetExcelData()
        {
            var excel = new ExcelQueryFactory();
            excel.FileName = string.Format("{0}\\Content\\Shogun2unitlist.xls", Directory.GetCurrentDirectory());
            excel.AddMapping<UnitModel>(x => x.Anti_Cav, "Anti-Cav");
            excel.AddMapping<UnitModel>(x => x.Base_Charge, "Base Charge");
            excel.AddMapping<UnitModel>(x => x.Base_Defence, "Base Defence");
            excel.AddMapping<UnitModel>(x => x.Base_Morale, "Base Morale");

            var units = from u in excel.Worksheet<UnitModel>("Melee Infantry")
                        select u;
            
            return units;
        }

    }
}