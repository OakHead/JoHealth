using System.Collections.Generic;

namespace JoHealth.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DoctorId { get; set; }

    }
}
