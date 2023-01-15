FROM mcr.microsoft.com/dotnet/sdk:6.0 as publish

WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /src/out


FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=publish /src/out .

# Kestrel cannot run as root on Kubernetes, and therefore cannot expose port 80
ENV ASPNETCORE_URLS=http://+:8000

ENTRYPOINT ["dotnet", "FoodDelivery.FE.dll"]
