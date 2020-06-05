using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TemplateAPI.Models.DTO
{
    public class EventDTO : IValidatableObject
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required, StringLength(20, ErrorMessage = "Name can only be 20 character long")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Cost { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (Name.Contains("ZZZZ"))
            {
                results.Add(new ValidationResult("Name cannot contain ZZZZ"));
            }
            if (GroupId < 22)
            {
                results.Add(new ValidationResult("Group Id has to greater then 22"));
            }
            if(Cost < 2222)
            {
                results.Add(new ValidationResult("Cost has to be greater then 2222"));
            }
            return results;
        }
    }
}
