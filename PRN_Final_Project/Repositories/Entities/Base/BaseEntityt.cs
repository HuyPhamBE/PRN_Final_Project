using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities.Base
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {            
            createdAt = updatedAt = DateTimeOffset.Now;            
        }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updatedAt { get; set; }
    }
}
