﻿using StudyProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Domain.Entities
{
    public class Client: EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ClientProductValue> ClientsProductsValues { get; set; }

        public Client()
        {
            this.ClientsProductsValues = new HashSet<ClientProductValue>();
        }
    }
}
