using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Data.Entities
{
    [Table("Participants")]
    public class Participant : IEntity
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
    }
}
