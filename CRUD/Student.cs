using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class Student
    {  
        public string stid { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }
        public string course { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        
        public Student(string Fname, string Mname, string Lname, string Course, string Email, string Address, string Stid)
        {
            stid = Stid;
            fname = Fname;
            mname = Mname;
            lname = Lname;
            course = Course;
            email = Email;
            address = Address;
        }
    }


}
