var project = "src/HelloLibrary/HelloLibrary.csproj";
var publishDir = "publish";

Task("Pack").Does(() => {
    CleanDirectory(publishDir);
    DotNetCorePack(project, new DotNetCorePackSettings {
        OutputDirectory = publishDir
    });
});

Task("Publish")
    .IsDependentOn("Pack")
    .Does(() => {
        var apiKey = EnvironmentVariable("NAPI");
        var nupkg = new System.IO.DirectoryInfo(publishDir).GetFiles("*.nupkg").LastOrDefault();
            var package = nupkg.FullName;
            NuGetPush(package, new NuGetPushSettings {
                Source = "https://www.nuget.org/api/v2/package",
                ApiKey = apiKey
        });
    });

var target = Argument("target", "Pack");
RunTarget(target);