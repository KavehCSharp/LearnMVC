namespace CrudRepository.Models
{
    public class Customer : Entity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }
    }
}