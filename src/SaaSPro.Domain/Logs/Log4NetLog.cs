using System;

namespace SaaSPro.Domain
{

    public class Log4NetLog : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual string Thread { get; set; }
        public virtual string Level { get; set; }
        public virtual string Logger { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
    }
}
