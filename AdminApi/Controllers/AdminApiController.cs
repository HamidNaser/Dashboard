using System.Diagnostics;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Web.Http;


using Azure.Storage.Blobs.Models;
using Importer.Core.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminApiController : ControllerBase
{
    // Add code to handle SSO and roles.
    // Add code to check if you have right access to call api.
    private string OAuthToken
    {
        get
        {
            if (!HttpContext.Session.TryGetValue("OAuthToken", out byte[]? oauthToken) || oauthToken == null)
            {
                return string.Empty;
            }

            return Encoding.UTF8.GetString(oauthToken);
        }
        set
        {
            HttpContext.Session.Set("OAuthToken", Encoding.UTF8.GetBytes(value));
        }
    }

    private ImporterSessionRestoreData ImporterSessionRestoreData
    {
        get
        {
            if (!HttpContext.Session.TryGetValue("ImporterSessionRestoreData", out byte[]? sessionData) || sessionData == null)
            {
                return new ImporterSessionRestoreData(); // Return a default instance or handle null appropriately
            }

            // Deserialize the byte array to ImporterSessionRestoreDataFull object
            string json = Encoding.UTF8.GetString(sessionData);
            return JsonConvert.DeserializeObject<ImporterSessionRestoreData>(json);
        }
        set
        {
            // Serialize the ImporterSessionRestoreDataFull object to a byte array and store it in the session
            string json = JsonConvert.SerializeObject(value);
            HttpContext.Session.Set("ImporterSessionRestoreData", Encoding.UTF8.GetBytes(json));
        }
    }

    private ImporterSessionInputLite ImporterSessionInputData
    {
        get
        {
            if (!HttpContext.Session.TryGetValue("ImporterSessionInputData", out byte[]? sessionData) || sessionData == null)
            {
                return new ImporterSessionInputLite(); // Return a default instance or handle null appropriately
            }

            // Deserialize the byte array to ImporterSessionRestoreDataFull object
            string json = Encoding.UTF8.GetString(sessionData);
            return JsonConvert.DeserializeObject<ImporterSessionInputLite>(json) ?? new ImporterSessionInputLite();
        }
        set
        {
            // Serialize the ImporterSessionRestoreDataFull object to a byte array and store it in the session
            string json = JsonConvert.SerializeObject(value);
            HttpContext.Session.Set("ImporterSessionInputData", Encoding.UTF8.GetBytes(json));
        }
    }
    
    private List<BlobItem> BlobsX
    {
        get
        {
            if (!HttpContext.Session.TryGetValue("Blobs", out byte[]? blobData) || blobData == null)
            {
                return new List<BlobItem>();
            }
            return JsonConvert.DeserializeObject<List<BlobItem>>(Encoding.UTF8.GetString(blobData)) ?? new List<BlobItem>();
        }
        set
        {
            HttpContext.Session.Set("Blobs", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }
    }
    
    [HttpGet]
    [Route("api/refresh-data")]
    public IActionResult RefreshData()
    {
        // Retrieve the data from the database
        var newData = GetDataFromDatabase();

        // Update the session with the new data
        HttpContext.Session.Set("Blobs", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newData)));

        return Ok();
    }

    private List<BlobItem> GetDataFromDatabase()
    {
        // Implement your logic to retrieve data from the database
        // For example:
        // var newData = _dbContext.Blobs.ToList();
        // return newData;
        return new List<BlobItem>();
    }
    
    private readonly IServiceProvider _serviceProvider;    
    private IConfiguration _configuration;
    public AdminApiController(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }    
    [HttpPost]
    [Route("ProcessSelections")]

    public ImporterSessionResults ProcessSelections(object importerSessionResultsTrackingIds)
    {
        var myobject = JsonSerializer.Deserialize<ImporterSessionResultsIds>(importerSessionResultsTrackingIds.ToString());
/*        var prov = new ProviderEntry();
        myobject.providersVLD = new List<ProviderEntry>();
        prov.TrackingId = "71320397-aa37-42e6-83e8-883456dd5501";
        myobject.providersVLD.Add(prov);
        
  */      
    
        
        Thread.Sleep(10000);

        // The frontend will show progress animation until this method returns. 
        var importerBlobStorage = new ImporterSessionResults();

        return importerBlobStorage;
    }

    [HttpPost]
    [Route("ImportFromInputData")]

    public ImporterSessionResults ImportFromInputData(object importerSessionInputIds, TargetEnvironment targetEnvironment)    
    {
        var myobject = JsonSerializer.Deserialize<ImporterSessionInputIds>(importerSessionInputIds.ToString());
/*        var prov = new ProviderEntry();
        myobject.providersVLD = new List<ProviderEntry>();
        prov.TrackingId = "71320397-aa37-42e6-83e8-883456dd5501";
        myobject.providersVLD.Add(prov);

  */      
    
        
        Thread.Sleep(10000);

        // The frontend will show progress animation until this method returns. 
        var importerBlobStorage = new ImporterSessionResults();

        return importerBlobStorage;
    }
    

    [HttpGet]
    [Route("GetMenu")]
    public Menu GetMenu()
    {
        var menu = new Menu
        {
            MenuHeader = "Matrix",
            MenuNodes = new List<MenuNode>
            {
                new MenuNode()
                {
                    MenuNodeCaption = "DASHBOARD",
                },
                new MenuNode()
                {
                    MenuNodeCaption = "CLIENTS",
                    MenuItems = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            MenuItemCaption = "Manchester United",
                            MenuItemId = "f89d2d36-7b3b-4e6a-8f43-0951f067fd11",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Liverpool",
                            MenuItemId = "ec5288e2-7f52-4a80-89e2-1f6282f51e95",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Chelsea",
                            MenuItemId = "48db09aa-af4c-4e18-a502-5a474b00e5b2",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Manchester City",
                            MenuItemId = "6bb7e7ef-2d4a-41fb-a922-f7a79d48d20a",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Tottenham Hotspur",
                            MenuItemId = "1b6a70aa-1583-4c90-81a1-ae5bbf82dbbc",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Arsenal",
                            MenuItemId = "e70dd6a5-dde1-42fb-8c4e-68ac27e97756",
                        },                        
                        new MenuItem
                        {
                            MenuItemCaption = "Real Madrid",
                            MenuItemId = "a0e8f7a1-fa8a-49e4-93ec-b1ec443f76a1",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Barcelona",
                            MenuItemId = "b5e9430c-2b86-43b1-8516-7ae1c5e79515",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Atletico Madrid",
                            MenuItemId = "a4a3fc63-9e96-49c3-968f-13273a5b6203",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Sevilla",
                            MenuItemId = "74e1c744-4f53-48e5-ba87-5879ce8716a4",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Real Sociedad",
                            MenuItemId = "43d87550-0a25-442c-b32b-2d0a8d6b67a3",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Valencia",
                            MenuItemId = "7d8a7a82-d675-4952-8163-78806f2d65a6",
                        },                        
                        new MenuItem
                        {
                            MenuItemCaption = "Bayern Munich",
                            MenuItemId = "63ee2b36-64d2-45e1-b686-1e9d3f25e17e",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Borussia Dortmund",
                            MenuItemId = "d10f8323-0b4a-4d54-9234-8b0356b5d1e0",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "RB Leipzig",
                            MenuItemId = "ac4e70de-5f85-4a50-b78f-7896c0c6de5c",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Bayer Leverkusen",
                            MenuItemId = "a8910b89-40c6-4bb5-a4b5-b8d6f13b876c",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "VfL Wolfsburg",
                            MenuItemId = "f87f7db0-0e7d-4c9f-9e05-6837c1c070d7",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Eintracht Frankfurt",
                            MenuItemId = "d4ba4e04-cdbf-4b6d-94d7-4b90e12dd135",
                        }                        
                    }
                },
                new MenuNode()
                {
                    MenuNodeCaption = "TOOLS",
                    MenuItems = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            MenuItemCaption = "Manchester United",
                            MenuItemId = "f89d2d36-7b3b-4e6a-8f43-0951f067fd11",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Liverpool",
                            MenuItemId = "ec5288e2-7f52-4a80-89e2-1f6282f51e95",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Chelsea",
                            MenuItemId = "48db09aa-af4c-4e18-a502-5a474b00e5b2",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Manchester City",
                            MenuItemId = "6bb7e7ef-2d4a-41fb-a922-f7a79d48d20a",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Tottenham Hotspur",
                            MenuItemId = "1b6a70aa-1583-4c90-81a1-ae5bbf82dbbc",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Arsenal",
                            MenuItemId = "e70dd6a5-dde1-42fb-8c4e-68ac27e97756",
                        },                        
                        new MenuItem
                        {
                            MenuItemCaption = "Real Madrid",
                            MenuItemId = "a0e8f7a1-fa8a-49e4-93ec-b1ec443f76a1",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Barcelona",
                            MenuItemId = "b5e9430c-2b86-43b1-8516-7ae1c5e79515",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Atletico Madrid",
                            MenuItemId = "a4a3fc63-9e96-49c3-968f-13273a5b6203",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Sevilla",
                            MenuItemId = "74e1c744-4f53-48e5-ba87-5879ce8716a4",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Real Sociedad",
                            MenuItemId = "43d87550-0a25-442c-b32b-2d0a8d6b67a3",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Valencia",
                            MenuItemId = "7d8a7a82-d675-4952-8163-78806f2d65a6",
                        },                        
                        new MenuItem
                        {
                            MenuItemCaption = "Bayern Munich",
                            MenuItemId = "63ee2b36-64d2-45e1-b686-1e9d3f25e17e",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Borussia Dortmund",
                            MenuItemId = "d10f8323-0b4a-4d54-9234-8b0356b5d1e0",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "RB Leipzig",
                            MenuItemId = "ac4e70de-5f85-4a50-b78f-7896c0c6de5c",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Bayer Leverkusen",
                            MenuItemId = "a8910b89-40c6-4bb5-a4b5-b8d6f13b876c",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "VfL Wolfsburg",
                            MenuItemId = "f87f7db0-0e7d-4c9f-9e05-6837c1c070d7",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "Eintracht Frankfurt",
                            MenuItemId = "d4ba4e04-cdbf-4b6d-94d7-4b90e12dd135",
                        }                        
                    }
                },
                new MenuNode()
                {
                    MenuNodeCaption = "SETTINGS",
                },
            }

        };

        return menu;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var googleClientId = _configuration["Google:ClientId"];
        var redirectUri = _configuration["Google:RedirectUri"];
        var authUri = $"https://accounts.google.com/o/oauth2/v2/auth" +
                      $"?client_id={googleClientId}" +
                      $"&redirect_uri={redirectUri}" +
                      $"&response_type=code" +
                      $"&scope=email";

        return Redirect(authUri);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        var googleClientId = _configuration["Google:ClientId"];
        var googleClientSecret = _configuration["Google:ClientSecret"];
        var redirectUri = _configuration["Google:RedirectUri"];
        var tokenUri = "https://oauth2.googleapis.com/token";

        using var client = new HttpClient();

        var tokenRequest = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("client_id", googleClientId),
            new KeyValuePair<string, string>("client_secret", googleClientSecret),
            new KeyValuePair<string, string>("redirect_uri", redirectUri),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
        });

        var tokenResponse = await client.PostAsync(tokenUri, tokenRequest);
        var tokenContent = await tokenResponse.Content.ReadAsStringAsync();

        OAuthToken = tokenContent;
        // Handle token response (e.g., store token in session or database)

        return Ok(tokenContent);
    }
    

    [HttpGet]
    [Route("GetSessions")]
    public ClientsSessionsDetailsAdmin GetSessions(string? clientId = null)
    {
        string json = System.IO.File.ReadAllText($"C:\\AdminImport\\Data\\Sessions.json");
        var clientsSessionsDetailsAdmin = JsonConvert.DeserializeObject<ClientsSessionsDetailsAdmin>(json);

        return clientsSessionsDetailsAdmin;
        //var connectionString = _configuration.GetValue<string>("BLOB_STORAGE_CONNECTION_STRING");   

        //return new SessionsDetailsManager().GetSessions(connectionString, clientId);
    }

    [HttpPost]
    [Route("DownloadSessionResults")]
    public IActionResult DownloadSessionResults()
    {
        var fileFullName = $"C:\\AdminImport\\Data\\DownloadResults.json";
        if (string.IsNullOrEmpty(fileFullName) || !System.IO.File.Exists(fileFullName))
        {
            return NotFound();
        }

        var fileInfo = new FileInfo(fileFullName);
        var stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
        return File(stream, "application/octet-stream", fileInfo.Name);
    }

    [HttpPost]
    [Route("DownloadInput")]
    public IActionResult DownloadInput(string? clientId = null, string? sessionId = null)
    {
        var fileFullName = $"C:\\AdminImport\\Data\\DownloadInput.json";
        if (string.IsNullOrEmpty(fileFullName) || !System.IO.File.Exists(fileFullName))
        {
            return NotFound();
        }

        var fileInfo = new FileInfo(fileFullName);
        var stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
        return File(stream, "application/octet-stream", fileInfo.Name);
    }

    [HttpPost]
    [Route("DownloadLiveInput")]
    public IActionResult DownloadLiveInput(string clientId)
    {
        var fileFullName = $"C:\\AdminImport\\Data\\DownloadInputLive.json";
        if (string.IsNullOrEmpty(fileFullName) || !System.IO.File.Exists(fileFullName))
        {
            return NotFound();
        }

        var fileInfo = new FileInfo(fileFullName);
        var stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
        return File(stream, "application/octet-stream", fileInfo.Name);
    }
    
    [HttpPost]
    [Route("DownloadBackup")]
    public IActionResult DownloadBackup(string? clientId = null, string? sessionId = null)
    {
        var fileFullName = $"C:\\AdminImport\\Data\\DownloadBackup.json";
        if (string.IsNullOrEmpty(fileFullName) || !System.IO.File.Exists(fileFullName))
        {
            return NotFound();
        }

        var fileInfo = new FileInfo(fileFullName);
        var stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
        return File(stream, "application/octet-stream", fileInfo.Name);
    }

    [HttpPost]
    [Route("RestoreBackup")]
    public IActionResult RestoreBackup(string clientId, string sessionId)
    {
        var fileFullName = $"C:\\AdminImport\\Data\\DownloadBackup.json";
        if (string.IsNullOrEmpty(fileFullName) || !System.IO.File.Exists(fileFullName))
        {
            return NotFound();
        }

        var fileInfo = new FileInfo(fileFullName);
        var stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
        return File(stream, "application/octet-stream", fileInfo.Name);
    }
    
    [HttpPost]
    [Route("OpenHelpUrl")]
    public IActionResult OpenHelpUrl(string? clientId = null, string? sessionId = null)
    {
        // URL to open
        string url = "https://www.cnn.com/";

        // Open the URL in the default browser
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });

        return Ok();
    }    
    private string GetFileFullPath(DownloadType downloadType)
    {
        return $"C:\\AdminImport\\Data\\Download{downloadType}.json";
    }
    
    [HttpGet]
    [Route("GetSessionResults")]
    public ImporterSessionResults GetSessionResults(string? clientId = null, string? sessionId = null)
    {
        string json = System.IO.File.ReadAllText($"C:\\AdminImport\\Data\\SessionResults.json");
        var importerSessionResults = JsonConvert.DeserializeObject<ImporterSessionResults>(json);
        //var importerSessionResults  = importerClient.GetImporterSessionDetails(runInfo);
/*
        var runInfo = new RunInfo()
        {
            ClientId = "0b814461-02ff-4c76-95cd-3c5c166e3c55",//clientId,
            ImportSessionId = "c6f8d8a4-8a89-40a6-bd16-aac57e9e4daa",
            RunUsingTraceBlobFromAzure = true,
            AppConfigurations = _configuration
        };
        
        var importerSessionResults = _lomaLindaClient.GetImporterSessionDetails(runInfo);
  */      
        importerSessionResults.ProvidersVLDHeader = "Failed Validation"; 
        importerSessionResults.ProvidersCTRHeader = "Failed Creating Connect Providers";    
        importerSessionResults.ProvidersINSTHeader = "Failed Batch Update";
        
        importerSessionResults.LocationsVLDHeader = "Failed Validation";
        importerSessionResults.LocationsCTRHeader = "Failed Creating Connect Locations";
        importerSessionResults.LocationsINSTHeader = "Failed Batch Update";
        
        importerSessionResults.ProvidersLocationsVLDHeader = "Failed Provider Location Validation";        
        importerSessionResults.ProvidersLocationsCTRHeader = "Failed Creating Connect Locations";
        importerSessionResults.ProvidersLocationsBINSTHeader = "Failed Provider Location Batch Update";
        
        SetProvidersTableColumnsNames(importerSessionResults);
        SetLocationsTableColumnsNames(importerSessionResults);
        SetLocationsFromProviderTableColumnsNames(importerSessionResults);
        SetProvidersSPECTableColumnsNames(importerSessionResults);

        //SetProviders(importerSessionResults);
        //SetLocations(importerSessionResults);
        //SetProviderLocations(importerSessionResults);
        
        SetProviderSpecialties(importerSessionResults);

        return importerSessionResults;
        
        /*
        var importerBlobStorage = new ImporterSessionResults();

        SetProvidersTableColumnsNames(importerBlobStorage);
        SetLocationsTableColumnsNames(importerBlobStorage);
        SetLocationsFromProviderTableColumnsNames(importerBlobStorage);
        SetProvidersSPECTableColumnsNames(importerBlobStorage);

        SetProviders(importerBlobStorage);
        SetLocations(importerBlobStorage);
        SetProviderLocations(importerBlobStorage);
        SetProviderSpecialties(importerBlobStorage);

        return importerBlobStorage;
        */
    }

    [HttpGet]
    [Route("GetSessionInputEntryJsonValue")]
    public string GetInputEntryJsonValue(string trackingId, JsonRestoreType jsonRestoreType, string? clientId = null, string? sessionId = null)
    {
        string json = System.IO.File.ReadAllText($"C:\\AdminImport\\Data\\SessionInput.json");
        var importerSessionInput = JsonConvert.DeserializeObject<ImporterSessionInput>(json);
        
        Task.Delay(10000);

        return importerSessionInput?.GetRestoreJsonValue(trackingId, jsonRestoreType) ?? string.Empty;
    }
    
    
    [HttpGet]
    [Route("GetSessionInput")]
    public ImporterSessionInputLite GetSessionInput(string? clientId = null, string? sessionId = null)
    {

        
        string json = System.IO.File.ReadAllText($"C:\\AdminImport\\Data\\SessionInput.json");
        var importerSessionInput = JsonConvert.DeserializeObject<ImporterSessionInput>(json);
        
        return importerSessionInput?.GetSessionInputData() ?? new ImporterSessionInputLite();        
        //var importerSessionResults  = importerClient.GetImporterSessionDetails(runInfo);
/*
        var runInfo = new RunInfo()
        {
            ClientId = "0b814461-02ff-4c76-95cd-3c5c166e3c55",//clientId,
            ImportSessionId = "c6f8d8a4-8a89-40a6-bd16-aac57e9e4daa",
            RunUsingTraceBlobFromAzure = true,
            AppConfigurations = _configuration
        };
        
        var importerSessionResults = _lomaLindaClient.GetImporterSessionDetails(runInfo);
  */   

        //SetProvidersTableColumnsNames(importerSessionResults);
        //SetLocationsTableColumnsNames(importerSessionResults);

        //return importerSessionInput;
        
        /*
        var importerBlobStorage = new ImporterSessionResults();

        SetProvidersTableColumnsNames(importerBlobStorage);
        SetLocationsTableColumnsNames(importerBlobStorage);
        SetLocationsFromProviderTableColumnsNames(importerBlobStorage);
        SetProvidersSPECTableColumnsNames(importerBlobStorage);

        SetProviders(importerBlobStorage);
        SetLocations(importerBlobStorage);
        SetProviderLocations(importerBlobStorage);
        SetProviderSpecialties(importerBlobStorage);

        return importerBlobStorage;
        */
    }

    [HttpGet]
    [Route("GetDataFromLiveFeed")]
    public ImporterSessionInputLite GetDataFromLiveFeed(string? clientId = null)
    {
        string json = System.IO.File.ReadAllText($"C:\\AdminImport\\Data\\SessionInput.json");
        var importerSessionInput = JsonConvert.DeserializeObject<ImporterSessionInput>(json);

        return importerSessionInput?.GetSessionInputData() ?? new ImporterSessionInputLite();
        //var importerSessionResults  = importerClient.GetImporterSessionDetails(runInfo);
/*
        var runInfo = new RunInfo()
        {
            ClientId = "0b814461-02ff-4c76-95cd-3c5c166e3c55",//clientId,
            ImportSessionId = "c6f8d8a4-8a89-40a6-bd16-aac57e9e4daa",
            RunUsingTraceBlobFromAzure = true,
            AppConfigurations = _configuration
        };
        
        var importerSessionResults = _lomaLindaClient.GetImporterSessionDetails(runInfo);
  */   

        //SetProvidersTableColumnsNames(importerSessionResults);
        //SetLocationsTableColumnsNames(importerSessionResults);

        //return importerSessionInput;
        
        /*
        var importerBlobStorage = new ImporterSessionResults();

        SetProvidersTableColumnsNames(importerBlobStorage);
        SetLocationsTableColumnsNames(importerBlobStorage);
        SetLocationsFromProviderTableColumnsNames(importerBlobStorage);
        SetProvidersSPECTableColumnsNames(importerBlobStorage);

        SetProviders(importerBlobStorage);
        SetLocations(importerBlobStorage);
        SetProviderLocations(importerBlobStorage);
        SetProviderSpecialties(importerBlobStorage);

        return importerBlobStorage;
        */
    }

    [HttpGet]
    [Route("GetRestoreData")]
    public ImporterSessionRestoreDataLite GetRestoreData(string? clientId = null, string? sessionId = null)
    {
        var importerSessionRestoreData = GetSampleImporterSessionRestoreDataFull();

        ImporterSessionRestoreData = importerSessionRestoreData; 

        Task.Delay(10000);

        return importerSessionRestoreData.GetImporterSessionRestoreData();
    }

    [HttpGet]
    [Route("GetRestoreGroupEntryJsonValue")]
    public string GetRestoreGroupEntryJsonValue(string gruopId, JsonRestoreType jsonRestoreType, string? clientId = null, string? sessionId = null)
    {
       var importerSessionRestoreData = GetSampleImporterSessionRestoreDataFull();

       Task.Delay(10000);

        return importerSessionRestoreData.GetRestoreJsonValue(gruopId, jsonRestoreType);
    }

    private static ImporterSessionRestoreData GetSampleImporterSessionRestoreDataFull()
    {
        string restoreProviderJsonValue = @"
        {
            ""JsonValue"": {
                ""__type"": ""Importers.Models.ApiModels.Provider, Importers.Models"",
                ""NPI"": ""1234567890"",
                ""FirstName"": ""John"",
                ""LastName"": ""Doe"",
                ""MarketEntityId"": 0,
                ""Specialty"": ""Cardiology"",
                ""SubSpecialty"": ""Interventional Cardiology"",
                ""PhoneNumber"": ""123-456-7890"",
                ""MiddleName"": ""M"",
                ""Degree"": ""MD"",
                ""ProfileURL"": ""https://example.com/provider/john-doe"",
                ""PhotoURL"": ""https://example.com/photos/john-doe.jpg"",
                ""FaxNumber"": ""123-456-7891"",
                ""Employed"": ""Yes"",
                ""AcceptingNew"": ""1"",
                ""Gender"": ""MALE"",
                ""LocationID"": ""1001"",
                ""locations"": [
                    {
                        ""Type"": ""OFFICE"",
                        ""LocationName"": ""Main Street Cardiology Clinic"",
                        ""LocationID"": ""1001"",
                        ""Address1"": ""123 Main Street"",
                        ""MarketEntityId"": 0,
                        ""EntityId"": ""1001"",
                        ""Address2"": ""Suite 101"",
                        ""City"": ""Anytown"",
                        ""State"": ""California"",
                        ""ZipCode"": ""12345"",
                        ""PhoneNumber"": ""123-456-7890"",
                        ""WebsiteURL"": ""https://example.com/locations/main-street"",
                        ""FaxNumber"": ""123-456-7891"",
                        ""PhotoURL"": ""https://example.com/photos/main-street.jpg"",
                        ""Longitude"": ""-117.123456"",
                        ""Latitude"": ""34.123456"",
                        ""Open247"": ""FALSE"",
                        ""MondayOpen"": ""08:00"",
                        ""MondayClose"": ""17:00"",
                        ""TuesdayOpen"": ""08:00"",
                        ""TuesdayClose"": ""17:00"",
                        ""WednesdayOpen"": ""08:00"",
                        ""WednesdayClose"": ""17:00"",
                        ""ThursdayOpen"": ""08:00"",
                        ""ThursdayClose"": ""17:00"",
                        ""FridayOpen"": ""08:00"",
                        ""FridayClose"": ""17:00"",
                        ""SaturdayOpen"": """",
                        ""SaturdayClose"": """",
                        ""SundayOpen"": """",
                        ""SundayClose"": """",
                        ""UrgentCare"": ""FALSE"",
                        ""EmergencyDept"": ""FALSE""
                    }
                ]
            }
        }";
       
        var npis = new List<string>
        {
            "1234567890",
            "2345678901",
            "3456789012",
            "4567890123",
            "5678901234"
        };

        var importerSessionRestoreData = new ImporterSessionRestoreData();
        importerSessionRestoreData.RestoreProviderEntries = new List<RestoreEntry>();
        
        for (var i = 0; i < 5; i++)
        {
            var resProvider = new RestoreEntry(Guid.NewGuid().ToString(), restoreProviderJsonValue, npis);
            importerSessionRestoreData.RestoreProviderEntries.Add(resProvider);            
        }   
        
        
        string restoreLocationJsonValue = @"
        {
            ""JsonValue"": {
                ""__type"": ""Importers.Models.ApiModels.LomaLinda.LomaLindaLocation, Importers.Models"",
                ""Type"": ""OFFICE"",
                ""LocationName"": ""Example Location"",
                ""LocationID"": ""101"",
                ""Address1"": ""123 Main Street"",
                ""MarketEntityId"": 0,
                ""EntityId"": ""101"",
                ""Address2"": ""Suite 101"",
                ""City"": ""Example City"",
                ""State"": ""California"",
                ""ZipCode"": ""12345"",
                ""PhoneNumber"": ""123-456-7890"",
                ""WebsiteURL"": ""https://example.com"",
                ""FaxNumber"": ""123-456-7891"",
                ""PhotoURL"": ""https://example.com/photo.jpg"",
                ""Longitude"": ""-117.123456"",
                ""Latitude"": ""34.123456"",
                ""Open247"": ""FALSE"",
                ""MondayOpen"": ""08:00"",
                ""MondayClose"": ""17:00"",
                ""TuesdayOpen"": ""08:00"",
                ""TuesdayClose"": ""17:00"",
                ""WednesdayOpen"": ""08:00"",
                ""WednesdayClose"": ""17:00"",
                ""ThursdayOpen"": ""08:00"",
                ""ThursdayClose"": ""17:00"",
                ""FridayOpen"": ""08:00"",
                ""FridayClose"": ""17:00"",
                ""SaturdayOpen"": """",
                ""SaturdayClose"": """",
                ""SundayOpen"": """",
                ""SundayClose"": """",
                ""UrgentCare"": ""FALSE"",
                ""EmergencyDept"": ""FALSE""
            }
        }";
        
        var entities = new List<string>
        {
            "allagespediatricspc49",
            "easycarewalkinclinic975",
            "eddyvilleclinic984",
            "footankleprofessionalcentersofiowa1123",
            "forefrontdermatology1127"
        };

        importerSessionRestoreData.RestoreLocationEntries = new List<RestoreEntry>();
        
        for (var i = 0; i < 5; i++)
        {
            var resLocation = new RestoreEntry(Guid.NewGuid().ToString(), restoreLocationJsonValue, entities);
            importerSessionRestoreData.RestoreLocationEntries.Add(resLocation);            
        }

        return importerSessionRestoreData;
    }

    [HttpPost]
    [Route("RestoreBackupFromFromSession")]
    public IActionResult RestoreBackupFromFromSession(
        string clientId,
        string sessionId,        
        RestoreType restoreType, 
        [FromQuery] List<string>? locationsGroups = null,
        [FromQuery] List<string>? providersGroups = null)    {
        Task.Delay(10000);        
        return Ok();
    }
    
    #region Private
    
    private void SetLocationsFromProviderTableColumnsNames(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.locationsFromProviderTableColumnsCaptions = new LocationsFromProviderTableColumnsCaptions();
        importerBlobStorage.locationsFromProviderTableColumnsCaptions.Column1 = "LocationName";
        importerBlobStorage.locationsFromProviderTableColumnsCaptions.Column2 = "MessageType";
        importerBlobStorage.locationsFromProviderTableColumnsCaptions.Column3 = "Address1";

        importerBlobStorage.locationsFromProviderTableColumnsCaptions.Column4 = "ClientId";
        importerBlobStorage.locationsFromProviderTableColumnsCaptions.Column5 = "MarketEntityId";
    }

    private void SetProvidersSPECTableColumnsNames(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.providersSPECTableColumnsCaptions = new ProvidersSPECTableColumnsCaptions();
        importerBlobStorage.providersSPECTableColumnsCaptions.Column1 = "Name";
    }

    private void SetLocationsTableColumnsNames(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.locationsTableColumnsCaptions = new LocationsTableColumnsCaptions();
        importerBlobStorage.locationsTableColumnsCaptions.Column1 = "LocationName";
        importerBlobStorage.locationsTableColumnsCaptions.Column2 = "MessageType";
        importerBlobStorage.locationsTableColumnsCaptions.Column3 = "Address1";

        importerBlobStorage.locationsTableColumnsCaptions.Column4 = "ClientId";
        importerBlobStorage.locationsTableColumnsCaptions.Column5 = "EntityId";
        importerBlobStorage.locationsTableColumnsCaptions.Column6 = "TrackingId";
    }

    private void SetProvidersTableColumnsNames(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.providersTableColumnsCaptions = new ProvidersTableColumnsCaptions();
        importerBlobStorage.providersTableColumnsCaptions.Column1 = "ProviderId";
        importerBlobStorage.providersTableColumnsCaptions.Column2 = "MessageType";
        importerBlobStorage.providersTableColumnsCaptions.Column3 = "FirstName";
        importerBlobStorage.providersTableColumnsCaptions.Column4 = "LastName";

        importerBlobStorage.providersTableColumnsCaptions.Column5 = "EntityId";
        importerBlobStorage.providersTableColumnsCaptions.Column6 = "LocationName";
        importerBlobStorage.providersTableColumnsCaptions.Column7 = "TrackingId";
    }

    private void SetProviderLocations(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.ProvidersLocationsVLDHeader = "Failed Validation";
        importerBlobStorage.providersLocationsVLD =
            Enumerable.Range(1, 5).Select(index => new ProviderEntryWithLocations()
            {
                Npi = $"ProviderId-VLD{index}",
                MessageType = $"MessageType-VLD{index}",
                FirstName = $"FirstName-VLD{index}",
                LastName = $"LastName-VLD{index}",
                MarketEntityId = $"EntityId-VLD{index}",
                LocationName = $"LocationName-VLD{index}",
                TrackingId = $"TrackingId-VLD{index}",
                ProviderLocationEntries = Enumerable.Range(1, 5).Select(iindex => new LocationEntry
                {
                    LocationName = $"LocationName-VLD{iindex}",
                    MessageType = $"MessageType-VLD{iindex}",
                    Address1 = $"Address1-VLD{iindex}",
                    ClientId = $"ClientId-VLD{iindex}",
                    MarketEntityId = $"EntityId-VLD{iindex}",
                    TrackingId = $"TrackingId-VLD{iindex}"
                })
                        .ToList()

            })
                .ToList();

        importerBlobStorage.ProvidersLocationsCTRHeader = "Failed Creating Connect Locations";
        importerBlobStorage.providersLocationsCTR =
            Enumerable.Range(1, 5).Select(index => new ProviderEntryWithLocations()
            {
                Npi = $"ProviderId-VLD{index}",
                MessageType = $"MessageType-CTR{index}",
                FirstName = $"FirstName-CTR{index}",
                LastName = $"LastName-CTR{index}",
                MarketEntityId = $"EntityId-CTR{index}",
                LocationName = $"LocationName-CTR{index}",
                TrackingId = $"TrackingId-CTR{index}",
                ProviderLocationEntries = Enumerable.Range(1, 5).Select(iindex => new LocationEntry
                {
                    LocationName = $"LocationName-CTR{iindex}",
                    MessageType = $"MessageType-CTR{iindex}",
                    Address1 = $"Address1-CTR{iindex}",
                    ClientId = $"ClientId-CTR{iindex}",
                    MarketEntityId = $"EntityId-CTR{iindex}",
                    TrackingId = $"TrackingId-CTR{iindex}"
                })
                        .ToList()

            })
                .ToList();

        importerBlobStorage.ProvidersLocationsINSTHeader = "Failed Batch Update";
        importerBlobStorage.providersLocationsBINST =
            Enumerable.Range(1, 5).Select(indexA => new ProviderEntryWithLocations()
            {
                Npi = $"ProviderId-INST{indexA}",
                MessageType = $"MessageType-INST{indexA}",
                FirstName = $"FirstName-INST{indexA}",
                LastName = $"LastName-INST{indexA}",
                MarketEntityId = $"EntityId-INST{indexA}",
                LocationName = $"LocationName-INST{indexA}",
                TrackingId = $"TrackingId-INST{indexA}",
                ProviderLocationEntries = Enumerable.Range(1, 5).Select(iindexA => new LocationEntry
                {
                    LocationName = $"LocationName-INST{iindexA}",
                    MessageType = $"MessageType-INST{iindexA}",
                    Address1 = $"Address1-INST{iindexA}",
                    ClientId = $"ClientId-INST{iindexA}",
                    MarketEntityId = $"EntityId-INST{iindexA}",
                    TrackingId = $"TrackingId-INST{iindexA}"
                })
                        .ToList()

            })
                .ToList();

        importerBlobStorage.ProvidersLocationsBINSTHeader = "Failed Batch Update";
        importerBlobStorage.providersLocationsINST =
            Enumerable.Range(1, 5).Select(index => new LocationGroupEntry()
            {
                GroupId = $"{Guid.NewGuid().ToString()}",
                Locations = Enumerable.Range(1, 5).Select(iindexPLINST => new LocationEntry()
                {
                    LocationName = $"LocationName-PLINST{iindexPLINST}",
                    MessageType = $"MessageType-PLINST{iindexPLINST}",
                    Address1 = $"Address1-PLINST{iindexPLINST}",
                    ClientId = $"ClientId-PLINST{iindexPLINST}",
                    MarketEntityId = $"EntityId-PLINST{iindexPLINST}",
                })
                        .ToList()

            })
                .ToList();

    }

    private void SetLocations(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.LocationsVLDHeader = "Failed Validation";
        importerBlobStorage.locationsVLD =
            Enumerable.Range(1, 5).Select(indexVLD => new LocationEntry
            {
                LocationName = $"LocationName-VLD{indexVLD}",
                MessageType = $"MessageType-VLD{indexVLD}",
                Address1 = $"Address1-VLD{indexVLD}",
                ClientId = $"ClientId-VLD{indexVLD}",
                MarketEntityId = $"EntityId-VLD{indexVLD}",
                TrackingId = $"TrackingId-VLD{indexVLD}"
            })
                .ToList();
        importerBlobStorage.LocationsCTRHeader = "Failed Creating Connect Locations";
        importerBlobStorage.locationsCTR =
            Enumerable.Range(1, 5).Select(indexCTR => new LocationEntry
            {
                LocationName = $"LocationName-CTR{indexCTR}",
                MessageType = $"MessageType-CTR{indexCTR}",
                Address1 = $"Address1-CTR{indexCTR}",
                ClientId = $"ClientId-CTR{indexCTR}",
                MarketEntityId = $"EntityId-CTR{indexCTR}",
                TrackingId = $"TrackingId-CTR{indexCTR}"
            })
                .ToList();

        importerBlobStorage.LocationsINSTHeader = "Failed Batch Update";
        importerBlobStorage.locationsINST =
            Enumerable.Range(1, 5).Select(index => new LocationGroupEntry()
            {
                GroupId = $"{Guid.NewGuid().ToString()}",
                Locations = Enumerable.Range(1, 5).Select(iindexLINST => new LocationEntry()
                {
                    LocationName = $"LocationName-LINST{iindexLINST}",
                    MessageType = $"MessageType-LINST{iindexLINST}",
                    Address1 = $"Address1-LINST{iindexLINST}",
                    ClientId = $"ClientId-LINST{iindexLINST}",
                    MarketEntityId = $"EntityId-LINST{iindexLINST}",
                })
                        .ToList()

            })
                .ToList();

    }
    private void SetProviders(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.ProvidersVLDHeader = "Failed Validation";
        importerBlobStorage.providersVLD =
            Enumerable.Range(1, 5).Select(indexVLD => new ProviderEntry
            {
                Npi = $"ProviderId-VLD{indexVLD}",
                MessageType = $"MessageType-VLD{indexVLD}",
                FirstName = $"FirstName-VLD{indexVLD}",
                LastName = $"LastName-VLD{indexVLD}",
                MarketEntityId = $"EntityId-VLD{indexVLD}",
                LocationName = $"LocationName-VLD{indexVLD}",
                TrackingId = $"TrackingId-VLD{indexVLD}"
            })
                .ToList();

        importerBlobStorage.ProvidersCTRHeader = "Failed Creating Connect Providers";
        importerBlobStorage.providersCTR =
            Enumerable.Range(1, 5).Select(indexCTR => new ProviderEntry
            {
                Npi = $"ProviderId-CTR{indexCTR}",
                MessageType = $"MessageType-CTR{indexCTR}",
                FirstName = $"FirstName-CTR{indexCTR}",
                LastName = $"LastName-CTR{indexCTR}",
                MarketEntityId = $"EntityId-CTR{indexCTR}",
                LocationName = $"LocationName-CTR{indexCTR}",
                TrackingId = $"TrackingId-CTR{indexCTR}"
            })
                .ToList();

        importerBlobStorage.ProvidersINSTHeader = "Failed Batch Update";
        importerBlobStorage.providersINST =
            Enumerable.Range(1, 5).Select(index => new ProviderGroupEntry()
            {
                GroupId = $"{Guid.NewGuid().ToString()}",
                Providers = Enumerable.Range(1, 5).Select(iindexPINST => new ProviderEntry()
                {
                    Npi = $"ProviderId-PINST{iindexPINST}",
                    MessageType = $"MessageType-PINST{iindexPINST}",
                    FirstName = $"FirstName-PINST{iindexPINST}",
                    LastName = $"LastName-PINST{iindexPINST}",
                    MarketEntityId = $"EntityId-PINST{iindexPINST}",
                    LocationName = $"LocationName-PINST{iindexPINST}",
                })
                        .ToList()

            })
                .ToList();

    }

    private void SetProviderSpecialties(ImporterSessionResults importerBlobStorage)
    {
        importerBlobStorage.ProvidersSPECHeader = "Missing Specialties Names"; 
        importerBlobStorage.ProvidersSPEC = new List<string>
        {
            RandomString(20),
            RandomString(20),
            RandomString(20),
            RandomString(20),
            RandomString(20),
            RandomString(20),
            RandomString(20)
        };
    }
    private readonly Random _random = new Random();

    private string RandomString(int size, bool lowerCase = false)
    {
        var builder = new StringBuilder(size);

        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65�90 / 97�122):
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.

        // char is a single Unicode character
        char offset = lowerCase ? 'a' : 'A';
        const int lettersOffset = 26; // A...Z or a..z: length=26

        for (var i = 0; i < size; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return lowerCase ? builder.ToString().ToLower() : builder.ToString();
    }

    private int RandomNumber(int min, int max)
    {
        return _random.Next(min, max);
    }

    #endregion
}