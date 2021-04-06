using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCardInformation:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public int CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardSecurityNumber { get; set; }
    }
}
