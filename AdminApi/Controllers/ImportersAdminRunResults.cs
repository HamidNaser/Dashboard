using System.Collections.Generic;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Importer.Core.Common
{
    public enum TargetEnvironment
    {
        Staging,
        Production,
    }

    public enum DownloadType
    {
        Results,
        Input,
        InputLive,
        Backup
    }
    
    public enum RestoreType
    {
        Full,
        Partial
    }

    public enum JsonRestoreType
    {
        Locations,
        Providers,
    }

    public enum MessageTypeEnum
    {
        Error_Exception,
        Error_Validation,
        Error_ProviderLocationValidation,
        Error_CreatingConnectProviderException,
        Error_CreatingConnectLocationException,
        Error_CreatingConnectProviderLocationException,
        Error_ConnectBulkUpdateException,
        Error_ProviderLocationConnectBulkUpdateException
    }

    public class ExceptionClass
    {
        public string Details { get; set; }
        public string InnerDetails { get; set; }
    }

    public class ImporterSessionRestoreData
    {
        public ImporterSessionRestoreData()
        {

        }

        public ImporterSessionRestoreData(List<RestoreEntry> restoreLocationEntries,
            List<RestoreEntry> restoreProviderEntries)
        {
            RestoreLocationEntries = restoreLocationEntries;
            RestoreProviderEntries = restoreProviderEntries;
        }

        public ImporterSessionRestoreDataLite GetImporterSessionRestoreData()
        {
            var importerSessionRestoreData = new ImporterSessionRestoreDataLite
            {
                RestoreProviderEntries = new List<RestoreEntryBase>(),
                RestoreLocationEntries = new List<RestoreEntryBase>()
            };

            RestoreLocationEntries.ForEach(x =>
            {
                importerSessionRestoreData.RestoreLocationEntries.Add(
                    new RestoreEntryBase(x.GroupId, x.EntitiesIds));
            });

            RestoreProviderEntries.ForEach(x =>
            {
                importerSessionRestoreData.RestoreProviderEntries.Add(
                    new RestoreEntryBase(x.GroupId, x.EntitiesIds));
            });

            return importerSessionRestoreData;
        }

        public List<RestoreEntry> RestoreLocationEntries { get; set; }
        public List<RestoreEntry> RestoreProviderEntries { get; set; }

        public string GetRestoreJsonValue(string groupId, JsonRestoreType restoreType)
        {
            var jsonValue = string.Empty;

            if (restoreType == JsonRestoreType.Locations)
            {
                if (RestoreLocationEntries.Where(x => x.GroupId.Equals(groupId)).ToList().Any())
                {
                    var restoreJsonValue =
                        RestoreLocationEntries.Where(x => x.GroupId.Equals(groupId))
                            .ToList().FirstOrDefault()?.RestoreJsonValue;

                    if (restoreJsonValue != null)
                    {
                        jsonValue = restoreJsonValue;
                    }
                }
            }
            else
            {
                if (RestoreProviderEntries.Where(x => x.GroupId.Equals(groupId)).ToList().Any())
                {
                    var restoreJsonValue =
                        RestoreProviderEntries.Where(x => x.GroupId.Equals(groupId))
                            .ToList().FirstOrDefault()?.RestoreJsonValue;

                    if (restoreJsonValue != null)
                    {
                        jsonValue = restoreJsonValue;
                    }
                }
            }

            return jsonValue;
        }
    }

    public class RestoreEntryBase
    {
        public RestoreEntryBase(string groupId, List<string> entitiesIds)
        {
            GroupId = groupId;
            EntitiesIds = entitiesIds;
        }

        public string GroupId { get; set; }
        public List<string> EntitiesIds { get; set; }
    }


    public class RestoreEntry : RestoreEntryBase
    {
        public RestoreEntry(string groupId, string restoreJsonValue, List<string> entitiesIds) : base(groupId,
            entitiesIds)
        {
            RestoreJsonValue = restoreJsonValue;
        }

        public string RestoreJsonValue { get; set; }
    }

    public class ImporterSessionRestoreDataLite
    {
        public ImporterSessionRestoreDataLite()
        {

        }

        public ImporterSessionRestoreDataLite(List<RestoreEntryBase> restoreLocationEntries,
            List<RestoreEntryBase> restoreProviderEntries)
        {
            RestoreLocationEntries = restoreLocationEntries;
            RestoreProviderEntries = restoreProviderEntries;
        }

        public List<RestoreEntryBase> RestoreLocationEntries { get; set; }
        public List<RestoreEntryBase> RestoreProviderEntries { get; set; }
    }

    public class LocationEntry
    {
        public LocationEntry()
        {

        }

        public LocationEntry(
            object record,
            string trackingId,
            MessageTypeEnum messageType,
            string message,
            Dictionary<string, string> trackingFields)
        {
            TrackingId = trackingId;
            MessageType = ErrorAdminUtilCaption.GetErrorCaption(messageType.ToString());
            //Error = message;
            JsonValue = record;


            foreach (var keyValuePair in trackingFields)
            {
                var propertyInfo = GetType().GetProperty(keyValuePair.Key);

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, keyValuePair.Value, null);
                }
            }

        }

        public LocationEntry(
            object record,
            string trackingId,
            Dictionary<string, string> trackingFields)
        {
            TrackingId = trackingId;
            JsonValue = record;

            foreach (var keyValuePair in trackingFields)
            {
                var propertyInfo = GetType().GetProperty(keyValuePair.Key);

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, keyValuePair.Value, null);
                }
            }

        }

        public string LocationName { get; set; }
        public string MessageType { get; set; }
        public string Address1 { get; set; }
        public string ClientId { get; set; }
        public string MarketEntityId { get; set; }
        public string TrackingId { get; set; }
        public object Error { get; set; }
        public object JsonValue { get; set; }

        public string JsonValueStr => JsonValue.ToString();
