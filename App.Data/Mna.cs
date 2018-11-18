using System;
using System.Collections.Generic;

namespace App.Data
{
    public class Mna
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string BaseTag { get; set; }
        public string Position { get; set; }
        public bool TagWithNumber { get; set; }

        public string TsSecurityCaption { get; set; }
        public IEnumerable<Tag> TsSecurity { get; set; }

        public string TsOtherCaption { get; set; }
        public IEnumerable<Tag> TsOther { get; set; }

        public string TuCaption { get; set; }
        public IEnumerable<Tag> Tu { get; set; }
    }
}
