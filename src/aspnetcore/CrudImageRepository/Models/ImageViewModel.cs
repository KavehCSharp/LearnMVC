using System;

namespace CrudImageRepository.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        public bool IsImageStoreInDB { get; set; }

        public bool IsInDocker { get; set; }
        
        public string Image { get; set; }
    }
}