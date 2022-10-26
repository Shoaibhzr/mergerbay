using MergerBay.Domain.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.UserProfile
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }

        public string ProfilePictureList { get; set; }

    }

    public class CompanyInformation
    {
        public Guid UserId { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string Designation { get; set; }
        public IEnumerable<CompanyCategory> CategoryList { get; set; }
        public IEnumerable<string> CategoryListStore { get; set; }
       // public string[] CategoryListStore = new string[1];
        public List<string>? CardAttachment = new List<string>();
        public List<string>? CertificateAttachment = new List<string>();

    }

    public class UserKey
    {
        public string User_Id { get; set; }
    }
    public class CategoryList
    {
        public Guid CategoryId { get; set; }
        public string  Category { get; set; }
    }


    public class CardAttachment
    {
        public string AttachmentName { get; set; }
    }

    public class CertificateAttachment
    {
        public string AttachmentName { get; set; }
    }

    public class ProfileAttachment
    {
        public string ProfilePicture { get; set; }
    }
}
