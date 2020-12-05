using System;

namespace CrudImageRepository.Models
{
    public class User : Entity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool IsActive { get; set; }

        // true for image in db
        public bool IsImageStoreInDB { get; set; }

        public string Image { get; set; }

        public int Age { get; set; }

        public string City { get; set; }
    }       
}