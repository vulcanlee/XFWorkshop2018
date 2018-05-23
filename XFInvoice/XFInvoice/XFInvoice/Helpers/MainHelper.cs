using System;
using System.Collections.Generic;
using System.Text;

namespace XFInvoice.Helpers
{
    public class MainHelper
    {
        public static Firebase.Database.FirebaseClient FirebaseClient { get; set; }=
            new Firebase.Database.FirebaseClient("https://test-58f8c.firebaseio.com");
    }
}
