using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Role")]
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
