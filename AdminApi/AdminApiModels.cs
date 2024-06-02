using System.Text.Json;

namespace AdminApi
{
    public class ExceptionClass
    {
        public string Details { get; set; }
        public string InnerDetails { get; set; }
    }
    public class LocationEntry
    {
        public string LocationName { get; set; }
        public string MessageType { get; set; }
        public string Address1 { get; set; }
        public string ClientId { get; set; }
        public string MarketEntityId { get; set; }
        public string TrackingId { get; set; }
        public string Error => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public string JsonValue => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });

    }

    public class ProviderEntry
    {
        public string Npi { get; set; }
        public string MessageType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MarketEntityId { get; set; }
        public string LocationName { get; set; }
        public string TrackingId { get; set; }
        public string Error => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public string JsonValue => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
    }

    public class ProviderEntryWithLocations : ProviderEntry
    {
        public List<LocationEntry> ProviderLocationEntries { get; set; }
    }
    public class ProviderLocationEntry
    {
        public string Npi { get; set; }
        public string MessageType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MarketEntityId { get; set; }
        public string LocationName { get; set; }
        public string TrackingId { get; set; }

        public string Error => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public string JsonValue => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });


        public List<LocationEntry> Locations { get; set; }
    }

    public class ProviderGroupEntry
    {
        public string GroupId { get; set; }
        public string Error => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });

        public string JsonValue => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public List<ProviderEntry> Providers { get; set; }

    }

    public class LocationGroupEntry
    {
        public string GroupId { get; set; }
        public string Error => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public string JsonValue => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public List<LocationEntry> Locations { get; set; }

    }

    public class ProviderLocationGroupEntry
    {
        public string GroupId { get; set; }

        public string Error => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });
        public string JsonValue => JsonSerializer.Serialize<ExceptionClass>(new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        });

        public List<LocationEntry> Locations { get; set; }
    }



    public class LocationsFromProviderTableColumnsCaptions
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public string Column4 { get; set; }
        public string Column5 { get; set; }
    }

    public class LocationsTableColumnsCaptions : LocationsFromProviderTableColumnsCaptions
    {
        public string Column6 { get; set; }
    }

    public class ProvidersTableColumnsCaptions
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public string Column4 { get; set; }
        public string Column5 { get; set; }
        public string Column6 { get; set; }
        public string Column7 { get; set; }
    }

    public class ProcessList
    {
        public List<string> provs { get; set; }
        public List<string> locs { get; set; }
        public List<string> provLocs { get; set; }
        public List<string> spec { get; set; }
    }
    public class ImporterSessionResults
    {
        public string ProvidersSPECHeader { get; set; }
        public string ProvidersLocationsBINSTHeader { get; set; }
        public string ProvidersLocationsINSTHeader { get; set; }
        public string ProvidersLocationsCTRHeader { get; set; }
        public string ProvidersLocationsVLDHeader { get; set; }
        public string LocationsINSTHeader { get; set; }
        public string LocationsCTRHeader { get; set; }
        public string LocationsVLDHeader { get; set; }
        public string ProvidersINSTHeader { get; set; }
        public string ProvidersCTRHeader { get; set; }
        public string ProvidersVLDHeader { get; set; }
        public List<LocationEntry> LocationsVLD { get; set; }
        public List<LocationEntry> LocationsCTR { get; set; }
        public List<LocationGroupEntry> LocationsINST { get; set; }

        public List<ProviderEntry> ProvidersVLD { get; set; }
        public List<ProviderEntry> ProvidersCTR { get; set; }
        public List<ProviderGroupEntry> ProvidersINST { get; set; }

        public List<ProviderEntryWithLocations> ProvidersLocationsVLD { get; set; }
        public List<ProviderEntryWithLocations> ProvidersLocationsCTR { get; set; }
        public List<ProviderEntryWithLocations> ProvidersLocationsINST { get; set; }
        public List<LocationGroupEntry> ProvidersLocationsBINST { get; set; }

        public LocationsFromProviderTableColumnsCaptions locationsFromProviderTableColumnsCaptions { get; set; }
        public LocationsTableColumnsCaptions locationsTableColumnsCaptions { get; set; }
        public ProvidersTableColumnsCaptions providersTableColumnsCaptions { get; set; }

        public ProvidersSPECTableColumnsCaptions providersSPECTableColumnsCaptions { get; set; }

        public List<string> ProvidersSPEC { get; set; }
    }

    public class ProvidersSPECTableColumnsCaptions
    {
        public string Column1 { get; set; }
    }
    public class ImporterSessionResultsTrackingIds
    {
        public List<string> locationsVLD { get; set; }
        public List<string> locationsCTR { get; set; }
        public List<string> locationsINST { get; set; }
        public List<string> providersVLD { get; set; }
        public List<string> providersCTR { get; set; }
        public List<string> providersINST { get; set; }
        public List<string> providersLocationsVLD { get; set; }
        public List<string> providersLocationsCTR { get; set; }
        public List<string> providersLocationsINST { get; set; }
        public List<string> providersLocationsBINST { get; set; }
        public List<string> providersSPEC { get; set; }
    }



    public class MenuItem
    {
        public string MenuItemCaption { get; set; }
        public string MenuItemId { get; set; }
    }
    public class MenuNode
    {
        public string MenuNodeCaption { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }

    public class Menu
    {
        public string MenuHeader { get; set; }
        public List<MenuNode> MenuNodes { get; set; }
    }

    public class ClientsSessionsDetails
    {
        public List<ClientBlobs> ClientBlobsList { get; set; }
    }
    public class ClientBlobs
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSessionId { get; set; }
        public bool HasProviders { get; set; }
        public bool HasLocations { get; set; }
        public List<BlobDetails> ClientBlobsList { get; set; }
    }
    public class BlobDetails
    {
        public string BlobName { get; set; }
        public string BlobType { get; set; }
        public string BlobModifiedDate { get; set; }
        public string BlobContentSize { get; set; }
        public string BlobContentType { get; set; }



        public int BlobNumberOfProvidersReceived { get; set; }
        public int BlobNumberOfProvidersFailedValidations { get; set; }
        public int BlobNumberOfProvidersFailedCreation { get; set; }
        public int BlobNumberOfProvidersFailedInsertion { get; set; }
        public int BlobNumberOfProvidersMissingSpecialties { get; set; }

        public int BlobNumberOfLocationsReceived { get; set; }
        public int BlobNumberOfLocationsFailedValidations { get; set; }
        public int BlobNumberOfLocationsFailedCreation { get; set; }
        public int BlobNumberOfLocationsFailedInsertion { get; set; }
        public string BlobProvidersCount { get; set; }
        public string BlobLocationsCount { get; set; }

        public string BlobProvidersCountPass { get; set; }
        public string BlobLocationsCountPass { get; set; }

    }
}