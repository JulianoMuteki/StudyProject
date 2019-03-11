using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Domain.Entities
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }

        public bool GetSpecialClient(Client client)
        {
            return client.IsActive && DateTime.Now.Year - client.CreateDate.Year >= 5;
               
        }
    }
}
