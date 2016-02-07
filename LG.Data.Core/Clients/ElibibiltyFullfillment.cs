using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.IMS.SF;

namespace LG.Data.Core.Clients
{
    public static class ElibibiltyFullfillment
    {

        public static System.Boolean DoSometing()
        {
            var client = LG.Services.ClientConnection.GetIMS_SF_Connection();
            client.InsertFulfillmentScheduleRecord(new InsertFulfillmentScheduleRecordRequest()
            {
                MessageGuid = Guid.NewGuid(),
                Record = new FulfillmentScheduleRecord()
                {
                    ClientRID = 2,
                    FulfillmentStatus = FulfillmentStatusEnum.NotStarted,
                    BlobURL = "http://www.somefile.com/test.csv",
                    
                }
            });
            client.GetEligibilityDataImportScheduledRecordsUsingFilter(new GetEligibilityDataImportScheduledRecordsUsingFilterRequest
                ()
            {
                MessageGuid = Guid.NewGuid(),
                EligibilityDataImportFilter = new EligibilityDataImportFilter()
                {
                    ClientRID = 10148,
                    EligibilityDataType = EligibilityDataTypeEnum.CompleteListToSync,
                    EligibilityDataImportStatus = EligibilityDataImportStatusEnum.Complete
                }
            });
            return true;
        }

    }
}
