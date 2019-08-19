FROM mcr.microsoft.com/dotnet/core/sdk:1.1

WORKDIR /artifacts

COPY . /artifacts

EXPOSE 80

CMD ["dotnet", "WebApi.dll"]
