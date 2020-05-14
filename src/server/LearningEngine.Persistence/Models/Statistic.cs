using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
        public double CardKnowledge { get; set; }
    }
}