/*
         public object Error => new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        };
        public object JsonValue => new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        };

 */

    }

    public static class ErrorAdminUtilCaption
    {
        public static string GetErrorCaption(string columnValue3)
        {
            switch (columnValue3)
            {
                case "Error_Exception":
                    return "Exception thrown.";
                case "Error_Validation":
                    return "Failed Validation";
                case "Error_CreatingConnectProviderException":
                    return "Failed to create Connect.Provider";
                case "Error_CreatingConnectLocationException":
                    return "Failed to create Connect.Location";

                case "Error_CreatingConnectProviderLocationException":
                case "Error_CreatingProviderPrimaryConnectLocationException":
                    return "Failed to create provider location";

                case "Error_ProviderLocationValidation":
                    return "Failed to validate provider location";

                case "Error_ProviderLocationConnectBulkUpdateException":
                    return "Failed provider location bulk insert";

                case "Error_ConnectBulkUpdateException":
                    return "Failed bulk insert";

            }

            return string.Empty;
        }
    }

    public class ErrorMessage
    {
        public ErrorMessage(string errorTitle, string errorContent)
        {
            ErrorTitle = errorTitle;
            ErrorContent = errorContent;
        }

        public string ErrorTitle { get; set; }
        public string ErrorContent { get; set; }
    }

    public class ProviderEntry
    {
        public ProviderEntry()
        {

        }

        public ProviderEntry(
            object record,
            string trackingId,
            MessageTypeEnum messageType,
            object message,
            Dictionary<string, string> trackingFields)
        {
            TrackingId = trackingId;
            MessageType = ErrorAdminUtilCaption.GetErrorCaption(messageType.ToString());
            Error = message;
            JsonValue = record;

            foreach (var keyValuePair in trackingFields)
            {
                var propertyInfo = GetType().GetProperty(keyValuePair.Key);

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, keyValuePair.Value, null);
                }
            }
        }

        public ProviderEntry(
            object record,
            string trackingId,
            Dictionary<string, string> trackingFields)
        {
            TrackingId = trackingId;
            JsonValue = record;

            foreach (var keyValuePair in trackingFields)
            {
                var propertyInfo = GetType().GetProperty(keyValuePair.Key);

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, keyValuePair.Value, null);
                }
            }

        }

        public string ClientId { get; set; }
        public string Npi { get; set; }
        public string MessageType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MarketEntityId { get; set; }
        public string LocationName { get; set; }
        public string TrackingId { get; set; }
        public object Error { get; set; }

        public object JsonValue { get; set; }
        public string JsonValueStr => JsonValue.ToString();        
