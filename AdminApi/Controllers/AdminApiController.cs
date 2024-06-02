using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace AdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminApiController : ControllerBase
{

    private readonly ILogger<AdminApiController> _logger;

    private IConfiguration _configuration;
    
    public AdminApiController(ILogger<AdminApiController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("ProcessSelections")]

    public ImporterSessionResults ProcessSelections(object importerSessionResultsTrackingIds)
    {
        var myobject = JsonSerializer.Deserialize<ImporterSessionResultsTrackingIds>(importerSessionResultsTrackingIds.ToString());       
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
                            MenuItemCaption = "UTAH",
                            MenuItemId = "740039a3-efb1-42e8-bda8-3196dc64620b",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "KANSAS",
                            MenuItemId = "0ecf8e08-333b-4d7d-bcb3-b687de7726a8",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "MAIN",
                            MenuItemId = "7bc6ca72-3f43-4ad2-8004-fa257e751678",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "NEW YORK",
                            MenuItemId = "16c5026f-0417-45a4-9731-9a08dd7df830",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "FLORIDA",
                            MenuItemId = "7cf2580c-abb5-43cc-82b9-86706eaa1fb8",
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
                            MenuItemCaption = "UTAH",
                            MenuItemId = "740039a3-efb1-42e8-bda8-3196dc64620b",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "KANSAS",
                            MenuItemId = "0ecf8e08-333b-4d7d-bcb3-b687de7726a8",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "MAIN",
                            MenuItemId = "7bc6ca72-3f43-4ad2-8004-fa257e751678",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "NEW YORK",
                            MenuItemId = "16c5026f-0417-45a4-9731-9a08dd7df830",
                        },
                        new MenuItem
                        {
                            MenuItemCaption = "FLORIDA",
                            MenuItemId = "7cf2580c-abb5-43cc-82b9-86706eaa1fb8",
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

    [HttpGet]
    [Route("GetSessions")]
    public ClientsSessionsDetails GetSessions(string? clientId = null)
    {
        return new SessionsDetailsManager().GetSessions(clientId);
    }

    [HttpGet]

    [Route("GetSessionResults")]
    public ImporterSessionResults GetSessionResults(string? clientId = null, string? sessionId = null)
    {
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
    }

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
        importerBlobStorage.ProvidersLocationsVLDHeader = "Header PLoc VLD";
        importerBlobStorage.ProvidersLocationsVLD =
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

        importerBlobStorage.ProvidersLocationsCTRHeader = "Header PLoc CTR";
        importerBlobStorage.ProvidersLocationsCTR =
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

        importerBlobStorage.ProvidersLocationsINSTHeader = "Header PLoc INST";
        importerBlobStorage.ProvidersLocationsINST =
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

        importerBlobStorage.ProvidersLocationsBINSTHeader = "Header PLoc BINS";
        importerBlobStorage.ProvidersLocationsBINST =
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
        importerBlobStorage.LocationsVLDHeader = "Header Location VLD";
        importerBlobStorage.LocationsVLD =
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

        importerBlobStorage.LocationsCTRHeader = "Header Location CTR";
        importerBlobStorage.LocationsCTR =
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

        importerBlobStorage.LocationsINSTHeader = "Header Location INST";
        importerBlobStorage.LocationsINST =
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
        importerBlobStorage.ProvidersVLDHeader = "Header Provider VLD";
        importerBlobStorage.ProvidersVLD =
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

        importerBlobStorage.ProvidersCTRHeader = "Header Provider CTR";
        importerBlobStorage.ProvidersCTR =
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

        importerBlobStorage.ProvidersINSTHeader = "Header Provider INST";
        importerBlobStorage.ProvidersINST =
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
        importerBlobStorage.ProvidersSPECHeader = "Header SPEC";
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
}