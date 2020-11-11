using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DataModels
{
    [Table("Task")]
    public class Tasks
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Subject { get; set; }
        public bool IsComplete { get; set; }
        public Guid? AssignedToId { get; set; }
        [ForeignKey(nameof(AssignedToId))]
        public Member AssignedTo { get; set; }
    }
}
