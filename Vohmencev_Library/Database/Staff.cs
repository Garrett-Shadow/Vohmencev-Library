//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vohmencev_Library.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff
    {
        public int StaffCode { get; set; }
        public string StaffLogin { get; set; }
        public string StaffPassword { get; set; }
        public string StaffName { get; set; }
        public string StaffPosition { get; set; }
    
        public virtual Roles Roles { get; set; }
    }
}
