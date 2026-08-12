FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build

EXPOSE 5200

CMD ["dotnet", "run", "--urls", "http://0.0.0.0:5200"]