//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Game
    {
        public long IdGame { get; set; }
        public Nullable<long> IdFirstPlayer { get; set; }
        public Nullable<long> IdSecondPlayer { get; set; }
        public Nullable<int> NumberOfStep { get; set; }
        public Nullable<int> NumbersOutStepsOfFirst { get; set; }
        public Nullable<int> NumbersOutStepsOfSecond { get; set; }
        public Nullable<int> NumbersOfFirst { get; set; }
        public Nullable<int> NumbersOfSecond { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public string WhoseStep { get; set; }
        public Nullable<long> IdWord { get; set; }
    }
}
