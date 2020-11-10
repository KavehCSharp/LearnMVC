using System.Collections.Generic;

namespace CrudRepository.Data
{
    public class ErrorService
    {
        List<string> errors = new List<string>();
        
        public void Add(string err) => errors.Add(err);

        public string[] All => errors.ToArray();
    }
}