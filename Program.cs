using System;
using System.Collections.Generic;
using FetchXmlWrapper;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;

//http://blog.cobalt.net/blog/performance-improvements-using-fetch-xml-wrapper
//https://github.com/TheCRMLab/CRMFetchXmlWrapper

namespace FetchXmlWrapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example: Static declaration of Fetch Xml
            string invoicePaymentSumFetch = string.Format(@" 
                <fetch distinct='false' mapping='logical' aggregate='true' no-lock='true'> 
                    <entity name='cobalt_invoicepayment'> 
                        <attribute name='cobalt_appliedamount' alias='cobalt_appliedamount_sum' aggregate='sum' /> 
                        <filter type='and'>
                            <condition attribute='cobalt_paymentid' operator='eq' value='D4BDF2E8-BE5D-4B33-ABFF-96E33F968BD7' />    
                        </filter>
                    </entity> 
                </fetch>");

            //manual fetchWrapper
            var fetchWrapper = new FetchWrapper("cobalt_invoicepayment");
            fetchWrapper.AddColumn("cobalt_appliedamount", FetchXmlWrapper.Aggregate.sum, "cobalt_appliedamount_sum");
            fetchWrapper.AppendFilter("cobalt_paymentid", new Guid("D4BDF2E8-BE5D-4B33-ABFF-96E33F968BD7"));
            Console.WriteLine(fetchWrapper.toXml() + "\n\n");
            fetchWrapper.AddActiveOnlyFilter();
            Console.WriteLine(fetchWrapper.toXml() + "\n\n");
            Console.WriteLine();
            
            //from fetchXml string
            var myWrapper = new FetchWrapper(FetchExpression.Deserialize(invoicePaymentSumFetch)); 
            Console.WriteLine(myWrapper.toXml() + "\n\n");
            myWrapper.AddActiveOnlyFilter();
            Console.WriteLine(myWrapper.toXml() + "\n\n");

            Console.WriteLine();
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
