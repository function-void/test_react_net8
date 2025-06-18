using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementApp.Presentation;

[Route(ApiConfigurationSettings.API_DEFAULT_ROUTE)]
[ApiVersion("1.0")]
[ApiController]
public abstract class ApiController : ControllerBase;