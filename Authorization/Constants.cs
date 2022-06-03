using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Authorization
{
    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ResolveOperationName = "Resolve";
        public static readonly string RejectOperationName = "Not yet";

        public static readonly string TicketAdministratorsRole ="TicketAdministrators";
        public static readonly string TicketRDsRole = "TicketRDs";
    }
}
