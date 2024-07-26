# Sample code on how to Unit Test the `Run` Azure Functions (http)
.Net 7 Azure functions (http) uses `HttpRequestData` and `HttpResponseData`.
These responses uses inbuilt extensions, which cannot be mocked.
## Requirements

* .NET 7 Azure function
* Moq Package or Similar

## Usage

Check `FunctionUnitTest` Project for sample unit tests
