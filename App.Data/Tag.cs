using System;

namespace App.Data
{
    public class Tag
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public bool Checkable { get; set; }
        public Guid Id { get; set; } 
    }
}
