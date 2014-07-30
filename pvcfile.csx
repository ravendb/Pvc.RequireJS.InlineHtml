pvc.Task("nuget-push", () => {
    pvc.Source("src/Pvc.TypeScript.csproj")
       .Pipe(new PvcNuGetPack(
            createSymbolsPackage: true
       ))
       .Pipe(new PvcNuGetPush());
});
