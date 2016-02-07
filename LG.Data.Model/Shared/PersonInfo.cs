using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public interface IPersonInfo
    {
        System.String FName { get; set; }
        System.String MName { get; set; }
        System.String LName { get; set; }
        System.DateTime? Dob { get; set; }
        System.Int32? Gender { get; set; }
        string DateOfBirthString { get; set; }

    }
    public class PersonInfo : IPersonInfo
    {
        public System.String FName { get; set; }
        public System.String MName { get; set; }
        public System.String LName { get; set; }
        public System.DateTime? Dob { get; set; }
        public System.Int32? Gender { get; set; }

        public string DateOfBirthString { get; set; }

        public PersonInfo() { }

        public PersonInfo(IPersonInfo personInfo)
        {
            this.Populate(personInfo);
        }
        public void Populate(IPersonInfo personInfo)
        {
            if (personInfo != null)
            {
                this.FName = personInfo.FName;
                this.MName = personInfo.MName;
                this.LName = personInfo.LName;
                this.Dob = personInfo.Dob;
                this.Gender = personInfo.Gender;
                this.DateOfBirthString = personInfo.DateOfBirthString;
            }
        }
    }
}
