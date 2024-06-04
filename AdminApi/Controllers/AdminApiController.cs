using System.Diagnostics;
using Importer.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminApiController : ControllerBase
{
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
        
        Thread.Sleep(10000);

        var importerBlobStorage = new ImporterSessionResults();

        return importerBlobStorage;
    }

    [HttpPost]
    [Route("ImportFromInputData")]

    public ImporterSessionResults ImportFromInputData(object importerSessionInputIds, TargetEnvironment targetEnvironment)    
    {
        Thread.Sleep(10000);

        return new ImporterSessionResults();
    }
    

    [HttpGet]
    [Route("GetMenu")]
    public Menu? GetMenu()
    {
        var json = ResourceReader.ReadEmbeddedResource("Menu.json");

        return JsonConvert.DeserializeObject<Menu>(json);
    }

    [HttpGet]
    [Route("GetSessions")]
    public ClientsSessionsDetailsAdmin? GetSessions(string? clientId = null)
    {
        var json = ResourceReader.ReadEmbeddedResource("Sessions.json");

        return JsonConvert.DeserializeObject<ClientsSessionsDetailsAdmin>(json);
    }

    [HttpPost]
    [Route("DownloadSessionResults")]
    public IActionResult DownloadSessionResults()
    {
        return GetFileDownload("AdminApi.Data.DownloadResults.json");
    }

    [HttpPost]
    [Route("DownloadInput")]
    public IActionResult DownloadInput(string? clientId = null, string? sessionId = null)
    {
        return GetFileDownload("AdminApi.Data.DownloadInput.json");
    }

    [HttpPost]
    [Route("DownloadLiveInput")]
    public IActionResult DownloadLiveInput(string clientId)
    {
        return GetFileDownload("AdminApi.Data.DownloadInputLive.json");
    }

    [HttpPost]
    [Route("DownloadBackup")]
    public IActionResult DownloadBackup(string? clientId = null, string? sessionId = null)
    {
        return GetFileDownload("AdminApi.Data.DownloadBackup.json");
    }

    [HttpPost]
    [Route("RestoreBackup")]
    public IActionResult RestoreBackup(string clientId, string sessionId)
    {
        return GetFileDownload("AdminApi.Data.DownloadBackup.json");
    }

    [HttpPost]
    [Route("OpenHelpUrl")]
    public IActionResult OpenHelpUrl(string? clientId = null, string? sessionId = null)
    {
        var url = "https://www.cnn.com/";
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });

        return Ok();
    }    
    
    [HttpGet]
    [Route("GetSessionResults")]
    public ImporterSessionResults? GetSessionResults(string? clientId = null, string? sessionId = null)
    {
        string json = ResourceReader.ReadEmbeddedResource("SessionResults.json");

        return JsonConvert.DeserializeObject<ImporterSessionResults>(json);
    }

    [HttpGet]
    [Route("GetSessionInputEntryJsonValue")]
    public string GetInputEntryJsonValue(string trackingId, JsonRestoreType jsonRestoreType, string? clientId = null, string? sessionId = null)
    {
        Task.Delay(10000);

        var json = ResourceReader.ReadEmbeddedResource("SessionInput.json");

        var importerSessionInput = JsonConvert.DeserializeObject<ImporterSessionInput>(json);

        return importerSessionInput?.GetRestoreJsonValue(trackingId, jsonRestoreType) ?? string.Empty;
    }
    
    
    [HttpGet]
    [Route("GetSessionInput")]
    public ImporterSessionInputLite GetSessionInput(string? clientId = null, string? sessionId = null)
    {
        var json = ResourceReader.ReadEmbeddedResource("SessionInput.json");

        var importerSessionInput = JsonConvert.DeserializeObject<ImporterSessionInput>(json);
        
        return importerSessionInput?.GetSessionInputData() ?? new ImporterSessionInputLite();        
    }

    [HttpGet]
    [Route("GetDataFromLiveFeed")]
    public ImporterSessionInputLite GetDataFromLiveFeed(string? clientId = null)
    {
        string json = ResourceReader.ReadEmbeddedResource("SessionInput.json");

        var importerSessionInput = JsonConvert.DeserializeObject<ImporterSessionInput>(json);

        return importerSessionInput?.GetSessionInputData() ?? new ImporterSessionInputLite();
    }


    [HttpPost]
    [Route("RestoreBackupFromFromSession")]
    public IActionResult RestoreBackupFromFromSession(
        string clientId,
        string sessionId,
        RestoreType restoreType, 
        [FromQuery] List<string>? locationsGroups = null,
        [FromQuery] List<string>? providersGroups = null)    
    {
        Task.Delay(10000);        

        return Ok();
    }
    
    #region Private

    private IActionResult GetFileDownload(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceStream = assembly.GetManifestResourceStream(resourceName);

        if (resourceStream == null)
        {
            return NotFound();
        }

        return File(resourceStream, "application/octet-stream", "DownloadBackup.json");
    }

    #endregion
}