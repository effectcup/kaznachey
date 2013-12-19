using System.Collections.Generic;

namespace effectcup.KaznacheyPayment
{
    public class PaySystem
    {
        public int Id { get; set; }

        public string PaySystemName { get; set; }

        public List<FieldInfo> Fields { get; set; }
    }
}
