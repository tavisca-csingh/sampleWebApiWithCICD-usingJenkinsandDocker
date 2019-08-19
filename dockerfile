
FROM mcr.microsoft.com/dotnet/core/aspnet

WORKDIR /artifacts

COPY . /artifacts

EXPOSE 80

CMD ["dotnet", "WebApi.dll"]
