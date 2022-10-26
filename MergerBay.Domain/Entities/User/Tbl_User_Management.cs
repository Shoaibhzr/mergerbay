using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.User
{
    [Table("Tbl_User_Management")]
    public class Tbl_User_Management
    {
        [Key]
        public Guid UserId { get; set; }
        public string User_First_Name { get; set; }
        public string User_Last_Name { get; set; }
        public string User_Contact_Number { get; set; }
        public string User_Joined_Email { get; set; }
        public Boolean Is_Company_Account { get; set; }

        public string TimeZone { get; set; }
        public string Business_Name { get; set; }
        public string Business_Email { get; set; }
        public string Address { get; set; }
        public string Business_Phone_Number { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public Guid Category_ID { get; set; }
        public string Designation { get; set; }
        public Guid Created_by { get; set; }
        public DateTime Created_Date { get; set; }
        public Guid Modified_By { get; set; }
        public Guid Modified_Date { get; set; }
        public Boolean Is_Active { get; set; }
        public Boolean Is_Deleted { get; set; }
        public Guid Role_ID { get; set; }

    }
}