/*
        public object JsonValue => new ExceptionClass
        {
            Details = "Network Exception",
            InnerDetails = "Network Exception Inner Details"
        };
        */
    }

    public class ProviderEntryWithLocations : ProviderEntry
    {
        public ProviderEntryWithLocations()
        {

        }

        public ProviderEntryWithLocations(
            object record,
            string trackingId,
            MessageTypeEnum messageType,
            object message,
            Dictionary<string, string> trackingFields) : base(record, trackingId, messageType, message, trackingFields)
        {

        }

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
        public object Error { get; set; }

        public object JsonValue { get; set; }

        /*
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
        */
        public List<ProviderEntry> Providers { get; set; }
        public string JsonValueToBatchUpdateApi { get; set; }
    }

    public class LocationGroupEntry
    {
        public string GroupId { get; set; }
        public object Error { get; set; }
        public object JsonValue { get; set; }

        public string JsonValueStr => JsonValue.ToString();

        /*
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

         */
        public List<LocationEntry> Locations { get; set; }
        public string JsonValueToBatchUpdateApi { get; set; }
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

    public class ImporterSessionInput
    {
        public List<LocationEntry> locations { get; set; }
        public List<ProviderEntry> providers { get; set; }

        public ImporterSessionInputLite GetSessionInputData()
        {
            var importerSessionInputData = new ImporterSessionInputLite
            {
                locations = new List<LocationEntryLite>(),
                providers = new List<ProviderEntryLite>()
            };

            locations.ForEach(x =>
            {
                importerSessionInputData.locations.Add(
                    new LocationEntryLite(x));
            });

            providers.ForEach(x =>
            {
                importerSessionInputData.providers.Add(
                    new ProviderEntryLite(x));
            });

            return importerSessionInputData;
        }
        
        
        public string GetRestoreJsonValue(string trackingId, JsonRestoreType restoreType)
        {
            var jsonValue = string.Empty;

            if (restoreType == JsonRestoreType.Locations)
            {
                if (locations.Where(x => x.TrackingId.Equals(trackingId)).ToList().Any())
                {
                    var restoreJsonValue =
                        locations.Where(x => x.TrackingId.Equals(trackingId))
                            .ToList().FirstOrDefault()?.JsonValueStr;

                    if (restoreJsonValue != null)
                    {
                        jsonValue = restoreJsonValue;
                    }
                }
            }
            else
            {
                if (providers.Where(x => x.TrackingId.Equals(trackingId)).ToList().Any())
                {
                    var restoreJsonValue =
                        providers.Where(x => x.TrackingId.Equals(trackingId))
                            .ToList().FirstOrDefault()?.JsonValueStr;

                    if (restoreJsonValue != null)
                    {
                        jsonValue = restoreJsonValue;
                    }
                }
            }

            return jsonValue;
        }
        
        
    }

    public class ImporterSessionInputLite
    {
        public List<LocationEntryLite> locations { get; set; }
        public List<ProviderEntryLite> providers { get; set; }
    }

    public class ProviderEntryLite
    {
        public ProviderEntryLite(ProviderEntry providerEntry)
        {
            ClientId = providerEntry.ClientId;
            Npi = providerEntry.Npi;
            FirstName = providerEntry.FirstName;
            LastName = providerEntry.LastName;
            MarketEntityId = providerEntry.MarketEntityId;
            LocationName = providerEntry.LocationName;
            TrackingId = providerEntry.TrackingId;
        }

        public string ClientId { get; set; }
        public string Npi { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MarketEntityId { get; set; }
        public string LocationName { get; set; }
        public string TrackingId { get; set; }
    }

    public class LocationEntryLite
    {
        public LocationEntryLite(LocationEntry locationEntry)
        {
            LocationName = locationEntry.LocationName;
            Address1 = locationEntry.Address1;
            ClientId = locationEntry.ClientId;
            MarketEntityId = locationEntry.MarketEntityId;
            TrackingId = locationEntry.TrackingId;
        }

        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string ClientId { get; set; }
        public string MarketEntityId { get; set; }
        public string TrackingId { get; set; }
    }

    public enum StatusEnum
    {
        Pass,
        Fail
    }

    public class ImporterSessionResults
    {
        public StatusEnum Status { get; set; }
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
        public List<LocationEntry> locationsVLD { get; set; }
        public List<LocationEntry> locationsCTR { get; set; }
        public List<LocationGroupEntry> locationsINST { get; set; }

        public List<ProviderEntry> providersVLD { get; set; }
        public List<ProviderEntry> providersCTR { get; set; }
        public List<ProviderGroupEntry> providersINST { get; set; }

        public List<ProviderEntryWithLocations> providersLocationsVLD { get; set; }
        public List<ProviderEntryWithLocations> providersLocationsCTR { get; set; }
        public List<ProviderEntryWithLocations> providersLocationsBINST { get; set; }
        public List<LocationGroupEntry> providersLocationsINST { get; set; }

        public LocationsFromProviderTableColumnsCaptions locationsFromProviderTableColumnsCaptions { get; set; }
        public LocationsTableColumnsCaptions locationsTableColumnsCaptions { get; set; }
        public ProvidersTableColumnsCaptions providersTableColumnsCaptions { get; set; }

        public ProvidersSPECTableColumnsCaptions providersSPECTableColumnsCaptions { get; set; }

        public List<string> ProvidersSPEC { get; set; }

        public string option { get; set; }

        public object LocationsTracer { get; set; }
        public object ProvidersTracer { get; set; }
    }

    public class ProvidersSPECTableColumnsCaptions
    {
        public string Column1 { get; set; }
    }

    public class ImporterSessionResultsIds
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
        public string option { get; set; }
    }

    public class ImporterSessionInputIds
    {
        public List<string> locations { get; set; }
        public List<string> providers { get; set; }
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

    public class ClientsSessionsDetailsAdmin
    {
        public List<ClientBlobsAdmin> ClientBlobsList { get; set; }
    }

    public class ClientBlobsAdmin
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSessionId { get; set; }
        public List<BlobDetailsAdmin> ClientBlobsList { get; set; }
    }

    public class BlobDetailsAdmin
    {
        public string BlobName { get; set; }
        public string? BlobModifiedDate { get; set; }
        public string BlobProvidersCount { get; set; }
        public string BlobLocationsCount { get; set; }
        public string BlobProvidersCountPass { get; set; }
        public string BlobLocationsCountPass { get; set; }
        public StatusEnum Status { get; set; }
        public bool HasProviders { get; set; }
        public bool HasLocations { get; set; }
        public string SessionInputSize { get; internal set; }
        public string SessionBackupSize { get; internal set; }
    }

    /*
    public class LocationEntry
    {
        public string LocationName { get; set; }
        public string MessageType { get; set; }
        public string Address1 { get; set; }
        public string ClientId { get; set; }
        public string MarketEntityId { get; set; }
        public string TrackingId { get; set; }
        public object Error { get; set; }
        public string JsonLocation { get; set; }

    }

    public class ProviderEntry
    {
        public ProviderEntry()
        {

        }
        public ProviderEntry(
            object record,
            string trackingId,
            MessageTypeEnum messageType,
            string message,
            Dictionary<string, string> trackingFields)
        {
            TrackingId = trackingId;
            MessageType = messageType.ToString();
            Error = message;
            JsonProvider = record;
            var index = 7;

            foreach (var keyValuePair in trackingFields)
            {
                var propertyInfo = GetType().GetProperty(keyValuePair.Key);

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, keyValuePair.Value, null);
                }

                index++;
            }

        }

        public string Npi { get; set; }
        public string MessageType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MarketEntityId { get; set; }
        public string LocationName { get; set; }
        public string TrackingId { get; set; }
        public object Error { get; set; }
        public object JsonProvider { get; set; }
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

        public List<LocationEntry> Locations { get; set; }
    }

    public class ProviderGroupEntry
    {
        public string GroupId { get; set; }
        public string Error { get; set; }
        public List<ProviderEntry> Providers { get; set; }

    }

    public class LocationGroupEntry
    {
        public string GroupId { get; set; }
        public string Error { get; set; }
        public List<LocationEntry> Locations { get; set; }

    }

    public class ProviderLocationGroupEntry
    {
        public string GroupId { get; set; }

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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int TotalNumberOfLocationsReceived { get; set; }
        public int TotalNumberOfLocationsFailedValidation { get; set; }
        public int TotalNumberOfLocationsFailedOnCreateConnectLocations { get; set; }
        public int TotalNumberOfLocationsReadyForConnectBulkInsert { get; set; }
        public int TotalNumberOfLocationsFailedOnConnectBulkInsert { get; set; }
        public int TotalNumberOfLocationsInsertedUsingConnectBulkInsert { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        public int TotalNumberOfProviderLocationsReceived { get; set; }
        public int TotalNumberOfProviderLocationsFailedValidation { get; set; }
        public int TotalNumberOfProviderLocationsFailedOnCreateConnectLocations { get; set; }
        public int TotalNumberOfProviderLocationsReadyForConnectBulkInsert { get; set; }
        public int TotalNumberOfProviderLocationsFailedOnConnectBulkInsert { get; set; }
        public int TotalNumberOfProviderLocationsInsertedUsingConnectBulkInsert { get; set; }
        //public int TotalNumberOfProviderLocationsFailedOnCreateProvidersConnectLocations { get; set; }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        public int TotalNumberOfProvidersReceived { get; set; }
        public int TotalNumberOfProvidersFailedValidation { get; set; }
        public int TotalNumberOfProvidersFailedOnCreateConnectProviders { get; set; }
        public int TotalNumberOfProvidersReadyForConnectBulkInsert { get; set; }
        public int TotalNumberOfProvidersFailedOnConnectBulkInsert { get; set; }
        public int TotalNumberOfProvidersInsertedUsingConnectBulkInsert { get; set; }



    }

    public class ProvidersSPECTableColumnsCaptions
    {
        public string Column1 { get; set; }
    }

    public class MenuDetails
    {
        public string MenuClientSessionsCaption { get; set; }
        public string MenuClientToolsCaption { get; set; }
        public List<MenuClientSessionDetails> MenuClientSessionsItems { get; set; }
        public List<MenuClientToolsDetails> MenuClientToolsItems { get; set; }
    }
    public class MenuClientToolsDetails
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }

    public class MenuClientSessionDetails
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
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
        public List<BlobDetailsAdmin> ClientBlobsList { get; set; }
    }
    public class BlobDetailsAdmin
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
    }
    */

}