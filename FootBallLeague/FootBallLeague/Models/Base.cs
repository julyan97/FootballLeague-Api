using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Models
{
    public class Base<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
