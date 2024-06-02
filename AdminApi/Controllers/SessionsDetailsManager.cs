using System.Text;
using AdminApi;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace AdminApi.Controllers
{

    public class SessionsDetailsManager
    {
        public ClientsSessionsDetails GetSessions(string? clientId = null)
        {

            var clientNamesToIds = new Dictionary<string, Guid>()
        {
            {"Utah",        new Guid("740039a3-efb1-42e8-bda8-3196dc64620b")},
            {"Kansas",      new Guid("0ecf8e08-333b-4d7d-bcb3-b687de7726a8")},
            {"Main",        new Guid("7bc6ca72-3f43-4ad2-8004-fa257e751678")},
            {"New York",    new Guid("16c5026f-0417-45a4-9731-9a08dd7df830")},
            {"Florida",     new Guid("7cf2580c-abb5-43cc-82b9-86706eaa1fb8")},
            {"Las Vegas",   new Guid("9cf2580c-abb5-43cc-82b9-86706eaa1fb8")},
        };

            var count = clientNamesToIds.Count();
            if (clientId != null)
            {
                count = 1;
            }

            var sessionsDetails = new ClientsSessionsDetails
            {
                ClientBlobsList = Enumerable.Range(1, count).Select(index => new ClientBlobs
                {
                    ClientId = clientNamesToIds.Values.ToList()[index - 1].ToString(),
                    ClientName = clientNamesToIds.Keys.ToList()[index - 1],
                    HasProviders = true,
                    HasLocations = true,

                    ClientBlobsList = Enumerable.Range(1, 5).Select(iindex => new BlobDetails
                    {
                        BlobName = Guid.NewGuid().ToString(),

                        //BlobType =  "Blok Blob",
                        BlobModifiedDate = DateTime.Now.ToLongDateString(),
                        BlobProvidersCount = $"{2000 * index}",
                        BlobLocationsCount = $"{2000 * index}",

                        BlobProvidersCountPass = $"{2000 * index}",
                        BlobLocationsCountPass = $"{2000 * index}",

                        //BlobContentSize =  "21 B",
                        //BlobContentType = "Json" ,

                        BlobNumberOfProvidersReceived = 2000 * index,
                        BlobNumberOfProvidersFailedValidations = 100 * index,
                        BlobNumberOfProvidersFailedCreation = 76 * index,
                        BlobNumberOfProvidersFailedInsertion = 50 * index,
                        BlobNumberOfProvidersMissingSpecialties = 150 * index,

                        BlobNumberOfLocationsReceived = 1500 * index,
                        BlobNumberOfLocationsFailedValidations = 20 * index,
                        BlobNumberOfLocationsFailedCreation = 13 * index,
                        BlobNumberOfLocationsFailedInsertion = 20 * index

                    })
                            .ToList()

                })
                    .ToList()
            };

            if (string.IsNullOrEmpty(clientId))
            {
                return sessionsDetails;
            }

            var client = sessionsDetails.ClientBlobsList.Where(x => x.ClientId == clientId).ToList();
            var singleClientSessionsDetails = new ClientsSessionsDetails() { ClientBlobsList = client };

            return singleClientSessionsDetails;

        }

    }
}