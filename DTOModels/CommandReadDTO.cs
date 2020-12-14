using System;
using System.ComponentModel.DataAnnotations;

namespace Commander.DTOModels
{
    public class CommandReadDTO
    {
        public Guid Id { get; set; }
            
        public string HowTo { get; set; }
            
        public string Line { get; set; }
    }
}