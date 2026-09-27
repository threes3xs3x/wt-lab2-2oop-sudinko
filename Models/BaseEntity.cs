using WtLab2.Interfaces;

namespace WtLab2.Models
{
    public abstract class BaseEntity : IStorable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public abstract void PrintInfo();
    }
}