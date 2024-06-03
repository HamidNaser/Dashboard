using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Importer.Core.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdminApi.Controllers
{
    
    public class SessionsDetailsManager
    {
        public ClientsSessionsDetailsAdmin GetSessions(string connectionString, string? clientId = null)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("importer-archive");
            
            var blobs = containerClient.GetBlobs();

            var validBlobs = blobs.Where(x => x.Name.StartsWith("prod")).ToList();            
            
            var clientNamesToIds = new Dictionary<string, Guid>()
            {
                {"Manchester United", new Guid("f89d2d36-7b3b-4e6a-8f43-0951f067fd11")},
                {"Liverpool", new Guid("ec5288e2-7f52-4a80-89e2-1f6282f51e95")},
                {"Chelsea", new Guid("48db09aa-af4c-4e18-a502-5a474b00e5b2")},
                {"Manchester City", new Guid("6bb7e7ef-2d4a-41fb-a922-f7a79d48d20a")},
                {"Tottenham Hotspur", new Guid("1b6a70aa-1583-4c90-81a1-ae5bbf82dbbc")},
                {"Arsenal", new Guid("e70dd6a5-dde1-42fb-8c4e-68ac27e97756")},
                {"Real Madrid", new Guid("a0e8f7a1-fa8a-49e4-93ec-b1ec443f76a1")},
                {"Barcelona", new Guid("b5e9430c-2b86-43b1-8516-7ae1c5e79515")},
                {"Atletico Madrid", new Guid("a4a3fc63-9e96-49c3-968f-13273a5b6203")},
                {"Sevilla", new Guid("74e1c744-4f53-48e5-ba87-5879ce8716a4")},
                {"Real Sociedad", new Guid("43d87550-0a25-442c-b32b-2d0a8d6b67a3")},
                {"Valencia", new Guid("7d8a7a82-d675-4952-8163-78806f2d65a6")},
                {"Bayern Munich", new Guid("63ee2b36-64d2-45e1-b686-1e9d3f25e17e")},
                {"Borussia Dortmund", new Guid("d10f8323-0b4a-4d54-9234-8b0356b5d1e0")},
                {"RB Leipzig", new Guid("ac4e70de-5f85-4a50-b78f-7896c0c6de5c")},
                {"Bayer Leverkusen", new Guid("a8910b89-40c6-4bb5-a4b5-b8d6f13b876c")},
                {"VfL Wolfsburg", new Guid("f87f7db0-0e7d-4c9f-9e05-6837c1c070d7")},
                {"Eintracht Frankfurt", new Guid("d4ba4e04-cdbf-4b6d-94d7-4b90e12dd135")}
            };

            var blobManager = new Dictionary<string, Dictionary<string, BlobItem>>();
            
            var sessionsDetails = new ClientsSessionsDetailsAdmin();
            sessionsDetails.ClientBlobsList = new List<ClientBlobsAdmin>();

            foreach (var blob in validBlobs.OrderByDescending(x => x.Properties.LastModified).ToList())
            {
                try
                {
                    var blobSections = blob.Name.Split("-").ToList();
                    var clientName = blobSections[1];
                    var importSessionId =
                        $"{blobSections[2]}-{blobSections[3]}-{blobSections[4]}-{blobSections[5]}-{blobSections[6]}";

                    Console.WriteLine(blob.Name + "      " + blob.Properties.CreatedOn + "   " +
                                      blob.Properties.ContentLength);
                    if (!blobManager.ContainsKey(clientName))
                    {
                        blobManager.Add(clientName, new Dictionary<string, BlobItem>());
                    }
                    else
                    {
                        if (!blobManager[clientName].ContainsKey(importSessionId) && blobManager[clientName].Count < 40)
                        {
                            blobManager[clientName].Add(importSessionId, blob);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            var blobManagerInOrder = new Dictionary<string, Dictionary<string, BlobItem>>();
            foreach (var clientNamesToId in clientNamesToIds)
            {
                if (blobManager.ContainsKey(clientNamesToId.Key))
                {
                    var dicSortedByDate = 
                        blobManager[clientNamesToId.Key].OrderBy(x => x.Value.Properties.LastModified?.LocalDateTime).ToDictionary(x => x.Key, x => x.Value);                    
                    blobManagerInOrder.Add(clientNamesToId.Key, dicSortedByDate);
                }
                else
                {
                    blobManagerInOrder.Add(clientNamesToId.Key, new Dictionary<string, BlobItem>());                    
                }
            }
            
            
            foreach (var keyValuePair in blobManagerInOrder)
            {
                var currentClientId = clientNamesToIds[keyValuePair.Key];
                var clientBlobs = new ClientBlobsAdmin
                {
                    ClientId = currentClientId.ToString(),
                    ClientName = keyValuePair.Key,
                };
                
                clientBlobs.ClientBlobsList = new List<BlobDetailsAdmin>();

                
                var ixx = 1;
                foreach (var valuePair in keyValuePair.Value)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 51);
                    ixx = randomNumber;
                    var blobDetails = new BlobDetailsAdmin
                    {
                        BlobName = valuePair.Key,
                        BlobModifiedDate = valuePair.Value.Properties.LastModified?.LocalDateTime.ToShortDateString(),
                        BlobProvidersCount = $"{2000 * ixx}",
                        BlobLocationsCount = $"{2000 * ixx}",
                        BlobProvidersCountPass = $"{1500 * ixx}",
                        BlobLocationsCountPass = $"{1500 * ixx}",
                        SessionInputSize =  "1 MB",
                        SessionBackupSize =  "1.3 MB",                        
                        Status =  StatusEnum.Pass,
                        HasProviders = true,
                        HasLocations = true,
                    };
                    ixx++;
                    
                    clientBlobs.ClientBlobsList.Add(blobDetails);                    
                }
                sessionsDetails.ClientBlobsList.Add(clientBlobs);                
            }
            
            if (string.IsNullOrEmpty(clientId))
            {
                return sessionsDetails;
            }

            var client = sessionsDetails.ClientBlobsList.Where(x => x.ClientId == clientId).ToList();
            var singleClientSessionsDetails = new ClientsSessionsDetailsAdmin() {ClientBlobsList = client};

            return singleClientSessionsDetails;

        }
    }
}