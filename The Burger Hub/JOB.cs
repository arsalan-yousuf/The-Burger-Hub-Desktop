//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace The_Burger_Hub
{
    using System;
    using System.Collections.Generic;
    
    public partial class JOB
    {
        public JOB()
        {
            this.EMPLOYEES = new HashSet<EMPLOYEE>();
            this.JOB_HISTORY = new HashSet<JOB_HISTORY>();
        }
    
        public string JOB_ID { get; set; }
        public string JOB_TITLE { get; set; }
        public Nullable<int> MIN_SALARY { get; set; }
        public Nullable<int> MAX_SALARY { get; set; }
    
        public virtual ICollection<EMPLOYEE> EMPLOYEES { get; set; }
        public virtual ICollection<JOB_HISTORY> JOB_HISTORY { get; set; }
    }
}
