using System;
using System.Collections.Generic;

namespace MNA.Data
{
    public class Mna
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string BaseTag { get; set; }
        public string Position { get; set; }
        public IEnumerable<Tag> TsSecurity { get; set; }
        public IEnumerable<Tag> TsOther { get; set; }
        public IEnumerable<Tag> Tu { get; set; }
    }
}
