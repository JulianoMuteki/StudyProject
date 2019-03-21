using StudyProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Domain.Entities
{
    public class ClientProductValue : ValueObject<ClientProductValue>
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Client Client { get; set; }

        public float Price { get; set; }

        protected override bool EqualsCore(ClientProductValue other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
